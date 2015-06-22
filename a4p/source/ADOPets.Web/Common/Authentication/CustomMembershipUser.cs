﻿using System;
using System.Web.Security;
using Model;

namespace ADOPets.Web.Common.Authentication
{
    public class CustomMembershipUser : MembershipUser
    {
        #region Properties

        public int UserId { get; set; }

        public string Email { get; set; }

        public int UserSubscriptionId { get; set; }

        public int OwnerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserRoleName { get; set; }

        public string TimeZoneInfoId { get; set; }

        public string InfoPath { get; set; }

        public bool TermAndConditions { get; set; }

        public bool IsPlanExpired { get; set; }

        public bool IsTemporalPassword { get; set; }

        public int MaxPetCount { get; set; }

        public string PromoCode { get; set; }

        public DateTime SubscriptionStartDate { get; set; }

        public bool IsPaidUser { get; set; }

        public bool IsPlanRenewed { get; set; }

        public int? CenterId { get; set; }
        #endregion

        public CustomMembershipUser(Login login)
            : base("CustomMembershipProvider", login.UserName, login.User.Id, login.User.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            IsPaidUser = true;
            var user = login.User;
            UserId = user.Id;
            CenterId = user.CenterID;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserRoleName = user.UserTypeId.ToString();
            TimeZoneInfoId = user.TimeZone != null ? user.TimeZone.TimeZoneInfoId : TimeZoneInfo.Local.Id;
            InfoPath = user.InfoPath;
            TermAndConditions = user.GeneralConditions;
            IsTemporalPassword = login.IsTemporalPassword;


            if (user.UserTypeId == UserTypeEnum.OwnerAdmin)
            {
                var aditionalPetCount = user.UserSubscription.SubscriptionService != null && user.UserSubscription.SubscriptionService.AditionalPetCount.HasValue
                    ? user.UserSubscription.SubscriptionService.AditionalPetCount.Value
                    : 0;
                MaxPetCount = user.UserSubscription.Subscription.MaxPetCount + aditionalPetCount;
                PromoCode = user.UserSubscription.Subscription.PromotionCode;
                SubscriptionStartDate = user.UserSubscription.StartDate.Value;
                UserSubscriptionId = user.UserSubscriptionId.Value;
                var promotionCode = string.IsNullOrEmpty(user.UserSubscription.Subscription.PromotionCode) ? "" : user.UserSubscription.Subscription.PromotionCode;

                if (!promotionCode.Equals(Constants.FreeUserPromoCode))
                {
                    //   IsPaidUser = user.UserSubscription.ispaymentDone;
                    if (user.UserSubscription.Subscription.IsTrial == true)
                    {
                        IsPaidUser = true;
                    }
                    else
                    {
                        IsPaidUser = user.UserSubscription.ispaymentDone;
                    }
                }
                else
                {
                    IsPaidUser = true;
                }

                if (user.UserSubscription != null && user.UserSubscription.TempUserSubscriptionId != null)
                {
                    IsPlanRenewed = true;
                }
                else
                {
                    IsPlanRenewed = false;
                }

                IsPlanExpired = user.UserSubscription.RenewalDate < DateTime.Today ? true : false;
                //IsPlanExpired = IsPaidUser == true ? false : true;
            }
        }
    }
}