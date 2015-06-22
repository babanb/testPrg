using System;
using ADOPets.Web.Common.Helpers;
using Model;

namespace ADOPets.Web.ViewModels.Medication
{
    public class IndexViewModel
    {
        public IndexViewModel()
        { }

        public IndexViewModel(PetMedication medication)
        {
            Id = medication.Id;
            Name = medication.MedicationId == null
                ? medication.CustomMedication
                : EnumHelper.GetResourceValueForEnumValue(medication.MedicationId);
            Reason = medication.Reason;
            VisitDate = medication.VisitDate;
            Status = medication.MedicationStatusId;
            Duration = medication.Duration;
            HowOften = medication.Frequency;
            Dosage = medication.Dosage;
            Route = medication.Route;
            Veterinarian = medication.Physician;
            Comment = medication.Comment;
            SendReminderMail = medication.SendReminderMail;
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Reason { get; set; }
        public MedicationStatusEnum? Status { get; set; }
        public DateTime VisitDate { get; set; }
        public int Duration { get; set; }
        public string HowOften { get; set; }
        public string Dosage { get; set; }
        public string Route { get; set; }
        public string Veterinarian { get; set; }
        public string Comment { get; set; }
        public bool SendReminderMail { get; set; }
    }
}