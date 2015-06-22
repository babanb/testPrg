using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using Model;
using ADOPets.Web.ViewModels.Insurance;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class InsuranceController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int petId)
        {
            var insurance = UnitOfWork.InsuranceRepository.GetAll(c => c.PetId == petId).OrderByDescending(c => c.Id).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            return PartialView("_Index", insurance);
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
                UnitOfWork.InsuranceRepository.Insert(model.Map());
                UnitOfWork.Save();
                Success(Resources.Wording.Insurance_Add_AddSuccessMessage);
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
            var insurance = UnitOfWork.InsuranceRepository.GetAll(c => c.PetId == petId).OrderByDescending(c => c.Id).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            return PartialView("_List", insurance);
        }

        [HttpGet]
        public ActionResult Edit(int insuranceId)
        {
            var insurance = UnitOfWork.InsuranceRepository.GetSingle(c => c.Id == insuranceId);

            return PartialView("_Edit", new EditViewModel(insurance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var insurance = UnitOfWork.InsuranceRepository.GetSingle(c => c.Id == model.Id);

                model.Map(insurance);

                UnitOfWork.InsuranceRepository.Update(insurance);
                UnitOfWork.Save();
                Success(Resources.Wording.Insurance_Edit_EditSuccessMessage);
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int insuranceId, int petId)
        {
            var insurance = UnitOfWork.InsuranceRepository.GetSingle(c => c.Id == insuranceId);

            ViewBag.PetId = petId;

            return PartialView("_Delete", new DeleteViewModel(insurance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var insurance = UnitOfWork.InsuranceRepository.GetSingle(c => c.Id == id);
            UnitOfWork.InsuranceRepository.Delete(insurance);
            UnitOfWork.Save();
            Success(Resources.Wording.Insurance_Delete_DeleteSuccessMessage);
            return RedirectToAction("List", new { petId });
        }
    }
}
