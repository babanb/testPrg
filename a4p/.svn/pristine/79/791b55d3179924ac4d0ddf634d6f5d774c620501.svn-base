using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using ADOPets.Web.ViewModels.FoodPlan;
using ADOPets.Web.Common.Authentication;
using Model;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class FoodPlanController : BaseController
    {
        public ActionResult Index(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var foodplan = UnitOfWork.PetFoodPlanRepository.GetAll(c => c.PetId == petId && !c.IsDeleted).OrderByDescending(c => c.Id).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            return PartialView("_Index", foodplan);
        }

        [HttpGet]
        public ActionResult Add(int petId)
        {
            return PartialView("_Add", new AddViewModel(petId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.PetFoodPlanRepository.Insert(model.Map());
                UnitOfWork.Save();
                Success(Resources.Wording.FoodPlan_Add_AddSuccessfullMessage);
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is well, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult List(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var foodplan = UnitOfWork.PetFoodPlanRepository.GetAll(c => c.PetId == petId && !c.IsDeleted).OrderByDescending(c => c.Id).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            return PartialView("_List", foodplan);
        }

        [HttpGet]
        public ActionResult Edit(int foodplanId)
        {
            var foodplan = UnitOfWork.PetFoodPlanRepository.GetSingle(c => c.Id == foodplanId);
            return PartialView("_Edit", new EditViewModel(foodplan));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var foodplan = UnitOfWork.PetFoodPlanRepository.GetSingle(c => c.Id == model.Id);

                model.Map(foodplan);
                UnitOfWork.PetFoodPlanRepository.Update(foodplan);
                UnitOfWork.Save();
                Success(Resources.Wording.FoodPlan_Edit_EditSuccessMessage);
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int foodplanId, int petId)
        {
            var foodplan = UnitOfWork.PetFoodPlanRepository.GetSingle(c => c.Id == foodplanId);
            ViewBag.PetId = petId;
            return PartialView("_Delete", new DeleteViewModel(foodplan));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var foodplan = UnitOfWork.PetFoodPlanRepository.GetSingle(c => c.Id == id);
            //UnitOfWork.PetFoodPlanRepository.Delete(foodplan);
            foodplan.IsDeleted = true;
            UnitOfWork.PetFoodPlanRepository.Update(foodplan);
            UnitOfWork.Save();
            Success(Resources.Wording.FoodPlan_Delete_DeleteSuccessMessage);
            return RedirectToAction("List", new { petId });
        }
    }
}
