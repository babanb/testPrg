using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.Insurance
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(Model.Insurance insurance)
        {
            Id = insurance.Id;
            Name = insurance.Name;
            AccountNumber = insurance.AccountNumber;
            StartDate = insurance.StartDate;
            EndDate = insurance.EndDate;
            Phone = insurance.Phone;
            Comment = insurance.Comment;
            NotificationMail = insurance.SendNotificationMail;
        }

        public int Id { get; set; }

        [Display(Name = "Insurance_Delete_Name", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "Insurance_Delete_AccountNumber", ResourceType = typeof(Wording))]
        public string AccountNumber { get; set; }

        [Display(Name = "Insurance_Delete_StartDate", ResourceType = typeof(Wording))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Insurance_Delete_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Insurance_Delete_Phone", ResourceType = typeof(Wording))]
        public string Phone { get; set; }

        [Display(Name = "Insurance_Delete_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        [Display(Name = "Insurance_Delete_NotificationMail", ResourceType = typeof(Wording))]
        public bool NotificationMail { get; set; }

        public int PetId { get; set; }
    }
}