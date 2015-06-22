using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.ViewModels.Users;
using ADOPets.Web.Common.Payment.Model;
using System.Web.Security;
using System.Linq.Expressions;
using System.Globalization;
using Model.Tools;
using ADOPets.Web.Resources;
using ADOPets.Web.Common;


namespace ADOPets.Web.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet]
        [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
        public ActionResult Index()
        {
            var userId = HttpContext.User.GetUserId();

            var users = new List<IndexViewModel>();

            if (HttpContext.User.IsInRole(UserTypeEnum.Admin.ToString()) || HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString()) || HttpContext.User.IsInRole(UserTypeEnum.VeterinarianLight.ToString()) || HttpContext.User.IsInRole(UserTypeEnum.VeterinarianExpert.ToString()))
            {
                IEnumerable<Model.Subscription> PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted);
                int? centerId = HttpContext.User.GetCenterId();

                ViewBag.Plans = PlansNPromocode.GroupBy(o => o.Name).Select(g => g.First()).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
                ViewBag.Promocode = PlansNPromocode.Where(u => u.IsPromotionCode).GroupBy(o => o.PromotionCode).Select(g => g.First()).Select(i => new SelectListItem { Text = i.PromotionCode, Value = i.Id.ToString() }).OrderBy(a => a.Text);

                var vetSpeciality = UnitOfWork.VetSpecialityRepository.GetAll();
                ViewBag.vetSpeciality = vetSpeciality.Select(i => new SelectListItem { Text = EnumHelper.GetResourceValueForEnumValue(i.ID), Value = i.ID.ToString() });

                Expression<Func<User, object>> parames1 = v => v.Veterinarians;
                Expression<Func<User, object>> parames2 = v => v.UserSubscription;
                Expression<Func<User, object>> parames3 = v => v.UserSubscription.Subscription;
                Expression<Func<User, object>> parames4 = v => v.Logins;
                Expression<Func<User, object>> parames5 = v => v.Pets;

                Expression<Func<User, object>>[] paramesArray = new Expression<Func<User, object>>[] { parames1, parames2, parames3, parames4, parames5 };
                var dbUsers = UnitOfWork.UsersRepository.GetAll(navigationProperties: paramesArray).OrderByDescending(u => u.CreationDate).Select(u => new IndexViewModel(u, u.Veterinarians.FirstOrDefault()));//Where(u => u.UserStatusId == UserStatusEnum.Active).

                if (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString()))
                {
                    dbUsers = null;
                    dbUsers = UnitOfWork.UsersRepository.GetAll(navigationProperties: paramesArray).Where(u => u.UserTypeId != UserTypeEnum.Admin).OrderByDescending(u => u.CreationDate).Select(u => new IndexViewModel(u, u.Veterinarians.FirstOrDefault())); // u.UserStatusId == UserStatusEnum.Active &&
                    //selecting user based on CenterId

                    if (!string.IsNullOrEmpty(centerId.ToString()))
                    {
                        ViewBag.Plans = PlansNPromocode.Where(u => u.CenterId == centerId).GroupBy(o => o.Name).Select(g => g.First()).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
                        ViewBag.Promocode = PlansNPromocode.Where(u => u.IsPromotionCode && u.CenterId == centerId).GroupBy(o => o.PromotionCode).Select(g => g.First()).Select(i => new SelectListItem { Text = i.PromotionCode, Value = i.Id.ToString() }).OrderBy(a => a.Text);
                        dbUsers = UnitOfWork.UsersRepository.GetAll(navigationProperties: paramesArray).Where(u => u.CenterID == centerId && u.UserTypeId != UserTypeEnum.Admin).OrderByDescending(u => u.CreationDate).Select(u => new IndexViewModel(u, u.Veterinarians.FirstOrDefault()));


                    }
                }
                if (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianLight.ToString()))
                {
                    dbUsers = null;
                    dbUsers = UnitOfWork.UsersRepository.GetAll(navigationProperties: paramesArray).Where(u => u.UserTypeId == UserTypeEnum.OwnerAdmin).OrderByDescending(u => u.CreationDate).Select(u => new IndexViewModel(u, u.Veterinarians.FirstOrDefault())); // u.UserStatusId == UserStatusEnum.Active &&
                    //selecting user based on CenterId
                    if (!string.IsNullOrEmpty(centerId.ToString()))
                    {
                        ViewBag.Plans = PlansNPromocode.Where(u => u.CenterId == centerId).GroupBy(o => o.Name).Select(g => g.First()).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
                        ViewBag.Promocode = PlansNPromocode.Where(u => u.IsPromotionCode && u.CenterId == centerId).GroupBy(o => o.PromotionCode).Select(g => g.First()).Select(i => new SelectListItem { Text = i.PromotionCode, Value = i.Id.ToString() }).OrderBy(a => a.Text);
                        dbUsers = UnitOfWork.UsersRepository.GetAll(navigationProperties: paramesArray).Where(u => u.CenterID == centerId && u.UserTypeId == UserTypeEnum.OwnerAdmin).OrderByDescending(u => u.CreationDate).Select(u => new IndexViewModel(u, u.Veterinarians.FirstOrDefault()));

                    }
                }
                if (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                {
                    var listSMOUser = UnitOfWork.SMOExpertRepository.GetAll(s => s.VetExpertID == userId, null, s => s.SMORequest, s => s.User, s => s.SMORequest.Pet, s => s.SMORequest.User, s => s.SMORequest.Pet.Users).OrderByDescending(s => s.SMORequest.RequestDate).Distinct().Select(a => a.SMORequest.UserId);
                    var listECUser = UnitOfWork.EConsultationRepository.GetAll(s => s.VetId == userId, null, s => s.Pet, s => s.User).Distinct().Select(a => a.UserId);

                    var assignedUser = listSMOUser.Concat(listECUser);
                    if (assignedUser != null)
                    {
                        dbUsers = UnitOfWork.UserRepository.GetAll(navigationProperties: paramesArray).Where(u => u.UserTypeId == UserTypeEnum.OwnerAdmin && assignedUser.Contains(u.Id)).OrderByDescending(u => u.CreationDate).Select(u => new IndexViewModel(u, u.Veterinarians.FirstOrDefault()));
                                                                      
                    }
                }

                ViewBag.ExcludedItemList = (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString())) ?
                    new List<string> { UserTypeEnum.Admin.ToString(), UserTypeEnum.Guest.ToString(), UserTypeEnum.Assistant.ToString(), UserTypeEnum.Partner.ToString() } :
                    (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianLight.ToString())) ?
                    new List<string> { UserTypeEnum.Admin.ToString(), UserTypeEnum.Assistant.ToString(), UserTypeEnum.Guest.ToString(), UserTypeEnum.Partner.ToString(), UserTypeEnum.VeterinarianAdo.ToString(), UserTypeEnum.VeterinarianExpert.ToString(), UserTypeEnum.VeterinarianLight.ToString(), UserTypeEnum.Guest.ToString(), UserTypeEnum.Assistant.ToString(), UserTypeEnum.Partner.ToString() } :
                    new List<string> { UserTypeEnum.Guest.ToString(), UserTypeEnum.Assistant.ToString(), UserTypeEnum.Partner.ToString() };
                ViewBag.UsertypeText = (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString())) ? Wording.Users_Index_UserTypePlaceHolder : null;
                return View(dbUsers);
            }

            return View();
        }

        [HttpGet]
        [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
        public ActionResult RemoveUser(int id)
        {
            var usr = UnitOfWork.UsersRepository.GetSingle(u => u.Id == id);
            if (usr != null)
            {
                usr.UserStatusId = UserStatusEnum.Disabled;
                UnitOfWork.UsersRepository.Update(usr);
                UnitOfWork.Save();
                Success(Resources.Wording.Users_RemoveUser_DeleteSuccessMessage);
            }
            return RedirectToAction("Index");
        }

        [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
        public ActionResult ViewUser(int id)
        {
            var usr = UnitOfWork.UsersRepository.GetSingle(u => u.Id == id, u => u.Logins, u => u.Veterinarians, u => u.UserSubscription, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);
            string finalPlan = null;
            if (usr.UserSubscription != null)
            {
                var subscription = usr.UserSubscription.Subscription;
                var AdditionalPets = usr.UserSubscription.SubscriptionService != null ? usr.UserSubscription.SubscriptionService.AditionalPetCount != null ? usr.UserSubscription.SubscriptionService.AditionalPetCount : 0 : 0;
                AdditionalPets += (usr.UserSubscription.AdditionalPetcount != null ? usr.UserSubscription.AdditionalPetcount : 0);
                if (subscription != null)
                {
                    if (subscription.IsTrial == true)
                    {
                        finalPlan = subscription.Name;
                    }
                    else
                    {
                        var totalAmmount = subscription.Amount + (subscription.AmmountPerAddionalPet * AdditionalPets);
                        var totalMra = AdditionalPets + subscription.MRACount;
                        var totalPets = AdditionalPets + subscription.MaxPetCount;
                        var totalSmo = subscription.SMOCount;

                        if (string.IsNullOrEmpty(subscription.AmmountPerAddionalPet.ToString()))
                        {
                            totalAmmount = subscription.Amount;
                        }

                        if (!string.IsNullOrEmpty(subscription.AditionalInfo) && subscription.IsBase)
                        {
                            totalAmmount = subscription.Amount + (subscription.AmmountPerAddionalPet * (AdditionalPets / subscription.MaxPetCount));
                        }

                        finalPlan = GetPlanName(subscription, totalAmmount ?? 0, totalPets ?? 0, totalMra ?? 0, totalSmo ?? 0);
                    }
                }
            }
            return View(new ViewUserViewModel(usr, finalPlan));
        }

        public ActionResult AddUser()
        {
            AddUserViewModel model = new AddUserViewModel();
            model = GetDefaultAddUserDetails(model);

            var defaultBasePlan = UnitOfWork.SubscriptionRepository.GetFirst(s => !s.IsPromotionCode && string.IsNullOrEmpty(s.PromotionCode));
            if (defaultBasePlan == null)
            {
                defaultBasePlan = UnitOfWork.SubscriptionRepository.GetFirst(s => !s.IsPromotionCode && string.IsNullOrEmpty(s.PromotionCode));
            }
            model.BasePlanId = defaultBasePlan.Id;
            model.BasePlanName = defaultBasePlan.Name;
            model.BasePlanPrice = defaultBasePlan.Amount;
            model.Description = defaultBasePlan.AditionalInfo;
            var UserDetails = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == model.BasePlanId);
            ViewBag.Discription = UserDetails.Description;
            ViewBag.PetCount = UserDetails.MaxPetCount;
            if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                model.TimeZone = TimeZoneEnum.UTC0500EasternTimeUSCanada;
                model.Country = CountryEnum.UnitedStates;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
            {
                model.TimeZone = TimeZoneEnum.UTC0530ChennaiKolkataMumbaiNewDelhi;
                model.Country = CountryEnum.India;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                model.TimeZone = TimeZoneEnum.UTC0100BrusselsCopenhagenMadridParis;
                model.Country = CountryEnum.France;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.Portuguese)
            {
                model.TimeZone = TimeZoneEnum.UTC0300Brasilia;
                model.Country = CountryEnum.BRAZIL;
            }
            return View(model);
        }

        private AddUserViewModel GetDefaultAddUserDetails(AddUserViewModel model)
        {
            var userId = HttpContext.User.GetUserId();
            model.Center = UnitOfWork.CenterRepository.GetAll(a => a.IsDeleted != true).ToList();
            ViewBag.CenterList = model.Center.Select(i => new SelectListItem { Text = i.CenterName, Value = i.Id.ToString() });
            int? centerId = HttpContext.User.GetCenterId();

            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                model.Center = UnitOfWork.CenterRepository.GetAll(a => a.IsDeleted != true && a.Id == centerId).ToList();
                model.CenterId = UnitOfWork.CenterRepository.GetSingleTracking(a => a.IsDeleted != true && a.Id == centerId).Id;
                ViewBag.CenterList = model.Center.Select(i => new SelectListItem { Text = i.CenterName, Value = i.Id.ToString() });

            }


            IEnumerable<Model.Subscription> PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted);
            if (DomainHelper.GetDomain() == DomainTypeEnum.US || DomainHelper.GetDomain() == DomainTypeEnum.India)
            {
                PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted).OrderByDescending(a => a.Name);
            }

            ViewBag.Plans = PlansNPromocode.GroupBy(o => o.Name).Select(g => g.First()).Where(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
            ViewBag.Promocode = PlansNPromocode.GroupBy(o => o.PromotionCode).Select(g => g.First()).Where(u => u.IsPromotionCode).Select(i => new SelectListItem { Text = i.PromotionCode, Value = i.PromotionCode }).OrderBy(a => a.Text);

            //selecting plan based on centerId
            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                ViewBag.Plans = PlansNPromocode.GroupBy(o => o.Name).Select(g => g.First()).Where(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
                ViewBag.Promocode = PlansNPromocode.GroupBy(o => o.PromotionCode).Select(g => g.First()).Where(u => u.IsPromotionCode && u.CenterId == centerId).Select(i => new SelectListItem { Text = i.PromotionCode, Value = i.PromotionCode }).OrderBy(a => a.Text);
                var defaultBasePlan = UnitOfWork.SubscriptionRepository.GetFirst(s => !s.IsPromotionCode && string.IsNullOrEmpty(s.PromotionCode));
            }

            ViewBag.ExcludedItemList = (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString())) ?
                    new List<UserTypeEnum> { UserTypeEnum.Admin, UserTypeEnum.Guest, UserTypeEnum.Assistant, UserTypeEnum.Partner } :
                    (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianLight.ToString())) ?
                    new List<UserTypeEnum> { UserTypeEnum.Admin, UserTypeEnum.Assistant, UserTypeEnum.Guest, UserTypeEnum.Partner, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianExpert, UserTypeEnum.VeterinarianLight, UserTypeEnum.Guest, UserTypeEnum.Assistant, UserTypeEnum.Partner } :
                        new List<UserTypeEnum> { UserTypeEnum.Guest, UserTypeEnum.Assistant, UserTypeEnum.Partner };
            ViewBag.UsertypeText = (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString())) ? Wording.Users_Index_UserTypePlaceHolder : null;



            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(HttpPostedFileBase imgNewUserPhoto, AddUserViewModel model)
        {
            var userId = HttpContext.User.GetUserId();
            if (!ModelState.IsValid)
            {
                if (ModelState.Keys.Contains("State") && ModelState["State"].Errors.Count > 0)
                {
                    ModelState.Remove("State");
                }
                if (!model.Usertype.Equals(UserTypeEnum.VeterinarianLight) || !model.Usertype.Equals(UserTypeEnum.VeterinarianAdo) || !model.Usertype.Equals(UserTypeEnum.VeterinarianExpert))
                {
                    if (ModelState.Keys.Contains("Speciality") && ModelState["Speciality"].Errors.Count > 0)
                    {
                        ModelState.Remove("Speciality");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                model = GetDefaultAddUserDetails(model);
                return View("AddUser", model);
            }
            else
            {
                byte[] data = null;
                if (imgNewUserPhoto != null)
                {
                    using (var reader = new System.IO.BinaryReader(imgNewUserPhoto.InputStream))
                    {
                        data = reader.ReadBytes(imgNewUserPhoto.ContentLength);
                    }
                }

                var subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == model.BasePlanId);
                model.BasePlanId = Convert.ToInt32(model.BasePlanName);

                if (model.BasePlanId != 0)
                {
                    subscription = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == model.BasePlanId);
                    var price = subscription.Amount;
                    model.BasePlanPrice = Convert.ToDecimal(price);
                    var amount = Convert.ToDecimal(price).ToString(CultureInfo.CurrentCulture);
                }
                var usrModel = model.Map(data, subscription);
                int? centerId = HttpContext.User.GetCenterId();
                if (!string.IsNullOrEmpty(centerId.ToString()))
                {
                    usrModel.CenterID = centerId;
                    UnitOfWork.UsersRepository.Insert(usrModel);
                    UnitOfWork.Save();
                }
                else
                {
                    var UserSub = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == model.BasePlanId);

                    if (UserSub.CenterId != null)
                    {
                        usrModel.CenterID = UserSub.CenterId;
                    }
                    UnitOfWork.UsersRepository.Insert(usrModel);
                    UnitOfWork.Save();
                }
                //End
                if (model.Usertype.Equals(UserTypeEnum.VeterinarianLight) || model.Usertype.Equals(UserTypeEnum.VeterinarianAdo) || model.Usertype.Equals(UserTypeEnum.VeterinarianExpert) || model.Usertype.Equals(UserTypeEnum.Admin))
                {
                    usrModel.UserSubscriptionId = null;
                    UnitOfWork.UserRepository.Update(usrModel);
                    UnitOfWork.Save();
                }
                var timeZoneInfoId = string.Empty;
                if (model.TimeZone != null)
                {
                    timeZoneInfoId = UnitOfWork.TimeZoneRepository.GetSingle(t => t.Id == model.TimeZone).TimeZoneInfoId;
                }

                User usr = UnitOfWork.UsersRepository.GetSingle(u => u.Id == usrModel.Id);
                Success(Resources.Wording.Users_AddUser_SuccessMsg);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var usr = UnitOfWork.UsersRepository.GetSingle(u => u.Id == id, u => u.Logins, u => u.Veterinarians, u => u.UserSubscription, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService);
            if (usr.BirthDate.Value == "01/01/0001 00:00:00")
            {
                usr.BirthDate.Value = null;
            }
            if (usr.UserTypeId == UserTypeEnum.OwnerAdmin)
            {
                string finalPlan = null;
                if (usr.UserSubscription != null)
                {
                    var PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted);
                    var subscription = usr.UserSubscription.Subscription;
                    var AdditionalPets = usr.UserSubscription.SubscriptionService != null ? usr.UserSubscription.SubscriptionService.AditionalPetCount : 0;
                    if (subscription != null)
                    {
                        if (subscription.IsTrial == true)
                        {
                            finalPlan = subscription.Name;
                        }
                        else
                        {
                            var totalAmmount = subscription.Amount + (subscription.AmmountPerAddionalPet * subscription.MaxContactCount);
                            var totalMra = AdditionalPets + subscription.MRACount;
                            var totalPets = AdditionalPets + subscription.MaxPetCount;
                            var totalSmo = subscription.SMOCount;
                            finalPlan = GetPlanName(subscription, totalAmmount ?? 0, totalPets ?? 0, totalMra ?? 0, totalSmo ?? 0);
                        }
                    }

                }
                return View("EditOwner", new EditOwnerViewModel(usr, finalPlan));
            }
            else if (usr.UserTypeId == UserTypeEnum.VeterinarianAdo || usr.UserTypeId == UserTypeEnum.VeterinarianExpert || usr.UserTypeId == UserTypeEnum.VeterinarianLight || usr.UserTypeId == UserTypeEnum.Assistant)
            {
                var center = UnitOfWork.CenterRepository.GetAll().ToList();
                ViewBag.CenterList = center.Select(i => new SelectListItem { Text = i.CenterName, Value = i.Id.ToString() });

                return View("EditVet", new EditVetViewModel(usr));
            }

            return View("EditAdmin", new EditAdminViewModel(usr));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOwner(HttpPostedFileBase imgNewUserPhoto, EditOwnerViewModel model, FormCollection fc)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.Keys.Contains("State") && ModelState["State"].Errors.Count > 0)
                {
                    ModelState.Remove("State");
                }
            }

            var user = UnitOfWork.UsersRepository.GetSingleTracking(i => i.Id == model.Id);
            var loginDetails = UnitOfWork.LoginRepository.GetSingleTracking(i => i.UserId == model.Id);

            byte[] data = null;
            if (imgNewUserPhoto != null)
            {
                using (var reader = new System.IO.BinaryReader(imgNewUserPhoto.InputStream))
                {
                    data = reader.ReadBytes(imgNewUserPhoto.ContentLength);
                }
            }


            bool isloginDetailChanged = false;
            if (model.Password != "========")
            {
                var randomPart = Membership.GeneratePassword(5, 2);
                loginDetails.Password = Encryption.EncryptAsymetric(model.Password + randomPart);
                loginDetails.RandomPart = randomPart;
                loginDetails.IsTemporalPassword = true;
                loginDetails.UserName = Encryption.Encrypt(model.Username);
                isloginDetailChanged = true;
            }
            else if (model.Password == "========" && model.Username != Encryption.Decrypt(loginDetails.UserName))
            {
                AccountController accountcontroller = new AccountController();
                var randomPart = Membership.GeneratePassword(5, 2);
                var newPassword = accountcontroller.GenerateRandomPassword();
                loginDetails.RandomPart = randomPart;
                loginDetails.Password = Encryption.EncryptAsymetric(newPassword + randomPart);
                loginDetails.IsTemporalPassword = true;
                loginDetails.UserName = Encryption.Encrypt(model.Username);
                isloginDetailChanged = true;
                model.Password = newPassword;
            }

            model.Map(user, data);
            UnitOfWork.UsersRepository.Update(user);
            UnitOfWork.LoginRepository.Update(loginDetails);
            UnitOfWork.Save();

            if (isloginDetailChanged)
            {
                EmailSender.SendAdminUpdatesLoginDetailsMail(model.Email, model.FirstName, model.LastName, model.Username, model.Password);
                EmailSender.SendToSupportLoginRecoverySuccess(model.Email, model.Id);
            }
            Success(Resources.Wording.Users_EditUser_SuccessMsg);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editvet(HttpPostedFileBase imgNewUserPhoto, EditVetViewModel model, FormCollection fc)
        {
            var userId = HttpContext.User.GetUserId();
            if (!ModelState.IsValid)
            {
                if (ModelState.Keys.Contains("State") && ModelState["State"].Errors.Count > 0)
                {
                    ModelState.Remove("State");
                }
            }

            var user = UnitOfWork.UsersRepository.GetSingleTracking(i => i.Id == model.Id);
            var loginDetails = UnitOfWork.LoginRepository.GetSingleTracking(i => i.UserId == model.Id);
            var Vet = UnitOfWork.VeterinarianRepository.GetSingleTracking(i => i.UserId == model.Id);

            byte[] data = null;
            if (imgNewUserPhoto != null)
            {
                using (var reader = new System.IO.BinaryReader(imgNewUserPhoto.InputStream))
                {
                    data = reader.ReadBytes(imgNewUserPhoto.ContentLength);
                }
            }

            bool isloginDetailChanged = false;
            if (model.Password != "========")
            {
                var randomPart = Membership.GeneratePassword(5, 2);
                loginDetails.Password = Encryption.EncryptAsymetric(model.Password + randomPart);
                loginDetails.RandomPart = randomPart;
                loginDetails.IsTemporalPassword = true;
                loginDetails.UserName = Encryption.Encrypt(model.Username);
                isloginDetailChanged = true;
            }
            else if (model.Password == "========" && model.Username != Encryption.Decrypt(loginDetails.UserName))
            {
                AccountController accountcontroller = new AccountController();
                var randomPart = Membership.GeneratePassword(5, 2);
                var newPassword = accountcontroller.GenerateRandomPassword();
                loginDetails.RandomPart = randomPart;
                loginDetails.Password = Encryption.EncryptAsymetric(newPassword + randomPart);
                loginDetails.IsTemporalPassword = true;
                loginDetails.UserName = Encryption.Encrypt(model.Username);
                isloginDetailChanged = true;
                model.Password = Encryption.EncryptAsymetric(newPassword + randomPart);
            }



            model.Map(user, data, Vet);
            //adding user based on centerId
            int? centerId = HttpContext.User.GetCenterId();
            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                user = UnitOfWork.UsersRepository.GetSingleTracking(i => i.Id == model.Id);
                user.CenterID = centerId;
                UnitOfWork.UsersRepository.Update(user);
                UnitOfWork.LoginRepository.Update(loginDetails);
                UnitOfWork.VeterinarianRepository.Update(Vet);
                UnitOfWork.Save();
            }
            else
            {
                UnitOfWork.UsersRepository.Update(user);
                UnitOfWork.LoginRepository.Update(loginDetails);
                UnitOfWork.VeterinarianRepository.Update(Vet);
                UnitOfWork.Save();
            }

            if (isloginDetailChanged)
            {
                EmailSender.SendAdminUpdatesLoginDetailsMail(model.Email, model.FirstName, model.LastName, model.Username, model.Password);
                EmailSender.SendToSupportLoginRecoverySuccess(model.Email, model.Id);
            }
            Success(Resources.Wording.Users_EditUser_SuccessMsg);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdmin(HttpPostedFileBase imgNewUserPhoto, EditAdminViewModel model, FormCollection fc)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.Keys.Contains("State") && ModelState["State"].Errors.Count > 0)
                {
                    ModelState.Remove("State");
                }
            }

            var user = UnitOfWork.UsersRepository.GetSingleTracking(i => i.Id == model.Id);
            var loginDetails = UnitOfWork.LoginRepository.GetSingleTracking(i => i.UserId == model.Id);

            byte[] data = null;
            if (imgNewUserPhoto != null)
            {
                using (var reader = new System.IO.BinaryReader(imgNewUserPhoto.InputStream))
                {
                    data = reader.ReadBytes(imgNewUserPhoto.ContentLength);
                }
            }

            bool isloginDetailChanged = false;
            if (model.Password != "========")
            {
                var randomPart = Membership.GeneratePassword(5, 2);
                loginDetails.Password = Encryption.EncryptAsymetric(model.Password + randomPart);
                loginDetails.RandomPart = randomPart;
                loginDetails.IsTemporalPassword = true;
                loginDetails.UserName = Encryption.Encrypt(model.Username);
                isloginDetailChanged = true;
            }
            else if (model.Password == "========" && model.Username != Encryption.Decrypt(loginDetails.UserName))
            {
                AccountController accountcontroller = new AccountController();
                var randomPart = Membership.GeneratePassword(5, 2);
                var newPassword = accountcontroller.GenerateRandomPassword();
                loginDetails.RandomPart = randomPart;
                loginDetails.Password = Encryption.EncryptAsymetric(newPassword + randomPart);
                loginDetails.IsTemporalPassword = true;
                loginDetails.UserName = Encryption.Encrypt(model.Username);
                isloginDetailChanged = true;
                model.Password = Encryption.EncryptAsymetric(newPassword + randomPart);
            }

            model.Map(user, data);
            UnitOfWork.UsersRepository.Update(user);
            UnitOfWork.LoginRepository.Update(loginDetails);
            UnitOfWork.Save();

            if (isloginDetailChanged)
            {
                EmailSender.SendAdminUpdatesLoginDetailsMail(model.Email, model.FirstName, model.LastName, model.Username, model.Password);
                EmailSender.SendToSupportLoginRecoverySuccess(model.Email, model.Id);
            }

            Success(Resources.Wording.Users_EditUser_SuccessMsg);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ValidateUserName(string userName, int Id)
        {
            var username = Encryption.Encrypt(userName);
            var user = UnitOfWork.LoginRepository.GetSingleTracking(i => i.UserName == username);
            bool Validate = true;
            if (user != null)
            {
                Validate = false;
                if (user.UserId == Id)
                {
                    Validate = true;
                }
            }
            return Json(Validate);
        }

        [HttpGet]
        public ActionResult GetPlansByPromocode(string Promocode, bool isTrial = false, bool isGetAll = false)
        {

            var userId = HttpContext.User.GetUserId();
            var Plans = UnitOfWork.SubscriptionRepository.GetAll(p => p.PromotionCode == Promocode && !p.IsDeleted);

            if (!isTrial)
            {
                Plans = UnitOfWork.SubscriptionRepository.GetAll(p => p.PromotionCode == Promocode && p.IsTrial == isTrial && !p.IsDeleted);
            }
            if (isGetAll)
            {
                if (Promocode == Resources.Wording.Users_Index_SearchbyPromocode)
                {
                    IEnumerable<Model.Subscription> PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(p => !p.IsDeleted);
                    Plans = PlansNPromocode.GroupBy(o => o.Name).Select(g => g.First());
                }
                else
                {
                    Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.PromotionCode == Promocode && !i.IsDeleted);
                }
            }
            else
            {
                if (!Plans.Any())
                {
                    Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false && !i.IsDeleted && i.Amount != null);
                }
            }

            //getting plan by promocode based on centerId
            int? centerId = HttpContext.User.GetCenterId();
            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                UnitOfWork.UserRepository.GetAll(a => a.CenterID != null);
                if (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString()) || HttpContext.User.IsInRole(UserTypeEnum.VeterinarianLight.ToString()))
                {
                    IEnumerable<Model.Subscription> PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(p => !p.IsDeleted);
                    Plans = PlansNPromocode.Where(u => u.CenterId == centerId && u.PromotionCode == Promocode).GroupBy(o => o.Name).Select(g => g.First());
                    if (Promocode == Resources.Wording.Users_Index_SearchbyPromocode)
                    {
                        PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(p => !p.IsDeleted && p.CenterId == centerId);
                        Plans = PlansNPromocode.GroupBy(o => o.Name).Select(g => g.First());
                    }
                    if (Promocode == "")
                    {
                        Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false && !i.IsDeleted);
                    }

                    if (!Plans.Any())
                    {
                        Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false && !i.IsDeleted);
                    }
                    //Plans = PlansNPromocode.Where(u => u.CenterId == UserData.CenterID && u.PromotionCode == Promocode).GroupBy(o => o.Name).Select(g => g.First());
                }

            }

            var ListPlans = Plans.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
            var basePlanDetails = (isGetAll) ? null : Plans.First();

            var data = new { Items = ListPlans, AdditionalInfo = (basePlanDetails != null) ? basePlanDetails.AditionalInfo : "", Description = (basePlanDetails != null) ? basePlanDetails.Description : "", MaxPetCount = (basePlanDetails != null) ? basePlanDetails.MaxPetCount : 0, Resources.Wording.Users_Index_SearchbyPlan };

            return Json(data, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public ActionResult GetAllPlansByPromocode1(string Promocode, bool isTrial = false, bool isGetAll = false)
        {

            var userId = HttpContext.User.GetUserId();
            var Plans = UnitOfWork.SubscriptionRepository.GetAll(p => p.PromotionCode == Promocode && !p.IsDeleted);
            if (!isTrial)
            {
                Plans = UnitOfWork.SubscriptionRepository.GetAll(p => p.PromotionCode == Promocode && p.IsTrial == isTrial && !p.IsDeleted);
            }
            if (isGetAll)
            {
                if (Promocode == Resources.Wording.Users_Index_SearchbyPromocode)
                {
                    IEnumerable<Model.Subscription> PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(p => !p.IsDeleted);
                    Plans = PlansNPromocode.GroupBy(o => o.Name).Select(g => g.First());
                }
                else
                {
                    Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.PromotionCode == Promocode && !i.IsDeleted);
                }
            }
            else
            {
                if (!Plans.Any())
                {
                    Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false && !i.IsDeleted);
                }
            }

            //getting plan by promocode based on centerId
            int? centerId = HttpContext.User.GetCenterId();
            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                UnitOfWork.UserRepository.GetAll(a => a.CenterID != null);
                if (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString()) || HttpContext.User.IsInRole(UserTypeEnum.VeterinarianLight.ToString()))
                {
                    IEnumerable<Model.Subscription> PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(p => !p.IsDeleted);
                    Plans = PlansNPromocode.Where(u => u.CenterId == centerId && u.PromotionCode == Promocode).GroupBy(o => o.Name).Select(g => g.First());
                    if (Promocode == Resources.Wording.Users_Index_SearchbyPromocode)
                    {
                        PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(p => !p.IsDeleted && p.CenterId == centerId);
                        Plans = PlansNPromocode.GroupBy(o => o.Name).Select(g => g.First());
                    }
                    if (Promocode == "")
                    {
                        Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false && !i.IsDeleted);
                    }

                    if (!Plans.Any())
                    {
                        Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false && !i.IsDeleted);
                    }
                   
                }

            }

            var ListPlans = Plans.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
            var basePlanDetails = (isGetAll) ? null : Plans.First();

            var data = new { Items = ListPlans, AdditionalInfo = (basePlanDetails != null) ? basePlanDetails.AditionalInfo : "", Description = (basePlanDetails != null) ? basePlanDetails.Description : "", MaxPetCount = basePlanDetails.MaxPetCount, Resources.Wording.Users_Index_SearchbyPlan };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPlansByPromo(int userId, string Promocode, bool isTrial = false, bool isGetAll = false)
        {

            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);
            var subscription = user.UserSubscription.Subscription;
            var subscriptionbaseID = subscription.SubscriptionBaseId;


            var Plans = UnitOfWork.SubscriptionRepository.GetAll(p => p.PromotionCode == Promocode && !p.IsDeleted);
            if (!isTrial)
            {
                Plans = UnitOfWork.SubscriptionRepository.GetAll(p => p.PromotionCode == Promocode && p.IsTrial == isTrial && !p.IsDeleted);
            }
            if (isGetAll)
            {
                if (Promocode == Resources.Wording.Users_Index_SearchbyPromocode)
                {
                    IEnumerable<Model.Subscription> PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(p => !p.IsDeleted);
                    Plans = PlansNPromocode.GroupBy(o => o.Name).Select(g => g.First());
                }
                else
                {
                    Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.PromotionCode == Promocode && !i.IsDeleted && i.IsBase && !i.IsPromotionCode);
                }
            }
            else
            {
                if (!Plans.Any())
                {
                    Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false && !i.IsDeleted && i.Amount != null);
                }
            }

            if (subscription.IsPromotionCode)
            {
                if (Promocode == user.UserSubscription.Subscription.PromotionCode)
                {
                    if (subscriptionbaseID != null)
                    {
                        Plans = Plans.Where(i => i.Id == subscriptionbaseID);
                        subscription = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == subscriptionbaseID);
                    }
                }
            }
            //getting plan based on the Center Id
            int? centerId = HttpContext.User.GetCenterId();
            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                UnitOfWork.UserRepository.GetAll(a => a.CenterID != null);
                if (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString()) || HttpContext.User.IsInRole(UserTypeEnum.VeterinarianLight.ToString()))
                {
                    IEnumerable<Model.Subscription> PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(p => !p.IsDeleted);
                    Plans = PlansNPromocode.Where(u => u.CenterId == centerId && u.PromotionCode == Promocode).GroupBy(o => o.Name).Select(g => g.First());
                    if (Promocode == Resources.Wording.Users_Index_SearchbyPromocode)
                    {
                        Plans = PlansNPromocode.Where(a => a.CenterId == centerId).GroupBy(o => o.Name).Select(g => g.First());
                    }
                    if (Promocode == "")
                    {
                        Plans = UnitOfWork.SubscriptionRepository.GetAll(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false && !i.IsDeleted);
                    }
                }
            }

            var ListPlans = Plans.Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
            var basePlanDetails = (isGetAll) ? null : Plans.First();
            var data = new { Items = ListPlans, AdditionalInfo = (basePlanDetails != null) ? basePlanDetails.AditionalInfo : "", MaxPetCount = basePlanDetails.MaxPetCount, Description = (basePlanDetails != null) ? basePlanDetails.Description : Resources.Wording.Users_Index_SearchbyPlan };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public string GetPlanName(Model.Subscription subscription, decimal totalAmt, int totalPets, int TotalMra, int totalSmo)
        {
            return subscription.Name;
        }

        [HttpPost]
        public JsonResult ValidateEmail(int Id, string Email)
        {
            var valid = UnitOfWork.UserRepository.IfMailAlreadyExist(Email, Id);
            return Json(valid);
        }

        [HttpGet]
        public ActionResult UpgradePlan(int id)
        {
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == id, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);
            int usrPlanId = user.UserSubscription.Subscription.Id;
            ViewBag.Discription = user.UserSubscription.Subscription.Description;
            var PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted && s.Id != usrPlanId);
            ViewBag.Plans = PlansNPromocode.Where(i => i.PromotionCode == user.UserSubscription.Subscription.PromotionCode && i.IsTrial == false).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
            ViewBag.Promocode = PlansNPromocode.GroupBy(o => o.PromotionCode).Select(g => g.First()).Where(u => u.IsPromotionCode).Select(i => new SelectListItem { Text = i.PromotionCode, Value = i.PromotionCode }).OrderBy(a => a.Text);
            var subscription = PlansNPromocode.Where(i => i.PromotionCode == user.UserSubscription.Subscription.PromotionCode && i.IsTrial == false).OrderBy(i => i.Name).First();
            var subscriptionbaseID = subscription.SubscriptionBaseId;
            if (subscription.IsPromotionCode)
            {
                ViewBag.Plans = PlansNPromocode.Where(i => i.PromotionCode == subscription.PromotionCode && i.IsTrial == false).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);

                if (subscriptionbaseID != null)
                {
                    ViewBag.Plans = PlansNPromocode.Where(i => i.Id == subscriptionbaseID).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
                    subscription = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == subscriptionbaseID);
                }
            }
            var planName = subscription.Name;
            var additionalPets = user.UserSubscription.SubscriptionService == null ? 0 : user.UserSubscription.SubscriptionService.AditionalPetCount ?? 0;
            var totalAmmount = subscription.Amount;
            var totalMra = additionalPets + subscription.MRACount;
            var totalPets = additionalPets + subscription.MaxPetCount;
            var totalSmo = subscription.SMOCount;
            var finalPlan = subscription.Description;
            var startDate = user.UserSubscription.RenewalDate ?? DateTime.Now;
            startDate = startDate.AddDays(1);

            if (DomainHelper.GetDomain() == DomainTypeEnum.India || DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                if (string.IsNullOrEmpty(user.UserSubscription.Subscription.PaymentTypeId.ToString()) && user.UserSubscription.Subscription.IsBase)//==> "Free Account With Limited Access")
                {
                    startDate = DateTime.Now;
                }

                if (user.UserSubscription.Subscription.Amount == null && user.UserSubscription.Subscription.IsBase && user.UserSubscription.Subscription.PlanTypeId == PlanTypeEnum.BasicFree)//==> "Free Account With Limited Access")
                {

                    startDate = DateTime.Now;        
                }
            }

            var endDate = getRenewalDate(subscription, startDate);
            if (user.UserSubscription.RenewalDate < DateTime.Today)
            {
                endDate = DateTime.Today;
                if (subscription.PaymentTypeId == PaymentTypeEnum.Yearly)
                {
                    endDate = endDate.AddYears(1);
                }
                else if (subscription.PaymentTypeId == PaymentTypeEnum.Monthly)
                {
                    endDate = endDate.AddMonths(1);
                }
                else if (subscription.PaymentTypeId == PaymentTypeEnum.SaleTransaction || subscription.PaymentTypeId == PaymentTypeEnum.ThirdPartyCompany)
                {
                    //todo: refactor this
                    endDate = endDate.AddYears(100);
                }
                else
                {
                    endDate = endDate.AddDays(subscription.Duration);
                }

                startDate = DateTime.Today;
                endDate = endDate.AddDays(-1);
            }

            var timezoneID = user.TimeZone != null ? user.TimeZone.TimeZoneInfoId : TimeZoneInfo.Local.Id;
            startDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startDate, timezoneID);
            endDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(endDate, timezoneID);
            return PartialView(new UpgradePlanViewModel(user, subscription, planName, finalPlan, additionalPets, startDate, endDate, totalAmmount));
          
        }

        [HttpPost]
        public ActionResult UpgradePlan(UpgradePlanViewModel model)
        {
            var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == model.UserSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users);
            var user = userSubscription.Users.First();
            model.PlanID = Convert.ToInt32(model.PlanName);
            var UserSub = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == model.PlanID);
            var UserIdChange = UnitOfWork.UserRepository.GetSingleTracking(s => s.Id == model.UserID);

            UserIdChange.CenterID = UserSub.CenterId;
            UnitOfWork.UserRepository.Update(UserIdChange);
            UnitOfWork.Save();

            var subscriptionHistory = model.Map(userSubscription);
            UnitOfWork.UserSubscriptionHistoryRepository.Insert(subscriptionHistory);
            userSubscription.ispaymentDone = false;

            UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
            UnitOfWork.Save();

            var MaxPetCount = UserSub.MaxPetCount;

            var PriceInfo = UnitOfWork.SubscriptionRepository.GetSingleTracking(u => u.Id == model.PlanID);
            var Price = PriceInfo.Amount;
            var createdUser = (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString())) == true ? EnumHelper.GetResourceValueForEnumValue(UserTypeEnum.VeterinarianAdo)
                : (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianLight.ToString())) == true ? EnumHelper.GetResourceValueForEnumValue(UserTypeEnum.VeterinarianLight)
                : (HttpContext.User.IsInRole(UserTypeEnum.Assistant.ToString())) == true ? EnumHelper.GetResourceValueForEnumValue(UserTypeEnum.Assistant)
                : EnumHelper.GetResourceValueForEnumValue(UserTypeEnum.Admin);

            EmailSender.SendAdminUpdatesPlanMail(Resources.Wording.Profile_PlanUpgrade_Title, Resources.Wording.Users_Edit_upgradedplan, user.Email, user.FirstName, user.LastName, createdUser, model.StartDate, model.EndDate, PriceInfo.Name, model.Promocode, MaxPetCount.ToString(), DomainHelper.GetCurrency() + Price);
            return RedirectToAction("ViewUser", new { id = model.UserID });
        }

        [HttpGet]
        public ActionResult RenewPlan(int id)
        {
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == id, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);
            var defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted).OrderBy(u => u.MaxPetCount);
            var subscription = user.UserSubscription.Subscription;
            var PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted);
            ViewBag.Plans = PlansNPromocode.Where(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false && i.Amount != null).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
            var userId = HttpContext.User.GetUserId();
            int? centerId = HttpContext.User.GetCenterId();
            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                if (!string.IsNullOrEmpty(subscription.PromotionCode))
                    PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted && s.CenterId == centerId);
                else
                    PlansNPromocode = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted);

                ViewBag.Plans = PlansNPromocode.Where(i => i.IsBase && !i.IsPromotionCode && i.IsVisibleToOwner && i.IsTrial == false && i.Amount != null).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);

            }
            var subscriptionbaseID = subscription.SubscriptionBaseId;
            if (subscription.IsPromotionCode)
            {
                ViewBag.Plans = PlansNPromocode.Where(i => i.PromotionCode == subscription.PromotionCode && i.IsTrial == false && i.Amount != null).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
                if (subscriptionbaseID != null)
                {
                    ViewBag.Plans = PlansNPromocode.Where(i => i.Id == subscriptionbaseID).Select(i => new SelectListItem { Text = i.Name, Value = i.Id.ToString() }).OrderBy(a => a.Text);
                    subscription = UnitOfWork.SubscriptionRepository.GetSingle(u => u.Id == subscriptionbaseID);
                }
            }
            var planName = subscription.Name;
            var additionalPets = user.UserSubscription.SubscriptionService == null ? 0 : user.UserSubscription.SubscriptionService.AditionalPetCount ?? 0;
            var totalAmmount = subscription.Amount + (subscription.AmmountPerAddionalPet * additionalPets);
            var totalMra = additionalPets + subscription.MRACount;
            var totalPets = additionalPets + subscription.MaxPetCount;
            var totalSmo = subscription.SMOCount;
            var finalPlan = subscription.Description;
            ViewBag.Promocode = PlansNPromocode.GroupBy(o => o.PromotionCode).Select(g => g.First()).Where(u => u.IsPromotionCode && u.PromotionCode != Constants.FreeUserPromoCode).Select(i => new SelectListItem { Text = i.PromotionCode, Value = i.PromotionCode }).OrderBy(a => a.Text);

            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                ViewBag.Promocode = PlansNPromocode.GroupBy(o => o.PromotionCode).Select(g => g.First()).Where(u => u.IsPromotionCode && u.PromotionCode != Constants.FreeUserPromoCode && u.CenterId == centerId).Select(i => new SelectListItem { Text = i.PromotionCode, Value = i.PromotionCode }).OrderBy(a => a.Text);

            }
            var listItems = Enumerable.Range(1, 10).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() }).ToList();
            listItems.Add(new SelectListItem { Value = "0", Text = Resources.Wording.Profile_PlanEdit_addpets });
            ViewBag.AdditionalPets = listItems;
            var startDate = user.UserSubscription.RenewalDate ?? DateTime.Now;
            startDate = startDate.AddDays(1);
            var endDate = getRenewalDate(subscription, startDate);
            if (user.UserSubscription.RenewalDate < DateTime.Today)
            {
                endDate = DateTime.Today;
                if (subscription.PaymentTypeId == PaymentTypeEnum.Yearly)
                {
                    endDate = endDate.AddYears(1);
                }
                else if (subscription.PaymentTypeId == PaymentTypeEnum.Monthly)
                {
                    endDate = endDate.AddMonths(1);
                }
                else if (subscription.PaymentTypeId == PaymentTypeEnum.SaleTransaction || subscription.PaymentTypeId == PaymentTypeEnum.ThirdPartyCompany)
                {
                    //todo: refactor this
                    endDate = endDate.AddYears(100);
                }
                else
                {
                    endDate = endDate.AddDays(subscription.Duration);
                }

                startDate = DateTime.Today;
                endDate = endDate.AddDays(-1);
            }
            var timezoneID = user.TimeZone != null ? user.TimeZone.TimeZoneInfoId : TimeZoneInfo.Local.Id;
            startDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(startDate, timezoneID);
            endDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(endDate, timezoneID);




            return PartialView(new RenewPlanViewModel(user, subscription, planName, finalPlan, additionalPets, startDate, endDate));
        }

        [HttpPost]
        public ActionResult RenewPlan(RenewPlanViewModel model)
        {
            var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == model.UserSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users);
            var user = userSubscription.Users.First();
            model.PlanID = Convert.ToInt32(model.PlanID);
            var UserSub = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == model.PlanID);
            var UserIdChange = UnitOfWork.UserRepository.GetSingleTracking(s => s.Id == model.UserID);

            var MaxPetCount = UnitOfWork.SubscriptionRepository.GetSingleTracking(s => s.Id == model.PlanID).MaxPetCount;
            if (UserSub.CenterId != null)
            {
                UserIdChange.CenterID = UserSub.CenterId;
                UnitOfWork.UserRepository.Update(UserIdChange);
                UnitOfWork.Save();
            }
            else
            {
                UserIdChange.CenterID = UserSub.CenterId;
                UnitOfWork.UserRepository.Update(UserIdChange);
                UnitOfWork.Save();
            }
            var subscriptionHistory = model.Map(model, userSubscription);
            UnitOfWork.UserSubscriptionHistoryRepository.Insert(subscriptionHistory);
            UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
            UnitOfWork.Save();

            
            var PriceInfo = UnitOfWork.SubscriptionRepository.GetSingleTracking(u => u.Id == model.PlanID);
            var Price = PriceInfo.Amount;
            var createdUser = (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString())) == true ? EnumHelper.GetResourceValueForEnumValue(UserTypeEnum.VeterinarianAdo)
               : (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianLight.ToString())) == true ? EnumHelper.GetResourceValueForEnumValue(UserTypeEnum.VeterinarianLight)
               : (HttpContext.User.IsInRole(UserTypeEnum.Assistant.ToString())) == true ? EnumHelper.GetResourceValueForEnumValue(UserTypeEnum.Assistant)
               : EnumHelper.GetResourceValueForEnumValue(UserTypeEnum.Admin);

            EmailSender.SendAdminUpdatesPlanMail(Resources.Wording.Profile_PlanDetail_RenewalPlan, Resources.Wording.Users_Edit_renewedplan, user.Email, user.FirstName, user.LastName, createdUser, model.StartDate, model.EndDate, PriceInfo.Name, model.Promocode, MaxPetCount.ToString(), DomainHelper.GetCurrency() + Price);

            return RedirectToAction("ViewUser", new { id = model.UserID });
        }

        [HttpGet]
        public ActionResult AddPets(int id)
        {
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == id, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);
            var defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(s => !s.IsDeleted).OrderBy(u => u.MaxPetCount);
            var subscription = user.UserSubscription.Subscription;
            var planName = subscription.Name;
            var additionalPets = user.UserSubscription.SubscriptionService == null ? 0 : user.UserSubscription.SubscriptionService.AditionalPetCount ?? 0;
            var totalAmmount = subscription.Amount + (subscription.AmmountPerAddionalPet * additionalPets);
            var totalMra = additionalPets + subscription.MRACount;
            var totalPets = additionalPets + subscription.MaxPetCount;
            var totalSmo = subscription.SMOCount;
            var finalPlan = GetPlanName(subscription, totalAmmount ?? 0, totalPets, totalMra ?? 0, totalSmo ?? 0);
            var listItems = Enumerable.Range(1, 10).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() }).ToList();
            listItems.Add(new SelectListItem { Value = "0", Text = Resources.Wording.Profile_PlanEdit_addpets });
            ViewBag.AdditionalPets = listItems;
            return PartialView(new AddPetsViewModel(user, planName, finalPlan, additionalPets));
        }


        [HttpPost]
        public ActionResult AddPets(AddPetsViewModel model)
        {

            var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == model.UserSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users, u => u.PaymentHistories);
            var user = userSubscription.Users.First();
            var flagPayment = false;
            if (userSubscription.PaymentHistories != null && userSubscription.PaymentHistories.Count() > 0)
            {
                flagPayment = true;
            }

            var subscriptionHistory = model.Map(model, userSubscription, flagPayment);

            UnitOfWork.UserSubscriptionHistoryRepository.Insert(subscriptionHistory);
            UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
            UnitOfWork.Save();

            var PriceInfo = UnitOfWork.SubscriptionRepository.GetSingleTracking(u => u.Id == model.PlanId);
            var numberofPets = (model.AdditionalPets - model.NumberOfPets);
            var Price = (PriceInfo.AmmountPerAddionalPet * numberofPets);
            var createdUser = (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianAdo.ToString())) == true ? EnumHelper.GetResourceValueForEnumValue(UserTypeEnum.VeterinarianAdo)
                : (HttpContext.User.IsInRole(UserTypeEnum.VeterinarianLight.ToString())) == true ? EnumHelper.GetResourceValueForEnumValue(UserTypeEnum.VeterinarianLight)
                : (HttpContext.User.IsInRole(UserTypeEnum.Assistant.ToString())) == true ? EnumHelper.GetResourceValueForEnumValue(UserTypeEnum.Assistant)
                : EnumHelper.GetResourceValueForEnumValue(UserTypeEnum.Admin);

            EmailSender.SendAdminUpdatesPlanMail(Resources.Wording.Users_Edit_AdditionalPets, Resources.Wording.Users_Edit_addedadditionalpets, user.Email, user.FirstName, user.LastName, createdUser, model.StartDate, model.EndDate, PriceInfo.Name, model.Promocode, numberofPets.ToString(), DomainHelper.GetCurrency() + Price);

            return RedirectToAction("ViewUser", new { id = model.UserID });
        }

        [HttpGet]
        public ActionResult PlanRenewalDetails(int id)
        {
            return PartialView("PlanRenewalDetails");
        }

        public DateTime getRenewalDate(Model.Subscription subscription, DateTime Startdate)
        {
            var renewalDate = Startdate;

            if (subscription.PaymentTypeId == PaymentTypeEnum.Yearly)
            {
                renewalDate = renewalDate.AddYears(1);
            }
            else if (subscription.PaymentTypeId == PaymentTypeEnum.Monthly)
            {
                renewalDate = renewalDate.AddMonths(1);
            }
            else if (subscription.PaymentTypeId == PaymentTypeEnum.SaleTransaction || subscription.PaymentTypeId == PaymentTypeEnum.ThirdPartyCompany)
            {
                //todo: refactor this
                renewalDate = renewalDate.AddYears(100);
            }
            else
            {
                renewalDate = renewalDate.AddDays(subscription.Duration);
            }


            renewalDate = renewalDate.AddDays(-1);
            return renewalDate;
        }

        public ActionResult SendEmailToOwner(int userId)
        {
            var usr = UnitOfWork.UsersRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService);
            AccountController accountcontroller = new AccountController();

            var credentials = UnitOfWork.LoginRepository.GetSingle(l => l.UserId == userId);
            var userName = Encryption.Decrypt(credentials.UserName);
            var randomPart = Membership.GeneratePassword(5, 2);
            var newPassword = accountcontroller.GenerateRandomPassword();
            credentials.RandomPart = randomPart;
            credentials.Password = Encryption.EncryptAsymetric(newPassword + randomPart);
            credentials.IsTemporalPassword = true;

            UnitOfWork.LoginRepository.Update(credentials);
            UnitOfWork.Save();
            if (usr.UserTypeId == UserTypeEnum.OwnerAdmin)
            {
                if (usr.UserSubscription != null && usr.UserSubscription.Subscription.PromotionCode == "HSBC")
                {
                    EmailSender.SendHsbcWelcomeMail(usr.Email, usr.FirstName, usr.LastName, usr.UserSubscription.Subscription.Name, usr.UserSubscription.Subscription.PromotionCode);
                    EmailSender.SendHsbcAccountActivationMail(usr.Email, usr.FirstName, usr.LastName, userName, newPassword);
                }
                else
                {
                    EmailSender.SendWelcomeMail(usr.Email, usr.FirstName, usr.LastName, usr.UserSubscription.Subscription.Name, usr.UserSubscription.Subscription.PromotionCode);
                    EmailSender.SendAccountActivationMail(usr.Email, usr.FirstName, usr.LastName, userName, newPassword);
                }

                int petCount = (usr.UserSubscription.SubscriptionService != null ? usr.UserSubscription.SubscriptionService.AditionalPetCount : 0) ?? 0;
                petCount = petCount + (usr.UserSubscription.AdditionalPetcount != null ? usr.UserSubscription.AdditionalPetcount : 0) ?? 0;


                if (usr.UserSubscription.ispaymentDone)
                {// users imported by hsbc importuser
                    EmailSender.SendToSupportSubscriptionMail(usr.FirstName, usr.LastName, usr.Email, usr.UserSubscription.Subscription.PromotionCode, usr.UserSubscription.Subscription.Name, usr.UserSubscription.Subscription.Amount.ToString(), "ImportUser");
                }
                else
                {
                    EmailSender.SendToSupportAccountActivationMail(usr.CreationDate.ToShortDateString(), usr.Id.ToString(), usr.Email, usr.FirstName, usr.LastName,
                        usr.UserSubscription.Subscription.Name, usr.UserSubscription.Subscription.PromotionCode, petCount.ToString(),
                        usr.UserSubscription.StartDate.Value.ToShortDateString(), usr.UserSubscription.RenewalDate.Value.ToShortDateString());
                }

            }
            else
            {
                EmailSender.SendAccountActivationMail(usr.Email, usr.FirstName, usr.LastName, userName, newPassword);
            }

            usr.IsEmailSent = true;



            UnitOfWork.UserRepository.Update(usr);
            UnitOfWork.Save();

            Success(Resources.Wording.Users_IndexUser_EmailSentSuccessMsg);
            return RedirectToAction("Index");
        }


        public void DeletePet(int id, int userId)
        {
            var pet = (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                ? UnitOfWork.PetRepository.GetSingle(p => p.Id == id, p => p.Insurances, p => p.Farmer, p => p.PetContacts, p => p.Veterinarians, p => p.Users)
                : UnitOfWork.PetRepository.GetSingle(p => p.Id == id && p.Users.Any(u => u.Id == userId), p => p.Insurances, p => p.Farmer, p => p.PetContacts, p => p.Veterinarians, p => p.Users);
            if (pet != null)
            {
                pet.IsDeleted = true;

                UnitOfWork.PetRepository.Update(pet);

                UnitOfWork.Save();
                Success(Resources.Wording.Pet_Card_DeleteMsg);
                Session[Constants.SessionCurrentUserPetCount] = UnitOfWork.PetRepository.GetPetCountByUser(userId);
            }

            var userSubscriptionId = User.GetUserSubscriptionId();
            var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == userSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users);
            var user = userSubscription.Users.First();

            var subscriptionHistory = new UserSubscriptionHistory
            {
                SubscriptionId = userSubscription.SubscriptionId,
                StartDate = userSubscription.StartDate ?? DateTime.Today,
                EndDate = userSubscription.RenewalDate ?? DateTime.Today,
                PurchaseDate = DateTime.Today,
                UsedDate = DateTime.Today,
                RenewvalDate = userSubscription.RenewalDate ?? DateTime.Today,
                AditionalPetCount = userSubscription.SubscriptionService.AditionalPetCount,
                UserId = userId
            };
            var TotalPets = userSubscription.SubscriptionService.AditionalPetCount;
            userSubscription.SubscriptionService.AditionalPetCount = TotalPets - 1;
            UnitOfWork.UserSubscriptionHistoryRepository.Insert(subscriptionHistory);
            UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
            UnitOfWork.Save();
        }

        public void DeleteUnUsedPets(int numberOfPets, int userId)
        {
            var userdetails = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId);
            var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(u => u.Id == userdetails.UserSubscriptionId, u => u.SubscriptionService, u => u.Subscription, u => u.Users);
            var user = userSubscription.Users.First();

            var subscriptionHistory = new UserSubscriptionHistory
            {
                SubscriptionId = userSubscription.SubscriptionId,
                StartDate = userSubscription.StartDate ?? DateTime.Today,
                EndDate = userSubscription.RenewalDate ?? DateTime.Today,
                PurchaseDate = DateTime.Today,
                UsedDate = DateTime.Today,
                RenewvalDate = userSubscription.RenewalDate ?? DateTime.Today,
                AditionalPetCount = userSubscription.SubscriptionService.AditionalPetCount,
                UserId = userId
            };
            var TotalPets = userSubscription.SubscriptionService.AditionalPetCount;
            userSubscription.SubscriptionService.AditionalPetCount = TotalPets - numberOfPets;
            UnitOfWork.UserSubscriptionHistoryRepository.Insert(subscriptionHistory);
            UnitOfWork.UserSubscriptionRepository.Update(userSubscription);
            UnitOfWork.Save();
        }

        public ActionResult EditUserStatus(FormCollection fc)
        {
            int userId = Convert.ToInt32(fc["Id"].ToString());

            var usr = UnitOfWork.UsersRepository.GetSingleTracking(u => u.Id == userId);
            if (usr != null)
            {
                usr.UserStatusId = UserStatusEnum.Active;
                UnitOfWork.UsersRepository.Update(usr);
                UnitOfWork.Save();

                AccountController accountcontroller = new AccountController();

                var credentials = UnitOfWork.LoginRepository.GetSingle(l => l.UserId == userId);
                var userName = Encryption.Decrypt(credentials.UserName);
                var randomPart = Membership.GeneratePassword(5, 2);
                var newPassword = accountcontroller.GenerateRandomPassword();
                credentials.RandomPart = randomPart;
                credentials.Password = Encryption.EncryptAsymetric(newPassword + randomPart);
                credentials.IsTemporalPassword = true;

                UnitOfWork.LoginRepository.Update(credentials);
                UnitOfWork.Save();

                EmailSender.SendStatusChangeToOwner(usr.Email, usr.FirstName, usr.LastName, userName, newPassword);

                Success(Resources.Wording.Users_UpdateUserStatus_SuccessMsg);
            }

            return RedirectToAction("ViewUser", new { id = userId });
        }

        public ActionResult MarkPaymentDone(int id, bool isPaymentDone)
        {
            try
            {
                var usr = UnitOfWork.UsersRepository.GetSingleTracking(u => u.Id == id, navigationProperties: u => u.UserSubscription);
                if (usr != null)
                {
                    if (isPaymentDone)
                    {
                        usr.UserStatusId = UserStatusEnum.Active;
                        usr.UserSubscription.ispaymentDone = true;
                        UnitOfWork.UsersRepository.Update(usr);
                        UnitOfWork.Save();

                        AccountController accountcontroller = new AccountController();

                        var credentials = UnitOfWork.LoginRepository.GetSingle(l => l.UserId == id);
                        var userName = Encryption.Decrypt(credentials.UserName);
                        var randomPart = Membership.GeneratePassword(5, 2);
                        var newPassword = accountcontroller.GenerateRandomPassword();
                        credentials.RandomPart = randomPart;
                        credentials.Password = Encryption.EncryptAsymetric(newPassword + randomPart);
                        credentials.IsTemporalPassword = true;

                        UnitOfWork.LoginRepository.Update(credentials);
                        UnitOfWork.Save();

                        EmailSender.SendStatusChangeToOwner(usr.Email, usr.FirstName, usr.LastName, userName, newPassword);


                        Success(Resources.Wording.Users_UpdateUserPaidStatus_SuccessMsg);
                    }
                    else
                    {
                        usr.UserStatusId = UserStatusEnum.Active;
                        usr.UserSubscription.ispaymentDone = false;
                        UnitOfWork.UsersRepository.Update(usr);
                        UnitOfWork.Save();

                       
                    }
                }
            }
            catch { }

            return RedirectToAction("Index");
        }
    }
}

