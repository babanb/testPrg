﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADOPets.Web.Common;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.ViewModels.Econsultation;
using ADOPets.Web.ViewModels.Account;
using Model;
using System.Web.Security;
using ADOPets.Web.Common.Payment.Model;
using System.IO;
using Model.Tools;
using System.Threading.Tasks;
using System.Net;
using System.Globalization;

namespace ADOPets.Web.Controllers
{
    [AllowAnonymous]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianLight, UserTypeEnum.Assistant, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianExpert)]
    public class EconsultationController : BaseController
    {
        int userId = System.Web.HttpContext.Current.User.GetUserId();
        // GET: /Econsultation/
        public ActionResult Index()
        {
            if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
            {
                var dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.VetId == userId && !s.IsDeleted, null, s => s.Pet, s => s.User)
                    .Select(p => new ExpertView(p)).OrderByDescending(s => s.date);
                var smos = dbECon.ToList();
                if (Roles.IsUserInRole(UserTypeEnum.Admin.ToString()))
                {
                    dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.ID != null && !s.IsDeleted, null, s => s.Pet, s => s.User)
                        .Select(p => new ExpertView(p)).OrderByDescending(s => s.date);
                    smos = dbECon.ToList();
                }
                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()))
                {
                    dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.VetId == userId || s.CreatedBy == userId, null, s => s.Pet, s => s.User)
                        .Where(s => !s.IsDeleted).Select(p => new ExpertView(p)).OrderByDescending(s => s.date);
                    smos = dbECon.ToList();
                }
                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()))
                {
                    dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.CreatedBy == userId, null, s => s.Pet, s => s.User).Where(s => !s.IsDeleted)
                        .Select(p => new ExpertView(p)).OrderByDescending(s => s.date);
                    smos = dbECon.ToList();
                }

                ViewBag.UserType = UserTypeEnum.Admin;
                TempData["UserType"] = "1";
                ViewBag.UserId = userId;
                ViewBag.TimeZoneId = System.Web.HttpContext.Current.User.GetTimeZoneInfoId();
                return View("ExpertView", dbECon);
            }
            else
            {
                // var econsultation = UnitOfWork.EConsultationRepository.GetAll(e => e.UserId == userId);
                var usersetupinfo = Session["UserProfile"] as AddSetupViewModel;
                ViewBag.UserType = UserTypeEnum.OwnerAdmin;
                TempData["UserType"] = "8";
                ViewBag.UserId = userId;
                var dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.UserId == userId && !s.IsDeleted, null, s => s.Pet, s => s.User).Select(p => new IndexViewModel(p)).OrderByDescending(s => s.date);
                var smos = dbECon.ToList();

                return View(dbECon);
            }
        }

        [HttpGet]
        public ActionResult ExpertViewDetails(int EconsultID, bool IsRead = false)
        {
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(c => c.ID == EconsultID);
            if (IsRead)
            {
                Econsult.IsRead = false;
                UnitOfWork.EConsultationRepository.Update(Econsult);
                UnitOfWork.Save();
            }
            var Pets = UnitOfWork.PetRepository.GetSingle(c => c.Id == Econsult.PetId);
            var vetinfo = UnitOfWork.UserRepository.GetSingle(s => s.Id == Econsult.VetId);
            Econsult.Pet = Pets;
            var vetSpecility = UnitOfWork.VeterinarianRepository.GetSingle(s => s.UserId == Econsult.VetId);
            Econsult.User = vetinfo;
            Econsult.User.Veterinarian = vetSpecility;
            TempData["EconsultStatusId"] = Convert.ToInt16(Econsult.EconsultationStatusId);
            TempData["UserType"] = "1";
            ExpertViewDetials vetExpert = new ExpertViewDetials(Econsult);

            vetExpert.VetId = vetinfo.Id;
            vetExpert.VetFirstName = vetinfo.FirstName;
            vetExpert.VetLastName = vetinfo.LastName;
            vetExpert.VetEmail = vetinfo.Email;
            if (vetSpecility != null)
            {
                vetExpert.VetSpecility = vetSpecility.VetSpecialtyID;
            }
            else
            {
                vetExpert.VetSpecility = VetSpecialityEnum.Surgery;
            }

            ViewBag.EconsulRequestStatus = Econsult.EconsultationStatusId;
            return View(vetExpert);
        }

        [HttpGet]
        public ActionResult saveTempData(string Diagnoses, string Treatment)
        {
            TempData["Diagnoses_" + userId] = Diagnoses;
            TempData["Treatment_" + userId] = Treatment;
            KeepTempData();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult VideoConference(int EcId, int UserType)
        {
            ViewBag.UserId = userId;
            ViewBag.UserType = UserType;
            TempData["ECID"] = EcId;
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == EcId);
            var ECRooms = UnitOfWork.EConsultationRoomRepository.GetSingle(s => s.EConsultationId == EcId);
            var ECMsg = UnitOfWork.EConsultationMsgRepository.GetAll(s => s.EConsultationId == EcId);
            var Users = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
            var DocList = UnitOfWork.EconsultDocumentRepository.GetAll(s => s.EcId == EcId);
            List<EconsultDocumentViewModel> lstDoc = DocList.Select(p => new EconsultDocumentViewModel(p)).ToList();

            TempData["PetId_" + userId] = Econsult.PetId.Value;
            VideoConferenceModel VideoConf = new VideoConferenceModel(Econsult);
            var Fullname = string.Empty;
            if (userId == Econsult.VetId.Value)
            {
                Fullname = "Dr." + Users.FirstName.Value + " " + Users.LastName.Value;
            }
            else
            {
                Fullname = Users.FirstName.Value + " " + Users.LastName.Value;
            }
            VideoConf.UserName = Fullname.ToString();
            VideoConf.RoomID = ECRooms.ID;
            VideoConf.VetId = Econsult.VetId.Value;
            VideoConf.PetOnwerId = Econsult.UserId.Value;
            VideoConf.EcID = Econsult.ID;
            VideoConf.ChatMsg = ECMsg.ToList();
            VideoConf.Files = lstDoc;
            TempData["RoomID"] = ECRooms.ID;
            TempData["ChatMsgCount"] = ECMsg.ToList().Count;
            ViewBag.ChatMsgCount = ECMsg.ToList().Count;
            return PartialView("VideoConference", VideoConf);
        }

        [HttpGet]
        public ActionResult CheckNewMessage(int EcId, string MsgCount)
        {
            var Econsult = UnitOfWork.EConsultationMsgRepository.GetAll(s => s.EConsultationId == EcId).ToList();
            ViewBag.ChatMsgCount = Econsult.Count;
            if (MsgCount != Econsult.Count.ToString())
            {
                return Json(new { success = true, Mcount = Econsult.Count }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public ActionResult AddNewMessage(string EcId, string MsgSender, string MsgContent, string MsgType, string RoomID, string remoteIP)
        {
            var ClientAddress = HttpContext.Request.UserHostAddress;
            var FileName = string.Empty;

            EconsultationMsg AddNewMsg = new EconsultationMsg();
            AddNewMsg.EConsultationId = Convert.ToInt16(EcId);
            AddNewMsg.MsgSender = MsgSender;
            AddNewMsg.MsgContent = MsgContent;
            AddNewMsg.EconsultationRoomId = Convert.ToInt16(RoomID);
            AddNewMsg.MsgIP = ClientAddress;
            AddNewMsg.MsgType = Convert.ToInt16(MsgType);
            AddNewMsg.DateTimeGMT = DateTime.Now.ToString();
            AddNewMsg.TimeD = DateTime.Now.ToShortTimeString();
            AddNewMsg.TimeE = DateTime.Now.ToShortTimeString();
            AddNewMsg.MsgDateTime = DateTime.Now;
            UnitOfWork.EConsultationMsgRepository.Insert(AddNewMsg);
            UnitOfWork.Save();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult SaveUploadFile(VideoConferenceModel model)
        {
            if (model != null)
            {
                string userAbsoluteInfoPath = String.Empty;
                string userAbsoluteInfoPath1 = String.Empty;
                if (model.VetId != userId)
                {
                    var user = UnitOfWork.UserRepository.GetSingle(a => a.Id == model.VetId);
                    userAbsoluteInfoPath = Path.Combine(WebConfigHelper.UserFilesPath, user.InfoPath, user.Id.ToString());
                    userAbsoluteInfoPath1 = getUserAbsolutePath();
                }
                else
                {
                    userAbsoluteInfoPath = getUserAbsolutePath();
                }


                if (model.Files != null)
                {
                    foreach (EconsultDocumentViewModel doc in model.Files)
                    {
                        if (!string.IsNullOrEmpty(doc.DocumentName))
                        {
                            var directoryPath = string.Empty;
                            var path = string.Empty;
                            string savepath = string.Empty;
                            var directoryPath1 = string.Empty;
                            var path1 = string.Empty;
                            string savepath1 = string.Empty;
                            if (userAbsoluteInfoPath1 != string.Empty)
                            {
                                directoryPath = Path.Combine(userAbsoluteInfoPath1, Constants.PetsFolderName, model.PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, model.EcID.ToString(), Constants.DocumentFolderName);
                                path = Path.Combine(directoryPath, doc.DocumentName);
                                savepath = Path.Combine(Constants.DocumentFolderName, doc.DocumentName);
                                directoryPath1 = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, model.PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, model.EcID.ToString(), Constants.DocumentFolderName);
                                path1 = Path.Combine(directoryPath1, doc.DocumentName);
                                savepath1 = Path.Combine(Constants.DocumentFolderName, doc.DocumentName);
                                if (System.IO.File.Exists(path))
                                {
                                    System.IO.File.Copy(path, path1, true);
                                    path = path1;
                                    savepath = savepath1;
                                }
                            }
                            else
                            {
                                directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, model.PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, model.EcID.ToString(), Constants.DocumentFolderName);
                                path = Path.Combine(directoryPath, doc.DocumentName);
                                savepath = Path.Combine(Constants.DocumentFolderName, doc.DocumentName);
                            }

                            var exist = System.IO.File.Exists(path);
                            if (exist)
                            {
                                EconsultDocument Savedoc = new EconsultDocument();
                                Savedoc.EcId = model.EcID.Value;
                                Savedoc.DocumentName = model.DocName;
                                Savedoc.DocumentPath = savepath;
                                Savedoc.ServiceDate = DateTime.Now.Date;
                                Savedoc.UploadDate = DateTime.Now.Date;
                                Savedoc.IsDeleted = false;
                                Savedoc.DocumentSubTypeId = DocumentSubTypeEnum.Prescription;
                                UnitOfWork.EconsultDocumentRepository.Insert(Savedoc);
                                UnitOfWork.Save();
                            }
                        }
                    }
                }
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult UploadFile(HttpPostedFileBase myFile)
        {
            int EcID = Convert.ToInt16(TempData["ECID"]);
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == EcID);
            var VetID = Econsult.VetId.Value;
            var PetId = Econsult.PetId.Value;
            bool isUploaded = false;
            string message = "File upload failed";

            string userAbsoluteInfoPath = String.Empty;

            if (VetID != userId)
            {
                var user = UnitOfWork.UserRepository.GetSingle(a => a.Id == VetID);
                userAbsoluteInfoPath = Path.Combine(WebConfigHelper.UserFilesPath, user.InfoPath, user.Id.ToString());
            }
            else
            {
                userAbsoluteInfoPath = getUserAbsolutePath();
            }

            if (myFile != null && myFile.ContentLength != 0)
            {
                var directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, EcID.ToString(), Constants.DocumentFolderName);
                var path = Path.Combine(directoryPath, myFile.FileName);
                string savepath = Path.Combine(Constants.DocumentFolderName, myFile.FileName);

                try
                {
                    myFile.SaveAs(savepath);
                    isUploaded = true;
                    message = "File uploaded successfully!";
                }
                catch (Exception ex)
                {
                    message = string.Format("File upload failed: {0}", ex.Message);
                }

            }
            return Json(new { isUploaded = isUploaded, message = message }, "text/html");
        }

        [HttpGet]
        public ActionResult GetUserImage(string fromUserId)
        {
            int userId = Convert.ToInt32(fromUserId);
            var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
            if (user == null || user.ProfileImage == null)
            {
                return File("~/Content/Images/ownerProfilepic.jpg", "image/jpg");
            }
            return File(user.ProfileImage, "image/jpg");
        }

        [HttpPost]
        public ActionResult SaveSubmitReport(DocumentUpload model, List<HttpPostedFileBase> fileUpload)
        {
            ViewBag.UserId = userId;
            int EcId = Convert.ToInt16(TempData["ECID"]);
            int PetId = (int)TempData["PetId_" + userId];
            int ownerId = 0;
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == EcId);
            ownerId = Econsult.UserId.Value;
            var Pets = UnitOfWork.PetRepository.GetFirst(s => s.Id == Econsult.PetId.Value);
            string PetDOB = Pets.BirthDate.ToString();
            var veterian = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);

            if (!ModelState.IsValid)
            {
                model.ActionBeginDate = Econsult.ActionDateTimeBegin.Value;
                model.ActionEndDate = Econsult.ActionDateTimeEnd.Value;
                model.Durations = Econsult.Periods.Value;
                model.PetId = PetId;
                model.EcId = EcId;
                model.VetId = Econsult.VetId.Value;
                TempData["ECID"] = EcId;
                TempData["PetId_" + userId] = PetId;
                KeepTempData();
                return View("_SubmitReport", model);
            }

            string userAbsoluteInfoPath = String.Empty;

            userAbsoluteInfoPath = getUserAbsolutePath();

            var EcSummary = UnitOfWork.EConsultationSummaryRepository.GetSingle(m => m.EConsultationId == EcId);
            if (EcSummary == null)
            {
                if (model.lstAttachment != null)
                {
                    foreach (EconsultDocumentViewModel doc in model.lstAttachment)
                    {
                        if (!string.IsNullOrEmpty(doc.DocumentName))
                        {
                            var directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, model.EcId.ToString(), Constants.DocumentFolderName);
                            var path = Path.Combine(directoryPath, doc.DocumentName);
                            string savepath = Path.Combine(Constants.DocumentFolderName, doc.DocumentName);
                            var exist = System.IO.File.Exists(path);
                            if (exist)
                            {
                                EconsultDocument Savedoc = new EconsultDocument();
                                Savedoc.EcId = EcId;
                                Savedoc.DocumentName = doc.DocumentName;
                                Savedoc.DocumentPath = savepath;
                                Savedoc.ServiceDate = DateTime.Now.Date;
                                Savedoc.UploadDate = DateTime.Now.Date;
                                Savedoc.IsDeleted = false;
                                Savedoc.DocumentSubTypeId = DocumentSubTypeEnum.Prescription;
                                UnitOfWork.EconsultDocumentRepository.Insert(Savedoc);
                                UnitOfWork.Save();
                            }
                        }
                    }
                }

                EconsultationSummary EconsultSubmit = new EconsultationSummary();
                //Save into EconsultationSummary
                EconsultSubmit.EConsultationId = EcId;
                EconsultSubmit.VetId = userId;
                EconsultSubmit.UserId = Econsult.UserId.Value;
                EconsultSubmit.DateSummary = DateTime.Now;
                if (model.SaveClick == 0)
                {
                    EconsultSubmit.EconsultationStatusId = Convert.ToInt16(EConsultationStatusEnum.Closed);
                }
                else
                {
                    EconsultSubmit.EconsultationStatusId = Convert.ToInt16(EConsultationStatusEnum.Complete);
                }
                EconsultSubmit.PetName = Pets.Name.Value;
                EconsultSubmit.PetDOB = Pets.BirthDate.Date.ToShortDateString();
                EconsultSubmit.EconsultationDateTime = Econsult.ActionDateTimeBegin.Value.ToShortDateString();
                EconsultSubmit.EconsultationDuration = Econsult.Periods.Value;
                EconsultSubmit.VeterianFirstName = veterian.FirstName.Value;
                EconsultSubmit.VeterianMiddleName = veterian.MiddleName.Value;
                EconsultSubmit.VeterianLastName = veterian.LastName.Value;
                EconsultSubmit.Diagnoses = model.Diagnosis;
                EconsultSubmit.Treatment = model.Treatment;
                UnitOfWork.EConsultationSummaryRepository.Insert(EconsultSubmit);
                UnitOfWork.Save();

                //Update Econsultation table
                var EconsultUpdate = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == EcId);
                EconsultUpdate.EconsultationStatusId = EConsultationStatusEnum.Complete;
                UnitOfWork.EConsultationRepository.Update(EconsultUpdate);
                UnitOfWork.Save();

            }
            else
            {
                //Update Econsultation Summary
                if (EcSummary != null)
                {
                    if (model.lstAttachment != null)
                    {
                        foreach (EconsultDocumentViewModel doc in model.lstAttachment)
                        {
                            if (!string.IsNullOrEmpty(doc.DocumentName))
                            {
                                var directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, model.EcId.ToString(), Constants.DocumentFolderName);
                                var path = Path.Combine(directoryPath, doc.DocumentName);
                                string savepath = Path.Combine(Constants.DocumentFolderName, doc.DocumentName);
                                var exist = System.IO.File.Exists(path);
                                if (exist)
                                {
                                    EconsultDocument Savedoc = new EconsultDocument();
                                    Savedoc.EcId = EcId;
                                    Savedoc.DocumentName = doc.DocumentName;
                                    Savedoc.DocumentPath = savepath;
                                    Savedoc.ServiceDate = DateTime.Now.Date;
                                    Savedoc.UploadDate = DateTime.Now.Date;
                                    Savedoc.IsDeleted = false;
                                    Savedoc.DocumentSubTypeId = DocumentSubTypeEnum.Prescription;
                                    UnitOfWork.EconsultDocumentRepository.Insert(Savedoc);
                                    UnitOfWork.Save();
                                }
                            }
                        }
                    }

                    EconsultationSummary EconsultSubmit = new EconsultationSummary();

                    EcSummary.Diagnoses = model.Diagnosis;
                    EcSummary.Treatment = model.Treatment;
                    UnitOfWork.EConsultationSummaryRepository.Update(EcSummary);
                    UnitOfWork.Save();

                    //Update Econsultation table
                    var EconsultUpdate = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == EcId);
                    EconsultUpdate.EconsultationStatusId = EConsultationStatusEnum.Complete;
                    UnitOfWork.EConsultationRepository.Update(EconsultUpdate);
                    UnitOfWork.Save();

                }
            }
            KeepTempData();
            return Json(new { success = true, successMessage = ADOPets.Web.Resources.Wording.Econsultation_SubmitReport_SaveMessage }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SubmitReport(int EconsultID)
        {
            ViewBag.UserId = userId;
            TempData["ECID"] = EconsultID;
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == EconsultID);
            TempData["PetId_" + userId] = Econsult.PetId.Value;
            SubmitReportModel submitReport = new SubmitReportModel(Econsult);
            var DocList = UnitOfWork.EconsultDocumentRepository.GetAll(s => s.EcId == EconsultID);
            List<EconsultDocumentViewModel> lstDoc = DocList.Select(p => new EconsultDocumentViewModel(p)).ToList();
            DocumentUpload EcDocument = new DocumentUpload(Econsult);
            EcDocument.PetId = Econsult.PetId.Value;
            EcDocument.PetOnwerId = Econsult.UserId.Value;
            EcDocument.VetId = Econsult.VetId.Value;
            if (Econsult.ActionDateTimeBegin == null)
            {
                var VetUser = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);

                EcDocument.ActionBeginDate = Econsult.ActionDateTimeBegin.Value;
                EcDocument.ActionEndDate = Econsult.ActionDateTimeEnd.Value;
                EcDocument.Durations = Econsult.Periods.Value;
            }

            var EcSummary = UnitOfWork.EConsultationSummaryRepository.GetSingle(m => m.EConsultationId == EconsultID);
            if (EcSummary != null)
            {
                EcDocument.Diagnosis = EcSummary.Diagnoses;
                EcDocument.Treatment = EcSummary.Treatment;
                EcDocument.ConsultationDate = (EcSummary.DateSummary.Value != null) ? EcSummary.DateSummary.Value : DateTime.Now.Date;
                EcDocument.lstAttachment = lstDoc;
                EcDocument.FlagSave = 1;
                EcDocument.SaveClick = 0;
            }

            return PartialView("_SubmitReport", EcDocument);
        }

        [HttpPost]
        public ActionResult SubmitReport(DocumentUpload model, List<HttpPostedFileBase> fileUpload)
        {
            ViewBag.UserId = userId;
            int EcId = Convert.ToInt16(TempData["ECID"]);
            int PetId = (int)TempData["PetId_" + userId];
            int ownerId = 0;
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == EcId);
            ownerId = Econsult.UserId.Value;
            var Pets = UnitOfWork.PetRepository.GetFirst(s => s.Id == Econsult.PetId.Value);
            string PetDOB = Pets.BirthDate.ToString();
            var veterian = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);

            if (!ModelState.IsValid)
            {
                model.ActionBeginDate = Econsult.ActionDateTimeBegin.Value;
                model.ActionEndDate = Econsult.ActionDateTimeEnd.Value;
                model.Durations = Econsult.Periods.Value;
                model.PetId = PetId;
                model.EcId = EcId;
                model.VetId = Econsult.VetId.Value;
                TempData["ECID"] = EcId;
                TempData["PetId_" + userId] = PetId;
                KeepTempData();
                return View("_SubmitReport", model);
            }

            string userAbsoluteInfoPath = String.Empty;
            int VetID = Econsult.VetId.Value;
            if (VetID != userId)
            {
                var user = UnitOfWork.UserRepository.GetSingle(a => a.Id == VetID);
                userAbsoluteInfoPath = Path.Combine(WebConfigHelper.UserFilesPath, user.InfoPath, user.Id.ToString());
            }
            else
            {
                userAbsoluteInfoPath = getUserAbsolutePath();
            }

            var EcSummary = UnitOfWork.EConsultationSummaryRepository.GetSingle(m => m.EConsultationId == EcId);
            if (EcSummary == null)
            {

                if (model.lstAttachment != null)
                {
                    foreach (EconsultDocumentViewModel doc in model.lstAttachment)
                    {
                        if (!string.IsNullOrEmpty(doc.DocumentName))
                        {
                            var directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, model.EcId.ToString(), Constants.DocumentFolderName);
                            var path = Path.Combine(directoryPath, doc.DocumentName);
                            string savepath = Path.Combine(Constants.DocumentFolderName, doc.DocumentName);
                            var exist = System.IO.File.Exists(path);
                            if (exist)
                            {
                                EconsultDocument Savedoc = new EconsultDocument();
                                Savedoc.EcId = EcId;
                                Savedoc.DocumentName = doc.DocumentName;
                                Savedoc.DocumentPath = savepath;
                                Savedoc.ServiceDate = DateTime.Now.Date;
                                Savedoc.UploadDate = DateTime.Now.Date;
                                Savedoc.IsDeleted = false;
                                Savedoc.DocumentSubTypeId = DocumentSubTypeEnum.Prescription;
                                UnitOfWork.EconsultDocumentRepository.Insert(Savedoc);
                                UnitOfWork.Save();
                            }
                        }
                    }
                }

                EconsultationSummary EconsultSubmit = new EconsultationSummary();

                string result = GeneratePDF(EcId, model.Diagnosis, model.Treatment, veterian.FirstName.Value, veterian.LastName.Value);
                EconsultSubmit.FilesFolder = Path.Combine(Constants.ECReportFolderName, TempData["fileName_" + userId].ToString());
                EconsultSubmit.FilesName = TempData["fileName_" + userId].ToString();

                //Save into EconsultationSummary
                EconsultSubmit.EConsultationId = EcId;
                EconsultSubmit.VetId = userId;
                EconsultSubmit.UserId = Econsult.UserId.Value;
                EconsultSubmit.DateSummary = DateTime.Now;
                EconsultSubmit.EconsultationStatusId = Convert.ToInt16(EConsultationStatusEnum.Closed);
                EconsultSubmit.PetName = Pets.Name.Value;
                EconsultSubmit.PetDOB = Pets.BirthDate.Date.ToShortDateString();
                EconsultSubmit.EconsultationDateTime = Econsult.ActionDateTimeBegin.Value.ToShortDateString();
                EconsultSubmit.EconsultationDuration = Econsult.Periods.Value;
                EconsultSubmit.VeterianFirstName = veterian.FirstName.Value;
                EconsultSubmit.VeterianMiddleName = veterian.MiddleName.Value;
                EconsultSubmit.VeterianLastName = veterian.LastName.Value;
                EconsultSubmit.Diagnoses = model.Diagnosis;
                EconsultSubmit.Treatment = model.Treatment;
                UnitOfWork.EConsultationSummaryRepository.Insert(EconsultSubmit);
                UnitOfWork.Save();

                //Update Econsultation table
                var EconsultUpdate = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == EcId);

                EconsultUpdate.EconsultationStatusId = EConsultationStatusEnum.Closed;
                EconsultUpdate.IsRead = true;
                UnitOfWork.EConsultationRepository.Update(EconsultUpdate);
                UnitOfWork.Save();

                //Send mail to Pet Owner for Submit Report
                var PetOwner = UnitOfWork.UserRepository.GetSingle(s => s.Id == ownerId);
                EmailSender.SendToUserEconsultationReportReady(PetOwner.Email.Value, PetOwner.FirstName.Value, PetOwner.LastName.Value, veterian.Email.Value, Pets.Name.Value, Pets.PetTypeId.ToString(),
                   DateTime.Now, DateTime.Now, EcId, EConsultationStatusEnum.Closed.ToString());

            }
            else
            {
                if (EcSummary != null)
                {
                    if (model.lstAttachment != null)
                    {
                        foreach (EconsultDocumentViewModel doc in model.lstAttachment)
                        {
                            if (!string.IsNullOrEmpty(doc.DocumentName))
                            {
                                var directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, model.EcId.ToString(), Constants.DocumentFolderName);
                                var path = Path.Combine(directoryPath, doc.DocumentName);
                                string savepath = Path.Combine(Constants.DocumentFolderName, doc.DocumentName);
                                var exist = System.IO.File.Exists(path);
                                if (exist)
                                {
                                    EconsultDocument Savedoc = new EconsultDocument();
                                    Savedoc.EcId = EcId;
                                    Savedoc.DocumentName = doc.DocumentName;
                                    Savedoc.DocumentPath = savepath;
                                    Savedoc.ServiceDate = DateTime.Now.Date;
                                    Savedoc.UploadDate = DateTime.Now.Date;
                                    Savedoc.IsDeleted = false;
                                    Savedoc.DocumentSubTypeId = DocumentSubTypeEnum.Prescription;
                                    UnitOfWork.EconsultDocumentRepository.Insert(Savedoc);
                                    UnitOfWork.Save();
                                }
                            }
                        }
                    }

                    EconsultationSummary EconsultSubmit = new EconsultationSummary();

                    string result = GeneratePDF(EcId, model.Diagnosis, model.Treatment, veterian.FirstName.Value, veterian.LastName.Value);

                    EcSummary.FilesFolder = Path.Combine(Constants.ECReportFolderName, TempData["fileName_" + userId].ToString());
                    EcSummary.FilesName = TempData["fileName_" + userId].ToString();

                    EcSummary.EconsultationStatusId = Convert.ToInt16(EConsultationStatusEnum.Closed);
                    EcSummary.Diagnoses = model.Diagnosis;
                    EcSummary.Treatment = model.Treatment;
                    UnitOfWork.EConsultationSummaryRepository.Update(EcSummary);
                    UnitOfWork.Save();

                    //Update Econsultation table
                    var EconsultUpdate = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == EcId);
                    EconsultUpdate.EconsultationStatusId = EConsultationStatusEnum.Closed;
                    EconsultUpdate.IsRead = true;
                    UnitOfWork.EConsultationRepository.Update(EconsultUpdate);
                    UnitOfWork.Save();

                    //Send mail to Pet Owner for Submit Report
                    var PetOwner = UnitOfWork.UserRepository.GetSingle(s => s.Id == ownerId);
                    EmailSender.SendToUserEconsultationReportReady(PetOwner.Email.Value, PetOwner.FirstName.Value, PetOwner.LastName.Value, veterian.Email.Value, Pets.Name.Value, Pets.PetTypeId.ToString(),
                      DateTime.Now, DateTime.Now, EcId, EConsultationStatusEnum.Closed.ToString());

                }
            }

            KeepTempData();
            return Json(new { success = true, successMessage = ADOPets.Web.Resources.Wording.Econsultation_SubmitReport_SaveSuccessMessage }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ExpertViewReport(int EconsultID)
        {
            ViewBag.UserId = userId;
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == EconsultID);
            TempData["PetId_" + userId] = Econsult.PetId.Value;
            TempData["EditECID_" + userId] = EconsultID;
            TempData["OwnerID_" + userId] = Econsult.UserId.Value;
            var EconsultSummry = UnitOfWork.EConsultationSummaryRepository.GetSingle(s => s.EConsultationId == EconsultID);
            //EconsultationSummary
            ExperViewReportModel viewReport = new ExperViewReportModel(EconsultSummry);
            viewReport.ActionBeginDate = (Econsult.ActionDateTimeBegin.Value != null) ? Econsult.ActionDateTimeBegin.Value : DateTime.Now;
            viewReport.ActionEndDate = (Econsult.ActionDateTimeEnd.Value != null) ? Econsult.ActionDateTimeEnd.Value : DateTime.Now;
            viewReport.ConsultationDate = EconsultSummry.DateSummary.Value;
            viewReport.PetCondition = Econsult.TitleConsultation;
            viewReport.PetId = Econsult.PetId.Value;
            var FileList = UnitOfWork.EconsultDocumentRepository.GetAll(s => s.EcId == EconsultID).ToList();
            IEnumerable<EconsultDocument> FileListView1 = FileList.ToList();
            List<EconsultDocumentViewModel> FileListView = new List<EconsultDocumentViewModel>();
            foreach (var FileDisplay in FileListView1)
            {
                EconsultDocumentViewModel FileNew = new EconsultDocumentViewModel(FileDisplay);
                FileListView.Add(FileNew);
            }
            viewReport.lstAttachment = FileListView;
            return PartialView("ExpertViewReport", viewReport);
        }

        [HttpGet]
        public FileResult GetFile(string fileName, int PetID, int EcID, string ownerId, string VetId, string imageType, string userInfoPath)
        {
            int PetId = PetID;
            int id = EcID;
            if (!string.IsNullOrEmpty(fileName))
            {
                var directoryPath = "";
                var userAbsoluteInfoPath = "";
                if (VetId != userId.ToString() && !string.IsNullOrEmpty(VetId))
                {
                    int uid = int.Parse(VetId);
                    var user = UnitOfWork.UserRepository.GetSingle(a => a.Id == uid);
                    userAbsoluteInfoPath = Path.Combine(WebConfigHelper.UserFilesPath, user.InfoPath, user.Id.ToString());
                }
                else
                {
                    userAbsoluteInfoPath = getUserAbsolutePath();
                }
                if (string.IsNullOrEmpty(imageType))
                {
                    directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, id.ToString());
                }
                else
                {
                    directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, id.ToString());
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

        [HttpGet]
        public ActionResult GetImage(int VetId, int EcId, string fileName, string imgType)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    int PetId = (int)TempData["PetId_" + userId];
                    int id = (int)userId;
                    string fileNameNew = "";
                    var directoryPath = "";

                    var userAbsoluteInfoPath = "";
                    if (VetId != id)
                    {
                        int uid = VetId;
                        var user = UnitOfWork.UserRepository.GetSingle(a => a.Id == uid);
                        userAbsoluteInfoPath = Path.Combine(WebConfigHelper.UserFilesPath, user.InfoPath, user.Id.ToString());
                    }
                    else
                    {
                        userAbsoluteInfoPath = getUserAbsolutePath();
                    }

                    if (!string.IsNullOrEmpty(imgType))
                    {
                        directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, EcId.ToString(), Constants.DocumentFolderName);
                        fileNameNew = "Thumb_" + fileName;
                    }
                    else
                    {
                        directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, EcId.ToString(), Constants.DocumentFolderName);
                        var splitExt = fileName.Split('.');
                        var ext = splitExt[1].ToUpper();
                        fileNameNew = splitExt[0] + "." + ext;
                    }
                    KeepTempData();
                    var path = Path.Combine(directoryPath, fileNameNew);
                    var exist = System.IO.File.Exists(path);
                    if (exist)
                    {
                        return new FileStreamResult(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read), "image/*");
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

        [HttpGet]
        public ActionResult PreviewEC(int Ecid)
        {
            string lblDiagnosis = (TempData["Diagnoses_" + userId] != null) ? TempData["Diagnoses_" + userId].ToString() : String.Empty;
            string lblTreatment = (TempData["Treatment_" + userId] != null) ? TempData["Treatment_" + userId].ToString() : String.Empty;

            var EcSummary = UnitOfWork.EConsultationSummaryRepository.GetSingle(p => p.EConsultationId == Ecid);
            if (lblDiagnosis == "")
                lblDiagnosis = EcSummary.Diagnoses;
            if (lblTreatment == "")
                lblTreatment = EcSummary.Treatment;
            string path = GeneratePDF(Ecid, lblDiagnosis, lblTreatment);
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

        public string GeneratePDF(int id, string Diagnosis = null, string Treatment = null, string FirstName = null, string LastName = null)
        {
            var fileName = string.Empty;
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == id);
            int PetId = (int)TempData["PetId_" + userId];
            int ownerId = Econsult.UserId.Value;
            int VetId = Econsult.VetId.Value;
            fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "EC" + id + ".pdf";
            string userAbsoluteInfoPath = String.Empty;
            if (VetId != userId)
            {
                var user = UnitOfWork.UserRepository.GetSingle(a => a.Id == VetId);
                userAbsoluteInfoPath = Path.Combine(WebConfigHelper.UserFilesPath, user.InfoPath, user.Id.ToString());
            }
            else
            {
                userAbsoluteInfoPath = getUserAbsolutePath();
            }

            var directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, id.ToString(), Constants.ECReportFolderName);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var path = Path.Combine(directoryPath, fileName);

            var Pets = UnitOfWork.PetRepository.GetFirst(s => s.Id == PetId);
            DateTime PetDOB = Pets.BirthDate;
            var veterian = UnitOfWork.UserRepository.GetSingle(s => s.Id == VetId);
            var Owner = UnitOfWork.UserRepository.GetSingle(s => s.Id == ownerId);
            SubmitReportModel model = new SubmitReportModel();
            model.EcId = id;
            if (Econsult.ActionDateTimeBegin != null)
            {
                model.ActionBeginDate = GetExpertEcTime(Econsult.ActionDateTimeBegin.Value, Convert.ToInt16(Econsult.VetTimezoneID), VetId);
                model.ActionEndDate = GetExpertEcTime(Econsult.ActionDateTimeEnd.Value, Convert.ToInt16(Econsult.VetTimezoneID), VetId);
                model.Durations = Econsult.Periods;
            }
            else
            {
                model.ActionBeginDate = DateTime.Now;
                model.ActionEndDate = DateTime.Now;
                model.Durations = 0;
            }
            model.Diagnosis = Diagnosis;
            model.Treatment = Treatment;
            model.EconsultationStatus = EConsultationStatusEnum.Closed;
            model.PetCondition = Econsult.TitleConsultation;
            model.PetDOB = PetDOB;
            model.PetName = Pets.Name.Value;
            if (Pets.BreedType != null)
            {
                model.PetBreadType = Pets.BreedType.Name.ToString();
            }
            else
            {
                model.PetBreadType = "";
            }
            model.PetOnwerId = Econsult.UserId.Value;
            model.VetFirstName = veterian.FirstName.Value;
            model.VetLastName = veterian.LastName.Value;
            model.OwnerFirstName = Owner.FirstName.Value.ToString();
            model.OwnerLastName = Owner.LastName.Value.ToString();
            model.OwnerEmail = Owner.Email.Value.ToString();
            model.PetType = (PetTypeEnum)Pets.PetTypeId;
            ECPDFHelper.GeneratePDF(path, model);
            TempData["fileName_" + userId] = fileName;
            KeepTempData();
            return path;
        }

        public FileResult GetReport(int EcId)
        {
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == EcId);
            int ownerId = Econsult.UserId.Value;
            int VetId = Econsult.VetId.Value;
            int PetId = Econsult.PetId.Value;
            var EcSummary = UnitOfWork.EConsultationSummaryRepository.GetSingle(s => s.EConsultationId == EcId);
            string fileName = EcSummary.FilesName;
            string userAbsoluteInfoPath = String.Empty;
            if (VetId != userId)
            {
                var user = UnitOfWork.UserRepository.GetSingle(a => a.Id == VetId);
                userAbsoluteInfoPath = Path.Combine(WebConfigHelper.UserFilesPath, user.InfoPath, user.Id.ToString());
            }
            else
            {
                userAbsoluteInfoPath = getUserAbsolutePath();
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                var directoryPath = "";
                directoryPath = Path.Combine(userAbsoluteInfoPath, Constants.PetsFolderName, PetId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, EcId.ToString(), Constants.ECReportFolderName);

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
        public ActionResult ViewDetails(int EconsultID, bool IsRead = false)
        {
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(c => c.ID == EconsultID);
            if (IsRead)
            {
                Econsult.IsOwnerRead = true;
                Econsult.IsRead = false;
                UnitOfWork.EConsultationRepository.Update(Econsult);
                UnitOfWork.Save();
            }
            var Pets = UnitOfWork.PetRepository.GetSingle(c => c.Id == Econsult.PetId);
            var vetinfo = UnitOfWork.UserRepository.GetSingle(s => s.Id == Econsult.VetId);
            Econsult.Pet = Pets;
            var vetSpecility = UnitOfWork.VeterinarianRepository.GetSingle(s => s.UserId == Econsult.VetId);
            Econsult.User = vetinfo;
            Econsult.User.Veterinarian = vetSpecility;
            TempData["EconsultStatusId"] = Convert.ToInt16(Econsult.EconsultationStatusId);
            DateTime compareDate = DateTime.Today.Date.AddHours(24);
            int ts = Convert.ToInt16(Econsult.RDVDateTime.Value.Hour.ToString());
            DateTime RVDDate = Econsult.RDVDate.Value.Date.AddHours(ts);

            if (RVDDate >= compareDate)
            {
                TempData["StatusModified"] = "1";
            }
            else
            {
                TempData["StatusModified"] = "0";
            }
            //if (Econsult.Pet != null)
            //{
            ViewDetailsModel viewDetailsModel = new ViewDetailsModel(Econsult);

            viewDetailsModel.VetId = vetinfo.Id;
            viewDetailsModel.VetFirstName = vetinfo.FirstName;
            viewDetailsModel.VetLastName = vetinfo.LastName;
            viewDetailsModel.VetEmail = vetinfo.Email;
            if (vetSpecility != null)
            {
                viewDetailsModel.VetSpecility = vetSpecility.VetSpecialtyID;
            }
            else
            {
                viewDetailsModel.VetSpecility = VetSpecialityEnum.Surgery;
            }
            ViewBag.EconsulRequestStatus = Econsult.EconsultationStatusId;

            return View(viewDetailsModel);
            //}
            //else
            //{

            //}
        }

        [HttpGet]
        public ActionResult DeleteEC(int id)
        {
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(c => c.ID == id);

            if (Econsult != null)
            {
                var EconsultRoom = UnitOfWork.EConsultationRoomRepository.GetSingle(c => c.EConsultationId == id);

                if (EconsultRoom != null)
                {
                    UnitOfWork.EConsultationRoomRepository.Delete(EconsultRoom);
                    UnitOfWork.Save();
                }

                if (Econsult.CalenderId.HasValue)
                {
                    var calender = UnitOfWork.CalendarRepository.GetSingle(c => c.Id == Econsult.CalenderId);
                    calender.SendNotificationMail = false;
                    calender.Comment = new EncryptedText(Resources.Wording.Econsultation_Withdraw_AddSuccessfullMessage);
                    UnitOfWork.CalendarRepository.Update(calender);
                    UnitOfWork.Save();
                }

                Econsult.EconsultationStatusId = EConsultationStatusEnum.Withdraw;
                UnitOfWork.EConsultationRepository.Update(Econsult);
                UnitOfWork.Save();

                //Support Mail
                var Userinfo = UnitOfWork.UserRepository.GetSingle(s => s.Id == Econsult.UserId);

                var Userpet = UnitOfWork.PetRepository.GetSingle(p => p.Id == Econsult.PetId);

                string Useremail = Userinfo.Email.Value.ToString();
                string UserFirstName = Userinfo.FirstName.Value.ToString();
                string UserLastName = Userinfo.LastName.Value.ToString();
                string UserPetName = Userpet.Name.Value.ToString();
                string UserPetType = Userpet.PetTypeId.ToString();
                int ECID = id;
                string PetCondition = Econsult.TitleConsultation.ToString();
                string UserID = Econsult.UserId.ToString();
                DateTime ConsultationDate = Convert.ToDateTime(Econsult.RDVDate.Value.AddHours(Econsult.RDVDateTime.Value.Hour));
                EmailSender.SendSupportUserEconsultWithdraw(ECID, UserPetType, UserPetType, PetCondition, ConsultationDate, UserFirstName,
                    UserLastName, Useremail, userId);

                Success(Resources.Wording.Econsultation_Withdraw_AddSuccessfullMessage);
            }
            var econsultation = UnitOfWork.EConsultationRepository.GetAll(e => e.UserId == userId);
            var usersetupinfo = Session["UserProfile"] as AddSetupViewModel;
            var dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.UserId == userId && !s.IsDeleted, null, s => s.Pet, s => s.User).Select(p => new IndexViewModel(p));
            var smos = dbECon.ToList();

            return View("Index", dbECon);
        }

        [HttpGet]
        public ActionResult WithdrawConfirm(int ecId)
        {
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(c => c.ID == ecId);
            WithdrawEC ECWithdrwaObj = new WithdrawEC(Econsult);

            var Pets = UnitOfWork.PetRepository.GetSingle(s => s.Id == Econsult.PetId);

            ECWithdrwaObj.PetName = Pets.Name.Value.ToString();

            return PartialView("_WithdrawConfirm", ECWithdrwaObj);
        }

        [HttpPost]
        public ActionResult WithdrawConfirm(WithdrawEC model)
        {
            if (ModelState.IsValid)
            {
                var EconsultRoom = UnitOfWork.EConsultationRoomRepository.GetSingle(c => c.EConsultationId == model.Id);
                if (EconsultRoom != null)
                {
                    UnitOfWork.EConsultationRoomRepository.Delete(EconsultRoom);
                    UnitOfWork.Save();
                }

                if (model.CalenderId.HasValue)
                {
                    var EcCalender = UnitOfWork.CalendarRepository.GetSingle(c => c.Id == model.CalenderId);
                    UnitOfWork.CalendarRepository.Delete(EcCalender);
                    UnitOfWork.Save();
                }

                var Econsult = UnitOfWork.EConsultationRepository.GetSingle(c => c.ID == model.Id);
                Econsult.EconsultationStatusId = EConsultationStatusEnum.Withdraw;
                UnitOfWork.EConsultationRepository.Update(Econsult);
                UnitOfWork.Save();

                //Support Mail
                var Userinfo = UnitOfWork.UserRepository.GetSingle(s => s.Id == Econsult.UserId);

                var Userpet = UnitOfWork.PetRepository.GetSingle(p => p.Id == Econsult.PetId);

                string Useremail = Userinfo.Email.Value.ToString();
                string UserFirstName = Userinfo.FirstName.Value.ToString();
                string UserLastName = Userinfo.LastName.Value.ToString();
                string UserPetName = Userpet.Name.Value.ToString();
                string UserPetType = Userpet.PetTypeId.ToString();
                int ECID = model.Id;
                string PetCondition = Econsult.TitleConsultation.ToString();
                string UserID = Econsult.UserId.ToString();
                DateTime ConsultationDate = Convert.ToDateTime(Econsult.RDVDate.Value.AddHours(Econsult.RDVDateTime.Value.Hour));
                EmailSender.SendSupportUserEconsultWithdraw(ECID, UserPetType, UserPetType, PetCondition, ConsultationDate, UserFirstName,
                    UserLastName, Useremail, userId);

                var econsultation = UnitOfWork.EConsultationRepository.GetAll(e => e.UserId == userId);
                var usersetupinfo = Session["UserProfile"] as AddSetupViewModel;
                var dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.UserId == userId, null, s => s.Pet, s => s.User).Select(p => new IndexViewModel(p));
                var smos = dbECon.ToList();

                return View("Index", dbECon);
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult StartEConsultation(int ecId, int userId)
        {
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(c => c.ID == ecId);

            if (Econsult != null)
            {
                Econsult.EconsultationStatusId = EConsultationStatusEnum.InProgress;
                UnitOfWork.EConsultationRepository.Update(Econsult);
                UnitOfWork.Save();
            }

            var ECRoom = UnitOfWork.EConsultationRoomRepository.GetSingle(c => c.EConsultationId == ecId);
            var Pets = UnitOfWork.PetRepository.GetSingle(c => c.Id == Econsult.PetId);
            var vetinfo = UnitOfWork.UserRepository.GetSingle(s => s.Id == Econsult.VetId);
            Econsult.Pet = Pets;
            var vetSpecility = UnitOfWork.VeterinarianRepository.GetSingle(s => s.UserId == Econsult.VetId);
            Econsult.User = vetinfo;
            Econsult.User.Veterinarian = vetSpecility;
            TempData["EconsultStatusId"] = Convert.ToInt16(Econsult.EconsultationStatusId);
            StartEConsultationModel viewDetailsModel = new StartEConsultationModel(Econsult);

            viewDetailsModel.VetId = vetinfo.Id;
            viewDetailsModel.VetFirstName = vetinfo.FirstName;
            viewDetailsModel.VetLastName = vetinfo.LastName;
            viewDetailsModel.VetEmail = vetinfo.Email;
            if (vetSpecility != null)
            {
                viewDetailsModel.VetSpecility = vetSpecility.VetSpecialtyID;
            }
            else
            {
                viewDetailsModel.VetSpecility = VetSpecialityEnum.Surgery;
            }
            ViewBag.EconsulRequestStatus = Econsult.EconsultationStatusId;
            viewDetailsModel.RoomId = ECRoom.ID;

            return View(viewDetailsModel);
        }

        [HttpGet]
        public ActionResult JoinEConsultation(int ecId, int userId)
        {
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(c => c.ID == ecId);
            var ECRoom = UnitOfWork.EConsultationRoomRepository.GetSingle(c => c.EConsultationId == ecId);

            var Pets = UnitOfWork.PetRepository.GetSingle(c => c.Id == Econsult.PetId);
            var vetinfo = UnitOfWork.UserRepository.GetSingle(s => s.Id == Econsult.VetId);
            Econsult.Pet = Pets;
            var vetSpecility = UnitOfWork.VeterinarianRepository.GetSingle(s => s.UserId == Econsult.VetId);
            Econsult.User = vetinfo;
            Econsult.User.Veterinarian = vetSpecility;
            TempData["EconsultStatusId"] = Convert.ToInt16(Econsult.EconsultationStatusId);
            JoinEConsultationModel viewDetailsModel = new JoinEConsultationModel(Econsult);

            viewDetailsModel.VetId = vetinfo.Id;
            viewDetailsModel.VetFirstName = vetinfo.FirstName;
            viewDetailsModel.VetLastName = vetinfo.LastName;
            viewDetailsModel.VetEmail = vetinfo.Email;
            if (vetSpecility != null)
            {
                viewDetailsModel.VetSpecility = vetSpecility.VetSpecialtyID;
            }
            else
            {
                viewDetailsModel.VetSpecility = VetSpecialityEnum.Surgery;
            }
            ViewBag.EconsulRequestStatus = Econsult.EconsultationStatusId;
            viewDetailsModel.RoomId = ECRoom.ID;
            return View(viewDetailsModel);

        }

        [HttpGet]
        public ActionResult EditDetails(int ecId)
        {
            var Econsult = UnitOfWork.EConsultationRepository.GetSingle(c => c.ID == ecId);

            return PartialView("_EditDetails", new EditDetailsModel(Econsult));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public JsonResult EditDetails(EditDetailsModel model)
        public ActionResult EditDetails(EditDetailsModel model)
        {
            if (ModelState.IsValid)
            {
                var checkEconsult = UnitOfWork.EConsultationRepository.GetSingle(c => c.ID == model.Id);
                model.CalenderId = checkEconsult.CalenderId.Value;
                model.ECTimeZone = checkEconsult.VetTimezoneID.Value;
                var EconsultRoom = UnitOfWork.EConsultationRoomRepository.GetSingle(c => c.EConsultationId == model.Id);
                if (EconsultRoom != null)
                {
                    model.MapEcRoom(EconsultRoom);
                    UnitOfWork.EConsultationRoomRepository.Update(EconsultRoom);
                    UnitOfWork.Save();
                }

                if (model.CalenderId.HasValue)
                {
                    var EcCalender = UnitOfWork.CalendarRepository.GetSingle(c => c.Id == model.CalenderId);
                    DateTime OldEcDate = model.Date.Value.AddHours(model.Time.Value.Hour).AddMinutes(model.Time.Value.Minute);
                    DateTime NewEcDate = GetEcTime(OldEcDate, Convert.ToInt16(model.ECTimeZone), Convert.ToInt32(checkEconsult.UserId));
                    EcCalender.Date = NewEcDate;
                    UnitOfWork.CalendarRepository.Update(EcCalender);
                    UnitOfWork.Save();
                }

                var Econsult = UnitOfWork.EConsultationRepository.GetSingle(c => c.ID == model.Id);
                Econsult.IsEditExpert = true;
                model.Map(Econsult);
                UnitOfWork.EConsultationRepository.Update(Econsult);
                UnitOfWork.Save();

                var UserInfo = UnitOfWork.UserRepository.GetSingle(s => s.Id == Econsult.UserId);
                var vetinfo = UnitOfWork.UserRepository.GetSingle(s => s.Id == Econsult.VetId);
                var petinfo = UnitOfWork.PetRepository.GetSingle(s => s.Id == Econsult.PetId);


                int EconsultStatusId = Convert.ToInt16(Econsult.EconsultationStatusId);

                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                {
                    if (EconsultStatusId == 1)
                    {
                        string email = UserInfo.Email.Value.ToString();
                        string User1FirstName = UserInfo.FirstName.Value.ToString();
                        string User1LastName = UserInfo.LastName.Value.ToString();
                        string User2FirstName = vetinfo.FirstName.Value.ToString();
                        string User2LastName = vetinfo.LastName.Value.ToString();
                        string User2Email = vetinfo.Email.Value.ToString();
                        string PetName = petinfo.Name.Value.ToString();
                        string PetType = petinfo.PetTypeId.ToString();
                        string EconsultDate = model.Date.Value.ToLongDateString();
                        string EconsultTime = model.Time.Value.ToShortTimeString();
                        string EcCondition = Econsult.TitleConsultation.ToString();
                        EmailSender.SendToVetEconsultationProposedDate(email, User1FirstName, User1LastName, User2FirstName,
                            User2LastName, User2Email, PetName, PetType, EconsultDate, EconsultTime, model.Id, EcCondition);

                    }

                    if (EconsultStatusId == 2)
                    {
                        string email = UserInfo.Email.Value.ToString();
                        string User1FirstName = UserInfo.FirstName.Value.ToString();
                        string User1LastName = UserInfo.LastName.Value.ToString();
                        string User2FirstName = vetinfo.FirstName.Value.ToString();
                        string User2LastName = vetinfo.LastName.Value.ToString();
                        string User2Email = vetinfo.Email.Value.ToString();
                        string PetName = petinfo.Name.Value.ToString();
                        string PetType = petinfo.PetTypeId.ToString();
                        string EconsultDate = model.Date.Value.ToLongDateString();
                        string EconsultTime = model.Time.Value.ToShortTimeString();
                        string EcCondition = Econsult.TitleConsultation.ToString();

                        EmailSender.SendToUserEconsultationSchedule(email, User1FirstName, User1LastName, User2FirstName, User2LastName,
                            User2Email, PetName, PetType, Convert.ToDateTime(EconsultDate), Convert.ToDateTime(EconsultTime), model.Id, EcCondition);
                    }
                }
                else
                {
                    if (Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()) || Roles.IsUserInRole(UserTypeEnum.Partner.ToString()))
                    {
                        if (EconsultStatusId == 1 || EconsultStatusId == 2)
                        {
                            string email = vetinfo.Email.Value.ToString();
                            string User1FirstName = vetinfo.FirstName.Value.ToString();
                            string User1LastName = vetinfo.LastName.Value.ToString();
                            string User2FirstName = UserInfo.FirstName.Value.ToString();
                            string User2LastName = UserInfo.LastName.Value.ToString();
                            string User2Email = UserInfo.Email.Value.ToString();
                            string PetName = petinfo.Name.Value.ToString();
                            string PetType = petinfo.PetTypeId.ToString();
                            string EconsultDate = model.Date.Value.ToLongDateString();
                            string EconsultTime = model.Time.Value.ToShortTimeString();
                            string EcCondition = Econsult.TitleConsultation.ToString();
                            EmailSender.SendToVetEconsultationProposedDate(email, User1FirstName, User1LastName, User2FirstName,
                                User2LastName, User2Email, PetName, PetType, EconsultDate, EconsultTime, model.Id, EcCondition);
                        }
                    }
                }

                return Json(new { success = "ViewDetails" });
                //return Json(new { success = true, successMessage = ADOPets.Web.Resources.Wording.SMO_SubmitReport_SubmitReportSuccessMessage }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            else
            {
                //if all is ok, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            var UserRepository = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId).UserSubscriptionId;
            var Subscription = UnitOfWork.UserSubscriptionRepository.GetSingle(s => s.Id == UserRepository, p => p.Subscription);

            TempData.Clear();
            ViewBag.UserId = userId;

            TempData["HasEconsult_" + userId] = Subscription.Subscription.HasEConsultation;
            KeepTempData();
            AddViewModel model = new AddViewModel();

            model.objSetup = new AddSetupViewModel();
            model.objSetup.Pets = new List<ViewModels.Pet.IndexViewModel>();
            var dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == userId));
            var pets = dbPets.Select(p => new ADOPets.Web.ViewModels.Pet.IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, p.GenderId, p.Image)).ToList();
            model.objSetup.Pets = pets;

            model.objSetup.vets = new List<ViewModels.Veterinarian.IndexViewModel>();

            var veterinarian = UnitOfWork.VeterinarianRepository.GetAll(c => c.UserId == userId).OrderByDescending(c => c.Id).Select(c => new ViewModels.Veterinarian.IndexViewModel(c));

            model.objSetup.vets = veterinarian.ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UnitOfWork.UserRepository.GetSingleTracking(u => u.Id == userId);
            Success(Resources.Econsultation.Add.AddSuccessfullMessage);
            Session[Constants.SessionCurrentUserPetCount] = UnitOfWork.PetRepository.GetPetCountByUser(userId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Setup()
        {
            AddSetupViewModel model = new AddSetupViewModel();
            if (TempData["Setup_" + userId] != null)
                model = (AddSetupViewModel)TempData["Setup_" + userId];
            if (TempData["PetId_" + userId] != null)
            {
                int Id = (int)TempData["PetId_" + userId];
                var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == Id);
                TempData["PetName_" + userId] = pet.Name.Value;
            }
            if (TempData["VetId_" + userId] != null)
            {
                int Id = (int)TempData["VetId_" + userId];
                var vet = UnitOfWork.UserRepository.GetSingle(p => p.Id == Id);
                TempData["VetName_" + userId] = vet.FirstName.Value + " " + vet.LastName.Value;
            }
            KeepTempData();
            ViewBag.UserId = userId;
            return PartialView("_Setup", model);
        }

        [HttpPost]
        public ActionResult Setup(AddSetupViewModel setupmodel)
        {
            if (!ModelState.IsValid)
            {
                if (HttpContext.User.ToCustomPrincipal().CustomIdentity.UserRoleName == UserTypeEnum.OwnerAdmin.ToString())
                {
                    return View(setupmodel);
                }
                else
                {
                    return View("SetupVD", setupmodel);
                }
            }
            if (HttpContext.User.ToCustomPrincipal().CustomIdentity.UserRoleName == UserTypeEnum.OwnerAdmin.ToString())
            {
                setupmodel.VetID = Convert.ToInt32(Session["ExpertVetId"]);
                Session["UserProfile"] = null;
                setupmodel.UserId = userId;
                Session["UserProfile"] = setupmodel;
                TempData["Setup_" + userId] = setupmodel;
                setupmodel.PetId = Convert.ToInt32(Session["PetId"]);
                KeepTempData();
                if (TempData["HasEconsult_" + userId].ToString().ToLower() == "true")
                {
                    PaymentResult paymentResult;
                    if (WebConfigHelper.DoFakePayment)
                    {
                        paymentResult = PaymentHelper.FakePayment(new ADOPets.Web.ViewModels.Account.ConfirmationViewModel());
                    }
                    else if (DomainHelper.GetDomain() == DomainTypeEnum.US)
                    {
                        paymentResult = PaymentHelper.USAuthorizePayment(new ADOPets.Web.ViewModels.Account.ConfirmationViewModel());
                    }
                    else
                    {
                        paymentResult = PaymentHelper.USPayment(new ADOPets.Web.ViewModels.Account.ConfirmationViewModel());
                    }

                    if (paymentResult.Success)
                    {
                        AddViewModel model = new AddViewModel();
                        if (TempData["Setup_" + userId] != null)
                            model.objSetup = (AddSetupViewModel)TempData.Peek("Setup_" + userId);

                        var user = UnitOfWork.UserRepository.GetSingleTracking(u => u.Id == userId);

                        if (TempData["PetId_" + userId] != null)
                            model.objSetup.PetId = (int)TempData["PetId_" + userId];

                        if (TempData["VetId_" + userId] != null)
                            model.objSetup.VetID = (int)TempData["VetId_" + userId];

                        KeepTempData();
                        TempData["Payment_" + userId] = paymentResult;
                    }
                }
            }
            else
            {
                try
                {
                    setupmodel.UserId = userId;
                    setupmodel.OwnerId = (int)TempData["OwnerId_" + userId];
                    if (TempData["PetId_" + userId] != null)
                        setupmodel.PetId = (int)TempData["PetId_" + userId];
                    if (TempData["VetId_" + userId] != null)
                        setupmodel.VetID = (int)TempData["VetId_" + userId];

                    var econscount = UnitOfWork.EConsultationRepository.GetAll().OrderByDescending(u => u.ID).FirstOrDefault().ID;
                    setupmodel.Id = econscount + 1;
                    UnitOfWork.EConsultationRepository.Insert(setupmodel.MapEC());
                    UnitOfWork.Save();
                    //long econsoulreqid = UnitOfWork.EConsultationRepository.GetAll().OrderByDescending(u => u.ID).FirstOrDefault().ID;

                    UnitOfWork.EConsultationRoomRepository.Insert(setupmodel.MapEcRoom());
                    UnitOfWork.Save();
                    KeepTempData();
                    string userRole = HttpContext.User.GetUserRole();
                    var owneruser = UnitOfWork.UserRepository.GetSingle(a => a.Id == setupmodel.OwnerId);
                    EmailSender.SendECRequestPaymentPendingMail(userRole, owneruser.Email.Value, owneruser.FirstName.Value, owneruser.LastName.Value, "EC" + setupmodel.Id);

                    Success(Resources.Wording.Econsultation_Add_SuccessfullMessage);
                }
                catch { }
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Billing()
        {
            AddBillingViewModel model = new AddBillingViewModel();
            if (TempData["Billing_" + userId] != null)
            {
                model = (AddBillingViewModel)Session["PaymentData"];
            }
            if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                model.BillingCountry = CountryEnum.UnitedStates;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.India)
            {
                model.BillingCountry = CountryEnum.India;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                model.BillingCountry = CountryEnum.France;
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.Portuguese)
            {
                model.BillingCountry = CountryEnum.BRAZIL;
            }
            KeepTempData();
            model.PetId = Convert.ToInt16(Session["PetId"]);
            return PartialView("_Billing", model);
        }

        [HttpPost]
        public ActionResult Billing(AddBillingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Session["PaymentData"] = model;
            TempData["Billing_" + userId] = model;
            KeepTempData();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Confirmation()
        {
            AddViewModel model = new AddViewModel();
            model.objSetup = new AddSetupViewModel();
            model.objBilling = new AddBillingViewModel();
            if (TempData["Setup_" + userId] != null)
                model.objSetup = (AddSetupViewModel)TempData.Peek("Setup_" + userId);
            if (model.objSetup.PetId == 0)
                model.objSetup.PetId = Convert.ToInt32(Session["PetId"]);
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == model.objSetup.PetId);

            model.objSetup.PetName = (pet == null) ? String.Empty : pet.Name;

            var veterinarian = UnitOfWork.UserRepository.GetSingle(c => c.Id == model.objSetup.VetID);

            model.objSetup.VetName = (veterinarian == null) ? String.Empty : veterinarian.FirstName + " " + veterinarian.LastName + "," + EnumHelper.GetResourceValueForEnumValue(veterinarian.CountryId);

            if (TempData["Billing_" + userId] != null)
                model.objBilling = (AddBillingViewModel)TempData.Peek("Billing_" + userId);


            model.CountryName = model.objSetup.Country.ToString();

            model.BillingCountryName = model.objBilling.BillingCountry.Value.ToString();

            KeepTempData();
            return PartialView("_Confirmation", model);
        }

        [HttpPost]
        public ActionResult Confirmation(AddViewModel model1)
        {
            var billinginfo = Session["PaymentData"] as ADOPets.Web.ViewModels.Econsultation.AddBillingViewModel;
            var usersetupinfo = Session["UserProfile"] as AddSetupViewModel;
            var model = new ConfirmationViewModel1(usersetupinfo, billinginfo);
            var users = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
            int petid = Convert.ToInt16(Session["PetId"]);
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == petid);
            var defaultPlans = UnitOfWork.SubscriptionRepository.GetAll(u => !u.IsPromotionCode).OrderBy(u => u.MaxPetCount);
            var defaultBasePlan = defaultPlans.First(p => p.IsBase);
            ViewBag.AdditionalPets = defaultPlans.Skip(1).Select(i => new SelectListItem { Text = (i.MaxPetCount - 1).ToString(), Value = i.Id.ToString() });
            var planName = defaultBasePlan.Name;
            var plan = UnitOfWork.SubscriptionRepository.GetSingle(s => s.Id == users.UserSubscriptionId);

            model.Price = 25;
            model.Plan = planName;
            model.FirstName = users.FirstName;
            model.LastName = users.LastName;
            model.Email = users.Email;

            PaymentResult paymentResult;
            if (WebConfigHelper.DoFakePayment)
            {
                paymentResult = PaymentHelper.FakePayment(new ConfirmationViewModel());
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                paymentResult = PaymentHelper.USAuthorizePayment1(model);
            }
            else if (DomainHelper.GetDomain() == DomainTypeEnum.French)
            {
                paymentResult = PaymentHelper.FakePayment(new ConfirmationViewModel());
            }
            else
            {
                paymentResult = PaymentHelper.USPayment(new ConfirmationViewModel());
            }

            if (paymentResult.Success)
            {

                if (model.objSetup == null)
                {
                    model.objSetup = (AddSetupViewModel)TempData.Peek("Setup_" + userId);
                }

                if (model.objBilling == null)
                {
                    model.objBilling = (AddBillingViewModel)TempData.Peek("Billing_" + userId);
                }

                var user = UnitOfWork.UserRepository.GetSingleTracking(u => u.Id == userId);

                bool edituser = false;
                if (!string.IsNullOrEmpty(usersetupinfo.Id.ToString()) && usersetupinfo.Id > 0)
                {
                    edituser = true;
                    model.Id = usersetupinfo.Id;
                }
                else
                {
                    var econscount = UnitOfWork.EConsultationRepository.GetAll().OrderByDescending(u => u.ID).FirstOrDefault().ID;
                    model.Id = econscount + 1;
                }


                model.PaymentCharged = Convert.ToDecimal(25);
                model.PaymentRefNum = paymentResult.OrderId;
                model.PaymentTransferDateTime = Convert.ToDateTime(paymentResult.TransactionDate);
                model.PaymentTransferMsg = paymentResult.TransactionResult;
                model.PaymentTransferNum = paymentResult.TransactionID;

                //Save into Calender
                UnitOfWork.CalendarRepository.Insert(model.MapCalendar());
                UnitOfWork.Save();

                int Calenderid = UnitOfWork.CalendarRepository.GetAll().OrderByDescending(u => u.Id).FirstOrDefault().Id;

                model.CalenderId = Calenderid;

                long econsoulreqid = UnitOfWork.EConsultationRepository.GetAll().OrderByDescending(u => u.ID).FirstOrDefault().ID;
                if (edituser)
                {
                    econsoulreqid = usersetupinfo.Id;
                    EConsultation ecModel = UnitOfWork.EConsultationRepository.GetSingleTracking(e => e.ID == model.Id);

                    UnitOfWork.EConsultationRepository.Update(model.MapUpdate(ecModel));
                    UnitOfWork.Save();
                }
                else
                {
                    UnitOfWork.EConsultationRepository.Insert(model.Map());
                    UnitOfWork.Save();
                    econsoulreqid = UnitOfWork.EConsultationRepository.GetAll().OrderByDescending(u => u.ID).FirstOrDefault().ID;
                    UnitOfWork.EConsultationRoomRepository.Insert(model.MapEcRoom());
                    UnitOfWork.Save();
                }



                TempData["ecid"] = econsoulreqid;
                KeepTempData();

                decimal ecroomid = UnitOfWork.EConsultationRoomRepository.GetAll().OrderByDescending(u => u.ID).FirstOrDefault().ID;// .GetAll().OrderByDescending(u => u.ID).FirstOrDefault().ID;

                EconsultationUser EcUsers = new EconsultationUser();
                EcUsers.EConsultationId = model.Id;
                EcUsers.UserId = Convert.ToInt32(usersetupinfo.VetID);
                EcUsers.EconsultationRoomID = Convert.ToInt32(ecroomid);
                EcUsers.UserStatus = 0;
                UnitOfWork.EConsultationUserRepository.Insert(EcUsers);
                UnitOfWork.Save();

                EconsultationUser EcUsers1 = new EconsultationUser();
                EcUsers1.EConsultationId = model.Id;
                EcUsers1.UserId = Convert.ToInt32(usersetupinfo.UserId);
                EcUsers1.EconsultationRoomID = Convert.ToInt32(ecroomid);
                EcUsers1.UserStatus = 0;
                UnitOfWork.EConsultationUserRepository.Insert(EcUsers1);
                UnitOfWork.Save();

                EmailSender.SendMemberEconsultSubscription(model.Email, model.FirstName, model.LastName, paymentResult.OrderId, model.Price.ToString(), econsoulreqid.ToString(), model.BillingAddress1, model.BillingAddress2,
                model.BillingCity, EnumHelper.GetResourceValueForEnumValue(model.BillingState), model.BillingZip,
                EnumHelper.GetResourceValueForEnumValue(model.BillingCountry), model.Email);

                EmailSender.SendSupportUserEconsultSubscription(model.FirstName, model.LastName, model.ConsultationDate, model.UserId, model.Id, paymentResult.OrderId,
                    "E-Consultation", model.PaymentCharged.ToString(), paymentResult.TransactionResult);

                int vetid = Convert.ToInt16(Session["ExpertVetId"]);
                var vetinfo = UnitOfWork.UserRepository.GetSingle(s => s.Id == vetid);

                var Userpet = UnitOfWork.PetRepository.GetSingle(p => p.Id == model.PetID);

                string Vetemail = vetinfo.Email.Value.ToString();
                string VetFirstName = vetinfo.FirstName.Value.ToString();
                string VetLastName = vetinfo.LastName.Value.ToString();
                string Useremail = model.Email.ToString();
                string UserFirstName = model.FirstName.ToString();
                string UserLastName = model.LastName.ToString();
                string UserPetName = Userpet.Name.Value.ToString();
                string UserPetType = Userpet.PetTypeId.ToString();
                int ECID = model.Id;
                string PetCondition = model.objSetup.PetCondition.ToString();
                string UserID = model.UserId.ToString();
                EmailSender.SendToVetExpertEconsultation(Vetemail, VetFirstName, VetLastName, UserFirstName, UserLastName, Useremail, UserPetName,
                    UserPetType, model.ConsultationDate.Date, model.ConsultationTime, ECID, PetCondition, UserID);

                if (TempData["HasEconsult_" + userId].ToString().ToLower() == "false")
                {
                    model.objBilling.PaymentTypeId = PaymentTypeEnum.SaleTransaction;
                    model.objBilling.UserId = userId;
                    UnitOfWork.BillingInformationRepository.Insert(model.objBilling.Map());
                    UnitOfWork.Save();
                }

                paymentResult.TransactionDate = DateTime.Now.ToShortDateString();

                TempData["Payment_" + userId] = paymentResult;
                KeepTempData();

                Session[Constants.SessionCurrentUserPetCount] = UnitOfWork.PetRepository.GetPetCountByUser(userId);

                Session["ecid"] = econsoulreqid;

            }
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
            PaymentResultViewModel1 model = new PaymentResultViewModel1();
            if (TempData["Payment_" + userId] != null)
            {
                PaymentResult result = (PaymentResult)TempData["Payment_" + userId];
                model.Success = result.Success;
                model.ECID = Convert.ToInt16(Session["ecid"]);
                model.ErrorMsg = result.ErrorMessage;
                model.OrderNumber = result.OrderId;
                model.TransactionDate = result.TransactionDate;
                model.TransactionResult = result.TransactionResult;
                model.TransactionTime = result.TransactionTime;
            }

            Success(Resources.Wording.Econsultation_Add_AddSuccessfullMessage);

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

            //  var dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == usrId), navigationProperties: p => p.Users);
            var dbPets = Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString())
                ? UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == userId), navigationProperties: p => p.Users) :
                UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == ownerId), navigationProperties: p => p.Users);


            //var petOwner = UnitOfWork.UserRepository.GetSingleTracking(u => u.Id == userId);

            //var pets = new List<ADOPets.Web.ViewModels.Pet.IndexViewModel>();
            //pets = dbPets.Select(p => new ADOPets.Web.ViewModels.Pet.IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, p.GenderId, p.Image, petOwner)).ToList();

            //Session[Constants.SessionCurrentUserPetCount] = pets.Count;
            //KeepTempData();
            //return PartialView("_SelectPet", pets);



            List<ADOPets.Web.ViewModels.Pet.IndexViewModel> pets = new List<ViewModels.Pet.IndexViewModel>();
            if (dbPets != null)
                pets = dbPets.Select(p => new ADOPets.Web.ViewModels.Pet.IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, p.GenderId, p.Image, p.Users.FirstOrDefault())).ToList();

            if (pets != null)
                Session[Constants.SessionCurrentUserPetCount] = pets.Count;
            KeepTempData();

            if (pets.Count >= 0)
            {
                return PartialView("_SelectPet", pets);
            }
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

            model.PetId = Id;

            TempData["Setup_" + userId] = model;
            TempData["PetId_" + userId] = Id;
            Session["PetId"] = Id;
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == Id);
            TempData["PetName_" + userId] = pet.Name.Value;
            KeepTempData();

            return Json(new { success = pet.Name }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SelectVet()
        {

            var veterinarian = UnitOfWork.UserRepository.GetAll(s => s.UserTypeId == UserTypeEnum.VeterinarianExpert || s.UserTypeId == UserTypeEnum.VeterinarianAdo, null, s => s.Veterinarians).Where(a => a.CenterID == null).Select(c => new ViewModels.Econsultation.ExpertVeterinarainViewModel(c));
            int? centerId = HttpContext.User.GetCenterId();
            if (!string.IsNullOrEmpty(centerId.ToString()))
            {
                veterinarian = UnitOfWork.UserRepository.GetAll(s => s.UserTypeId == UserTypeEnum.VeterinarianExpert || s.UserTypeId == UserTypeEnum.VeterinarianAdo, null, s => s.Veterinarians).Where(a => a.CenterID == centerId).Select(c => new ViewModels.Econsultation.ExpertVeterinarainViewModel(c));
            }

            KeepTempData();
            return PartialView("_SelectVet1", veterinarian);
        }

        [HttpPost]
        public ActionResult SelectVet(int Id)
        {
            AddSetupViewModel model = new AddSetupViewModel();
            if (TempData["Setup_" + userId] != null)
                model = (AddSetupViewModel)TempData["Setup_" + userId];
            else
                model = new AddSetupViewModel();

            model.VetID = Id;

            TempData["VetId_" + userId] = Id;
            Session["ExpertVetId"] = Id;
            var vet = UnitOfWork.UserRepository.GetSingle(c => c.Id == Id);
            TempData["VetName_" + userId] = vet.FirstName + " " + vet.LastName;
            KeepTempData();
            var vetInfo = new
            {
                FirstName = vet.FirstName,
                LastName = vet.LastName
            };

            return Json(vetInfo, JsonRequestBehavior.AllowGet);
        }

        private void KeepTempData()
        {
            TempData.Keep("HasEconsult_" + userId);
            TempData.Keep("Setup_" + userId);
            TempData.Keep("Billing_" + userId);
            TempData.Keep("Payment_" + userId);
            TempData.Keep("PetId_" + userId);
            TempData.Keep("PetName_" + userId);
            TempData.Keep("VetId_" + userId);
            TempData.Keep("VetName_" + userId);
            TempData.Keep("OwnerId_" + userId);
        }

        [HttpGet]
        public ActionResult GetStates(string country)
        {
            var countryEnum = Enum.Parse(typeof(CountryEnum), country);
            var states = UnitOfWork.StateRepository.GetAll(s => s.CountryId == (CountryEnum)countryEnum).OrderBy(s => s.Name);
            var items = states.Select(i => new SelectListItem { Text = EnumHelper.GetResourceValueForEnumValue(i.Id), Value = i.Id.ToString() });
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public DateTime GetEcTime(DateTime thisTime, int TimeZoneId, int EcUserId)
        {
            var User = UnitOfWork.UserRepository.GetSingle(s => s.Id == EcUserId);
            int UserTimeZone = 15;
            if (User.TimeZoneId.HasValue)
            {
                UserTimeZone = Convert.ToInt16(User.TimeZoneId.Value);
            }
            DateTime tstTime = thisTime;
            if (UserTimeZone != TimeZoneId)
            {
                var TimeZoneName = UnitOfWork.TimeZoneRepository.GetSingle(t => t.Id == (TimeZoneEnum)TimeZoneId);
                var EcTimeZoneName = UnitOfWork.TimeZoneRepository.GetSingle(t => t.Id == (TimeZoneEnum)UserTimeZone);
                TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneName.TimeZoneInfoId);
                TimeZoneInfo tst1 = TimeZoneInfo.FindSystemTimeZoneById(EcTimeZoneName.TimeZoneInfoId);
                tstTime = TimeZoneInfo.ConvertTime(thisTime, tst, tst1); //TimeZoneInfo.Local
            }

            return tstTime;

        }

        public DateTime GetExpertEcTime(DateTime thisTime, int TimeZoneId, int EcUserId)
        {
            var User = UnitOfWork.UserRepository.GetSingle(s => s.Id == EcUserId);
            int UserTimeZone = 15;
            TimeZoneId = 36;
            if (User.TimeZoneId.HasValue)
            {
                UserTimeZone = Convert.ToInt16(User.TimeZoneId.Value);
            }
            DateTime tstTime = thisTime;

            var TimeZoneName = UnitOfWork.TimeZoneRepository.GetSingle(t => t.Id == (TimeZoneEnum)TimeZoneId);
            var EcTimeZoneName = UnitOfWork.TimeZoneRepository.GetSingle(t => t.Id == (TimeZoneEnum)UserTimeZone);

            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById(EcTimeZoneName.TimeZoneInfoId);
            TimeZoneInfo EctimeZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

            tstTime = TimeZoneInfo.ConvertTime(thisTime, EctimeZone, tst); //TimeZoneInfo.Local

            return Convert.ToDateTime(tstTime.ToString());

        }

        public ActionResult GetPetInfoStatus(int ecId)
        {
            var econsult = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == ecId);
            var Result = true;
            if (econsult != null)
                Result = true;
            else
                Result = false;

            return Json(new { success = Result }, JsonRequestBehavior.AllowGet);
        }

        public void RemoveSeletedFile(string Filename, string PetId, string EcId)
        {
            var dirPath = Path.Combine(getUserAbsolutePath(), Constants.PetsFolderName, PetId, Constants.DocumentFolderName, Constants.ECFolderName, EcId, Constants.DocumentFolderName);
            var path = Path.Combine(dirPath, Filename);
            var exist = System.IO.File.Exists(path);

            if (exist)
            {
                System.IO.File.Delete(path);
            }
            KeepTempData();
        }

        public void DeleteDocument(string fileId, string ECId)
        {
            KeepTempData();
            int docId = Convert.ToInt32(fileId);
            int ecId = Convert.ToInt32(ECId);

            var ECDocument = UnitOfWork.EconsultDocumentRepository.GetSingle(d => d.Id == docId);
            var petId = UnitOfWork.EConsultationRepository.GetSingle(s => s.ID == ecId).PetId;

            var dirPath = Path.Combine(getUserAbsolutePath(), Constants.PetsFolderName, petId.ToString(), Constants.DocumentFolderName, Constants.ECFolderName, ECId);
            var path = Path.Combine(dirPath, ECDocument.DocumentPath);
            var exist = System.IO.File.Exists(path);

            if (exist)
            {
                System.IO.File.Delete(path);
            }
            KeepTempData();
            UnitOfWork.EconsultDocumentRepository.Delete(ECDocument);
            UnitOfWork.Save();
        }

        private string getUserAbsolutePath()
        {
            var uid = HttpContext.User.ToCustomPrincipal().CustomIdentity.UserId.ToString();
            var infoPath = HttpContext.User.ToCustomPrincipal().CustomIdentity.InfoPath;

            return Path.Combine(WebConfigHelper.UserFilesPath, infoPath, uid);
        }



        #region Admin/VD/VA Econsultation Creation

        /// <summary>
        /// Action For Add New SMO After VD Login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateEC()
        {
            var UserRepository = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId).UserSubscriptionId;
            var Subscription = UnitOfWork.UserSubscriptionRepository.GetSingle(s => s.Id == UserRepository, p => p.Subscription);

            TempData.Clear();
            ViewBag.UserId = userId;
            TempData["HasEconsult_" + userId] = Subscription.Subscription.HasEConsultation;
            KeepTempData();
            AddViewModel model = new AddViewModel();

            model.objSetup = new AddSetupViewModel();
            model.objSetup.Pets = new List<ViewModels.Pet.IndexViewModel>();
            var dbPets = UnitOfWork.PetRepository.GetAll(p => p.Users.Any(u => u.Id == userId));
            var pets = dbPets.Select(p => new ADOPets.Web.ViewModels.Pet.IndexViewModel(p.Id, p.Name, p.PetTypeId, p.BirthDate, p.GenderId, p.Image)).ToList();
            model.objSetup.Pets = pets;
            model.objSetup.vets = new List<ViewModels.Veterinarian.IndexViewModel>();
            var veterinarian = UnitOfWork.VeterinarianRepository.GetAll(c => c.UserId == userId).OrderByDescending(c => c.Id).Select(c => new ViewModels.Veterinarian.IndexViewModel(c));
            model.objSetup.vets = veterinarian.ToList();
            return View(model);
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
            var ecrequest = UnitOfWork.UserRepository.GetSingle(p => p.Id == Id);

            model.OwnerId = Id;

            TempData["Setup_" + userId] = model;
            TempData["OwnerId_" + userId] = Id;
            TempData["OwnerName_" + userId] = ecrequest.FirstName.Value;
            KeepTempData();

            return Json(new { success = true, data = ecrequest.FirstName }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditEconsultation(int ecId)
        {
            ViewBag.UserId = userId;

            var UserRepository = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId).UserSubscriptionId;
            var Subscription = UnitOfWork.UserSubscriptionRepository.GetSingle(s => s.Id == UserRepository, p => p.Subscription);

            TempData.Clear();
            ViewBag.UserId = userId;

            TempData["HasEconsult_" + userId] = Subscription.Subscription.HasEConsultation;
            KeepTempData();

            EConsultation eConsultation = UnitOfWork.EConsultationRepository.GetSingleTracking(e => e.ID == ecId, navigationProperties: e => e.EconsultationRooms);
            AddSetupViewModel model = new AddSetupViewModel(eConsultation);
            TempData["PetId_" + userId] = model.PetId;
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == model.PetId);
            TempData["PetName_" + userId] = pet.Name.Value;
            Session["PetId"] = model.PetId;
            TempData["VetId_" + userId] = model.VetID;
            var vet = UnitOfWork.UserRepository.GetSingle(p => p.Id == model.VetID);
            TempData["VetName_" + userId] = vet.FirstName.Value + " " + vet.LastName.Value;

            Session["ExpertVetId"] = model.VetID;
            AddViewModel mdlAddEC = new AddViewModel();
            mdlAddEC.objSetup = model;
            KeepTempData();
            return View("Add", mdlAddEC);
        }

        public ActionResult DeleteECRequest(int ECId)
        {
            var ecRequest = UnitOfWork.EConsultationRepository.GetSingle(e => e.ID == ECId);
            ecRequest.IsDeleted = true;
            UnitOfWork.EConsultationRepository.Update(ecRequest);
            UnitOfWork.Save();
            Success(Resources.Wording.Econsultation_Delete_SuccessfullMessage);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
