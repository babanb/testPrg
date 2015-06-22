using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.ViewModels.Hemoglobin;
using Model;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class HemoglobinController : BaseController
    {
        [HttpGet]
        public ActionResult List(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var measures = UnitOfWork.PetHealthMeasureTrackerRepository.GetAll(m => m.PetId == petId && m.HealthMeasureTypeId == HealthMeasureTypeEnum.Hemoglobin && !m.IsDeleted)
                .OrderByDescending(m => m.MeasuredDate).Select(m => new IndexViewModel(m));

            ViewBag.PetId = petId;

            return PartialView("_List", measures);
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
                UnitOfWork.PetHealthMeasureTrackerRepository.Insert(model.Map());
                UnitOfWork.Save();
                Success(Resources.Wording.Hemoglobin_Add_AddSuccessfullMessage);
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is well, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult Edit(int measureId)
        {
            var measure = UnitOfWork.PetHealthMeasureTrackerRepository.GetSingle(c => c.Id == measureId);

            return PartialView("_Edit", new EditViewModel(measure));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var measure = UnitOfWork.PetHealthMeasureTrackerRepository.GetSingle(c => c.Id == model.Id);

                model.Map(measure);

                UnitOfWork.PetHealthMeasureTrackerRepository.Update(measure);
                UnitOfWork.Save();
                Success(Resources.Wording.Hemoglobin_Edit_EditSuccessMessage);
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int measureId)
        {
            var measure = UnitOfWork.PetHealthMeasureTrackerRepository.GetSingle(c => c.Id == measureId);

            ViewBag.PetId = measure.PetId;

            return PartialView("_Delete", new DeleteViewModel(measure));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var measure = UnitOfWork.PetHealthMeasureTrackerRepository.GetSingle(c => c.Id == id);
            measure.IsDeleted = true;
            UnitOfWork.PetHealthMeasureTrackerRepository.Update(measure);
            UnitOfWork.Save();
            Success(Resources.Wording.Hemoglobin_Delete_DeleteSuccessMessage);
            return RedirectToAction("List", new { petId });
        }
    }
}
