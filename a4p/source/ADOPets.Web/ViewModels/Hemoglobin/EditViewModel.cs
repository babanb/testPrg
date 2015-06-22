using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Hemoglobin
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(PetHealthMeasure measure)
        {
            PetId = measure.PetId;
            Id = measure.Id;
            MeasureDate = measure.MeasuredDate;
            LeftValue = double.Parse(measure.MeasureValue, CultureInfo.InvariantCulture);
        }

        public int PetId { get; set; }

        public int Id { get; set; }

        [Display(Name = "Hemoglobin_Edit_Date", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Hemoglobin_Edit_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Hemoglobin_Add_Hemoglobin", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Hemoglobin_Add_LeftValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression(@"\d+([,.](\d{1,2})?)?", ErrorMessageResourceName = "Hemoglobin_Add_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        [Range(typeof(double), "0", "99", ErrorMessageResourceName = "Hemoglobin_Add_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public double LeftValue { get; set; }

        public void Map(PetHealthMeasure measure)
        {
            measure.MeasuredDate = MeasureDate;
            measure.MeasureValue = LeftValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}