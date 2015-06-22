using System;
using System.Globalization;
using Model;
using ADOPets.Web.Common.Helpers;

namespace ADOPets.Web.ViewModels.Hemoglobin
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
            Value = HemoglobinConverterHelper.GetFormatedHemoglobin(measure.MeasureValue);
            HemoglobinValue = double.Parse(measure.MeasureValue, CultureInfo.InvariantCulture);
        }

        public int Id { get; set; }
        public DateTime MeasureDate { get; set; }
        public string Value { get; set; }
        public double HemoglobinValue { get; set; }
    }
}