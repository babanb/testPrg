using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.ViewModels.Medication;
using Model;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class MedicationController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var medications = UnitOfWork.PetMedicationRepository.GetAll(c => c.PetId == petId && !c.IsDeleted).OrderByDescending(c => c.VisitDate).Select(c => new IndexViewModel(c));

            ViewBag.PetId = petId;

            return PartialView("_Index", medications);
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
                UnitOfWork.PetMedicationRepository.Insert(model.Map());
                UnitOfWork.Save();
                Success(Resources.Wording.Medication_Add_AddSuccessMessage);
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
            var medications = UnitOfWork.PetMedicationRepository.GetAll(c => c.PetId == petId && !c.IsDeleted).OrderByDescending(c => c.VisitDate).Select(c => new IndexViewModel(c));

            ViewBag.PetId = petId;

            return PartialView("_List", medications);
        }

        [HttpGet]
        public ActionResult Edit(int medicationId)
        {
            var medication = UnitOfWork.PetMedicationRepository.GetSingle(c => c.Id == medicationId);

            return PartialView("_Edit", new EditViewModel(medication));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var medication = UnitOfWork.PetMedicationRepository.GetSingle(c => c.Id == model.Id);

                model.Map(medication);

                UnitOfWork.PetMedicationRepository.Update(medication);
                UnitOfWork.Save();
                Success(Resources.Wording.Medication_Edit_EditSuccessMessage);
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int medicationId, int petId)
        {
            var medication = UnitOfWork.PetMedicationRepository.GetSingle(c => c.Id == medicationId);

            ViewBag.PetId = petId;

            return PartialView("_Delete", new DeleteViewModel(medication));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var medication = UnitOfWork.PetMedicationRepository.GetSingle(c => c.Id == id);
          //  UnitOfWork.PetMedicationRepository.Delete(medication);
            medication.IsDeleted = true;
            UnitOfWork.PetMedicationRepository.Update(medication);
            UnitOfWork.Save();
            Success(Resources.Wording.Medication_Delete_DeleteSuccessMessage);
            return RedirectToAction("List", new { petId });
        }
    }
}
