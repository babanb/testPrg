﻿using ADOPets.Web.Common;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Common.Payment.Model;
using ADOPets.Web.Resources;
using ADOPets.Web.ViewModels.Account;
using ADOPets.Web.ViewModels.SMO;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ADOPets.Web.Controllers
{
    /// <summary>
    /// Controller created for SMO request process
    /// </summary>
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianLight, UserTypeEnum.Assistant, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianExpert)]
    public class SMOController : BaseController
    {
        #region Owner

        public int userId { get { return System.Web.HttpContext.Current.User.GetUserId(); } set { } }

        /// <summary>
        /// Summary page for SMO
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.CanAddSMOUserId = HttpContext.User.GetUserId();
            if (HttpContext.User.ToCustomPrincipal().CustomIdentity.UserRoleName == UserTypeEnum.VeterinarianAdo.ToString() || HttpContext.User.ToCustomPrincipal().CustomIdentity.UserRoleName == UserTypeEnum.VeterinarianLight.ToString())
            {
                var VD_SMO = UnitOfWork.SMORequestRepository.GetAll(null, s => s.OrderByDescending(a => a.RequestDate), s => s.Pet, s => s.User).OrderByDescending(a => a.RequestDate).Select(p => new IndexViewModel(p));
                var smos = VD_SMO.ToList();

                var userId1 = HttpContext.User.GetUserId();
                int? centerId = HttpContext.User.GetCenterId();
                if (!string.IsNullOrEmpty(centerId.ToString()))
                {
                    VD_SMO = UnitOfWork.SMORequestRepository.GetAll(null, s => s.OrderByDescending(a => a.RequestDate), s => s.Pet, s => s.User).Where(s => s.User.CenterID == centerId).OrderByDescending(a => a.RequestDate).Select(p => new IndexViewModel(p));
                    smos = VD_SMO.ToList();
                }
                Session[Constants.SessionCurrentUserPetCount] = smos.Count;
                return View("Index_VD", VD_SMO);
            }
            else if (HttpContext.User.ToCustomPrincipal().CustomIdentity.UserRoleName == UserTypeEnum.Admin.ToString())
            {
                var VD_SMO = UnitOfWork.SMORequestRepository.GetAll(null, s => s.OrderByDescending(a => a.RequestDate), s => s.Pet, s => s.User).OrderByDescending(a => a.RequestDate).Select(p => new IndexViewModel(p));
                var smos = VD_SMO.ToList();
                Session[Constants.SessionCurrentUserPetCount] = smos.Count;
                return View("Index_VD", VD_SMO);
            }
            else if (HttpContext.User.ToCustomPrincipal().CustomIdentity.UserRoleName == UserTypeEnum.VeterinarianExpert.ToString())
            {
                var VD_SMO = UnitOfWork.SMOExpertRepository.GetAll(s => s.VetExpertID == userId, null, s => s.SMORequest, s => s.User, s => s.SMORequest.Pet, s => s.SMORequest.User, s => s.SMORequest.Pet.Users).OrderByDescending(s => s.SMORequest.RequestDate).Select(p => new ExpertRelationViewModel(p, p.SMORequest.Pet.Users.FirstOrDefault()));
                var smos = VD_SMO.ToList();
                Session[Constants.SessionCurrentUserPetCount] = smos.Count;
                return View("Index_VE", VD_SMO);
            }
            else
            {
                var dbSMO = UnitOfWork.SMORequestRepository.GetAll(s => s.UserId == userId, null, s => s.Pet, s => s.User).OrderByDescending(s => s.RequestDate).Select(p => new IndexViewModel(p));
                var smos = dbSMO.ToList();
                Session[Constants.SessionCurrentUserPetCount] = smos.Count;
                Session["CanAddSMOUserId"] = userId;
                return View(dbSMO);
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            var userSubscriptionId = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId).UserSubscriptionId;
            var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingle(s => s.Id == userSubscriptionId, p => p.Subscription);

            TempData.Clear();
            ViewBag.UserId = userId;
            TempData["HasSMO_" + userId] = userSubscription.Subscription.HasSMO;
            KeepTempData();
            AddViewModel model = new AddViewModel();

            model.objSetup = new AddSetupViewModel();
            model.objSetup.Pets = new List<ViewModels.Pet.IndexViewModel>();
            var dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == userId), navigationProperties: p => p.Users);
            var userData = dbPets.FirstOrDefault().Users.FirstOrDefault();
            var pets = dbPets.Select(p => new ADOPets.Web.ViewModels.Pet.IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, p.GenderId, p.Image, userData)).ToList();
            model.objSetup.Pets = pets;
            model.objBilling = new AddBillingViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UnitOfWork.UserRepository.GetSingleTracking(u => u.Id == userId);

            UnitOfWork.SMORequestRepository.Insert(model.Map());
            UnitOfWork.Save();

            Success(Wording.Smo_Add_AddSuccessfullMessage);

            Session[Constants.SessionCurrentUserPetCount] = UnitOfWork.PetRepository.GetPetCountByUser(userId);
            KeepTempData();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Show SMO details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SMODetails(int id)
        {
            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, p => p.User);

            if (SMODetails == null)
            {
                return RedirectToAction("Index");
            }

            SMODetails.IsOwnerRead = true;
            UnitOfWork.SMORequestRepository.Update(SMODetails);
            UnitOfWork.Save();

            KeepTempData();
            ViewBag.SMORequestID = id;
            return View(new DetailsViewModel(SMODetails));
        }

        /// <summary>
        /// Call Setup partial view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Setup()
        {
            AddSetupViewModel model = new AddSetupViewModel();
            if (TempData["Setup_" + userId] != null)
                model = (AddSetupViewModel)TempData["Setup_" + userId];

            KeepTempData();
            ViewBag.UserId = userId;
            return PartialView("_Setup", model);
        }

        [HttpPost]
        public ActionResult Setup(AddSetupViewModel setupmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(setupmodel);
            }
            TempData["Setup_" + userId] = setupmodel;
            KeepTempData();
            if (HttpContext.User.ToCustomPrincipal().CustomIdentity.UserRoleName == UserTypeEnum.OwnerAdmin.ToString())
            {
                if (TempData["HasSMO_" + userId].ToString().ToLower() == "true")
                {
                    AddViewModel model = new AddViewModel();
                    if (TempData["Setup_" + userId] != null)
                        model.objSetup = (AddSetupViewModel)TempData.Peek("Setup_" + userId);

                    var user = UnitOfWork.UserRepository.GetSingleTracking(u => u.Id == userId);
                    model.IsSMOPaymentDone = false;
                    model.InCompleteMedicalRecord = false;
                    model.RequestStatus = SMORequestStatusEnum.Open.ToString();
                    model.smoRequestStatus = SMORequestStatusEnum.Open;
                    model.UserId = Convert.ToInt32(userId);
                    if (TempData["PetId_" + userId] != null)
                        model.objSetup.PetId = (int)TempData["PetId_" + userId];

                    UnitOfWork.SMORequestRepository.Insert(model.Map());
                    UnitOfWork.Save();

                    #region AddInvestigation
                    long smoreqid = UnitOfWork.SMORequestRepository.GetAll().OrderByDescending(u => u.ID).FirstOrDefault().ID;

                    if (TempData["Investigation_" + userId] != null)
                        model.objSetup.Investigations = (List<InvestigationViewModel>)TempData["Investigation_" + userId];
                    if (model.objSetup.Investigations != null)
                    {
                        foreach (InvestigationViewModel mdl in model.objSetup.Investigations)
                        {
                            mdl.SMORequestID = smoreqid;
                            UnitOfWork.SMOInvestigationRepository.Insert(mdl.Map());
                            UnitOfWork.Save();
                        }
                    }
                    #endregion
                    KeepTempData();

                    #region find VD
                    var userVD = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId && s.VetDirectorID != null);
                    var userID = Roles.IsUserInRole(UserTypeEnum.Admin.ToString())
                        ? HttpContext.User.GetOwnerId()
                        : HttpContext.User.GetUserId();
                    var usertomail = UnitOfWork.UserRepository.GetSingle(a => a.Id == userID);
                    if (userVD != null)
                    {
                        //  int VDCId = Convert.ToInt32(userVD.VetDirectorID);
                        var list = UnitOfWork.UserRepository.GetAll(s => s.UserTypeId == UserTypeEnum.VeterinarianLight);
                        foreach (User u in list)
                        {
                            EmailSender.SendSMONotificationToVetDirectorMail(u.Email, u.FirstName, u.LastName, model.objSetup.Title);
                            EmailSender.SendSMORequestToSupport(u.FirstName, u.LastName, u.Email, SMOHelper.GetDefaultPlanAmount().ToString(), null);
                        }
                    }
                    else
                    {
                        var list = UnitOfWork.UserRepository.GetAll(s => s.UserTypeId == UserTypeEnum.VeterinarianAdo && s.UserTypeId == UserTypeEnum.VeterinarianLight);
                        foreach (User u in list)
                        {
                            EmailSender.SendSMONotificationToVetDirectorMail(u.Email, u.FirstName, u.LastName, model.objSetup.Title);
                            EmailSender.SendSMORequestToSupport(u.FirstName, u.LastName, u.Email, SMOHelper.GetDefaultPlanAmount().ToString(), null);
                        }
                    }
                    EmailSender.SendSMODetailsToOwnerMail(usertomail.Email, usertomail.FirstName, usertomail.LastName, model.objSetup.Title, model.objSetup.PetName);


                    #endregion
                }
            }
            else
            {
                AddViewModel model = new AddViewModel();
                if (TempData["Setup_" + userId] != null)
                    model.objSetup = (AddSetupViewModel)TempData.Peek("Setup_" + userId);

                var user = UnitOfWork.UserRepository.GetSingleTracking(u => u.Id == userId);
                model.IsSMOPaymentDone = false;
                model.InCompleteMedicalRecord = false;
                model.RequestStatus = SMORequestStatusEnum.PaymentPending.ToString();
                if (TempData["OwnerId_" + userId] != null)
                    model.UserId = (int)TempData["OwnerId_" + userId];
                else
                    model.UserId = Convert.ToInt32(userId);
                if (TempData["PetId_" + userId] != null)
                    model.objSetup.PetId = (int)TempData["PetId_" + userId];
                model.smoRequestStatus = SMORequestStatusEnum.PaymentPending;
                model.SMOSubmittedBy = userId;
                model.objSetup.SMOSubmittedBy = userId;
                UnitOfWork.SMORequestRepository.Insert(model.Map());
                UnitOfWork.Save();
                var owneruser = UnitOfWork.UserRepository.GetSingle(a => a.Id == model.UserId);

                #region AddInvestigation
                long smoreqid = UnitOfWork.SMORequestRepository.GetAll().OrderByDescending(u => u.ID).FirstOrDefault().ID;

                if (TempData["Investigation_" + userId] != null)
                    model.objSetup.Investigations = (List<InvestigationViewModel>)TempData["Investigation_" + userId];
                if (model.objSetup.Investigations != null)
                {
                    foreach (InvestigationViewModel mdl in model.objSetup.Investigations)
                    {
                        mdl.SMORequestID = smoreqid;
                        UnitOfWork.SMOInvestigationRepository.Insert(mdl.Map());
                        UnitOfWork.Save();
                    }
                }
                #endregion
                KeepTempData();
                string userRole = HttpContext.User.GetUserRole();
                EmailSender.SendSMORequestPaymentPendingMail(userRole, owneruser.Email.Value, owneruser.FirstName.Value, owneruser.LastName.Value, SMOHelper.GetFormatedSMOID(smoreqid.ToString()));
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Call Billing partial view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Billing()
        {
            AddBillingViewModel model = new AddBillingViewModel();
            if (TempData["Billing_" + userId] != null)
                model = (AddBillingViewModel)TempData.Peek("Billing_" + userId);

            if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                model.Country = CountryEnum.UnitedStates;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
            {
                model.Country = CountryEnum.India;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                model.Country = CountryEnum.France;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.Portuguese)
            {
                model.Country = CountryEnum.BRAZIL;
            }
            KeepTempData();
            return PartialView("_Billing", model);
        }

        [HttpPost]
        public ActionResult Billing(AddBillingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            TempData["Billing_" + userId] = model;

            KeepTempData();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Call Confirmation partial view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Confirmation()
        {
            AddViewModel model = new AddViewModel();
            model.objSetup = new AddSetupViewModel();
            model.objBilling = new AddBillingViewModel();
            if (TempData["Setup_" + userId] != null)
                model.objSetup = (AddSetupViewModel)TempData.Peek("Setup_" + userId);
            if (model.objSetup.PetId == 0)
                model.objSetup.PetId = Convert.ToInt32(TempData["PetId_" + userId]);
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == model.objSetup.PetId);

            model.objSetup.PetName = (pet == null) ? String.Empty : pet.Name;
            if (TempData["Investigation_" + userId] != null)
                model.objSetup.Investigations = (List<InvestigationViewModel>)TempData["Investigation_" + userId];
            if (TempData["Billing_" + userId] != null)
                model.objBilling = (AddBillingViewModel)TempData.Peek("Billing_" + userId);
            KeepTempData();
            return PartialView("_Confirmation", model);
        }

        [HttpPost]
        public ActionResult Confirmation(AddViewModel model)
        {
            PaymentResult paymentResult;
            model.objBilling = new AddBillingViewModel();
            if (TempData["Billing_" + userId] != null)
                model.objBilling = (AddBillingViewModel)TempData.Peek("Billing_" + userId);
            model.objBilling.Price = SMOHelper.GetDefaultPlanAmount();
            model.objBilling.Plan = "";
            if (WebConfigHelper.DoFakePayment)
            {
                paymentResult = PaymentHelper.FakePayment(new ConfirmationViewModel());
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)  //Fake Payment for India Domain
            {
                paymentResult = PaymentHelper.FakePayment(new ConfirmationViewModel());
            }
            else if (CultureHelper.GetCurrentCulture() == "pt-BR")
            {
                paymentResult = PaymentHelper.PTSMOPayment(model.objBilling);
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                paymentResult = PaymentHelper.SMOPayment(model.objBilling);
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                paymentResult = PaymentHelper.FakePayment(new ConfirmationViewModel());
            }
            else
            {
                paymentResult = PaymentHelper.SMOPayment(model.objBilling);
            }
            if (paymentResult.Success)
            {
                model.objSetup = new AddSetupViewModel();
                model.objBilling = new AddBillingViewModel();
                if (TempData["Setup_" + userId] != null)
                    model.objSetup = (AddSetupViewModel)TempData.Peek("Setup_" + userId);
                if (TempData["Billing_" + userId] != null)
                    model.objBilling = (AddBillingViewModel)TempData.Peek("Billing_" + userId);

                var user = UnitOfWork.UserRepository.GetSingleTracking(u => u.Id == userId);
                model.InCompleteMedicalRecord = false;
                model.RequestStatus = SMORequestStatusEnum.Open.ToString();
                model.smoRequestStatus = SMORequestStatusEnum.Open;
                var userID = Roles.IsUserInRole(UserTypeEnum.Admin.ToString())
                    ? HttpContext.User.GetOwnerId()
                    : HttpContext.User.GetUserId();
                int ownerId = 0;

                var dbPets = UnitOfWork.PetRepository.GetSingle(p => p.Id == model.objSetup.PetId, p => p.Users);

                var petOwner = dbPets.Users.FirstOrDefault();
                if (petOwner != null)
                {
                    if (petOwner.Id != userId)
                        ownerId = petOwner.Id;
                    else
                        ownerId = userId;
                }

                model.UserId = ownerId;

                int? smoIdEdit = null;
                try
                {
                    if (TempData["SMOID_" + userId] != null)
                    {
                        smoIdEdit = Convert.ToInt32(TempData["SMOID_" + userId]);
                    }
                }
                catch { }
                model.IsSMOPaymentDone = true;
                if (smoIdEdit != null)
                {
                    model.Id = Convert.ToInt32(smoIdEdit);
                    UnitOfWork.SMORequestRepository.Update(model.Map());
                }
                else
                {
                    UnitOfWork.SMORequestRepository.Insert(model.Map());
                }
                UnitOfWork.Save();
                long smoreqid = UnitOfWork.SMORequestRepository.GetAll().OrderByDescending(u => u.ID).FirstOrDefault().ID;
                TempData["SMOID_" + userId] = smoreqid;
                if (TempData["Investigation_" + userId] != null)
                    model.objSetup.Investigations = (List<InvestigationViewModel>)TempData["Investigation_" + userId];
                if (model.objSetup.Investigations != null)
                {
                    foreach (InvestigationViewModel mdl in model.objSetup.Investigations)
                    {
                        mdl.SMORequestID = smoreqid;
                        UnitOfWork.SMOInvestigationRepository.Insert(mdl.Map());
                        UnitOfWork.Save();
                    }
                }
                KeepTempData();
                if (TempData["HasSMO_" + userId].ToString().ToLower() == "false")
                {
                    model.objBilling.PaymentTypeId = PaymentTypeEnum.SaleTransaction;
                    model.objBilling.UserId = userId;
                    var billingObj = model.objBilling.Map();
                    UnitOfWork.BillingInformationRepository.Insert(billingObj);
                    UnitOfWork.Save();
                }

                #region find VD
                bool flag = false;
                var owneruser = UnitOfWork.UserRepository.GetSingle(s => s.Id == model.UserId);

                try
                {
                    var userVD = UnitOfWork.UserRepository.GetSingle(u => u.Id == model.objSetup.SMOSubmittedBy);
                    if (userVD != null)
                    {
                        flag = true;
                        EmailSender.SendToVetDirectorNewSMORequestMail(userVD.Email.Value, owneruser.FirstName.Value, owneruser.LastName.Value, dbPets.Name, dbPets.PetTypeId.ToString(), SMOHelper.GetFormatedSMOID(smoreqid.ToString()), model.objSetup.Title, owneruser.Id.ToString(), owneruser.Email.Value);
                        EmailSender.SendSupportNewSMORequest(owneruser.FirstName.Value, owneruser.LastName.Value, paymentResult.OrderId, model.UserId.ToString(), null, SMOHelper.GetDefaultPlanAmount().ToString(), SMOHelper.GetFormatedSMOID(smoreqid.ToString()), paymentResult.TransactionResult);
                    }
                }
                catch { }
                var usertomail = UnitOfWork.UserRepository.GetSingle(a => a.Id == userID);

                if (!flag)
                {
                    var userVD = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId && s.VetDirectorID != null);

                    if (userVD != null)
                    {
                        var list = UnitOfWork.UserRepository.GetAll(s => s.UserTypeId == UserTypeEnum.VeterinarianLight);
                        foreach (User u in list)
                        {
                            EmailSender.SendToVetDirectorNewSMORequestMail(u.Email.Value, owneruser.FirstName.Value, owneruser.LastName.Value, dbPets.Name, dbPets.PetTypeId.ToString(), SMOHelper.GetFormatedSMOID(smoreqid.ToString()), model.objSetup.Title, owneruser.Id.ToString(), owneruser.Email.Value);
                            EmailSender.SendSupportNewSMORequest(owneruser.FirstName.Value, owneruser.LastName.Value, paymentResult.OrderId, model.UserId.ToString(), null, SMOHelper.GetDefaultPlanAmount().ToString(), SMOHelper.GetFormatedSMOID(smoreqid.ToString()), paymentResult.TransactionResult);
                        }
                    }
                    else
                    {
                        var list = UnitOfWork.UserRepository.GetAll(s => s.UserTypeId == UserTypeEnum.VeterinarianAdo);
                        foreach (User u in list)
                        {
                            EmailSender.SendToVetDirectorNewSMORequestMail(u.Email.Value, owneruser.FirstName.Value, owneruser.LastName.Value, dbPets.Name, dbPets.PetTypeId.ToString(), SMOHelper.GetFormatedSMOID(smoreqid.ToString()), model.objSetup.Title, owneruser.Id.ToString(), owneruser.Email.Value);
                            EmailSender.SendSupportNewSMORequest(owneruser.FirstName.Value, owneruser.LastName.Value, paymentResult.OrderId, model.UserId.ToString(), null, SMOHelper.GetDefaultPlanAmount().ToString(), SMOHelper.GetFormatedSMOID(smoreqid.ToString()), paymentResult.TransactionResult);
                        }
                    }
                }
                EmailSender.SendNewSMORequestMail(usertomail.Email.Value, usertomail.FirstName.Value, usertomail.LastName.Value, paymentResult.OrderId, model.objBilling.Price.ToString(), SMOHelper.GetFormatedSMOID(smoreqid.ToString()), model.objBilling.Address1, model.objBilling.Address2, model.objBilling.City, model.objBilling.State.ToString(), model.objBilling.Zip, model.objBilling.Country.ToString(), owneruser.Email.Value);

                #endregion

                paymentResult.TransactionDate = DateTime.Now.ToShortDateString();

                TempData["Payment_" + userId] = paymentResult;
                KeepTempData();
                Success(Wording.Smo_Add_AddSuccessfullMessage);

                Session[Constants.SessionCurrentUserPetCount] = UnitOfWork.PetRepository.GetPetCountByUser(userId);
            }
            TempData["Payment_" + userId] = paymentResult;
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Call Payment partial view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Payment()
        {
            KeepTempData();
            PaymentResultViewModel model = new PaymentResultViewModel();
            if (TempData["Payment_" + userId] != null)
            {
                PaymentResult result = (PaymentResult)TempData["Payment_" + userId];

                if (result.Success)
                {
                    var UserRepository = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId).UserSubscriptionId;
                    var subscription = UnitOfWork.UserSubscriptionRepository.GetSingle(s => s.Id == UserRepository, p => p.Subscription);
                    var amount = subscription.Subscription.HasSMO ? subscription.Subscription.Amount.Value.ToString(CultureInfo.InvariantCulture) : SMOHelper.GetDefaultPlanAmount().ToString(CultureInfo.InvariantCulture);

                    model.Success = result.Success;
                    model.SMOID = SMOHelper.GetFormatedSMOID(TempData["SMOID_" + userId].ToString());
                    model.Price = amount;
                    model.ErrorMsg = result.ErrorMessage;
                    model.OrderNumber = result.OrderId;
                    model.TransactionDate = result.TransactionDate;
                    model.TransactionResult = result.TransactionResult;
                    model.TransactionTime = result.TransactionTime;
                }
                else
                {
                    model.OrderNumber = "";
                    model.Success = result.Success;
                    model.ErrorMsg = result.ErrorMessage;
                    model.OrderNumber = result.OrderId;
                    model.TransactionDate = result.TransactionDate;
                    model.TransactionResult = result.TransactionResult;
                    model.TransactionTime = result.TransactionTime;
                }
                if (TempData["HasSMO_" + userId].ToString().ToLower() == "false" && model.Success == false)
                {
                    model.OrderNumber = "";
                    model.ErrorMsg = result.ErrorMessage;
                    model.OrderNumber = result.OrderId;
                    model.TransactionDate = result.TransactionDate;
                    model.TransactionResult = result.TransactionResult;
                    model.TransactionTime = result.TransactionTime;
                }
            }
            return PartialView("_Payment", model);
        }

        [HttpGet]
        public ActionResult SelectPet()
        {
            int ownerId = 0;
            if (TempData["OwnerId_" + userId] != null)
            {
                ownerId = Convert.ToInt32(TempData["OwnerId_" + userId].ToString());
            }
            else
            {
                ownerId = userId;
            }
            var dbPets = Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString())
                ? UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == userId), navigationProperties: p => p.Users) : UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == ownerId), navigationProperties: p => p.Users);

            List<ADOPets.Web.ViewModels.Pet.IndexViewModel> pets = new List<ViewModels.Pet.IndexViewModel>();
            if (dbPets != null)
                pets = dbPets.Select(p => new ADOPets.Web.ViewModels.Pet.IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, p.GenderId, p.Image, p.Users.FirstOrDefault())).ToList();

            if (pets != null)
                Session[Constants.SessionCurrentUserPetCount] = pets.Count;
            KeepTempData();
            if (pets.Count > 0)
                return PartialView("_SelectPet", pets);

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SelectPet(int Id)
        {
            AddSetupViewModel model = new AddSetupViewModel();
            if (TempData["Setup_" + userId] != null)
                model = (AddSetupViewModel)TempData["Setup_" + userId];
            else
                model = new AddSetupViewModel();
            var smorequest = UnitOfWork.SMORequestRepository.GetSingleTracking(a => a.PetId == Id && a.SMORequestStatusId != SMORequestStatusEnum.Complete && a.IsSMOPaymentDone);
            if (smorequest != null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            model.PetId = Id;

            TempData["Setup_" + userId] = model;
            TempData["PetId_" + userId] = Id;
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == Id);
            TempData["PetName_" + userId] = pet.Name.Value;

            KeepTempData();

            return Json(new { success = true, data = pet.Name }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SelectOwner()
        {
            IEnumerable<ADOPets.Web.ViewModels.Profile.OwnerDetailViewModel> owners = null;
            var dbOwners = UnitOfWork.UserRepository.GetAll(p => p.UserTypeId == UserTypeEnum.OwnerAdmin && p.UserStatusId == UserStatusEnum.Active && p.UserSubscription.RenewalDate > DateTime.Today, null, p => p.Pets, u => u.UserSubscription);
            //Owners who have pets
            if (dbOwners != null && dbOwners.Count() > 0)
            {
                var dbOwner = dbOwners.Where(a => a.Pets.Count() != 0).Select(a => a);
                owners = dbOwner.Select(p => new ADOPets.Web.ViewModels.Profile.OwnerDetailViewModel(p.FirstName.Value, p.LastName.Value, p.Email.Value, p.Id));
                KeepTempData();
            }
            return PartialView("_SelectOwner", owners);
        }

        [HttpPost]
        public ActionResult SelectOwner(int Id)
        {
            AddSetupViewModel model = new AddSetupViewModel();
            if (TempData["Setup_" + userId] != null)
                model = (AddSetupViewModel)TempData["Setup_" + userId];
            else
                model = new AddSetupViewModel();
            var smorequest = UnitOfWork.UserRepository.GetSingle(p => p.Id == Id);

            model.OwnerId = Id;

            TempData["Setup_" + userId] = model;
            TempData["OwnerId_" + userId] = Id;
            TempData["OwnerName_" + userId] = smorequest.FirstName.Value;
            KeepTempData();

            return Json(new { success = true, data = smorequest.FirstName }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Call Investigation view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddInvestigation()
        {
            KeepTempData();
            InvestigationViewModel model = new InvestigationViewModel();
            return PartialView("_PetInvestigation", model);
        }

        [HttpPost]
        public ActionResult AddInvestigation(string TestName, DateTime TestDate)
        {
            InvestigationViewModel model = new InvestigationViewModel();
            model.TestDescription = TestName;
            model.TestDate = TestDate;
            model.IsDeleted = false;
            int editId = 0;
            List<InvestigationViewModel> listInvestigation = null;
            if (TempData["Investigation_" + userId] == null)
                listInvestigation = new List<InvestigationViewModel>();
            else
                listInvestigation = (List<InvestigationViewModel>)TempData["Investigation_" + userId];
            model.ID = listInvestigation.Count() + 1;
            if (TempData["InvestigationID_" + userId] != null)
            {
                editId = (int)TempData["InvestigationID_" + userId];
            }
            InvestigationViewModel existingModel = listInvestigation.Find(a => a.TestDescription == TestName);
            if (existingModel == null)
            {
                listInvestigation.Add(model);
                var list = listInvestigation.OrderByDescending(a => a.TestDate).ToList();
                TempData["Investigation_" + userId] = list;
                KeepTempData();
            }
            else
            {
                var list = listInvestigation.OrderByDescending(a => a.TestDate).ToList();
                TempData["Investigation_" + userId] = list;
                KeepTempData();
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteConfirm(string ID)
        {
            int Id = Convert.ToInt32(ID);
            InvestigationViewModel model = new InvestigationViewModel();
            List<InvestigationViewModel> listInvestigation = null;
            if (TempData["Investigation_" + userId] == null)
                listInvestigation = new List<InvestigationViewModel>();
            else
                listInvestigation = (List<InvestigationViewModel>)TempData["Investigation_" + userId];
            model = listInvestigation.Find(a => a.ID == Id);
            var list = listInvestigation.OrderByDescending(a => a.TestDate).ToList();
            TempData["Investigation_" + userId] = list;
            KeepTempData();
            return PartialView("_PetDeleteInvestigation", model);
        }

        [HttpPost]
        public ActionResult DeleteInvestigation(string ID)
        {
            int Id = Convert.ToInt32(ID);
            if (ID != "")
            {
                List<InvestigationViewModel> listInvestigation = null;
                if (TempData["Investigation_" + userId] == null)
                    listInvestigation = new List<InvestigationViewModel>();
                else
                    listInvestigation = (List<InvestigationViewModel>)TempData["Investigation_" + userId];

                InvestigationViewModel m = listInvestigation.Find(a => a.ID == Id);
                listInvestigation.Remove(m);
                var list = listInvestigation.OrderByDescending(a => a.TestDate).ToList();
                TempData["Investigation_" + userId] = list;
                KeepTempData();
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditInvestigation(int ID)
        {
            InvestigationViewModel model = new InvestigationViewModel();
            List<InvestigationViewModel> listInvestigation = null;
            if (TempData["Investigation_" + userId] == null)
                listInvestigation = new List<InvestigationViewModel>();
            else
                listInvestigation = (List<InvestigationViewModel>)TempData["Investigation_" + userId];
            model = listInvestigation.Find(a => a.ID == ID);
            TempData["InvestigationID_" + userId] = ID;
            var list = listInvestigation.OrderByDescending(a => a.TestDate).ToList();
            TempData["Investigation_" + userId] = list;
            KeepTempData();
            return PartialView("_PetEditInvestigation", model);
        }

        [HttpPost]
        public ActionResult EditInvestigation(string TestName, DateTime TestDate)
        {
            InvestigationViewModel model = new InvestigationViewModel();
            model.TestDescription = TestName;
            model.TestDate = TestDate;
            model.IsDeleted = false;
            if (TempData["InvestigationID_" + userId] != null)
            {
                model.ID = (int)TempData["InvestigationID_" + userId];
            }
            List<InvestigationViewModel> listInvestigation = null;
            if (TempData["Investigation_" + userId] == null)
                listInvestigation = new List<InvestigationViewModel>();
            else
                listInvestigation = (List<InvestigationViewModel>)TempData["Investigation_" + userId];
            InvestigationViewModel existingModel = listInvestigation.Find(a => a.TestDescription == TestName);
            string testdesc = listInvestigation.Find(a => a.ID == model.ID).TestDescription;
            if (testdesc != TestName)
            {
                if (existingModel != null)
                {
                    var list = listInvestigation.OrderByDescending(a => a.TestDate).ToList();
                    TempData["Investigation_" + userId] = list;
                    KeepTempData();
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            if (model.ID > 0)
            {
                InvestigationViewModel editmodel = listInvestigation.Find(a => a.ID == model.ID);
                listInvestigation.Remove(editmodel);
                listInvestigation.Add(model);
                var list = listInvestigation.OrderByDescending(a => a.TestDate).ToList();
                TempData["Investigation_" + userId] = list;
                TempData["InvestigationID_" + userId] = null;
            }

            KeepTempData();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult InvestigationList()
        {
            AddSetupViewModel model = new AddSetupViewModel();
            model.Investigations = new List<InvestigationViewModel>();
            List<InvestigationViewModel> listInvestigation = new List<InvestigationViewModel>();
            if (TempData["Investigation_" + userId] != null)
                model.Investigations = (List<InvestigationViewModel>)TempData["Investigation_" + userId];
            //model.Investigations= model.Investigations.AsQueryable().OrderByDescending(a=>a.TestDate);
            KeepTempData();
            return PartialView("_InvestigationList", model);
        }

        /// <summary>
        /// index for vd login
        /// </summary>
        /// <returns></returns>
        /// 
        // [ActionName("Index")]
        public ActionResult Index_VD()
        {
            var dbSMO = UnitOfWork.SMORequestRepository.GetAll(s => s.UserId == userId, null, s => s.Pet).OrderByDescending(c => c.RequestDate).Select(p => new IndexViewModel(p));
            var smos = dbSMO.ToList();

            Session[Constants.SessionCurrentUserPetCount] = smos.Count;

            return View(dbSMO);
        }

        /// <summary>
        /// Edit for vd login
        /// </summary>
        /// <returns></returns>
        [ActionName("Edit")]
        [HttpGet]
        public ActionResult Edit_VD(int id)
        {
            ViewBag.UserId = userId;

            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            try
            {
                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()))
                {
                    SMODetails.IsRead = true;
                    UnitOfWork.SMORequestRepository.Update(SMODetails);
                    UnitOfWork.Save();
                }

                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                {
                    var smoexpert = UnitOfWork.SMOExpertRepository.GetSingle(s => s.SMORequestID == id && s.VetExpertID == userId);
                    if (smoexpert != null)
                    {
                        smoexpert.IsRead = true;
                        UnitOfWork.SMOExpertRepository.Update(smoexpert);
                        UnitOfWork.Save();
                    }
                }
            }
            catch { }

            var results = from result in SMODetails.SMOExpertRelations
                          where result.IsFinal == true
                          select result;

            TempData["ExpertResponseCount_" + userId] = results.Count();
            ViewBag.SMORequestID = id;
            TempData["EditSMORequestID_" + userId] = id;
            if (SMODetails.SMORequestStatusId != SMORequestStatusEnum.Complete)
                TempData["IncompleteMedicalRecord_" + userId] = SMODetails.InCompleteMedicalRecord;
            TempData["SMODetailsViewModel"] = new DetailsViewModel(SMODetails);
            TempData["PetId_" + userId] = SMODetails.Pet.Id;
            Session[Constants.SessionCurrentUserRequestFrom] = "smo";
            KeepTempData();
            Session["EditSMORequestID"] = id;
            TempData["AssigendExperts" + id] = null;
            if (SMODetails == null)
            {
                return RedirectToAction("Index");
            }

            return View("Edit_VD", new DetailsViewModel(SMODetails));

        }

        /// <summary>
        /// Vet Experts Model Popup
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VetExperts(int id)
        {
            var userId = HttpContext.User.GetUserId();
            int? centerId = HttpContext.User.GetCenterId();

            var list = UnitOfWork.UserRepository.GetAll(s => s.UserTypeId == UserTypeEnum.VeterinarianExpert, null, s => s.Veterinarians);
            ADOPets.Web.ViewModels.Profile.VetExpertModel model = new ViewModels.Profile.VetExpertModel();

            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                list = UnitOfWork.UserRepository.GetAll(s => s.UserTypeId == UserTypeEnum.VeterinarianExpert && s.CenterID == centerId, null, s => s.Veterinarians);
            }

            List<ViewModels.Profile.VetExpert> listOfExperts = list.Select(p => new ADOPets.Web.ViewModels.Profile.VetExpert(id, p.Id, p.FirstName, p.LastName, p.Email, p.StateId, p.CountryId, false, null, null)).ToList();

            var listOfAssignedExperts = UnitOfWork.SMOExpertRepository.GetAll(p => p.SMORequestID == id);
            List<ViewModels.Profile.VetExpert> listOfAssignedExpertsModel = null;

            listOfAssignedExpertsModel = (from a in listOfAssignedExperts
                                          join b in list
                                          on a.VetExpertID equals b.Id
                                          select new ADOPets.Web.ViewModels.Profile.VetExpert
                                          {
                                              SMOId = id,
                                              Id = b.Id,
                                              FirstName = b.FirstName,
                                              LastName = b.LastName,
                                              Email = b.Email,
                                              Speciality = null,
                                          }).ToList();
            // Compare two List<string> and display items of lstOne not in lstTwo
            List<ViewModels.Profile.VetExpert> lstNew = listOfExperts.Where(n => !listOfAssignedExpertsModel.Any(a => a.Id == n.Id && a.SMOId == n.SMOId)).ToList();
            model.listOfVetExperts = new List<ViewModels.Profile.VetExpert>();
            var list1 = UnitOfWork.VeterinarianRepository.GetAll(p => p.User.UserTypeId == UserTypeEnum.VeterinarianExpert, null);
            List<ViewModels.Profile.VetExpert> listOfExperts1 = list1.Select(p => new ADOPets.Web.ViewModels.Profile.VetExpert(id, p.UserId.Value, p.FirstName, p.LastName, p.Email, p.StateId, p.CountryId, false, null, p.VetSpecialtyID)).ToList();

            model.listOfVetExperts = lstNew.Except(listOfExperts1).Select(i => new ADOPets.Web.ViewModels.Profile.VetExpert
            {
                Id = i.Id,
                FirstName = i.FirstName,
                LastName = i.LastName,
                Email = i.Email,
                SMOId = i.SMOId,
                Speciality = (listOfExperts1.Where(a => a.Id == i.Id).Select(a => a).FirstOrDefault() != null ?
                listOfExperts1.Where(a => a.Id == i.Id).Select(a => a.Speciality).FirstOrDefault() : null)
            }).ToList();


            KeepTempData();
            return PartialView("_VetExpertEdit", model);
        }

        [HttpPost]
        public ActionResult Edit_VD(DetailsViewModel model)
        {
            ViewBag.UserId = userId;
            int smoId = int.Parse(model.SMOId);
            int PetId = (int)TempData["PetId_" + userId];
            if (ModelState.ContainsKey("SMOExpertResponse"))
                ModelState["SMOExpertResponse"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //model.SMOREquest = new SMORequest();

            var expert = UnitOfWork.SMORequestRepository.GetSingleTracking(c => c.ID == smoId, s => s.User);
            expert.VetComment = new EncryptedText(model.SMOVetConclusion);
            expert.RequestReason = new EncryptedText(model.RequestReason);
            expert.MedicalHistoryComment = new EncryptedText(model.MedicalHistoryComment);
            expert.AdditionalInformation = new EncryptedText(model.AdditionalInformation);
            //expert.SMORequestStatusId = SMORequestStatusEnum.Complete;
            ////  expert.InCompleteMedicalRecord = true;
            //expert.ClosedOn = DateTime.Now;
            AddViewModel addmodel = new AddViewModel();
            addmodel.Map(expert);
            expert.IsOwnerRead = false;
            UnitOfWork.SMORequestRepository.Update(expert);
            UnitOfWork.Save();

            var smoexpert = UnitOfWork.SMOExpertRepository.GetSingleTracking(s => s.SMORequestID == expert.ID);
            if (smoexpert != null)
            {
                smoexpert.IsRead = false;
                UnitOfWork.SMOExpertRepository.Update(smoexpert);
                UnitOfWork.Save();
            }

            EmailSender.SendSMOReportReadyMail(expert.User.Email.Value, expert.User.FirstName.Value, expert.User.LastName.Value, SMOHelper.GetFormatedSMOID(expert.ID.ToString()), expert.SMORequestStatusId.ToString());
            if (model.lstAttachment != null)
            {
                foreach (SMODocumentViewModel doc in model.lstAttachment)
                {
                    if (!string.IsNullOrEmpty(doc.DocumentName))
                    {
                        var directoryPath = Path.Combine(getUserAbsolutePath(), Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, model.SMOId.ToString(), Constants.DocumentFolderName);
                        var path = Path.Combine(directoryPath, doc.DocumentName);
                        string savepath = Path.Combine(Constants.DocumentFolderName, doc.DocumentName);
                        var exist = System.IO.File.Exists(path);
                        if (exist)
                        {
                            doc.UserId = userId;
                            UnitOfWork.SMODocumentRepository.Insert(doc.Map(savepath));
                            UnitOfWork.Save();
                        }
                    }
                }
            }
            string RequestReason = (!string.IsNullOrEmpty(model.RequestReason)) ? model.RequestReason : (TempData["RequestReason_" + userId] != null) ? TempData["RequestReason_" + userId].ToString() : String.Empty;
            string MedicalHistoryComment = (!string.IsNullOrEmpty(model.MedicalHistoryComment)) ? model.MedicalHistoryComment : (TempData["MedicalHistoryComment_" + userId] != null) ? TempData["MedicalHistoryComment_" + userId].ToString() : String.Empty;
            string VetComment = (!string.IsNullOrEmpty(model.SMOVetConclusion)) ? model.SMOVetConclusion : (TempData["VetComment_" + userId] != null) ? TempData["VetComment_" + userId].ToString() : String.Empty;
            string AdditionalInformation = (!string.IsNullOrEmpty(model.AdditionalInformation)) ? model.AdditionalInformation : (TempData["AdditionalInformation_" + userId] != null) ? TempData["AdditionalInformation_" + userId].ToString() : String.Empty;

            //  var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == smoId, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            if (RequestReason == "")
                RequestReason = expert.RequestReason;
            if (MedicalHistoryComment == "")
                MedicalHistoryComment = expert.MedicalHistoryComment;
            if (VetComment == "")
                VetComment = expert.VetComment;
            if (AdditionalInformation == "")
                AdditionalInformation = expert.AdditionalInformation;
            string result = GeneratePDF(smoId, RequestReason, MedicalHistoryComment, VetComment, AdditionalInformation);
            if (!String.IsNullOrEmpty(result))
            {
                SMODocument docmodel = new SMODocument();
                if (TempData["fileName_" + userId] != null)
                {
                    docmodel.DocumentName = new EncryptedText(TempData["fileName_" + userId].ToString());
                    string docpath = Path.Combine(Constants.SMOReportFolderName, TempData["fileName_" + userId].ToString());
                    docmodel.DocumentPath = docpath;
                }
                docmodel.DocumentSubTypeId = DocumentSubTypeEnum.SMO;
                docmodel.IsDeleted = false;
                docmodel.SMOId = smoId;
                docmodel.UploadDate = DateTime.Today;
                docmodel.UserId = userId;
                UnitOfWork.SMODocumentRepository.Insert(docmodel);
                UnitOfWork.Save();
            }
            KeepTempData();

            // change the smo status after generating the smo report
            var SMODetails = UnitOfWork.SMORequestRepository.GetSingleTracking(c => c.ID == smoId, s => s.User);
            SMODetails.SMORequestStatusId = SMORequestStatusEnum.Complete;
            SMODetails.ClosedOn = DateTime.Now;
            SMODetails.IsOwnerRead = false;
            UnitOfWork.SMORequestRepository.Update(SMODetails);
            UnitOfWork.Save();

            return Json(new { success = true, successMessage = ADOPets.Web.Resources.Wording.SMO_SubmitReport_SubmitReportSuccessMessage }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Call SMO Request view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SMORequestForm(int id)
        {
            KeepTempData();
            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            return PartialView("_SMORequestForm", new DetailsViewModel(SMODetails));
        }

        /// <summary>
        /// Call SMO Request view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AssignedExperts(int id)
        {

            ViewBag.UserId = userId;

            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            List<Veterinarian> listExpert = new List<Veterinarian>();

            foreach (SMOExpertRelation e in SMODetails.SMOExpertRelations)
            {
                Veterinarian v = new Veterinarian();
                v = UnitOfWork.VeterinarianRepository.GetSingle(a => a.UserId == e.VetExpertID);
                if (v != null)
                    listExpert.Add(v);
            }
            if (SMODetails.SMOExpertRelations.Count() == 0)
            {
                var smoRequest = UnitOfWork.SMORequestRepository.GetSingle(s => s.ID == id); //GetSingle(p => p.VeterinaryExpertId == VeterinaryExpertId && p.VeterinaryId == VeterinaryId);
                if (smoRequest != null)
                {
                    smoRequest.SMORequestStatusId = SMORequestStatusEnum.Open;
                    UnitOfWork.SMORequestRepository.Update(smoRequest);
                    UnitOfWork.Save();
                }
            }

            var results = from result in SMODetails.SMOExpertRelations
                          where result.IsFinal == true
                          select result;
            TempData["ExpertResponseCount_" + userId] = results.Count();
            TempData["EditSMORequestID_" + userId] = id;

            KeepTempData();
            var SMODetails1 = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);

            return View("AssignedExperts", new DetailsViewModel(SMODetails1, listExpert));
        }

        [HttpGet]
        public ActionResult ShowExpertResponse(int id, int vetexpertId)
        {
            var VetExpertResponse = UnitOfWork.SMOExpertRepository.GetSingle(p => p.ID == id, p => p.User, p => p.SMORequest);
            var DocList = UnitOfWork.SMODocumentRepository.GetAll(s => !s.DocumentPath.Contains("Report") && s.SMOId == VetExpertResponse.SMORequest.ID && s.UserId == vetexpertId && !s.IsDeleted);
            List<SMODocumentViewModel> lstDoc = DocList.Select(p => new SMODocumentViewModel(p)).ToList();
            KeepTempData();
            return PartialView("_ShowExpertResponse", new ADOPets.Web.ViewModels.Profile.VetExpert(VetExpertResponse, lstDoc));
        }

        public ActionResult SelectExpert(int id)
        {
            ViewBag.UserId = userId;
            int SMOID = Convert.ToInt32(TempData["EditSMORequestID_" + userId]);
            KeepTempData();

            ViewModels.Profile.VetExpert listOfExperts1 = new ViewModels.Profile.VetExpert();
            var list1 = UnitOfWork.UserRepository.GetAll(p => p.Id == id, null, s => s.SMORequests);
            listOfExperts1 = list1.Select(p => new ADOPets.Web.ViewModels.Profile.VetExpert(SMOID, p.Id, p.FirstName, p.LastName, p.Email, p.StateId, p.CountryId, false, null, null)).FirstOrDefault();
            Veterinarian list = UnitOfWork.VeterinarianRepository.GetAll(p => p.UserId == listOfExperts1.Id, null).FirstOrDefault();
            // ViewModels.Profile.VetExpert listOfExperts1 = new ViewModels.Profile.VetExpert();
            //if (list.Count() != 0)
            //    listOfExperts1 = list.Select(p => new ADOPets.Web.ViewModels.Profile.VetExpert(SMOID, p.UserId.Value, p.FirstName, p.LastName, p.Email, p.StateId, p.CountryId, false, null, p.VetSpecialtyID)).FirstOrDefault();
            //else
            //   listOfExperts1 = listOfExperts;

            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == SMOID, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            List<Veterinarian> listExpert = new List<Veterinarian>();

            foreach (SMOExpertRelation e in SMODetails.SMOExpertRelations)
            {
                Veterinarian v = new Veterinarian();
                v = UnitOfWork.VeterinarianRepository.GetSingle(a => a.UserId == e.VetExpertID);
                if (v != null)
                    listExpert.Add(v);
            }
            ADOPets.Web.ViewModels.Profile.VetExpertModel model = new ViewModels.Profile.VetExpertModel();
            UnitOfWork.SMOExpertRepository.Insert(model.Map(SMOID, listOfExperts1.Id));
            UnitOfWork.Save();
            EmailSender.SendSMOAssignedVetMail(listOfExperts1.Email, listOfExperts1.FirstName, listOfExperts1.LastName, SMODetails.User.FirstName, SMODetails.User.LastName, SMODetails.Pet.Name, SMODetails.Pet.PetTypeId.ToString(), SMODetails.ID.ToString(), SMODetails.Title, SMODetails.UserId.ToString(), SMODetails.User.Email.Value);
            EmailSender.SendSMORequestInProcessMail(SMODetails.User.Email.Value, SMODetails.User.FirstName, SMODetails.User.LastName, SMODetails.ID.ToString());
            SMOExpertRelation b = new SMOExpertRelation();
            b.User = new User();
            b.User.Veterinarian = new Veterinarian();
            b.SMORequestID = SMOID;
            b.ID = UnitOfWork.SMOExpertRepository.GetAll().OrderByDescending(u => u.ID).FirstOrDefault().ID;
            // b.ID = listOfExperts1.Id;
            b.User.Id = listOfExperts1.Id;
            b.User.FirstName.Value = listOfExperts1.FirstName;
            b.User.LastName.Value = listOfExperts1.LastName;
            b.User.Email.Value = listOfExperts1.Email;
            b.User.Veterinarian.VetSpecialtyID = list != null ? list.VetSpecialtyID : null;
            SMODetails.SMOExpertRelations.Add(b);


            var smoreq = UnitOfWork.SMORequestRepository.GetSingle(c => c.ID == SMOID);
            smoreq.SMORequestStatusId = SMORequestStatusEnum.Assigned;
            if (smoreq.SMOSubmittedBy != null)
            {
                smoreq.IsOwnerRead = false;
            }
            UnitOfWork.SMORequestRepository.Update(smoreq);
            UnitOfWork.Save(); KeepTempData();
            return PartialView("_showExpertList", new DetailsViewModel(SMODetails, "AssignExpert", listExpert));
        }

        /// <summary>
        /// Call SMO Show Report View To Owner
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowSMOOwnerReport(int id)
        {
            KeepTempData();
            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);

            return PartialView("_ShowSMOOwnerReport", new DetailsViewModel(SMODetails));
        }

        /// <summary>
        /// Call SMO Show Report View To Vet
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowSMOVetReport(int id, int PetId)
        {
            TempData["PetId_" + userId] = PetId;
            TempData["EditSMORequestID_" + userId] = id;
            KeepTempData();
            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            var DocList = UnitOfWork.SMODocumentRepository.GetAll(s => s.SMOId == id && !s.IsDeleted);
            List<SMODocumentViewModel> lstDoc = DocList.Select(p => new SMODocumentViewModel(p)).ToList();
            foreach (var item in lstDoc)
            {
                if (item.DocumentPath.Contains("Report"))
                {
                    string ownerId = item.UserId.ToString();
                    return RedirectToAction("GetReport", new { @fileName = item.DocName, @ownerId = ownerId });
                }
            }
            return RedirectToAction("Index");//PartialView("_ShowSMOVetReport", new DetailsViewModel(SMODetails));
        }

        [HttpGet]
        public FileResult GetFile(string fileName, string imageType, string userInfoPath, string ownerId)
        {
            int PetId = (int)TempData["PetId_" + userId];
            int id = (int)TempData["EditSMORequestID_" + userId];
            if (!string.IsNullOrEmpty(fileName))
            {
                var directoryPath = "";
                var userAbsoluteInfoPath = "";
                if (ownerId != userId.ToString() && !string.IsNullOrEmpty(ownerId))
                {
                    int uid = int.Parse(ownerId);
                    var user = UnitOfWork.UserRepository.GetSingle(a => a.Id == uid);
                    userAbsoluteInfoPath = Path.Combine(WebConfigHelper.UserFilesPath, user.InfoPath, user.Id.ToString());
                }
                else
                {
                    userAbsoluteInfoPath = getUserAbsolutePath();
                }
                if (string.IsNullOrEmpty(imageType))
                {
                    directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, id.ToString());
                }
                else
                {
                    directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, id.ToString());
                }
                var path = Path.Combine(directoryPath, fileName);

                var exist = System.IO.File.Exists(path);

                if (exist)
                {
                    KeepTempData();
                    string extension = new FileInfo(path).Extension;
                    if (extension != null || extension != string.Empty)
                    {
                        switch (extension)
                        {
                            case ".pdf":
                                return File(path, "application/pdf", Path.GetFileName(path));
                            case ".txt":
                                return File(path, "application/plain", Path.GetFileName(path));
                            case ".jpeg":
                                return File(path, "application/jpeg", Path.GetFileName(path));
                            //case ".jpg":
                            //    return File(path, "application/jpeg", Path.GetFileName(path));
                            case ".doc":
                                return File(path, "application/msword", Path.GetFileName(path));
                            case ".docx":
                                return File(path, "application/msword", Path.GetFileName(path));

                            default:
                                return File(path, "application/octet-stream", Path.GetFileName(path));
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Add BioData For Expert
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddBioForExpert(int id)
        {
            //var VetExpertResponse = UnitOfWork.SMOExpertRepository.GetSingle(p => p.ID == id);
            ExpertBioData expert = new ExpertBioData();
            expert.User = new Model.User();
            expert.User.Id = id;
            int? VeterinaryExpertId = Convert.ToInt32(id);
            int? VeterinaryId = Convert.ToInt32(userId);
            var VetExpertBiodata = UnitOfWork.ExpertBioDataRepository.GetSingle(s => s.VeterinaryExpertId == VeterinaryExpertId && s.VeterinaryId == VeterinaryId, s => s.User, s => s.User1); //GetSingle(p => p.VeterinaryExpertId == VeterinaryExpertId && p.VeterinaryId == VeterinaryId);
            if (VetExpertBiodata != null)
            {
                if (VetExpertBiodata.Information != null)
                    expert.Information = VetExpertBiodata.Information;
            }
            KeepTempData();
            return PartialView("_AddBioForExpert", new ADOPets.Web.ViewModels.SMO.ExpertBio(expert));
        }

        /// <summary>
        /// Add BioData For Expert
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveBioForExpert(int vetExpertId, string txtcomment)
        {
            int smoid = 0;
            //var VetExpertResponse = UnitOfWork.SMOExpertRepository.GetSingle(p => p.ID == id);
            if (TempData["EditSMORequestID_" + userId] != null)
                smoid = Convert.ToInt32(Convert.ToString(TempData["EditSMORequestID_" + userId]));
            KeepTempData();
            var VetExpertBiodata = UnitOfWork.ExpertBioDataRepository.GetSingle(s => s.VeterinaryExpertId == vetExpertId && s.VeterinaryId == userId); //GetSingle(p => p.VeterinaryExpertId == VeterinaryExpertId && p.VeterinaryId == VeterinaryId);
            if (VetExpertBiodata != null)
            {
                if (VetExpertBiodata.Information == null)
                {
                    VetExpertBiodata.Information = txtcomment;
                    UnitOfWork.ExpertBioDataRepository.Update(VetExpertBiodata);
                }
                else
                {
                    VetExpertBiodata.Information = txtcomment;
                    UnitOfWork.ExpertBioDataRepository.Update(VetExpertBiodata);
                }
                UnitOfWork.Save();
            }
            else
            {
                ExpertBioData expertBio = new ExpertBioData();
                expertBio.VeterinaryExpertId = vetExpertId;
                expertBio.VeterinaryId = userId;
                expertBio.Information = txtcomment;
                UnitOfWork.ExpertBioDataRepository.Insert(expertBio);
                UnitOfWork.Save();
            }

            return Json(new { success = true, smoid = smoid }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method For Submit SMO Report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SubmitReport(int id)
        {
            ViewBag.UserId = userId;
            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            var results = from result in SMODetails.SMOExpertRelations
                          where result.IsFinal == true
                          select result;

            var DocList = UnitOfWork.SMODocumentRepository.GetAll(s => !s.DocumentPath.Contains("Report") && s.SMOId == id && s.UserId == userId && !s.IsDeleted);
            List<SMODocumentViewModel> lstDoc = DocList.Select(p => new SMODocumentViewModel(p)).ToList();
            TempData["ExpertResponseCount_" + userId] = results.Count();
            KeepTempData();
            return View("SubmitReport", new DetailsViewModel(SMODetails, lstDoc));

        }

        /// <summary>
        /// Get All Expert Discussions for SMO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExpertCommittee(int id)
        {
            ViewBag.UserId = userId;
            // var ExpertList = UnitOfWork.SMOExpertCommitteeRepository.GetAll(p => p.SMORequestId == id, null, e => e.Veterinarian).GroupBy(a => a.Veterinarian.Id);

            var ExpertList1 = UnitOfWork.SMOExpertRepository.GetAll(p => p.SMORequestID == id, null, s => s.User);
            //Veterinarian ExpertNameList = null;
            //List<Veterinarian> list = new List<Veterinarian>();
            //foreach (var temp in ExpertList)
            //{
            //    int vetId = temp.Select(a => a.Veterinarian.Id).FirstOrDefault();
            //    ExpertNameList = UnitOfWork.VeterinarianRepository.GetSingle(p => p.Id == vetId);
            //    list.Add(ExpertNameList);
            //}
            //ViewBag.ExpertCommittee = list.Select(i => new SelectListItem { Text = (i.FirstName + " " + i.LastName).ToString(), Value = i.Id.ToString() });
            ViewBag.ExpertCommittee = ExpertList1.Select(i => new SelectListItem { Text = (i.User.FirstName + " " + i.User.LastName).ToString(), Value = i.User.Id.ToString() });
            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User, p => p.SMOExpertCommittees.Select(a => a.User));
            KeepTempData();
            return View("ExpertCommittee", new DetailsViewModel(SMODetails));
        }

        /// <summary>
        /// Get Preview Of SMO Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreviewSMO(int id)
        {
            string RequestReason = (TempData["RequestReason_" + userId] != null) ? TempData["RequestReason_" + userId].ToString() : String.Empty;
            string MedicalHistoryComment = (TempData["MedicalHistoryComment_" + userId] != null) ? TempData["MedicalHistoryComment_" + userId].ToString() : String.Empty;
            string VetComment = (TempData["VetComment_" + userId] != null) ? TempData["VetComment_" + userId].ToString() : String.Empty;
            string AdditionalInformation = (TempData["AdditionalInformation_" + userId] != null) ? TempData["AdditionalInformation_" + userId].ToString() : String.Empty;

            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            if (RequestReason == "")
                RequestReason = SMODetails.RequestReason;
            if (MedicalHistoryComment == "")
                MedicalHistoryComment = SMODetails.MedicalHistoryComment;
            if (VetComment == "")
                VetComment = SMODetails.VetComment;
            if (AdditionalInformation == "")
                AdditionalInformation = SMODetails.AdditionalInformation;
            string path = GeneratePDF(id, RequestReason, MedicalHistoryComment, VetComment, AdditionalInformation);
            var exist = System.IO.File.Exists(path);
            KeepTempData();

            if (exist)
            {
                string extension = new FileInfo(path).Extension;
                if (extension != null || extension != string.Empty)
                {
                    ViewData["PDFFileName"] = path;
                    return File(path, "application/pdf", Path.GetFileName(path));
                }
            }
            else
            {
                throw new Exception("Ajax Call Failed!");
            }
            return View();
        }

        /// <summary>
        /// Show BioData For Expert
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ShowBioForExpert(int id)
        {
            var VetExpertBiodata = UnitOfWork.ExpertBioDataRepository.GetSingle(p => p.VeterinaryExpertId == id && p.VeterinaryId == userId);
            KeepTempData();
            return PartialView("_ShowBioForExpert", new ADOPets.Web.ViewModels.SMO.ExpertBio(VetExpertBiodata.Information));
        }

        /// <summary>
        /// View SMO Report
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewReport(int id)
        {
            ViewBag.UserId = userId;
            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            var DocList = UnitOfWork.SMODocumentRepository.GetAll(s => !s.DocumentPath.Contains("Report") && s.SMOId == id && s.UserId == userId && !s.IsDeleted);
            List<SMODocumentViewModel> lstDoc = DocList.Select(p => new SMODocumentViewModel(p)).ToList();
            KeepTempData();
            return View("ViewReport", new DetailsViewModel(SMODetails, lstDoc));
        }

        /// <summary>
        /// index for ve login
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult Index_VE()
        {
            return View();
        }

        public ActionResult EditPet(int PetId, int SmoId)
        {
            TempData["EditSMORequestID_" + userId] = SmoId;
            Session["EditSMORequestID"] = SmoId;
            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == SmoId, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            if (SMODetails.SMORequestStatusId != SMORequestStatusEnum.Complete)
                TempData["IncompleteMedicalRecord_" + userId] = SMODetails.InCompleteMedicalRecord;

            Session[Constants.SessionCurrentUserRequestFrom] = "smo";
            KeepTempData();
            return RedirectToAction("Edit", "Pet", new { id = PetId });
        }

        [HttpPost]
        public ActionResult SendMessage(string response, int smoid)
        {
            ViewBag.UserId = userId;
            //  var veterianId = UnitOfWork.VeterinarianRepository.GetSingle(p => p.UserId == userId);
            AddExpertCommitteeModel expertModel = new AddExpertCommitteeModel();
            expertModel.Message = response;
            expertModel.VeterianId = userId;
            expertModel.SMOId = smoid;
            expertModel.Date = DateTime.Now;
            UnitOfWork.SMOExpertCommitteeRepository.Insert(expertModel.Map());
            UnitOfWork.Save();
            KeepTempData();
            // return RedirectToAction("ExpertCommittee", new { id=smoid});
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMessage(int msgId, int smoId)
        {
            ViewBag.UserId = userId;
            var deleteMessageObject = UnitOfWork.SMOExpertCommitteeRepository.GetSingle(sm => sm.Id == msgId && sm.SMORequestId == smoId);
            UnitOfWork.SMOExpertCommitteeRepository.Delete(deleteMessageObject);
            UnitOfWork.Save();
            KeepTempData();
            return Json(new { success = true, successMessage = ADOPets.Web.Resources.Wording.SMO_EmailExpert_DeleteSuccessMessage }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteAssignedExpert(int id)
        {
            var SMOExpert = UnitOfWork.SMOExpertRepository.GetSingle(c => c.ID == id);
            long? smoid = SMOExpert.SMORequestID;
            UnitOfWork.SMOExpertRepository.Delete(SMOExpert);
            UnitOfWork.Save();
            KeepTempData();
            //Success(Resources.Smo_Index_.Allergy.Delete.DeleteSuccessMessage);
            return Json(new { success = true, smoid = smoid }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DeleteConfirmAssignedExpert(int ID)
        {
            var SMOExpert = UnitOfWork.SMOExpertRepository.GetSingle(c => c.ID == ID, s => s.User, s => s.SMORequest, s => s.SMORequest.Pet);

            KeepTempData();
            return PartialView("_ExpertDeleteConfirmation", new ExpertRelationViewModel(SMOExpert));
        }

        [HttpGet]
        public ActionResult VetExpertResponse(int id)
        {
            ViewBag.UserId = userId;
            var VE_SMO = UnitOfWork.SMOExpertRepository.GetSingle(s => s.SMORequest.ID == id && s.VetExpertID == userId, s => s.SMORequest, s => s.User, s => s.SMORequest.Pet, s => s.SMORequest.User, s => s.SMORequest.Pet.Users);//.Select(p => new ExpertRelationViewModel(p, p.SMORequest.Pet.Users.FirstOrDefault()));
            var DocList = UnitOfWork.SMODocumentRepository.GetAll(s => !s.DocumentPath.Contains("Report") && s.SMOId == id && s.UserId == userId && !s.IsDeleted);
            List<SMODocumentViewModel> lstDoc = DocList.Select(p => new SMODocumentViewModel(p)).ToList();
            KeepTempData();
            return View("VetExpertResponse", new ExpertRelationViewModel(VE_SMO, lstDoc));
        }

        [HttpPost]
        public ActionResult VetExpertResponse(ExpertRelationViewModel addViewModel)
        {
            DetailsViewModel model = new DetailsViewModel();
            int PetId = (int)TempData["PetId_" + userId];
            if (HttpContext.User.ToCustomPrincipal().CustomIdentity.UserRoleName == "VeterinarianExpert")
            {
                if (ModelState.ContainsKey("SMOVetConclusion"))
                    ModelState["SMOVetConclusion"].Errors.Clear();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (TempData["SMODetailsViewModel"] != null)
                    model = (DetailsViewModel)TempData["SMODetailsViewModel"];
                model.SMOExpert = new SMOExpertRelation();

                var expert = UnitOfWork.SMOExpertRepository.GetSingle(c => c.ID == addViewModel.ExpertRelId, navigationProperties: se => se.User);
                addViewModel.Map(expert);
                expert.IsFinal = true;
                UnitOfWork.SMOExpertRepository.Update(expert);
                UnitOfWork.Save();
                List<User> list = null;
                #region find VD
                var userVD = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId && s.VetDirectorID != null);
                var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(s => s.ID == expert.SMORequestID, s => s.Pet);
                if (userVD != null)
                {
                    int VDCId = Convert.ToInt32(userVD.VetDirectorID);
                    list = UnitOfWork.UserRepository.GetAll(s => s.UserTypeId == UserTypeEnum.VeterinarianLight).ToList();
                    //foreach (User u in list)
                    //{
                    //    EmailSender.SendSMOExpertResponseMail(u.Email.Value, u.FirstName.Value, u.LastName.Value, SMODetails.Pet.Name.Value, SMODetails.Pet.PetTypeId.ToString(), SMOHelper.GetFormatedSMOID(SMODetails.ID.ToString()), SMODetails.Title);
                    //}
                }
                else
                {
                    list = UnitOfWork.UserRepository.GetAll(s => s.UserTypeId == UserTypeEnum.VeterinarianAdo).ToList();
                }

                foreach (User u in list)
                {
                    if (expert.User != null)
                    {
                        EmailSender.SendSMOExpertResponseMail(u.Email.Value, u.FirstName.Value + " " + u.LastName.Value, expert.User.FirstName + " " + expert.User.LastName, SMODetails.Pet.Name.Value, SMODetails.Pet.PetTypeId.ToString(), SMOHelper.GetFormatedSMOID(SMODetails.ID.ToString()), SMODetails.Title);
                    }
                    else
                    {
                        EmailSender.SendSMOExpertResponseMail(u.Email.Value, u.FirstName.Value + " " + u.LastName.Value, " ", SMODetails.Pet.Name.Value, SMODetails.Pet.PetTypeId.ToString(), SMOHelper.GetFormatedSMOID(SMODetails.ID.ToString()), SMODetails.Title);
                    }
                }
                #endregion
                if (addViewModel.lstAttachment != null)
                {
                    foreach (SMODocumentViewModel doc in addViewModel.lstAttachment)
                    {
                        var directoryPath = Path.Combine(getUserAbsolutePath(), Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, addViewModel.SMOId.ToString(), Constants.DocumentFolderName);
                        var path = Path.Combine(directoryPath, doc.DocumentName);
                        string savepath = Path.Combine(Constants.DocumentFolderName, doc.DocumentName);
                        var exist = System.IO.File.Exists(path);
                        if (exist)
                        {
                            doc.UserId = userId;
                            UnitOfWork.SMODocumentRepository.Insert(doc.Map(savepath));
                            UnitOfWork.Save();
                        }
                    }
                }
                Int64 smoid = Int64.Parse(addViewModel.SMOId);
                var smoreq = UnitOfWork.SMORequestRepository.GetSingle(c => c.ID == smoid);
                AddViewModel addmodel = new AddViewModel();
                addmodel.MapSmo(smoreq);
                smoreq.IsRead = false;
                UnitOfWork.SMORequestRepository.Update(smoreq);
                UnitOfWork.Save();
                KeepTempData();
                Success(ADOPets.Web.Resources.Wording.Smo_Edit_AddExpertResponseMessage);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }


            return View("Index_VD");
        }

        [HttpPost]
        public ActionResult SaveSMOResponse(ExpertRelationViewModel addViewModel)
        {
            DetailsViewModel model = new DetailsViewModel();
            int PetId = (int)TempData["PetId_" + userId];
            if (HttpContext.User.ToCustomPrincipal().CustomIdentity.UserRoleName == "VeterinarianExpert")
            {
                if (ModelState.ContainsKey("SMOVetConclusion"))
                    ModelState["SMOVetConclusion"].Errors.Clear();
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if (TempData["SMODetailsViewModel"] != null)
                    model = (DetailsViewModel)TempData["SMODetailsViewModel"];
                model.SMOExpert = new SMOExpertRelation();

                var expert = UnitOfWork.SMOExpertRepository.GetSingle(c => c.ID == addViewModel.ExpertRelId, navigationProperties: se => se.User);
                addViewModel.Map(expert);
                UnitOfWork.SMOExpertRepository.Update(expert);
                UnitOfWork.Save();

                if (addViewModel.lstAttachment != null)
                {
                    foreach (SMODocumentViewModel doc in addViewModel.lstAttachment)
                    {
                        var directoryPath = Path.Combine(getUserAbsolutePath(), Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, addViewModel.SMOId.ToString(), Constants.DocumentFolderName);
                        var path = Path.Combine(directoryPath, doc.DocumentName);
                        string savepath = Path.Combine(Constants.DocumentFolderName, doc.DocumentName);
                        var exist = System.IO.File.Exists(path);
                        if (exist)
                        {
                            doc.UserId = userId;
                            UnitOfWork.SMODocumentRepository.Insert(doc.Map(savepath));
                            UnitOfWork.Save();
                        }
                    }
                }
                //  Int64 smoid = Int64.Parse(addViewModel.SMOId);
                //  var smoreq = UnitOfWork.SMORequestRepository.GetSingle(c => c.ID == smoid);
                //  AddViewModel addmodel = new AddViewModel();
                //  addmodel.MapSmo(smoreq);
                ////  smoreq.IsRead = false;
                //  UnitOfWork.SMORequestRepository.Update(smoreq);
                //  UnitOfWork.Save();
                KeepTempData();
                Success(ADOPets.Web.Resources.Wording.Smo_Edit_SaveExpertResponseMessage);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }


            return View("VetExpertResponse", model.SMOId);
        }

        public ActionResult VetExpertAdo(int id)
        {
            KeepTempData();
            if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()))
            {

                return RedirectToAction("AssignedExperts", "SMO", new { id = id });
            }
            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            bool flagResponse = false;
            foreach (var item in SMODetails.SMOExpertRelations)
            {
                if (item.VetExpertID == userId && item.ExpertResponse != null)
                {
                    flagResponse = true;
                }
            }
            if (flagResponse)
            {
                return RedirectToAction("ShowVetExpertResponse", "SMO", new { id = id });
            }

            return RedirectToAction("VetExpertResponse", "SMO", new { id = id });
        }

        public ActionResult ShowVetExpertResponse(int id)
        {
            ViewBag.UserId = userId;
            var VE_SMO = UnitOfWork.SMOExpertRepository.GetSingle(s => s.SMORequest.ID == id && s.VetExpertID == userId, s => s.SMORequest, s => s.User, s => s.SMORequest.Pet, s => s.SMORequest.User, s => s.SMORequest.Pet.Users);
            var DocList = UnitOfWork.SMODocumentRepository.GetAll(s => !s.DocumentPath.Contains("Report") && s.SMOId == id && s.UserId == userId && !s.IsDeleted);
            List<SMODocumentViewModel> lstDoc = DocList.Select(p => new SMODocumentViewModel(p)).ToList();
            KeepTempData();
            return View("ViewResponse", new ExpertRelationViewModel(VE_SMO, lstDoc));
        }

        [HttpGet]
        public ActionResult GetImage(string fileName, string imgType)
        {
            var ext = "";
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    int PetId = (int)TempData["PetId_" + userId];
                    int id = (int)TempData["EditSMORequestID_" + userId];
                    string fileNameNew = "";
                    var directoryPath = "";
                    if (!string.IsNullOrEmpty(imgType))
                    {
                        directoryPath = Path.Combine(getUserAbsolutePath(), Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, id.ToString(), Constants.DocumentFolderName);
                        fileNameNew = "Thumb_" + fileName;
                    }
                    else
                    {
                        directoryPath = Path.Combine(getUserAbsolutePath(), Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, id.ToString(), Constants.DocumentFolderName);
                        var splitExt = fileName.Split('.');
                        ext = splitExt[1].ToUpper();
                        fileNameNew = splitExt[0] + "." + ext;
                    }
                    KeepTempData();
                    var path = Path.Combine(directoryPath, fileNameNew);
                    var exist = System.IO.File.Exists(path);
                    if (exist)
                    {
                        var contentType = MimeMapping.GetMimeMapping(fileNameNew);
                        ext = ext.ToLower();
                        if (ext != null || ext != string.Empty)
                        {
                            switch (ext)
                            {
                                case ".pdf":
                                    return File(path, "application/pdf", Path.GetFileName(path));
                                case ".txt":
                                    return File(path, "application/plain", Path.GetFileName(path));
                                case ".jpeg":
                                    return File(path, "application/jpeg", Path.GetFileName(path));
                                //case ".jpg":
                                //    return File(path, "application/jpeg", Path.GetFileName(path));
                                case ".doc":
                                    return File(path, "application/msword", Path.GetFileName(path));
                                case ".docx":
                                    return File(path, "application/msword", Path.GetFileName(path));

                                default:
                                    return File(path, "application/octet-stream", Path.GetFileName(path));
                            }
                        }                        // return new FileStreamResult(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read), contentType);
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch { return null; }
        }

        public string GeneratePDF(int id, string RequestReason = null, string MedicalHistoryComment = null, string VetComment = null, string AdditionalInformation = null)
        {
            #region generatePDF
            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == id, p => p.SMOInvestigations, p => p.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            #region find VD
            var userVD = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
            string VetName = String.Empty;
            string VetBio = String.Empty;
            if (userVD != null && (Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString())))
            {
                int VDCId = Convert.ToInt32(userVD.VetDirectorID);
                VetName = userVD.FirstName + " " + userVD.LastName;
                var vetbio = UnitOfWork.ExpertBioDataRepository.GetSingle(a => a.VeterinaryExpertId == userVD.Id);
                VetBio = (vetbio != null) ? vetbio.Information : String.Empty;
            }
            else
            {
                var list = UnitOfWork.UserRepository.GetAll(s => s.UserTypeId == UserTypeEnum.VeterinarianAdo).FirstOrDefault();
                VetName = list.FirstName + " " + list.LastName;
                var vetbio = UnitOfWork.ExpertBioDataRepository.GetSingle(a => a.VeterinaryExpertId == list.Id);
                VetBio = (vetbio != null) ? vetbio.Information : String.Empty;
            }
            #endregion
            var fileName = string.Empty;
            int PetId = (int)TempData["PetId_" + userId];
            fileName = DateTime.Now.ToString("yyyymmddhhmmss") + SMOHelper.GetFormatedSMOID(id.ToString()) + ".pdf";
            var directoryPath = Path.Combine(getUserAbsolutePath(), Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, id.ToString(), Constants.SMOReportFolderName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var path = Path.Combine(directoryPath, fileName);
            DetailsViewModel model = new DetailsViewModel(SMODetails);
            model.SMOREquest.RequestReason = RequestReason;
            model.SMOREquest.MedicalHistoryComment = MedicalHistoryComment;
            model.SMOREquest.VetComment.Value = VetComment;
            model.SMOREquest.AdditionalInformation = AdditionalInformation;
            model.VetName = VetName;
            model.VetBio = VetBio;
            ExpertBioData expertBio = new ExpertBioData();
            foreach (var s in model.lstExpertRel)
            { //TODO :: CHKK
                s.User = UnitOfWork.UserRepository.GetSingle(a => a.Id == s.VetExpertID);
                expertBio = UnitOfWork.ExpertBioDataRepository.GetSingleTracking(a => a.VeterinaryExpertId == s.VetExpertID && a.VeterinaryId == userVD.Id);
                s.expertBioData = expertBio;
            }

            PDFHelper.GeneratePDF(path, model);
            TempData["fileName_" + userId] = fileName;
            KeepTempData();
            return path;
            #endregion
        }

        public FileResult GetReport(string fileName, string imageType, string userInfoPath, string ownerId)
        {
            int PetId = (int)TempData["PetId_" + userId];
            int id = (int)TempData["EditSMORequestID_" + userId];
            string userAbsoluteInfoPath = String.Empty;
            if (ownerId != userId.ToString())
            {
                int uid = int.Parse(ownerId);
                var user = UnitOfWork.UserRepository.GetSingle(a => a.Id == uid);
                userAbsoluteInfoPath = Path.Combine(WebConfigHelper.UserFilesPath, user.InfoPath, user.Id.ToString());
            }
            else
            {
                userAbsoluteInfoPath = getUserAbsolutePath();
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                var directoryPath = "";
                if (string.IsNullOrEmpty(imageType))
                {
                    directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, id.ToString(), Constants.SMOReportFolderName);
                }
                else
                {
                    directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, id.ToString(), Constants.SMOReportFolderName);
                }
                var path = Path.Combine(directoryPath, fileName);
                var exist = System.IO.File.Exists(path);

                if (exist)
                {
                    KeepTempData();
                    return File(path, "application/pdf", fileName);
                }
            }
            return null;
        }

        [HttpGet]
        public ActionResult saveTempData(string RequestReason, string MedicalHistoryComment, string VetComment, string AdditionalInformation)
        {
            TempData["RequestReason_" + userId] = RequestReason;
            TempData["MedicalHistoryComment_" + userId] = MedicalHistoryComment;
            TempData["VetComment_" + userId] = VetComment;
            TempData["AdditionalInformation_" + userId] = AdditionalInformation;
            KeepTempData();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ConfirmSendReport()
        {
            KeepTempData();
            return PartialView("_SendReportConfirmation");
        }

        public void RemoveSeletedFile(string Filename, string PetId, string SmoId)
        {
            //string newfileName = DateTime.Now.Day + "" + DateTime.Now.Ticks + DateTime.Now.Month + Filename.Trim().Replace(" ", "_");
            var dirPath = Path.Combine(getUserAbsolutePath(), Constants.PetsFolderName, PetId, Constants.DocumentFolderName, Constants.SMOFolderName, SmoId, Constants.DocumentFolderName);
            var path = Path.Combine(dirPath, Filename);
            var exist = System.IO.File.Exists(path);

            if (exist)
            {
                System.IO.File.Delete(path);
            }
            KeepTempData();
        }

        public void DeleteDocument(string fileId, string SMOId)
        {
            KeepTempData();
            int docId = Convert.ToInt32(fileId);
            int smoId = Convert.ToInt32(SMOId);

            var smoDocument = UnitOfWork.SMODocumentRepository.GetSingle(d => d.Id == docId);
            var petId = UnitOfWork.SMORequestRepository.GetSingle(s => s.ID == smoId).PetId;

            var dirPath = Path.Combine(getUserAbsolutePath(), Constants.PetsFolderName, petId.ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, SMOId);
            var path = Path.Combine(dirPath, smoDocument.DocumentPath);
            var exist = System.IO.File.Exists(path);

            if (exist)
            {
                System.IO.File.Delete(path);
            }
            KeepTempData();
            UnitOfWork.SMODocumentRepository.Delete(smoDocument);
            UnitOfWork.Save();
        }

        [HttpPost]
        public ActionResult SaveSMODetails(DetailsViewModel model)
        {
            ViewBag.UserId = userId;
            int smoId = int.Parse(model.SMOId);
            int PetId = (int)TempData["PetId_" + userId];
            if (ModelState.ContainsKey("SMOExpertResponse"))
                ModelState["SMOExpertResponse"].Errors.Clear();
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var expert = UnitOfWork.SMORequestRepository.GetSingle(c => c.ID == smoId);
            expert.VetComment = new EncryptedText(model.SMOVetConclusion);
            expert.RequestReason = new EncryptedText(model.RequestReason);
            expert.MedicalHistoryComment = new EncryptedText(model.MedicalHistoryComment);
            expert.AdditionalInformation = new EncryptedText(model.AdditionalInformation);

            AddViewModel addmodel = new AddViewModel();
            addmodel.Map(expert);
            UnitOfWork.SMORequestRepository.Update(expert);
            UnitOfWork.Save();

            if (model.lstAttachment != null)
            {
                foreach (SMODocumentViewModel doc in model.lstAttachment)
                {
                    if (!string.IsNullOrEmpty(doc.DocumentName))
                    {
                        var directoryPath = Path.Combine(getUserAbsolutePath(), Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, model.SMOId.ToString(), Constants.DocumentFolderName);
                        var path = Path.Combine(directoryPath, doc.DocumentName);
                        string savepath = Path.Combine(Constants.DocumentFolderName, doc.DocumentName);
                        var exist = System.IO.File.Exists(path);
                        if (exist)
                        {
                            doc.UserId = userId;
                            UnitOfWork.SMODocumentRepository.Insert(doc.Map(savepath));
                            UnitOfWork.Save();
                        }
                    }
                }
            }

            KeepTempData();
            return Json(new { success = true, successMessage = ADOPets.Web.Resources.Wording.SMO_SubmitReport_SaveSuccessMessage }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult petInfoStatus(int smoId, bool isInComplete)
        {
            var smo = UnitOfWork.SMORequestRepository.GetSingle(s => s.ID == smoId);
            if (smo != null)
            {
                smo.InCompleteMedicalRecord = isInComplete;
                UnitOfWork.SMORequestRepository.Update(smo);
                UnitOfWork.Save();
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPetInfoStatus(int smoId)
        {
            var smo = UnitOfWork.SMORequestRepository.GetSingle(s => s.ID == smoId);
            return Json(new { success = smo.InCompleteMedicalRecord }, JsonRequestBehavior.AllowGet);
        }

        public string GenerateFileExtDiv(string filename)
        {
            var extNew = Path.GetExtension(filename);
            var ext = extNew.ToLower();

            var className = "fa  fa-file-text-o";

            if (!string.IsNullOrEmpty(ext))
            {
                if (ext.Contains("jpg") || ext.Contains("png") || ext.Contains("gif") || ext.Contains("jpeg"))
                {
                    className = "fa fa-file-photo-o (alias)";
                }
                else if (ext.Contains("txt"))
                {
                    className = "fa  fa-file-text-o";
                }
                else if (ext.Contains("pdf"))
                {
                    className = "fa fa-file-pdf-o";
                }
                else if (ext.Contains("doc"))
                {
                    className = "fa  fa-file-word-o";
                }
                else if (ext.Contains("xls"))
                {
                    className = "fa fa-file-excel-o";
                }
            }

            var text = className != "fa fa-file-photo-o (alias)" ? string.Format("<i class=\"{0}\"></i>", className) : string.Format("<span class=\"{0}\"></span>", className);

            return text;
        }

        #endregion

        #region Admin/VD/VA SMO Creation

        /// <summary>
        /// Action For Add New SMO After VD Login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NewSMO()
        {
            var UserType = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId).UserTypeId;
            if (UserType.ToString().Equals ("OwnerAdmin"))
            {
                var UserRepository = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId).UserSubscriptionId;
                var Subscription = UnitOfWork.UserSubscriptionRepository.GetSingle(s => s.Id == UserRepository, p => p.Subscription);

                TempData.Clear();
                ViewBag.UserId = userId;
                TempData["HasSMO_" + userId] = Subscription.Subscription.HasSMO;
                KeepTempData();
            }
            AddViewModel model = new AddViewModel();
            ViewBag.UserId = userId;
            model.objSetup = new AddSetupViewModel();
            model.objSetup.Pets = new List<ViewModels.Pet.IndexViewModel>();
            var dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == userId));
            //  var userData = dbPets.Any(u => u.Users.FirstOrDefault());
            var pets = dbPets.Select(p => new ADOPets.Web.ViewModels.Pet.IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, p.GenderId, p.Image)).ToList();
            model.objSetup.Pets = pets;
            model.objBilling = new AddBillingViewModel();
            return View(model);
        }

        /// <summary>
        /// Edit SMO details creted by VD/Admin ::  owner
        /// </summary>
        /// <param name="smoId"></param>
        /// <returns></returns>
        public ActionResult EditSMO(int smoId)
        {
            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(p => p.ID == smoId);

            if (SMODetails.SMORequestStatusId == SMORequestStatusEnum.Complete)
            {
                return RedirectToAction("SMODetails", new { id = smoId });
            }
            else
            {
                int usrId = Convert.ToInt32(SMODetails.UserId);
                var userSubscriptionId = UnitOfWork.UserRepository.GetSingle(s => s.Id == usrId).UserSubscriptionId;
                var userSubscription = UnitOfWork.UserSubscriptionRepository.GetSingle(s => s.Id == userSubscriptionId, p => p.Subscription);

                SMODetails.IsOwnerRead = true;
                UnitOfWork.SMORequestRepository.Update(SMODetails);
                UnitOfWork.Save();


                TempData.Clear();
                ViewBag.UserId = usrId;
                TempData["HasSMO_" + usrId] = userSubscription.Subscription.HasSMO;
                KeepTempData();
                AddViewModel model = new AddViewModel();
                List<InvestigationViewModel> lstInvestigation = UnitOfWork.SMOInvestigationRepository.GetAll(inv => inv.SMORequestID == smoId).Select(i => new InvestigationViewModel(i)).ToList();
                model.objSetup = UnitOfWork.SMORequestRepository.GetAll(s => s.ID == smoId).Select(s => new AddSetupViewModel(s, lstInvestigation)).FirstOrDefault();
                model.objSetup.Pets = new List<ViewModels.Pet.IndexViewModel>();
                var dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == usrId) && p.Id == model.objSetup.PetId, navigationProperties: p => p.Users);
                var userData = dbPets.FirstOrDefault().Users.FirstOrDefault();
                var pets = dbPets.Select(p => new ADOPets.Web.ViewModels.Pet.IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, p.GenderId, p.Image, userData)).ToList();
                model.objSetup.Pets = pets;
                model.objBilling = new AddBillingViewModel();
                TempData["Investigation_" + userId] = lstInvestigation;
                TempData["PetId_" + userId] = model.objSetup.PetId;
                var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == model.objSetup.PetId);
                TempData["PetName_" + userId] = pet.Name.Value;

                TempData["SMOID_" + userId] = model.objSetup.Id;
                return View("Add", model);
            }
        }

        public ActionResult DeleteSMO(object smoId)
        {
            int smoRequestId = Convert.ToInt32(smoId);

            var SMODetails = UnitOfWork.SMORequestRepository.GetSingle(s => s.ID == smoRequestId, s => s.SMOInvestigations, s => s.Pet, e => e.SMOExpertRelations.Select(a => a.User), p => p.User);
            UnitOfWork.SMORequestRepository.Delete(SMODetails);
            UnitOfWork.Save();
            return Json(new { success = true, successMessage = Resources.Wording.SMO_Index_DeleteSMOSuccessMessage }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Private Methods

        private void KeepTempData()
        {
            TempData.Keep("HasSMO_" + userId);
            TempData.Keep("Setup_" + userId);
            TempData.Keep("Billing_" + userId);
            TempData.Keep("Investigation_" + userId);
            TempData.Keep("Payment_" + userId);
            TempData.Keep("PetId_" + userId);
            TempData.Keep("SMODetailsViewModel");
            TempData.Keep("PetName_" + userId);
            TempData.Keep("InvestigationID_" + userId);
            TempData.Keep("ExpertResponseCount_" + userId);
            TempData.Keep("SMOID_" + userId);
            TempData.Keep("EditSMORequestID_" + userId);
            TempData.Keep("IncompleteMedicalRecord_" + userId);
            TempData.Keep("fileName_" + userId);
            TempData.Keep("OwnerId_" + userId);
            TempData.Keep("OwnerName_" + userId);
            TempData.Keep("RequestReason_" + userId);
            TempData.Keep("MedicalHistoryComment_" + userId);
            TempData.Keep("VetComment_" + userId);
            TempData.Keep("AdditionalInformation_" + userId);
        }

        private string getUserAbsolutePath()
        {
            var uid = HttpContext.User.ToCustomPrincipal().CustomIdentity.UserId.ToString();
            var infoPath = HttpContext.User.ToCustomPrincipal().CustomIdentity.InfoPath;

            return Path.Combine(WebConfigHelper.UserFilesPath, infoPath, uid);
        }

        #endregion
    }
}
