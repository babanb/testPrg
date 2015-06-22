using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using Model;

namespace ADOPets.Web.ViewModels.Weight
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

            LeftValueUnit = unit == HealthMeasureUnitEnum.Pounds
                ? EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Pounds)
                : EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Kilogram);

            RightValueUnit = unit == HealthMeasureUnitEnum.Pounds
               ? EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Ounce)
               : EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Gram);
        }

        public int PetId { get; set; }

        [Display(Name = "Weight_Add_Date", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Weight_Add_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Weight_Add_Weight", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "RightValue", TargetValue = null, ErrorMessageResourceName = "Weight_Add_LeftValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Weight_Add_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public int? LeftValue { get; set; }

        [RequiredIf(DependentProperty = "LeftValue", TargetValue = null, ErrorMessageResourceName = "Weight_Add_RightValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Weight_Add_RightValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public int? RightValue { get; set; }

        public HealthMeasureUnitEnum MeasureUnit { get; set; }

        public string LeftValueUnit { get; set; }

        public string RightValueUnit { get; set; }

        public PetHealthMeasure Map()
        {
            LeftValue = LeftValue ?? 0;
            RightValue = RightValue ?? 0;

            return new PetHealthMeasure
            {
                MeasuredDate = MeasureDate,
                MeasureValue = WeightConverterHelper.GetTotalInGrams(MeasureUnit, LeftValue.Value, RightValue.Value).ToString(CultureInfo.InvariantCulture),
                UploadTime = DateTime.Today,
                HealthMeasureTypeId = HealthMeasureTypeEnum.Weight,
                PetId = PetId
            };
        }
    }
}