using ADOPets.Web.Common.Authentication;
using Model;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADOPets.Web.ViewModels.Center;

namespace ADOPets.Web.Controllers
{
    public class CenterController : BaseController
    {
        //
        // GET: /Center/
        [UserRoleAuthorize(UserTypeEnum.Admin)]
        public ActionResult Index()
        {
            var listCenter = UnitOfWork.CenterRepository.GetAll(s => !s.IsDeleted, navigationProperties: s => s.Subscriptions).OrderByDescending(p => p.Id).Select(p => new IndexViewModel(p, p.Subscriptions.FirstOrDefault()));
            ViewBag.CenterList = listCenter.GroupBy(o => o.CenterName).Select(g => g.First()).Select(i => new SelectListItem { Text = i.CenterName, Value = i.CenterID.ToString() }).OrderBy(a => a.Text);
            return View(listCenter);
        }

        [HttpGet]
        public ActionResult AddCenter()
        {
            return View("_AddCenter");
        }

        [HttpPost]
        public ActionResult AddCenter(AddViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                Center center = new Center();
                center.CenterName = model.CenterName;
     
                UnitOfWork.CenterRepository.Insert(center);
                UnitOfWork.Save();
            //    var CenterId = UnitOfWork.CenterRepository.GetSingle(a => a.CenterName==model.CenterName).Id;
                
                Subscription sub = new Subscription();

                sub.CenterId = center.Id;
                sub.PromotionCode = model.PromoCode;
                sub.IsPromotionCode = true;
                sub.MaxContactCount = 0;
                sub.MaxPetCount = 0;
                sub.LanguageId = Model.LanguageEnum.English;
                UnitOfWork.SubscriptionRepository.Insert(sub);
                UnitOfWork.Save();

            }
            Success(Resources.Wording.Center_Save_SaveSuccessMessage);
            return RedirectToAction("Index");
        }

    [HttpGet]
        public ActionResult EditCenter(int CenterID)
        {
            var cen = UnitOfWork.CenterRepository.GetSingle(c => c.Id == CenterID);
            var sub = UnitOfWork.SubscriptionRepository.GetSingle(c => c.CenterId == CenterID);
            EditViewModel model = new EditViewModel(cen,sub);
            

            return View("_EditCenter",model);
        }
    [HttpPost]
    public ActionResult EditCenter(EditViewModel model)
    {
        if (ModelState.IsValid)
        {
            //var centerId = model.CenterID;
            var UpdateCenter = UnitOfWork.CenterRepository.GetSingleTracking(g => g.Id == model.CenterID);
            UpdateCenter.CenterName = model.CenterName;
            UnitOfWork.CenterRepository.Update(UpdateCenter);
            UnitOfWork.Save();

            Subscription sub = new Subscription();

            //sub.CenterId = cen.Id;
            var SubCenter = UnitOfWork.SubscriptionRepository.GetSingleTracking(g => g.CenterId == model.CenterID);
            SubCenter.PromotionCode = model.PromoCode;
            SubCenter.IsPromotionCode = true;
            SubCenter.MaxContactCount = 0;
            SubCenter.MaxPetCount = 0;
            SubCenter.LanguageId = Model.LanguageEnum.English;
            UnitOfWork.SubscriptionRepository.Update(SubCenter);
            UnitOfWork.Save();
        }

        Success(Resources.Wording.Center_Edit_EditSuccessMessage);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public ActionResult DeleteCenter(int CenterID)
    {
        var center = UnitOfWork.CenterRepository.GetAll(s => s.Id == CenterID, navigationProperties: s => s.Subscriptions).FirstOrDefault();
       // var Sub = UnitOfWork.SubscriptionRepository.GetSingleTracking(a => a.CenterId == CenterID);
        DeleteViewModel centerModel = new DeleteViewModel();
        centerModel.Id = center.Id;
        centerModel.Promocode = center.Subscriptions.FirstOrDefault().PromotionCode;
        centerModel.CenterName = center.CenterName;
        return PartialView("_DeleteCenter", centerModel);
    }

    [HttpPost]
    public ActionResult DeleteCenter(DeleteViewModel model)
    {
        if (ModelState.IsValid)
        {
            var UpdateCenter = UnitOfWork.CenterRepository.GetSingleTracking(g => g.Id == model.Id);
            UpdateCenter.IsDeleted = true;
            UnitOfWork.CenterRepository.Update(UpdateCenter);
            UnitOfWork.Save();

            var subCenter = UnitOfWork.SubscriptionRepository.GetSingleTracking(g => g.CenterId == model.Id);
            subCenter.IsDeleted = true;
            UnitOfWork.SubscriptionRepository.Update(subCenter);
            UnitOfWork.Save();
            // success msg
            Success(Resources.Wording.Center_Delete_DeleteSuccessMessage);
            return RedirectToAction("Index");
        }
        else {
            var center = UnitOfWork.CenterRepository.GetAll(s => s.Id == model.Id, navigationProperties: s => s.Subscriptions).FirstOrDefault();
            DeleteViewModel centerModel = new DeleteViewModel();
            centerModel.Id = center.Id;
            centerModel.Promocode = center.Subscriptions.FirstOrDefault().PromotionCode;
            centerModel.CenterName = center.CenterName;
            return PartialView("_DeleteCenter", centerModel);
        }
    }
    
    }

}
