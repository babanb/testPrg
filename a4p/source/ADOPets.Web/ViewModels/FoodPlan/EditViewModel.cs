using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.FoodPlan
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(Model.PetFoodPlan foodplan)
        {
            Id = foodplan.Id;
            FoodName = foodplan.FoodName;
            RecomendedFood = foodplan.IsRecomended.HasValue ? foodplan.IsRecomended.Value : false;
            ForbidenFood = foodplan.IsForbiden.HasValue ? foodplan.IsForbiden.Value : false;
            Comment = foodplan.Comment;
            PetId = foodplan.PetId;
        }

        public int Id { get; set; }

        [Display(Name = "FoodPlan_Add_FoodName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "FoodPlan_Add_FoodNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FoodName { get; set; }

        [Display(Name = "FoodPlan_Add_RecomendedFood", ResourceType = typeof(Wording))]
        public bool RecomendedFood { get; set; }

        [Display(Name = "FoodPlan_Add_ForbidenFood", ResourceType = typeof(Wording))]
        public bool ForbidenFood { get; set; }

        [Display(Name = "FoodPlan_Add_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public int PetId { get; set; }

        public void Map(Model.PetFoodPlan foodplan)
        {
            foodplan.FoodName = FoodName;
            foodplan.IsRecomended = RecomendedFood;
            foodplan.IsForbiden = ForbidenFood;
            foodplan.Comment = new EncryptedText(Comment);
            
        }
    }
}