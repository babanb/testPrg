using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.ViewModels.Temperature;
using Model;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class TemperatureController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int petId, HealthMeasureUnitEnum unit)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var measures = UnitOfWork.PetHealthMeasureTrackerRepository.GetAll(m => m.PetId == petId && m.HealthMeasureTypeId == HealthMeasureTypeEnum.Temperature && !m.IsDeleted)
                .OrderByDescending(m => m.MeasuredDate).Select(m => new IndexViewModel(m, unit));

            ViewBag.PetId = petId;
            ViewBag.MeasureUnit = unit;

            return PartialView("_Index", measures);
        }

        [HttpGet]
        public ActionResult Add(int petId, HealthMeasureUnitEnum? unit)
        {
            unit = unit ?? DomainHelper.GetTemperaturetMeasureUnitDefault();

            return PartialView("_Add", new AddViewModel(petId, unit.Value));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.PetHealthMeasureTrackerRepository.Insert(model.Map());
                UnitOfWork.Save();
                Success(Resources.Wording.Temperature_Add_AddSuccessfullMessage);
                return RedirectToAction("Index", new { petId = model.PetId, unit = model.MeasureUnit });
            }
            else
            {
                //if all is well, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult Edit(int measureId, HealthMeasureUnitEnum? unit)
        {
            var measure = UnitOfWork.PetHealthMeasureTrackerRepository.GetSingle(c => c.Id == measureId);

            unit = unit ?? DomainHelper.GetTemperaturetMeasureUnitDefault();

            return PartialView("_Edit", new EditViewModel(measure, unit.Value));
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
                Success(Resources.Wording.Temperature_Edit_EditSuccessMessage);
                return RedirectToAction("Index", new { petId = model.PetId, unit = model.MeasureUnit });
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int measureId, HealthMeasureUnitEnum? unit)
        {
            var measure = UnitOfWork.PetHealthMeasureTrackerRepository.GetSingle(c => c.Id == measureId);

            ViewBag.PetId = measure.PetId;

            unit = unit ?? DomainHelper.GetTemperaturetMeasureUnitDefault();

            return PartialView("_Delete", new DeleteViewModel(measure, unit.Value));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId, HealthMeasureUnitEnum measureUnit)
        {
            var measure = UnitOfWork.PetHealthMeasureTrackerRepository.GetSingle(c => c.Id == id);
            measure.IsDeleted = true;
            UnitOfWork.PetHealthMeasureTrackerRepository.Update(measure);
            UnitOfWork.Save();
            Success(Resources.Wording.Temperature_Delete_DeleteSuccessMessage);
            return RedirectToAction("Index", new { petId, unit = measureUnit });
        }

        [HttpGet]
        public ActionResult SwithMeasureUnit(int petId, HealthMeasureUnitEnum unit)
        {
            return RedirectToAction("Index", new { petId, unit });
        }
    }
}
