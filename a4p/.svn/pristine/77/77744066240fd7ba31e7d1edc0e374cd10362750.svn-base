using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;


namespace ADOPets.Web.ViewModels.Pulse
{
    public class AddViewModel
    {
        public AddViewModel()
        {

        }

        public AddViewModel(int petId)
        {
            PetId = petId;
        }

        public int PetId { get; set; }

        [Display(Name = "Pulse_Add_Date", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Pulse_Add_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Pulse_Add_Pulse", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Pulse_Add_LeftValueRequired", ErrorMessageResourceType = typeof(Wording))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Pulse_Add_LeftValueValidationNumber", ErrorMessageResourceType = typeof(Wording))]
        [Range(typeof(int), "0", "999", ErrorMessageResourceName = "Pulse_Add_LeftValueRange", ErrorMessageResourceType = typeof(Wording))]
        public int? LeftValue { get; set; }

        public PetHealthMeasure Map()
        {
            return new PetHealthMeasure
            {
                MeasuredDate = MeasureDate,
                MeasureValue = LeftValue.HasValue ? LeftValue.Value.ToString() : "0",
                UploadTime = DateTime.Today,
                HealthMeasureTypeId = HealthMeasureTypeEnum.Pulse,
                PetId = PetId
            };
        }
    }
}