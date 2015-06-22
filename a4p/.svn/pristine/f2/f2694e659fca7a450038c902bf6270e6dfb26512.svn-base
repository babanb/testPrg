using System;
using ADOPets.Web.Common.Helpers;
using Model;

namespace ADOPets.Web.ViewModels.Temperature
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

            var unit = DomainHelper.GetTemperaturetMeasureUnitDefault();

            Temperature = TemperatureConverterHelper.GetFullTemperatureAsString(unit, petHealthMeasure.MeasureValue);

            TemperatureValue = TemperatureConverterHelper.GetFullTemperatureAsDouble(unit, petHealthMeasure.MeasureValue);
        }

        public IndexViewModel(PetHealthMeasure petHealthMeasure, HealthMeasureUnitEnum unit)
        {
            Id = petHealthMeasure.Id;
            Date = petHealthMeasure.MeasuredDate;
            Temperature = TemperatureConverterHelper.GetFullTemperatureAsString(unit, petHealthMeasure.MeasureValue);

            TemperatureValue = TemperatureConverterHelper.GetFullTemperatureAsDouble(unit, petHealthMeasure.MeasureValue);
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Temperature { get; set; }

        public double TemperatureValue { get; set; }
    }
}