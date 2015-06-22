using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using Model;
using ADOPets.Web.ViewModels.Hospitalization;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class HospitalizationController : BaseController
    {
        public ActionResult Index(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var hospitalization = UnitOfWork.PetHospitalizationRepository.GetAll(c => c.PetId == petId && !c.IsDeleted).OrderByDescending(c => c.StartDate).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            return PartialView("_Index", hospitalization);
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
                UnitOfWork.PetHospitalizationRepository.Insert(model.Map());
                UnitOfWork.Save();
                Success(Resources.Wording.Hospitalization_Add_AddSuccessMessage);
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
            var hospitalization = UnitOfWork.PetHospitalizationRepository.GetAll(c => c.PetId == petId && !c.IsDeleted).OrderByDescending(c => c.StartDate).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            return PartialView("_List", hospitalization);
        }

        [HttpGet]
        public ActionResult Edit(int hospitalizationId)
        {
            var hospitalization = UnitOfWork.PetHospitalizationRepository.GetSingle(c => c.Id == hospitalizationId);
            return PartialView("_Edit", new EditViewModel(hospitalization));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hospitalization = UnitOfWork.PetHospitalizationRepository.GetSingle(c => c.Id == model.Id);

                model.Map(hospitalization);
                UnitOfWork.PetHospitalizationRepository.Update(hospitalization);
                UnitOfWork.Save();
                Success(Resources.Wording.Hospitalization_Edit_EditSuccessMessage);
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int hospitalizationId, int petId)
        {
            var hospitalization = UnitOfWork.PetHospitalizationRepository.GetSingle(c => c.Id == hospitalizationId);
            ViewBag.PetId = petId;
            return PartialView("_Delete", new DeleteViewModel(hospitalization));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var hospitalization = UnitOfWork.PetHospitalizationRepository.GetSingle(c => c.Id == id);
           // UnitOfWork.PetHospitalizationRepository.Delete(hospitalization);
            hospitalization.IsDeleted = true;
            UnitOfWork.PetHospitalizationRepository.Update(hospitalization);
            UnitOfWork.Save();
            Success(Resources.Wording.Hospitalization_Delete_DeleteSuccessMessage);
            return RedirectToAction("List", new { petId });
        }
    }
}
