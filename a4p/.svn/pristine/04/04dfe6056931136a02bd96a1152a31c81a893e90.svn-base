using ADOPets.Web.Resources;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.Profile
{
    public class RemovePetsViewModel
    {
        public RemovePetsViewModel(int id, string name, PetTypeEnum petType, System.DateTime petBirthDate, PetGenderEnum? petGender, string petImage, int? ownerId = null,
            string ownerFirstName = null, string ownerLastName = null, string ownerInfoPath = null)
        {
            Id = id;
            PetName = name;
            PetType = petType;
            PetBirthDate = petBirthDate.ToShortDateString();
            PetGender = petGender;
            PetImage = petImage; 
          
        }

        public RemovePetsViewModel(int totalUnUsedPets)
        {
            UnUsed = totalUnUsedPets;
        }

        public int Id { get; set; }

        [Display(Name = "Pet_Index_AnimalType", ResourceType = typeof(Wording))]
        public PetTypeEnum? PetType { get; set; }

        [Display(Name = "Pet_Index_AnimalName", ResourceType = typeof(Wording))]
        public string PetName { get; set; }

        public string PetBirthDate { get; set; }

        public PetGenderEnum? PetGender { get; set; }

        public string PetImage { get; set; }

        public int UnUsed { get; set; }
    }
}