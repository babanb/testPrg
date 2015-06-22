﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using ADOPets.Web.Resources;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.ViewModels.PlansAndPromo;

namespace ADOPets.Web.Controllers
{
    public class PlansAndPromoController : BaseController
    {
        //
        // GET: /PlansAndPromo/
        [UserRoleAuthorize(UserTypeEnum.Admin)]
        public ActionResult Index()
        {
            IEnumerable<Model.Subscription> PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted);
            ViewBag.Plans = PlansNPromocode.GroupBy(o => o.Name).Select(g => g.First()).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
            ViewBag.Promocode = PlansNPromocode.Where(u => u.IsPromotionCode).GroupBy(o => o.PromotionCode).Select(g => g.First()).Select(i => new SelectListItem { Text = i.PromotionCode, Value = i.Id.ToString() }).OrderBy(a => a.Text);
            var plans = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted).OrderByDescending(p => p.Id).Select(p => new IndexViewModel(p, UnitOfWork.SubscriptionRepository.GetSingle(s => s.Id == p.SubscriptionBaseId)));
            return View(plans);
        }

        #region plan
        #region Add plan
        [HttpGet]
        public ActionResult AddPlan()
        {
            return View("_AddPlan");
        }

        [HttpPost]
        public ActionResult AddPlan(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var plan = model.Map();
                UnitOfWork.SubscriptionRepository.Insert(plan);
                UnitOfWork.Save();
                Success(Resources.Wording.PlansAndPromo_Add_SuccessMsg);
                ViewBag.plaID = plan.Id;
                return RedirectToAction("AddPromocode");
            }
            else
            {
                return View("_AddPlan");
            }
        }
        #endregion

        #region Edit plan
        [HttpGet]
        public ActionResult EditPlan(int planId)
        {
            var sub = UnitOfWork.SubscriptionRepository.GetSingle(c => c.Id == planId);

            EditViewModel model = new EditViewModel(sub);
            ViewBag.plaID = sub.Id;

            return View("_EditPlan", model);
        }

        [HttpPost]
        public ActionResult EditPlan(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sub = UnitOfWork.SubscriptionRepository.GetSingle(c => c.Id == model.Id);
                Model.Subscription subObject = model.Map(sub);
                UnitOfWork.SubscriptionRepository.Update(subObject);
                UnitOfWork.Save();
                Success(Resources.Wording.PlansAndPromo_Edit_SuccessMsg);

                return RedirectToAction("Index");
            }
            else
            {
                return View("_AddPlan");
            }
        }

        #endregion

        #region Edit Promocode
        [HttpGet]
        public ActionResult EditPromocode(int planId)
        {
            var sub = UnitOfWork.SubscriptionRepository.GetSingle(c => c.Id == planId);

            EditPromocodeViewModel model = new EditPromocodeViewModel(sub);
            ViewBag.plaID = sub.Id;
            return View("_EditPromocode", model);
        }

        [HttpPost]
        public ActionResult EditPromocode(EditPromocodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sub = UnitOfWork.SubscriptionRepository.GetSingle(m => m.Id == model.PlanID);
                model.Map(sub);
                UnitOfWork.SubscriptionRepository.Update(sub);
                UnitOfWork.Save();
                Success(Resources.Wording.PlansAndPromo_Edit_SuccessMsg);

                return RedirectToAction("Index");
            }
            else
            {
                return View("_EditPromocode");
            }
        }
        #endregion

        #region Delete Plan
        [HttpGet]
        public ActionResult DeleteConfirm(int planId)
        {
            return PartialView("_DeletePlan", new DeleteViewModel(planId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePlan(DeleteViewModel model)
        {
            string message = "";
            var sub = UnitOfWork.SubscriptionRepository.GetSingle(c => c.Id == model.PlanId);
            if (sub != null)
            {
                if (model.IsDeletePlan && model.IsDeletePromocode)
                {
                    UnitOfWork.SubscriptionRepository.Delete(sub);
                    message = Resources.Wording.PlansAndPrmo_Index_Title;
                }
                else if (model.IsDeletePromocode)
                {
                    sub.PromotionCode = null;
                    UnitOfWork.SubscriptionRepository.Update(sub);
                    message = Resources.Wording.PlanAndPromo_Add_PromocodeTitle;

                }
                else
                {
                    //TODO ::  confirm for plan to be deleted or just the plan name should be null
                    UnitOfWork.SubscriptionRepository.Delete(sub);
                    message = Resources.Wording.PlanAndPromo_Add_PlanTitle;
                }
            }

            UnitOfWork.Save();
            string successMsg = string.Format(Resources.Wording.PlansAndPromo_Delete_DeleteSuccessMessage, message);
            Success(successMsg);
            return RedirectToAction("Index");
        }

        #endregion
        #endregion

        #region Promocode

        [HttpGet]
        public ActionResult AddPromocode()
        {
            AddPromocodeViewModel model = new AddPromocodeViewModel();
            model.ListPlans = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted).GroupBy(p=>p.Name).Select(p => new IndexPromoCode(p.First())).OrderByDescending(p=>p.PlanId).ToList();            
            return View("_AddPromocode", model);
        }

        [HttpPost]
        public ActionResult AddPromocode(AddPromocodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ListPlans != null && model.ListPlans.Count() > 0)
                {
                    foreach (IndexPromoCode index in model.ListPlans)
                    {
                        if (index.IsChecked)
                        {
                            Model.Subscription sub = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == index.IsCheckedId);

                            
                            sub.IsVisibleToOwner = model.IsVisibleToOwner;
                            sub.PromotionCode = model.Promocode;                            
                            sub.startDateValide = model.StartDate;
                            sub.EndDateValide = model.EndDate;
                            sub.CompanyName = model.CompanyName;
                            sub.SalesPerson = model.SalesPerson;
                            sub.MaxOwner = model.MaxOwner;

                            if (sub.IsPromotionCode || sub.IsBase)
                            {
                                sub.IsPromotionCode = true;
                                sub.IsBase = false;
                                UnitOfWork.SubscriptionRepository.Insert(sub);
                            }
                            else
                            {
                                sub.IsPromotionCode = true;
                                UnitOfWork.SubscriptionRepository.Update(sub);
                            }
                            
                            UnitOfWork.Save();
                        }
                    }
                    Success(Resources.Wording.PlansAndPromo_AddPromo_SuccessMsg);
                }

                return RedirectToAction("RenewUpgradePlan");
            }
            return RedirectToAction("AddPromocode");
        }

        [HttpPost]
        public ActionResult IsPromocodeExists(string promocode)
        {
            var Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.PromotionCode.ToLower() == promocode.ToLower() && !i.IsDeleted);
            bool isValid = (Plans != null && Plans.Count() > 0) ? false : true;
            return Json(isValid);
        }


        [HttpPost]
        public ActionResult IsPromocodeExistsForEdit(string promocode)
        {
            //  var Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.PromotionCode.ToLower() == promocode.ToLower() && !i.IsDeleted);
            // bool isValid = (Plans != null && Plans.Count() > 0) ? false : true;
            return Json(true);
        }

        #endregion

        #region Renew/Upgrade Plan

        [HttpGet]
        public ActionResult RenewUpgradePlan()
        {
            IEnumerable<Model.Subscription> PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted);
            ViewBag.Plans = PlansNPromocode.GroupBy(o => o.Name).Select(g => g.First()).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
            ViewBag.Promocode = PlansNPromocode.Where(u => u.IsPromotionCode).GroupBy(o => o.PromotionCode).Select(g => g.First()).Select(i => new SelectListItem { Text = i.PromotionCode, Value = i.Id.ToString() }).OrderBy(a => a.Text);

            return View("_RenewUpgradePlan");
        }

        [HttpPost]
        public ActionResult RenewUpgradePlan(PlanRenewUpgradeViewModel model)
        {
            Model.Subscription subBase = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == model.PlanatRenewalId);
            UnitOfWork.SubscriptionRepository.Insert(subBase);
            UnitOfWork.Save();

            Model.Subscription sub = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == model.PlanId);
            sub.SubscriptionBaseId = subBase.Id;
            UnitOfWork.SubscriptionRepository.Update(sub);
            UnitOfWork.Save();

            Model.Subscription basePromocode = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == subBase.Id);
            basePromocode.PromotionCode = sub.PromotionCode;
            UnitOfWork.SubscriptionRepository.Update(basePromocode);
            UnitOfWork.Save();

            Success(Resources.Wording.PlansAndPromo_RenewUpgrade_SuccessMsg);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetPlansByPromocode(string promocode, bool isRenewPlan)
        {
            var userId = HttpContext.User.GetUserId();

            string promocodeVal = "";
            if (!string.IsNullOrEmpty(promocode))
            {
                int subId = Convert.ToInt32(promocode);
                promocodeVal = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == subId && !s.IsDeleted).PromotionCode;
            }

            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription);
            var subscriptionBaseID = user.UserSubscription.Subscription.SubscriptionBaseId;
            object data;
            if (isRenewPlan)
            {
                var defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(i => i.PromotionCode == promocodeVal && i.IsTrial == false && i.IsVisibleToOwner && !i.IsDeleted).OrderBy(u => u.MaxPetCount);
                var isvalid = true;
                if (!defaultPlans.Any())
                {
                    defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(i => i.IsBase && !i.IsPromotionCode && i.IsTrial == false && !i.IsDeleted).OrderBy(u => u.MaxPetCount);
                }
                List<SelectListItem> items = new List<SelectListItem>();

                if (defaultPlans != null && defaultPlans.Count() > 0)
                {
                    items = defaultPlans.Select(i => new SelectListItem { Text = string.Format(Resources.Wording.Account_SignUp_AdditionalPetsInformation, i.Amount, i.MaxPetCount, i.MaxPetCount), Value = (i.Id).ToString() }).ToList();
                }
                else
                {
                    isvalid = false;
                }

                data = new { Items = items, isvalid = isvalid };
            }
            else
            {
                var isvalid = true;
                var Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.PromotionCode == promocodeVal && !i.IsDeleted);
                List<SelectListItem> items = new List<SelectListItem>();

                if (Plans != null && Plans.Count() > 0)
                {
                    items = Plans.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text).ToList();
                }
                else { isvalid = false; }
                data = new { Items = items, isvalid = isvalid };

            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetRenewPlanByPlan(string planId)
        {
            int _planId = Convert.ToInt32(planId);
            var isvalid = true;
            int baseplanId = Convert.ToInt32(UnitOfWork.SubscriptionRepository.GetSingleTracking(sub => sub.Id == _planId).SubscriptionBaseId);
            var Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.Id == baseplanId);
            List<SelectListItem> items = new List<SelectListItem>();
            if (Plans != null && Plans.Count() > 0)
            {
                items = Plans.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text).ToList();
            }
            else { isvalid = false; }
            var data = new { Items = items, isvalid = isvalid };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
