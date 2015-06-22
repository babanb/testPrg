using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADOPets.Web.ViewModels.Account;
using ADOPets.Web.Common.Payment.Model;
using ADOPets.Web.Common.Helpers;
using Model;
using System.Globalization;
using System.Linq.Expressions;
using ADOPets.Web.Common.Authentication;
using System.Web.Security;
using ADOPets.Web.ViewModels.Profile;

namespace ADOPets.Web.Controllers
{
    public class OwnerPaymentController : BaseController
    {
        public ActionResult ShowMultiplePlanDetails()
        {
            try
            {
                var userId = HttpContext.User.GetUserId();

                Expression<Func<User, object>> parames1 = v => v.Veterinarians;
                Expression<Func<User, object>> parames2 = v => v.UserSubscription;
                Expression<Func<User, object>> parames3 = v => v.UserSubscription.SubscriptionService;
                Expression<Func<User, object>>[] paramesArray = new Expression<Func<User, object>>[] { parames1, parames2, parames3 };
                var dbUsers = UnitOfWork.UsersRepository.GetSingle(u => u.Id == userId && u.UserStatusId == UserStatusEnum.Active, navigationProperties: paramesArray);//.Where(u => u.UserStatusId == UserStatusEnum.Active);//.Select(u => new ViewModels.Users.IndexViewModel(u, u.Veterinarians.FirstOrDefault()));
                var subscriptionObj = UnitOfWork.SubscriptionRepository.GetSingle(s => s.Id == dbUsers.UserSubscription.SubscriptionId);
                BasicInfoViewModel model = new BasicInfoViewModel();
                UsersController usersCon = new UsersController();

                var tempusersubscriptionID = dbUsers.UserSubscription.TempUserSubscriptionId != null ? dbUsers.UserSubscription.TempUserSubscriptionId : 0;

                var tempSubscrition = UnitOfWork.TempUserSubscriptionRepository.GetSingle(i => i.Id == tempusersubscriptionID && i.isCreatedbyOwner == false);

                if (dbUsers.UserSubscription.IsAdditionalPet == true && tempSubscrition != null)
                {
                    #region additional pet
                    PlanRenewalViewModel model1 = new PlanRenewalViewModel();
                    var priceInfo = UnitOfWork.SubscriptionRepository.GetSingle(s => s.Id == dbUsers.UserSubscription.SubscriptionId);

                    model1.PlanID = priceInfo.Id;
                    model1.StartDate = dbUsers.UserSubscription.StartDate ?? DateTime.Today;
                    model1.Promocode = priceInfo.IsPromotionCode ? priceInfo.PromotionCode : "NA";
                    model1.EndDate = dbUsers.UserSubscription.RenewalDate ?? DateTime.Now;
                    model1.AdditionalPetRenewal = dbUsers.UserSubscription.AdditionalPetcount ?? 0;
                    var Dueration = priceInfo.Duration == 0 ? (int)(model1.EndDate.Subtract(model1.StartDate).TotalDays) + 1 : priceInfo.Duration;
                    var RemainingDays = (int)(model1.EndDate - DateTime.Today).TotalDays < 0 ? 0 : (int)(model1.EndDate - DateTime.Today).TotalDays;
                    RemainingDays = RemainingDays == 0 ? RemainingDays : RemainingDays + 1;
                    model1.Duration = Dueration;
                    model1.RemainingDays = RemainingDays;
                    var additionalPets = dbUsers.UserSubscription.SubscriptionService != null ? dbUsers.UserSubscription.SubscriptionService.AditionalPetCount ?? 0 : 0;
                    var totalAmmount = priceInfo.Amount + (priceInfo.AmmountPerAddionalPet * (model1.AdditionalPetRenewal + additionalPets));
                    var totalMra = additionalPets + model1.AdditionalPetRenewal + priceInfo.MRACount;

                    var totalPets = additionalPets + model1.AdditionalPetRenewal + priceInfo.MaxPetCount;
                    var totalSmo = priceInfo.SMOCount;
                    model1.PlanName = usersCon.GetPlanName(priceInfo, totalAmmount ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);
                    model1.BasicPlan = priceInfo.Name;
                    model1.Description = priceInfo.Description;
                    model1.AdditionalInfo = priceInfo.AditionalInfo;

                    if (dbUsers.UserSubscription.StartDate == dbUsers.UserSubscription.StartDate && dbUsers.UserSubscription.RenewalDate == dbUsers.UserSubscription.RenewalDate)
                    {
                        model1.Price = (priceInfo.AmmountPerAddionalPet * model1.AdditionalPetRenewal);
                    }
                    else
                    {
                        model1.Price = priceInfo.Amount + (priceInfo.AmmountPerAddionalPet * model1.AdditionalPetRenewal);
                    }
                    model1.PlanType = @Resources.Wording.OwnerBilling_ShowPlan_AdditionalPet;
                    #endregion

                    #region tempsub
                    PlanRenewalViewModel model2 = new PlanRenewalViewModel();
                    var priceInfo1 = UnitOfWork.SubscriptionRepository.GetSingle(s => s.Id == tempSubscrition.SubscriptionId);

                    model2.PlanID = priceInfo1.Id;
                    model2.StartDate = tempSubscrition.StartDate;
                    model2.Promocode = priceInfo1.IsPromotionCode ? priceInfo1.PromotionCode : "NA";
                    model2.EndDate = tempSubscrition.RenewalDate ?? DateTime.Now;
                    model2.AdditionalPetRenewal = tempSubscrition.AditionalPetCount ?? 0;
                    var Duration = priceInfo1.Duration == 0 ? (int)(model2.EndDate.Subtract(model2.StartDate).TotalDays) + 1 : priceInfo1.Duration;
                    model2.Duration = Duration;
                    model2.RemainingDays = Duration;
                    var totalAmount1 = priceInfo1.Amount + (priceInfo1.AmmountPerAddionalPet * model2.AdditionalPetRenewal);
                    var totalMra1 = model2.AdditionalPetRenewal + priceInfo1.MRACount;
                    var totalPets1 = model2.AdditionalPetRenewal + priceInfo1.MaxPetCount;
                    var totalSmo1 = priceInfo1.SMOCount;
                    model2.PlanName = usersCon.GetPlanName(priceInfo1, totalAmount1 ?? 0, totalPets1, totalMra1 ?? 0, totalSmo1 ?? 0);
                    model2.BasicPlan = priceInfo1.Name;
                    model2.Description = priceInfo1.Description;
                    model2.AdditionalInfo = priceInfo1.AditionalInfo;

                    if (dbUsers.UserSubscription.StartDate == tempSubscrition.StartDate && dbUsers.UserSubscription.RenewalDate == tempSubscrition.RenewalDate)
                    {
                        model2.Price = (priceInfo1.AmmountPerAddionalPet * model2.AdditionalPetRenewal);
                    }
                    else
                    {
                        model2.Price = priceInfo1.Amount + (priceInfo1.AmmountPerAddionalPet * model2.AdditionalPetRenewal);
                    }
                    model2.PlanType = @Resources.Wording.OwnerBilling_ShowPlan_RenewPlan;
                    #endregion
                    List<PlanRenewalViewModel> lstMultiplePlan = new List<PlanRenewalViewModel>();
                    lstMultiplePlan.Add(model1);
                    lstMultiplePlan.Add(model2);
                    Session["MultiplePlan"] = lstMultiplePlan;

                    List<PlanBillingViewModel> lstModels = new List<PlanBillingViewModel>();
                    PlanBillingViewModel plan1 = new PlanBillingViewModel(model1.PlanName, model1.Price ?? 0, model1.PlanID, model1.PlanType);
                    PlanBillingViewModel plan2 = new PlanBillingViewModel(model2.PlanName, model2.Price ?? 0, model2.PlanID, model2.PlanType);

                    lstModels.Add(plan1);
                    lstModels.Add(plan2);

                    Session["MultiplePlanDetails"] = lstModels;

                    return View("ShowPlan", lstModels);
                }
            }
            catch (Exception) { }
            return RedirectToAction("Billing");
        }

        [HttpPost]
        public JsonResult ShowMultiplePlanDetails(List<PlanBillingViewModel> lstPlanId, FormCollection fc)
        {
            var userId = HttpContext.User.GetUserId();

            #region Plan details

            List<PlanBillingViewModel> sessionModel = (List<PlanBillingViewModel>)Session["MultiplePlanDetails"];
            Expression<Func<User, object>> parames1 = v => v.Veterinarians;
            Expression<Func<User, object>> parames2 = v => v.UserSubscription;
            Expression<Func<User, object>> parames3 = v => v.UserSubscription.SubscriptionService;
            Expression<Func<User, object>>[] paramesArray = new Expression<Func<User, object>>[] { parames1, parames2, parames3 };
            var dbUsers = UnitOfWork.UsersRepository.GetSingle(u => u.Id == userId && u.UserStatusId == UserStatusEnum.Active, navigationProperties: paramesArray);//.Where(u => u.UserStatusId == UserStatusEnum.Active);//.Select(u => new ViewModels.Users.IndexViewModel(u, u.Veterinarians.FirstOrDefault()));
            var subscriptionObj = UnitOfWork.SubscriptionRepository.GetSingle(s => s.Id == dbUsers.UserSubscription.SubscriptionId);
            BasicInfoViewModel model = new BasicInfoViewModel();
            UsersController usersCon = new UsersController();

            var tempusersubscriptionID = dbUsers.UserSubscription.TempUserSubscriptionId != null ? dbUsers.UserSubscription.TempUserSubscriptionId : 0;

            var tempSubscrition = UnitOfWork.TempUserSubscriptionRepository.GetSingle(i => i.Id == tempusersubscriptionID && i.isCreatedbyOwner == false);
            #endregion
            Session["MultiplePlanDetails"] = sessionModel;
            if (sessionModel.Count() == lstPlanId.Count())
            {
                // both plans are selected
            }
            else
            {
                List<PlanBillingViewModel> listTobeDeleted = sessionModel.Where(p => !lstPlanId.Any(p2 => p2.PlanType == p.PlanType)).ToList();

                var DelUserSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(s => s.Id == dbUsers.UserSubscriptionId);

                foreach (PlanBillingViewModel viewModel in listTobeDeleted)
                {
                    if (viewModel.PlanType == Resources.Wording.OwnerBilling_ShowPlan_AdditionalPet)
                    {
                        DelUserSubscription.IsAdditionalPet = false;
                        DelUserSubscription.AdditionalPetcount = null;
                        UnitOfWork.UserSubscriptionRepository.Update(DelUserSubscription);
                        UnitOfWork.Save();
                    }
                    if (viewModel.PlanType == Resources.Wording.OwnerBilling_ShowPlan_RenewPlan)
                    {
                        DelUserSubscription.TempUserSubscriptionId = null;
                        DelUserSubscription.TempUserSubscription = null;
                        UnitOfWork.UserSubscriptionRepository.Update(DelUserSubscription);
                        UnitOfWork.Save();
                    }
                }
                return Json(new { success = false });
            }
            return Json(new { success = true });
        }

        public ActionResult MultiplePlanBilling()
        {
            List<PlanRenewalViewModel> multiplePlan = (List<PlanRenewalViewModel>)Session["MultiplePlan"];

            decimal? totalPrice = 0;
            decimal? addpetPrice = 0;
            PlanBillingViewModel planBill = new PlanBillingViewModel();

            foreach (PlanRenewalViewModel plan in multiplePlan)
            {
                totalPrice += plan.Price;
                if (plan.PlanType == Resources.Wording.OwnerBilling_ShowPlan_AdditionalPet)
                {
                    addpetPrice += plan.Price;
                }
                if (plan.PlanType == Resources.Wording.OwnerBilling_ShowPlan_RenewPlan)
                {
                    planBill.Plan = plan.PlanName;
                }

                planBill.PlanId = plan.PlanID;
                planBill.ExpirationDate = plan.EndDate.ToShortDateString();

            }
            planBill.AddPetCharge = addpetPrice.ToString();
            planBill.Price = totalPrice.ToString();



            Session["MultiplePlan"] = multiplePlan;
            Session["MultiplePlanTotalPrice"] = totalPrice;

            return View("MultiplePlanBilling", planBill);
        }

        [HttpPost]
        public ActionResult MultiplePlanBilling(PlanBillingViewModel model)
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

            Session["PlanPaymentData"] = model;
            //var planDetails = Session["Plandetails"] as PlanRenewalViewModel;
            List<PlanRenewalViewModel> multiplePlan = (List<PlanRenewalViewModel>)Session["MultiplePlan"];
            var totalPrice = Session["MultiplePlanTotalPrice"];
            PlanRenewalViewModel myPlan = null;
            PlanRenewalViewModel renewalPlan = null;
            foreach (PlanRenewalViewModel plan in multiplePlan)
            {
                if (plan.PlanType == Resources.Wording.Users_OwnerPayment_RenewPlan)
                {
                    renewalPlan = plan;
                }
                else
                {
                    myPlan = plan;
                }
            }
            Session["MultiplePlan"] = multiplePlan;
            Session["MultiplePlanTotalPrice"] = totalPrice;
            return PartialView("MultiplePlanConfirmation", new PlanConfirmationViewModel(renewalPlan, myPlan, model, HttpContext.User.GetUserEmail()));
        }

        [HttpGet]
        public ActionResult CmcicPaymentError()
        {
            PaymentResult paymentResult = new PaymentResult
            {
                OrderId = "",
                Success = false,
                TransactionID = "",
                ErrorMessage = "Transaction Failed"
            };
            return View("CmcicPlanConfirmation", new ADOPets.Web.ViewModels.Profile.PaymentResultViewModel(paymentResult, "", DateTime.Now, ""));
        }

        [HttpGet]
        public ActionResult CmcicPaymentConfirmation()
        {

            var userId = HttpContext.User.GetUserId();
            List<PlanRenewalViewModel> multiplePlan = (List<PlanRenewalViewModel>)Session["MultiplePlan"];

            PlanRenewalViewModel planInfo = null;
            PlanRenewalViewModel currentPlan = null;

            PaymentResult paymentResult = new PaymentResult
           {
               OrderId = "",
               Success = false,
               TransactionID = "",
               ErrorMessage = "Transaction Failed"
           };

            if (multiplePlan != null)
            {
                paymentResult = new PaymentResult
                {
                    OrderId = "",
                    Success = true,
                    TransactionID = ""
                };



                foreach (PlanRenewalViewModel plan in multiplePlan)
                {
                    if (plan.PlanType == Resources.Wording.Users_OwnerPayment_RenewPlan)
                    {
                        planInfo = plan;
                    }
                    else
                    {
                        currentPlan = plan;
                    }
                }

                var renewplanprice = planInfo.Price;

                planInfo.Price = Convert.ToDecimal(Session["MultiplePlanTotalPrice"].ToString());
                var billinginfo = Session["PlanPaymentData"] as PlanBillingViewModel;
                var email = HttpContext.User.GetUserEmail();
                var confirmationModel = new PlanConfirmationViewModel(planInfo, billinginfo, email);


                //datetime in the user timezone
                var ActionMade = "";

                var userSubscriptionId = User.GetUserSubscriptionId();
                var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == userSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users, u => u.TempUserSubscription);
                var user = userSubscription.Users.First();

                var maxPets = planInfo.MaxPetCount;
                var startDate = planInfo.StartDate;
                var expirationDate = planInfo.EndDate;
                var planName = planInfo.BasicPlan;

                ActionMade = user.UserSubscription.TempUserSubscription != null ? user.UserSubscription.TempUserSubscription.actionName : "";
                var tempdate = user.UserSubscription.TempUserSubscription;
                var userSubscriptionNew = user.UserSubscription;
                if (userSubscriptionNew.SubscriptionService == null)
                {
                    var serviceObj = new SubscriptionService
                    {
                        AditionalPetCount = userSubscriptionNew.AdditionalPetcount
                    };

                    userSubscription.SubscriptionService = serviceObj;
                }
                else
                {
                    if (userSubscription.SubscriptionService.AditionalPetCount == null)
                    {
                        userSubscription.SubscriptionService.AditionalPetCount = userSubscriptionNew.AdditionalPetcount;
                    }
                    else
                    {
                        userSubscription.SubscriptionService.AditionalPetCount += userSubscriptionNew.AdditionalPetcount;
                    }
                }
                Model.BillingInformation billingInfo = new BillingInformation
                {
                    PaymentTypeId = PaymentTypeEnum.Yearly,
                    Address1 = billinginfo.BillingAddress1,
                    Address2 = billinginfo.BillingAddress2,
                    City = billinginfo.BillingCity,
                    CountryId = billinginfo.BillingCountry.Value,
                    StateId = billinginfo.BillingState,
                    Zip = billinginfo.BillingZip.ToString(),
                    CreditCardTypeId = billinginfo.CreditCardType
                };
                UnitOfWork.BillingInformationRepository.Insert(billingInfo);
                UnitOfWork.Save();

                var paymentHistory = new PaymentHistory
                {
                    Amount = planInfo.Price ?? 0,
                    PaymentDate = DateTime.Today,
                    TransactionNumber = paymentResult.OrderId,
                    ErrorMessage = paymentResult.ErrorMessage,
                    UserSubscriptionId = userSubscriptionNew.Id,
                    BillingInformationId = billingInfo.Id,
                    BillingInformation = billingInfo
                };

                userSubscription.PaymentHistories = new List<PaymentHistory> { paymentHistory };
                userSubscription.ispaymentDone = true;
                userSubscription.IsAdditionalPet = false;
                userSubscription.AdditionalPetcount = null;
                UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
                UnitOfWork.Save();

                var maxpet = (userSubscription.Subscription != null) ? userSubscriptionNew.Subscription.MaxPetCount : 0;
                var maxPetCount = maxpet +
                         (userSubscription.SubscriptionService.AditionalPetCount.HasValue
                             ? userSubscription.SubscriptionService.AditionalPetCount.Value
                             : 0);
                User.UpdateUserMaxPetCount(maxPetCount);
                ActionMade = "MultiTransaction";
                // }
                EmailSender.SendMyPlanConfirmationMail(user.Email, user.FirstName, user.LastName, currentPlan.StartDate, currentPlan.EndDate, currentPlan.BasicPlan, currentPlan.Promocode, currentPlan.AdditionalPetRenewal.ToString(), DomainHelper.GetCurrency() + currentPlan.Price, paymentResult.OrderId, confirmationModel.BillingAddress1, confirmationModel.BillingAddress2,
                     confirmationModel.BillingCity, EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingState), confirmationModel.BillingZip.HasValue ? confirmationModel.BillingZip.Value.ToString() : null,
                     EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingCountry), user.Email);
                EmailSender.SendToSupportPlanChange(user.FirstName, userId.ToString(), user.LastName, paymentResult.OrderId, currentPlan.Promocode, currentPlan.BasicPlan, currentPlan.AdditionalPetRenewal.ToString(), currentPlan.StartDate, currentPlan.EndDate, DomainHelper.GetCurrency() + currentPlan.Price, paymentResult.TransactionResult);



                if (user.UserSubscription.RenewalDate < DateTime.Today)
                {
                    tempdate = user.UserSubscription.TempUserSubscription;
                    userSubscriptionNew = user.UserSubscription;
                    userSubscriptionNew.StartDate = tempdate.StartDate;
                    userSubscriptionNew.RenewalDate = tempdate.RenewalDate;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();

                    userSubscriptionNew.Subscription = tempdate.Subscription;
                    userSubscriptionNew.SubscriptionId = tempdate.SubscriptionId;

                    if (userSubscriptionNew.SubscriptionService == null)
                    {
                        var serviceObj = new SubscriptionService
                        {
                            AditionalPetCount = tempdate.AditionalPetCount
                        };

                        userSubscriptionNew.SubscriptionService = serviceObj;
                    }
                    else
                    {
                        userSubscriptionNew.SubscriptionService.AditionalPetCount = tempdate.AditionalPetCount;
                    }
                    userSubscriptionNew.TempUserSubscriptionId = null;
                    userSubscriptionNew.TempUserSubscription = null;

                    userSubscriptionNew.ispaymentDone = true;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();
                }


                EmailSender.SendMyPlanConfirmationMail(user.Email, user.FirstName, user.LastName, planInfo.StartDate, planInfo.EndDate, planInfo.BasicPlan, planInfo.Promocode, planInfo.AdditionalPetRenewal.ToString(), DomainHelper.GetCurrency() + renewplanprice, paymentResult.OrderId, confirmationModel.BillingAddress1, confirmationModel.BillingAddress2,
                    confirmationModel.BillingCity, EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingState), confirmationModel.BillingZip.HasValue ? confirmationModel.BillingZip.Value.ToString() : null,
                    EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingCountry), user.Email);
                EmailSender.SendToSupportPlanChange(user.FirstName, userId.ToString(), user.LastName, paymentResult.OrderId, planInfo.Promocode, planInfo.BasicPlan, planInfo.AdditionalPetRenewal.ToString(), planInfo.StartDate, planInfo.EndDate, DomainHelper.GetCurrency() + renewplanprice, paymentResult.TransactionResult);

                HttpContext.User.UpdateIsPaymentDone();

                Session["CurrentPlan"] = currentPlan.PlanName;
                Session["RenewPlan"] = planInfo.PlanName;

                Session["Plandetails"] = null;
                Session["PlanPaymentData"] = null;
                return View("CmcicPlanConfirmation", new ADOPets.Web.ViewModels.Profile.PaymentResultViewModel(paymentResult, confirmationModel.PlanName, DateTime.Now, ActionMade));
            }

            return View("CmcicPlanConfirmation", new ADOPets.Web.ViewModels.Profile.PaymentResultViewModel(paymentResult, "", DateTime.Now, ""));
        }

        [HttpPost]
        public ActionResult MultiplePlanConfirmation()
        {
            var userId = HttpContext.User.GetUserId();
            List<PlanRenewalViewModel> multiplePlan = (List<PlanRenewalViewModel>)Session["MultiplePlan"];

            PlanRenewalViewModel planInfo = null;
            PlanRenewalViewModel currentPlan = null;

            foreach (PlanRenewalViewModel plan in multiplePlan)
            {
                if (plan.PlanType == Resources.Wording.Users_OwnerPayment_RenewPlan)
                {
                    planInfo = plan;
                }
                else
                {
                    currentPlan = plan;
                }
            }

            var renewplanprice = planInfo.Price;

            planInfo.Price = Convert.ToDecimal(Session["MultiplePlanTotalPrice"].ToString());
            var billinginfo = Session["PlanPaymentData"] as PlanBillingViewModel;
            var email = HttpContext.User.GetUserEmail();
            // var planInfo = Session["Plandetails"] as PlanRenewalViewModel;
            var confirmationModel = new PlanConfirmationViewModel(planInfo, billinginfo, email);

            PaymentResult paymentResult;

            if (WebConfigHelper.DoFakePayment)
            {
                paymentResult = PaymentHelper.FakePaymentForUpgradePlan(confirmationModel);
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)  //For the Momonet Fake Payment for IN Domain
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
            var ActionMade = "";
            if (paymentResult.Success)
            {
                var userSubscriptionId = User.GetUserSubscriptionId();
                var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == userSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users, u => u.TempUserSubscription);
                var user = userSubscription.Users.First();

                var maxPets = planInfo.MaxPetCount;
                var startDate = planInfo.StartDate;
                var expirationDate = planInfo.EndDate;
                var planName = planInfo.BasicPlan;

                ActionMade = user.UserSubscription.TempUserSubscription != null ? user.UserSubscription.TempUserSubscription.actionName : "";
                //if (userSubscription.IsAdditionalPet == true)
                //{
                var tempdate = user.UserSubscription.TempUserSubscription;
                var userSubscriptionNew = user.UserSubscription;
                if (userSubscriptionNew.SubscriptionService == null)
                {
                    var serviceObj = new SubscriptionService
                    {
                        AditionalPetCount = userSubscriptionNew.AdditionalPetcount
                    };

                    userSubscription.SubscriptionService = serviceObj;
                }
                else
                {
                    if (userSubscription.SubscriptionService.AditionalPetCount == null)
                    {
                        userSubscription.SubscriptionService.AditionalPetCount = userSubscriptionNew.AdditionalPetcount;
                    }
                    else
                    {
                        userSubscription.SubscriptionService.AditionalPetCount += userSubscriptionNew.AdditionalPetcount;
                    }
                }
                Model.BillingInformation billingInfo = new BillingInformation
                {
                    PaymentTypeId = PaymentTypeEnum.Yearly,
                    Address1 = billinginfo.BillingAddress1,
                    Address2 = billinginfo.BillingAddress2,
                    City = billinginfo.BillingCity,
                    CountryId = billinginfo.BillingCountry.Value,
                    StateId = billinginfo.BillingState,
                    Zip = billinginfo.BillingZip.ToString(),
                    CreditCardTypeId = billinginfo.CreditCardType
                };
                UnitOfWork.BillingInformationRepository.Insert(billingInfo);
                UnitOfWork.Save();

                var paymentHistory = new PaymentHistory
                {
                    Amount = planInfo.Price ?? 0,
                    PaymentDate = DateTime.Today,
                    TransactionNumber = paymentResult.OrderId,
                    ErrorMessage = paymentResult.ErrorMessage,
                    UserSubscriptionId = userSubscriptionNew.Id,
                    BillingInformationId = billingInfo.Id,
                    BillingInformation = billingInfo
                };

                userSubscription.PaymentHistories = new List<PaymentHistory> { paymentHistory };
                userSubscription.ispaymentDone = true;
                userSubscription.IsAdditionalPet = false;
                userSubscription.AdditionalPetcount = null;
                UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
                UnitOfWork.Save();

                var maxpet = (userSubscription.Subscription != null) ? userSubscriptionNew.Subscription.MaxPetCount : 0;
                var maxPetCount = maxpet +
                         (userSubscription.SubscriptionService.AditionalPetCount.HasValue
                             ? userSubscription.SubscriptionService.AditionalPetCount.Value
                             : 0);
                User.UpdateUserMaxPetCount(maxPetCount);
                ActionMade = "MultiTransaction";
                // }
                EmailSender.SendMyPlanConfirmationMail(user.Email, user.FirstName, user.LastName, currentPlan.StartDate, currentPlan.EndDate, currentPlan.BasicPlan, currentPlan.Promocode, currentPlan.AdditionalPetRenewal.ToString(), DomainHelper.GetCurrency() + currentPlan.Price, paymentResult.OrderId, confirmationModel.BillingAddress1, confirmationModel.BillingAddress2,
                     confirmationModel.BillingCity, EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingState), confirmationModel.BillingZip.HasValue ? confirmationModel.BillingZip.Value.ToString() : null,
                     EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingCountry), user.Email);
                EmailSender.SendToSupportPlanChange(user.FirstName, userId.ToString(), user.LastName, paymentResult.OrderId, currentPlan.Promocode, currentPlan.BasicPlan, currentPlan.AdditionalPetRenewal.ToString(), currentPlan.StartDate, currentPlan.EndDate, DomainHelper.GetCurrency() + currentPlan.Price, paymentResult.TransactionResult);



                if (user.UserSubscription.RenewalDate < DateTime.Today)
                {
                    tempdate = user.UserSubscription.TempUserSubscription;
                    userSubscriptionNew = user.UserSubscription;
                    userSubscriptionNew.StartDate = tempdate.StartDate;
                    userSubscriptionNew.RenewalDate = tempdate.RenewalDate;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();

                    userSubscriptionNew.Subscription = tempdate.Subscription;
                    userSubscriptionNew.SubscriptionId = tempdate.SubscriptionId;

                    if (userSubscriptionNew.SubscriptionService == null)
                    {
                        var serviceObj = new SubscriptionService
                        {
                            AditionalPetCount = tempdate.AditionalPetCount
                        };

                        userSubscriptionNew.SubscriptionService = serviceObj;
                    }
                    else
                    {
                        userSubscriptionNew.SubscriptionService.AditionalPetCount = tempdate.AditionalPetCount;
                    }
                    userSubscriptionNew.TempUserSubscriptionId = null;
                    userSubscriptionNew.TempUserSubscription = null;

                    //Model.BillingInformation billingInfo = new BillingInformation
                    //{
                    //    PaymentTypeId = PaymentTypeEnum.Yearly,
                    //    Address1 = billinginfo.BillingAddress1,
                    //    Address2 = billinginfo.BillingAddress2,
                    //    City = billinginfo.BillingCity,
                    //    CountryId = billinginfo.BillingCountry.Value,
                    //    StateId = billinginfo.BillingState,
                    //    Zip = billinginfo.BillingZip.ToString(),
                    //    CreditCardTypeId = billinginfo.CreditCardType
                    //};
                    //UnitOfWork.BillingInformationRepository.Insert(billingInfo);
                    //UnitOfWork.Save();

                    //var paymentHistory = new PaymentHistory
                    //{
                    //    Amount = planInfo.Price ?? 0,
                    //    PaymentDate = DateTime.Today,
                    //    TransactionNumber = paymentResult.OrderId,
                    //    ErrorMessage = paymentResult.ErrorMessage,
                    //    UserSubscriptionId = userSubscriptionNew.Id,
                    //    BillingInformationId = billingInfo.Id,
                    //    BillingInformation = billingInfo
                    //};

                    //userSubscriptionNew.PaymentHistories = new List<PaymentHistory> { paymentHistory };
                    userSubscriptionNew.ispaymentDone = true;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();

                    //var maxpet = (tempdate.Subscription != null) ? tempdate.Subscription.MaxPetCount : 0;
                    //var maxPetCount = maxpet +
                    //         (tempdate.AditionalPetCount.HasValue
                    //             ? tempdate.AditionalPetCount.Value
                    //             : 0);
                    //User.UpdateUserMaxPetCount(maxPetCount);
                }


                EmailSender.SendMyPlanConfirmationMail(user.Email, user.FirstName, user.LastName, planInfo.StartDate, planInfo.EndDate, planInfo.BasicPlan, planInfo.Promocode, planInfo.AdditionalPetRenewal.ToString(), DomainHelper.GetCurrency() + renewplanprice, paymentResult.OrderId, confirmationModel.BillingAddress1, confirmationModel.BillingAddress2,
                    confirmationModel.BillingCity, EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingState), confirmationModel.BillingZip.HasValue ? confirmationModel.BillingZip.Value.ToString() : null,
                    EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingCountry), user.Email);
                EmailSender.SendToSupportPlanChange(user.FirstName, userId.ToString(), user.LastName, paymentResult.OrderId, planInfo.Promocode, planInfo.BasicPlan, planInfo.AdditionalPetRenewal.ToString(), planInfo.StartDate, planInfo.EndDate, DomainHelper.GetCurrency() + renewplanprice, paymentResult.TransactionResult);

                HttpContext.User.UpdateIsPaymentDone();

                Session["CurrentPlan"] = currentPlan.PlanName;
                Session["RenewPlan"] = planInfo.PlanName;

                Session["Plandetails"] = null;
                Session["PlanPaymentData"] = null;
            }

            return PartialView("PlanPaymentResult", new ADOPets.Web.ViewModels.Profile.PaymentResultViewModel(paymentResult, confirmationModel.PlanName, DateTime.Now, ActionMade));
        }

        public ActionResult Billing()
        {
            var userId = HttpContext.User.GetUserId();

            Expression<Func<User, object>> parames1 = v => v.Veterinarians;
            Expression<Func<User, object>> parames2 = v => v.UserSubscription;
            Expression<Func<User, object>> parames3 = v => v.UserSubscription.SubscriptionService;
            Expression<Func<User, object>>[] paramesArray = new Expression<Func<User, object>>[] { parames1, parames2, parames3 };
            var dbUsers = UnitOfWork.UsersRepository.GetSingle(u => u.Id == userId && u.UserStatusId == UserStatusEnum.Active, navigationProperties: paramesArray);//.Where(u => u.UserStatusId == UserStatusEnum.Active);//.Select(u => new ViewModels.Users.IndexViewModel(u, u.Veterinarians.FirstOrDefault()));
            var subscriptionObj = UnitOfWork.SubscriptionRepository.GetSingle(s => s.Id == dbUsers.UserSubscription.SubscriptionId);
            BasicInfoViewModel model = new BasicInfoViewModel();
            UsersController usersCon = new UsersController();

            var tempusersubscriptionID = dbUsers.UserSubscription.TempUserSubscriptionId != null ? dbUsers.UserSubscription.TempUserSubscriptionId : 0;

            var tempSubscrition = UnitOfWork.TempUserSubscriptionRepository.GetSingle(i => i.Id == tempusersubscriptionID && i.isCreatedbyOwner == false);

            if (dbUsers.UserSubscription.IsAdditionalPet == true)
            {
                PlanRenewalViewModel model1 = new PlanRenewalViewModel();

                var priceInfo = UnitOfWork.SubscriptionRepository.GetSingle(s => s.Id == dbUsers.UserSubscription.SubscriptionId);

                model1.PlanID = priceInfo.Id;
                model1.StartDate = dbUsers.UserSubscription.StartDate ?? DateTime.Today;
                model1.Promocode = priceInfo.IsPromotionCode ? priceInfo.PromotionCode : "NA";
                model1.EndDate = dbUsers.UserSubscription.RenewalDate ?? DateTime.Now;
                model1.AdditionalPetRenewal = dbUsers.UserSubscription.AdditionalPetcount ?? 0;
                var Dueration = priceInfo.Duration == 0 ? (int)(model1.EndDate.Subtract(model1.StartDate).TotalDays) + 1 : priceInfo.Duration;
                var RemainingDays = (int)(model1.EndDate - DateTime.Today).TotalDays < 0 ? 0 : (int)(model1.EndDate - DateTime.Today).TotalDays;
                RemainingDays = RemainingDays == 0 ? RemainingDays : RemainingDays + 1;
                model1.Duration = Dueration;
                model1.RemainingDays = RemainingDays;
                var additionalPets = dbUsers.UserSubscription.SubscriptionService != null ? dbUsers.UserSubscription.SubscriptionService.AditionalPetCount ?? 0 : 0;
                var totalAmmount = priceInfo.Amount + (priceInfo.AmmountPerAddionalPet * (model1.AdditionalPetRenewal + additionalPets));
                var totalMra = additionalPets + model1.AdditionalPetRenewal + priceInfo.MRACount;

                var totalPets = additionalPets + model1.AdditionalPetRenewal + priceInfo.MaxPetCount;
                var totalSmo = priceInfo.SMOCount;
                model1.PlanName = usersCon.GetPlanName(priceInfo, totalAmmount ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);
                model1.BasicPlan = priceInfo.Name;
                model1.Description = priceInfo.Description;
                model1.AdditionalInfo = priceInfo.AditionalInfo;

                //if (dbUsers.UserSubscription.StartDate == dbUsers.UserSubscription.StartDate && dbUsers.UserSubscription.RenewalDate == dbUsers.UserSubscription.RenewalDate)
                //{
                model1.Price = (priceInfo.AmmountPerAddionalPet * model1.AdditionalPetRenewal);
                //}
                //else
                //{
                //    model1.Price = priceInfo.Amount + (priceInfo.AmmountPerAddionalPet * model1.AdditionalPetRenewal);
                //}

                Session["Plandetails"] = model1;

                return View("PlanBilling", new PlanBillingViewModel(model1.PlanName, model1.Price ?? 0));
            }
            else if (tempSubscrition != null)
            {
                PlanRenewalViewModel model1 = new PlanRenewalViewModel();

                var priceInfo = UnitOfWork.SubscriptionRepository.GetSingle(s => s.Id == tempSubscrition.SubscriptionId);


                model1.PlanID = priceInfo.Id;
                model1.StartDate = tempSubscrition.StartDate;
                model1.Promocode = priceInfo.IsPromotionCode ? priceInfo.PromotionCode : "NA";
                model1.EndDate = tempSubscrition.RenewalDate ?? DateTime.Now;
                if (model1.EndDate < DateTime.Today)
                {
                    var renewalDate = DateTime.Today;
                    if (priceInfo.PaymentTypeId == PaymentTypeEnum.Yearly)
                    {
                        renewalDate = renewalDate.AddYears(1);
                    }
                    else if (priceInfo.PaymentTypeId == PaymentTypeEnum.Monthly)
                    {
                        renewalDate = renewalDate.AddMonths(1);
                    }
                    else if (priceInfo.PaymentTypeId == PaymentTypeEnum.SaleTransaction || priceInfo.PaymentTypeId == PaymentTypeEnum.ThirdPartyCompany)
                    {
                        //todo: refactor this
                        renewalDate = renewalDate.AddYears(100);
                    }
                    else
                    {
                        renewalDate = renewalDate.AddDays(priceInfo.Duration);
                    }

                    model1.StartDate = DateTime.Today;
                    model1.EndDate = renewalDate.AddDays(-1);
                }
                model1.AdditionalPetRenewal = tempSubscrition.AditionalPetCount ?? 0;
                var Dueration = priceInfo.Duration == 0 ? (int)(model1.EndDate.Subtract(model1.StartDate).TotalDays) + 1 : priceInfo.Duration;
                model1.Duration = Dueration;
                model1.RemainingDays = Dueration;
                var totalAmmount = priceInfo.Amount;
                var totalMra = model1.AdditionalPetRenewal + priceInfo.MRACount;
                var totalPets = model1.AdditionalPetRenewal + priceInfo.MaxPetCount;
                var totalSmo = priceInfo.SMOCount;
                model1.PlanName = priceInfo.Name;// usersCon.GetPlanName(priceInfo, totalAmmount ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);
                model1.BasicPlan = priceInfo.Name;
                model1.Description = priceInfo.Description;
                model1.AdditionalInfo = priceInfo.AditionalInfo;
                model1.MaxPetCount = priceInfo.MaxPetCount;

                if (dbUsers.UserSubscription.StartDate == tempSubscrition.StartDate && dbUsers.UserSubscription.RenewalDate == tempSubscrition.RenewalDate)
                {
                    model1.Price = (priceInfo.AmmountPerAddionalPet * model1.AdditionalPetRenewal);
                }
                else
                {
                    model1.Price = priceInfo.Amount;
                       // + (priceInfo.AmmountPerAddionalPet * model1.AdditionalPetRenewal);
                }
                model1.Price = priceInfo.Amount;
                Session["Plandetails"] = model1;

                return View("PlanBilling", new PlanBillingViewModel(model1.PlanName, model1.Price ?? 0));
            }
            else
            {
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
                else if (DomainHelper.GetDomain() == DomainTypeEnum.Portuguese)
                {
                    model.Country = CountryEnum.BRAZIL;
                }

                model.PlanId = subscriptionObj.Id;
                model.FirstName = dbUsers.FirstName;
                model.Promocode = subscriptionObj.PromotionCode;
                model.MiddleName = dbUsers.MiddleName;
                model.LastName = dbUsers.LastName;
                model.Email = dbUsers.Email;
                model.BirthDate = Convert.ToDateTime(dbUsers.BirthDate, CultureInfo.InvariantCulture);
                model.Gender = dbUsers.GenderId;
                model.TimeZone = dbUsers.TimeZoneId;
                model.PetCount = subscriptionObj.MaxPetCount;
                model.PlanName = subscriptionObj.Name;
                model.Price = subscriptionObj.Amount;
            }

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

            return View("Billing", new BillingViewModel(model));
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

            if (ModelState.IsValid)
            {
                Session["PlanPaymentData"] = model;
                var planDetails = Session["Plandetails"] as PlanRenewalViewModel;

                return PartialView("PlanConfirmation", new PlanConfirmationViewModel(planDetails, model, HttpContext.User.GetUserEmail()));
            }
            throw new Exception("Client Validation Failed!");
        }

        [HttpGet]
        public ActionResult CmcicPlanConfirmation()
        {

            var userId = HttpContext.User.GetUserId();
            var billinginfo = Session["PlanPaymentData"] as PlanBillingViewModel;
            var planInfo = Session["Plandetails"] as PlanRenewalViewModel;
            PaymentResult paymentResult = new PaymentResult
            {
                OrderId = "",
                Success = false,
                TransactionID = "",
                ErrorMessage = "Transaction Failed"
            };

            if (billinginfo != null)
            {
                var confirmationModel = new PlanConfirmationViewModel(planInfo, billinginfo, HttpContext.User.GetUserEmail());

                paymentResult = new PaymentResult
                {
                    OrderId = "",
                    Success = true,
                    TransactionID = ""
                };

                //datetime in the user timezone
                var ActionMade = "";


                var userSubscriptionId = User.GetUserSubscriptionId();
                var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == userSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users, u => u.TempUserSubscription);
                var user = userSubscription.Users.First();

                var maxPets = planInfo.MaxPetCount;
                var startDate = planInfo.StartDate;
                var expirationDate = planInfo.EndDate;
                var planName = planInfo.BasicPlan;

                ActionMade = user.UserSubscription.TempUserSubscription != null ? user.UserSubscription.TempUserSubscription.actionName : "";
                if (userSubscription.IsAdditionalPet == true)
                {
                    var tempdate = user.UserSubscription.TempUserSubscription;
                    var userSubscriptionNew = user.UserSubscription;
                    if (userSubscriptionNew.SubscriptionService == null)
                    {
                        var serviceObj = new SubscriptionService
                        {
                            AditionalPetCount = userSubscriptionNew.AdditionalPetcount
                        };

                        userSubscription.SubscriptionService = serviceObj;
                    }
                    else
                    {
                        if (userSubscription.SubscriptionService.AditionalPetCount == null)
                        {
                            userSubscription.SubscriptionService.AditionalPetCount = userSubscriptionNew.AdditionalPetcount;
                        }
                        else
                        {
                            userSubscription.SubscriptionService.AditionalPetCount += userSubscriptionNew.AdditionalPetcount;
                        }
                    }
                    Model.BillingInformation billingInfo = new BillingInformation
                    {
                        PaymentTypeId = PaymentTypeEnum.Yearly,
                        Address1 = billinginfo.BillingAddress1,
                        Address2 = billinginfo.BillingAddress2,
                        City = billinginfo.BillingCity,
                        CountryId = billinginfo.BillingCountry.Value,
                        StateId = billinginfo.BillingState,
                        Zip = billinginfo.BillingZip.ToString(),
                        CreditCardTypeId = billinginfo.CreditCardType
                    };
                    UnitOfWork.BillingInformationRepository.Insert(billingInfo);
                    UnitOfWork.Save();

                    var paymentHistory = new PaymentHistory
                    {
                        Amount = planInfo.Price ?? 0,
                        PaymentDate = DateTime.Today,
                        TransactionNumber = paymentResult.OrderId,
                        ErrorMessage = paymentResult.ErrorMessage,
                        UserSubscriptionId = userSubscriptionNew.Id,
                        BillingInformationId = billingInfo.Id,
                        BillingInformation = billingInfo
                    };

                    userSubscription.PaymentHistories = new List<PaymentHistory> { paymentHistory };
                    userSubscription.ispaymentDone = true;
                    userSubscription.IsAdditionalPet = false;
                    userSubscription.AdditionalPetcount = null;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
                    UnitOfWork.Save();

                    var maxpet = (userSubscription.Subscription != null) ? userSubscriptionNew.Subscription.MaxPetCount : 0;
                    var maxPetCount = maxpet +
                             (userSubscription.SubscriptionService.AditionalPetCount.HasValue
                                 ? userSubscription.SubscriptionService.AditionalPetCount.Value
                                 : 0);
                    User.UpdateUserMaxPetCount(maxPetCount);
                    ActionMade = "Addpets";
                }



                if (user.UserSubscription.RenewalDate < DateTime.Today)
                {
                    var tempdate = user.UserSubscription.TempUserSubscription;
                    var userSubscriptionNew = user.UserSubscription;

                    var subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(u => u.Id == tempdate.SubscriptionId);


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
                    else
                    {
                        renewalDate = renewalDate.AddDays(subscription.Duration);
                    }

                    userSubscriptionNew.StartDate = DateTime.Today;
                    userSubscriptionNew.RenewalDate = renewalDate.AddDays(-1);

                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();

                    userSubscriptionNew.Subscription = tempdate.Subscription;
                    userSubscriptionNew.SubscriptionId = tempdate.SubscriptionId;

                    if (userSubscriptionNew.SubscriptionService == null)
                    {
                        var serviceObj = new SubscriptionService
                        {
                            AditionalPetCount = tempdate.AditionalPetCount
                        };

                        userSubscriptionNew.SubscriptionService = serviceObj;
                    }
                    else
                    {
                        userSubscriptionNew.SubscriptionService.AditionalPetCount = tempdate.AditionalPetCount;
                    }
                    userSubscriptionNew.TempUserSubscriptionId = null;
                    userSubscriptionNew.TempUserSubscription = null;

                    Model.BillingInformation billingInfo = new BillingInformation
                    {
                        PaymentTypeId = PaymentTypeEnum.Yearly,
                        Address1 = billinginfo.BillingAddress1,
                        Address2 = billinginfo.BillingAddress2,
                        City = billinginfo.BillingCity,
                        CountryId = billinginfo.BillingCountry.Value,
                        StateId = billinginfo.BillingState,
                        Zip = billinginfo.BillingZip.ToString(),
                        CreditCardTypeId = billinginfo.CreditCardType
                    };
                    UnitOfWork.BillingInformationRepository.Insert(billingInfo);
                    UnitOfWork.Save();

                    var paymentHistory = new PaymentHistory
                    {
                        Amount = planInfo.Price ?? 0,
                        PaymentDate = DateTime.Today,
                        TransactionNumber = paymentResult.OrderId,
                        ErrorMessage = paymentResult.ErrorMessage,
                        UserSubscriptionId = userSubscriptionNew.Id,
                        BillingInformationId = billingInfo.Id,
                        BillingInformation = billingInfo
                    };

                    userSubscriptionNew.PaymentHistories = new List<PaymentHistory> { paymentHistory };
                    userSubscriptionNew.ispaymentDone = true;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();

                    var maxpet = (tempdate.Subscription != null) ? tempdate.Subscription.MaxPetCount : 0;
                    var maxPetCount = maxpet +
                             (tempdate.AditionalPetCount.HasValue
                                 ? tempdate.AditionalPetCount.Value
                                 : 0);
                    User.UpdateUserMaxPetCount(maxPetCount);
                    HttpContext.User.UpdateIsPlanExpired();

                }
                else
                {
                    var tempdate = user.UserSubscription.TempUserSubscription;
                    var userSubscriptionNew = user.UserSubscription;
                    Model.BillingInformation billingInfo = new BillingInformation
                    {
                        PaymentTypeId = PaymentTypeEnum.Yearly,
                        Address1 = billinginfo.BillingAddress1,
                        Address2 = billinginfo.BillingAddress2,
                        City = billinginfo.BillingCity,
                        CountryId = billinginfo.BillingCountry.Value,
                        StateId = billinginfo.BillingState,
                        Zip = billinginfo.BillingZip.ToString(),
                        CreditCardTypeId = billinginfo.CreditCardType
                    };
                    UnitOfWork.BillingInformationRepository.Insert(billingInfo);
                    UnitOfWork.Save();

                    var paymentHistory = new PaymentHistory
                    {
                        Amount = planInfo.Price ?? 0,
                        PaymentDate = DateTime.Today,
                        TransactionNumber = paymentResult.OrderId,
                        ErrorMessage = paymentResult.ErrorMessage,
                        UserSubscriptionId = userSubscriptionNew.Id,
                        BillingInformationId = billingInfo.Id,
                        BillingInformation = billingInfo
                    };

                    userSubscriptionNew.PaymentHistories = new List<PaymentHistory> { paymentHistory };
                    userSubscriptionNew.ispaymentDone = true;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();
                }


                EmailSender.SendMyPlanConfirmationMail(user.Email, user.FirstName, user.LastName, startDate, expirationDate, planName, confirmationModel.Promocode, confirmationModel.AdditionalPets.ToString(), DomainHelper.GetCurrency() + confirmationModel.Price, paymentResult.OrderId, confirmationModel.BillingAddress1, confirmationModel.BillingAddress2,
                        confirmationModel.BillingCity, EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingState), confirmationModel.BillingZip.HasValue ? confirmationModel.BillingZip.Value.ToString() : null,
                        EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingCountry), user.Email);
                EmailSender.SendToSupportPlanChange(user.FirstName, userId.ToString(), user.LastName, paymentResult.OrderId, confirmationModel.Promocode, planName, confirmationModel.AdditionalPets.ToString(), startDate, expirationDate, DomainHelper.GetCurrency() + confirmationModel.Price, paymentResult.TransactionResult);

                HttpContext.User.UpdateIsPaymentDone();

                Session["Plandetails"] = null;
                Session["PlanPaymentData"] = null;
                return View("CmcicPlanConfirmation", new ADOPets.Web.ViewModels.Profile.PaymentResultViewModel(paymentResult, confirmationModel.PlanName, DateTime.Now, ActionMade));
            }

            return View("CmcicPlanConfirmation", new ADOPets.Web.ViewModels.Profile.PaymentResultViewModel(paymentResult, "", DateTime.Now, ""));


        }

        [HttpGet]
        public ActionResult CmcicPlanError()
        {
            PaymentResult paymentResult = new PaymentResult
            {
                OrderId = "",
                Success = false,
                TransactionID = "",
                ErrorMessage = "Transaction Failed"
            };
            return View("CmcicPlanConfirmation", new ADOPets.Web.ViewModels.Profile.PaymentResultViewModel(paymentResult, "", DateTime.Now, ""));
        }

        [HttpPost]
        public ActionResult PlanConfirmation()
        {

            var userId = HttpContext.User.GetUserId();

            var billinginfo = Session["PlanPaymentData"] as PlanBillingViewModel;
            var planInfo = Session["Plandetails"] as PlanRenewalViewModel;
            var confirmationModel = new PlanConfirmationViewModel(planInfo, billinginfo, HttpContext.User.GetUserEmail());

            PaymentResult paymentResult;

            if (WebConfigHelper.DoFakePayment)
            {
                paymentResult = PaymentHelper.FakePaymentForUpgradePlan(confirmationModel);
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)  //For the Momonet Fake Payment for IN Domain
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
            var ActionMade = "";
            if (paymentResult.Success)
            {

                var userSubscriptionId = User.GetUserSubscriptionId();
                var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == userSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users, u => u.TempUserSubscription);
                var user = userSubscription.Users.First();

                var maxPets = planInfo.MaxPetCount;
                var startDate = planInfo.StartDate;
                var expirationDate = planInfo.EndDate;
                var planName = planInfo.BasicPlan;

                ActionMade = user.UserSubscription.TempUserSubscription != null ? user.UserSubscription.TempUserSubscription.actionName : "";
                if (userSubscription.IsAdditionalPet == true)
                {
                    var tempdate = user.UserSubscription.TempUserSubscription;
                    var userSubscriptionNew = user.UserSubscription;
                    if (userSubscriptionNew.SubscriptionService == null)
                    {
                        var serviceObj = new SubscriptionService
                        {
                            AditionalPetCount = userSubscriptionNew.AdditionalPetcount
                        };

                        userSubscription.SubscriptionService = serviceObj;
                    }
                    else
                    {
                        if (userSubscription.SubscriptionService.AditionalPetCount == null)
                        {
                            userSubscription.SubscriptionService.AditionalPetCount = userSubscriptionNew.AdditionalPetcount;
                        }
                        else
                        {
                            userSubscription.SubscriptionService.AditionalPetCount += userSubscriptionNew.AdditionalPetcount;
                        }
                    }
                    Model.BillingInformation billingInfo = new BillingInformation
                    {
                        PaymentTypeId = PaymentTypeEnum.Yearly,
                        Address1 = billinginfo.BillingAddress1,
                        Address2 = billinginfo.BillingAddress2,
                        City = billinginfo.BillingCity,
                        CountryId = billinginfo.BillingCountry.Value,
                        StateId = billinginfo.BillingState,
                        Zip = billinginfo.BillingZip.ToString(),
                        CreditCardTypeId = billinginfo.CreditCardType
                    };
                    UnitOfWork.BillingInformationRepository.Insert(billingInfo);
                    UnitOfWork.Save();

                    var paymentHistory = new PaymentHistory
                    {
                        Amount = planInfo.Price ?? 0,
                        PaymentDate = DateTime.Today,
                        TransactionNumber = paymentResult.OrderId,
                        ErrorMessage = paymentResult.ErrorMessage,
                        UserSubscriptionId = userSubscriptionNew.Id,
                        BillingInformationId = billingInfo.Id,
                        BillingInformation = billingInfo
                    };

                    userSubscription.PaymentHistories = new List<PaymentHistory> { paymentHistory };
                    userSubscription.ispaymentDone = true;
                    userSubscription.IsAdditionalPet = false;
                    userSubscription.AdditionalPetcount = null;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
                    UnitOfWork.Save();

                    var maxpet = (userSubscription.Subscription != null) ? userSubscriptionNew.Subscription.MaxPetCount : 0;
                    var maxPetCount = maxpet +
                             (userSubscription.SubscriptionService.AditionalPetCount.HasValue
                                 ? userSubscription.SubscriptionService.AditionalPetCount.Value
                                 : 0);
                    User.UpdateUserMaxPetCount(maxPetCount);
                    ActionMade = "Addpets";
                }



                if (user.UserSubscription.Subscription.Amount == null && user.UserSubscription.Subscription.PlanTypeId == PlanTypeEnum.BasicFree && user.UserSubscription.Subscription.IsBase )
                {
                    var tempdate = user.UserSubscription.TempUserSubscription;
                    var userSubscriptionNew = user.UserSubscription;

                    var subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(u => u.Id == tempdate.SubscriptionId);


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
                    else
                    {
                        renewalDate = renewalDate.AddDays(subscription.Duration);
                    }

                    userSubscriptionNew.StartDate = DateTime.Today;
                    userSubscriptionNew.RenewalDate = renewalDate.AddDays(-1);

                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();

                    userSubscriptionNew.Subscription = tempdate.Subscription;
                    userSubscriptionNew.SubscriptionId = tempdate.SubscriptionId;

                    if (userSubscriptionNew.SubscriptionService == null)
                    {
                        var serviceObj = new SubscriptionService
                        {
                            AditionalPetCount = tempdate.AditionalPetCount
                        };

                        userSubscriptionNew.SubscriptionService = serviceObj;
                    }
                    else
                    {
                        userSubscriptionNew.SubscriptionService.AditionalPetCount = tempdate.AditionalPetCount;
                    }
                    userSubscriptionNew.TempUserSubscriptionId = null;
                    userSubscriptionNew.TempUserSubscription = null;

                    Model.BillingInformation billingInfo = new BillingInformation
                    {
                        PaymentTypeId = PaymentTypeEnum.Yearly,
                        Address1 = billinginfo.BillingAddress1,
                        Address2 = billinginfo.BillingAddress2,
                        City = billinginfo.BillingCity,
                        CountryId = billinginfo.BillingCountry.Value,
                        StateId = billinginfo.BillingState,
                        Zip = billinginfo.BillingZip.ToString(),
                        CreditCardTypeId = billinginfo.CreditCardType
                    };
                    UnitOfWork.BillingInformationRepository.Insert(billingInfo);
                    UnitOfWork.Save();

                    var paymentHistory = new PaymentHistory
                    {
                        Amount = planInfo.Price ?? 0,
                        PaymentDate = DateTime.Today,
                        TransactionNumber = paymentResult.OrderId,
                        ErrorMessage = paymentResult.ErrorMessage,
                        UserSubscriptionId = userSubscriptionNew.Id,
                        BillingInformationId = billingInfo.Id,
                        BillingInformation = billingInfo
                    };

                    userSubscriptionNew.PaymentHistories = new List<PaymentHistory> { paymentHistory };
                    userSubscriptionNew.ispaymentDone = true;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();

                    var maxpet = (tempdate.Subscription != null) ? tempdate.Subscription.MaxPetCount : 0;
                    var maxPetCount = maxpet +
                             (tempdate.AditionalPetCount.HasValue
                                 ? tempdate.AditionalPetCount.Value
                                 : 0);
                    User.UpdateUserMaxPetCount(maxPetCount);
                    HttpContext.User.UpdateIsPlanExpired();

                }
                else
                {
                    var tempdate = user.UserSubscription.TempUserSubscription;
                    var userSubscriptionNew = user.UserSubscription;
                    Model.BillingInformation billingInfo = new BillingInformation
                    {
                        PaymentTypeId = PaymentTypeEnum.Yearly,
                        Address1 = billinginfo.BillingAddress1,
                        Address2 = billinginfo.BillingAddress2,
                        City = billinginfo.BillingCity,
                        CountryId = billinginfo.BillingCountry.Value,
                        StateId = billinginfo.BillingState,
                        Zip = billinginfo.BillingZip.ToString(),
                        CreditCardTypeId = billinginfo.CreditCardType
                    };
                    UnitOfWork.BillingInformationRepository.Insert(billingInfo);
                    UnitOfWork.Save();

                    var paymentHistory = new PaymentHistory
                    {
                        Amount = planInfo.Price ?? 0,
                        PaymentDate = DateTime.Today,
                        TransactionNumber = paymentResult.OrderId,
                        ErrorMessage = paymentResult.ErrorMessage,
                        UserSubscriptionId = userSubscriptionNew.Id,
                        BillingInformationId = billingInfo.Id,
                        BillingInformation = billingInfo
                    };

                    userSubscriptionNew.PaymentHistories = new List<PaymentHistory> { paymentHistory };
                    userSubscriptionNew.ispaymentDone = true;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();
                }

                if (user.UserSubscription.RenewalDate < DateTime.Today)
                {
                    var tempdate = user.UserSubscription.TempUserSubscription;
                    var userSubscriptionNew = user.UserSubscription;

                    var subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(u => u.Id == tempdate.SubscriptionId);


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
                    else
                    {
                        renewalDate = renewalDate.AddDays(subscription.Duration);
                    }

                    userSubscriptionNew.StartDate = DateTime.Today;
                    userSubscriptionNew.RenewalDate = renewalDate.AddDays(-1);

                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();

                    userSubscriptionNew.Subscription = tempdate.Subscription;
                    userSubscriptionNew.SubscriptionId = tempdate.SubscriptionId;

                    if (userSubscriptionNew.SubscriptionService == null)
                    {
                        var serviceObj = new SubscriptionService
                        {
                            AditionalPetCount = tempdate.AditionalPetCount
                        };

                        userSubscriptionNew.SubscriptionService = serviceObj;
                    }
                    else
                    {
                        userSubscriptionNew.SubscriptionService.AditionalPetCount = tempdate.AditionalPetCount;
                    }
                    userSubscriptionNew.TempUserSubscriptionId = null;
                    userSubscriptionNew.TempUserSubscription = null;

                    Model.BillingInformation billingInfo = new BillingInformation
                    {
                        PaymentTypeId = PaymentTypeEnum.Yearly,
                        Address1 = billinginfo.BillingAddress1,
                        Address2 = billinginfo.BillingAddress2,
                        City = billinginfo.BillingCity,
                        CountryId = billinginfo.BillingCountry.Value,
                        StateId = billinginfo.BillingState,
                        Zip = billinginfo.BillingZip.ToString(),
                        CreditCardTypeId = billinginfo.CreditCardType
                    };
                    UnitOfWork.BillingInformationRepository.Insert(billingInfo);
                    UnitOfWork.Save();

                    var paymentHistory = new PaymentHistory
                    {
                        Amount = planInfo.Price ?? 0,
                        PaymentDate = DateTime.Today,
                        TransactionNumber = paymentResult.OrderId,
                        ErrorMessage = paymentResult.ErrorMessage,
                        UserSubscriptionId = userSubscriptionNew.Id,
                        BillingInformationId = billingInfo.Id,
                        BillingInformation = billingInfo
                    };

                    userSubscriptionNew.PaymentHistories = new List<PaymentHistory> { paymentHistory };
                    userSubscriptionNew.ispaymentDone = true;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();

                    var maxpet = (tempdate.Subscription != null) ? tempdate.Subscription.MaxPetCount : 0;
                    var maxPetCount = maxpet +
                             (tempdate.AditionalPetCount.HasValue
                                 ? tempdate.AditionalPetCount.Value
                                 : 0);
                    User.UpdateUserMaxPetCount(maxPetCount);
                    HttpContext.User.UpdateIsPlanExpired();

                }

                EmailSender.SendMyPlanConfirmationMail(user.Email, user.FirstName, user.LastName, startDate, expirationDate, planName, confirmationModel.Promocode, confirmationModel.AdditionalPets.ToString(), DomainHelper.GetCurrency() + confirmationModel.Price, paymentResult.OrderId, confirmationModel.BillingAddress1, confirmationModel.BillingAddress2,
                        confirmationModel.BillingCity, EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingState), confirmationModel.BillingZip.HasValue ? confirmationModel.BillingZip.Value.ToString() : null,
                        EnumHelper.GetResourceValueForEnumValue(confirmationModel.BillingCountry), user.Email);
                EmailSender.SendToSupportPlanChange(user.FirstName, userId.ToString(), user.LastName, paymentResult.OrderId, confirmationModel.Promocode, planName, confirmationModel.AdditionalPets.ToString(), startDate, expirationDate, DomainHelper.GetCurrency() + confirmationModel.Price, paymentResult.TransactionResult);

                HttpContext.User.UpdateIsPaymentDone();

                Session["Plandetails"] = null;
                Session["PlanPaymentData"] = null;
            }

            return PartialView("PlanPaymentResult", new ADOPets.Web.ViewModels.Profile.PaymentResultViewModel(paymentResult, confirmationModel.PlanName, DateTime.Now, ActionMade));
        }

        [HttpPost]
        public ActionResult Confirmation()
        {
            var userId = HttpContext.User.GetUserId();

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

                Expression<Func<User, object>> parames1 = v => v.Veterinarians;
                Expression<Func<User, object>> parames2 = v => v.UserSubscription;
                Expression<Func<User, object>>[] paramesArray = new Expression<Func<User, object>>[] { parames1, parames2 };
                var dbUsers = UnitOfWork.UsersRepository.GetSingleTracking(u => u.Id == userId && u.UserStatusId == UserStatusEnum.Active, navigationProperties: paramesArray);//.Where(u => u.UserStatusId == UserStatusEnum.Active);//.Select(u => new ViewModels.Users.IndexViewModel(u, u.Veterinarians.FirstOrDefault()));
                //var usr = model.Map(dbUsers, paymentResult);

                Model.BillingInformation billingInfo = new BillingInformation
                {
                    PaymentTypeId = PaymentTypeEnum.Yearly,
                    Address1 = model.BillingAddress1,
                    Address2 = model.BillingAddress2,
                    City = model.BillingCity,
                    CountryId = model.BillingCountry.Value,
                    StateId = model.BillingState,
                    Zip = model.BillingZip.ToString(),
                    CreditCardTypeId = model.CreditCardType
                };
                UnitOfWork.BillingInformationRepository.Insert(billingInfo);
                UnitOfWork.Save();

                var paymentHistory = new PaymentHistory
                {
                    Amount = model.Price,
                    PaymentDate = DateTime.Today,
                    TransactionNumber = paymentResult.OrderId,
                    ErrorMessage = paymentResult.ErrorMessage,
                    UserSubscriptionId = dbUsers.UserSubscriptionId,
                    BillingInformationId = billingInfo.Id,
                    BillingInformation = billingInfo
                };

                dbUsers.GeneralConditions = false;

                dbUsers.UserSubscription.PaymentHistories = new List<PaymentHistory> { paymentHistory };
                dbUsers.UserSubscription.ispaymentDone = true;
                UnitOfWork.UserRepository.Update(dbUsers);
                UnitOfWork.Save();

                EmailSender.SendSubscriptionMail(model.Email, model.FirstName, model.LastName, userDateTime, dbUsers.UserSubscription.RenewalDate.Value, model.Plan, model.Promocode, paymentResult.OrderId, model.BillingAddress1, model.BillingAddress2,
                        model.BillingCity, EnumHelper.GetResourceValueForEnumValue(model.BillingState), model.BillingZip.HasValue ? model.BillingZip.Value.ToString() : null,
                        EnumHelper.GetResourceValueForEnumValue(model.BillingCountry), model.Email);
                EmailSender.SendSupportUserSubscription(model.FirstName, model.LastName, model.Email, model.Promocode, model.Plan, amount, model.ReferralName);

                HttpContext.User.UpdateIsPaymentDone();

                Session["PaymentData"] = null;
                Session["UserProfile"] = null;
            }

            return PartialView("_PaymentResult", new ADOPets.Web.ViewModels.Account.PaymentResultViewModel(paymentResult, model.Plan, userDateTime));
        }

        [HttpGet]
        public ActionResult CmcicAccountConfirmation()
        {
            var userId = HttpContext.User.GetUserId();

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
                paymentResult = new PaymentResult
                 {
                     OrderId = "",
                     Success = true,
                     TransactionID = ""
                 };

                var model = new ConfirmationViewModel(userInfo, billinginfo);

                //datetime in the user timezone
                var timeZoneInfoId = UnitOfWork.TimeZoneRepository.GetTimeZoneInfoId(model.TimeZone.Value);
                var userDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, timeZoneInfoId);


                var subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == userInfo.PlanId);
                var amount = model.Price.ToString(CultureInfo.CurrentCulture);

                Expression<Func<User, object>> parames1 = v => v.Veterinarians;
                Expression<Func<User, object>> parames2 = v => v.UserSubscription;
                Expression<Func<User, object>>[] paramesArray = new Expression<Func<User, object>>[] { parames1, parames2 };
                var dbUsers = UnitOfWork.UsersRepository.GetSingleTracking(u => u.Id == userId && u.UserStatusId == UserStatusEnum.Active, navigationProperties: paramesArray);//.Where(u => u.UserStatusId == UserStatusEnum.Active);//.Select(u => new ViewModels.Users.IndexViewModel(u, u.Veterinarians.FirstOrDefault()));
                //var usr = model.Map(dbUsers, paymentResult);

                Model.BillingInformation billingInfo = new BillingInformation
                {
                    PaymentTypeId = PaymentTypeEnum.Yearly,
                    Address1 = model.BillingAddress1,
                    Address2 = model.BillingAddress2,
                    City = model.BillingCity,
                    CountryId = model.BillingCountry.Value,
                    StateId = model.BillingState,
                    Zip = model.BillingZip.ToString(),
                    CreditCardTypeId = model.CreditCardType
                };
                UnitOfWork.BillingInformationRepository.Insert(billingInfo);
                UnitOfWork.Save();

                var paymentHistory = new PaymentHistory
                {
                    Amount = model.Price,
                    PaymentDate = DateTime.Today,
                    TransactionNumber = paymentResult.OrderId,
                    ErrorMessage = paymentResult.ErrorMessage,
                    UserSubscriptionId = dbUsers.UserSubscriptionId,
                    BillingInformationId = billingInfo.Id,
                    BillingInformation = billingInfo
                };

                dbUsers.GeneralConditions = false;

                dbUsers.UserSubscription.PaymentHistories = new List<PaymentHistory> { paymentHistory };
                dbUsers.UserSubscription.ispaymentDone = true;
                UnitOfWork.UserRepository.Update(dbUsers);
                UnitOfWork.Save();

                EmailSender.SendSubscriptionMail(model.Email, model.FirstName, model.LastName, userDateTime, dbUsers.UserSubscription.RenewalDate.Value, model.Plan, model.Promocode, paymentResult.OrderId, model.BillingAddress1, model.BillingAddress2,
                        model.BillingCity, EnumHelper.GetResourceValueForEnumValue(model.BillingState), model.BillingZip.HasValue ? model.BillingZip.Value.ToString() : null,
                        EnumHelper.GetResourceValueForEnumValue(model.BillingCountry), model.Email);
                EmailSender.SendSupportUserSubscription(model.FirstName, model.LastName, model.Email, model.Promocode, model.Plan, amount, model.ReferralName);

                HttpContext.User.UpdateIsPaymentDone();

                Session["PaymentData"] = null;
                Session["UserProfile"] = null;



                Session["PaymentData"] = null;
                Session["UserProfile"] = null;

                return View("CmcicAccountConfirmation", new ViewModels.Account.PaymentResultViewModel(paymentResult, model.Plan, userDateTime));
            }

            return View("CmcicAccountConfirmation", new ViewModels.Account.PaymentResultViewModel(paymentResult, "", DateTime.Now));
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

                return View("CmcicAccountConfirmation", new ViewModels.Account.PaymentResultViewModel(paymentResult, model.Plan, userDateTime));
            }
            return View("CmcicAccountConfirmation", new ViewModels.Account.PaymentResultViewModel(paymentResult, "", DateTime.Today));
        }

        public ActionResult CancelPlan()
        {
            var userId = HttpContext.User.GetUserId();
            var dbUsers = UnitOfWork.UsersRepository.GetSingle(u => u.Id == userId && u.UserStatusId == UserStatusEnum.Active);
            var DelUserSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(s => s.Id == dbUsers.UserSubscriptionId);
            DelUserSubscription.IsAdditionalPet = false;
            DelUserSubscription.AdditionalPetcount = null;
            DelUserSubscription.TempUserSubscriptionId = null;
            DelUserSubscription.TempUserSubscription = null;
            DelUserSubscription.ispaymentDone = true;

            UnitOfWork.UserSubscriptionRepository.Update(DelUserSubscription);
            UnitOfWork.Save();

            HttpContext.User.UpdateIsPaymentDone();

            return RedirectToAction("Index", "Pet");
        }
    }
}