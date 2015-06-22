using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace ADOPets.Web.ViewModels.Pet
{
    public class LoadReportModel
    {
        public LoadReportModel()
        {

        }

        public LoadReportModel(Model.Pet pet)
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

            var farmer = pet.Farmer;
            if (pet.Farmer != null)
            {
                FarmerId = farmer.Id;
                FarmerName = farmer.Name;
                FarmerAddress1 = farmer.Address1;
                FarmerAddress2 = farmer.Address2;
                FarmerCountry = farmer.CountryId;
                FarmerState = farmer.StateId;
                FarmerCity = farmer.City;
                FarmerZip = farmer.Zip;
                FarmerHomePhone = farmer.PhoneHome;
                FarmerOfficePhone = farmer.PhoneOffice;
                FarmerCellPhone = farmer.PhoneCell;
                FarmerFax = farmer.Fax;
            }

            Insurances = pet.Insurances.Select(i => new Insurance.IndexViewModel(i)).ToList();
        }

        public List<Insurance.IndexViewModel> Insurances { get; set; }

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

        #region Farmer Fields

        public int? FarmerId { get; set; }

        [Display(Name = "Pet_Card_FarmerName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Pet_Card_FarmerNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FarmerName { get; set; }

        [Display(Name = "Pet_Card_FarmerAddress1", ResourceType = typeof(Wording))]

        public string FarmerAddress1 { get; set; }

        [Display(Name = "Pet_Card_FarmerAddress2", ResourceType = typeof(Wording))]
        public string FarmerAddress2 { get; set; }

        [Display(Name = "Pet_Card_FarmerCountry", ResourceType = typeof(Wording))]
        public CountryEnum? FarmerCountry { get; set; }

        [Display(Name = "Pet_Card_FarmerState", ResourceType = typeof(Wording))]
        public StateEnum? FarmerState { get; set; }

        [Display(Name = "Pet_Card_FarmerCity", ResourceType = typeof(Wording))]

        public string FarmerCity { get; set; }

        [Display(Name = "Pet_Card_FarmerHomePhone", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\S[0-9\-. ]+", ErrorMessageResourceName = "Pet_Card_HomeNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string FarmerHomePhone { get; set; }

        [Display(Name = "Pet_Card_FarmerOfficePhone", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\S[0-9\-. ]+", ErrorMessageResourceName = "Pet_Card_OfficeNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string FarmerOfficePhone { get; set; }

        [Display(Name = "Pet_Card_FarmerCellPhone", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\S[0-9\-. ]+", ErrorMessageResourceName = "Pet_Card_CellNumberFormat", ErrorMessageResourceType = typeof(Wording))]
        public string FarmerCellPhone { get; set; }

        [Display(Name = "Pet_Card_FarmerFax", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\S[0-9\-. ]+", ErrorMessageResourceName = "Pet_Card_FaxFormat", ErrorMessageResourceType = typeof(Wording))]
        public string FarmerFax { get; set; }

        [Display(Name = "Pet_Card_FarmerZip", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\S\d+$", ErrorMessageResourceName = "Pet_Card_FarmerZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        public string FarmerZip { get; set; }

        #endregion
    }
}