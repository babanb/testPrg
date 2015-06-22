using System;
using ADOPets.Web.Common.Helpers;

namespace ADOPets.Web.ViewModels.FoodPlan
{
    public class IndexViewModel
    {
        public IndexViewModel()
        { }

        public IndexViewModel(Model.PetFoodPlan foodplan)
        {
            Id = foodplan.Id;
            FoodName = foodplan.FoodName;
            RecomendedFood = foodplan.IsRecomended;
            ForbidenFood = foodplan.IsForbiden;
            Comment = foodplan.Comment;
        }

        public int Id { get; set; }
        public string FoodName { get; set; }
        public bool? RecomendedFood { get; set; }
        public bool? ForbidenFood { get; set; }
        public string Comment { get; set; }
    }
}