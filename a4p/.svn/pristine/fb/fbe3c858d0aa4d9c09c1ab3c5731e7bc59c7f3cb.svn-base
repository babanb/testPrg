using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
namespace ADOPets.Web.ViewModels.Medication
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(Model.PetMedication medication)
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
            SendReminderMail = medication.SendReminderMail;
        }

        public int Id { get; set; }

        [Display(Name = "Medication_Delete_MedicationName", ResourceType = typeof(Wording))]
        public string MedicationName { get; set; }

        [Display(Name = "Medication_Delete_Reason", ResourceType = typeof(Wording))]
        public string Reason { get; set; }

        [Display(Name = "Medication_Delete_VisitDate", ResourceType = typeof(Wording))]
        public DateTime VisitDate { get; set; }

        [Display(Name = "Medication_Delete_Status", ResourceType = typeof(Wording))]
        public MedicationStatusEnum? Status { get; set; }

        [Display(Name = "Medication_Delete_Duration", ResourceType = typeof(Wording))]
        public int Duration { get; set; }

        [Display(Name = "Medication_Delete_HowOften", ResourceType = typeof(Wording))]
        public string HowOften { get; set; }

        [Display(Name = "Medication_Delete_Dosage", ResourceType = typeof(Wording))]
        public string Dosage { get; set; }

        [Display(Name = "Medication_Delete_Route", ResourceType = typeof(Wording))]
        public string Route { get; set; }

        [Display(Name = "Medication_Delete_Veterinarian", ResourceType = typeof(Wording))]
        public string Veterinarian { get; set; }

        [Display(Name = "Medication_Delete_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        [Display(Name = "Medication_Delete_SendReminderMail", ResourceType = typeof(Wording))]
        public Boolean SendReminderMail { get; set; }

        public int PetId { get; set; }
    }
}