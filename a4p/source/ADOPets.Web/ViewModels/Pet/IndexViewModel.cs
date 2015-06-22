using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Pet
{
    public class IndexViewModel
    {
        public IndexViewModel(int id, string name, PetTypeEnum petType, System.DateTime petBirthDate, UserStatusEnum userStatusId, PetGenderEnum? petGender, string petImage, int? ownerId = null,
            string ownerFirstName = null, string ownerLastName = null, string ownerInfoPath = null)
        {
            Id = id;
            PetName = name;
            PetType = petType;
            PetBirthDate = petBirthDate.ToShortDateString();
            PetGender = petGender;
            PetImage = petImage;

            OwnerId = ownerId;
            OwnerName = string.Format("{0} {1}", ownerFirstName, ownerLastName);
            OwnerFirstName = ownerFirstName;
            OwnerLastName = ownerLastName;
            OwnerInfoPath = ownerInfoPath;
            IsActive = (userStatusId == UserStatusEnum.Disabled) ? false : true;
        }
        public IndexViewModel(int id, string name, PetTypeEnum petType, System.DateTime petBirthDate, PetGenderEnum? petGender, string petImage, int? ownerId = null,
           string ownerFirstName = null, string ownerLastName = null, string ownerInfoPath = null)
        {
            Id = id;
            PetName = name;
            PetType = petType;
            PetBirthDate = petBirthDate.ToShortDateString();
            PetGender = petGender;
            PetImage = petImage;

            OwnerId = ownerId;
            OwnerName = string.Format("{0} {1}", ownerFirstName, ownerLastName);
            OwnerFirstName = ownerFirstName;
            OwnerLastName = ownerLastName;
            OwnerInfoPath = ownerInfoPath;
          //  IsActive = (userStatusId == UserStatusEnum.Disabled) ? false : true;
        }

        public IndexViewModel(int id, string name, PetTypeEnum petType, System.DateTime petBirthDate, PetGenderEnum? petGender, string petImage, User user)
        {
            Id = id;
            PetName = name;
            PetType = petType;
            PetBirthDate = petBirthDate.ToShortDateString();
            PetGender = petGender;
            PetImage = petImage;
            if (user != null)
            {
                OwnerId = user.Id;
                OwnerName = string.Format("{0} {1}", user.FirstName, user.LastName);
                OwnerFirstName = user.FirstName;
                OwnerLastName = user.LastName;
                OwnerInfoPath = user.InfoPath;
            }
            IsActive = (user.UserStatusId == UserStatusEnum.Disabled) ? false : true;
        }

        public bool IsActive { get; set; }

        public int Id { get; set; }

        [Display(Name = "Pet_Index_AnimalType", ResourceType = typeof(Wording))]
        public PetTypeEnum? PetType { get; set; }

        [Display(Name = "Pet_Index_AnimalName", ResourceType = typeof(Wording))]
        public string PetName { get; set; }

        public string PetBirthDate { get; set; }

        public PetGenderEnum? PetGender { get; set; }

        public string PetImage { get; set; }



        #region Admin use

        public int? OwnerId { get; set; }

        public string OwnerName { get; set; }

        public string OwnerInfoPath { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        #endregion
    }
}