using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using Model;


namespace ADOPets.Web.ViewModels.Weight
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

            LeftValueUnit = unit == HealthMeasureUnitEnum.Pounds
                ? EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Pounds)
                : EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Kilogram);

            RightValueUnit = unit == HealthMeasureUnitEnum.Pounds
               ? EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Ounce)
               : EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Gram);

            LeftValue = WeightConverterHelper.GetLefttValue(unit, measure.MeasureValue);
            RightValue = WeightConverterHelper.GetRightValue(unit == HealthMeasureUnitEnum.Pounds ? HealthMeasureUnitEnum.Ounce : HealthMeasureUnitEnum.Gram, measure.MeasureValue);
        }

        public int PetId { get; set; }

        public int Id { get; set; }

        [Display(Name = "Weight_Edit_Date", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Weight_Edit_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Weight_Add_Weight", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "RightValue", TargetValue = null, ErrorMessageResourceName = "Weight_Edit_LeftValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Weight_Edit_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public int? LeftValue { get; set; }

        [RequiredIf(DependentProperty = "LeftValue", TargetValue = null, ErrorMessageResourceName = "Weight_Edit_RightValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Weight_Edit_RightValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public int? RightValue { get; set; }

        public HealthMeasureUnitEnum MeasureUnit { get; set; }

        public string LeftValueUnit { get; set; }

        public string RightValueUnit { get; set; }

        public void Map(PetHealthMeasure measure)
        {
            LeftValue = LeftValue ?? 0;
            RightValue = RightValue ?? 0;

            measure.MeasuredDate = MeasureDate;
            measure.MeasureValue = WeightConverterHelper.GetTotalInGrams(MeasureUnit, LeftValue.Value, RightValue.Value).ToString(CultureInfo.InvariantCulture);

        }
    }
}