using System;
using System.Linq;
using EmailSender;
using Model;
using System.Data.Objects;


using Repository.Implementations;

namespace AdoPets.Notifier
{
    public class CalendarNotifier
    {
        public CalendarNotifier(UnitOfWork uow, MailSenderHelper mailSender)
        {
            UnitOfWork = uow;
            MailSender = mailSender;
        }

        private UnitOfWork UnitOfWork { get; set; }

        public MailSenderHelper MailSender { get; set; }


        public void Notify()
        {
            var tomorrow = DateTime.Today.AddDays(1);
            var reminders = UnitOfWork.CalendarRepository.GetAll(i => i.SendNotificationMail && i.Date.HasValue && EntityFunctions.DiffDays(i.Date.Value, tomorrow) == 0, null, i => i.User).ToList();
            foreach (var reminder in reminders)
            {
                var owner = reminder.User;
                if (owner != null && owner.TimeZoneId != null)
                {
                    var userTimeZoneInfoId = UnitOfWork.TimeZoneRepository.GetTimeZoneInfoId(owner.TimeZoneId.Value);

                    var userTomorrowDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(tomorrow, userTimeZoneInfoId);

                    if ((userTomorrowDate - tomorrow).Days == 0 && !HasBeenSent(reminder.Id.ToString()))
                    {
                        var mailSuccess = MailSender.SendReminderNotification(owner.Email, owner.FirstName, owner.LastName, reminder.Reason, reminder.Date.Value, reminder.Physician, reminder.Comment);
                        var notification = new Notification
                        {
                            NotificationTypeId = NotificationTypeEnum.Reminder,
                            UserId = owner.Id,
                            UserEmail = owner.Email,
                            Date = DateTime.Now,
                            Succeed = mailSuccess,
                            ExtraValue = reminder.Id.ToString()
                        };
                        UnitOfWork.NotificationRepository.Insert(notification);
                        UnitOfWork.Save();

                    }
                }
            }
        }

        private bool HasBeenSent(string reminderId)
        {
            var notification = UnitOfWork.NotificationRepository.GetSingle(n => n.Date == DateTime.Today && n.NotificationTypeId == NotificationTypeEnum.Reminder && n.ExtraValue == reminderId);
            return notification != null;
        }
    }
}
