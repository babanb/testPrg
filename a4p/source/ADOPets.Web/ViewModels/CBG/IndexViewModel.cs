using System;
using System.Globalization;
using Model;
using ADOPets.Web.Common.Helpers;

namespace ADOPets.Web.ViewModels.CBG
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
            Value = CBGConverterHelper.GetFormatedCBG(measure.MeasureValue);
            CBGValue = double.Parse(measure.MeasureValue, CultureInfo.InvariantCulture);
        }

        public int Id { get; set; }
        public DateTime MeasureDate { get; set; }
        public string Value { get; set; }
        public double CBGValue { get; set; }
    }
}