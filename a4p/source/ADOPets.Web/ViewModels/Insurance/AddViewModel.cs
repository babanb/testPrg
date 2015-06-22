using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Insurance
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


        [Display(Name = "Insurance_Add_Name", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Insurance_Add_NameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "Insurance_Add_AccountNumber", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Insurance_Add_AccountNumberRequired", ErrorMessageResourceType = typeof(Wording))]
        public string AccountNumber { get; set; }

        [Display(Name = "Insurance_Add_StartDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Insurance_Add_StartDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime StartDate { get; set; }

        [Display(Name = "Insurance_Add_EndDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Insurance_Add_EndDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Insurance_Add_Phone", ResourceType = typeof(Wording))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "Insurance_Add_PhonNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string Phone { get; set; }

        [Display(Name = "Insurance_Add_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        [Display(Name = "Insurance_Add_NotificationMail", ResourceType = typeof(Wording))]
        public bool NotificationMail { get; set; }

        public Model.Insurance Map()
        {
            var petInsurance = new Model.Insurance
            {
                Name = Name,
                AccountNumber = new EncryptedText(AccountNumber),
                StartDate = StartDate,
                EndDate = EndDate,
                Phone =Phone,
                Comment = new EncryptedText(Comment),
                SendNotificationMail = NotificationMail,
                PetId = PetId
            };

            return petInsurance;
        }
    }
}