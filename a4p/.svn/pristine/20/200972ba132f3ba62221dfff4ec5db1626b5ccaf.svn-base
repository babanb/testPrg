using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using ADOPets.Web.Common.Authentication;
using Model;
using ADOPets.Web.ViewModels.Veterinarian;


namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class VeterinarianController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var veterinarian = UnitOfWork.VeterinarianRepository.GetAll(c => c.PetId == petId).OrderByDescending(c => c.Id).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            return PartialView("_Index", veterinarian);
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
                    UnitOfWork.VeterinarianRepository.Insert(model.Map(pet.PetTypeId, model.PetId ,userId));
                }
                else
                {
                    UnitOfWork.VeterinarianRepository.Insert(model.Map());
                }
                UnitOfWork.Save();
                Success(Resources.Wording.Veterinarian_Add_AddSuccessfullMessage);
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
            var veterinarian = UnitOfWork.VeterinarianRepository.GetAll(c => c.PetId == petId).OrderByDescending(c => c.Id).Select(c => new IndexViewModel(c));
            ViewBag.PetId = petId;
            return PartialView("_List", veterinarian);
        }

        [HttpGet]
        public ActionResult Edit(int veterinarianId)
        {
            var veterinarian = UnitOfWork.VeterinarianRepository.GetSingle(c => c.Id == veterinarianId);

            return PartialView("_Edit", new EditViewModel(veterinarian));
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
                var contact = UnitOfWork.ContactRepository.GetSingleTracking(c => c.VeterinarianId == model.Id);
                if (!model.IsEmergencyContact)
                {
                    if (contact != null)
                    {
                        UnitOfWork.ContactRepository.Delete(contact);
                        UnitOfWork.Save();
                    }
                }

                var veterinarian = UnitOfWork.VeterinarianRepository.GetSingleTracking(c => c.Id == model.Id, c => c.Contacts);
                var petTypeName = UnitOfWork.PetRepository.GetSingle(c => c.Id == model.PetId);
                model.Map(veterinarian, contact, userId, petTypeName.PetTypeId, petTypeName.Name);

                UnitOfWork.VeterinarianRepository.Update(veterinarian);
                UnitOfWork.Save();
                Success(Resources.Wording.Veterinarian_Edit_EditSuccessMessage);
                TempData["ECSuccessMessage"] = Resources.Wording.Veterinarian_Edit_EditSuccessMessage;
                return RedirectToAction("List", new { petId = model.PetId });
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int veterinarianId, int petId)
        {
            var veterinarian = UnitOfWork.VeterinarianRepository.GetSingle(c => c.Id == veterinarianId);

            ViewBag.PetId = petId;

            return PartialView("_Delete", new DeleteViewModel(veterinarian));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int petId)
        {
            var deletecontact = UnitOfWork.ContactRepository.GetSingle(c => c.VeterinarianId == id);
            if (deletecontact != null)
            {
                UnitOfWork.ContactRepository.Delete(deletecontact);
                UnitOfWork.Save();
            }

            var veterinarian = UnitOfWork.VeterinarianRepository.GetSingle(c => c.Id == id);
            UnitOfWork.VeterinarianRepository.Delete(veterinarian);
            UnitOfWork.Save();
            Success(Resources.Wording.Veterinarian_Delete_DeleteSuccessMessage);
            return RedirectToAction("List", new { petId });
        }
    }
}
