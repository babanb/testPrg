﻿using System;
using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using ADOPets.Web.Common.Helpers;
using Model;

namespace ADOPets.Web.Common.Authentication
{
    public static class SecurityExtentions
    {
        public static CustomPrincipal ToCustomPrincipal(this IPrincipal principal)
        {
            return (CustomPrincipal)principal;
        }

        public static int GetUserId(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.UserId;
        }

        public static int? GetCenterId(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.CenterId;
        }
        public static int GetUserSubscriptionId(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.UserSubscriptionId;
        }

        public static string GetUserFirstName(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.FirstName;
        }

        public static string GetUserLastName(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.LastName;
        }

        public static string GetUserEmail(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.Email;
        }

        public static string GetTimeZoneInfoId(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.TimeZoneInfoId;
        }

        public static string GetInfoPath(this IPrincipal principal) {
            return principal.ToCustomPrincipal().CustomIdentity.InfoPath;
        }

        public static bool HasAcceptedTermAndConditions(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.TermAndConditions;
        }

        public static bool HasPlanExpired(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.IsPlanExpired;
        }

        public static bool HasPlanRenewed(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.IsPlanRenewed;
        }

        public static bool HasTemporalPassword(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.IsTemporalPassword;
        }

        public static bool HasPaymentDone(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.IsPaidUser;
        }

        public static string GetUserAbsoluteInfoPath(this IPrincipal principal, string userInfoPath = null, string userId = null)
        {
            if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()))
            {
                userInfoPath = userInfoPath ?? principal.ToCustomPrincipal().CustomIdentity.InfoPath;
                userId = userId ?? GetOwnerId(principal).ToString();
                return Path.Combine(WebConfigHelper.UserFilesPath, userInfoPath, userId);
            }
            var chkUserId = principal.GetUserId();

            return Path.Combine(WebConfigHelper.UserFilesPath, principal.ToCustomPrincipal().CustomIdentity.InfoPath, chkUserId.ToString());
        }

        public static int GetUserMaxPetCount(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.MaxPetCount;
        }

        public static void UpdateUserMaxPetCount(this IPrincipal principal, int petcount)
        {
            var cacheKey = (string)HttpContext.Current.Session[Constants.SessionCurrentUserKey];
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                ((CustomMembershipUser)HttpRuntime.Cache[cacheKey]).MaxPetCount = petcount;
            }
        }


        public static int GetOwnerId(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.OwnerId;
        }


        public static void UpdateTermsAndConditions(this IPrincipal principal)
        {
            var cacheKey = (string)HttpContext.Current.Session[Constants.SessionCurrentUserKey];
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                ((CustomMembershipUser)HttpRuntime.Cache[cacheKey]).TermAndConditions = true;
            }
        }

        public static void UpdateIsTemporalPassword(this IPrincipal principal)
        {
            var cacheKey = (string)HttpContext.Current.Session[Constants.SessionCurrentUserKey];
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                ((CustomMembershipUser)HttpRuntime.Cache[cacheKey]).IsTemporalPassword = false;
            }
        }

        public static void UpdateIsPlanExpired(this IPrincipal principal)
        {
            var cacheKey = (string)HttpContext.Current.Session[Constants.SessionCurrentUserKey];
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                ((CustomMembershipUser)HttpRuntime.Cache[cacheKey]).IsPlanExpired = false;
            }
        }
        public static void UpdateIsPaymentDone(this IPrincipal principal)
        {
            var cacheKey = (string)HttpContext.Current.Session[Constants.SessionCurrentUserKey];
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                ((CustomMembershipUser)HttpRuntime.Cache[cacheKey]).IsPaidUser = true;
            }
        }
        public static void UpdateUserSettings(this IPrincipal principal, User user, string timeZoneInfoId)
        {
            var cacheKey = (string)HttpContext.Current.Session[Constants.SessionCurrentUserKey];
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                var cachedUser = ((CustomMembershipUser)HttpRuntime.Cache[cacheKey]);
                cachedUser.FirstName = user.FirstName;
                cachedUser.LastName = user.LastName;
                cachedUser.Email = user.Email;
                cachedUser.TimeZoneInfoId = timeZoneInfoId ?? TimeZoneInfo.Local.Id;
            }
        }

        public static void UpdateOwnerAdminInfo(this IPrincipal principal, User user)
        {
            var cacheKey = (string)HttpContext.Current.Session[Constants.SessionCurrentUserKey];
            if (HttpRuntime.Cache[cacheKey] != null)
            {
                var cachedUser = ((CustomMembershipUser)HttpRuntime.Cache[cacheKey]);
                cachedUser.OwnerId = user.Id;
                cachedUser.InfoPath = user.InfoPath;
            }
        }

        //Only for HSBC subscription
        public static bool IsHsbcUser(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.PromoCode == "HSBC";
        }

        public static bool IsHsbcUserFirstTenDays(this IPrincipal principal)
        {
            return principal.ToCustomPrincipal().CustomIdentity.PromoCode == "HSBC" &&
                   (DateTime.Today - principal.ToCustomPrincipal().CustomIdentity.SubscriptionStartDate).Days < 10;
        }

        public static bool IsHsbcUserTrial(this IPrincipal principal)
        {

            var result = principal.ToCustomPrincipal().CustomIdentity.PromoCode == "HSBC" &&
                   (DateTime.Today - principal.ToCustomPrincipal().CustomIdentity.SubscriptionStartDate).Days < 40;
            return result;
        }

        public static string GetUserRole(this IPrincipal principal)
        {
            string result = String.Empty;
            if (principal.ToCustomPrincipal().CustomIdentity.UserRoleName == UserTypeEnum.VeterinarianAdo.ToString())
            {
                result = Resources.Enums.UserTypeEnum_VeterinarianAdo;// "Veterinarian Director";
            }
            else if (principal.ToCustomPrincipal().CustomIdentity.UserRoleName == UserTypeEnum.VeterinarianExpert.ToString())
            {
                result = Resources.Enums.UserTypeEnum_VeterinarianExpert;// "Veterinarian Expert";
            }
            else if (principal.ToCustomPrincipal().CustomIdentity.UserRoleName == UserTypeEnum.Admin.ToString())
            {
                result = Resources.Enums.UserTypeEnum_Admin;
            }
            else if (principal.ToCustomPrincipal().CustomIdentity.UserRoleName == UserTypeEnum.VeterinarianLight.ToString())
            {
                result = Resources.Enums.UserTypeEnum_VeterinarianLight;
            }
            return result;
        }
    }
}