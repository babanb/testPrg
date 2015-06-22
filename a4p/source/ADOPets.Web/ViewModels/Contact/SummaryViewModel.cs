using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;


namespace ADOPets.Web.ViewModels.Contact
{
    public class SummaryViewModel
    {

        public SummaryViewModel()
        {

        }
        public SummaryViewModel(Model.Contact contact)
        {
            Id = contact.Id;
            Relationship = contact.ContactTypeId;
            FirstName = contact.FirstName;
            MiddleName = contact.MiddleName;
            LastName = contact.LastName;
            Home = contact.PhoneHome;
            Office = contact.PhoneOffice;
            Cell = contact.PhoneCell;
            Fax = contact.Fax;
            Email = contact.Email;
            UserId = contact.UserId;
            //PetTypeId = contact.PetTypeId;
            PetId =Convert.ToInt32(contact.PetId);
        }

        public int Id { get; set; }
        public int PetId { get; set; }

        [Display(Name = "Contact_Summary_Relationship", ResourceType = typeof(Wording))]
        public ContactTypeEnum Relationship { get; set; }

        [Display(Name = "Contact_Summary_LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Display(Name = "Contact_Summary_FirstName", ResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Contact_Summary_Home", ResourceType = typeof(Wording))]
        public string Home { get; set; }

        [Display(Name = "Contact_Summary_Office", ResourceType = typeof(Wording))]
        public string Office { get; set; }

        [Display(Name = "Contact_Summary_Cell", ResourceType = typeof(Wording))]
        public string Cell { get; set; }

        [Display(Name = "Contact_Summary_Fax", ResourceType = typeof(Wording))]
        public string Fax { get; set; }

        [Display(Name = "Contact_Summary_Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }

        public int UserId { get; set; }
    }
}