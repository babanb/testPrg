﻿using System;
using System.ComponentModel.DataAnnotations;
using Model;
using ADOPets.Web.Resources;
using ADOPets.Web.Common.Helpers;
using System.Web.Mvc;
using System.Globalization;
using System.Linq;
using ADOPets.Web.Common;

namespace ADOPets.Web.ViewModels.Users
{
    public class ViewUserViewModel
    {
        public ViewUserViewModel(User user, string finalPlan)
        {
            Id = user.Id;
            Usertype = user.UserTypeId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            BirthDate = !String.IsNullOrEmpty(user.BirthDate)
                ? Convert.ToDateTime(user.BirthDate, CultureInfo.InvariantCulture).ToShortDateString()
                : String.Empty;
            Gender = user.GenderId;
            Address1 = user.Address1;
            Address2 = user.Address2;
            TimeZone = user.TimeZoneId;
            City = user.City;
            Country = user.CountryId;
            State = user.StateId;
            Zip = user.PostalCode;
            PrimaryPhone = user.PrimaryPhone;
            SecondaryPhone = user.SecondaryPhone;
            Image = user.ProfileImage;

            Status = Resources.Wording.User_Index_ActiveStatus;

            CreationDate = !string.IsNullOrEmpty(user.CreationDate.ToString())
            ? (DateTime?)Convert.ToDateTime(user.CreationDate, CultureInfo.InvariantCulture)
            : null;

            //other Details
            if (user.UserTypeId != UserTypeEnum.OwnerAdmin && user.UserTypeId != UserTypeEnum.Admin)
            {
                if (user.Veterinarians != null && user.Veterinarians.Count() > 0)
                {
                    user.Veterinarian = user.Veterinarians.First();
                    CenterId = user.CenterID;
                    Speciality = user.Veterinarian.VetSpecialtyID;
                    Hospital = user.Veterinarian.HospitalName;
                    Diploma = user.Veterinarian.Diploma;
                    StateExercise = user.Veterinarian.StateExercise;
                    DEA = user.Veterinarian.DEA;
                    NPI = user.Veterinarian.NPI;
                    LicenceToPractice = user.Veterinarian.LicenceToPractice;
                    Comments = user.Veterinarian.Comment;
                }
            }
            else if (user.UserTypeId == UserTypeEnum.OwnerAdmin)
            {
                Plan = finalPlan;
                BasicPlan = user.UserSubscription.Subscription.Name;
                PlanId = user.UserSubscription.Subscription.Id;
                IsBase = user.UserSubscription.Subscription.IsBase;
                Amount = user.UserSubscription.Subscription.Amount;
                Promocode = user.UserSubscription.Subscription.PromotionCode;
                AdditionalInfo = user.UserSubscription.Subscription.AditionalInfo;
                AdditionalPets = (user.UserSubscription.SubscriptionService != null ? user.UserSubscription.SubscriptionService.AditionalPetCount : 0) ?? 0;
                AdditionalPets = AdditionalPets + (user.UserSubscription.AdditionalPetcount != null ? user.UserSubscription.AdditionalPetcount : 0) ?? 0;
                description = user.UserSubscription.Subscription.Description;
                var startDate = user.UserSubscription.StartDate ?? DateTime.Now;
                var endDate = user.UserSubscription.RenewalDate ?? DateTime.Now;
                StartDate = startDate;
                ExpirationDate = endDate;
                Dueration = user.UserSubscription.Subscription.Duration == 0 ? (int)(endDate.Subtract(startDate).TotalDays) + 1 : user.UserSubscription.Subscription.Duration;
                RemainingDays = (int)(endDate - DateTime.Today).TotalDays < 0 ? 0 : (int)(endDate - DateTime.Today).TotalDays;
                RemainingDays = RemainingDays == 0 ? RemainingDays : RemainingDays + 1;
                isTrial = user.UserSubscription.Subscription.IsTrial ?? false;
                isRenewed = user.UserSubscription.TempUserSubscriptionId != null ? true : false;
                MaxPetCount = user.UserSubscription.Subscription.MaxPetCount;
                Status = (user.UserSubscription != null) ?
             (user.UserSubscription.ispaymentDone) ? Resources.Wording.User_Index_ActiveStatus : Resources.Wording.User_Index_PaymentPendingStatus :
             "";

                if (Promocode == Constants.FreeUserPromoCode || user.UserSubscription.Subscription.Id == 12)
                {
                    Status = Resources.Wording.User_Index_ActiveStatus;
                }

                if (user.GeneralConditions == false && user.UserSubscription.ispaymentDone == false)
                {
                    IsNewUser = true;
                }

                if (user.UserSubscription != null && user.UserSubscription.Subscription.Amount == null && user.UserSubscription.Subscription.IsBase && user.UserSubscription.Subscription.PlanTypeId == PlanTypeEnum.BasicFree)
                {
                    IsFreeUser = true;
                }
                else
                {
                    //  IsNewUser = false;
                    IsFreeUser = false;
                }
            }

            if (user.UserTypeId != UserTypeEnum.OwnerAdmin)
            {
                Status = Resources.Wording.User_Index_ActiveStatus;
            }
            if (user.UserStatusId == UserStatusEnum.Disabled)
            {
                Status = Resources.Wording.User_Index_InActiveStatus;
            }

            if (IsBase == true && Amount == null && Status != Resources.Wording.User_Index_PaymentPendingStatus)
            {
                Status = Resources.Wording.User_Index_ActiveStatus;
            }

        }

        public bool IsFreeUser { get; set; }
        public bool IsNewUser { get; set; }

        public string Status { get; set; }
        public int PlanId { get; set; }
        public bool IsBase { get; set; }
        public decimal? Amount { get; set; }

        //Basic Info
        public int Id { get; set; }

        [Display(Name = "Users_AddUser_UserType", ResourceType = typeof(Wording))]
        public UserTypeEnum Usertype { get; set; }

        [Display(Name = "Users_AddUser_FirstName", ResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Users_AddUser_LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }


        public string Email { get; set; }

        [Display(Name = "Users_AddUser_DateOfBirth", ResourceType = typeof(Wording))]
        public string BirthDate { get; set; }

        [Display(Name = "Profile_OwnerEdit_CreationDate", ResourceType = typeof(Wording))]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "Users_AddUser_Gender", ResourceType = typeof(Wording))]
        public GenderEnum? Gender { get; set; }

        [Display(Name = "Users_AddUser_TimeZone", ResourceType = typeof(Wording))]
        public TimeZoneEnum? TimeZone { get; set; }

        [Display(Name = "Users_AddUser_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "Users_AddUser_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Display(Name = "Users_AddUser_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Display(Name = "Users_AddUser_Country", ResourceType = typeof(Wording))]
        public CountryEnum? Country { get; set; }

        [Display(Name = "Users_AddUser_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessageResourceName = "Account_SignUp_ZipRequiredNumber", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_Zip", ResourceType = typeof(Wording))]
        public string Zip { get; set; }

        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "Account_SignUp_InValidPhone", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_PrimaryPhone", ResourceType = typeof(Wording))]
        public string PrimaryPhone { get; set; }

        [RegularExpression(@"^[0-9\-. ]+", ErrorMessageResourceName = "Account_SignUp_InValidPhone", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Users_AddUser_SecondaryPhone", ResourceType = typeof(Wording))]
        public string SecondaryPhone { get; set; }

        [Display(Name = "Users_ViewUser_ProfilePic", ResourceType = typeof(Wording))]
        public byte[] Image { get; set; }

        //Other Details
        [Display(Name = "Users_AddUser_Speciality", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Users_AddUser_SpecialityIsRequired", ErrorMessageResourceType = typeof(Wording))]
        public VetSpecialityEnum? Speciality { get; set; }
        public int SpecialityId { get; set; }

        [Display(Name = "Users_AddUser_Hospital", ResourceType = typeof(Wording))]
        public string Hospital { get; set; }

        [Display(Name = "Users_AddUser_Diploma", ResourceType = typeof(Wording))]
        public string Diploma { get; set; }

        [Display(Name = "Users_AddUser_StateExercise", ResourceType = typeof(Wording))]
        public string StateExercise { get; set; }

        [Display(Name = "Users_AddUser_DEA", ResourceType = typeof(Wording))]
        public string DEA { get; set; }

        [Display(Name = "Users_AddUser_NPI", ResourceType = typeof(Wording))]
        public string NPI { get; set; }

        [Display(Name = "Users_AddUser_LicenceToPractice", ResourceType = typeof(Wording))]
        public string LicenceToPractice { get; set; }

        [Display(Name = "Users_AddUser_Comments", ResourceType = typeof(Wording))]
        public string Comments { get; set; }

        [Display(Name = "Users_AddUser_Center", ResourceType = typeof(Wording))]
        public string center { get; set; }
        public int? CenterId { get; set; }

        //Owner Plan Information
        public string BasicPlan { get; set; }
        public string Plan { get; set; }
        public string Promocode { get; set; }
        public string AdditionalInfo { get; set; }
        public int AdditionalPets { get; set; }
        public string description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Dueration { get; set; }
        public int RemainingDays { get; set; }
        public bool isTrial { get; set; }
        public bool isRenewed { get; set; }

        public int MaxPetCount { get; set; }
    }
}