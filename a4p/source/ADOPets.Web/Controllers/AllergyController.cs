using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using Model;
using ADOPets.Web.ViewModels.Allergy;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class AllergyController : BaseController
    {
        public ActionResult Index(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var allergy = UnitOfWork.PetAllergyRepository.GetAll(c => c.PetId == petId && c.IsDeleted == false).OrderByDescending(c => c.StartDate).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            return PartialView("_Index", allergy);
        }

        [HttpGet]
        public ActionResult List(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var allergy = UnitOfWork.PetAllergyRepository.GetAll(c => c.PetId == petId && c.IsDeleted == false).OrderByDescending(c => c.StartDate).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            return PartialView("_List", allergy);
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
                UnitOfWork.PetAllergyRepository.Insert(model.Map());
                UnitOfWork.Save();
                Success(Resources.Wording.Allergy_Add_AddSuccessfullMessage);
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is well, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult Edit(int allergyId)
        {
            var allergy = UnitOfWork.PetAllergyRepository.GetSingle(c => c.Id == allergyId);
            return PartialView("_Edit", new EditViewModel(allergy));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var allergy = UnitOfWork.PetAllergyRepository.GetSingle(c => c.Id == model.Id);

                model.Map(allergy);
                UnitOfWork.PetAllergyRepository.Update(allergy);
                UnitOfWork.Save();
                Success(Resources.Wording.Allergy_Edit_EditSuccessMessage);
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int allergyId, int petId)
        {
            var allergy = UnitOfWork.PetAllergyRepository.GetSingle(c => c.Id == allergyId);
            ViewBag.PetId = petId;
            return PartialView("_Delete", new DeleteViewModel(allergy));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var allergy = UnitOfWork.PetAllergyRepository.GetSingle(c => c.Id == id);
            // UnitOfWork.PetAllergyRepository.Delete(allergy);
            allergy.IsDeleted = true;
            UnitOfWork.PetAllergyRepository.Update(allergy);
            UnitOfWork.Save();
            Success(Resources.Wording.Allergy_Delete_DeleteSuccessMessage);
            return RedirectToAction("List", new { petId });
        }
    }
}
