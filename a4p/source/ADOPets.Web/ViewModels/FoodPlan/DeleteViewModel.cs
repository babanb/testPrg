using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.FoodPlan
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(Model.PetFoodPlan foodplan)
        {
            Id = foodplan.Id;
            FoodName = foodplan.FoodName;
            RecomendedFood = foodplan.IsRecomended;
            ForbidenFood = foodplan.IsForbiden;
            Comment = foodplan.Comment;
            PetId = foodplan.PetId;
            
        }

        public int Id { get; set; }

        [Display(Name = "FoodPlan_Add_FoodName", ResourceType = typeof(Wording))]
        public string FoodName { get; set; }

        [Display(Name = "FoodPlan_Add_RecomendedFood", ResourceType = typeof(Wording))]
        public bool? RecomendedFood { get; set; }

        [Display(Name = "FoodPlan_Add_ForbidenFood", ResourceType = typeof(Wording))]
        public bool? ForbidenFood { get; set; }

        [Display(Name = "FoodPlan_Add_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public int PetId { get; set; }
    }
}