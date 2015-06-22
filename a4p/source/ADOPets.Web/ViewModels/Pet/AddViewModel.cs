using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using Model;

namespace ADOPets.Web.ViewModels.Pet
{
    public class AddViewModel
    {
        public AddViewModel()
        {
            if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                CountryOfBirth = CountryEnum.UnitedStates;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
            {
                CountryOfBirth = CountryEnum.India;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                CountryOfBirth = CountryEnum.France;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.Portuguese)
            {
                CountryOfBirth = CountryEnum.BRAZIL;
            }
        }

        public int Id { get; set; }

        [Display(Name = "Pet_Add_PetName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Pet_Add_PetNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string PetName { get; set; }

        [Display(Name = "Pet_Add_PetType", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Pet_Add_PetTypeRequired", ErrorMessageResourceType = typeof(Wording))]
        public PetTypeEnum PetType { get; set; }

        public string CurrentPetType { get; set; }

        [Display(Name = "Pet_Add_Breed", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "CurrentPetType", TargetValue = "Dog", ErrorMessageResourceName = "Pet_Add_BreedRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Breed { get; set; }


        [Display(Name = "Pet_Add_SecondaryBreed", ResourceType = typeof(Wording))]
        public string SecondaryBreed { get; set; }
        [Display(Name = "Pet_Add_AdoptionDate", ResourceType = typeof(Wording))]
        public DateTime? AdoptionDate { get; set; }

        [Display(Name = "Pet_Add_Pedigree", ResourceType = typeof(Wording))]
        public string Pedigree { get; set; }

        [Display(Name = "Pet_Add_BloodGroupType", ResourceType = typeof(Wording))]
        public BloodGroupTypeEnum? BloodGroupType { get; set; }

        [Display(Name = "Pet_Add_ChipNumber", ResourceType = typeof(Wording))]
        public string ChipNumber { get; set; }

        [Required(ErrorMessageResourceName = "Pet_Add_BirthDateRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Pet_Add_BirthDate", ResourceType = typeof(Wording))]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Pet_Add_PlaceOfBirth", ResourceType = typeof(Wording))]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Pet_Add_CountryOfBirth", ResourceType = typeof(Wording))]
        public CountryEnum? CountryOfBirth { get; set; }

        [Display(Name = "Pet_Add_StateOfBirth", ResourceType = typeof(Wording))]
        public StateEnum? StateOfBirth { get; set; }

        [Display(Name = "Pet_Add_Zip", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Pet_Add_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        public string Zip { get; set; }

        [Display(Name = "Pet_Add_HairType", ResourceType = typeof(Wording))]
        public HairTypeEnum? HairType { get; set; }

        public string CurrentHairType { get; set; }

        [Display(Name = "Pet_Add_HairTypeOther", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "CurrentHairType", TargetValue = "Other", ErrorMessageResourceName = "Pet_Add_HairTypeOtherRequired", ErrorMessageResourceType = typeof(Wording))]
        public string HairTypeOther { get; set; }

        [Display(Name = "Pet_Add_Color", ResourceType = typeof(Wording))]
        public ColorEnum? Color { get; set; }

        public string CurrentColor { get; set; }

        [Display(Name = "Pet_Add_ColorOther", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "CurrentColor", TargetValue = "Other", ErrorMessageResourceName = "Pet_Add_ColorOtherRequired", ErrorMessageResourceType = typeof(Wording))]
        public string ColorOther { get; set; }

        [Display(Name = "Pet_Add_IsSterile", ResourceType = typeof(Wording))]
        public bool? IsSterile { get; set; }

        [Display(Name = "Pet_Add_TattooNumber", ResourceType = typeof(Wording))]
        public string TattooNumber { get; set; }

        [Display(Name = "Pet_Add_Gender", ResourceType = typeof(Wording))]
        public PetGenderEnum? Gender { get; set; }

        public string ImagePath { get; set; }

        public string CoverImage { get; set; }

        public Model.Pet Map(string path, string coverFileName, User user)
        {
            var pet = new Model.Pet
            {
                Name = new EncryptedText(PetName),
                PetTypeId = PetType,
                BirthDate = BirthDate.Value,
                BloodGroupTypeId = BloodGroupType,
                HairTypeId = HairType,
                CustomHairType = HairTypeOther,
                CustomBreed = Breed,
                SecondaryBreed = SecondaryBreed,
                AdoptionDate = (AdoptionDate == null) ? AdoptionDate : AdoptionDate.Value,
                GenderId = Gender,
                ColorId = Color,
                CustomColor = ColorOther,
                Pedigree = Pedigree,
                ChipNumber = new EncryptedText(ChipNumber),
                TattooNumber = new EncryptedText(TattooNumber),
                IsSterile = IsSterile,
                CountryOfBirthId = CountryOfBirth,
                StateOfBirthId = StateOfBirth,
                PlaceOfBirth = new EncryptedText(PlaceOfBirth),
                CreationDate = DateTime.Now,
                PetStatusId = PetStatusEnum.Active,
                Image = path,
                CoverImage = coverFileName,
                Zip = new EncryptedText(Zip),
                Users = new List<User> { user }
            };
            return pet;
        }
    }
}