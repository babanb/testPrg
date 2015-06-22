using System;
using System.Globalization;
using Model;
using ADOPets.Web.Common.Helpers;

namespace ADOPets.Web.ViewModels.SerumElectrolyte
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {

        }

        public IndexViewModel(PetHealthMeasure measure)
        {
            Id = measure.Id;
            MeasureDate = measure.MeasuredDate;
            Value = SerumElectrolyteConverterHelper.GetFormatedSerumElectrolyte(measure.MeasureValue);
            SerumElectrolyteValue = double.Parse(measure.MeasureValue, CultureInfo.InvariantCulture);
        }

        public int Id { get; set; }
        public DateTime MeasureDate { get; set; }
        public string Value { get; set; }
        public double SerumElectrolyteValue { get; set; }
    }
}