using System;
using ADOPets.Web.Common.Helpers;

namespace ADOPets.Web.ViewModels.Condition
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {}

        public IndexViewModel(Model.PetCondition condition)
        {
            Id = condition.Id;
            Name = condition.PathologyId == null
                ? condition.CustomPathology
                : EnumHelper.GetResourceValueForEnumValue(condition.PathologyId);
            VisitDate = condition.VisitDate;
            EndDate = condition.EndDate;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime VisitDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}