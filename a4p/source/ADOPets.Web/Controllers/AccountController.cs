﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.Common.Payment.Model;
using ADOPets.Web.ViewModels.Account;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Common;
using Model;
using System.IO;
using Model.Tools;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ADOPets.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Login(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(ViewModels.Account.Login model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    var cacheKey = string.Format("UserData_{0}", model.UserName);
                    System.Web.HttpContext.Current.Session[Constants.SessionCurrentUserKey] = cacheKey;

                    return RedirectToAction("IndexSelector", "Base");
                }

                ModelState.AddModelError("", Resources.Wording.Account_Login_AuthenticationFailed);
            }

            return View();
        }

        [HttpGet]
        public ActionResult SignUp(int planId = 0, string CodeSelect = "")
        {
            if (!User.Identity.IsAuthenticated)
            {
                List<SelectPlanViewModel> planModel = null;
                Expression<Func<Subscription, object>> parames1 = s => s.PlanType;
                Expression<Func<Subscription, object>> parames2 = s => s.PlanFeatures;
                Expression<Func<Subscription, object>> parames3 = s => s.PaymentType;
                Expression<Func<Subscription, object>>[] paramesArray = new Expression<Func<Subscription, object>>[] { parames1, parames2, parames3 };

                if (planId > 0)
                {
                    if (string.IsNullOrEmpty(CodeSelect))
                    {
                        planModel = UnitOfWork.SubscriptionRepository.GetAll(s => s.IsBase && s.IsTrial != true && !s.IsDeleted && !s.IsPromotionCode, navigationProperties: paramesArray).Select(s => new SelectPlanViewModel(s)).OrderBy(s => s.lstPlanData.PlanType).ToList();
                    }
                    else
                    {
                        planModel = UnitOfWork.SubscriptionRepository.GetAll(s => s.Id == planId, navigationProperties: paramesArray).Select(s => new SelectPlanViewModel(s)).OrderBy(s => s.lstPlanData.PlanType).ToList();
                    }
                    return PartialView("_SelectPlan", planModel);
                }
                else
                {
                    planModel = UnitOfWork.SubscriptionRepository.GetAll(s => s.IsBase && s.IsTrial != true && !s.IsDeleted && !s.IsPromotionCode, navigationProperties: paramesArray).Select(s => new SelectPlanViewModel(s)).OrderBy(s => s.lstPlanData.PlanType).ToList();
                }
                return View("SignUp", planModel);
            }
            else
            {
                return RedirectToAction("IndexSelector", "Base");
            }
        }

        [HttpGet]
        public ActionResult UserInformation(int? planId, string promoCode)
        {
            var model = new BasicInfoViewModel();
            if (string.IsNullOrEmpty(planId.ToString()))
            {
                model = Session["UserInformation"] as BasicInfoViewModel;
                planId = model.PlanId;
                
            }
            var subscription = UnitOfWork.SubscriptionRepository.GetFirst(s => s.Id == planId, navigationProperties: p => p.PlanType);
            model.PlanId = (int)planId;
            model.BasePlanDescription = subscription.Description;
            model.PlanType = (subscription.PlanType != null) ? subscription.PlanType.Name.ToString() : "";
            model.PlanName = subscription.Name;

            if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                model.TimeZone = TimeZoneEnum.UTC0500EasternTimeUSCanada;
                model.Country = CountryEnum.UnitedStates;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
            {
                model.TimeZone = TimeZoneEnum.UTC0530ChennaiKolkataMumbaiNewDelhi;
                model.Country = CountryEnum.India;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                model.TimeZone = TimeZoneEnum.UTC0100BrusselsCopenhagenMadridParis;
                model.Country = CountryEnum.France;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.Portuguese)
            {
                model.TimeZone = TimeZoneEnum.UTC0300Brasilia;
                model.Country = CountryEnum.BRAZIL;
            }
            model.PlanId = (int)planId;
            return PartialView("_UserInformation", model);
        }

        [HttpPost]
        public ActionResult UserInformation(BasicInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Session["UserInformation"] = model;

                var paymentData = Session["PaymentData"] as BillingViewModel;

                if (paymentData != null)
                {
                    paymentData.UpdateUserInfo(model);
                    return PartialView("Billing", paymentData);
                }

                var subscription = UnitOfWork.SubscriptionRepository.GetFirst(s => s.Id == model.PlanId, navigationProperties: p => p.PlanType);
                var planTypeId = subscription.PlanTypeId;
                ViewBag.PlanName = subscription.Name;
                model.Price = subscription.Amount;
                model.PlanId = model.PlanId;
                model.BasePlanDescription = subscription.Description;
                model.PlanType = (subscription.PlanType != null) ? subscription.PlanType.Name.ToString() : "";
                model.PlanName = subscription.Name;
                var userInfo = Session["UserInformation"] as BasicInfoViewModel;
                var model1 = new ConfirmationViewModel(userInfo, paymentData);
                if (model1.IsFreeUser)
                {
                    var timeZoneInfoId = UnitOfWork.TimeZoneRepository.GetTimeZoneInfoId(model.TimeZone.Value);
                    var userDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, timeZoneInfoId);
                    subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == userInfo.PlanId);
                    EmailSender.SendWelcomeMailFreeUser(model.Email, model.FirstName, model.LastName, model.PlanType, model.Promocode);
                    var newUser = model1.Map(subscription);
                    UnitOfWork.UserRepository.Insert(newUser);
                    UnitOfWork.Save();
                    return PartialView("_SubscriptionConfirmation", new PaymentResultViewModel(model1.IsFreeUser));
                }

                if (DomainHelper.GetDomain() == DomainTypeEnum.US)
                {
                    model.TimeZone = TimeZoneEnum.UTC0500EasternTimeUSCanada;
                    model.Country = CountryEnum.UnitedStates;
                }
                else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
                {
                    model.TimeZone = TimeZoneEnum.UTC0530ChennaiKolkataMumbaiNewDelhi;
                    model.Country = CountryEnum.India;
                }
                else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
                {
                    model.TimeZone = TimeZoneEnum.UTC0100BrusselsCopenhagenMadridParis;
                    model.Country = CountryEnum.France;
                }
                else if (DomainHelper.GetDomain() == DomainTypeEnum.Portuguese)
                {
                    model.TimeZone = TimeZoneEnum.UTC0300Brasilia;
                    model.Country = CountryEnum.BRAZIL;
                }

                return PartialView("_MakePayment", new BillingViewModel(model, true));
            }
            else
            {
                return PartialView("_UserInformation", model);
            }
        }

        [HttpPost]
        public ActionResult MakePayment(BillingViewModel billinginfo)
        {
            var userInfo = Session["UserInformation"] as BasicInfoViewModel;
            var model = new ConfirmationViewModel(userInfo, billinginfo);

            PaymentResult paymentResult;
            if (WebConfigHelper.DoFakePayment)
            {
                paymentResult = PaymentHelper.FakePayment(model);
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)  //For the Momonet Fake Payment for IN Domain
            {
                paymentResult = PaymentHelper.FakePayment(model);
            }
            else if (CultureHelper.GetCurrentCulture() == "pt-BR")
            {
                paymentResult = PaymentHelper.PTAuthorizePayment(model);
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                paymentResult = PaymentHelper.USAuthorizePayment(model);
            }
            else
            {
                paymentResult = PaymentHelper.USAuthorizePayment(model);
            }

            //datetime in the user timezone
            var timeZoneInfoId = UnitOfWork.TimeZoneRepository.GetTimeZoneInfoId(model.TimeZone.Value);
            var userDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, timeZoneInfoId);

            if (paymentResult.Success)
            {
                var subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == userInfo.PlanId);
                var amount = model.Price.ToString(CultureInfo.CurrentCulture);
                var newUser = model.Map(subscription, paymentResult);
                UnitOfWork.UserRepository.Insert(newUser);
                UnitOfWork.Save();

                if (subscription.PromotionCode == "HSBC")
                {
                    EmailSender.SendHsbcWelcomeMail(model.Email, model.FirstName, model.LastName, model.Plan, model.Promocode);
                }
                else
                {
                    EmailSender.SendWelcomeMail(model.Email, model.FirstName, model.LastName, model.Plan, model.Promocode);
                }

                EmailSender.SendSubscriptionMail(model.Email, model.FirstName, model.LastName, userDateTime, newUser.UserSubscription.RenewalDate.Value, model.Plan, model.Promocode, paymentResult.OrderId, model.BillingAddress1, model.BillingAddress2,
                        model.BillingCity, EnumHelper.GetResourceValueForEnumValue(model.BillingState), model.BillingZip.HasValue ? model.BillingZip.Value.ToString() : null,
                        EnumHelper.GetResourceValueForEnumValue(model.BillingCountry), model.Email);
                EmailSender.SendSupportUserSubscription(model.FirstName, model.LastName, model.Email, model.Promocode, model.Plan, amount, model.ReferralName);
                Session["UserInformation"] = null;
            }

            return PartialView("_SubscriptionConfirmation", new PaymentResultViewModel(paymentResult, model.Plan, userDateTime));
        }


        [HttpPost]
        public ActionResult BasicInfo(BasicInfoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var subscription = UnitOfWork.SubscriptionRepository.GetFirst(s => s.Id == model.PlanId);
                var price = subscription.Amount + subscription.AmmountPerAddionalPet * model.AdditionalPetCount;

                model.Price = (string.IsNullOrEmpty(price.ToString())) ? model.Price : price;

                Session["UserProfile"] = model;



                if (DomainHelper.GetDomain() == DomainTypeEnum.US)
                {
                    model.Country = CountryEnum.UnitedStates;
                }
                else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
                {
                    model.Country = CountryEnum.India;
                }
                else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
                {
                    model.Country = CountryEnum.France;
                }

                var paymentData = Session["PaymentData"] as BillingViewModel;
                if (paymentData != null)
                {
                    paymentData.UpdateUserInfo(model);

                    return PartialView("Billing", paymentData);
                }

                return PartialView("Billing", new BillingViewModel(model));
            }
            throw new Exception("Client Validation Failed!");
        }

        [HttpPost]
        public ActionResult Billing(BillingViewModel model)
        {

            if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                if (!ModelState.IsValid)
                {
                    if (ModelState.Keys.Contains("CreditCardType") && ModelState["CreditCardType"].Errors.Count > 0)
                    {
                        ModelState.Remove("CreditCardType");
                    }
                    if (ModelState.Keys.Contains("CreditCardNumber") && ModelState["CreditCardNumber"].Errors.Count > 0)
                    {
                        ModelState.Remove("CreditCardNumber");
                    }
                    if (ModelState.Keys.Contains("ExpirationDate") && ModelState["ExpirationDate"].Errors.Count > 0)
                    {
                        ModelState.Remove("ExpirationDate");
                    }

                    if (ModelState.Keys.Contains("CVV") && ModelState["CVV"].Errors.Count > 0)
                    {
                        ModelState.Remove("CVV");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                Session["PaymentData"] = model;
                var userInfo = Session["UserProfile"] as BasicInfoViewModel;

                return PartialView("Confirmation", new ConfirmationViewModel(userInfo, model));
            }


            //if all is well, this will never happen
            throw new Exception("Client Validation Failed!");
        }

        [HttpPost]
        public ActionResult Confirmation()
        {
            var billinginfo = Session["PaymentData"] as BillingViewModel;
            var userInfo = Session["UserProfile"] as BasicInfoViewModel;
            var model = new ConfirmationViewModel(userInfo, billinginfo);
            PaymentResult paymentResult;
            if (WebConfigHelper.DoFakePayment)
            {
                paymentResult = PaymentHelper.FakePayment(model);
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)  //For the Momonet Fake Payment for IN Domain
            {
                paymentResult = PaymentHelper.FakePayment(model);
            }
            else if (CultureHelper.GetCurrentCulture() == "pt-BR")
            {
                paymentResult = PaymentHelper.PTAuthorizePayment(model);
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                paymentResult = PaymentHelper.USAuthorizePayment(model);
            }

            else
            {
                paymentResult = PaymentHelper.USAuthorizePayment(model);
            }

            //datetime in the user timezone
            var timeZoneInfoId = UnitOfWork.TimeZoneRepository.GetTimeZoneInfoId(model.TimeZone.Value);
            var userDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, timeZoneInfoId);

            if (paymentResult.Success)
            {
                var subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == userInfo.PlanId);
                var amount = model.Price.ToString(CultureInfo.CurrentCulture);
                int addPetCount = model.AdditionalPets;
                model.AdditionalPets = addPetCount;
                var newUser = model.Map(subscription, paymentResult);
                UnitOfWork.UserRepository.Insert(newUser);
                UnitOfWork.Save();

               
                if (subscription.PromotionCode == "HSBC")
                {
                    EmailSender.SendHsbcWelcomeMail(model.Email, model.FirstName, model.LastName, model.Plan, model.Promocode);
                }
                else
                {
                    EmailSender.SendWelcomeMail(model.Email, model.FirstName, model.LastName, model.Plan, model.Promocode);
                }

                EmailSender.SendSubscriptionMail(model.Email, model.FirstName, model.LastName, userDateTime, newUser.UserSubscription.RenewalDate.Value, model.Plan, model.Promocode, paymentResult.OrderId, model.BillingAddress1, model.BillingAddress2,
                        model.BillingCity, EnumHelper.GetResourceValueForEnumValue(model.BillingState), model.BillingZip.HasValue ? model.BillingZip.Value.ToString() : null,
                        EnumHelper.GetResourceValueForEnumValue(model.BillingCountry), model.Email);
                EmailSender.SendSupportUserSubscription(model.FirstName, model.LastName, model.Email, model.Promocode, model.Plan, amount, model.ReferralName);

                Session["PaymentData"] = null;
                Session["UserProfile"] = null;
            }
           

            return PartialView("_PaymentResult", new PaymentResultViewModel(paymentResult, model.Plan, userDateTime));
            //}
        }

        [HttpGet]
        public ActionResult CmcicAccountConfirmation()
        {
            var billinginfo = Session["PaymentData"] as BillingViewModel;
            var userInfo = Session["UserProfile"] as BasicInfoViewModel;

            PaymentResult paymentResult = new PaymentResult
            {
                OrderId = "",
                Success = false,
                TransactionID = "",
                ErrorMessage = "Transaction Failed"
            };

            if (billinginfo != null)
            {
                var model = new ConfirmationViewModel(userInfo, billinginfo);
                //datetime in the user timezone
                var timeZoneInfoId = UnitOfWork.TimeZoneRepository.GetTimeZoneInfoId(model.TimeZone.Value);
                var userDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, timeZoneInfoId);

                paymentResult = new PaymentResult
                {
                    OrderId = "",
                    Success = true,
                    TransactionID = ""
                };

                var subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == userInfo.PlanId);
                var amount = model.Price.ToString(CultureInfo.CurrentCulture);
                var newUser = model.Map(subscription, paymentResult);
                UnitOfWork.UserRepository.Insert(newUser);
                UnitOfWork.Save();

                if (subscription.PromotionCode == "HSBC")
                {
                    EmailSender.SendHsbcWelcomeMail(model.Email, model.FirstName, model.LastName, model.Plan, model.Promocode);

                }
                else
                {
                    EmailSender.SendWelcomeMail(model.Email, model.FirstName, model.LastName, model.Plan, model.Promocode);
                }

                EmailSender.SendSubscriptionMail(model.Email, model.FirstName, model.LastName, userDateTime, newUser.UserSubscription.RenewalDate.Value, model.Plan, model.Promocode, paymentResult.OrderId, model.BillingAddress1, model.BillingAddress2,
                        model.BillingCity, EnumHelper.GetResourceValueForEnumValue(model.BillingState), model.BillingZip.HasValue ? model.BillingZip.Value.ToString() : null,
                        EnumHelper.GetResourceValueForEnumValue(model.BillingCountry), model.Email);
                EmailSender.SendSupportUserSubscription(model.FirstName, model.LastName, model.Email, model.Promocode, model.Plan, amount, model.ReferralName);

                Session["PaymentData"] = null;
                Session["UserProfile"] = null;

                return View("CmcicAccountConfirmation", new PaymentResultViewModel(paymentResult, model.Plan, userDateTime));
            }

            return View("CmcicAccountConfirmation", new PaymentResultViewModel(paymentResult, "", DateTime.Now));
        }

        [HttpGet]
        public ActionResult CmcicAccountError()
        {
            var billinginfo = Session["PaymentData"] as BillingViewModel;
            var userInfo = Session["UserProfile"] as BasicInfoViewModel;
            PaymentResult paymentResult = new PaymentResult
            {
                OrderId = "",
                Success = false,
                TransactionID = "",
                ErrorMessage = "Transaction Failed"
            };
            if (billinginfo != null || userInfo != null)
            {
                var model = new ConfirmationViewModel(userInfo, billinginfo);

                //datetime in the user timezone
                var timeZoneInfoId = UnitOfWork.TimeZoneRepository.GetTimeZoneInfoId(model.TimeZone.Value);
                var userDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, timeZoneInfoId);

                Session["PaymentData"] = null;
                Session["UserProfile"] = null;

                return View("CmcicAccountConfirmation", new PaymentResultViewModel(paymentResult, model.Plan, userDateTime));
            }
            return View("CmcicAccountConfirmation", new PaymentResultViewModel(paymentResult, "", DateTime.Now));
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = UnitOfWork.UserRepository.GetUserIdByEmail(model.Email);
                if (userId.HasValue)
                {
                    var login = UnitOfWork.LoginRepository.GetSingle(l => l.UserId == userId.Value, l => l.User);
                    var user = login.User;
                    var newPassword = ChangePassword(login);

                    EmailSender.SendForgotPasswordMail(user.Email, user.FirstName, user.LastName, Encryption.Decrypt(login.UserName), newPassword);
                    EmailSender.SendToSupportLoginRecoverySuccess(model.Email, userId.Value);
                    return RedirectToAction("PasswordInformation", "Account");
                }
                else
                {
                    ModelState.AddModelError("", Resources.Wording.Account_ForgotPassword_AuthenticationFailed);
                    EmailSender.SendToSupportLoginRecoveryFailed(model.Email);
                }
            }

            return View("ForgotPassword");
        }

        [HttpGet]
        public ActionResult PasswordInformation()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStates(string country)
        {
            var countryEnum = Enum.Parse(typeof(CountryEnum), country);
            var states = UnitOfWork.StateRepository.GetAll(s => s.CountryId == (CountryEnum)countryEnum).OrderBy(s => s.Name);
            var items = states.Select(i => new SelectListItem { Text = EnumHelper.GetResourceValueForEnumValue(i.Id), Value = i.Id.ToString() });

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPlanDetails(int id)
        {
            var plan = UnitOfWork.SubscriptionRepository.GetSingle(s => s.Id == id);

            if (plan != null)
            {
                var data = new { PlanId = plan.Id, Price = plan.Amount, PlanName = plan.Name };

                return Json(data, JsonRequestBehavior.AllowGet);
            }

            throw new Exception("Invalid Plan Id");
        }

        [HttpGet]
        public ActionResult GetPlanDetailsByPetCount(int petToAdd, int planId, string promocode)
        {
            Model.Subscription plan;
            plan = UnitOfWork.SubscriptionRepository.GetFirst(p => p.Id == planId);
            
            if (plan != null)
            {
                decimal? price = null;
                int totalPets = 0;
                totalPets = plan.MaxPetCount + petToAdd;
                price = plan.Amount + plan.AmmountPerAddionalPet * petToAdd;
                var data = new { PlanId = plan.Id, Price = plan.Amount, PlanName = string.Format(Resources.Wording.Account_SignUp_AdditionalPetsInformation, price, totalPets, totalPets) };

                return Json(data, JsonRequestBehavior.AllowGet);
            }

            throw new Exception("Invalid Plan Id");
        }

        [HttpGet]
        public ActionResult GetAdditionalPetsByPromocode(string promocode)
        {
            var plans = UnitOfWork.SubscriptionRepository.GetAll(u => u.IsVisibleToOwner && u.PromotionCode == promocode && !u.IsDeleted).OrderBy(u => u.MaxPetCount);

            if (!plans.Any())
            {
                return Json(plans, JsonRequestBehavior.AllowGet);
            }

            var items = plans.Select(i => new SelectListItem { Text = (i.MaxPetCount - 1).ToString(), Value = i.Id.ToString() });

            var plan = plans.First(p => p.IsPromotionCode);


            var planName = string.Format(Resources.Wording.Account_SignUp_PetsInformation, plan.Amount, plan.MaxPetCount, plan.MaxPetCount);
            var additionalInfo = string.Format(Resources.Wording.Account_Signup_AdditionalPetInfo, plan.AmmountPerAddionalPet);

            var planDetails = new { PlanId = plan.Id, Price = plan.Amount, PlanName = planName, PlanDescription = additionalInfo, MaxPetCount = plan.MaxPetCount };

            var data = new { Items = items, BasePlanDetails = planDetails };

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetPlansByPromocode(string promocode)
        {
            List<SelectPlanViewModel> planModel;
            Expression<Func<Subscription, object>> parames1 = s => s.PlanType;
            Expression<Func<Subscription, object>> parames2 = s => s.PlanFeatures;
            Expression<Func<Subscription, object>> parames3 = s => s.PaymentType;
            Expression<Func<Subscription, object>>[] paramesArray = new Expression<Func<Subscription, object>>[] { parames1, parames2, parames3 };
            planModel = UnitOfWork.SubscriptionRepository.GetAll(u => u.IsVisibleToOwner && u.PromotionCode == promocode && !u.IsDeleted, navigationProperties: paramesArray).Select(s => new SelectPlanViewModel(s)).OrderBy(s => s.lstPlanData.PlanType).ToList();

            return PartialView("SelectPlanList", planModel);
        }

        public ActionResult SetCulture(string culture)
        {
            culture = CultureHelper.GetImplementedCulture(culture);

            Session[Constants.CurrentCultureName] = culture;

            return RedirectToAction("Login");
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            var key = System.Web.HttpContext.Current.Session[Constants.SessionCurrentUserKey];
            if (key != null && HttpRuntime.Cache[key.ToString()] != null)
            {
                HttpRuntime.Cache.Remove(key.ToString());
            }

            return RedirectToAction("Login", "Account", null);
        }

        [HttpPost]
        public JsonResult ValidateUserName(string userName)
        {
            var user = Membership.GetUser(userName);
            return Json(user == null);
        }

        [HttpPost]
        public JsonResult ValidateEmail(string Email)
        {
            var valid = UnitOfWork.UserRepository.IfMailAlreadyExist(Email, 0);
            return Json(valid);
        }

        private string ChangePassword(Model.Login login)
        {
            var randomPart = Membership.GeneratePassword(5, 2);
            var newPassword = GenerateRandomPassword();
            login.RandomPart = randomPart;
            login.Password = Encryption.EncryptAsymetric(newPassword + randomPart);
            login.IsTemporalPassword = true;

            UnitOfWork.LoginRepository.Update(login);
            UnitOfWork.Save();

            return newPassword;
        }

        public string GenerateRandomPassword()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random(Environment.TickCount);

            var result = new char[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = chars[random.Next(0, chars.Length)];
            }

            return new string((result));
        }

        #region Share media
        [AllowAnonymous]
        public ActionResult GetImage(int galleryId)
        {
            var gal = UnitOfWork.GalleryRepository.GetSingle(x => x.Id == galleryId);
            PetsGlobalViewModel obj = null;
            obj = new PetsGlobalViewModel();
            obj.GalleryId = galleryId;
            obj.GalleryType = 1;
            obj.Title = gal.Title;
            obj.VideoUrl = "";

            return View("PetsGlobal", obj);
        }

        [AllowAnonymous]
        public ActionResult GetVideo(int galleryId)
        {
            var gal = UnitOfWork.VideoGalleryRepository.GetSingle(x => x.Id == galleryId);
            PetsGlobalViewModel obj = null;
            obj = new PetsGlobalViewModel();
            obj.GalleryId = galleryId;
            obj.GalleryType = 3;
            obj.Title = gal.Title;
            string path = System.Configuration.ConfigurationManager.AppSettings["WebsitePath"];
            obj.VideoUrl = path + gal.VideoURL.Trim().Replace("\\", "/");

            return View("PetsGlobal", obj);
        }

        [AllowAnonymous]
        public ActionResult GetImageFile(int galleryId)
        {
            var gal = UnitOfWork.GalleryRepository.GetSingle(x => x.Id == galleryId);
            string ownerId = "";
            string ownerInfoPath = "";
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == gal.PetId, p => p.Users);
            if (pet != null)
            {
                var petOwner = pet.Users.FirstOrDefault();
                if (petOwner != null)
                {
                    ownerId = Convert.ToString(petOwner.Id);
                    ownerInfoPath = petOwner.InfoPath;
                }
            }

            var directoryPath = Path.Combine(Path.Combine(WebConfigHelper.UserFilesPath, ownerInfoPath, ownerId), Constants.GalleryPhotoFolderName);
            var splitExt = gal.ImageURL.Split('.');
            var ext = splitExt[1].ToUpper();
            string fileNameNew = splitExt[0] + "." + ext;
            var path = Path.Combine(directoryPath, fileNameNew);
            var exist = System.IO.File.Exists(path);
            if (exist)
            {
                return new FileStreamResult(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read), "image/*");
            }
            return null;
        }

        #endregion

        public FileResult UserGuide()
        {
            return File(WebConfigHelper.UserGuidePDFPath, "application/pdf", Constants.UserGuidePDFFile);
        }


        [HttpGet]
        public ActionResult GetPlanDetailsByPlanId(int planId)
        {
            Model.Subscription plan;

            plan = UnitOfWork.SubscriptionRepository.GetFirst(p => p.Id == planId);

            if (plan != null)
            {
                int success = 0; int totalPets = 0;
                decimal? price = null;
                price = plan.Amount + plan.AmmountPerAddionalPet;
                totalPets = plan.MaxPetCount;
                var data = new { success = success, PlanId = plan.Id, Price = plan.Amount, PlanName = string.Format(Resources.Wording.Account_SignUp_AdditionalPetsInformation, price, totalPets, totalPets), MaxPetCount = plan.MaxPetCount };

                return Json(data, JsonRequestBehavior.AllowGet);
            }

            throw new Exception("Invalid Plan Id");
        }

        public ActionResult Index()
        {
            return PartialView("PopUpBlockedAlert");
        }

    }
}
