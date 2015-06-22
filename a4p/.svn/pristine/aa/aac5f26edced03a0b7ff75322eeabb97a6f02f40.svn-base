using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources.Contact;
using Model;


namespace ADOPets.Web.ViewModels.Contact
{
    public class AddViewModel
    {
        public AddViewModel()
        {
          
        }

        public int UserId { get; set; }

        [Display(Name = "Relationship", ResourceType = typeof(Add))]
        [Required(ErrorMessageResourceName = "RelationshipRequired", ErrorMessageResourceType = typeof(Add))]
        public ContactTypeEnum Relationship { get; set; }

        [Display(Name = "IsEmergencyContact", ResourceType = typeof(Add))]
        public bool IsEmergencyContact { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Add))]
        [Required(ErrorMessageResourceName = "LastNameRequired", ErrorMessageResourceType = typeof(Add))]
        public string LastName { get; set; }

        [Display(Name = "MiddleName", ResourceType = typeof(Add))]
        public string MiddleName { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Add))]
        [Required(ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(Add))]
        public string FirstName { get; set; }

        [Display(Name = "Home", ResourceType = typeof(Add))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "HomeRequiredNumber", ErrorMessageResourceType = typeof(Add))]
        public string Home { get; set; }

        [Display(Name = "Office", ResourceType = typeof(Add))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "OfficeRequiredNumber", ErrorMessageResourceType = typeof(Add))]
        public string Office { get; set; }

        [Display(Name = "Cell", ResourceType = typeof(Add))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "CellRequiredNumber", ErrorMessageResourceType = typeof(Add))]
        public string Cell { get; set; }

        [Display(Name = "Fax", ResourceType = typeof(Add))]
        [RegularExpression(@"[0-9\-. ]+", ErrorMessageResourceName = "FaxRequiredNumber", ErrorMessageResourceType = typeof(Add))]
        public string Fax { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Add))]
        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(Add))]
        public string Email { get; set; }

        public Model.Contact Map(int userId)
        {
            var contact = new Model.Contact
            {
                ContactTypeId = Relationship,
                IsEmergencyContact = false,
                FirstName = new EncryptedText(FirstName),
                LastName = new EncryptedText(LastName),
                MiddleName = new EncryptedText(MiddleName),
                PhoneHome = new EncryptedText(Home),
                PhoneOffice = new EncryptedText(Office),
                PhoneCell = new EncryptedText(Cell),
                Fax = new EncryptedText(Fax),
                Email = new EncryptedText(Email),
                UserId=userId
            };
            return contact;
        }

    }
}

