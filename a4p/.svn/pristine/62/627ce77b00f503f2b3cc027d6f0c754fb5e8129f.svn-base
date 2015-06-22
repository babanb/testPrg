using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
namespace ADOPets.Web.ViewModels.Medication
{
    public class AddViewModel
    {
        public AddViewModel()
        {

        }

        public AddViewModel(int petid)
        {
            PetId = petid;
        }

        public int PetId { get; set; }

        [Display(Name = "Medication_Add_MedicationName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Medication_Add_MedicationNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string MedicationName { get; set; }

        [Display(Name = "Medication_Add_Reason", ResourceType = typeof(Wording))]
        public string Reason { get; set; }


        [Display(Name = "Medication_Add_VisitDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Medication_Add_VisitDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? VisitDate { get; set; }

        [Display(Name = "Medication_Add_Status", ResourceType = typeof(Wording))]
        public MedicationStatusEnum? Status { get; set; }

        [Display(Name = "Medication_Add_Duration", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Medication_Add_DurationRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Medication_Add_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        [Range(typeof(int), "0", "999", ErrorMessageResourceName = "Medication_Add_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public int Duration { get; set; }

        [Display(Name = "Medication_Add_HowOften", ResourceType = typeof(Wording))]
        public string HowOften { get; set; }

        [Display(Name = "Medication_Add_Dosage", ResourceType = typeof(Wording))]
        public string Dosage { get; set; }


        [Display(Name = "Medication_Add_Route", ResourceType = typeof(Wording))]
        public string Route { get; set; }

        [Display(Name = "Medication_Add_Veterinarian", ResourceType = typeof(Wording))]
        public string Veterinarian { get; set; }

        [Display(Name = "Medication_Add_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        [Display(Name = "Medication_Add_SendReminderMail", ResourceType = typeof(Wording))]
        public Boolean SendReminderMail { get; set; }

        public PetMedication Map()
        {
            var petMedication = new PetMedication
            {
                CustomMedication = MedicationName,
                Reason = Reason,
                VisitDate = VisitDate.Value,
                MedicationStatusId = Status,
                Duration = Duration,
                Frequency = HowOften,
                Route = Route,
                Dosage=Dosage,
                Physician = new EncryptedText(Veterinarian),
                SendReminderMail = Convert.ToBoolean(SendReminderMail),
                Comment = new EncryptedText(Comment),
                PetId = PetId
            };

            return petMedication;
        }
    }
}