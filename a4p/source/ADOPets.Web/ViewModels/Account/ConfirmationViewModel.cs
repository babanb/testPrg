﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Security;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Common.Payment.Model;
using ADOPets.Web.Resources;
using Model;
using Model.Tools;
using Subscription = Model.Subscription;

namespace ADOPets.Web.ViewModels.Account
{
    public class ConfirmationViewModel
    {
        public ConfirmationViewModel()
        {

        }

        public ConfirmationViewModel(BasicInfoViewModel userInfo, BillingViewModel billingInfo = null)
        {
            IsFreeUser = false;
            //User Info
            Promocode = userInfo.Promocode;
            Plan = userInfo.PlanName;
            Price = userInfo.Price.HasValue ? userInfo.Price.Value : 0;
            AdditionalPets = userInfo.AdditionalPetCount;
            FirstName = userInfo.FirstName;
            MiddleName = userInfo.MiddleName;
            LastName = userInfo.LastName;
            BirthDate = userInfo.BirthDate;
            Email = userInfo.Email.ToLower();
            Gender = userInfo.Gender;

            Address1 = userInfo.Address1;
            Address2 = userInfo.Address2;
            Zip = userInfo.Zip;
            City = userInfo.City;
            State = userInfo.State;
            Country = userInfo.Country;

            PrimaryPhone = userInfo.PrimaryPhone;
            SecondaryPhone = userInfo.SecondaryPhone;
            TimeZone = userInfo.TimeZone;

            ReferralName = userInfo.Reference;
            UserName = userInfo.Username;
            Password = userInfo.Password;
            ReferralSource = userInfo.ReferralSource;

            if (string.IsNullOrEmpty(Price.ToString()) || Price == 0) { IsFreeUser = true; }

            //Billing Info
            if (billingInfo != null)
            {
                CreditCardType = billingInfo.CreditCardType;
                CreditCardNumber = billingInfo.CreditCardNumber;
                ExpirationDate = billingInfo.ExpirationDate;
                CVV = billingInfo.CVV;

                if (billingInfo.IsBillingAddressSame)
                {
                    BillingAddress1 = userInfo.Address1;
                    BillingAddress2 = userInfo.Address2;
                    BillingZip = userInfo.Zip;
                    BillingCity = userInfo.City;
                    BillingState = userInfo.State;
                    BillingCountry = userInfo.Country;
                }
                else
                {
                    BillingAddress1 = billingInfo.BillingAddress1;
                    BillingAddress2 = billingInfo.BillingAddress2;
                    BillingZip = billingInfo.BillingZip;
                    BillingCity = billingInfo.BillingCity;
                    BillingState = billingInfo.BillingState;
                    BillingCountry = billingInfo.BillingCountry;
                }
            }
            else
            {

            }
        }

        public bool IsFreeUser { get; set; }

        [Display(Name = "Account_SignUp_Promocode", ResourceType = typeof(Wording))]
        public string Promocode { get; set; }

        [Display(Name = "Account_SignUp_Plan", ResourceType = typeof(Wording))]
        public string Plan { get; set; }

        public int AdditionalPets { get; set; }

        [Display(Name = "Account_SignUp_TotalPrice", ResourceType = typeof(Wording))]
        public decimal Price { get; set; }

        [Display(Name = "Account_SignUp_FirstName", ResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Account_SignUp_MiddleName", ResourceType = typeof(Wording))]
        public string MiddleName { get; set; }

        [Display(Name = "Account_SignUp_LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "Account_SignUp_DateofBirth", ResourceType = typeof(Wording))]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Account_SignUp_Gender", ResourceType = typeof(Wording))]
        public GenderEnum? Gender { get; set; }

        [Display(Name = "Account_SignUp_Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Account_SignUp_Address1", ResourceType = typeof(Wording))]
        public string Address1 { get; set; }

        [Display(Name = "Account_SignUp_Address2", ResourceType = typeof(Wording))]
        public string Address2 { get; set; }

        [Display(Name = "Account_SignUp_City", ResourceType = typeof(Wording))]
        public string City { get; set; }

        [Display(Name = "Account_SignUp_Country", ResourceType = typeof(Wording))]
        public CountryEnum? Country { get; set; }

        [Display(Name = "Account_SignUp_State", ResourceType = typeof(Wording))]
        public StateEnum? State { get; set; }

        [Display(Name = "Account_SignUp_Zip", ResourceType = typeof(Wording))]
        public int? Zip { get; set; }

        [Display(Name = "Account_SignUp_PrimaryPhone", ResourceType = typeof(Wording))]
        public string PrimaryPhone { get; set; }

        [Display(Name = "Account_SignUp_SecondaryPhone", ResourceType = typeof(Wording))]
        public string SecondaryPhone { get; set; }

        [Display(Name = "Account_SignUp_TimeZone", ResourceType = typeof(Wording))]
        public TimeZoneEnum? TimeZone { get; set; }

        [Display(Name = "Account_SignUp_Howdidyouhearaboutus", ResourceType = typeof(Wording))]
        public ReferralSourceEnum? ReferralSource { get; set; }

        public string ReferralName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        [Display(Name = "Account_SignUp_CreditCardType", ResourceType = typeof(Wording))]
        public CreditCardTypeEnum? CreditCardType { get; set; }

        [Display(Name = "Account_SignUp_CreditCardNumber", ResourceType = typeof(Wording))]
        public string CreditCardNumber { get; set; }

        [Display(Name = "Account_SignUp_ExpirationDate", ResourceType = typeof(Wording))]
        public string ExpirationDate { get; set; }

        [Display(Name = "Account_SignUp_CVV", ResourceType = typeof(Wording))]
        public int? CVV { get; set; }

        [Display(Name = "Account_SignUp_Address1", ResourceType = typeof(Wording))]
        public string BillingAddress1 { get; set; }

        [Display(Name = "Account_SignUp_Address2", ResourceType = typeof(Wording))]
        public string BillingAddress2 { get; set; }

        [Display(Name = "Account_SignUp_City", ResourceType = typeof(Wording))]
        public string BillingCity { get; set; }

        [Display(Name = "Account_SignUp_Country", ResourceType = typeof(Wording))]
        public CountryEnum? BillingCountry { get; set; }

        [Display(Name = "Account_SignUp_State", ResourceType = typeof(Wording))]
        public StateEnum? BillingState { get; set; }

        [Display(Name = "Account_SignUp_Zip", ResourceType = typeof(Wording))]
        public int? BillingZip { get; set; }

        public User Map(Subscription subscription, PaymentResult paymentResult = null)
        {
            var user = new User
            {
                UserTypeId = UserTypeEnum.OwnerAdmin,
                DomainTypeId = DomainHelper.GetDomain(),
                TimeZoneId = TimeZone,
                FirstName = new EncryptedText(FirstName),
                MiddleName = new EncryptedText(MiddleName),
                LastName = new EncryptedText(LastName),
                BirthDate = (BirthDate == null) ? new EncryptedText((Convert.ToDateTime(BirthDate)).ToString(CultureInfo.InvariantCulture)) : new EncryptedText(BirthDate.Value.ToString(CultureInfo.InvariantCulture)),
                GenderId = Gender,
                Email = new EncryptedText(Email),
                GeneralConditions = false,
                IsUsingDST = false,
                CreationDate = DateTime.Now,
                UserStatusId = UserStatusEnum.Active,
                PrimaryPhone = new EncryptedText(PrimaryPhone),
                SecondaryPhone = new EncryptedText(SecondaryPhone),
                InfoPath = string.Format("{0}\\{1}", DateTime.Today.Year, DateTime.Today.DayOfYear),
                ReferralSource = new EncryptedText(ReferralName),
                Address1 = new EncryptedText(Address1),
                Address2 = new EncryptedText(Address2),
                City = new EncryptedText(City),
                CountryId = Country,
                StateId = State,
                PostalCode = new EncryptedText(Zip.ToString()),
                ReferralSourceId = ReferralSource,
                CenterID = subscription.CenterId
            };

            var randomPart = Membership.GeneratePassword(5, 2);
            var credentials = new Model.Login
            {
                UserName = Encryption.Encrypt(UserName),
                Password = Encryption.EncryptAsymetric(Password + randomPart),
                RandomPart = randomPart
            };
            user.Logins = new List<Model.Login> { credentials };

            var renewalDate = DateTime.Today;
            if (subscription.PaymentTypeId == PaymentTypeEnum.Yearly)
            {
                renewalDate = renewalDate.AddYears(1);
            }
            else if (subscription.PaymentTypeId == PaymentTypeEnum.Monthly)
            {
                renewalDate = renewalDate.AddMonths(1);
            }
            else if (subscription.PaymentTypeId == PaymentTypeEnum.SaleTransaction || subscription.PaymentTypeId == PaymentTypeEnum.ThirdPartyCompany)
            {
                //todo: refactor this
                renewalDate = renewalDate.AddYears(100);
            }
            else if (IsFreeUser)
            {
                renewalDate = renewalDate.AddYears(subscription.Duration);
            }
            else
            {
                renewalDate = renewalDate.AddDays(subscription.Duration);
            }

            var subscriptionService = new SubscriptionService
            {
                AditionalPetCount = AdditionalPets,
                AditionalMRACount = AdditionalPets
            };
            if (paymentResult != null)
            {
                var billingInfo = new BillingInformation
                {
                    PaymentTypeId = PaymentTypeEnum.Yearly,
                    Address1 = BillingAddress1,
                    Address2 = BillingAddress2,
                    City = BillingCity,
                    CountryId = BillingCountry.Value,
                    StateId = BillingState,
                    Zip = BillingZip.ToString(),
                    CreditCardTypeId = CreditCardType
                };
                var paymentHistory = new PaymentHistory
                {
                    Amount = Price,
                    PaymentDate = DateTime.Today,
                    TransactionNumber = paymentResult.OrderId,
                    ErrorMessage = paymentResult.ErrorMessage,
                    BillingInformation = billingInfo
                };
                user.UserSubscription = new UserSubscription
                {
                    Subscription = subscription,
                    ispaymentDone = true,
                    PaymentHistories = new List<PaymentHistory> { paymentHistory },
                    StartDate = DateTime.Today,
                    RenewalDate = renewalDate.AddDays(-1),
                    SubscriptionExpirationAlertId = SubscriptionExpirationAlertEnum.NotSent,
                    SubscriptionMailSent = true,
                    SubscriptionService = subscriptionService
                };
            }
            else
            {
                user.UserSubscription = new UserSubscription
                {
                    Subscription = subscription,
                    ispaymentDone = true,
                    StartDate = DateTime.Today,
                    RenewalDate = renewalDate.AddDays(-1),
                    SubscriptionExpirationAlertId = SubscriptionExpirationAlertEnum.NotSent,
                    SubscriptionMailSent = true,
                    SubscriptionService = subscriptionService
                };
            }

            return user;
        }

        //public User Map(User dbUser, PaymentResult paymentResult)
        //{
        //    var billingInfo = new BillingInformation
        //    {
        //        PaymentTypeId = PaymentTypeEnum.Yearly,
        //        Address1 = BillingAddress1,
        //        Address2 = BillingAddress2,
        //        City = BillingCity,
        //        CountryId = BillingCountry.Value,
        //        StateId = BillingState,
        //        Zip = BillingZip.ToString(),
        //        CreditCardTypeId = CreditCardType
        //    };

        //    var paymentHistory = new PaymentHistory
        //    {
        //        Amount = Price,
        //        PaymentDate = DateTime.Today,
        //        TransactionNumber = paymentResult.OrderId,
        //        ErrorMessage = paymentResult.ErrorMessage,
        //        BillingInformation = billingInfo,
        //        UserSubscriptionId = dbUser.UserSubscriptionId
        //    };
        //    dbUser.UserSubscription.PaymentHistories.Add(paymentHistory);
        //    return dbUser;

        //}
    }
}