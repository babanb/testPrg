using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using Model;
using ADOPets.Web.ViewModels.Condition;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class ConditionController : BaseController
    {
        public ActionResult Index(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;

            var conditions = UnitOfWork.PetConditionRepository.GetAll(c => c.PetId == petId && c.IsDeleted==false).OrderByDescending(c => c.VisitDate).Select(c => new IndexViewModel(c));

            ViewBag.PetId = petId;

            return PartialView("_Index", conditions);
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
                UnitOfWork.PetConditionRepository.Insert(model.Map());
                UnitOfWork.Save();
                Success(Resources.Wording.Condition_Add_AddSuccessfullMessage);
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
            var conditions = UnitOfWork.PetConditionRepository.GetAll(c => c.PetId == petId && c.IsDeleted == false).OrderByDescending(c => c.VisitDate).Select(c => new IndexViewModel(c));

            ViewBag.PetId = petId;

            return PartialView("_List", conditions);
        }

        [HttpGet]
        public ActionResult Edit(int conditionId)
        {
            var condition = UnitOfWork.PetConditionRepository.GetSingle(c => c.Id == conditionId);

            return PartialView("_Edit", new EditViewModel(condition));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var condition = UnitOfWork.PetConditionRepository.GetSingle(c => c.Id == model.Id);

                model.Map(condition);

                UnitOfWork.PetConditionRepository.Update(condition);
                UnitOfWork.Save();
                Success(Resources.Wording.Condition_Edit_EditSuccessMessage);
                return RedirectToAction("List", new {petId = model.PetId});
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int conditionId, int petId)
        {
            var condition = UnitOfWork.PetConditionRepository.GetSingle(c => c.Id == conditionId);

            ViewBag.PetId = petId;

            return PartialView("_Delete", new DeleteViewModel(condition));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var condition = UnitOfWork.PetConditionRepository.GetSingle(c => c.Id == id);
            //UnitOfWork.PetConditionRepository.Delete(condition);
            condition.IsDeleted = true;
            UnitOfWork.PetConditionRepository.Update(condition);
            UnitOfWork.Save();
            Success(Resources.Wording.Condition_Delete_DeleteSuccessMessage);
            return RedirectToAction("List", new { petId });
        }
    }
}
