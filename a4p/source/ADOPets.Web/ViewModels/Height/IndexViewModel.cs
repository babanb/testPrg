using System;
using ADOPets.Web.Common.Helpers;
using Model;

namespace ADOPets.Web.ViewModels.Height
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

            var unit = DomainHelper.GetHeightMeasureUnitDefault();

            Height = HeightConverterHelper.GetFullHeigtAsString(unit, petHealthMeasure.MeasureValue);

            HeightValue = HeightConverterHelper.GetFullHeightAsDouble(unit, petHealthMeasure.MeasureValue);
        }

        public IndexViewModel(PetHealthMeasure petHealthMeasure, HealthMeasureUnitEnum unit)
        {
            Id = petHealthMeasure.Id;
            Date = petHealthMeasure.MeasuredDate;
            Height = HeightConverterHelper.GetFullHeigtAsString(unit, petHealthMeasure.MeasureValue);
            HeightValue = HeightConverterHelper.GetFullHeightAsDouble(unit, petHealthMeasure.MeasureValue);
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Height { get; set; }

        public double HeightValue{ get; set; }
    }
}