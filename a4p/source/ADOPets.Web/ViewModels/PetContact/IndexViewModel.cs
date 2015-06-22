using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace ADOPets.Web.ViewModels.PetContact
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {

        }

        public IndexViewModel(Model.PetContact petcontact)
        {
            Id = petcontact.Id;
            FirstName = petcontact.FirstName;
            LastName = petcontact.LastName;
            Relationship = petcontact.ContactTypeId;
            IsEmergencyContact = petcontact.IsEmergencyContact;
            PhoneHome = petcontact.PhoneHome;
            PhoneOffice = petcontact.PhoneOffice;
            PhoneCell = petcontact.PhoneCell;

        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsEmergencyContact { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneOffice { get; set; }
        public string PhoneCell { get; set; }
        public ContactTypeEnum Relationship { get; set; }
    }
}