using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Hemogram
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

        [Display(Name = "Hemogram_Edit_Date", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Hemogram_Edit_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Hemogram_Edit_Hemogram", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Hemogram_Edit_LeftValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression(@"\d+([,.](\d{1,2})?)?", ErrorMessageResourceName = "Hemogram_Edit_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        [Range(typeof(double), "0", "999", ErrorMessageResourceName = "Hemogram_Edit_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public double LeftValue { get; set; }

        public void Map(PetHealthMeasure measure)
        {
            measure.MeasuredDate = MeasureDate;
            measure.MeasureValue = LeftValue.ToString(CultureInfo.InvariantCulture);

        }
    }
}