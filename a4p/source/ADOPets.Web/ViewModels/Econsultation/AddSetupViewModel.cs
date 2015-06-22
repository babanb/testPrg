using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using System.Globalization;
using System.Linq;
using System.Web;
using Model;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class AddSetupViewModel
    {
        public AddSetupViewModel()
        {
            if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                Country = CountryEnum.UnitedStates;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
            {
                Country = CountryEnum.India;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                Country = CountryEnum.France;
            }
        }

        public AddSetupViewModel(EConsultation e)
        {
            PetCondition = e.TitleConsultation;
            Date = e.DateConsultation;
            OwnerId = Convert.ToInt32(e.UserId);
            VetID = Convert.ToInt32(e.VetId);
            PetId = Convert.ToInt32(e.PetId);
            //status = EConsultationStatusEnum.PaymentPending,
            Date = e.BDateConsultation;
            Time = e.BTimeConsultation;
            Date = e.RDVDate;
            Time = e.RDVDateTime;
            TimeZoneId = e.VetTimezoneID;
            ContactType = e.EConsultationContactTypeId;
            //    EConsultationContactValue = (ContactType.HasValue) ? ((ContactType.Value == EConsultationContactTypeEnum.Email) ? Email : Phone) : null,
            ContactType = e.EConsultationContactTypeId;


            CountryId = e.CountryId;
            Symptoms1 = e.Symptoms1;
            Symptoms2 = e.Symptoms2;
            Symptoms3 = e.Symptoms3;

            Id = e.ID;
        }

        public int Id { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Smo_Add_SelectOwner", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Smo_Add_SelectOwnerRequired", ErrorMessageResourceType = typeof(Wording))]
        public int OwnerId { get; set; }

        [Display(Name = "Econsultation_Add_SelectPet", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_PetNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public int PetId { get; set; }

        [Display(Name = "Econsultation_Add_Symptoms1", ResourceType = typeof(Wording))]
        public string Symptoms1 { get; set; }

        [Display(Name = "Econsultation_Add_Symptoms2", ResourceType = typeof(Wording))]
        public string Symptoms2 { get; set; }

        [Display(Name = "Econsultation_Add_Symptoms3", ResourceType = typeof(Wording))]
        public string Symptoms3 { get; set; }

        [Display(Name = "Econsultation_Add_PetName", ResourceType = typeof(Wording))]
        public string PetName { get; set; }

        [Display(Name = "Econsultation_Add_CountryOfEconsultation", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_ConsultationCountryRequired", ErrorMessageResourceType = typeof(Wording))]
        public CountryEnum Country { get; set; }

        public CountryEnum? CountryId { get; set; }

        [Display(Name = "Econsultation_Add_TimeZoneOfEconsultation", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_TimeZoneRequired", ErrorMessageResourceType = typeof(Wording))]
        public TimeZoneEnum TimeZone { get; set; }

        public TimeZoneEnum? TimeZoneId { get; set; }

        [Display(Name = "Econsultation_Add_DayOfEconsultation", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_ConsultationDayRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? Date { get; set; }

        [Display(Name = "Econsultation_Add_Time", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_ConsultationTImeRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? Time { get; set; }

        [Display(Name = "Econsultation_Add_ContactType", ResourceType = typeof(Wording))]
        public EConsultationContactTypeEnum? ContactType { get; set; }

        [Display(Name = "Econsultation_Add_Phone", ResourceType = typeof(Wording))]
        [RegularExpression(@"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})", ErrorMessageResourceName = "Econsultation_Add_InValidPhone", ErrorMessageResourceType = typeof(Wording))]
        public string Phone { get; set; }

        [RegularExpression(@"^\w+([-+.]*[\w-]+)*@(\w+([-.]?\w+)){1,}\.\w{2,4}$", ErrorMessageResourceName = "Econsultation_Add_Invalidemail", ErrorMessageResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Econsultation_Add_PetCondition", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_PetConditionRequired", ErrorMessageResourceType = typeof(Wording))]
        public string PetCondition { get; set; }

        [Display(Name = "Econsultation_Add_AssignVet", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_AssignVetRequired", ErrorMessageResourceType = typeof(Wording))]
        public int VetID { get; set; }

        [Display(Name = "Econsultation_Add_VetName", ResourceType = typeof(Wording))]
        public string VetName { get; set; }

        [Display(Name = "Econsultation_Add_ConfirmMedicalRecords", ResourceType = typeof(Wording))]
        public bool ConfirmMedicalRecords { get; set; }

        [Display(Name = "Econsultation_Add_AgreeTerms", ResourceType = typeof(Wording))]
        public bool TermsAndConditions { get; set; }

        [Required]
        public bool certify { get; set; }

        public string PetImage { get; set; }

        public List<Pet.IndexViewModel> Pets { get; set; }

        public List<Veterinarian.IndexViewModel> vets { get; set; }


        public Model.EConsultation Map()
        {
            var eConsultation = new Model.EConsultation
            {
                TitleConsultation = PetCondition,
                PetId = PetId,
                Symptoms1 = Symptoms1,
                Symptoms2 = Symptoms2,
                Symptoms3 = Symptoms3,
                DateConsultation = Date,
                BTimeConsultation = Time,
                BDateConsultation = Time,
                EConsultationContactTypeId = ContactType,
                VetId = VetID,
                UserId = UserId
            };

            return eConsultation;
        }

        /***********Admin/VD/VA************************/

        public Model.EConsultation MapEC()
        {
            var eConsul = new Model.EConsultation
            {
                TitleConsultation = PetCondition,
                DateConsultation = Date,
                TypeConsultation = 0,
                UserId = OwnerId,
                VetId = VetID,
                PetId = PetId,
                EconsultationStatusId = EConsultationStatusEnum.PaymentPending,
                BDateConsultation = Date,
                BTimeConsultation = Convert.ToDateTime(Time),
                RDVDate = Date,
                RDVDateTime = Convert.ToDateTime(Time),
                RequestedTimeRange = null,
                VetTimezoneID = TimeZone,
                EConsultationContactTypeId = ContactType,
                EConsultationContactValue = (ContactType.HasValue) ? ((ContactType.Value == EConsultationContactTypeEnum.Email) ? Email : Phone) : null,
                CountryId = Country,
                Symptoms1 = Symptoms1,
                Symptoms2 = Symptoms2,
                Symptoms3 = Symptoms3,
                Shared = 1,
                PurchaseFlag = "1",
                PaymentFlag = 0,
                Survey = "N",
                VetNotificationFlag = 1,
                VetNotificationDateTime = DateTime.Now,
                ID = Id,
                IsRead = false,
                CreatedBy = UserId,
                IsOwnerRead = false
            };
            return eConsul;
        }

        public Model.EconsultationRoom MapEcRoom()
        {
            var eConsulRoom = new Model.EconsultationRoom
            {
                Status = Convert.ToInt16(EConsultationStatusEnum.Open),
                EConsultationId = Id
            };
            return eConsulRoom;
        }
    }
}