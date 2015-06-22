using System;
using System.Configuration;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using System.Threading;
using EmailSender;
using Model;
using Repository.Implementations;
using System.IO;

namespace Subscription.NotifierActivator
{
    class Program
    {
        static void Main(string[] args)
        {
            var uow = new UnitOfWork();
            var mailSender = new MailSenderHelper(ConfigurationManager.AppSettings[Constants.LogoForEmailPath], ConfigurationManager.AppSettings[Constants.SignaturePath]);

            //this solution is very tight to HSBC, the day we implement the others alert emails (20, 30 days before, etc), this method could be refactored
            SendHsbcTenDaysBeforeSubscriptionNotification(uow, mailSender);

            uow.Save();
        }

        private static void SendHsbcTenDaysBeforeSubscriptionNotification(UnitOfWork uow, MailSenderHelper mailSender)
        {
            var noOfEmailSent = 0;
            var userSubscription = uow.UserSubscriptionRepository.GetAllSingleTracking(us =>
                us.Subscription.PromotionCode == "HSBC" &&
                EntityFunctions.DiffDays(DateTime.Today, us.RenewalDate.Value).Value <= 10 && us.SubscriptionExpirationAlertId == SubscriptionExpirationAlertEnum.NotSent,
                null,
                us => us.Users, us => us.Subscription);

            foreach (var us in userSubscription)
            {
                var user = us.Users.First();
                var sent = mailSender.SendHsbcSubscriptionExpirationAlert(user.Email, user.FirstName, user.LastName);
                if (sent)
                {
                    noOfEmailSent = noOfEmailSent + 1;
                    us.SubscriptionExpirationAlertId = SubscriptionExpirationAlertEnum.TenDaysBefore;
                    uow.UserSubscriptionRepository.Update(us);
                }
            }

            if (noOfEmailSent > 0)
            {
                var filePath = ConfigurationManager.AppSettings[Constants.FilePath];

                string fileName = filePath + "Pet_log_Task.txt";

                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Close();

                StreamWriter sw = File.AppendText(fileName);
                sw.WriteLine(DateTime.Now.Date + ",  HSBC trial period expire : " + noOfEmailSent + " emails sent");
                sw.Close();
            }
        }
    }
}
