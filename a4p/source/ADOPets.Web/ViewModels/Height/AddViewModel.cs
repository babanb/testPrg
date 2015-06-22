using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using Model;

namespace ADOPets.Web.ViewModels.Height
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

            LeftValueUnit = unit == HealthMeasureUnitEnum.Feet
                ? EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Feet)
                : EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Meter);

            RightValueUnit = unit == HealthMeasureUnitEnum.Feet
               ? EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Inches)
               : EnumHelper.GetResourceValueForEnumValue(HealthMeasureUnitEnum.Centimeter);
        }

        public int PetId { get; set; }

        [Display(Name = "Height_Add_Date", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Height_Add_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Height_Add_Height", ResourceType = typeof(Wording))]
        [RequiredIf(DependentProperty = "RightValue", TargetValue = null, ErrorMessageResourceName = "Height_Add_LeftValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Height_Add_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        public int? LeftValue { get; set; }

        [RequiredIf(DependentProperty = "LeftValue", TargetValue = null, ErrorMessageResourceName = "Height_Add_RightValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Height_Add_RightValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
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
                MeasureValue = HeightConverterHelper.GetTotalInMeters(MeasureUnit, LeftValue.Value, RightValue.Value).ToString(CultureInfo.InvariantCulture),
                UploadTime = DateTime.Today,
                HealthMeasureTypeId = HealthMeasureTypeEnum.Height,
                PetId = PetId
            };
        }
    }
}