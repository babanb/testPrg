﻿using System;
using System.ComponentModel.DataAnnotations;
using Model;
using ADOPets.Web.Resources;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Common;

namespace ADOPets.Web.ViewModels.Users
{
    public class IndexViewModel
    {
        public IndexViewModel(Model.User user, Model.Veterinarian vet)
        {
            Id = user.Id;
            Usertype = user.UserTypeId;
            CenterId = user.CenterID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserTypeSub = user.UserSubscriptionId;
            Speciality = vet == null ? null : vet.VetSpecialtyID;
            Email = user.Email;
            PlanID = user.UserSubscription == null ? 0 : user.UserSubscription.SubscriptionId;
            PlanName = user.UserSubscription == null ? "" : user.UserSubscription.Subscription == null ? "" : user.UserSubscription.Subscription.Name;
            PromoCode = (user.UserSubscription != null) ? user.UserSubscription.Subscription != null ? user.UserSubscription.Subscription.PromotionCode : "" : "";
            IsTemporalPassword = user.IsEmailSent;
            IsPetExist = (user.Pets != null && user.Pets.Count > 0) ? true : false;
            if (UserTypeSub != null)
            {
                Amount = user.UserSubscription.Subscription.Amount;
                IsBase = user.UserSubscription.Subscription.IsBase;
            }
            Status = (user.UserSubscription != null) ?
                (user.UserSubscription.ispaymentDone) ? Resources.Wording.User_Index_ActiveStatus : Resources.Wording.User_Index_PaymentPendingStatus :
                "";

            if (PromoCode == Constants.FreeUserPromoCode || PlanID == 12)
            {
                Status = Resources.Wording.User_Index_ActiveStatus;
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

            if (PromoCode == Constants.FreeUserPromoCode)
            {
                IsFreeUser = true;
            }
            else { IsFreeUser = false; }


            if (user.UserSubscription != null && string.IsNullOrEmpty(user.UserSubscription.Subscription.PaymentTypeId.ToString()) && user.UserSubscription.Subscription.IsBase && string.IsNullOrEmpty(user.UserSubscription.Subscription.Amount.ToString()))
            {
                IsFreeUser = true;
            }
        }

        public int Id { get; set; }
        public UserTypeEnum Usertype { get; set; }
        public int? CenterId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public VetSpecialityEnum? Speciality { get; set; }
        public string Email { get; set; }
        public int PlanID { get; set; }
        public string PromoCode { get; set; }
        public bool IsTemporalPassword { get; set; }
        public bool IsPetExist { get; set; }
        public string PlanName { get; set; }
        public string Status { get; set; }
        public bool IsFreeUser { get; set; }
        public bool? IsBase { get; set; }
        public decimal? Amount { get; set; }
        public int? UserTypeSub { get; set; }
        public void Map(Model.User user)
        {
            user.Id = Id;
            user.UserTypeId = Usertype;
            user.CenterID = CenterId;
            user.FirstName = new EncryptedText(FirstName);
            user.LastName = new EncryptedText(LastName);
            user.Email = new EncryptedText(user.Email);
        }

    }
}