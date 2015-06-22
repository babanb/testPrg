using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ADOPets.Web.Common;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.ViewModels.Pet;
using Model;
using WebGrease.Css.Extensions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq.Expressions;
using ADOPets.Web.ViewModels.HealthMeasureTracker;

namespace ADOPets.Web.Controllers
{
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class PetController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            var userId = id;
            ViewBag.OwnerId = 0;
            ViewBag.TotalPetCount = 0;
            var pets = new List<IndexViewModel>();
            if (userId == 0)
            {
                if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                {
                    var dbPets = UnitOfWork.PetRepository.GetAll(navigationProperties: p => p.Users);
                    dbPets.ForEach(p =>
                    {
                        var petOwner = p.Users.FirstOrDefault();
                        if (petOwner != null)
                        {
                            pets.Add(new IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, petOwner.UserStatusId, p.GenderId, p.Image,
                                petOwner.Id, petOwner.FirstName, petOwner.LastName, petOwner.InfoPath));
                        }
                    });


                   
                    //var UserData = UnitOfWork.UserRepository.GetSingleTracking(a => a.Id == userId1);
                    int? centerId = HttpContext.User.GetCenterId();
                    if (!string.IsNullOrEmpty(centerId.ToString()))
                    {

                        dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.CenterID == centerId), navigationProperties: p => p.Users);
                        pets = new List<IndexViewModel>();
                        dbPets.ForEach(p =>
                        {
                            var petOwner = p.Users.FirstOrDefault();
                            if (petOwner != null)
                            {
                                pets.Add(new IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, petOwner.UserStatusId, p.GenderId, p.Image,
                                    petOwner.Id, petOwner.FirstName, petOwner.LastName, petOwner.InfoPath));
                            }
                        });

                    }
                  if (Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                  {
                      var userId1 = HttpContext.User.GetUserId();
                      var listSMOUser = UnitOfWork.SMOExpertRepository.GetAll(s => s.VetExpertID == userId1, null, s => s.SMORequest, s => s.SMORequest.Pet).OrderByDescending(s => s.SMORequest.RequestDate).Distinct().Select(a => a.SMORequest.PetId);
                      var listECUser = UnitOfWork.EConsultationRepository.GetAll(s => s.VetId == userId1, null, s => s.Pet, s => s.User).Distinct().Select(a => a.PetId);
                      var assignedUser = listSMOUser.Concat(listECUser);
                      dbPets = UnitOfWork.PetRepository.GetAll(p => assignedUser.Contains(p.Id), navigationProperties: p => p.Users);
                      pets = new List<IndexViewModel>();
                      dbPets.ForEach(p =>
                      {
                          var petOwner = p.Users.FirstOrDefault();
                          if (petOwner != null)
                          {
                              pets.Add(new IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, petOwner.UserStatusId, p.GenderId, p.Image,
                                  petOwner.Id, petOwner.FirstName, petOwner.LastName, petOwner.InfoPath));
                          }
                      });
                    
                         centerId = HttpContext.User.GetCenterId();
                      if (!string.IsNullOrEmpty(centerId.ToString()))
                      {
                          dbPets = UnitOfWork.PetRepository.GetAll(p => assignedUser.Contains(p.Id), navigationProperties: p => p.Users);
                          pets = new List<IndexViewModel>();
                          dbPets.ForEach(p =>
                          {
                              var petOwner = p.Users.FirstOrDefault();
                              if (petOwner != null)
                              {
                                  pets.Add(new IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, petOwner.UserStatusId, p.GenderId, p.Image,
                                      petOwner.Id, petOwner.FirstName, petOwner.LastName, petOwner.InfoPath));
                              }
                          });  
                      
                      
                      }
                  
                  }

                }
                else
                {
                    var usrId = HttpContext.User.GetUserId();
                    var dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == usrId), navigationProperties: p => p.Users);

                    var petOwner = UnitOfWork.UserRepository.GetSingleTracking(u => u.Id == usrId);

                    pets = dbPets.Select(p => new IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, p.GenderId, p.Image, petOwner)).ToList();
                    checkRenual(usrId);
                }


               
                Session[Constants.SessionCurrentUserPetCount] = pets.Count;

                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()) || Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                {
                    Session[Constants.SessionCurrentUserRequestFrom] = "pet";

                    return View("Index_VD", pets);
                }
                return View(pets);
            }
            else
            {
                var dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == userId), navigationProperties: p => p.Users);

                dbPets.ForEach(p =>
                {
                    var petOwner = p.Users.FirstOrDefault();
                    if (petOwner != null)
                    {
                        pets.Add(new IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, petOwner.UserStatusId, p.GenderId, p.Image,
                            petOwner.Id, petOwner.FirstName, petOwner.LastName, petOwner.InfoPath));
                    }
                });

                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                {
                    var userId1 = HttpContext.User.GetUserId();
                    var listSMOUser = UnitOfWork.SMOExpertRepository.GetAll(s => s.VetExpertID == userId1 && s.SMORequest.UserId == userId, null, s => s.SMORequest, s => s.SMORequest.Pet).OrderByDescending(s => s.SMORequest.RequestDate).Distinct().Select(a => a.SMORequest.PetId);
                    var listECUser = UnitOfWork.EConsultationRepository.GetAll(s => s.VetId == userId1 && s.UserId == userId, null, s => s.Pet, s => s.User).Distinct().Select(a => a.PetId);
                    var assignedUser = listSMOUser.Concat(listECUser);
                    dbPets = UnitOfWork.PetRepository.GetAll(p => assignedUser.Contains(p.Id), navigationProperties: p => p.Users);
                    pets = new List<IndexViewModel>();

                    dbPets.ForEach(p =>
                    {
                        var petOwner = p.Users.FirstOrDefault();
                        if (petOwner != null)
                        {
                            pets.Add(new IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, petOwner.UserStatusId, p.GenderId, p.Image,
                                petOwner.Id, petOwner.FirstName, petOwner.LastName, petOwner.InfoPath));
                        }
                    });
                  var centerId = HttpContext.User.GetCenterId();
                if (!string.IsNullOrEmpty(centerId.ToString()))
                {
                    dbPets = UnitOfWork.PetRepository.GetAll(p => assignedUser.Contains(p.Id), navigationProperties: p => p.Users).Where(p => p.Users.Any(u => u.CenterID == centerId));
                    pets = new List<IndexViewModel>();

                    dbPets.ForEach(p =>
                    {
                        var petOwner = p.Users.FirstOrDefault();
                        if (petOwner != null)
                        {
                            pets.Add(new IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, petOwner.UserStatusId, p.GenderId, p.Image,
                                petOwner.Id, petOwner.FirstName, petOwner.LastName, petOwner.InfoPath));
                        }
                    });
                
                
                
                }

                }


                //    checkRenual(userId);
                Session[Constants.SessionCurrentUserPetCount] = pets.Count;
                try
                {
                    //var userDetail = dbPets.FirstOrDefault().Users.FirstOrDefault();

                    var userDetail = UnitOfWork.UserRepository.GetSingleTracking(u => u.Id == userId, u => u.UserSubscription, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService);

                    if (userDetail != null)
                    {
                        var aditionalPetCount = userDetail.UserSubscription.SubscriptionService != null && userDetail.UserSubscription.SubscriptionService.AditionalPetCount.HasValue
                   ? userDetail.UserSubscription.SubscriptionService.AditionalPetCount.Value
                   : 0;

                        ViewBag.OwnerId = userDetail.Id;
                        ViewBag.TotalPetCount = (userDetail.UserSubscription.Subscription.MaxPetCount + aditionalPetCount);
                        ViewBag.OwnerFirstName = userDetail.FirstName.Value;
                        ViewBag.OwnerLastName = userDetail.LastName.Value;
                    }
                }
                catch { }

                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()) || Roles.IsUserInRole(UserTypeEnum.Admin.ToString()))
                {
                    Session[Constants.SessionCurrentUserRequestFrom] = "user";
                }
                return View(pets);
            }
        }

        [HttpGet]
        public ActionResult Add(int id = 0)
        {
            var userId = id;
            int MaxPetcount = 0;
            if (userId != 0)
            {
                if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()))
                {
                    var pets = new List<IndexViewModel>();

                    var dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == userId), navigationProperties: p => p.Users);
                    dbPets.ForEach(p =>
                    {
                        var petOwner = p.Users.FirstOrDefault();
                        if (petOwner != null)
                        {
                            pets.Add(new IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, petOwner.UserStatusId, p.GenderId, p.Image,
                                petOwner.Id, petOwner.FirstName, petOwner.LastName, petOwner.InfoPath));
                        }
                    });

                    Session[Constants.SessionCurrentUserPetCount] = pets.Count;

                    Expression<Func<User, object>> parames1 = v => v.UserSubscription;
                    Expression<Func<User, object>> parames2 = v => v.UserSubscription.Subscription;
                    Expression<Func<User, object>> parames3 = v => v.UserSubscription.SubscriptionService;

                    Expression<Func<User, object>>[] paramesArray = new Expression<Func<User, object>>[] { parames1, parames2, parames3 };


                    var user = UnitOfWork.UsersRepository.GetSingle(u => u.Id == userId, navigationProperties: paramesArray);

                    var aditionalPetCount = user.UserSubscription.SubscriptionService != null && user.UserSubscription.SubscriptionService.AditionalPetCount.HasValue
                          ? user.UserSubscription.SubscriptionService.AditionalPetCount.Value : 0;

                    MaxPetcount = user.UserSubscription.Subscription.MaxPetCount + aditionalPetCount;
                    Session["UserIdAddPet"] = user.Id;
                }
            }
            if (SubscriptionConstraintHelper.CanAddNewPet(userId, MaxPetcount))
            {
                return View(new AddViewModel());
            }
            if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()))
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(HttpPostedFileBase imageFile, AddViewModel model, FormCollection fc)
        {
            if (ModelState.Keys.Contains("IsSterile") && ModelState["IsSterile"].Errors.Count > 0)
            {
                ModelState.Remove("IsSterile");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var fileName = string.Empty;
            var coverFileName = string.Empty;
            User userInfo = null;
            if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()))
            {
                int usrId = Convert.ToInt32(Session["UserIdAddPet"].ToString());
                userInfo = UnitOfWork.UsersRepository.GetSingle(u => u.Id == usrId);
            }

            var userId = (userInfo == null) ? HttpContext.User.GetUserId() : userInfo.Id;
            var user = UnitOfWork.UserRepository.GetSingleTracking(u => u.Id == userId);

            #region Prof Pic
            if (imageFile != null && imageFile.ContentLength > 0)
            {
                fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "_" + Path.GetFileName(imageFile.FileName);
                var directoryPath = (userInfo == null) ? Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(user.InfoPath, user.Id.ToString()), Constants.PetProfilePicsFolderName) : Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(userInfo.InfoPath, userInfo.Id.ToString()), Constants.PetProfilePicsFolderName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var path = Path.Combine(directoryPath, fileName);
                imageFile.SaveAs(path);
            }
            #endregion

            #region Cover photo
            HttpPostedFileBase imgCoverFile = Request.Files["imageFileCover"];
            if (imgCoverFile != null && imgCoverFile.ContentLength > 0)
            {
                coverFileName = DateTime.Now.ToString("yyyymmddhhmmss") + "_" + Path.GetFileName(imgCoverFile.FileName);

                var directoryPath = (userInfo == null) ? Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(user.InfoPath, user.Id.ToString()), Constants.PetCoverPicsFolderName) : Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(userInfo.InfoPath, userInfo.Id.ToString()), Constants.PetCoverPicsFolderName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                try
                {
                    int y = Math.Abs(Convert.ToInt32(fc["hdnTop"].Replace("px", "")));
                    int x = Math.Abs(Convert.ToInt32(fc["hdnLeft"].Replace("px", "")));
                    if (x == 0 && y == 0) { throw new Exception(); }
                    else
                    {
                        int width = 1200;
                        int height = 200;

                        Bitmap original = Bitmap.FromStream(imgCoverFile.InputStream) as Bitmap;
                        var img = CreateImage(original, x, y, width, height);

                        img.Save(Path.Combine(directoryPath, coverFileName));
                    }
                }
                catch
                {
                    var pathCatch = Path.Combine(directoryPath, coverFileName);
                    imgCoverFile.SaveAs(pathCatch);
                }

                //  var fn = "_test" + coverFileName;
                //  var path = Path.Combine(directoryPath, fn);
                //  imgCoverFile.SaveAs(path);
            }
            #endregion


            UnitOfWork.PetRepository.Insert(model.Map(fileName, coverFileName, user));
            UnitOfWork.Save();

            Success(Resources.Wording.Pet_Add_SuccessMsg);
            Session[Constants.SessionCurrentUserPetCount] = UnitOfWork.PetRepository.GetPetCountByUser(userId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == id, p => p.Insurances, p => p.Farmer, p => p.PetContacts, p => p.Veterinarians, p => p.Users);
            
            if (pet == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var userId = pet.Users.FirstOrDefault().Id;
                ViewBag.UserId = userId;
                Session["PetBirthDate"] = pet.BirthDate;

                //if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                //{
                var petOwner = pet.Users.FirstOrDefault();
                if (petOwner != null)
                {
                    // User.UpdateOwnerAdminInfo(petOwner);
                    ViewBag.OwnerName = string.Format("{0} {1}", petOwner.FirstName.Value, petOwner.LastName.Value);
                    ViewBag.OwnerEmail = petOwner.Email.Value;
                    ViewBag.OwnerId = petOwner.Id;
                    ViewBag.OwnerFirstName = petOwner.FirstName.Value;
                    ViewBag.OwnerLastName = petOwner.LastName.Value;
                    ViewBag.UserInfoPath = petOwner.InfoPath;
                    ViewBag.smoId = ViewBag.SMORequestID;
                }
                // }
                if (Session["EditSMORequestID"] != null && !string.IsNullOrEmpty(Session["EditSMORequestID"].ToString()))
                {
                    var smoid = Convert.ToInt32(Session["EditSMORequestID"].ToString());
                    ViewBag.smoId = smoid.ToString();
                    var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.Pet.Id == id && p.ID == smoid);//&& p.SMORequestStatusId != SMORequestStatusEnum.Complete);
                    if (SMODetails != null)
                    {
                        if (SMODetails.SMORequestStatusId != SMORequestStatusEnum.Complete)
                        {
                            Session["IncompleteMedicalRecord"] = SMODetails.InCompleteMedicalRecord;
                        }
                        Session["EditSMORequestID"] = SMODetails.ID;
                    }
                    else
                    {
                        Session["IncompleteMedicalRecord"] = true;
                        Session["EditSMORequestID"] = "";
                    }

                }
                ViewBag.PetId = id;
                return View(new EditViewModel(pet));
            }
        }

        [HttpGet]
        public ActionResult CardEdit(int id)
        {
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == id, p => p.Farmer);

            ViewBag.States = UnitOfWork.StateRepository.GetAll(s => s.CountryId == pet.CountryOfBirthId);

            return PartialView("_PetEdit", new CardViewModel(pet));
        }

        [HttpGet]
        public ActionResult BreederEdit(int id)
        {
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == id, p => p.Farmer);

            ViewBag.States = UnitOfWork.StateRepository.GetAll(s => s.CountryId == pet.CountryOfBirthId);

            return PartialView("_BreederEdit", new BreederEditViewModel(pet));
        }

        [HttpGet]
        public ActionResult LoadReport(int id)
        {
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == id, p => p.Farmer, p => p.Insurances);

            ViewBag.States = UnitOfWork.StateRepository.GetAll(s => s.CountryId == pet.CountryOfBirthId);

            return PartialView("_LoadReport", new LoadReportModel(pet));
        }

        [HttpGet]
        public FileResult GetDownload(int petid)
        {
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == petid, p => p.Farmer, p => p.Insurances);

            LoadReportModel model = new LoadReportModel(pet);
            string userAbsoluteInfoPath = String.Empty;
            var fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "Pet" + model.Id + ".pdf";
            userAbsoluteInfoPath = getUserAbsolutePath();

            if (!string.IsNullOrEmpty(fileName))
            {
                var directoryPath = "";
                directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, model.Id.ToString(), Constants.DocumentFolderName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var path = Path.Combine(directoryPath, fileName);

                PetSummaryPdfHelper.GeneratePDF(path, model);

                var exist = System.IO.File.Exists(path);

                if (exist)
                {
                    return File(path, "application/pdf", fileName);

                }
            }
            return null;
        }

        [HttpGet]
        public FileResult GetDownloadMedicalRecord(int petid)
        {
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == petid, p => p.PetConditions, p => p.PetMedications, p => p.PetSurgeries, p => p.PetAllergies, p => p.PetVaccinations,
                                                          p => p.PetFoodPlans, p => p.PetHospitalizations, p => p.PetConsultations);

            LoadMedicalRecordModel model = new LoadMedicalRecordModel(pet);
            string userAbsoluteInfoPath = String.Empty;
            var fileName = DateTime.Now.ToString("yyyymmddhhmmss");
            if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                fileName += "Dossier_Médical_Animal" + model.Id + ".pdf";
            }
            else
            {
                fileName += "PetMedicalRecords" + model.Id + ".pdf";
            }
            userAbsoluteInfoPath = getUserAbsolutePath();

            if (!string.IsNullOrEmpty(fileName))
            {
                var directoryPath = "";
                directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, model.Id.ToString(), Constants.DocumentFolderName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var path = Path.Combine(directoryPath, fileName);

                PetMedicalSummaryPdfHelper.GeneratePDF(path, model);

                var exist = System.IO.File.Exists(path);

                if (exist)
                {
                    return File(path, "application/pdf", fileName);

                }
            }

            return null;
        }

        [HttpGet]
        public FileResult GetDownloadHealthMeasure(int petid)
        {
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == petid, p => p.PetHealthMeasures);

            HealthMeasureTrackerViewModel model = new HealthMeasureTrackerViewModel(pet, Common.Enums.MedicalRecordTypeEnum.HealthMeasureTracker, 1);
            string userAbsoluteInfoPath = String.Empty;


            var fileName = DateTime.Now.ToString("yyyymmddhhmmss");
            if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                fileName += "Paramètres_vitaux" + model.PetId + ".pdf";
            }
            else
            {
                fileName += "PetHealthMeasures" + model.PetId + ".pdf";
            }

            userAbsoluteInfoPath = getUserAbsolutePath();

            if (!string.IsNullOrEmpty(fileName))
            {
                var directoryPath = "";
                directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, model.PetId.ToString(), Constants.DocumentFolderName);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var path = Path.Combine(directoryPath, fileName);
                var petTypeVar = Resources.Enums.ResourceManager.GetString("PetTypeEnum_" + pet.PetTypeId);

                PetMedicalHealthMeasurePdf.GeneratePDF(path, model, pet.Name, petTypeVar.ToString());

                var exist = System.IO.File.Exists(path);

                if (exist)
                {
                    return File(path, "application/pdf", fileName);

                }
            }

            return null;
        }

        private string getUserAbsolutePath()
        {
            var uid = HttpContext.User.ToCustomPrincipal().CustomIdentity.UserId.ToString();
            var infoPath = HttpContext.User.ToCustomPrincipal().CustomIdentity.InfoPath;

            return Path.Combine(WebConfigHelper.UserFilesPath, infoPath, uid);
        }

        [HttpGet]
        public ActionResult ImageUpload(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                id = Convert.ToInt32(Request.QueryString["Card.Id"]);

                return RedirectToAction("Edit", new { id = id });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult ImageUpload(CardViewModel model, FormCollection fc)
        {
            var profFileName = string.Empty;
            var coverFileName = string.Empty;
            bool isProfPicDelete = Convert.ToBoolean(fc["hdnDeleteProfPic"]);
            bool isCoverPicDelete = Convert.ToBoolean(fc["hdnDeleteCoverPic"]);

            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == model.Id, navigationProperties: p => p.Users);
            var users = pet.Users.FirstOrDefault();
            if (pet != null)
            {
                if (isProfPicDelete)
                {
                    DeleteImage(pet.Image, Constants.PetProfilePicsFolderName, users); pet.Image = null;
                }
                if (isCoverPicDelete)
                {
                    DeleteImage(pet.CoverImage, Constants.PetCoverPicsFolderName, users); pet.CoverImage = null;
                }

                var directoryPath = "";
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    try
                    {
                        HttpPostedFileBase imgFile = Request.Files["imageFile"];
                        HttpPostedFileBase imgCoverFile = Request.Files["imageFileCover"];
                        #region Prof Pic
                        if (imgFile != null && imgFile.ContentLength > 0)
                        {
                            profFileName = DateTime.Now.ToString("yyyymmddhhmmss") + "_" + Path.GetFileName(imgFile.FileName);
                            pet.Image = (pet.Image == null) ? "" : pet.Image;

                            if (!pet.Image.Contains(imgFile.FileName))
                            {
                                if (!string.IsNullOrEmpty(pet.Image))
                                {
                                    DeleteImage(pet.Image, Constants.PetProfilePicsFolderName, users);
                                }
                            }
                            if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                            {
                                var user = pet.Users.FirstOrDefault();
                                directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(user.InfoPath, user.Id.ToString()), Constants.PetProfilePicsFolderName);
                            }
                            else
                            {
                                var user = pet.Users.FirstOrDefault();
                                directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(user.InfoPath, user.Id.ToString()), Constants.PetProfilePicsFolderName);
                            }
                            pet.Image = profFileName;
                            //}
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            var path = Path.Combine(directoryPath, profFileName);
                            imgFile.SaveAs(path);
                        }
                        #endregion

                        #region Cover Pic
                        if (imgCoverFile != null && imgCoverFile.ContentLength > 0)
                        {
                            directoryPath = "";
                            coverFileName = DateTime.Now.ToString("yyyymmddhhmmss") + "_" + Path.GetFileName(imgCoverFile.FileName);
                            pet.CoverImage = (pet.CoverImage == null) ? "" : pet.CoverImage;

                            if (!pet.CoverImage.Contains(imgCoverFile.FileName))
                            {
                                if (!string.IsNullOrEmpty(pet.CoverImage))
                                {
                                    DeleteImage(pet.CoverImage, Constants.PetCoverPicsFolderName, users);
                                }
                            }
                            //if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                            //{
                            //    var user = pet.Users.FirstOrDefault();
                            //    directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(user.InfoPath, user.Id.ToString()), Constants.PetCoverPicsFolderName);
                            //}
                            //else
                            //{
                            var user = pet.Users.FirstOrDefault();
                            directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(user.InfoPath, user.Id.ToString()), Constants.PetCoverPicsFolderName);
                            //  }
                            pet.CoverImage = coverFileName;

                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }
                            try
                            {
                                int y = Math.Abs(Convert.ToInt32(fc["hdnTop"].Replace("px", "")));
                                int x = Math.Abs(Convert.ToInt32(fc["hdnLeft"].Replace("px", "")));
                                if (x == 0 && y == 0) { throw new Exception(); }
                                else
                                {
                                    int width = 1200;
                                    int height = 200;

                                    Bitmap original = Bitmap.FromStream(imgCoverFile.InputStream) as Bitmap;
                                    var img = CreateImage(original, x, y, width, height);

                                    img.Save(Path.Combine(directoryPath, coverFileName));
                                }
                            }
                            catch
                            {
                                var pathCatch = Path.Combine(directoryPath, coverFileName);
                                imgCoverFile.SaveAs(pathCatch);
                            }

                            //var fn = "_test" + coverFileName;
                            //var path = Path.Combine(directoryPath, fn);
                            //imgCoverFile.SaveAs(path);
                        }
                        #endregion
                    }
                    catch { }
                }

                if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                {
                    var petOwner = pet.Users.FirstOrDefault();
                    ViewBag.OwnerName = string.Format("{0} {1}", petOwner.FirstName.Value, petOwner.LastName.Value);
                    ViewBag.OwnerEmail = petOwner.Email.Value;
                    ViewBag.OwnerId = petOwner.Id;
                    ViewBag.OwnerFirstName = petOwner.FirstName.Value;
                    ViewBag.OwnerLastName = petOwner.LastName.Value;
                    ViewBag.UserInfoPath = petOwner.InfoPath;
                }

                UnitOfWork.PetRepository.Update(pet);
                UnitOfWork.Save();
                Success(Resources.Wording.Pet_Card_SuccessMsg);
            }
            return RedirectToAction("PetImage", new { id = pet.Id });
            //}
            //catch { return RedirectToAction("PetImage", new { id = model.Id }); }
        }

        private Bitmap CreateImage(Bitmap original, int x, int y, int width, int height)
        {
            var img = new Bitmap(width, height);

            using (var g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(original, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            }

            return img;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CardEdit(CardViewModel model, FormCollection fc)
        {
            ///
            /// NOTE : IE Browser throws exception on these fields, 
            /// So removed the erros from the modelstate
            /// 
            if (!ModelState.IsValid)
            {
                try
                {
                    if (ModelState.Keys.Contains("IsSterile") && ModelState["IsSterile"].Errors.Count > 0)
                    {
                        ModelState.Remove("IsSterile");
                    }
                    if (ModelState.Keys.Contains("StateOfBirth") && ModelState["StateOfBirth"].Errors.Count > 0)
                    {
                        ModelState.Remove("StateOfBirth");
                    }
                    if (ModelState.Keys.Contains("BloodGroupType") && ModelState["BloodGroupType"].Errors.Count > 0)
                    {
                        ModelState.Remove("BloodGroupType");
                    }
                }
                catch { }
            }

            if (!ModelState.IsValid)
            {
                return PartialView("_PetEdit", model);
            }

            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == model.Id, p => p.Farmer);
            model.Map(pet);

            UnitOfWork.PetRepository.Update(pet);

            UnitOfWork.Save();


            Success(Resources.Wording.Pet_Card_SuccessMsg);
            return PartialView("_CardDetail", model);
            //RedirectToAction("Edit", new { id = model.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BreederEdit(BreederEditViewModel model, FormCollection fc)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_BreederEdit", model);
            }
            if (!string.IsNullOrEmpty(model.FarmerName))
            {
                var farmer = UnitOfWork.FarmerRepository.GetSingle(f => f.Id == model.FarmerId);
                if (farmer != null)
                {
                    UnitOfWork.FarmerRepository.Delete(farmer);
                    UnitOfWork.Save();
                }
            }
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == model.Id, p => p.Farmer);
            model.Map(pet);
            UnitOfWork.PetRepository.Update(pet);

            UnitOfWork.Save();
            Success(Resources.Wording.Pet_Card_SuccessMsg);
            return PartialView("_BreederDetail", model);
            // return RedirectToAction("Edit", new { id = model.Id });
        }

        [HttpGet]
        public ActionResult CardDetails(int id)
        {
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == id, p => p.Farmer);
            return PartialView("_CardDetail", new CardViewModel(pet));
        }

        [HttpGet]
        public ActionResult BreederDetail(int id)
        {
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == id, p => p.Farmer);
            return PartialView("_BreederDetail", new BreederEditViewModel(pet));
        }

        [HttpGet]
        public ActionResult PetImage(int id)
        {
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == id, p => p.Farmer, p => p.Users);
            var petOwner = (pet.Users != null) ? pet.Users.FirstOrDefault() : null;
            if (petOwner != null)
            {
                ViewBag.OwnerEmail = petOwner.Email.Value;
                ViewBag.OwnerId = petOwner.Id;
                ViewBag.OwnerFirstName = petOwner.FirstName.Value;
                ViewBag.OwnerLastName = petOwner.LastName.Value;
                ViewBag.UserInfoPath = petOwner.InfoPath;
            }
            return PartialView("_PetImage", new CardViewModel(pet));
        }

        [HttpGet]
        public ActionResult MedicalRecords(int id)
        {
            return PartialView("_MedicalRecord", id);
        }

        [HttpGet]
        public ActionResult DeletePet(int id)
        {
            var userId = User.GetUserId();
            //var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == id && p.Users.Any(u => u.Id == userId));
            var pet = (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
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
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetStates(string country)
        {
            var countryEnum = Enum.Parse(typeof(CountryEnum), country);
            var states = UnitOfWork.StateRepository.GetAll(s => s.CountryId == (CountryEnum)countryEnum).OrderBy(s => s.Name);
            var items = states.Select(i => new SelectListItem { Text = EnumHelper.GetResourceValueForEnumValue(i.Id), Value = i.Id.ToString() });

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetBloodGroupTypes(string petType)
        {
            var type = (PetTypeEnum)Enum.Parse(typeof(PetTypeEnum), petType);
            var bloodTypes = type == PetTypeEnum.Dog || type == PetTypeEnum.Cat
                ? UnitOfWork.BloodGroupTypeRepository.GetAll(b => b.PetTypeId == type)
                : UnitOfWork.BloodGroupTypeRepository.GetAll(b => b.PetTypeId == null);
            var items = bloodTypes.Select(i => new SelectListItem { Text = EnumHelper.GetResourceValueForEnumValue(i.Id), Value = i.Id.ToString() });

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult Image(string fileName, string imageType, string userInfoPath, string ownerId)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var directoryPath = "";
                if (string.IsNullOrEmpty(imageType))
                {
                    directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(userInfoPath, ownerId), Constants.PetProfilePicsFolderName);
                }
                else
                {
                    directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(userInfoPath, ownerId), Constants.PetCoverPicsFolderName);
                    //fileName="_test
                }
                var path = Path.Combine(directoryPath, fileName);

                var exist = System.IO.File.Exists(path);

                if (exist)
                {
                    var fs = new FileStream(path, FileMode.Open);
                    return new FileStreamResult(fs, "image/*");
                }
                else
                {
                    String RelativePath = Server.MapPath("~/Content/images/animal-dimg.jpg");// : Server.MapPath("~/Content/images/animal-dimg.jpg");

                    var fs = (string.IsNullOrEmpty(imageType)) ? new FileStream(RelativePath, FileMode.Open) : Stream.Null;
                    return new FileStreamResult(fs, "image/*");
                }
            }
            return null;
        }

        [HttpGet]
        public ActionResult DeleteImage(int petId, string imagetype)
        {
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == petId, navigationProperties: p => p.Users);
            string petPicsFolderName = string.Empty;
            if (string.IsNullOrEmpty(imagetype))
            {
                petPicsFolderName = Constants.PetProfilePicsFolderName;
            }
            else
            {
                petPicsFolderName = Constants.PetCoverPicsFolderName;
            }
            var user = pet.Users.FirstOrDefault();
            DeleteImage(pet.Image, petPicsFolderName, user);

            pet.Image = null;

            UnitOfWork.PetRepository.Update(pet);

            UnitOfWork.Save();

            //  return RedirectToAction("CardEdit", new { id = petId });
            return View("PetImage", new { id = petId });
        }

        private void DeleteImage(string fileName, string petPicsFolderName, User user = null)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var directoryPath = (user == null) ? Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(), petPicsFolderName) : Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(user.InfoPath, user.Id.ToString()), petPicsFolderName);
                var path = Path.Combine(directoryPath, fileName);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }

        [HttpPost]
        public void ClearTempData()
        {
            TempData.Remove("SuccessMessage");
        }

        public void checkRenual(int userId)
        {
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);

            if (user.UserSubscription.RenewalDate < DateTime.Today)
            {
                if (user.UserSubscription.TempUserSubscription != null)
                {
                    var tempdate = user.UserSubscription.TempUserSubscription;
                    var userSubscriptionNew = UnitOfWork.UserSubscriptionRepository.GetSingleTracking(us => us.Id == user.UserSubscriptionId);
                    userSubscriptionNew.StartDate = tempdate.StartDate;
                    userSubscriptionNew.RenewalDate = tempdate.RenewalDate;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();

                    userSubscriptionNew.Subscription = tempdate.Subscription;
                    userSubscriptionNew.SubscriptionId = tempdate.SubscriptionId;

                    if (userSubscriptionNew.SubscriptionService == null)
                    {
                        var serviceObj = new SubscriptionService
                         {
                             AditionalPetCount = tempdate.AditionalPetCount
                         };

                        userSubscriptionNew.SubscriptionService = serviceObj;
                    }
                    else
                    {
                        userSubscriptionNew.SubscriptionService.AditionalPetCount = tempdate.AditionalPetCount;
                    }
                    userSubscriptionNew.TempUserSubscriptionId = null;
                    userSubscriptionNew.TempUserSubscription = null;
                    UnitOfWork.UserSubscriptionRepository.Update(userSubscriptionNew);
                    UnitOfWork.Save();

                    var maxpet = (tempdate.Subscription != null) ? tempdate.Subscription.MaxPetCount : 0;
                    var maxPetCount = maxpet +
                             (tempdate.AditionalPetCount.HasValue
                                 ? tempdate.AditionalPetCount.Value
                                 : 0);
                    User.UpdateUserMaxPetCount(maxPetCount);
                    HttpContext.User.UpdateIsPlanExpired();

                }
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int ID)
        {
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == ID);
            return PartialView("_Confirmbx", new EditViewModel(pet));
        }

        [HttpGet]
        public ActionResult PetDetails(int id, ShareCategoryTypeEnum sharecategory)
        {
            var userId = User.GetUserId();
            var sharePetInformation = UnitOfWork.SharePetInformationRepository.GetSingle(c => c.PetId == id && c.ShareCategoryTypeId == sharecategory && c.ContactId == userId);
            if (sharePetInformation != null)
            {
                sharePetInformation.IsRead = true;
                UnitOfWork.SharePetInformationRepository.Update(sharePetInformation);
                UnitOfWork.Save();
            }
            else
            {
                if (sharecategory == ShareCategoryTypeEnum.ShareGallery)
                {
                    var sharePetInfoCommunity = UnitOfWork.SharePetInfoCommunityRepository.GetSingle(c => c.PetId == id && c.ShareCategoryTypeId == sharecategory && c.ContactId == userId);
                    if (sharePetInfoCommunity != null)
                    {
                        sharePetInfoCommunity.IsRead = true;
                        UnitOfWork.SharePetInfoCommunityRepository.Update(sharePetInfoCommunity);
                        UnitOfWork.Save();
                    }
                }
            }
            ViewBag.UserId = userId;
            ViewBag.PetId = id;
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == id, p => p.Insurances, p => p.Farmer, p => p.PetContacts, p => p.Veterinarians, p => p.Users, p => p.SharePetInformations, p => p.SharePetInfoCommunities);
            if (pet == null)
            {
                return RedirectToAction("Index", "Pet");
            }
            else
            {
                Session["PetBirthDate"] = pet.BirthDate;
                if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()))
                {
                    var petOwner = pet.Users.FirstOrDefault();
                    if (petOwner != null)
                    {
                        ViewBag.OwnerName = string.Format("{0} {1}", petOwner.FirstName.Value, petOwner.LastName.Value);
                        ViewBag.OwnerEmail = petOwner.Email.Value;
                        ViewBag.OwnerId = petOwner.Id;
                        ViewBag.OwnerFirstName = petOwner.FirstName.Value;
                        ViewBag.OwnerLastName = petOwner.LastName.Value;
                        ViewBag.UserInfoPath = petOwner.InfoPath;
                    }

                    ViewBag.ShareIDInformation = false; ViewBag.ShareMedicalRecords = false; ViewBag.ShareGallery = false; ViewBag.ShareVeterinarians = false; ViewBag.ShareContacts = false;
                    ViewBag.ShareIDInformationActive = false; ViewBag.ShareMedicalRecordsActive = false; ViewBag.ShareGalleryActive = false; ViewBag.ShareVeterinariansActive = false; ViewBag.ShareContactsActive = false;

                    if (sharecategory == ShareCategoryTypeEnum.ShareIDInformation)
                        ViewBag.ShareIDInformationActive = true;
                    if (sharecategory == ShareCategoryTypeEnum.ShareMedicalRecords)
                        ViewBag.ShareMedicalRecordsActive = true;
                    if (sharecategory == ShareCategoryTypeEnum.ShareContacts)
                        ViewBag.ShareContactsActive = true;
                    if (sharecategory == ShareCategoryTypeEnum.ShareVeterinarians)
                        ViewBag.ShareVeterinariansActive = true;
                    if (sharecategory == ShareCategoryTypeEnum.ShareGallery)
                        ViewBag.ShareGalleryActive = true;

                    var petshareInfo = pet.SharePetInformations.Where(x => x.PetId == id && x.ContactId == userId).Select(x => new { ShareCategoryTypeId = x.ShareCategoryTypeId }).Distinct().ToList();
                    foreach (var item in petshareInfo)
                    {
                        if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareIDInformation)
                            ViewBag.ShareIDInformation = true;
                        if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareMedicalRecords)
                            ViewBag.ShareMedicalRecords = true;
                        if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareContacts)
                            ViewBag.ShareContacts = true;
                        if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareVeterinarians)
                            ViewBag.ShareVeterinarians = true;
                        if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareGallery)
                            ViewBag.ShareGallery = true;
                    }

                    if (sharecategory == ShareCategoryTypeEnum.ShareGallery)
                    {
                        var petshareGalleryInfo = pet.SharePetInfoCommunities.Where(x => x.PetId == id && x.ContactId == userId).Select(x => new { ShareCategoryTypeId = x.ShareCategoryTypeId }).Distinct().ToList();
                        foreach (var item in petshareGalleryInfo)
                        {
                            if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareGallery)
                                ViewBag.ShareGallery = true;
                        }
                    }
                }
                return View(new EditViewModel(pet));
            }


            
        }
    }
}