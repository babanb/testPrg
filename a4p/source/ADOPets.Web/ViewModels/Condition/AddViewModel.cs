using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Condition
{
    public class AddViewModel
    {
        public AddViewModel()
        {
            
        }

        public AddViewModel(int petid)
        {
            PetId = petid;
        }

        public int PetId { get; set; }

        [Display(Name = "Condition_Add_PathologyName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Condition_Add_PathologyNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string PathologyName { get; set; }

        [Display(Name = "Condition_Add_VisitDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Condition_Add_VisitDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? VisitDate { get; set; }

        [Display(Name = "Condition_Add_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Condition_Add_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public Model.PetCondition Map()
        {
            var petCondition = new Model.PetCondition
            {
                CustomPathology = PathologyName,
                VisitDate = VisitDate.Value,
                EndDate = EndDate.HasValue ? EndDate.Value : (DateTime?) null,
                Comment = new EncryptedText(Comment),
                PetId = PetId
            };

            return petCondition;
        }
    }
}