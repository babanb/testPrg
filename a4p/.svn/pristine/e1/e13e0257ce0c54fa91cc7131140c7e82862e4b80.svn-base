using System.Collections.Generic;
using System.Linq;
using Model;

namespace ADOPets.Web.ViewModels.Vitals
{
    public class VitalsViewModel
    {
        public VitalsViewModel(IList<PetHealthMeasure> vitals)
        {
            Height = vitals.Where(e => e.HealthMeasureTypeId == HealthMeasureTypeEnum.Height).Select(e => new Height.IndexViewModel(e)).ToList();
            Weight = vitals.Where(e => e.HealthMeasureTypeId == HealthMeasureTypeEnum.Weight).Select(e => new Weight.IndexViewModel(e)).ToList();
            Temperature = vitals.Where(e => e.HealthMeasureTypeId == HealthMeasureTypeEnum.Temperature).Select(e => new Temperature.IndexViewModel(e)).ToList();
            Pulse = vitals.Where(e => e.HealthMeasureTypeId == HealthMeasureTypeEnum.Pulse).Select(e => new Pulse.IndexViewModel(e)).ToList();
        }

        public IList<Height.IndexViewModel> Height { get; set; }

        public IList<Weight.IndexViewModel> Weight { get; set; }

        public IList<Temperature.IndexViewModel> Temperature { get; set; }

        public IList<Pulse.IndexViewModel> Pulse { get; set; }

    }
}