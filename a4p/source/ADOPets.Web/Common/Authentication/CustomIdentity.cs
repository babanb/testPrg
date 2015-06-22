using System;
using System.Security.Principal;
using System.Web.Security;

namespace ADOPets.Web.Common.Authentication
{
    [Serializable]
    public class CustomIdentity : IIdentity
    {
        #region Properties

        public int UserId { get; set; }

        public int? CenterId { get; set; }

        public int UserSubscriptionId { get; set; }

        public int OwnerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

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

        #endregion

        #region Implementation of IIdentity

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns>
        /// The name of the user on whose behalf the code is running.
        /// </returns>
        public string Name { get; set; }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <returns>
        /// The type of authentication used to identify the user.
        /// </returns>
        public string AuthenticationType { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <returns>
        /// true if the user was authenticated; otherwise, false.
        /// </returns>
        public bool IsAuthenticated { get; set; }

        #endregion

        #region Constructor

        public CustomIdentity(IIdentity identity)
        {
            Name = identity.Name;
            AuthenticationType = identity.AuthenticationType;
            IsAuthenticated = identity.IsAuthenticated;

            var customMembershipUser = (CustomMembershipUser)Membership.GetUser(identity.Name);
            if (customMembershipUser != null)
            {
                CenterId = customMembershipUser.CenterId;
                UserId = customMembershipUser.UserId;
                UserSubscriptionId = customMembershipUser.UserSubscriptionId;
                OwnerId = customMembershipUser.OwnerId;
                FirstName = customMembershipUser.FirstName;
                LastName = customMembershipUser.LastName;
                Email = customMembershipUser.Email;
                UserRoleName = customMembershipUser.UserRoleName;
                TimeZoneInfoId = customMembershipUser.TimeZoneInfoId;
                InfoPath = customMembershipUser.InfoPath;
                TermAndConditions = customMembershipUser.TermAndConditions;
                IsTemporalPassword = customMembershipUser.IsTemporalPassword;
                IsPlanExpired = customMembershipUser.IsPlanExpired;
                MaxPetCount = customMembershipUser.MaxPetCount;
                PromoCode = customMembershipUser.PromoCode;
                SubscriptionStartDate = customMembershipUser.SubscriptionStartDate;
                IsPaidUser = customMembershipUser.IsPaidUser;
                IsPlanRenewed = customMembershipUser.IsPlanRenewed;
            }
        }

        #endregion
    }
}