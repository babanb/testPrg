using System.Configuration;
using System.Threading;
using EmailSender;
using Repository.Implementations;

namespace AdoPets.Notifier
{
    public class BaseNotifier
    {
        public BaseNotifier()
        {           
                string culture = ConfigurationManager.AppSettings[Constants.Culture];
            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        public void Notify()
        {
            var uow = new UnitOfWork();
            var emailSender = new MailSenderHelper(ConfigurationManager.AppSettings[Constants.LogoPath], ConfigurationManager.AppSettings[Constants.SignaturePath]);
            
            var medicationNotifier = new MedicationNotifier(uow, emailSender);
            medicationNotifier.Notify();

            var insuranceNotifier = new InsuranceNotifier(uow, emailSender);
            insuranceNotifier.Notify();

            var calenderNotifier = new CalendarNotifier(uow, emailSender);
            calenderNotifier.Notify();

            uow.Dispose();
        }
    }
}
