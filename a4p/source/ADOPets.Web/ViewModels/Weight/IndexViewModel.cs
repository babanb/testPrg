using System;
using ADOPets.Web.Common.Helpers;
using Model;

namespace ADOPets.Web.ViewModels.Weight
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

            var unit = DomainHelper.GetWeightMeasureUnitDefault();

            Weight = WeightConverterHelper.GetFullWeightAsString(unit, petHealthMeasure.MeasureValue);
            WeightValue = WeightConverterHelper.GetFullWeightAsDouble(unit, petHealthMeasure.MeasureValue);
        }

        public IndexViewModel(PetHealthMeasure petHealthMeasure, HealthMeasureUnitEnum unit)
        {
            Id = petHealthMeasure.Id;
            Date = petHealthMeasure.MeasuredDate;
            Weight = WeightConverterHelper.GetFullWeightAsString(unit, petHealthMeasure.MeasureValue);
            WeightValue = WeightConverterHelper.GetFullWeightAsDouble(unit, petHealthMeasure.MeasureValue);
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Weight { get; set; }

        public double WeightValue { get; set; }
    }
}