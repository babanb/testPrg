using System;
using System.Data.Objects;
using System.Linq;
using EmailSender;
using Model;

using Repository.Implementations;

namespace AdoPets.Notifier
{
    public class InsuranceNotifier
    {
        public InsuranceNotifier(UnitOfWork uow, MailSenderHelper mailSender)
        {
            UnitOfWork = uow;
            MailSender = mailSender;
        }

        private UnitOfWork UnitOfWork { get; set; }

        public MailSenderHelper MailSender { get; set; }


        public void Notify()
        {
            var tomorrow = DateTime.Today.AddDays(1);
            var insurances = UnitOfWork.InsuranceRepository.GetAll(i => i.SendNotificationMail && EntityFunctions.DiffDays(i.EndDate, tomorrow) == 0, null, i => i.Pet.Users).ToList();
            foreach (var insurance in insurances)
            {
                var owner = insurance.Pet.Users.FirstOrDefault(u => u.UserTypeId == UserTypeEnum.OwnerAdmin);
                if (owner != null && owner.TimeZoneId != null)
                {
                    var userTimeZoneInfoId = UnitOfWork.TimeZoneRepository.GetTimeZoneInfoId(owner.TimeZoneId.Value);

                    var userTomorrowDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(tomorrow, userTimeZoneInfoId);

                    if ((userTomorrowDate - tomorrow).Days == 0 && !HasBeenSent(insurance.Id.ToString()))
                    {
                        var mailSuccess = MailSender.SendInsuranceNotification(owner.Email, owner.FirstName, owner.LastName, insurance.Name, insurance.AccountNumber, insurance.StartDate,
                                           insurance.EndDate, insurance.Phone, insurance.Comment);
                        var notification = new Notification
                        {
                            NotificationTypeId = NotificationTypeEnum.Insurance,
                            UserId = owner.Id,
                            UserEmail = owner.Email,
                            Date = DateTime.Now,
                            Succeed = mailSuccess,
                            ExtraValue = insurance.Id.ToString()
                        };
                        UnitOfWork.NotificationRepository.Insert(notification);
                        UnitOfWork.Save();
                    }
                }
            }
        }

        private bool HasBeenSent(string insuranceId)
        {
            var notification = UnitOfWork.NotificationRepository.GetSingle(n => n.Date == DateTime.Today && n.NotificationTypeId == NotificationTypeEnum.Insurance && n.ExtraValue == insuranceId);
            return notification != null;
        }
    }
}
