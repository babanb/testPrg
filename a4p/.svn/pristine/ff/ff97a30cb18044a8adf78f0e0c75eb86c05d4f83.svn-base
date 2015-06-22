using System;
using ADOPets.Web.Common.Helpers;
using Model;


namespace ADOPets.Web.ViewModels.Pulse
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {

        }

        public IndexViewModel(PetHealthMeasure petHealthMeasure)
        {
            Id = petHealthMeasure.Id;
            Date = petHealthMeasure.MeasuredDate;
            Pulse = PulseConverterHelper.GetFormatedPulse(petHealthMeasure.MeasureValue);
            PulseValue = int.Parse(petHealthMeasure.MeasureValue);
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Pulse { get; set; }

        public int PulseValue { get; set; }
    }
}