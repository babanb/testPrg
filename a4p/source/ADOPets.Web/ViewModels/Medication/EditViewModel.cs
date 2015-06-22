using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Medication
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(Model.PetMedication medication)
        {
            Id = medication.Id;
            MedicationName = medication.CustomMedication;
            Reason = medication.Reason;
            VisitDate = medication.VisitDate;
            Status = medication.MedicationStatusId;
            Duration = medication.Duration;
            HowOften = medication.Frequency;
            Dosage = medication.Dosage;
            Route = medication.Route;
            Veterinarian = medication.Physician;
            Comment = medication.Comment;
            SendReminderMail = Convert.ToBoolean(medication.SendReminderMail);
            PetId = medication.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "Medication_Edit_MedicationName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Medication_Edit_MedicationNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string MedicationName { get; set; }

        [Display(Name = "Medication_Edit_Reason", ResourceType = typeof(Wording))]
        public string Reason { get; set; }

        [Display(Name = "Medication_Edit_VisitDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Medication_Edit_VisitDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? VisitDate { get; set; }

        [Display(Name = "Medication_Edit_Status", ResourceType = typeof(Wording))]
        public MedicationStatusEnum? Status { get; set; }

        [Display(Name = "Medication_Edit_Duration", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Medication_Edit_DurationRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Medication_Add_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        [Range(typeof(int), "0", "999", ErrorMessageResourceName = "Medication_Add_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public int Duration { get; set; }

        [Display(Name = "Medication_Edit_HowOften", ResourceType = typeof(Wording))]
        public string HowOften { get; set; }

        [Display(Name = "Medication_Edit_Dosage", ResourceType = typeof(Wording))]
        public string Dosage { get; set; }

        [Display(Name = "Medication_Edit_Route", ResourceType = typeof(Wording))]
        public string Route { get; set; }

        [Display(Name = "Medication_Edit_Veterinarian", ResourceType = typeof(Wording))]
        public string Veterinarian { get; set; }

        [Display(Name = "Medication_Edit_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        [Display(Name = "Medication_Edit_SendReminderMail", ResourceType = typeof(Wording))]
        public bool SendReminderMail { get; set; }

        public int PetId { get; set; }

        public void Map(PetMedication medication)
        {
            medication.CustomMedication = MedicationName;
            medication.Reason = Reason;
            medication.VisitDate = VisitDate.Value;
            medication.MedicationStatusId = Status;
            medication.Duration = Duration;
            medication.Frequency = HowOften;
            medication.Dosage = Dosage;
            medication.Route = Route;
            medication.Physician = new EncryptedText(Veterinarian);
            medication.Comment = new EncryptedText(Comment);
            medication.SendReminderMail = SendReminderMail;
        }
    }
}