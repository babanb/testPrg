using System;
using ADOPets.Web.Common.Helpers;

namespace ADOPets.Web.ViewModels.Allergy
{
    public class IndexViewModel
    {
        public IndexViewModel()
        { }

        public IndexViewModel(Model.PetAllergy allergy)
        {
            Id = allergy.Id;
            Allergy = allergy.AllergyId == null
                ? allergy.CustomAllergy
                : EnumHelper.GetResourceValueForEnumValue(allergy.AllergyId);
            StartDate = allergy.StartDate;
            EndDate = allergy.EndDate;
            Reaction = allergy.Reaction;
            Comment = allergy.Comment;

        }

        public int Id { get; set; }
        public string Allergy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reaction { get; set; }
        public string Comment { get; set; }
    }
}