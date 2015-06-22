using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.FoodPlan
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

        [Display(Name = "FoodPlan_Add_FoodName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "FoodPlan_Add_FoodNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string FoodName { get; set; }

        [Display(Name = "FoodPlan_Add_RecomendedFood", ResourceType = typeof(Wording))]
        public bool RecomendedFood { get; set; }

        [Display(Name = "FoodPlan_Add_ForbidenFood", ResourceType = typeof(Wording))]
        public bool ForbidenFood { get; set; }

        [Display(Name = "FoodPlan_Add_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public Model.PetFoodPlan Map()
        {
            var petFoodPlan = new Model.PetFoodPlan
            {
                FoodName = FoodName,
                IsRecomended = RecomendedFood,
                IsForbiden = ForbidenFood,
                Comment = new EncryptedText(Comment),
                PetId = PetId
                
            };

            return petFoodPlan;
        }
    }
}