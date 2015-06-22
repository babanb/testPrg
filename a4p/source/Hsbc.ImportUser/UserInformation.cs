using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsbc.ImportUser
{
    public class UserInformation
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }

        public string Email { get; set; }

        public DateTime AdoptionDate { get; set; }

        public string PetName { get; set; }

        public PetTypeEnum PetType { get; set; }

        public DateTime? PetBirthDate { get; set; }

        public string PetPrimaryBreed { get; set; }

        public string PetSecondaryBreed { get; set; }

        public PetGenderEnum? PetGender { get; set; }

        public string ChipNumber { get; set; }

        public string PromoCode { get; set; }

        public string Immunization { get; set; }

        public DateTime? ImmunizationDate { get; set; }

        public Boolean Sterile { get; set; }

        public int UserId { get; set; }
    }
}
