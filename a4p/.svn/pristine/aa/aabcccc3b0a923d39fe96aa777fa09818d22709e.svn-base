using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using Model;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.ViewModels.Profile;
using ADOPets.Web.Common.Payment.Model;
using System.Globalization;
using System.Web.Mvc;
using System.Collections.Generic;
using ADOPets.Web.Common;
using System.Linq.Expressions;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        #region Only for admin user

        [HttpGet]
        [UserRoleAuthorize(UserTypeEnum.Admin)]
        public ActionResult Index()
        {
            var userId = HttpContext.User.GetUserId();
            var model = UnitOfWork.UserRepository.GetAll(u => u.Id != userId).Select(u => new IndexViewModel(u));
            return View(model);
        }

        [HttpGet]
        [UserRoleAuthorize(UserTypeEnum.Admin)]
        public ActionResult Add()
        {
            var defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(u => !u.IsPromotionCode && !u.IsDeleted).OrderBy(u => u.MaxPetCount);

            ViewBag.Subscriptions = defaultPlans.Select(i => new SelectListItem { Text = (i.MaxPetCount).ToString(), Value = i.Id.ToString() });

            return PartialView("_Add");
        }

        [HttpPost]
        [UserRoleAuthorize(UserTypeEnum.Admin)]
        public ActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == model.Subscription);
                var password = Membership.GeneratePassword(8, 0);

                var user = model.Map(subscription, password);

                UnitOfWork.UserRepository.Insert(user);
                UnitOfWork.Save();

                EmailSender.SendAccountActivationMail(model.Email, model.FirstName, model.LastName, model.UserName, password);

                var planName = string.Format(Resources.Wording.Account_SignUp_AdditionalPetsInformation, subscription.Amount, subscription.MaxPetCount, subscription.MaxPetCount);

                EmailSender.SendWelcomeMail(model.Email, model.FirstName, model.LastName, planName, null);

                //MailHelper.SendToSupportSubscriptionMail(model.FirstName, model.LastName, model.Email, model.Promocode, model.Plan, amount, model.ReferralSource);

                Success(Resources.Wording.Profile_Add_AddSuccessMessage);
            }
            return RedirectToAction("Index");
        }

        #endregion


        [HttpGet]
        public ActionResult PlanRenewal()
        {
            var userId = HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);

            int curPlanId = user.UserSubscription.SubscriptionId;

            List<NewPlanRenewViewModel> planModel = null;
            Expression<Func<Subscription, object>> parames1 = s => s.PlanType;
            Expression<Func<Subscription, object>> parames2 = s => s.PlanFeatures;
            Expression<Func<Subscription, object>> parames3 = s => s.PaymentType;
            Expression<Func<Subscription, object>>[] paramesArray = new Expression<Func<Subscription, object>>[] { parames1, parames2, parames3 };
            if (user.UserSubscription.Subscription != null && user.UserSubscription.Subscription.IsPromotionCode)
            {
                planModel = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsBase && s.Id == user.UserSubscription.Subscription.Id, navigationProperties: paramesArray).Select(s => new NewPlanRenewViewModel(s, curPlanId)).OrderBy(s => s.lstPlanData.PlanType).ToList();
            }
            else
            {
                planModel = UnitOfWork.SubscriptionRepository.GetAll(s => s.IsBase && s.IsTrial != true && !s.IsDeleted && !s.IsPromotionCode && s.Amount != null, navigationProperties: paramesArray).Select(s => new NewPlanRenewViewModel(s, curPlanId)).OrderBy(s => s.lstPlanData.PlanType).ToList();
            }
            return View(planModel);
        }

        [HttpGet]
        public ActionResult OwnerDetail()
        {
            var userId = HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);
            IEnumerable<Subscription> defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted).OrderBy(u => u.MaxPetCount);
            var subscription = user.UserSubscription.Subscription;
            var subscriptionRenewal = subscription;
            var subscriptionbaseID = subscription.SubscriptionBaseId;
            if (subscriptionbaseID != null)
            {
                subscriptionRenewal = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == subscriptionbaseID);
            }

            List<ADOPets.Web.ViewModels.Profile.SelectPlanViewModel> planModel;
            Expression<Func<Subscription, object>> parames1 = s => s.PlanType;
            Expression<Func<Subscription, object>> parames2 = s => s.PlanFeatures;
            Expression<Func<Subscription, object>> parames3 = s => s.PaymentType;
            Expression<Func<Subscription, object>>[] paramesArray = new Expression<Func<Subscription, object>>[] { parames1, parames2, parames3 };
            planModel = UnitOfWork.SubscriptionRepository.GetAll(u => u.IsVisibleToOwner && u.PromotionCode == subscription.PromotionCode && !u.IsDeleted, navigationProperties: paramesArray).Select(s => new SelectPlanViewModel(s)).OrderBy(s => s.lstPlanData.PlanType).ToList();

            var listItems = Enumerable.Range(1, 10).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() }).ToList();
            listItems.Add(new SelectListItem { Value = "0", Text = Resources.Wording.Profile_PlanEdit_addpets });
            ViewBag.AdditionalPets = listItems;

            if (subscription.IsPromotionCode)
            {
                var promocode = user.UserSubscription.Subscription.PromotionCode;
                ViewBag.Plan = defaultPlans.Where(i => i.PromotionCode == promocode && i.IsTrial == false).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
                if (subscriptionbaseID != null)
                {
                    ViewBag.Plan = defaultPlans.Where(i => i.Id == subscriptionbaseID).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
                }
                else
                {
                    ViewBag.Plans = defaultPlans.Where(i => i.Id == subscription.Id).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
                }
            }
            else
            {
                ViewBag.Plan = defaultPlans.Where(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
            }
            if (DomainHelper.GetDomain() == DomainTypeEnum.India || DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                int defaultId = 0;
                if (string.IsNullOrEmpty(subscription.PaymentTypeId.ToString()) && subscription.IsBase)//==> "Free Account With Limited Access")
                {
                    defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted && s.IsBase && s.Id != subscription.Id).OrderBy(u => u.MaxPetCount);
                    IEnumerable<SelectListItem> lstPlans = defaultPlans.Where(i => !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
                    ViewBag.Plan = lstPlans;
                    defaultId = Convert.ToInt32(lstPlans.FirstOrDefault().Value);

                    subscriptionRenewal = defaultPlans.Where(p => p.Id == defaultId).FirstOrDefault();
                }
            }

            return View(new OwnerDetailViewModel(user, subscription, subscriptionRenewal, planModel));
        }

        [HttpGet]
        public ActionResult OwnerEdit()
        {
            var userId = HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
            return PartialView("_OwnerEdit", new OwnerEditViewModel(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OwnerEdit(HttpPostedFileBase imageFile, OwnerEditViewModel model)
        {
            ///
            /// NOTE : IE Browser throws exception on these fields, 
            /// So removed the erros from the modelstate
            /// 

            if (!ModelState.IsValid)
            {
                if (ModelState.Keys.Contains("State") && ModelState["State"].Errors.Count > 0)
                {
                    ModelState.Remove("State");
                }
            }

            if (!ModelState.IsValid)
            {
                return PartialView("_OwnerEdit", model);

            }

            byte[] data = null;
            if (imageFile != null)
            {
                using (var reader = new System.IO.BinaryReader(imageFile.InputStream))
                {
                    data = reader.ReadBytes(imageFile.ContentLength);
                }
            }

            var userId = HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);

            model.Map(user, data);
            UnitOfWork.UserRepository.Update(user);
            UnitOfWork.Save();

            var timeZoneInfoId = string.Empty;
            if (user.TimeZoneId != null)
            {
                timeZoneInfoId = UnitOfWork.TimeZoneRepository.GetSingle(t => t.Id == user.TimeZoneId).TimeZoneInfoId;
            }

            HttpContext.User.UpdateUserSettings(user, timeZoneInfoId);


            Success(Resources.Wording.Profile_OwnerEdit_SuccessMsg);
            if (Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()))
                return RedirectToAction("MyAccount");
            return RedirectToAction("OwnerDetail");
        }

        [HttpPost]
        public ActionResult PlanAddPets(PlanAddPetsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userSubscriptionId = User.GetUserSubscriptionId();
                var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingle(us => us.Id == userSubscriptionId, us => us.Subscription);
                var subscription = userSubscription.Subscription;
                var petToAdd = model.AdditionalPets - model.NumberOfPets;
                var priceToPay = (petToAdd * subscription.AmmountPerAddionalPet).Value;


                var totalPrice = subscription.Amount + (subscription.AmmountPerAddionalPet * model.AdditionalPets);
                var totalMra = model.AdditionalPets + subscription.MRACount;
                var totalPets = model.AdditionalPets + subscription.MaxPetCount;
                var totalSmo = subscription.SMOCount;


                var planName = string.Format(Resources.Wording.Account_SignUp_AdditionalPetsInformation, subscription.Amount, subscription.MaxPetCount, subscription.MaxPetCount);
                var additionalInfo = string.Format(Resources.Wording.Account_Signup_AdditionalPetInfo, subscription.AmmountPerAddionalPet);

                model.BasicPan = planName;
                model.Price = totalPrice.ToString();
                model.PlanName = GetPlanName(subscription, totalPrice ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);
                model.Promocode = subscription.PromotionCode;
                model.Description = subscription.Description;
                model.MaxPetCount = subscription.MaxPetCount;
                model.AdditionalInfo = additionalInfo;
                model.AdditionalPets = petToAdd;
                model.PriceToPay = priceToPay;
                model.StartDate = userSubscription.StartDate.Value;
                model.EndDate = userSubscription.RenewalDate.Value;
                model.Duration = subscription.Duration == 0
                ? (int)(userSubscription.RenewalDate.Value.Subtract(userSubscription.StartDate.Value).TotalDays) + 1
                : subscription.Duration;
                model.RemainingDays = (int)(userSubscription.RenewalDate - DateTime.Today).Value.TotalDays < 0 ? 0 : (int)(userSubscription.RenewalDate - DateTime.Today).Value.TotalDays + 1;

                Session["PlanAddPets"] = model;
                Session["RenewalInfo"] = null;
                Session["PlanUpgradeInfo"] = null;
                return PartialView("PlanBilling", new PlanBillingViewModel(model.PlanName, model.PriceToPay));
            }

            //if all is well, this will never happen
            throw new Exception("Client Validation Failed!");
        }

        [HttpGet]
        public ActionResult GetPlansByPromocodeDesc(string promocode)
        {
            List<SelectPlanViewModel> planModel;
            Expression<Func<Subscription, object>> parames1 = s => s.PlanType;
            Expression<Func<Subscription, object>> parames2 = s => s.PlanFeatures;
            Expression<Func<Subscription, object>> parames3 = s => s.PaymentType;
            Expression<Func<Subscription, object>>[] paramesArray = new Expression<Func<Subscription, object>>[] { parames1, parames2, parames3 };
            planModel = UnitOfWork.SubscriptionRepository.GetAll(u => u.IsVisibleToOwner && u.PromotionCode == promocode && !u.IsDeleted, navigationProperties: paramesArray).Select(s => new SelectPlanViewModel(s)).OrderBy(s => s.lstPlanData.PlanType).ToList();

            return PartialView("SelectPlanList", planModel);
        }

        [HttpGet]
        public ActionResult SelectPlanList()
        {
            string promocode = null;
            List<SelectPlanViewModel> planModel;
            Expression<Func<Subscription, object>> parames1 = s => s.PlanType;
            Expression<Func<Subscription, object>> parames2 = s => s.PlanFeatures;
            Expression<Func<Subscription, object>> parames3 = s => s.PaymentType;
            Expression<Func<Subscription, object>>[] paramesArray = new Expression<Func<Subscription, object>>[] { parames1, parames2, parames3 };
            planModel = UnitOfWork.SubscriptionRepository.GetAll(u => u.IsVisibleToOwner && u.PromotionCode == promocode && !u.IsDeleted, navigationProperties: paramesArray).Select(s => new SelectPlanViewModel(s)).OrderBy(s => s.lstPlanData.PlanType).ToList();

            return PartialView("SelectPlanList", planModel);
        }

        [HttpGet]
        public ActionResult PlanUpgrade()
        {
            List<SelectPlanViewModel> planModel = null;
            var userId = HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);
            IEnumerable<Subscription> defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted).OrderBy(u => u.MaxPetCount);
            var subscription = user.UserSubscription.Subscription;
            var subscriptionRenewal = subscription;
            var subscriptionbaseID = subscription.SubscriptionBaseId;
            if (subscriptionbaseID != null)
            {
                subscriptionRenewal = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == subscriptionbaseID);
            }

            var listItems = Enumerable.Range(1, 10).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() }).ToList();
            listItems.Add(new SelectListItem { Value = "0", Text = Resources.Wording.Profile_PlanEdit_addpets });
            ViewBag.AdditionalPets = listItems;

            if (subscription.IsPromotionCode)
            {
                var promocode = user.UserSubscription.Subscription.PromotionCode;
                ViewBag.Plan = defaultPlans.Where(i => i.PromotionCode == promocode && i.IsTrial == false).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
                if (subscriptionbaseID != null)
                {
                    ViewBag.Plan = defaultPlans.Where(i => i.Id == subscriptionbaseID).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
                }
                else
                {
                    ViewBag.Plans = defaultPlans.Where(i => i.Id == subscription.Id).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
                }
            }
            else
            {
                ViewBag.Plan = defaultPlans.Where(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
            }
            if (DomainHelper.GetDomain() == DomainTypeEnum.India || DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                int defaultId = 0;
                if (string.IsNullOrEmpty(subscription.PaymentTypeId.ToString()) && subscription.IsBase)//==> "Free Account With Limited Access")
                {
                    defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted && s.IsBase && s.Id != subscription.Id).OrderBy(u => u.MaxPetCount);
                    IEnumerable<SelectListItem> lstPlans = defaultPlans.Where(i => !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
                    ViewBag.Plan = lstPlans;
                    defaultId = Convert.ToInt32(lstPlans.FirstOrDefault().Value);

                    subscriptionRenewal = defaultPlans.Where(p => p.Id == defaultId).FirstOrDefault();

                    Expression<Func<Subscription, object>> parames1 = s => s.PlanType;
                    Expression<Func<Subscription, object>> parames2 = s => s.PlanFeatures;
                    Expression<Func<Subscription, object>> parames3 = s => s.PaymentType;
                    Expression<Func<Subscription, object>>[] paramesArray = new Expression<Func<Subscription, object>>[] { parames1, parames2, parames3 };

                    if (string.IsNullOrEmpty(subscription.PaymentTypeId.ToString()) && subscription.IsBase)//==> "Free Account With Limited Access")
                    {
                        planModel = UnitOfWork.SubscriptionRepository.GetAll(u => u.IsVisibleToOwner && u.IsBase == true && u.PlanTypeId > subscription.PlanTypeId && !u.IsDeleted, navigationProperties: paramesArray).Select(s => new SelectPlanViewModel(s)).OrderBy(s => s.lstPlanData.PlanType).ToList();
                    }
                    else
                    {
                        planModel = UnitOfWork.SubscriptionRepository.GetAll(u => u.IsVisibleToOwner && u.PromotionCode == subscription.PromotionCode && !u.IsDeleted, navigationProperties: paramesArray).Select(s => new SelectPlanViewModel(s)).OrderBy(s => s.lstPlanData.PlanType).ToList();
                    }
                }
            }
            PlanUpgradeViewModel upgradeModel = new OwnerDetailViewModel(user, subscription, subscriptionRenewal, planModel).UpgradePlan;

            return View("PlanUpgrade1", upgradeModel);
        }

        [HttpPost]
        public ActionResult PlanUpgrade(int planId)
        {
            //TODO : Signupchanges -commented on 26th May 2015 (Nutan)
            //if (basePlan != null)
            //{
            //    if (DomainHelper.GetDomain() == DomainTypeEnum.US || DomainHelper.GetDomain() == DomainTypeEnum.India)
            //    {
            //        if (!string.IsNullOrEmpty(basePlan.AditionalInfo) && basePlan.IsBase) // $50/year for 5 pets
            //        {
            //            ModelState.Remove("AdditionalPets");
            //        }
            //        else if (basePlan.IsBase) // $70/year for 10 pets
            //        {
            //            ModelState.Remove("AdditionalPets");
            //        }
            //    }                
            //}

            if (ModelState.IsValid)
            {

                var basePlan = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == planId);
                var userId = HttpContext.User.GetUserId();
                var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);
                var timezoneID = user.TimeZone != null ? user.TimeZone.TimeZoneInfoId : TimeZoneInfo.Local.Id;

                //planId = Convert.ToInt16(model.PlanName);
                PlanUpgradeViewModel model = new PlanUpgradeViewModel();

                model.PlanID = planId;
                basePlan = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == planId);
                if (basePlan != null)
                {
                    var totalPrice = basePlan.Amount + (basePlan.AmmountPerAddionalPet * model.AdditionalPets);
                    var totalMra = model.AdditionalPets + basePlan.MRACount;
                    var totalPets = model.AdditionalPets + basePlan.MaxPetCount;
                    var totalSmo = basePlan.SMOCount;


                    var updateDate = DateTime.Today;
                    var startDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(updateDate, timezoneID);
                    updateDate = startDate.AddDays(-1);


                    //var planName = string.Format(Resources.Wording.Account_SignUp_AdditionalPetsInformation, basePlan.Amount, basePlan.MaxPetCount, basePlan.MaxPetCount);
                    //var additionalInfo = string.Format(Resources.Wording.Account_Signup_AdditionalPetInfo, basePlan.AmmountPerAddionalPet);

                    model.BasicPlan = basePlan.Name;
                    model.Price = totalPrice;
                    model.Promocode = basePlan.PromotionCode;
                    model.PlanName = GetPlanName(basePlan, totalPrice ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);
                    model.MaxPetCount = basePlan.MaxPetCount;
                    model.AdditionalInfo = basePlan.AditionalInfo;
                    model.Duration = basePlan.Duration == 0 ? (int)(model.EndDate.Subtract(model.StartDate).TotalDays) + 1 : basePlan.Duration;
                    model.RemainingDays = (int)(model.EndDate.Subtract(model.StartDate).TotalDays) + 1;

                    if (basePlan.PaymentTypeId == PaymentTypeEnum.Yearly)
                    {
                        updateDate = updateDate.AddYears(1);
                    }
                    else if (basePlan.PaymentTypeId == PaymentTypeEnum.Monthly)
                    {
                        updateDate = updateDate.AddMonths(1);
                    }
                    else if (basePlan.PaymentTypeId == PaymentTypeEnum.SaleTransaction || basePlan.PaymentTypeId == PaymentTypeEnum.ThirdPartyCompany)
                    {
                        //todo: refactor this
                        updateDate = updateDate.AddYears(100);
                    }
                    else if (basePlan.PlanTypeId == PlanTypeEnum.BasicFree && basePlan.IsBase)
                    {
                        updateDate = updateDate.AddYears(basePlan.Duration);
                    }
                    else
                    {
                        updateDate = updateDate.AddDays(basePlan.Duration);
                    }
                    //renewalDate = renewalDate.AddDays(-1);
                    updateDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(updateDate, timezoneID);

                    PlanBillingViewModel model1 = new PlanBillingViewModel(basePlan);
                    model1.StartDate = startDate;
                    model1.EndDate = updateDate;
                    model1.PlanType = (basePlan.PlanTypeId == null) ? string.Empty : (basePlan.PlanTypeId == PlanTypeEnum.Premium) ? PlanTypeEnum.Premium.ToString() : PlanTypeEnum.PremiumPlus.ToString();

                    Session["PlanUpgradeInfo"] = model;
                    Session["RenewalInfo"] = null;
                    Session["PlanAddPets"] = null;

                    ViewBag.IsUpgrade = true;

                    if (DomainHelper.GetDomain() == DomainTypeEnum.US)
                    {
                        model1.BillingCountry = CountryEnum.UnitedStates;
                    }
                    else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
                    {
                        model1.BillingCountry = CountryEnum.India;
                    }
                    else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
                    {
                        model1.BillingCountry = CountryEnum.France;
                    }
                    else if (DomainHelper.GetDomain() == DomainTypeEnum.Portuguese)
                    {
                        model1.BillingCountry = CountryEnum.BRAZIL;
                    }

                    return PartialView("PlanBilling", model1);
                }

            }

            //if all is well, this will never happen
            throw new Exception("Client Validation Failed!");
        }
        [HttpPost]
        public ActionResult PlanBilling(PlanBillingViewModel model)
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

            //Session["PaymentData"] = model;
            var upgradePlan = Session["PlanUpgradeInfo"] as PlanUpgradeViewModel;
            var email = HttpContext.User.GetUserEmail();

            var confirmationModel = new PlanConfirmationViewModel(upgradePlan, model, email);
            PlanUpgradeViewModel upgradeModel = new PlanUpgradeViewModel();


            //var billinginfo = Session["PaymentData"] as PlanBillingViewModel;
            var basePlan = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == upgradePlan.PlanID, navigationProperties: p => p.PlanType);


            var duration = basePlan.Duration == 0 ? (int)(model.EndDate.Subtract(model.StartDate).TotalDays) + 1 : basePlan.Duration;
            var remainingDays = (int)(model.EndDate.Subtract(model.StartDate).TotalDays) + 1;

            model.PlanId = upgradePlan.PlanID;
            model.PlanType = upgradePlan.Myplan;
            model.Price = upgradePlan.Price.ToString();
            model.Plan = upgradePlan.BasicPlan;

            upgradeModel.PlanID = basePlan.Id;
            upgradeModel.BasicPlan = basePlan.Name;
            upgradeModel.Promocode = basePlan.PromotionCode;
            upgradeModel.PlanName = model.Plan;
            upgradeModel.Price = basePlan.Amount ?? 0;
            upgradeModel.Description = basePlan.Description;
            upgradeModel.MaxPetCount = basePlan.MaxPetCount;
            upgradeModel.AdditionalInfo = "";
            upgradeModel.StartDate = model.StartDate;
            upgradeModel.EndDate = model.EndDate;
            upgradeModel.Duration = duration;
            upgradeModel.RemainingDays = remainingDays;

            confirmationModel.PlanID = basePlan.Id;
            confirmationModel.Promocode = basePlan.PromotionCode;
            confirmationModel.PlanName = model.Plan;
            confirmationModel.Price = basePlan.Amount ?? 0;
            confirmationModel.Description = basePlan.Description;
            confirmationModel.AdditionalPets = basePlan.MaxPetCount;
            confirmationModel.AdditionalInfo = "";
            confirmationModel.StartDate = model.StartDate;
            confirmationModel.EndDate = model.EndDate;
            confirmationModel.Duration = duration;
            confirmationModel.RemainingDays = remainingDays;

            confirmationModel.CreditCardNumber = model.CreditCardNumber;
            confirmationModel.BillingExpirationDate = model.ExpirationDate;
            confirmationModel.CVV = model.CVV;
            confirmationModel.BillingAddress1 = model.BillingAddress1;
            confirmationModel.BillingCity = model.BillingCity;
            confirmationModel.BillingState = model.BillingState;
            confirmationModel.BillingCountry = model.BillingCountry;
            confirmationModel.BillingZip = model.BillingZip;
            confirmationModel.Email = email;

            PaymentResult paymentResult;
            if (ModelState.IsValid)
            {
                try
                {
                    if (WebConfigHelper.DoFakePayment)
                    {
                        paymentResult = PaymentHelper.FakePaymentForUpgradePlan(confirmationModel);
                    }
                    else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
                    {
                        paymentResult = PaymentHelper.FakePaymentForUpgradePlan(confirmationModel);
                    }
                    else if (CultureHelper.GetCurrentCulture() == "pt-BR")
                    {
                        paymentResult = PaymentHelper.PTAuthorizePaymentForUpgradePlan(confirmationModel);
                    }
                    else if (DomainHelper.GetDomain() == DomainTypeEnum.US)
                    {
                        paymentResult = PaymentHelper.USAuthorizePaymentForUpgradePlan(confirmationModel);
                    }
                    else
                    {
                        paymentResult = PaymentHelper.USAuthorizePaymentForUpgradePlan(confirmationModel);
                    }

                    //datetime in the user timezone

                    if (paymentResult.Success)
                    {
                        var userId = HttpContext.User.GetUserId();
                        var userSubscriptionId = User.GetUserSubscriptionId();
                        var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == userSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users);
                        var user = userSubscription.Users.First();

                        bool isFreeAccountUser = false;
                        var timeZoneInfoId = UnitOfWork.TimeZoneRepository.GetSingleTracking(t => t.Id == user.TimeZoneId).TimeZoneInfoId;
                        var userDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, timeZoneInfoId);

                        if (DomainHelper.GetDomain() == DomainTypeEnum.US || DomainHelper.GetDomain() == DomainTypeEnum.India)
                        {
                            try
                            {
                                if (userSubscription.Subscription.PlanTypeId == PlanTypeEnum.BasicFree && userSubscription.Subscription.IsBase)//==> "Free Account With Limited Access")
                                {
                                    isFreeAccountUser = true;
                                }
                            }
                            catch { }
                        }


                        var maxPets = confirmationModel.AdditionalPets;
                        var startDate = confirmationModel.StartDate;
                        var expirationDate = confirmationModel.EndDate;
                        var planName = confirmationModel.PlanName;

                        upgradePlan.StartDate = startDate;
                        upgradePlan.EndDate = expirationDate;

                        var subscriptionHistory = confirmationModel.Map(upgradePlan, paymentResult, userSubscription, userId);
                        UnitOfWork.UserSubscriptionHistoryRepository.Insert(subscriptionHistory);
                        UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
                        UnitOfWork.Save();

                        var maxPetCount = maxPets + (userSubscription.SubscriptionService != null ?
                                          (userSubscription.SubscriptionService.AditionalPetCount.HasValue
                                              ? userSubscription.SubscriptionService.AditionalPetCount.Value
                                              : 0) : 0);
                        User.UpdateUserMaxPetCount(maxPetCount);
                        HttpContext.User.UpdateIsPlanExpired();

                        //TODO : Signupchanges -commented on 26th May 2015 (Nutan)
                        if (isFreeAccountUser)
                        {
                            EmailSender.SendWelcomeMail(user.Email, user.FirstName, user.LastName, planName, confirmationModel.Promocode);
                        }


                        EmailSender.SendMyPlanConfirmationMail(user.Email, user.FirstName, user.LastName, startDate, expirationDate, planName, confirmationModel.Promocode, confirmationModel.AdditionalPets.ToString(), DomainHelper.GetCurrency() + confirmationModel.Price, paymentResult.OrderId, confirmationModel.BillingAddress1, confirmationModel.BillingAddress2,
                                confirmationModel.BillingCity, EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingState), confirmationModel.BillingZip.HasValue ? confirmationModel.BillingZip.Value.ToString() : null,
                                EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingCountry), user.Email);

                        EmailSender.SendToSupportPlanChange(user.FirstName, userId.ToString(), user.LastName, paymentResult.OrderId, confirmationModel.Promocode, planName, confirmationModel.AdditionalPets.ToString(), startDate, expirationDate, DomainHelper.GetCurrency() + confirmationModel.Price, paymentResult.TransactionResult);

                        Session["PlanAddPets"] = null;
                        Session["RenewalInfo"] = null;
                        Session["PlanUpgradeInfo"] = null;
                        Session["PaymentData"] = null;
                    }
                }

                catch (Exception ex)
                {
                    return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
                }

            }
            //if all is well, this will never happen
            //throw new Exception("Client Validation Failed!");
            return RedirectToAction("OwnerDetail");
        }

        [HttpPost]
        public ActionResult PlanConfirmation()
        {
            //  bool isFreeAccountUser = false;

            var billinginfo = Session["PaymentData"] as PlanBillingViewModel;

            var planAddPets = Session["PlanAddPets"] as PlanAddPetsViewModel;
            var renewalPlan = Session["RenewalInfo"] as PlanRenewalViewModel;
            var upgradePlan = Session["PlanUpgradeInfo"] as PlanUpgradeViewModel;
            var email = HttpContext.User.GetUserEmail();
            var confirmationModel = upgradePlan != null ? new PlanConfirmationViewModel(upgradePlan, billinginfo, email)
                : planAddPets != null
                ? new PlanConfirmationViewModel(planAddPets, billinginfo, email)
                : new PlanConfirmationViewModel(renewalPlan, billinginfo, email);
            var ActionMade = upgradePlan != null ? "Upgradeplan" : planAddPets != null ? "PlanAddPets" : "RenewalPlan";
            PaymentResult paymentResult;

            try
            {
                if (WebConfigHelper.DoFakePayment)
                {
                    paymentResult = PaymentHelper.FakePaymentForUpgradePlan(confirmationModel);
                }
                else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
                {
                    paymentResult = PaymentHelper.FakePaymentForUpgradePlan(confirmationModel);
                }
                else if (CultureHelper.GetCurrentCulture() == "pt-BR")
                {
                    paymentResult = PaymentHelper.PTAuthorizePaymentForUpgradePlan(confirmationModel);
                }
                else if (DomainHelper.GetDomain() == DomainTypeEnum.US)
                {
                    paymentResult = PaymentHelper.USAuthorizePaymentForUpgradePlan(confirmationModel);
                }
                else
                {
                    paymentResult = PaymentHelper.USAuthorizePaymentForUpgradePlan(confirmationModel);
                }

                //datetime in the user timezone

                if (paymentResult.Success)
                {
                    if (renewalPlan != null)
                    {
                        if (renewalPlan.DeletedPets != null)
                        {
                            var petID = renewalPlan.DeletedPets.Split();
                            foreach (var item in petID)
                            {
                                DeletePet(Convert.ToInt32(item));
                            }
                        }

                        DeleteUnUsedPets(renewalPlan.DeletedUnUsedPets);
                    }
                    var userId = HttpContext.User.GetUserId();
                    var userSubscriptionId = User.GetUserSubscriptionId();
                    var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == userSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users);
                    var user = userSubscription.Users.First();

                    //TODO : Signupchanges -commented on 26th May 2015 (Nutan)
                    //check if the user basic plan is free account with limited access?
                    //var timeZoneInfoId = UnitOfWork.TimeZoneRepository.GetSingleTracking(t => t.Id == user.TimeZoneId).TimeZoneInfoId;
                    //var userDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, timeZoneInfoId);
                    //if (DomainHelper.GetDomain() == DomainTypeEnum.US || DomainHelper.GetDomain() == DomainTypeEnum.India)
                    //{
                    //    try
                    //    {
                    //        if (string.IsNullOrEmpty(userSubscription.Subscription.PaymentTypeId.ToString()) && userSubscription.Subscription.IsBase)//==> "Free Account With Limited Access")
                    //        {
                    //            isFreeAccountUser = true;
                    //        }
                    //    }
                    //    catch { }
                    //}

                    var maxPets = upgradePlan != null ? upgradePlan.MaxPetCount : planAddPets != null ? planAddPets.MaxPetCount : renewalPlan.MaxPetCount;
                    var startDate = upgradePlan != null ? upgradePlan.StartDate : planAddPets != null ? planAddPets.StartDate : renewalPlan.StartDate;
                    var expirationDate = upgradePlan != null ? upgradePlan.EndDate : planAddPets != null ? planAddPets.EndDate : renewalPlan.EndDate;
                    var planName = upgradePlan != null ? upgradePlan.BasicPlan : planAddPets != null ? planAddPets.BasicPan : userSubscription.Subscription.Name;

                    var subscriptionHistory = upgradePlan != null
                        ? confirmationModel.Map(upgradePlan, paymentResult, userSubscription, userId)
                        : planAddPets != null
                        ? confirmationModel.Map(planAddPets, paymentResult, userSubscription, userId)
                        : confirmationModel.Map(renewalPlan, paymentResult, userSubscription, userId);

                    UnitOfWork.UserSubscriptionHistoryRepository.Insert(subscriptionHistory);
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
                    UnitOfWork.Save();

                    var maxPetCount = maxPets + (userSubscription.SubscriptionService != null ?
                                      (userSubscription.SubscriptionService.AditionalPetCount.HasValue
                                          ? userSubscription.SubscriptionService.AditionalPetCount.Value
                                          : 0) : 0);
                    User.UpdateUserMaxPetCount(maxPetCount);
                    HttpContext.User.UpdateIsPlanExpired();
                    //TODO : Signupchanges -commented on 26th May 2015 (Nutan)
                    //if (isFreeAccountUser)
                    //{
                    //    //EmailSender.SendSubscriptionMail(user.Email, user.FirstName, user.LastName, userDateTime, startDate, planName, confirmationModel.Promocode, paymentResult.OrderId, confirmationModel.BillingAddress1, confirmationModel.BillingAddress2,
                    //    //        confirmationModel.BillingCity, EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingState), confirmationModel.BillingZip.HasValue ? confirmationModel.BillingZip.Value.ToString() : null,
                    //    //        EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingCountry), user.Email);
                    //    EmailSender.SendWelcomeMail(user.Email, user.FirstName, user.LastName, planName, confirmationModel.Promocode);
                    //}


                    EmailSender.SendMyPlanConfirmationMail(user.Email, user.FirstName, user.LastName, startDate, expirationDate, planName, confirmationModel.Promocode, confirmationModel.AdditionalPets.ToString(), DomainHelper.GetCurrency() + confirmationModel.Price, paymentResult.OrderId, confirmationModel.BillingAddress1, confirmationModel.BillingAddress2,
                            confirmationModel.BillingCity, EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingState), confirmationModel.BillingZip.HasValue ? confirmationModel.BillingZip.Value.ToString() : null,
                            EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingCountry), user.Email);

                    EmailSender.SendToSupportPlanChange(user.FirstName, userId.ToString(), user.LastName, paymentResult.OrderId, confirmationModel.Promocode, planName, confirmationModel.AdditionalPets.ToString(), startDate, expirationDate, DomainHelper.GetCurrency() + confirmationModel.Price, paymentResult.TransactionResult);



                    Session["PlanAddPets"] = null;
                    Session["RenewalInfo"] = null;
                    Session["PlanUpgradeInfo"] = null;
                    Session["PaymentData"] = null;
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

            return PartialView("_PaymentResult", new PaymentResultViewModel(paymentResult, confirmationModel.PlanName, DateTime.Now, ActionMade));
        }

        [HttpGet]
        public ActionResult CmcicPlanConfirmation()
        {
            var billinginfo = Session["PaymentData"] as PlanBillingViewModel;

            var planAddPets = Session["PlanAddPets"] as PlanAddPetsViewModel;
            var renewalPlan = Session["RenewalInfo"] as PlanRenewalViewModel;
            var upgradePlan = Session["PlanUpgradeInfo"] as PlanUpgradeViewModel;

            PaymentResult paymentResult = new PaymentResult
            {
                OrderId = "",
                Success = false,
                TransactionID = "",
                ErrorMessage = "Transaction Failed"
            };

            var email = HttpContext.User.GetUserEmail();
            if (billinginfo != null)
            {
                paymentResult = new PaymentResult
                {
                    OrderId = "",
                    Success = true,
                    TransactionID = ""
                };

                var confirmationModel = upgradePlan != null ? new PlanConfirmationViewModel(upgradePlan, billinginfo, email)
                    : planAddPets != null
                    ? new PlanConfirmationViewModel(planAddPets, billinginfo, email)
                    : new PlanConfirmationViewModel(renewalPlan, billinginfo, email);
                var ActionMade = upgradePlan != null ? "Upgradeplan" : planAddPets != null ? "PlanAddPets" : "RenewalPlan";

                if (renewalPlan != null)
                {
                    if (renewalPlan.DeletedPets != null)
                    {
                        var petID = renewalPlan.DeletedPets.Split();
                        foreach (var item in petID)
                        {
                            DeletePet(Convert.ToInt32(item));
                        }
                    }

                    DeleteUnUsedPets(renewalPlan.DeletedUnUsedPets);
                }

                var userId = HttpContext.User.GetUserId();
                var userSubscriptionId = User.GetUserSubscriptionId();
                var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == userSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users);
                var user = userSubscription.Users.First();

                var maxPets = upgradePlan != null ? upgradePlan.MaxPetCount : planAddPets != null ? planAddPets.MaxPetCount : renewalPlan.MaxPetCount;
                var startDate = upgradePlan != null ? upgradePlan.StartDate : planAddPets != null ? planAddPets.StartDate : renewalPlan.StartDate;
                var expirationDate = upgradePlan != null ? upgradePlan.EndDate : planAddPets != null ? planAddPets.EndDate : renewalPlan.EndDate;
                var planName = upgradePlan != null ? upgradePlan.BasicPlan : planAddPets != null ? planAddPets.BasicPan : userSubscription.Subscription.Name;

                var subscriptionHistory = upgradePlan != null
                    ? confirmationModel.Map(upgradePlan, paymentResult, userSubscription, userId)
                    : planAddPets != null
                    ? confirmationModel.Map(planAddPets, paymentResult, userSubscription, userId)
                    : confirmationModel.Map(renewalPlan, paymentResult, userSubscription, userId);
                UnitOfWork.UserSubscriptionHistoryRepository.Insert(subscriptionHistory);
                UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
                UnitOfWork.Save();

                var maxPetCount = maxPets + (userSubscription.SubscriptionService != null ?
                                  (userSubscription.SubscriptionService.AditionalPetCount.HasValue
                                      ? userSubscription.SubscriptionService.AditionalPetCount.Value
                                      : 0) : 0);
                User.UpdateUserMaxPetCount(maxPetCount);
                HttpContext.User.UpdateIsPlanExpired();

                EmailSender.SendMyPlanConfirmationMail(user.Email, user.FirstName, user.LastName, startDate, expirationDate, planName, confirmationModel.Promocode, confirmationModel.AdditionalPets.ToString(), DomainHelper.GetCurrency() + confirmationModel.Price, paymentResult.OrderId, confirmationModel.BillingAddress1, confirmationModel.BillingAddress2,
                        confirmationModel.BillingCity, EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingState), confirmationModel.BillingZip.HasValue ? confirmationModel.BillingZip.Value.ToString() : null,
                        EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingCountry), user.Email);
                EmailSender.SendToSupportPlanChange(user.FirstName, userId.ToString(), user.LastName, paymentResult.OrderId, confirmationModel.Promocode, planName, confirmationModel.AdditionalPets.ToString(), startDate, expirationDate, DomainHelper.GetCurrency() + confirmationModel.Price, paymentResult.TransactionResult);

                Session["PlanAddPets"] = null;
                Session["RenewalInfo"] = null;
                Session["PlanUpgradeInfo"] = null;
                Session["PaymentData"] = null;


                return View("CmcicPlanConfirmation", new PaymentResultViewModel(paymentResult, confirmationModel.PlanName, DateTime.Now, ActionMade));
            }

            return View("CmcicPlanConfirmation", new PaymentResultViewModel(paymentResult, "", DateTime.Now, ""));
        }

        [HttpGet]
        public ActionResult CmcicPlanError()
        {
            var billinginfo = Session["PaymentData"] as PlanBillingViewModel;

            PaymentResult paymentResult = new PaymentResult
            {
                OrderId = "",
                Success = false,
                TransactionID = "",
                ErrorMessage = "Transaction Failed"
            };
            return View("CmcicPlanConfirmation", new PaymentResultViewModel(paymentResult, "", DateTime.Now, ""));
        }

        [HttpPost]
        public ActionResult PlanRenewal(PlanRenewalViewModel model)
        {
            if (ModelState.IsValid)
            {
                var planId = Convert.ToInt16(model.PlanName);
                model.PlanID = planId;
                var basePlan = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == planId);
                if (basePlan != null)
                {
                    var totalPrice = basePlan.Amount + (basePlan.AmmountPerAddionalPet * model.AdditionalPetRenewal);
                    var totalMra = model.AdditionalPetRenewal + basePlan.MRACount;
                    var totalPets = model.AdditionalPetRenewal + basePlan.MaxPetCount;
                    var totalSmo = basePlan.SMOCount;

                    //var planName = string.Format(Resources.Wording.Account_SignUp_AdditionalPetsInformation, basePlan.Amount, basePlan.MaxPetCount, basePlan.MaxPetCount);
                    var planName = string.Format(Resources.Wording.Account_SignUp_PetsInformation, basePlan.Amount, basePlan.MaxPetCount, basePlan.MaxPetCount);
                    var additionalInfo = string.Format(Resources.Wording.Account_Signup_AdditionalPetInfo, basePlan.AmmountPerAddionalPet);

                    model.Price = totalPrice;
                    model.BasicPlan = planName;
                    model.Promocode = basePlan.PromotionCode;
                    model.MaxPetCount = basePlan.MaxPetCount;
                    model.PlanName = GetPlanName(basePlan, totalPrice ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);
                    model.AdditionalInfo = additionalInfo;
                    model.Duration = basePlan.Duration == 0 ? (int)(model.EndDate.Subtract(model.StartDate).TotalDays) + 1 : basePlan.Duration;
                    model.RemainingDays = (int)(model.EndDate.Subtract(model.StartDate).TotalDays) + 1;
                }

                Session["RenewalInfo"] = model;
                Session["PlanInfo"] = null;

                ViewBag.IsRenewal = true;

                return PartialView("PlanBilling", new PlanBillingViewModel(model.PlanName, model.Price ?? 0));
            }
            //if all is well, this will never happen
            throw new Exception("Client Validation Failed!");
        }

        [HttpGet]
        public ActionResult GetPetsPrice(int subscriptionId, int petCount, int? NumberofPets)
        {
            var subscription = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == subscriptionId);
            var currentMaxPet = NumberofPets ?? User.GetUserMaxPetCount();
            var petAdded = currentMaxPet - subscription.MaxPetCount;
            var petToAdd = petCount - petAdded;

            if (subscription != null)
            {
                var totalPrice = subscription.Amount + subscription.AmmountPerAddionalPet * petCount;
                var totalMra = petCount + subscription.MRACount;
                var totalPets = petCount + subscription.MaxPetCount;
                var totalSmo = subscription.SMOCount;
                var finalPlan = GetPlanName(subscription, totalPrice ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);

                var data = new
                {
                    TotalPrice = totalPrice,
                    finalPlan = finalPlan,
                    PriceToPay = petToAdd * subscription.AmmountPerAddionalPet
                };

                return Json(data, JsonRequestBehavior.AllowGet);
            }

            throw new Exception("Invalid Subscription Id");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var login = UnitOfWork.LoginRepository.GetByUserNameAndPassword(HttpContext.User.Identity.Name, model.OldPassword);
                if (login != null)
                {
                    model.Map(login);
                    UnitOfWork.LoginRepository.Update(login);
                    UnitOfWork.Save();

                    HttpContext.User.UpdateIsTemporalPassword();
                    Success(Resources.Wording.Profile_ChangePassword_ChangePasswordSuccessMessage);
                    return PartialView(model);
                }
            }

            ModelState.AddModelError("", Resources.Wording.Profile_ChangePassword_IncorrectPassword);
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult ForceToChangePassword()
        {
            return View(new ForceToChangePasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForceToChangePassword(ForceToChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var login = UnitOfWork.LoginRepository.GetByUserNameAndPassword(HttpContext.User.Identity.Name, model.OldPassword);
                if (login != null)
                {
                    model.Map(login);
                    login.IsTemporalPassword = false;

                    UnitOfWork.LoginRepository.Update(login);
                    UnitOfWork.Save();

                    HttpContext.User.UpdateIsTemporalPassword();

                    Success(Resources.Wording.Profile_ChangePassword_ChangePasswordSuccessMessage);
                    if (HttpContext.User.IsInRole(UserTypeEnum.Admin.ToString()) || HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString()) || HttpContext.User.IsInRole(UserTypeEnum.VeterinarianLight.ToString()))
                    {
                        return RedirectToAction("Index", "Users");
                    }
                    else if (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                    {
                        return RedirectToAction("Index", "SMO");
                    }
                    return RedirectToAction("Index", "Pet");
                }
            }

            ModelState.AddModelError("", Resources.Wording.Profile_ChangePassword_IncorrectPassword);
            return View(model);
        }

        [HttpPost]
        public JsonResult ValidateEmail(string Email)
        {
            var userId = HttpContext.User.GetUserId();
            //var usedByUserId = UnitOfWork.UserRepository.GetUserIdByEmail(Email);
            //var valid = !usedByUserId.HasValue || usedByUserId.Value == userId;
            var valid = UnitOfWork.UserRepository.IfMailAlreadyExist(Email, userId);
            return Json(valid);
        }

        [HttpGet]
        public ActionResult GetAdditionalPetPrice(int NumberOfPets)
        {

            var BasePlan = UnitOfWork.SubscriptionRepository.GetSingle(u => !u.IsPromotionCode && u.IsBase == true);

            if (BasePlan != null)
            {
                var data = new { Price = (BasePlan.AmmountPerAddionalPet * NumberOfPets) };

                return Json(data, JsonRequestBehavior.AllowGet);
            }

            throw new Exception("Invalid Plan Id");
        }

        [HttpGet]
        public ActionResult GetStartDate(int PlanID, DateTime RenewalDate)
        {
            var subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == PlanID);
            var renewalDate = RenewalDate;

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
            else
            {
                renewalDate = renewalDate.AddDays(subscription.Duration);
            }

            renewalDate = renewalDate.AddDays(-1);

            var userId = HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);
            var timezoneID = user.TimeZone != null ? user.TimeZone.TimeZoneInfoId : TimeZoneInfo.Local.Id;

            var startDate = user.UserSubscription.RenewalDate ?? DateTime.Now;
            //  var endDate = getRenewalDate(subscription, startDate);

            //var startDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(RenewalDate, timezoneID);
            renewalDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(renewalDate, timezoneID);

            var planDetails = new
            {
                StartDate = startDate,
                ExpirationDate = renewalDate,
                Dueration = string.Format(string.Format(Resources.Wording.Profile_PlanEdit_Days, subscription.Duration == 0 ? (int)(renewalDate.Subtract(RenewalDate).TotalDays) + 1 : subscription.Duration)),
                RemainingDyas = string.Format(string.Format(Resources.Wording.Profile_PlanEdit_Days, (int)(renewalDate.Subtract(RenewalDate).TotalDays) + 1)),
                price = DomainHelper.GetCurrency() + subscription.Amount,
                planinfo = subscription.Name,
                additionalInfo = subscription.AditionalInfo,
                MaxPetCount = subscription.MaxPetCount
            };

            return Json(planDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPlanPrice(int PlanID)
        {
            var subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == PlanID);
            var totalAmmount = subscription.Amount;
            var totalMra = subscription.MRACount;
            var totalPets = subscription.MaxPetCount;
            var totalSmo = subscription.SMOCount;
            int planvalue = 0;
            //TODO : Signupchanges -commented on 26th May 2015 (Nutan)
            //if (DomainHelper.GetDomain() == DomainTypeEnum.US || DomainHelper.GetDomain() == DomainTypeEnum.India)
            //{
            //    totalMra = (AdditionalPets * subscription.MaxPetCount) + subscription.MRACount;
            //    if (!string.IsNullOrEmpty(subscription.AditionalInfo) && subscription.IsBase)
            //    {
            //        //$50
            //        planvalue = 1;
            //        totalPets = subscription.MaxPetCount + (AdditionalPets * subscription.MaxPetCount);
            //    }
            //    else if (subscription.IsBase)
            //    {
            //        //$70
            //        planvalue = 2;
            //        totalPets = subscription.MaxPetCount + AdditionalPets;
            //    }
            //    else
            //    {
            //        totalPets = subscription.MaxPetCount + AdditionalPets;
            //    }
            //}
            //else
            //{
            totalPets = subscription.MaxPetCount;
            // }

            var finalPlan = subscription.Name;
            // GetPlanName(subscription, totalAmmount ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);
            return Json(new { finalPlan = finalPlan, price = totalAmmount ?? 0, AdditionalInfo = subscription.AditionalInfo, Description = subscription.Description, planvalue = planvalue, MaxPetCount = subscription.MaxPetCount }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ClearSessionData()
        {
            Session["PlanAddPets"] = null;
            Session["RenewalInfo"] = null;
            Session["PlanUpgradeInfo"] = null;
            Session["PaymentData"] = null;
            return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        }

        private string GetPlanName(Subscription subscription, decimal totalAmt, int totalPets, int TotalMra, int totalSmo)
        {
            var subscriptionName = "";
            //if (subscription.HasMRA && subscription.HasSMO)
            //{
            //    subscriptionName = string.Format(totalPets == 1 ? Resources.Subscription.SinglePlanWithMRAWithSMO : Resources.Subscription.PlanWithMRAWithSMO, totalAmt, totalPets, TotalMra, totalSmo);
            //}
            //else if (subscription.HasMRA && !subscription.HasSMO)
            //{
            //    subscriptionName = string.Format(totalPets == 1 ? Resources.Subscription.SinglePlanWithMRA : Resources.Subscription.PlanWithMRA, totalAmt, totalPets, TotalMra);
            //}
            //else if (!subscription.HasMRA && subscription.HasSMO)
            //{
            //    subscriptionName = string.Format(totalPets == 1 ? Resources.Subscription.SinglePlanWithSMO : Resources.Subscription.PlanWithSMO, totalAmt, totalPets, totalSmo);
            //}
            //else if (!subscription.HasMRA && !subscription.HasSMO)
            //{
            //    subscriptionName = string.Format(totalPets == 1 ? Resources.Subscription.SinglePlanWithoutMRA : Resources.Subscription.PlanWithoutMRA, totalAmt, totalPets);
            //}
            return subscription.Name;
        }

        [HttpGet]
        public ActionResult GetPlansByPromocode(string promocode, int AdditionalPets, string CurrentPromo)
        {
            var userId = HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription);
            var subscriptionBaseID = user.UserSubscription.Subscription.SubscriptionBaseId;

            var defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(i => i.PromotionCode == promocode && i.IsTrial == false && i.IsVisibleToOwner && !i.IsDeleted).OrderBy(u => u.MaxPetCount);
            var isvalid = true;

            if (!defaultPlans.Any())
            {
                if (CurrentPromo == promocode)
                {
                    defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(i => i.PromotionCode == promocode && i.IsTrial == false && !i.IsDeleted).OrderBy(u => u.MaxPetCount);
                }
                else
                {
                    defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(i => i.IsBase && !i.IsPromotionCode && i.IsTrial == false && !i.IsDeleted).OrderBy(u => u.MaxPetCount);
                    isvalid = false;
                }
            }
            int? centerId = HttpContext.User.GetCenterId();
            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(i => i.PromotionCode == promocode && i.IsTrial == false && i.IsVisibleToOwner && !i.IsDeleted && i.CenterId == centerId).OrderBy(u => u.MaxPetCount);
                isvalid = true;
                if (!defaultPlans.Any())
                {
                    if (CurrentPromo == promocode)
                    {
                        defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(i => i.PromotionCode == promocode && i.IsTrial == false && !i.IsDeleted && i.CenterId == centerId).OrderBy(u => u.MaxPetCount);
                    }
                    else
                    {
                        defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(i => i.IsBase && !i.IsPromotionCode && i.IsTrial == false && !i.IsDeleted).OrderBy(u => u.MaxPetCount);
                        isvalid = false;
                    }
                }
            }

            if (subscriptionBaseID != null)
            {
                defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(i => i.Id == subscriptionBaseID && i.IsTrial == false && !i.IsDeleted).OrderBy(u => u.MaxPetCount);
            }
            else if (CurrentPromo == promocode)
            {
                int _subId = user.UserSubscription.SubscriptionId;
                defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(i => i.Id == _subId && i.IsTrial == false && !i.IsDeleted).OrderBy(u => u.MaxPetCount);
            }//6April2015

            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                if (subscriptionBaseID != null)
                {
                    defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(i => i.Id == subscriptionBaseID && i.IsTrial == false && !i.IsDeleted && i.CenterId == centerId).OrderBy(u => u.MaxPetCount);
                }
                else if (CurrentPromo == promocode)
                {
                    int _subId = user.UserSubscription.SubscriptionId;
                    defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(i => i.Id == _subId && i.IsTrial == false && !i.IsDeleted && i.CenterId == centerId).OrderBy(u => u.MaxPetCount);
                }//6April2015

            }


            var items = defaultPlans.Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_AdditionalPetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });

            var basePlan = defaultPlans.First();

            var totalAmmount = basePlan.Amount + (basePlan.AmmountPerAddionalPet * AdditionalPets);
            var totalMra = AdditionalPets + basePlan.MRACount;
            var totalPets = AdditionalPets + basePlan.MaxPetCount;
            int? totalSmo = basePlan.SMOCount;

            var additionalInfo = string.Format(Resources.Wording.Account_Signup_AdditionalPetInfo, basePlan.AmmountPerAddionalPet);

            var finalPlan = GetPlanName(basePlan, totalAmmount ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);
            var planDetails = new { FinalPlanName = finalPlan, AdditionInfo = additionalInfo };
            if (string.IsNullOrEmpty(promocode))
            {
                isvalid = false;
            }
            var data = new { Items = items, BasePlanDetails = planDetails, Price = totalAmmount, isvalid = isvalid, MaxPetCount = basePlan.MaxPetCount };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetImage()
        {
            var userId = HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
            if (user == null || user.ProfileImage == null)
            {
                return File("/Content/Images/ownerProfilepic.jpg", "image/jpg");
            }
            return File(user.ProfileImage, "image/jpg");
        }

        [HttpGet]
        public ActionResult GetImageById(int Id)
        {
            //var userId = HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == Id);
            if (user.ProfileImage == null)
            {
                return File("/Content/Images/ownerProfilepic.jpg", "image/jpg");
            }
            return File(user.ProfileImage, "image/jpg");
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
        public ActionResult GetRenewalInfo(int? userid)
        {
            var userId = userid ?? HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.TempUserSubscription, u => u.UserSubscription.TempUserSubscription.Subscription);
            var defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted).OrderBy(u => u.MaxPetCount);
            var subscription = user.UserSubscription.TempUserSubscription.Subscription;
            var planName = subscription.Name;
            var additionalPets = user.UserSubscription.TempUserSubscription.AditionalPetCount ?? 0;
            var totalAmmount = subscription.Amount + (subscription.AmmountPerAddionalPet * additionalPets);
            var totalMra = additionalPets + subscription.MRACount;
            var totalPets = additionalPets + subscription.MaxPetCount;
            var totalSmo = subscription.SMOCount;
            var finalPlan = subscription.Name;// GetPlanName(subscription, totalAmmount ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);

            return PartialView("PlanRenewalDetails", new PlanRenewalDetailsViewModel(user.UserSubscription.TempUserSubscription, planName, finalPlan, additionalPets));
        }

        [HttpGet]
        public ActionResult OwnerProfile(int id, int petId)
        {
            var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == id);
            ViewBag.PetId = petId;
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            Session[Constants.SessionCurrentUserRequestFrom] = (Session[Constants.SessionCurrentUserRequestFrom] != null) ? Session[Constants.SessionCurrentUserRequestFrom].ToString() : "pet";
            TempData.Keep("PetId_" + user.Id);
            return View("OwnerProfile", new OwnerEditViewModel(user));
        }

        [HttpGet]
        public ActionResult MyAccount()
        {
            int userId = HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);

            Session[Constants.SessionCurrentUserRequestFrom] = (Session[Constants.SessionCurrentUserRequestFrom] != null) ? Session[Constants.SessionCurrentUserRequestFrom].ToString() : "pet";
            TempData.Keep("PetId_" + user.Id);
            string vetspeciality = String.Empty;
            var specility = UnitOfWork.VeterinarianRepository.GetSingle(s => s.UserId == user.Id);
            if (specility != null)
                vetspeciality = specility.VetSpecialtyID.Value.ToString();
            return View("MyAccountProfile", new OwnerEditViewModel(user, vetspeciality));
        }

        [HttpGet]
        public ActionResult ChangePasswordPopup()
        {
            return PartialView("_ChangePasswordPopup");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePasswordPopup(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var login = UnitOfWork.LoginRepository.GetByUserNameAndPassword(HttpContext.User.Identity.Name, model.OldPassword);
                if (login != null)
                {
                    model.Map(login);
                    UnitOfWork.LoginRepository.Update(login);
                    UnitOfWork.Save();

                    HttpContext.User.UpdateIsTemporalPassword();
                    Success(Resources.Wording.Profile_ChangePassword_ChangePasswordSuccessMessage);

                    return RedirectToAction("MyAccount");
                }
            }

            ModelState.AddModelError("", Resources.Wording.Profile_ChangePassword_IncorrectPassword);
            return PartialView("_ChangePasswordPopup", model);
        }

        [HttpGet]
        public ActionResult GetTotalPets(int? userid, int? totalPets)
        {
            int userId = userid ?? HttpContext.User.GetUserId();
            var dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == userId));
            var currentPetCount = totalPets ?? User.GetUserMaxPetCount();

            List<RemovePetsViewModel> pets = new List<RemovePetsViewModel>();
            if (dbPets != null)
                pets = dbPets.Select(p => new RemovePetsViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, p.GenderId, p.Image)).ToList();

            ViewBag.UnUsedPets = currentPetCount - pets.Count;

            //  if (pets.Count > 0)
            return PartialView("RemovePets", pets);

            //return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public void DeletePet(int id)
        {
            var userId = User.GetUserId();
            var pet = (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                ? UnitOfWork.PetRepository.GetSingle(p => p.Id == id, p => p.Insurances, p => p.Farmer, p => p.PetContacts, p => p.Veterinarians, p => p.Users)
                : UnitOfWork.PetRepository.GetSingle(p => p.Id == id && p.Users.Any(u => u.Id == userId), p => p.Insurances, p => p.Farmer, p => p.PetContacts, p => p.Veterinarians, p => p.Users);
            if (pet != null)
            {
                pet.IsDeleted = true;

                UnitOfWork.PetRepository.Update(pet);

                UnitOfWork.Save();
                Success(Resources.Wording.Pet_Card_DeleteMsg);
                Session[Constants.SessionCurrentUserPetCount] = UnitOfWork.PetRepository.GetPetCountByUser(userId);
            }

            var userSubscriptionId = User.GetUserSubscriptionId();
            var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == userSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users);
            var user = userSubscription.Users.First();

            var subscriptionHistory = new UserSubscriptionHistory
            {
                SubscriptionId = userSubscription.SubscriptionId,
                StartDate = userSubscription.StartDate ?? DateTime.Today,
                EndDate = userSubscription.RenewalDate ?? DateTime.Today,
                PurchaseDate = DateTime.Today,
                UsedDate = DateTime.Today,
                RenewvalDate = userSubscription.RenewalDate ?? DateTime.Today,
                AditionalPetCount = userSubscription.SubscriptionService.AditionalPetCount,
                UserId = userId
            };
            var TotalPets = userSubscription.SubscriptionService.AditionalPetCount;
            userSubscription.SubscriptionService.AditionalPetCount = TotalPets - 1;
            UnitOfWork.UserSubscriptionHistoryRepository.Insert(subscriptionHistory);
            UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
            UnitOfWork.Save();

            var maxPetCount = userSubscription.Subscription.MaxPetCount +
                              (userSubscription.SubscriptionService.AditionalPetCount.HasValue
                                  ? userSubscription.SubscriptionService.AditionalPetCount.Value
                                  : 0);
            User.UpdateUserMaxPetCount(maxPetCount);

        }

        public void DeleteUnUsedPets(int numberOfPets)
        {
            var userId = User.GetUserId();
            var userSubscriptionId = User.GetUserSubscriptionId();
            var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == userSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users);
            var user = userSubscription.Users.First();

            var subscriptionHistory = new UserSubscriptionHistory
            {
                SubscriptionId = userSubscription.SubscriptionId,
                StartDate = userSubscription.StartDate ?? DateTime.Today,
                EndDate = userSubscription.RenewalDate ?? DateTime.Today,
                PurchaseDate = DateTime.Today,
                UsedDate = DateTime.Today,
                RenewvalDate = userSubscription.RenewalDate ?? DateTime.Today,
                AditionalPetCount = userSubscription.SubscriptionService.AditionalPetCount,
                UserId = userId
            };
            var TotalPets = userSubscription.SubscriptionService.AditionalPetCount;
            userSubscription.SubscriptionService.AditionalPetCount = TotalPets - numberOfPets;
            UnitOfWork.UserSubscriptionHistoryRepository.Insert(subscriptionHistory);
            UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
            UnitOfWork.Save();

            var maxPetCount = userSubscription.Subscription.MaxPetCount +
                              (userSubscription.SubscriptionService.AditionalPetCount.HasValue
                                  ? userSubscription.SubscriptionService.AditionalPetCount.Value
                                  : 0);
            User.UpdateUserMaxPetCount(maxPetCount);

            Session[Constants.SessionCurrentUserPetCount] = UnitOfWork.PetRepository.GetPetCountByUser(userId);

        }

        public ActionResult EditPlan(int userid)
        {
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userid, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);
            var defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted).OrderBy(u => u.MaxPetCount);
            var subscription = user.UserSubscription.Subscription;
            var PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted);
            ViewBag.Plans = PlansNPromocode.Where(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
            var subscriptionbaseID = subscription.SubscriptionBaseId;
            int? centerId = HttpContext.User.GetCenterId();
            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                if (!string.IsNullOrEmpty(subscription.PromotionCode))
                    PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted && s.CenterId == centerId);
                else
                    PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted);

                ViewBag.Plans = PlansNPromocode.Where(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
                ViewBag.Promocode = PlansNPromocode.Where(p => p.CenterId == centerId).GroupBy(o => o.PromotionCode).Select(g => g.First()).Where(u => u.IsPromotionCode && u.PromotionCode != Constants.FreeUserPromoCode).Select(i => new SelectListItem { Text = i.PromotionCode, Value = i.PromotionCode }).OrderBy(a => a.Text);
            }

            if (subscription.IsPromotionCode)
            {
                ViewBag.Plans = PlansNPromocode.Where(i => i.PromotionCode == subscription.PromotionCode && i.IsTrial == false).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
                //if (subscriptionbaseID != null)
                //{
                //    // ViewBag.Plans = PlansNPromocode.Where(i => i.Id == subscriptionbaseID).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
                //    // subscription = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == subscriptionbaseID);
                //}
            }


            //editing plan based on CenterId


            var planName = subscription.Name;
            var additionalPets = user.UserSubscription.SubscriptionService == null ? 0 : user.UserSubscription.SubscriptionService.AditionalPetCount ?? 0;
            var totalAmmount = subscription.Amount + (subscription.AmmountPerAddionalPet * additionalPets);
            var totalMra = additionalPets + subscription.MRACount;
            var totalPets = additionalPets + subscription.MaxPetCount;
            var totalSmo = subscription.SMOCount;
            var finalPlan = GetPlanName(subscription, totalAmmount ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);
            if (string.IsNullOrEmpty(centerId.ToString()))
            {
                ViewBag.Promocode = PlansNPromocode.GroupBy(o => o.PromotionCode).Select(g => g.First()).Where(u => u.IsPromotionCode).Select(i => new SelectListItem { Text = i.PromotionCode, Value = i.PromotionCode }).OrderBy(a => a.Text);
            }
            var listItems = Enumerable.Range(1, 10).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() }).ToList();
            listItems.Add(new SelectListItem { Value = "0", Text = Resources.Wording.Profile_PlanEdit_addpets });
            ViewBag.AdditionalPets = listItems;
            var startDate = user.UserSubscription.StartDate ?? DateTime.Now;
            var endDate = startDate.AddYears(1);
            startDate = DateTime.Today;
            endDate = endDate.AddDays(-1);
            var timezoneID = user.TimeZone != null ? user.TimeZone.TimeZoneInfoId : TimeZoneInfo.Local.Id;
            startDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startDate, timezoneID);
            endDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(endDate, timezoneID);
            var userId = HttpContext.User.GetUserId();
            //var UserData = UnitOfWork.UserRepository.GetSingleTracking(a => a.Id == userId);

            return PartialView("_EditPlan", new EditPlanViewModel(user, subscription, planName, finalPlan, additionalPets, startDate, endDate));
        }

        [HttpPost]
        public ActionResult EditPlan(EditPlanViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Expression<Func<UserSubscription, object>> parames1 = u=>u.;
                //Expression<Func<UserSubscription, object>> parames2 = u => u.SubscriptionService;

                //Expression<Func<User, object>>[] paramesArray = new Expression<Func<User, object>>[] { parames1, parames2, parames3, parames4, parames5 };

                var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(us => us.Id == model.UserSubscriptionId, navigationProperties: u => u.SubscriptionService);
                if (userSubscription != null)
                {
                    model.map(userSubscription);
                    var planId = Convert.ToInt32(model.PlanName);
                    var UserSub = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == planId);
                    var UserIdChange = UnitOfWork.UserRepository.GetSingleTracking(s => s.Id == model.UserID);
                    if (UserSub.CenterId != null)
                    {
                        UserIdChange.CenterID = UserSub.CenterId;
                        UnitOfWork.UserRepository.Update(UserIdChange);
                        UnitOfWork.Save();

                    }
                    if (UserSub.CenterId == null)
                    {
                        UserIdChange.CenterID = UserSub.CenterId;
                        UnitOfWork.UserRepository.Update(UserIdChange);
                        UnitOfWork.Save();
                    }
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
                    UnitOfWork.Save();
                }
            }
            else
            {
                return PartialView("EditPlan", new { userid = model.UserID });
            }
            return RedirectToAction("ViewUser", "Users", new { id = model.UserID });
        }

        [HttpGet]
        public ActionResult GetPlanAdditionalDetails(int PlanID)
        {
            var subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == PlanID);

            //var userId = HttpContext.User.GetUserId();
            //var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);

            var data = new { AdditionalInformation = subscription.AditionalInfo, MaxPetCount = subscription.MaxPetCount, Price = subscription.Amount == null ? 0 : subscription.Amount };

            return Json(data, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult UpgradePlan()
        {
            var userId = HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);
            IEnumerable<Subscription> defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted).OrderBy(u => u.MaxPetCount);
            var subscription = user.UserSubscription.Subscription;
            var subscriptionRenewal = subscription;
            var subscriptionbaseID = subscription.SubscriptionBaseId;
            if (subscriptionbaseID != null)
            {
                subscriptionRenewal = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == subscriptionbaseID);
            }

            List<SelectPlanViewModel> planModel;
            Expression<Func<Subscription, object>> parames1 = s => s.PlanType;
            Expression<Func<Subscription, object>> parames2 = s => s.PlanFeatures;
            Expression<Func<Subscription, object>> parames3 = s => s.PaymentType;
            Expression<Func<Subscription, object>>[] paramesArray = new Expression<Func<Subscription, object>>[] { parames1, parames2, parames3 };
            planModel = UnitOfWork.SubscriptionRepository.GetAll(u => u.IsVisibleToOwner && u.PromotionCode == subscription.PromotionCode && !u.IsDeleted, navigationProperties: paramesArray).Select(s => new SelectPlanViewModel(s)).OrderBy(s => s.lstPlanData.PlanType).ToList();

            var listItems = Enumerable.Range(1, 10).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() }).ToList();
            listItems.Add(new SelectListItem { Value = "0", Text = Resources.Wording.Profile_PlanEdit_addpets });
            ViewBag.AdditionalPets = listItems;

            if (subscription.IsPromotionCode)
            {
                var promocode = user.UserSubscription.Subscription.PromotionCode;
                ViewBag.Plan = defaultPlans.Where(i => i.PromotionCode == promocode && i.IsTrial == false).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
                if (subscriptionbaseID != null)
                {
                    ViewBag.Plan = defaultPlans.Where(i => i.Id == subscriptionbaseID).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
                }
                else
                {
                    ViewBag.Plans = defaultPlans.Where(i => i.Id == subscription.Id).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
                }
            }
            else
            {
                ViewBag.Plan = defaultPlans.Where(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
            }
            if (DomainHelper.GetDomain() == DomainTypeEnum.India || DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                int defaultId = 0;
                if (string.IsNullOrEmpty(subscription.PaymentTypeId.ToString()) && subscription.IsBase)//==> "Free Account With Limited Access")
                {
                    defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted && s.IsBase && s.Id != subscription.Id).OrderBy(u => u.MaxPetCount);
                    IEnumerable<SelectListItem> lstPlans = defaultPlans.Where(i => !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false).Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_PetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() });
                    ViewBag.Plan = lstPlans;
                    defaultId = Convert.ToInt32(lstPlans.FirstOrDefault().Value);

                    subscriptionRenewal = defaultPlans.Where(p => p.Id == defaultId).FirstOrDefault();
                }
            }
            PlanUpgradeViewModel upgradeModel = new OwnerDetailViewModel(user, subscription, subscriptionRenewal, planModel).UpgradePlan;

            return View("PlanUpgrade", upgradeModel);
        }



        [HttpGet]
        public ActionResult GetRenewPlansByPromocode(string promocode)
        {
            var lstPlans = UnitOfWork.SubscriptionRepository.GetAll(p => p.PromotionCode == promocode && !p.IsDeleted);
            if (lstPlans.Count() > 0)
            {
                List<NewPlanRenewViewModel> planModel;
                Expression<Func<Subscription, object>> parames1 = s => s.PlanType;
                Expression<Func<Subscription, object>> parames2 = s => s.PlanFeatures;
                Expression<Func<Subscription, object>> parames3 = s => s.PaymentType;
                Expression<Func<Subscription, object>>[] paramesArray = new Expression<Func<Subscription, object>>[] { parames1, parames2, parames3 };
                planModel = UnitOfWork.SubscriptionRepository.GetAll(u => u.IsVisibleToOwner && u.PromotionCode == promocode && !u.IsDeleted, navigationProperties: paramesArray).Select(s => new NewPlanRenewViewModel(s)).OrderBy(s => s.lstPlanData.PlanType).ToList();

                return PartialView("_SelectPlan_Renew", planModel);
            }
            throw new Exception("Something went wrong..!!!");
        }

        [HttpGet]
        public ActionResult RenewPlanBilling(int planId, string promocode)
        {
            var basePlan = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == planId, navigationProperties: p => p.PlanType);


            var userId = HttpContext.User.GetUserId();
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);
            var timezoneID = user.TimeZone != null ? user.TimeZone.TimeZoneInfoId : TimeZoneInfo.Local.Id;

            var renewalDate = DateTime.Today;
            var oldStartDate = (user.UserSubscription.RenewalDate.Value).AddDays(1);
            var startDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(oldStartDate, timezoneID);
            renewalDate = startDate.AddDays(-1);

            if (basePlan.PaymentTypeId == PaymentTypeEnum.Yearly)
            {
                renewalDate = renewalDate.AddYears(1);
            }
            else if (basePlan.PaymentTypeId == PaymentTypeEnum.Monthly)
            {
                renewalDate = renewalDate.AddMonths(1);
            }
            else if (basePlan.PaymentTypeId == PaymentTypeEnum.SaleTransaction || basePlan.PaymentTypeId == PaymentTypeEnum.ThirdPartyCompany)
            {
                //todo: refactor this
                renewalDate = renewalDate.AddYears(100);
            }
            else if (basePlan.PlanTypeId == PlanTypeEnum.BasicFree && basePlan.IsBase)
            {
                renewalDate = renewalDate.AddYears(basePlan.Duration);
            }
            else
            {
                renewalDate = renewalDate.AddDays(basePlan.Duration);
            }
            renewalDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(renewalDate, timezoneID);

            PlanBillingViewModel model = new PlanBillingViewModel(basePlan);
            model.StartDate = startDate;
            model.EndDate = renewalDate;

            ViewBag.IsRenewal = true;
            return PartialView("_RenewPlanBilling", model);
        }

        [HttpPost]
        public ActionResult RenewPlanBilling(PlanBillingViewModel model)
        {
            var email = HttpContext.User.GetUserEmail();

            PlanConfirmationViewModel confirmationModel = new PlanConfirmationViewModel();
            var basePlan = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == model.PlanId, navigationProperties: p => p.PlanType);
            PlanRenewalViewModel renewModel = new PlanRenewalViewModel();

            #region map billing to renew model

            var duration = basePlan.Duration == 0 ? (int)(model.EndDate.Subtract(model.StartDate).TotalDays) + 1 : basePlan.Duration;
            var remainingDays = (int)(model.EndDate.Subtract(model.StartDate).TotalDays) + 1;

            renewModel.PlanID = basePlan.Id;
            renewModel.BasicPlan = basePlan.Name;
            renewModel.Promocode = basePlan.PromotionCode;
            renewModel.PlanName = model.Plan;
            renewModel.Price = basePlan.Amount ?? 0;
            renewModel.Description = basePlan.Description;
            renewModel.MaxPetCount = basePlan.MaxPetCount;
            renewModel.AdditionalInfo = "";
            renewModel.StartDate = model.StartDate;
            renewModel.EndDate = model.EndDate;
            renewModel.Duration = duration;
            renewModel.RemainingDays = remainingDays;

            confirmationModel.PlanID = basePlan.Id;
            confirmationModel.Promocode = basePlan.PromotionCode;
            confirmationModel.PlanName = basePlan.Name;
            confirmationModel.Price = basePlan.Amount ?? 0;
            confirmationModel.Description = basePlan.Description;
            confirmationModel.AdditionalPets = basePlan.MaxPetCount;
            confirmationModel.AdditionalInfo = "";
            confirmationModel.StartDate = model.StartDate;
            confirmationModel.EndDate = model.EndDate;
            confirmationModel.Duration = duration;
            confirmationModel.RemainingDays = remainingDays;

            confirmationModel.CreditCardNumber = model.CreditCardNumber;
            confirmationModel.BillingExpirationDate = model.ExpirationDate;
            confirmationModel.CVV = model.CVV;
            confirmationModel.BillingAddress1 = model.BillingAddress1;
            confirmationModel.BillingCity = model.BillingCity;
            confirmationModel.BillingState = model.BillingState;
            confirmationModel.BillingCountry = model.BillingCountry;
            confirmationModel.BillingZip = model.BillingZip;
            confirmationModel.Email = email;
            #endregion

            PaymentResult paymentResult;

            try
            {
                if (WebConfigHelper.DoFakePayment)
                {
                    paymentResult = PaymentHelper.FakePaymentForUpgradePlan(confirmationModel);
                }
                else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
                {
                    paymentResult = PaymentHelper.FakePaymentForUpgradePlan(confirmationModel);
                }
                else if (CultureHelper.GetCurrentCulture() == "pt-BR")
                {
                    paymentResult = PaymentHelper.PTAuthorizePaymentForUpgradePlan(confirmationModel);
                }
                else if (DomainHelper.GetDomain() == DomainTypeEnum.US)
                {
                    paymentResult = PaymentHelper.USAuthorizePaymentForUpgradePlan(confirmationModel);
                }
                else
                {
                    paymentResult = PaymentHelper.USAuthorizePaymentForUpgradePlan(confirmationModel);
                }

                if (paymentResult.Success)
                {
                    //if (renewalPlan != null)
                    //{
                    //    if (renewalPlan.DeletedPets != null)
                    //    {
                    //        var petID = renewalPlan.DeletedPets.Split();
                    //        foreach (var item in petID)
                    //        {
                    //            DeletePet(Convert.ToInt32(item));
                    //        }
                    //    }

                    //    DeleteUnUsedPets(renewalPlan.DeletedUnUsedPets);
                    //}
                    var userId = HttpContext.User.GetUserId();
                    var userSubscriptionId = User.GetUserSubscriptionId();
                    var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == userSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users);
                    var user = userSubscription.Users.First();


                    //TODO : Signupchanges -commented on 26th May 2015 (Nutan)
                    //check if the user basic plan is free account with limited access?

                    bool isFreeAccountUser = false;
                    var timeZoneInfoId = UnitOfWork.TimeZoneRepository.GetSingleTracking(t => t.Id == user.TimeZoneId).TimeZoneInfoId;
                    var userDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, timeZoneInfoId);
                    if (DomainHelper.GetDomain() == DomainTypeEnum.US || DomainHelper.GetDomain() == DomainTypeEnum.India)
                    {
                        try
                        {
                            if (userSubscription.Subscription.PlanTypeId == PlanTypeEnum.BasicFree && userSubscription.Subscription.IsBase)//==> "Free Account With Limited Access")
                            {
                                isFreeAccountUser = true;
                            }
                        }
                        catch { }
                    }

                    var maxPets = confirmationModel.AdditionalPets;
                    var startDate = confirmationModel.StartDate;
                    var expirationDate = confirmationModel.EndDate;
                    var planName = confirmationModel.PlanName;

                    var subscriptionHistory = confirmationModel.Map(renewModel, paymentResult, userSubscription, userId);
                    UnitOfWork.UserSubscriptionHistoryRepository.Insert(subscriptionHistory);
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
                    UnitOfWork.Save();

                    var maxPetCount = maxPets + (userSubscription.SubscriptionService != null ?
                                      (userSubscription.SubscriptionService.AditionalPetCount.HasValue
                                          ? userSubscription.SubscriptionService.AditionalPetCount.Value
                                          : 0) : 0);
                    User.UpdateUserMaxPetCount(maxPetCount);
                    HttpContext.User.UpdateIsPlanExpired();
                    //  TODO : Signupchanges -commented on 26th May 2015 (Nutan)
                    if (isFreeAccountUser)
                    {
                        //EmailSender.SendSubscriptionMail(user.Email, user.FirstName, user.LastName, userDateTime, startDate, planName, confirmationModel.Promocode, paymentResult.OrderId, confirmationModel.BillingAddress1, confirmationModel.BillingAddress2,
                        //        confirmationModel.BillingCity, EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingState), confirmationModel.BillingZip.HasValue ? confirmationModel.BillingZip.Value.ToString() : null,
                        //        EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingCountry), user.Email);
                        EmailSender.SendWelcomeMail(user.Email, user.FirstName, user.LastName, planName, confirmationModel.Promocode);
                    }

                    EmailSender.SendMyPlanConfirmationMail(user.Email, user.FirstName, user.LastName, startDate, expirationDate, planName, confirmationModel.Promocode, confirmationModel.AdditionalPets.ToString(), DomainHelper.GetCurrency() + confirmationModel.Price, paymentResult.OrderId, confirmationModel.BillingAddress1, confirmationModel.BillingAddress2,
                            confirmationModel.BillingCity, EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingState), confirmationModel.BillingZip.HasValue ? confirmationModel.BillingZip.Value.ToString() : null,
                            EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingCountry), user.Email);

                    EmailSender.SendToSupportPlanChange(user.FirstName, userId.ToString(), user.LastName, paymentResult.OrderId, confirmationModel.Promocode, planName, confirmationModel.AdditionalPets.ToString(), startDate, expirationDate, DomainHelper.GetCurrency() + confirmationModel.Price, paymentResult.TransactionResult);

                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("OwnerDetail");
        }
    }
}

