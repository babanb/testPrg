using System;
using System.Globalization;
using Model;
using ADOPets.Web.Common.Helpers;


namespace ADOPets.Web.ViewModels.Hemogram
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
            Value = HemogramConverterHelper.GetFormatedHemogram(measure.MeasureValue);
            HemogramValue = double.Parse(measure.MeasureValue, CultureInfo.InvariantCulture);
        }

        public int Id { get; set; }
        public DateTime MeasureDate { get; set; }
        public string Value { get; set; }
        public double HemogramValue { get; set; }
    }
}