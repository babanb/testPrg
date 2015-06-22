using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Pulse
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
            LeftValue = int.Parse(measure.MeasureValue);
        }

        public int PetId { get; set; }

        public int Id { get; set; }

        [Display(Name = "Pulse_Edit_Date", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Pulse_Edit_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Pulse_Add_Pulse", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Pulse_Edit_LeftValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Pulse_Edit_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        [Range(typeof(int), "0", "999", ErrorMessageResourceName = "Pulse_Edit_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public int? LeftValue { get; set; }

        public void Map(PetHealthMeasure measure)
        {
            measure.MeasuredDate = MeasureDate;
            measure.MeasureValue = LeftValue.HasValue ? LeftValue.Value.ToString() : "0";
        }
    }
}