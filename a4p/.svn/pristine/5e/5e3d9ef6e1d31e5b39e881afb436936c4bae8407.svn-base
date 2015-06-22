using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources.Contact;
using Model;


namespace ADOPets.Web.ViewModels.Contact
{
    public class EditViewModel
    {

        public EditViewModel()
        {

        }
        public EditViewModel(Model.Contact contact)
        {
            Id = contact.Id;
            Relationship = contact.ContactTypeId;
            IsEmergencyContact = contact.IsEmergencyContact;
            FirstName = contact.FirstName;
            MiddleName = contact.MiddleName;
            LastName = contact.LastName;
            Home = contact.PhoneHome;
            Office = contact.PhoneOffice;
            Cell = contact.PhoneCell;
            Fax = contact.Fax;
            Email = contact.Email;
            UserId = contact.UserId;
        }

        public int Id { get; set; }

        [Display(Name = "Relationship", ResourceType = typeof(Edit))]
        [Required(ErrorMessageResourceName = "RelationshipRequired", ErrorMessageResourceType = typeof(Edit))]
        public ContactTypeEnum Relationship { get; set; }

        [Display(Name = "IsEmergencyContact", ResourceType = typeof(Edit))]
        public bool IsEmergencyContact { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Edit))]
        [Required(ErrorMessageResourceName = "LastNameRequired", ErrorMessageResourceType = typeof(Edit))]
        public string LastName { get; set; }

        [Display(Name = "MiddleName", ResourceType = typeof(Edit))]
        public string MiddleName { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Edit))]
        [Required(ErrorMessageResourceName = "FirstNameRequired", ErrorMessageResourceType = typeof(Edit))]
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

        public int UserId { get; set; }

        public void Map(Model.Contact contact)
        {
            
            contact.ContactTypeId = Relationship;
            contact.IsEmergencyContact = IsEmergencyContact;
            contact.FirstName = new EncryptedText(FirstName);
            contact.MiddleName = new EncryptedText(MiddleName);
            contact.LastName = new EncryptedText(LastName);
            contact.PhoneHome = new EncryptedText(Home);
            contact.PhoneOffice = new EncryptedText(Office);
            contact.PhoneCell = new EncryptedText(Cell);
            contact.Fax = new EncryptedText(Fax);
            contact.Email = new EncryptedText(Email);
          

        }
    }
}