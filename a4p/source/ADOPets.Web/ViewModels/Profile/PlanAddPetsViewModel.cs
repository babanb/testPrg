using ADOPets.Web.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace ADOPets.Web.ViewModels.Profile
{
    public class PlanAddPetsViewModel
    {

        public int PlanId { get; set; }

        [Display(Name = "Profile_PlanEdit_Promocode", ResourceType = typeof(Wording))]
        public string Promocode { get; set; }

        [Display(Name = "Profile_PlanEdit_PlanName", ResourceType = typeof(Wording))]
        public string PlanName { get; set; }

        [Display(Name = "Profile_PlanEdit_PlanName", ResourceType = typeof(Wording))]
        public string BasicPan { get; set; }

        [Display(Name = "Profile_PlanEdit_Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }

        [Display(Name = "Profile_PlanEdit_AdditionalInfo", ResourceType = typeof(Wording))]
        public string AdditionalInfo { get; set; }

        public int NumberOfPets { get; set; }

        [Display(Name = "Profile_PlanEdit_AdditionalPets", ResourceType = typeof(Wording))]
        public int AdditionalPets { get; set; }

        [Display(Name = "Profile_PlanEdit_Price", ResourceType = typeof(Wording))]
        public string Price { get; set; }

        [Display(Name = "Profile_PlanRenewal_PriceToPay", ResourceType = typeof(Wording))]
        public decimal PriceToPay { get; set; }

        [Display(Name = "Profile_PlanEdit_StartDate", ResourceType = typeof(Wording))]
        public DateTime StartDate { get; set; }

        [Display(Name = "Profile_PlanEdit_ExpirationDate", ResourceType = typeof(Wording))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Profile_PlanEdit_Duration", ResourceType = typeof(Wording))]
        public int Duration { get; set; }

        [Display(Name = "Profile_PlanEdit_RemainingDays", ResourceType = typeof(Wording))]
        public int RemainingDays { get; set; }

        public int MaxPetCount { get; set; }
        public string Myplan { get; set; }
    }

}