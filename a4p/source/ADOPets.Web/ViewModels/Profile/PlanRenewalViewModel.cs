using ADOPets.Web.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace ADOPets.Web.ViewModels.Profile
{
    public class PlanRenewalViewModel
    {
        public PlanRenewalViewModel()
        {

        }       
      //  public bool IsFreeUser { get; set; }

        public string PlanType { get; set; }
        public int PlanID { get; set; }

        [Display(Name = "Profile_PlanRenewal_Promocode", ResourceType = typeof(Wording))]
        public string Promocode { get; set; }

        [Display(Name = "Profile_PlanRenewal_PlanName", ResourceType = typeof(Wording))]
        public string PlanName { get; set; }

        [Display(Name = "Profile_PlanRenewal_PlanName", ResourceType = typeof(Wording))]
        public string BasicPlan { get; set; }

        [Display(Name = "Profile_PlanRenewal_Description", ResourceType = typeof(Wording))]
        public string Description { get; set; }

        [Display(Name = "Profile_PlanRenewal_AdditionalInfo", ResourceType = typeof(Wording))]
        public string AdditionalInfo { get; set; }

        
        public string NumberOfPets { get; set; }
        [Display(Name = "Profile_PlanRenewal_AdditionalPets", ResourceType = typeof(Wording))]
        public int AdditionalPetRenewal { get; set; }

        [Display(Name = "Profile_PlanRenewal_Price", ResourceType = typeof(Wording))]
        public decimal? Price { get; set; }

        [Display(Name = "Profile_PlanRenewal_StartDate", ResourceType = typeof(Wording))]
        public DateTime StartDate { get; set; }

        [Display(Name = "Profile_PlanRenewal_ExpirationDate", ResourceType = typeof(Wording))]
        public DateTime EndDate { get; set; }

        [Display(Name = "Profile_PlanRenewal_Duration", ResourceType = typeof(Wording))]
        public int Duration { get; set; }

        [Display(Name = "Profile_PlanRenewal_RemainingDays", ResourceType = typeof(Wording))]
        public int RemainingDays { get; set; }

        public int MaxPetCount { get; set; }
        public string FinalplanName { get; set; }
        public string Myplan { get; set; }
        public string DeletedPets { get; set; }
        public int DeletedUnUsedPets { get; set; }
    }
}