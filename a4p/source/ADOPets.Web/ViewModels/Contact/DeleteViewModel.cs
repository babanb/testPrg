using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources.Contact;
using Model;

namespace ADOPets.Web.ViewModels.Contact
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {
          
        }

        public DeleteViewModel(Model.Contact contact)
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
        }

        public int Id { get; set; }

        [Display(Name = "Relationship", ResourceType = typeof(Delete))]
        public ContactTypeEnum Relationship { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Delete))]
        public string LastName { get; set; }

        [Display(Name = "MiddleName", ResourceType = typeof(Delete))]
        public string MiddleName { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Delete))]
        public string FirstName { get; set; }

        [Display(Name = "Home", ResourceType = typeof(Delete))]
        public string Home { get; set; }

        [Display(Name = "Office", ResourceType = typeof(Delete))]
        public string Office { get; set; }

        [Display(Name = "Cell", ResourceType = typeof(Delete))]
        public string Cell { get; set; }

        [Display(Name = "Fax", ResourceType = typeof(Delete))]
        public string Fax { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Delete))]
        public string Email { get; set; }

        public int UserId { get; set; }

    }
}