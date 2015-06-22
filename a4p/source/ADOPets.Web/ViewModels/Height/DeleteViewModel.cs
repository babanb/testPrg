using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Height
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {
            
        }

        public DeleteViewModel(PetHealthMeasure measure, HealthMeasureUnitEnum unit)
        {
            PetId = measure.PetId;
            Id = measure.Id;
            MeasureDate = measure.MeasuredDate;
            Height = HeightConverterHelper.GetFullHeigtAsString(unit, measure.MeasureValue);
            MeasureUnit = unit;
        }

        public int PetId { get; set; }

        public int Id { get; set; }

        [Display(Name = "Height_Delete_Date", ResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Height_Delete_Height", ResourceType = typeof(Wording))]
        public string Height { get; set; }

        public HealthMeasureUnitEnum MeasureUnit { get; set; }
    }
}