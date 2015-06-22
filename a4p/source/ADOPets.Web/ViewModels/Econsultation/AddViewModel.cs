using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class AddViewModel
    {
        public AddSetupViewModel objSetup { get; set; }
        public AddBillingViewModel objBilling { get; set; }

        public int Price { get; set; }

        public long Id { get; set; }
        [Display(Name = "Econsultation_Add_PetName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_PetNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string PetName { get; set; }

        [Display(Name = "Econsultation_Add_CountryOfEconsultation", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_ConsultationCountryRequired", ErrorMessageResourceType = typeof(Wording))]
        public CountryEnum Country { get; set; }

        public string CountryName { get; set; }

        public string BillingCountryName { get; set; }

        [Display(Name = "Econsultation_Add_TimeOfEconsultation", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_TimeZoneRequired", ErrorMessageResourceType = typeof(Wording))]
        public TimeZoneEnum TimeZone { get; set; }

        [Display(Name = "Econsultation_Add_DayOfEconsultation", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_ConsultationDayRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? ConsultationDate { get; set; }

        [Display(Name = "Econsultation_Add_TimeOfEconsultation", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_ConsultationTImeRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? ConsultationTime { get; set; }

        [Display(Name = "Econsultation_Add_ContactType", ResourceType = typeof(Wording))]
        public EConsultationContactTypeEnum ContactType { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [Display(Name = "Econsultation_Add_PetCondition", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_PetConditionRequired", ErrorMessageResourceType = typeof(Wording))]
        public string PetCondition { get; set; }

        [Display(Name = "Econsultation_Add_SymptomsOfPet", ResourceType = typeof(Wording))]
        public string Symptoms1 { get; set; }

        public string Symptoms2 { get; set; }

        public string Symptoms3 { get; set; }

        [Display(Name = "Econsultation_Add_AssignVet", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_AssignVetRequired", ErrorMessageResourceType = typeof(Wording))]
        public int VetID { get; set; }

        [Display(Name = "Econsultation_Add_ConfirmMedicalRecords", ResourceType = typeof(Wording))]
        public bool ConfirmMedicalRecords { get; set; }

        [Display(Name = "Econsultation_Add_AgreeTerms", ResourceType = typeof(Wording))]
        public bool TermsAndConditions { get; set; }

        [Display(Name = "Econsultation_Add_SendInfoToVet", ResourceType = typeof(Wording))]
        public bool SendInfoToCurrentVet { get; set; }

        [Display(Name = "Econsultation_Add_UserId", ResourceType = typeof(Wording))]
        public Nullable<int> UserId { get; set; }

        [Display(Name = "Econsultation_Add_RequestStatus", ResourceType = typeof(Wording))]
        public string RequestStatus { get; set; }

        [Display(Name = "Econsultation_Add_RequestDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> RequestDate { get; set; }

        [Display(Name = "Econsultation_Index_AnimalType", ResourceType = typeof(Wording))]
        public PetTypeEnum? PetType { get; set; }

        [Display(Name = "Econsultation_Index_AnimalName", ResourceType = typeof(Wording))]
        public string PetName1 { get; set; }

        public string PetBirthDate { get; set; }

        public GenderEnum? PetGender { get; set; }

        public string PetImage { get; set; }

        public Model.EConsultation Map()
        {
            var eConsul = new Model.EConsultation
            {
                TitleConsultation = objSetup.PetCondition,
                DateConsultation = DateTime.Now,
                TypeConsultation = 0,
                UserId = objSetup.UserId,
                VetId = objSetup.VetID,
                PetId = objSetup.PetId,
                EconsultationStatusId = EConsultationStatusEnum.Open,
                BDateConsultation = objSetup.Date,
                BTimeConsultation = objSetup.Time,
                RDVDate = objSetup.Date,
                RDVDateTime = objSetup.Time,
                RequestedTimeRange = null,
                VetTimezoneID = TimeZone,
                EConsultationContactTypeId = objSetup.ContactType,

            };
            return eConsul;

        }
    }
}


