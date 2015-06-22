using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ADOPets.Web.Common.Authentication;
using Model;
using ADOPets.Web.ViewModels.PetContact;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class PetContactController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            var petcontact = UnitOfWork.PetContactRepository.GetAll(c => c.PetId == petId).OrderByDescending(c => c.Id).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            ViewBag.UserId = userId;
            return PartialView("_Index", petcontact);
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
                var userId = Roles.IsUserInRole(UserTypeEnum.Admin.ToString())
                    ? HttpContext.User.GetOwnerId()
                    : HttpContext.User.GetUserId();

                if (model.IsEmergencyContact)
                {
                    var pet = UnitOfWork.PetRepository.GetSingle(c => c.Id == model.PetId);
                    UnitOfWork.PetContactRepository.Insert(model.Map(pet.PetTypeId, pet.Id, userId));
                }
                else
                {
                    UnitOfWork.PetContactRepository.Insert(model.Map());
                }

                UnitOfWork.Save();
                Success(Resources.Wording.PetContact_Add_AddSuccessMessage);
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
            var petcontact = UnitOfWork.PetContactRepository.GetAll(c => c.PetId == petId).OrderByDescending(c => c.Id).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            ViewBag.UserId = userId;
            return PartialView("_List", petcontact);
        }

        [HttpGet]
        public ActionResult Edit(int petcontactId)
        {
            var petcontact = UnitOfWork.PetContactRepository.GetSingle(c => c.Id == petcontactId);
            return PartialView("_Edit", new EditViewModel(petcontact));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = Roles.IsUserInRole(UserTypeEnum.Admin.ToString())
                    ? HttpContext.User.GetOwnerId()
                    : HttpContext.User.GetUserId();
                var contact = UnitOfWork.ContactRepository.GetSingleTracking(c => c.PetContactId == model.Id);
                if (!model.IsEmergencyContact)
                {
                    if (contact != null)
                    {
                        UnitOfWork.ContactRepository.Delete(contact);
                        UnitOfWork.Save();
                    }
                }
                var petcontact = UnitOfWork.PetContactRepository.GetSingleTracking(c => c.Id == model.Id, c => c.Contacts);
                var petTypeName = UnitOfWork.PetRepository.GetSingle(c => c.Id == model.PetId);
                model.Map(petcontact, contact, userId, petTypeName.PetTypeId, petTypeName.Name);

                UnitOfWork.PetContactRepository.Update(petcontact);
                UnitOfWork.Save();
                Success(Resources.Wording.PetContact_Edit_EditSuccessMessage);
                TempData["ECSuccessMessage"] = Resources.Wording.PetContact_Edit_EditSuccessMessage;
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int petcontactId, int petId)
        {
            var petcontact = UnitOfWork.PetContactRepository.GetSingle(c => c.Id == petcontactId);
            ViewBag.PetId = petId;
            return PartialView("_Delete", new DeleteViewModel(petcontact));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var contact=UnitOfWork.ContactRepository.GetSingle(c => c.PetContactId == id);
            if (contact != null)
            {
                UnitOfWork.ContactRepository.Delete(contact);
                UnitOfWork.Save();
            }

            var petcontact = UnitOfWork.PetContactRepository.GetSingle(c => c.Id == id);
            UnitOfWork.PetContactRepository.Delete(petcontact);
            UnitOfWork.Save();
            Success(Resources.Wording.PetContact_Delete_DeleteSuccessMessage);
            return RedirectToAction("List", new { petId });
        }
    }
}
