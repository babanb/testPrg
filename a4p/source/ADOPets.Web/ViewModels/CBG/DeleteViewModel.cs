using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.CBG
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
            CBG = CBGConverterHelper.GetFormatedCBG(measure.MeasureValue);
        }

        public int PetId { get; set; }

        public int Id { get; set; }

        [Display(Name = "CBG_Delete_Date", ResourceType = typeof(Wording))]
        public DateTime MeasureDate { get; set; }

        [Display(Name = "CBG_Delete_CBG", ResourceType = typeof(Wording))]
        public string CBG { get; set; }
    }
}