using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using Model;
using System.IO;
using ADOPets.Web.Common;
using System.Drawing;
using ADOPets.Web.ViewModels.SharePetInfo;
using System.Linq.Expressions;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.OwnerAdmin)]
    public class SharePetInfoController : BaseController
    {
        //List Share Information of selected Pet
        [HttpGet]
        public ActionResult Index()
        {
            var userId = HttpContext.User.GetUserId();
            IEnumerable<Pet> dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == userId));
            var pets = dbPets.Select(p => new { p.Name, p.Id }).ToList();
            var items = pets.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
            int petId = 0;
            SelectListItem tempItem = new SelectListItem { Text = Resources.Wording.SharePetInfo_Index_SelectAllPet, Value = "0" };
            var listItem = items.ToList();
            if (listItem.FirstOrDefault() != null)
            {
                listItem.FirstOrDefault().Selected = true;
                petId = TempData["PetId"] == null ? Convert.ToInt32(listItem.FirstOrDefault().Value) : (int)TempData["PetId"];
                listItem.Where(x => x.Value == petId.ToString()).FirstOrDefault().Selected = true;
            }
            else
            {
                listItem.Insert(0, tempItem);
            }
            ViewData["drpPetNameFilter"] = listItem;

            IEnumerable<ShareCategoryTypeContact> shareCategoryTypeContactList;
            List<IndexViewModel> shareCategoryType;
            if (petId == 0)
            {
                ViewBag.TotalShareContacts = UnitOfWork.ShareContactRepository.GetAll(m => m.ContactId != userId && m.UserId == userId, navigationProperties: sc => sc.User1).Where(m => m.User1.UserTypeId == UserTypeEnum.OwnerAdmin && m.User1.UserStatusId == UserStatusEnum.Active).Select(m => new MyContactViewModel(m.User1, m.Id, userId)).Distinct().Count();
                shareCategoryType = UnitOfWork.ShareCategoryTypeRepository.GetAll(navigationProperties: sc => sc.SharePetInformations).Select(s => new IndexViewModel(s, 0, petId)).ToList();
            }
            else
            {
                shareCategoryTypeContactList = UnitOfWork.ShareCategoryTypeContactRepository.GetAll(navigationProperties: s => s.ShareContact);
                ViewBag.TotalShareContacts = UnitOfWork.ShareContactRepository.GetAll(m => m.ContactId != userId && m.UserId == userId, navigationProperties: sc => sc.User1).Where(m => m.User1.UserTypeId == UserTypeEnum.OwnerAdmin && m.User1.UserStatusId == UserStatusEnum.Active).Select(m => new MyContactViewModel(m.User1, m.Id, userId)).Distinct().Count();
                shareCategoryType = UnitOfWork.ShareCategoryTypeRepository.GetAll(navigationProperties: sc => sc.SharePetInformations).Select(s => new IndexViewModel(s, shareCategoryTypeContactList.Where(x => x.ShareCategoryTypeId == s.Id && x.PetId == petId && x.ShareContact.UserId == userId).Count(), petId)).ToList();
            }
            return View(shareCategoryType);
        }

        [HttpGet]
        public ActionResult ShareIndex(int petId)
        {
            var userId = HttpContext.User.GetUserId();
            IEnumerable<Pet> dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == userId));
            var pets = dbPets.Select(p => new { p.Name, p.Id }).ToList();
            var items = pets.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString(), Selected = i.Id == petId ? true : false });
            var listItem = items.ToList();
            ViewData["drpPetNameFilter"] = listItem;

            IEnumerable<ShareCategoryTypeContact> shareCategoryTypeContactList = UnitOfWork.ShareCategoryTypeContactRepository.GetAll(navigationProperties: s => s.ShareContact);
            ViewBag.TotalShareContacts = UnitOfWork.ShareContactRepository.GetAll(m => m.ContactId != userId && m.UserId == userId, navigationProperties: sc => sc.User1).Where(m => m.User1.UserTypeId == UserTypeEnum.OwnerAdmin).Select(m => new MyContactViewModel(m.User1, m.ContactId, m.Id)).Distinct().Count();
            List<IndexViewModel> shareCategoryType = UnitOfWork.ShareCategoryTypeRepository.GetAll(navigationProperties: sc => sc.SharePetInformations).Select(s => new IndexViewModel(s, shareCategoryTypeContactList.Where(x => x.ShareCategoryTypeId == s.Id && x.PetId == petId && x.ShareContact.UserId == userId).Count(), petId)).ToList();
            return PartialView("_Index", shareCategoryType);
        }

        //Share the Information to all shared contact according to its share category type
        [HttpPost]
        public ActionResult ShareInfo(ShareCategoryTypeEnum[] shareInputs, int drpPetNameFilter)
        {
            bool flag = false;
            string SharedInfo = "";
            if (drpPetNameFilter != 0)
            {
                var valuesAsList = Enum.GetValues(typeof(ShareCategoryTypeEnum)).Cast<ShareCategoryTypeEnum>().ToList();
                foreach (var item in valuesAsList)
                {
                    List<SharePetInformation> sharePetInformationlist = UnitOfWork.SharePetInformationRepository.GetAll(x => x.ShareCategoryTypeId == item && x.PetId == drpPetNameFilter).ToList();
                    foreach (var sharePetInformation in sharePetInformationlist)
                    {
                        UnitOfWork.SharePetInformationRepository.Delete(sharePetInformation);
                        UnitOfWork.Save();
                    }
                }

                if (shareInputs != null)
                {
                    var userId = HttpContext.User.GetUserId();
                    SharePetInformation objSharePetInformation = new SharePetInformation();
                    foreach (ShareCategoryTypeEnum shareCategoryTypeId in shareInputs)
                    {
                        var lstShareCategoryType = UnitOfWork.ShareCategoryTypeContactRepository.GetAll(x => x.ShareCategoryTypeId == shareCategoryTypeId && x.PetId == drpPetNameFilter, navigationProperties: sc => sc.ShareContact)
                            .Where(x => x.ShareContact.UserId == userId)
                            .Select(x => new { Id = x.Id, ShareContactId = x.ShareContactId, ShareCategoryTypeId = x.ShareCategoryTypeId, PetIDs = x.PetId, ShareContact = x.ShareContact }).ToList();
                        foreach (var contact in lstShareCategoryType)
                        {
                            objSharePetInformation = new SharePetInformation();
                            objSharePetInformation.ContactId = contact.ShareContact.ContactId;
                            objSharePetInformation.UserId = userId;
                            objSharePetInformation.ShareCategoryTypeId = shareCategoryTypeId;
                            objSharePetInformation.PetId = drpPetNameFilter;
                            objSharePetInformation.IsRead = false;
                            objSharePetInformation.CreationDate = DateTime.Now;
                            objSharePetInformation.Id = 0;
                            UnitOfWork.SharePetInformationRepository.Insert(objSharePetInformation);
                            UnitOfWork.Save();

                            Expression<Func<ShareContact, object>> parames1 = v => v.User;
                            Expression<Func<ShareContact, object>> parames2 = v => v.User1;
                            Expression<Func<ShareContact, object>>[] paramesArray = new Expression<Func<ShareContact, object>>[] { parames1, parames2 };
                            var sharecontact = UnitOfWork.ShareContactRepository.GetSingle(x => x.UserId == userId && x.ContactId == contact.ShareContact.ContactId, navigationProperties: paramesArray);
                            if (sharecontact != null)
                            {
                                EmailSender.SendSharedPetInformationToContacts(sharecontact.User1.Email, sharecontact.User1.FirstName + " " + sharecontact.User1.LastName, sharecontact.User.FirstName + " " + sharecontact.User.LastName, sharecontact.UserId.ToString());
                            }
                            flag = true;

                        }

                        if (shareCategoryTypeId == ShareCategoryTypeEnum.ShareIDInformation)
                            SharedInfo += Resources.Wording.ShareCategoryType_IdInformation + ",";
                        if (shareCategoryTypeId == ShareCategoryTypeEnum.ShareMedicalRecords)
                            SharedInfo += Resources.Wording.ShareCategoryType_MedicalRecords + ",";
                        if (shareCategoryTypeId == ShareCategoryTypeEnum.ShareVeterinarians)
                            SharedInfo += Resources.Wording.ShareCategoryType_Veterinarians + ",";
                        if (shareCategoryTypeId == ShareCategoryTypeEnum.ShareContacts)
                            SharedInfo += Resources.Wording.ShareCategoryType_Contacts + ",";
                        if (shareCategoryTypeId == ShareCategoryTypeEnum.ShareGallery)
                            SharedInfo += Resources.Wording.ShareCategoryType_Gallery + ",";
                    }
                }
            }
            if (flag)
            {
                if (shareInputs.Length == 5)
                    Success(Resources.Wording.SharePetInfo_ShareInfo_Message);
                else
                {
                    SharedInfo = SharedInfo.Substring(0, SharedInfo.Length - 1);
                    string sucessmessage = string.Format(Resources.Wording.SharePetInfo_ShareInfo_ShareCategoryType_Message, SharedInfo);
                    Success(sucessmessage);
                }
            }
            else
            {
                Success(Resources.Wording.SharePetInfo_Index_PetsNotAvailable);
            }
            return RedirectToAction("Index");
        }

        //Get List of My Contacts(Pet Owners which added in contacts)
        [HttpGet]
        public ActionResult MyContact()
        {
            var userId = HttpContext.User.GetUserId();
            var userlist = UnitOfWork.ShareContactRepository.GetAll(m => m.ContactId != userId && m.UserId == userId, navigationProperties: sc => sc.User1).Where(x => x.User1.UserTypeId == UserTypeEnum.OwnerAdmin && x.User1.UserStatusId == UserStatusEnum.Active).Select(m => new MyContactViewModel(m.User1, m.Id, m.UserId, UnitOfWork.LoginRepository.GetSingleTracking(lg => lg.UserId == m.ContactId)));
            return View(userlist);
        }

        // Get List of contacts from my contacts to select sharable contacts 
        [HttpGet]
        public ActionResult SelectContact(ShareCategoryTypeEnum ShareContactType, int petId)
        {
            TempData["ShareContactType"] = ShareContactType;
            TempData.Keep("ShareContactType");
            TempData["PetId"] = petId;
            TempData.Keep("PetId");
            var userId = HttpContext.User.GetUserId();
            Expression<Func<ShareContact, object>> parames1 = v => v.User1;
            Expression<Func<ShareContact, object>> parames2 = v => v.ShareCategoryTypeContacts;
            Expression<Func<ShareContact, object>>[] paramesArray = new Expression<Func<ShareContact, object>>[] { parames1, parames2 };
            List<MyContactViewModel> userlist = null;
            userlist = UnitOfWork.ShareContactRepository.GetAll(m => m.ContactId != userId && m.UserId == userId, navigationProperties: paramesArray).Where(m => m.User1.UserTypeId == UserTypeEnum.OwnerAdmin && m.User1.UserStatusId == UserStatusEnum.Active).Select(m => new MyContactViewModel(m.User1, m.Id, userId, m.ShareCategoryTypeContacts.ToList().Where(x => x.ShareCategoryTypeId == ShareContactType && x.PetId == petId && x.ShareContact.ContactId == m.ContactId).Count(), UnitOfWork.LoginRepository.GetSingleTracking(lg => lg.UserId == m.ContactId))).ToList();
            return PartialView("_SelectContact", userlist);
        }

        //Get List of Selected contacts from share contacts
        [HttpGet]
        public ActionResult SelectedContacts(ShareCategoryTypeEnum ShareContactType, int petId)
        {
            TempData["ShareContactType"] = ShareContactType;
            TempData.Keep("ShareContactType");

            TempData["PetId"] = petId;
            TempData.Keep("PetId");

            var userId = HttpContext.User.GetUserId();
            Expression<Func<ShareContact, object>> parames1 = v => v.User1;
            Expression<Func<ShareContact, object>> parames2 = v => v.ShareCategoryTypeContacts;
            Expression<Func<ShareContact, object>>[] paramesArray = new Expression<Func<ShareContact, object>>[] { parames1, parames2 };
            List<MyContactViewModel> userlist = null;
            userlist = UnitOfWork.ShareContactRepository.GetAll(m => m.ContactId != userId && m.UserId == userId, navigationProperties: paramesArray).Where(m => m.User1.UserTypeId == UserTypeEnum.OwnerAdmin && m.User1.UserStatusId == UserStatusEnum.Active).Select(m => new MyContactViewModel(m.User1, m.Id, userId, m.ShareCategoryTypeContacts.ToList().Where(x => x.ShareCategoryTypeId == ShareContactType && x.PetId == petId && x.ShareContact.ContactId == m.ContactId).Count(), UnitOfWork.LoginRepository.GetSingleTracking(lg => lg.UserId == m.ContactId))).Where(x => x.IsContactExist == true).ToList();
            return PartialView("_SelectedContacts", userlist);
        }

        //Get List of Contacts(Owners) to add contacts in My Contacts  
        [HttpGet]
        public ActionResult AddContact()
        {
            var actualUser = HttpContext.User.GetUserId();
            var sharecontactList = UnitOfWork.ShareContactRepository.GetAll(sc => sc.UserId == actualUser).Select(a => a.ContactId);
            var usrlist = UnitOfWork.UserRepository.GetAll(m => m.Id != actualUser && m.Id != 0 && !m.IsNonSearchable && m.UserType.Id == UserTypeEnum.OwnerAdmin && m.UserStatusId == UserStatusEnum.Active, navigationProperties: u => u.Logins);
            var userList = usrlist.Where(ul => !sharecontactList.Contains(ul.Id)).Select(m => new AddContactViewModel(m, m.Logins.FirstOrDefault()));
            return PartialView("_AddContact", userList);
        }

        // Add Contacts into My Contacts for share pet Information
        [HttpPost]
        public ActionResult AddContact(int id)
        {
            AddContactViewModel model = new AddContactViewModel();
            model.ContactId = id;
            var usr = UnitOfWork.UserRepository.GetSingle(u => u.Id == id && u.UserType.Id == UserTypeEnum.OwnerAdmin);
            model.FirstName = usr.FirstName;
            model.LastName = usr.LastName;
            var userId = HttpContext.User.GetUserId();
            model.UserId = userId;
            UnitOfWork.ShareContactRepository.Insert(model.Map());
            UnitOfWork.Save();
            Success(Resources.Wording.SharePetInfo_Add_AddSuccessMsg);

            var actualUser = HttpContext.User.GetUserId();
            var sharecontactList = UnitOfWork.ShareContactRepository.GetAll(sc => sc.UserId == actualUser).Select(a => a.ContactId);
            var usrlist = UnitOfWork.UserRepository.GetAll(m => m.Id != actualUser && m.Id != 0 && !m.IsNonSearchable && m.UserType.Id == UserTypeEnum.OwnerAdmin, navigationProperties: u => u.Logins);
            var userList = usrlist.Where(ul => !sharecontactList.Contains(ul.Id)).Select(m => new AddContactViewModel(m, m.Logins.FirstOrDefault()));
            return PartialView("_AddContact", userList);
        }

        //Save Selected contacts in share contacts
        [HttpPost]
        public ActionResult AddShareContacts(int[] shareInputs)
        {
            if (TempData["ShareContactType"] != null)
            {
                var userId = HttpContext.User.GetUserId();
                ShareCategoryTypeEnum shareContactType = (ShareCategoryTypeEnum)TempData["ShareContactType"];
                int petId = TempData["PetId"] == null ? 0 : (int)TempData["PetId"];
                List<AddShareCategoryTypeContactModel> lstShareCTypeContactModel = UnitOfWork.ShareCategoryTypeContactRepository.GetAll(x => x.ShareCategoryTypeId == shareContactType && x.PetId == petId).Select(x => new AddShareCategoryTypeContactModel { Id = x.Id, ShareContactId = x.ShareContactId, ShareCategoryTypeId = x.ShareCategoryTypeId, PetId = x.PetId }).ToList();
                foreach (var shareCategoryTypeContactModel in lstShareCTypeContactModel)
                {
                    UnitOfWork.ShareCategoryTypeContactRepository.Delete(shareCategoryTypeContactModel.Map());
                    UnitOfWork.Save();
                }
                if (shareInputs != null && petId != 0)
                {
                    AddShareCategoryTypeContactModel model;
                    if (petId != 0)
                    {
                        foreach (int shareContactId in shareInputs)
                        {
                            model = new AddShareCategoryTypeContactModel();
                            model.ShareContactId = shareContactId;
                            model.ShareCategoryTypeId = shareContactType;
                            model.PetId = Convert.ToInt32(petId);
                            UnitOfWork.ShareCategoryTypeContactRepository.Insert(model.Map());
                            UnitOfWork.Save();
                        }
                    }
                    Success(Resources.Wording.SharePetInfo_SelectContact_SuccessMsg);
                }
            }
            return RedirectToAction("Index");
        }

        //Get List for Community Contacts for Add Contacts into community
        [HttpGet]
        public ActionResult SelectCommunityContact(int petId)
        {
            TempData["PetId"] = petId;
            TempData.Keep("PetId");
            var userId = HttpContext.User.GetUserId();
            var CommuntyUsers = UnitOfWork.SharePetInfoCommunityRepository.GetAll(x => x.UserId == userId && x.ContactId != userId && x.PetId == petId).ToList();
            var usrlist = UnitOfWork.UserRepository.GetAll(m => m.Id != userId && m.Id != 0 && !m.IsNonSearchable && m.UserTypeId == UserTypeEnum.OwnerAdmin && m.UserStatusId == UserStatusEnum.Active, navigationProperties: u => u.Logins);
            var userList = usrlist.Select(m => new AddContactViewModel(m, CommuntyUsers.Where(x => x.ContactId == m.Id).Count(), m.Logins.FirstOrDefault()));
            return PartialView("_CommunityContacts", userList);
        }

        //Add contacts in Community Contacts
        [HttpPost]
        public ActionResult AddCommunityContacts(int[] shareInputs)
        {
            bool flag = false;
            var userId = HttpContext.User.GetUserId();
            int petId = TempData["PetId"] == null ? 0 : (int)TempData["PetId"];
            List<SharePetInfoCommunity> sharePetCommunitylist = UnitOfWork.SharePetInfoCommunityRepository.GetAll(x => x.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareGallery && x.PetId == petId).ToList();
            foreach (var sharePetCommunity in sharePetCommunitylist)
            {
                UnitOfWork.SharePetInfoCommunityRepository.Delete(sharePetCommunity);
                UnitOfWork.Save();
            }
            if (shareInputs != null && petId != 0)
            {
                AddCommunityContactsModel model;
                foreach (int contactId in shareInputs)
                {
                    model = new AddCommunityContactsModel();
                    model.ContactId = contactId;
                    model.ShareCategoryTypeId = ShareCategoryTypeEnum.ShareGallery;
                    model.PetId = Convert.ToInt32(petId);
                    model.UserId = userId;
                    model.CreationDate = DateTime.Now;
                    model.IsRead = false;
                    UnitOfWork.SharePetInfoCommunityRepository.Insert(model.Map());
                    UnitOfWork.Save();
                    flag = true;

                    //Expression<Func<ShareContact, object>> parames1 = v => v.User;
                    //Expression<Func<ShareContact, object>> parames2 = v => v.User1;
                    //Expression<Func<ShareContact, object>>[] paramesArray = new Expression<Func<ShareContact, object>>[] { parames1, parames2 };
                    var sharecontact = UnitOfWork.UsersRepository.GetSingleTracking(x => x.Id == contactId);
                    var currUser = UnitOfWork.UsersRepository.GetSingleTracking(x => x.Id == userId);

                    if (sharecontact != null)
                    {
                        EmailSender.SendSharedPetInformationToContacts(sharecontact.Email, sharecontact.FirstName + " " + sharecontact.LastName, currUser.FirstName + " " + currUser.LastName, currUser.Id.ToString());
                    }
                }
            }
            if (flag)
            {
                Success(Resources.Wording.SharePetInfo_CommunityUser_SuccessMsg);
            }
            return RedirectToAction("Index");
        }

        //Get Share Contact for Delete Contacts
        [HttpGet]
        public ActionResult DeleteContact(int contactId)
        {
            var userId = HttpContext.User.GetUserId();
            var shareCon = UnitOfWork.ShareContactRepository.GetSingle(sc => sc.ContactId == contactId && sc.UserId == userId);
            return PartialView("_DeleteContact", new DeleteContactViewModel(shareCon));
        }

        //Delete Contacts From share contacts
        [HttpPost]
        public ActionResult DeleteContact(DeleteContactViewModel model)
        {
            var userId = HttpContext.User.GetUserId();
            model.UserId = userId;
            List<AddShareCategoryTypeContactModel> lstShareCategoryTypeContactModel = UnitOfWork.ShareCategoryTypeContactRepository.GetAll(x => x.ShareContactId == model.Id).Select(x => new AddShareCategoryTypeContactModel { Id = x.Id, ShareContactId = x.ShareContactId, ShareCategoryTypeId = x.ShareCategoryTypeId, PetId = x.PetId }).ToList();
            foreach (var shareCategoryTypeContactModel in lstShareCategoryTypeContactModel)
            {
                UnitOfWork.ShareCategoryTypeContactRepository.Delete(shareCategoryTypeContactModel.Map());
                UnitOfWork.Save();
            }
            var shareCon = UnitOfWork.ShareContactRepository.GetSingle(sc => sc.Id == model.Id && sc.UserId == model.UserId);
            UnitOfWork.ShareContactRepository.Delete(shareCon);
            UnitOfWork.Save();
            Success(Resources.Wording.SharePetInfo_Add_DeleteSuccessMsg);
            return RedirectToAction("MyContact");
        }

        [HttpGet]
        public ActionResult GetImage(string fromUserId)
        {
            int userId = Convert.ToInt32(fromUserId);
            var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
            if (user == null || user.ProfileImage == null)
            {
                return File("~/Content/Images/ownerProfilepic.jpg", "image/jpg");
            }
            return File(user.ProfileImage, "image/jpg");
        }

        [HttpGet]
        public ActionResult GetPets()
        {
            var userId = HttpContext.User.GetUserId();
            var dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == userId));
            var pets = dbPets.Select(p => new { p.Name, p.Id }).ToList();
            var items = pets.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}
