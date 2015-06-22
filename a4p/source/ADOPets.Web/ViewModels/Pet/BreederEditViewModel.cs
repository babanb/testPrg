using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Pet
{
    public class BreederEditViewModel
    {
        public BreederEditViewModel()
        {

        }

        public BreederEditViewModel(Model.Pet pet)
        {
            Id = pet.Id;
            Zip = pet.Zip;
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
        }

        public int Id { get; set; }

        [Display(Name = "Pet_Card_Zip", ResourceType = typeof(Wording))]
        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Pet_Add_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        public string Zip { get; set; }

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

        public void Map(Model.Pet pet)
        {
            if (pet.FarmerId == null)
            {
                pet.Farmer = new Farmer
                {
                    Name = new EncryptedText(FarmerName),
                    Address1 = new EncryptedText(FarmerAddress1),
                    Address2 = new EncryptedText(FarmerAddress2),
                    CountryId = FarmerCountry,
                    StateId = FarmerState,
                    City = new EncryptedText(FarmerCity),
                    Zip = new EncryptedText(FarmerZip),
                    PhoneHome = new EncryptedText(FarmerHomePhone),
                    PhoneOffice = new EncryptedText(FarmerOfficePhone),
                    PhoneCell = new EncryptedText(FarmerCellPhone),
                    Fax = new EncryptedText(FarmerFax)
                };
            }
            else
            {
                pet.Farmer.Name = new EncryptedText(FarmerName);
                pet.Farmer.Address1 = new EncryptedText(FarmerAddress1);
                pet.Farmer.Address2 = new EncryptedText(FarmerAddress2);
                pet.Farmer.CountryId = FarmerCountry;
                pet.Farmer.StateId = FarmerState;
                pet.Farmer.City = new EncryptedText(FarmerCity);
                pet.Farmer.Zip = new EncryptedText(Zip);
                pet.Farmer.PhoneHome = new EncryptedText(FarmerHomePhone);
                pet.Farmer.PhoneOffice = new EncryptedText(FarmerOfficePhone);
                pet.Farmer.PhoneCell = new EncryptedText(FarmerCellPhone);
                pet.Farmer.Fax = new EncryptedText(FarmerFax);
            }
        }
    }
}