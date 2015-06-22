using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.SerumElectrolyte
{
    public class AddViewModel
    {
        public AddViewModel()
        {

        }

        public AddViewModel(int petId)
        {
            PetId = petId;
        }

        public int PetId { get; set; }

        [Display(Name = "SerumElectrolyte_Add_Date", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "SerumElectrolyte_Add_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "SerumElectrolyte_Add_SerumElectrolyte", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "SerumElectrolyte_Add_LeftValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression(@"\d+([,.](\d{1,2})?)?", ErrorMessageResourceName = "SerumElectrolyte_Add_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        [Range(typeof(double), "0", "999", ErrorMessageResourceName = "SerumElectrolyte_Add_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public double LeftValue { get; set; }
     
        public PetHealthMeasure Map()
        {
            return new PetHealthMeasure
            {
                MeasuredDate = MeasureDate,
                MeasureValue = LeftValue.ToString(CultureInfo.InvariantCulture),
                UploadTime = DateTime.Today,
                HealthMeasureTypeId = HealthMeasureTypeEnum.SerumElectrolytes,
                PetId = PetId
            };
        }
    }
}