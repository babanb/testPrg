using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class ExpertVeterinarainViewModel
    {

        public ExpertVeterinarainViewModel()
        { 
        
        }
        public ExpertVeterinarainViewModel(Model.User user)
        {
            Id = user.Id;
            LastName = user.LastName;
            FirstName = user.FirstName;
            PrimaryPhone = user.PrimaryPhone;
            Email = user.Email;
            if (user.Veterinarians.Count > 0)
            {
                VetSpeciality = user.Veterinarians.FirstOrDefault().VetSpecialtyID;
            }
            else
            {
                VetSpeciality = VetSpecialityEnum.Surgery;
            }
        }

        public AddSetupViewModel objVet { get; set; }

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsCurrentVeterinarian { get; set; }
        public bool IsEmergencyContact { get; set; }
        public string PrimaryPhone { get; set; }
        public string Email { get; set; }
        public string PhoneCell { get; set; }
        public VetSpecialityEnum? VetSpeciality { get; set; }
    }
}