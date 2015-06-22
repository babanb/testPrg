﻿using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using Model;

namespace ADOPets.Web.ViewModels.Pet
{
    public class CardViewModel
    {
        public CardViewModel()
        {

        }

        public CardViewModel(Model.Pet pet)
        {
            Id = pet.Id;
            PetName = pet.Name;
            PetType = pet.PetTypeId;
            Breed = pet.CustomBreed;
            SecondaryBreed = pet.SecondaryBreed;
            AdoptionDate = pet.AdoptionDate;
            Pedigree = pet.Pedigree;
            BloodGroupType = pet.BloodGroupTypeId;
            ChipNumber = pet.ChipNumber;
            BirthDate = pet.BirthDate;
            PlaceOfBirth = pet.PlaceOfBirth;
            CountryOfBirth = pet.CountryOfBirthId;
            StateOfBirth = pet.StateOfBirthId;
            Zip = pet.Zip;
            HairType = pet.HairTypeId;
            HairTypeOther = pet.CustomHairType;
            Color = pet.ColorId;
            ColorOther = pet.CustomColor;
            IsSterile = pet.IsSterile;
            TattooNumber = pet.TattooNumber;
            Gender = pet.GenderId;
            ImagePath = pet.Image;
            CoverImage = pet.CoverImage;
          
            if (!string.IsNullOrEmpty(pet.PetTypeId.ToString()))
            {
                CurrentPetType = pet.PetTypeId.ToString();
            }
            //if (petOwnerId != 0)
            //{
            //    UserId = petOwnerId;
            //}
        }
        public int UserId { get; set; }
        public int Id { get; set; }

        [Display(Name = "Pet_Card_PetName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Pet_Card_PetNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string PetName { get; set; }

        [Display(Name = "Pet_Card_PetType", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Pet_Card_PetTypeRequired", ErrorMessageResourceType = typeof(Wording))]
        public PetTypeEnum PetType { get; set; }

        public string CurrentPetType { get; set; }

        [Display(Name = "Pet_Card_Breed", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "CurrentPetType", TargetValue = "Dog", ErrorMessageResourceName = "Pet_Card_BreedRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Breed { get; set; }

        [Display(Name = "Pet_Card_SecondaryBreed", ResourceType = typeof(Wording))]
        public string SecondaryBreed { get; set; }

        [Display(Name = "Pet_Card_AdoptionDate", ResourceType = typeof(Wording))]
        public DateTime? AdoptionDate { get; set; }

        [Display(Name = "Pet_Card_Pedigree", ResourceType = typeof(Wording))]
        public string Pedigree { get; set; }

        [Display(Name = "Pet_Card_BloodGroupType", ResourceType = typeof(Wording))]
        public BloodGroupTypeEnum? BloodGroupType { get; set; }

        [Display(Name = "Pet_Card_ChipNumber", ResourceType = typeof(Wording))]
        public string ChipNumber { get; set; }

        [Required(ErrorMessageResourceName = "Pet_Card_BirthDateRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Pet_Card_BirthDate", ResourceType = typeof(Wording))]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Pet_Card_PlaceOfBirth", ResourceType = typeof(Wording))]
        public string PlaceOfBirth { get; set; }

        [Display(Name = "Pet_Card_CountryOfBirth", ResourceType = typeof(Wording))]
        public CountryEnum? CountryOfBirth { get; set; }

        [Display(Name = "Pet_Card_StateOfBirth", ResourceType = typeof(Wording))]
        public StateEnum? StateOfBirth { get; set; }

        [Display(Name = "Pet_Card_Zip", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Pet_Add_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        public string Zip { get; set; }

        [Display(Name = "Pet_Card_HairType", ResourceType = typeof(Wording))]
        public HairTypeEnum? HairType { get; set; }

        public string CurrentHairType { get; set; }

        [Display(Name = "Pet_Card_HairTypeOther", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "CurrentHairType", TargetValue = "Other", ErrorMessageResourceName = "Pet_Card_HairTypeOtherRequired", ErrorMessageResourceType = typeof(Wording))]
        public string HairTypeOther { get; set; }

        [Display(Name = "Pet_Card_Color", ResourceType = typeof(Wording))]
        public ColorEnum? Color { get; set; }

        public string CurrentColor { get; set; }

        [Display(Name = "Pet_Card_ColorOther", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "CurrentColor", TargetValue = "Other", ErrorMessageResourceName = "Pet_Card_ColorOtherRequired", ErrorMessageResourceType = typeof(Wording))]
        public string ColorOther { get; set; }

        [Display(Name = "Pet_Card_IsSterile", ResourceType = typeof(Wording))]
        public bool? IsSterile { get; set; }

        [Display(Name = "Pet_Card_TattooNumber", ResourceType = typeof(Wording))]
        public string TattooNumber { get; set; }

        [Display(Name = "Pet_Card_Gender", ResourceType = typeof(Wording))]
        public PetGenderEnum? Gender { get; set; }

        public string ImagePath { get; set; }

        public string CoverImage { get; set; }
        
        public void Map(Model.Pet pet)
        {
            pet.Name = new EncryptedText(PetName);
            pet.PetTypeId = PetType;
            pet.BirthDate = BirthDate;
            pet.BloodGroupTypeId = BloodGroupType;
            pet.HairTypeId = HairType;
            pet.CustomHairType = HairTypeOther;
            pet.CustomBreed = Breed;
            pet.SecondaryBreed = SecondaryBreed;
            pet.AdoptionDate = AdoptionDate;
            pet.GenderId = Gender;
            pet.ColorId = Color;
            pet.CustomColor = ColorOther;
            pet.Pedigree = Pedigree;
            pet.ChipNumber = new EncryptedText(ChipNumber);
            pet.TattooNumber = new EncryptedText(TattooNumber);
            pet.IsSterile = IsSterile;
            pet.CountryOfBirthId = CountryOfBirth;
            pet.StateOfBirthId = StateOfBirth;
            pet.PlaceOfBirth = new EncryptedText(PlaceOfBirth);
            pet.PetStatusId = PetStatusEnum.Active;
            pet.Image = ImagePath;
            pet.CoverImage = CoverImage;
            pet.Zip = new EncryptedText(Zip);
        }
    }
}