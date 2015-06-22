using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.Veterinarian
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {

        }

        public IndexViewModel(Model.Veterinarian veterinarian)
        {
            Id = veterinarian.Id;
            LastName = veterinarian.LastName;
            FirstName = veterinarian.FirstName;
            IsCurrentVeterinarian = veterinarian.IsCurrentVeterinarian;
            IsEmergencyContact = veterinarian.IsEmergencyContact;
            PhoneHome = veterinarian.PhoneHome;
            PhoneOffice = veterinarian.PhoneOffice;
            PhoneCell = veterinarian.PhoneCell;
            UserId = veterinarian.UserId;
        }

        public int? UserId { get; set; }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsCurrentVeterinarian { get; set; }
        public bool IsEmergencyContact { get; set; }
        public string PhoneHome { get; set; }
        public string PhoneOffice { get; set; }
        public string PhoneCell { get; set; }

    }
}