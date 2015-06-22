using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Insurance
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(Model.Insurance insurance)
        {
            Id = insurance.Id;
            Name = insurance.Name;
            AccountNumber = insurance.AccountNumber;
             StartDate = insurance.StartDate;
            EndDate = insurance.EndDate;
            Phone = insurance.Phone;
            Comment = insurance.Comment;
            NotificationMail = insurance.SendNotificationMail;
            PetId = insurance.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "Insurance_Edit_Name", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Insurance_Edit_NameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "Insurance_Edit_AccountNumber", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Insurance_Edit_AccountNumberRequired", ErrorMessageResourceType = typeof(Wording))]
        public string AccountNumber { get; set; }

        [Display(Name = "Insurance_Edit_StartDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Insurance_Edit_StartDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime StartDate { get; set; }

        [Display(Name = "Insurance_Edit_EndDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Insurance_Edit_EndDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Insurance_Edit_Phone", ResourceType = typeof(Wording))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "Insurance_Edit_PhonNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Phone { get; set; }

        [Display(Name = "Insurance_Edit_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        [Display(Name = "Insurance_Edit_NotificationMail", ResourceType = typeof(Wording))]
        public bool NotificationMail { get; set; }

        public int PetId { get; set; }

        public void Map(Model.Insurance insurance)
        {
            insurance.Name = Name;
            insurance.AccountNumber = new EncryptedText(AccountNumber);
            insurance.StartDate = StartDate;
            insurance.EndDate = EndDate;
            insurance.Phone = Phone;
            insurance.Comment = new EncryptedText(Comment);
            insurance.SendNotificationMail = NotificationMail;
        }
    }
}