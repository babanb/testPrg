using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Hemogram
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(PetHealthMeasure measure)
        {
            PetId = measure.PetId;
            Id = measure.Id;
            MeasureDate = measure.MeasuredDate;
            Hemogram = HemogramConverterHelper.GetFormatedHemogram(measure.MeasureValue);
        }

        public int PetId { get; set; }

        public int Id { get; set; }

        [Display(Name = "Hemogram_Delete_Date", ResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "Hemogram_Delete_Hemogram", ResourceType = typeof(Wording))]
        public string Hemogram { get; set; }
    }
}