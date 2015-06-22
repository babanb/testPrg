using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.SerumElectrolyte
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

        [Display(Name = "SerumElectrolyte_Edit_Date", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "SerumElectrolyte_Edit_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "SerumElectrolyte_Add_SerumElectrolyte", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "SerumElectrolyte_Add_LeftValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression(@"\d+([,.](\d{1,2})?)?", ErrorMessageResourceName = "SerumElectrolyte_Add_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        [Range(typeof(double), "0", "999", ErrorMessageResourceName = "SerumElectrolyte_Add_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public double LeftValue { get; set; }

        public void Map(PetHealthMeasure measure)
        {
            measure.MeasuredDate = MeasureDate;
            measure.MeasureValue = LeftValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}