using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.Condition
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {
            
        }

        public DeleteViewModel(Model.PetCondition condition)
        {
            Id = condition.Id;
            PathologyName = condition.CustomPathology;
            VisitDate = condition.VisitDate;
            EndDate = condition.EndDate;
            Comment = condition.Comment;
            PetId = condition.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "Condition_Delete_PathologyName", ResourceType = typeof(Wording))]
        public string PathologyName { get; set; }

        [Display(Name = "Condition_Delete_VisitDate", ResourceType = typeof(Wording))]
        public DateTime VisitDate { get; set; }

        [Display(Name = "Condition_Delete_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Condition_Delete_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public int PetId { get; set; }
    }
}