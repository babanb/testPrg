using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.ViewModels.Contact;
using Model;
using System;
using ADOPets.Web.Common.Helpers;
using System.Collections.Generic;


namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class ContactController : BaseController
    {
        public string[] GetPetDetail(int? petId)
        {
            string[] petDetail = new string[2];
            if (petId != null && petId > 0)
            {
                var dbPets = UnitOfWork.PetRepository.GetSingle(p => p.Id == petId);
                petDetail[0]= dbPets.Name;
                petDetail[1] = EnumHelper.GetResourceValueForEnumValue(dbPets.PetTypeId); ;
            }
            return petDetail;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var contact = new List<IndexViewModel>();

            var userId = HttpContext.User.GetUserId();
            var dbContact = UnitOfWork.ContactRepository.GetAll(c => c.UserId == userId).OrderByDescending(c => c.Id);//.Select(c => new IndexViewModel(c));
            foreach (Contact c in dbContact)
            {
                var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == c.PetId);
                if (pet != null)
                {
                    contact.Add(new IndexViewModel(c,pet.Name,pet.PetTypeId));
                }
            }

            return View(contact);
        }

        #region Removed functionality
        //[HttpGet]
        //public ActionResult Add()
        //{
        //    return View(new AddViewModel());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Add(AddViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var userId = HttpContext.User.GetUserId();
        //    UnitOfWork.ContactRepository.Insert(model.Map(userId));
        //    UnitOfWork.Save();
        //    Success(Resources.Contact.Add.SuccessMsg);
        //    return RedirectToAction("Index");
        //}


        //[HttpGet]
        //public ActionResult Edit(int contactId)
        //{
        //    var contact = UnitOfWork.ContactRepository.GetSingle(c => c.Id == contactId);
        //    return View(new EditViewModel(contact));

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(EditViewModel model)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var contact = UnitOfWork.ContactRepository.GetSingle(c => c.Id == model.Id);
        //    model.Map(contact);
        //    UnitOfWork.ContactRepository.Update(contact);
        //    UnitOfWork.Save();
        //    Success(Resources.Contact.Edit.SuccessMsg);
        //    return RedirectToAction("Index");

        //}

        //[HttpGet]
        //public ActionResult DeleteConfirm(int contactId)
        //{
        //    var contact = UnitOfWork.ContactRepository.GetSingle(c => c.Id == contactId);
        //    return PartialView("_Delete", new DeleteViewModel(contact));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id)
        //{
        //    var contact = UnitOfWork.ContactRepository.GetSingle(c => c.Id == id);
        //    UnitOfWork.ContactRepository.Delete(contact);
        //    UnitOfWork.Save();
        //    Success(Resources.Contact.Delete.DeleteMsg);
        //    return RedirectToAction("Index");
        //}
        #endregion

        [HttpGet]
        public ActionResult Summary(int contactId, int contactType)
        {
            var contact = new Contact();
            var type = (ContactTypeEnum)contactType;

            if (type == ContactTypeEnum.Veterinarian)
            {
                contact = UnitOfWork.ContactRepository.GetSingle(c => c.VeterinarianId == contactId);
            }
            else
            {
                contact = UnitOfWork.ContactRepository.GetSingle(c => c.PetContactId == contactId);
            }
            return View(new SummaryViewModel(contact));
        }

    }
}
