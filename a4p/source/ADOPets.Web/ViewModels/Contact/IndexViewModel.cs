using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace ADOPets.Web.ViewModels.Contact
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {

        }

        public IndexViewModel(Model.Contact contact, string petName, PetTypeEnum petType)
        {
            Id = contact.Id;
            AnimalType = null;
            AnimalName = null;
            Relationship = contact.ContactTypeId;
            LastName = contact.LastName;
            FirstName = contact.FirstName;
            PhoneHome = contact.PhoneHome;
            PhoneOffice = contact.PhoneOffice;
            PhoneCell = contact.PhoneCell;
            VeterinarianId = contact.VeterinarianId;
            PetName = petName;
            PetTypeId = petType;
            PetContactId = contact.PetContactId;
            PetId = contact.PetId;
        }

        public int Id { get; set; }
        public string AnimalType { get; set; }
        public string AnimalName { get; set; }
        public ContactTypeEnum Relationship { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneCell { get; set; }
        public string PhoneOffice { get; set; }
        public string Phone { get; set; }
        public int? PetContactId { get; set; }
        public int? VeterinarianId { get; set; }        
        public int? PetId { get; set; }

        public string PetName { get; set; }
        public PetTypeEnum? PetTypeId { get; set; }
    }
}