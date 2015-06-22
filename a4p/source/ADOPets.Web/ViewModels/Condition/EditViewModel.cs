using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Condition
{
    public class EditViewModel
    {
        public EditViewModel()
        {
            
        }

        public EditViewModel(Model.PetCondition condition)
        {
            Id = condition.Id;
            PathologyName = condition.CustomPathology;
            VisitDate = condition.VisitDate;
            EndDate = condition.EndDate;
            Comment = condition.Comment;
            PetId = condition.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "Condition_Edit_PathologyName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Condition_Edit_PathologyNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string PathologyName { get; set; }

        [Display(Name = "Condition_Edit_VisitDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Condition_Edit_VisitDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime VisitDate { get; set; }

        [Display(Name = "Condition_Edit_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Condition_Edit_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public int PetId { get; set; }

        public void Map(Model.PetCondition condition)
        {
            condition.CustomPathology = PathologyName;
            condition.VisitDate = VisitDate;
            condition.EndDate = EndDate;
            condition.Comment = new EncryptedText(Comment);
        }
    }
}