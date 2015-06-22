using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using Model;
using ADOPets.Web.ViewModels.Surgery;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class SurgeryController : BaseController
    {
        public ActionResult Index(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var surgery = UnitOfWork.PetSurgeryRepository.GetAll(c => c.PetId == petId && !c.IsDeleted).OrderByDescending(c => c.SurgeryDate).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            return PartialView("_Index", surgery);
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
                UnitOfWork.PetSurgeryRepository.Insert(model.Map());
                UnitOfWork.Save();
                Success(Resources.Wording.Surgery_Add_AddSuccessfullMessage);
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
            var surgery = UnitOfWork.PetSurgeryRepository.GetAll(c => c.PetId == petId && !c.IsDeleted).OrderByDescending(c => c.SurgeryDate).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            return PartialView("_List", surgery);
        }


        [HttpGet]
        public ActionResult Edit(int surgeryId)
        {
            var surgery = UnitOfWork.PetSurgeryRepository.GetSingle(c => c.Id == surgeryId);

            return PartialView("_Edit", new EditViewModel(surgery));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var surgery = UnitOfWork.PetSurgeryRepository.GetSingle(c => c.Id == model.Id);
                model.Map(surgery);
                UnitOfWork.PetSurgeryRepository.Update(surgery);
                UnitOfWork.Save();
                Success(Resources.Wording.Surgery_Edit_EditSuccessMessage);
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }



        [HttpGet]
        public ActionResult DeleteConfirm(int surgeryId, int petId)
        {
            var surgery = UnitOfWork.PetSurgeryRepository.GetSingle(c => c.Id == surgeryId);
            ViewBag.PetId = petId;
            return PartialView("_Delete", new DeleteViewModel(surgery));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var surgery = UnitOfWork.PetSurgeryRepository.GetSingle(c => c.Id == id);
            //UnitOfWork.PetSurgeryRepository.Delete(surgery);

            surgery.IsDeleted = true;
            UnitOfWork.PetSurgeryRepository.Update(surgery);
            UnitOfWork.Save();
            Success(Resources.Wording.Surgery_Delete_DeleteSuccessMessage);
            return RedirectToAction("List", new { petId });
        }


    }
}
