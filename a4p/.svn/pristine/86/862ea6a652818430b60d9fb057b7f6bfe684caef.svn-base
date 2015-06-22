using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Temperature
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(PetHealthMeasure measure, HealthMeasureUnitEnum unit)
        {
            PetId = measure.PetId;
            Id = measure.Id;
            MeasureDate = measure.MeasuredDate;
            MeasureUnit = unit;

            ValueUnit = unit == HealthMeasureUnitEnum.Fahrenheit
                ? EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Fahrenheit)
                : EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Celsius);

            LeftValue = TemperatureConverterHelper.GetValue(unit, measure.MeasureValue);
        }

        public int PetId { get; set; }

        public int Id { get; set; }

        [Display(Name = "Temperature_Edit_Date", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Temperature_Edit_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Temperature_Add_Temperature", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Temperature_Edit_LeftValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression(@"\d+([,.](\d{1,2})?)?", ErrorMessageResourceName = "Temperature_Edit_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public double LeftValue { get; set; }

        public HealthMeasureUnitEnum MeasureUnit { get; set; }

        public string ValueUnit { get; set; }

        public void Map(PetHealthMeasure measure)
        {
            measure.MeasuredDate = MeasureDate;
            measure.MeasureValue = TemperatureConverterHelper.GetTotalInCelsius(MeasureUnit, LeftValue);
        }
    }
}