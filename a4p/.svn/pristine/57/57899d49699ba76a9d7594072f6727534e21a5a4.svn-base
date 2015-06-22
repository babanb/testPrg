using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.ViewModels.Consultation;
using Model;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class ConsultationController : BaseController
    {
        public ActionResult Index(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var consultations = UnitOfWork.PetConsultationRepository.GetAll(c => c.PetId == petId && !c.IsDeleted).OrderByDescending(c => c.VisitDate).Select(c => new IndexViewModel(c));

            ViewBag.PetId = petId;

            return PartialView("_Index", consultations);
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
                UnitOfWork.PetConsultationRepository.Insert(model.Map());
                UnitOfWork.Save();
                Success(Resources.Wording.Consultation_Add_AddSuccessfullMessage);
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
            var consultations = UnitOfWork.PetConsultationRepository.GetAll(c => c.PetId == petId && !c.IsDeleted).OrderByDescending(c => c.VisitDate).Select(c => new IndexViewModel(c));

            ViewBag.PetId = petId;

            return PartialView("_List", consultations);
        }

        [HttpGet]
        public ActionResult Edit(int consultationId)
        {
            var consultation = UnitOfWork.PetConsultationRepository.GetSingle(c => c.Id == consultationId);

            return PartialView("_Edit", new EditViewModel(consultation));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var consultation = UnitOfWork.PetConsultationRepository.GetSingle(c => c.Id == model.Id);

                model.Map(consultation);

                UnitOfWork.PetConsultationRepository.Update(consultation);
                UnitOfWork.Save();
                Success(Resources.Wording.Consultation_Edit_EditSuccessMessage);
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int consultationId, int petId)
        {
            var consultation = UnitOfWork.PetConsultationRepository.GetSingle(c => c.Id == consultationId);

            ViewBag.PetId = petId;

            return PartialView("_Delete", new DeleteViewModel(consultation));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var consultation = UnitOfWork.PetConsultationRepository.GetSingle(c => c.Id == id);
            //UnitOfWork.PetConsultationRepository.Delete(consultation);
            consultation.IsDeleted = true;
            UnitOfWork.PetConsultationRepository.Update(consultation);
            UnitOfWork.Save();
            Success(Resources.Wording.Consultation_Delete_DeleteSuccessMessage);
            return RedirectToAction("List", new { petId });
        }
    }
}
