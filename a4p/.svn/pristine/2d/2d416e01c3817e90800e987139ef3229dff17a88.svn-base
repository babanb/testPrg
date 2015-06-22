using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using Model;

namespace ADOPets.Web.ViewModels.Height
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

            LeftValueUnit = unit == HealthMeasureUnitEnum.Feet
                ? EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Feet)
                : EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Meter);

            RightValueUnit = unit == HealthMeasureUnitEnum.Feet
               ? EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Inches)
               : EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Centimeter);

            LeftValue = HeightConverterHelper.GetLefttValue(unit, measure.MeasureValue);
            RightValue = HeightConverterHelper.GetRightValue(unit == HealthMeasureUnitEnum.Feet ? HealthMeasureUnitEnum.Inches : HealthMeasureUnitEnum.Centimeter, measure.MeasureValue);
        }

        public int PetId { get; set; }

        public int Id { get; set; }

        [Display(Name = "Height_Edit_Date", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Height_Edit_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Height_Add_Height", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "RightValue", TargetValue = null, ErrorMessageResourceName = "Height_Edit_LeftValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Height_Edit_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public int? LeftValue { get; set; }

        [RequiredIf(DependentProperty = "LeftValue", TargetValue = null, ErrorMessageResourceName = "Height_Edit_RightValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Height_Edit_RightValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public int? RightValue { get; set; }

        public HealthMeasureUnitEnum MeasureUnit { get; set; }

        public string LeftValueUnit { get; set; }

        public string RightValueUnit { get; set; }

        public void Map(PetHealthMeasure measure)
        {
            LeftValue = LeftValue ?? 0;
            RightValue = RightValue ?? 0;

            measure.MeasuredDate = MeasureDate;
            measure.MeasureValue = HeightConverterHelper.GetTotalInMeters(MeasureUnit, LeftValue.Value, RightValue.Value).ToString(CultureInfo.InvariantCulture);
        }
    }
}