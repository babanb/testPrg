using System;
using System.Configuration;
using System.Linq;
using EmailSender;
using Model.Tools;
using Repository.Implementations;

namespace Hsbc.NotifierActivator
{
    class Program
    {
        private static UnitOfWork uow;

        static void Main(string[] args)
        {
            using (uow = new UnitOfWork())
            {
                GetHsbcUsersToNotify();

                GetGrouponUsersToNotify();
            }
        }

        /// <summary>
        /// Gets the HSBC users wich have not received the Welcome and Subscription mail. Send the emails.
        /// </summary>
        private static void GetHsbcUsersToNotify()
        {
            var userSubscriptions = uow.UserSubscriptionRepository.GetAllSingleTracking(us => !us.SubscriptionMailSent && us.Subscription.PromotionCode == "HSBC" && us.StartDate <= DateTime.Today,
                    null, us => us.Subscription, us => us.Users);
            
            var emailSender = new MailSenderHelper(ConfigurationManager.AppSettings[Constants.LogoForEmailPath], ConfigurationManager.AppSettings[Constants.SignaturePath]);

            foreach (var us in userSubscriptions)
            {
                var user = us.Users.First();
                emailSender.SendHsbcWelcomeMail(user.Email, user.FirstName, user.LastName, user.UserSubscription.Subscription.Name, user.UserSubscription.Subscription.PromotionCode);

                var credentials = uow.LoginRepository.GetSingle(l => l.UserId == user.Id);
                var userName = Encryption.Decrypt(credentials.UserName);

                emailSender.SendHsbcAccountActivationMail(user.Email, user.FirstName, user.LastName, userName, userName);

                emailSender.SendHsbcToSupportSubscriptionMail(user.FirstName, user.LastName, user.Email, user.UserSubscription.Subscription.PromotionCode, user.UserSubscription.Subscription.Name, 
                    user.UserSubscription.Subscription.Amount.ToString(), user.UserSubscription.Subscription.PromotionCode);

                us.SubscriptionMailSent = true;
                uow.UserSubscriptionRepository.Update(us);
                uow.Save();
            }
           
        }

        private static void GetGrouponUsersToNotify()
        {
            var userSubscriptions = uow.UserSubscriptionRepository.GetAllSingleTracking(us => !us.SubscriptionMailSent && us.Subscription.PromotionCode == "GRPN-111914" && us.StartDate <= DateTime.Today,
                    null, us => us.Subscription, us => us.Users);

            var emailSender = new MailSenderHelper(ConfigurationManager.AppSettings[Constants.LogoForEmailPath], ConfigurationManager.AppSettings[Constants.SignaturePath], ConfigurationManager.AppSettings[Constants.MraFormPdfPath]);

            foreach (var us in userSubscriptions)
            {
                var user = us.Users.First();
                emailSender.SendWelcomeMail(user.Email, user.FirstName, user.LastName, user.UserSubscription.Subscription.Name, user.UserSubscription.Subscription.PromotionCode);

                var credentials = uow.LoginRepository.GetSingle(l => l.UserId == user.Id);
                var userName = Encryption.Decrypt(credentials.UserName);

                emailSender.SendAccountActivationMail(user.Email, user.FirstName, user.LastName, userName, userName);

                emailSender.SendToSupportSubscriptionMail(user.FirstName, user.LastName, user.Email, user.UserSubscription.Subscription.PromotionCode, user.UserSubscription.Subscription.Name,
                    user.UserSubscription.Subscription.Amount.ToString(), user.UserSubscription.Subscription.PromotionCode);

                us.SubscriptionMailSent = true;
                uow.UserSubscriptionRepository.Update(us);

                uow.Save();
            }
        }

    }
}
