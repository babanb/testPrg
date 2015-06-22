using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ADOPets.Web.Common.Enums;
using ADOPets.Web.ViewModels.Vitals;
using Model;

namespace ADOPets.Web.ViewModels.HealthMeasureTracker
{
    public class HealthMeasureTrackerViewModel
    {
        public HealthMeasureTrackerViewModel()
        {
            
        }

        public HealthMeasureTrackerViewModel(Model.Pet pet, MedicalRecordTypeEnum mRType, int hMType)
        {
            PetId = pet.Id;
            Current = mRType == MedicalRecordTypeEnum.HealthMeasureTracker
                ? (HealthMeasureGroupEnum)hMType
                : HealthMeasureGroupEnum.Vitals;

            Vitals = new VitalsViewModel(pet.PetHealthMeasures.Where(p => p.HealthMeasureGroupEnum == HealthMeasureGroupEnum.Vitals && !p.IsDeleted).OrderByDescending(hm => hm.MeasuredDate).ToList());

            Cbg = pet.PetHealthMeasures.Where(m => m.HealthMeasureGroupEnum == HealthMeasureGroupEnum.CBG && !m.IsDeleted).OrderByDescending(m => m.MeasuredDate).Select(m => new CBG.IndexViewModel(m)).ToList();
            Hmglbin = pet.PetHealthMeasures.Where(m => m.HealthMeasureGroupEnum == HealthMeasureGroupEnum.Hemoglobin && !m.IsDeleted).OrderByDescending(m => m.MeasuredDate).Select(m => new Hemoglobin.IndexViewModel(m)).ToList();
            Hmgram = pet.PetHealthMeasures.Where(m => m.HealthMeasureGroupEnum == HealthMeasureGroupEnum.Hemogram && !m.IsDeleted).OrderByDescending(m => m.MeasuredDate).Select(m => new Hemogram.IndexViewModel(m)).ToList();
            SerumElectro = pet.PetHealthMeasures.Where(m => m.HealthMeasureGroupEnum == HealthMeasureGroupEnum.SerumElectrolytes && !m.IsDeleted).OrderByDescending(m => m.MeasuredDate).Select(m => new SerumElectrolyte.IndexViewModel(m)).ToList();

            //add others Health Measures properties here

        }

        public int PetId { get; set; }

        public HealthMeasureGroupEnum Current { get; set; }

        public VitalsViewModel Vitals { get; set; }

        public IEnumerable<CBG.IndexViewModel> Cbg { get; set; }
        public IEnumerable<Hemoglobin.IndexViewModel> Hmglbin { get; set; }
        public IEnumerable<Hemogram.IndexViewModel> Hmgram { get; set; }
        public IEnumerable<SerumElectrolyte.IndexViewModel> SerumElectro { get; set; }
    }

}