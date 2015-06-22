using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using Model;
using ADOPets.Web.ViewModels.Immunization;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class ImmunizationController : BaseController
    {
        public ActionResult Index(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var immunization = UnitOfWork.PetVaccinationRepository.GetAll(c => c.PetId == petId && !c.IsDeleted).OrderByDescending(c => c.InjectionDate).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            return PartialView("_Index", immunization);
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
                UnitOfWork.PetVaccinationRepository.Insert(model.Map());
                UnitOfWork.Save();
                Success(Resources.Wording.Immunization_Add_AddSuccessMessage);
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
            var immunization = UnitOfWork.PetVaccinationRepository.GetAll(c => c.PetId == petId && !c.IsDeleted).OrderByDescending(c => c.InjectionDate).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            return PartialView("_List", immunization);
        }

        [HttpGet]
        public ActionResult Edit(int immunizationId)
        {
            var immunization = UnitOfWork.PetVaccinationRepository.GetSingle(c => c.Id == immunizationId);
            return PartialView("_Edit", new EditViewModel(immunization));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var immunization = UnitOfWork.PetVaccinationRepository.GetSingle(c => c.Id == model.Id);

                model.Map(immunization);
                UnitOfWork.PetVaccinationRepository.Update(immunization);
                UnitOfWork.Save();
                Success(Resources.Wording.Immunization_Edit_EditSuccessMessage);
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int immunizationId, int petId)
        {
            var immunization = UnitOfWork.PetVaccinationRepository.GetSingle(c => c.Id == immunizationId);
            ViewBag.PetId = petId;
            return PartialView("_Delete", new DeleteViewModel(immunization));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var immunization = UnitOfWork.PetVaccinationRepository.GetSingle(c => c.Id == id);
           // UnitOfWork.PetVaccinationRepository.Delete(immunization);
            immunization.IsDeleted = true;
            UnitOfWork.PetVaccinationRepository.Update(immunization);
            UnitOfWork.Save();
            Success(Resources.Wording.Immunization_Delete_DeleteSuccessMessage);
            return RedirectToAction("List", new { petId });
        }
    }
}
