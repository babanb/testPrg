using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Temperature
{
    public class AddViewModel
    {
        public AddViewModel()
        {

        }

        public AddViewModel(int petId, HealthMeasureUnitEnum unit)
        {
            PetId = petId;
            MeasureUnit = unit;

            ValueUnit = unit == HealthMeasureUnitEnum.Fahrenheit
                ? EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Fahrenheit)
                : EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Celsius);
        }

        public int PetId { get; set; }

        [Display(Name = "Temperature_Add_Date", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Temperature_Add_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Temperature_Add_Temperature", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Temperature_Add_LeftValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression(@"\d+([,.](\d{1,2})?)?", ErrorMessageResourceName = "Temperature_Edit_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public double LeftValue { get; set; }

        public HealthMeasureUnitEnum MeasureUnit { get; set; }

        public string ValueUnit { get; set; }
        

        public PetHealthMeasure Map()
        {
            return new PetHealthMeasure
            {
                MeasuredDate = MeasureDate,
                MeasureValue = TemperatureConverterHelper.GetTotalInCelsius(MeasureUnit, LeftValue).ToString(CultureInfo.InvariantCulture),
                UploadTime = DateTime.Today,
                HealthMeasureTypeId = HealthMeasureTypeEnum.Temperature,
                PetId = PetId
            };
        }
    }
}