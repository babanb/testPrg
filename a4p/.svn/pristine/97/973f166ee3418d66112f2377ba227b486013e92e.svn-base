using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Pulse
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(PetHealthMeasure measure)
        {
            PetId = measure.PetId;
            Id = measure.Id;
            MeasureDate = measure.MeasuredDate;
            Pulse = PulseConverterHelper.GetFormatedPulse(measure.MeasureValue);
        }

        public int PetId { get; set; }

        public int Id { get; set; }

        [Display(Name = "Pulse_Delete_Date", ResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Pulse_Delete_Pulse", ResourceType = typeof(Wording))]
        public string Pulse { get; set; }
    }
}