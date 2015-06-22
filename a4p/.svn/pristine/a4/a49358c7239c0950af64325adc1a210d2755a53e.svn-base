using System;
using System.Data.Objects;
using System.Linq;
using AdoPets.Notifier.Helpers;
using EmailSender;
using Model;
using Repository.Implementations;

namespace AdoPets.Notifier
{
    public class MedicationNotifier
    {
        public MedicationNotifier(UnitOfWork uow, MailSenderHelper mailSender)
        {
            UnitOfWork = uow;
            MailSender = mailSender;
        }

        private UnitOfWork UnitOfWork { get; set; }

        public MailSenderHelper MailSender { get; set; }

        public void Notify()
        {
            var tomorrow = DateTime.Today.AddDays(1);
            var medications = UnitOfWork.PetMedicationRepository.GetAll(m => m.SendReminderMail && EntityFunctions.DiffDays(EntityFunctions.AddDays(m.VisitDate, m.Duration), tomorrow) == 0, null, m => m.Pet.Users).ToList();
            foreach (var medication in medications)
            {
                var owner = medication.Pet.Users.FirstOrDefault(u => u.UserTypeId == UserTypeEnum.OwnerAdmin);
                if (owner != null && owner.TimeZoneId != null)
                {
                    var userTimeZoneInfoId = UnitOfWork.TimeZoneRepository.GetTimeZoneInfoId(owner.TimeZoneId.Value);

                    var userTomorrowDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(tomorrow, userTimeZoneInfoId);

                    if ((userTomorrowDate - tomorrow).Days == 0 && !HasBeenSent(medication.Id.ToString()))
                    {
                        var mailSuccess = MailSender.SendMedicationNotification(owner.Email, owner.FirstName, owner.LastName, medication.CustomMedication, EnumHelper.GetResourceValueForEnumValue(medication.MedicationStatusId),
                            medication.VisitDate, medication.Duration, medication.Frequency, medication.Dosage, medication.Physician);
                        var notification = new Notification
                        {
                            NotificationTypeId = NotificationTypeEnum.Medication,
                            UserId = owner.Id,
                            UserEmail = owner.Email,
                            Date = DateTime.Now,
                            Succeed = mailSuccess,
                            ExtraValue = medication.Id.ToString()
                        };
                        UnitOfWork.NotificationRepository.Insert(notification);
                        UnitOfWork.Save();
                    }
                }
            }
        }

        private bool HasBeenSent(string medicationId)
        {
            var notification = UnitOfWork.NotificationRepository.GetSingle(n => n.Date == DateTime.Today && n.NotificationTypeId == NotificationTypeEnum.Medication && n.ExtraValue == medicationId);
            return notification != null;
        }
    }
}
