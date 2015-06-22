using System;
using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.ViewModels.Message;
using Model;
using System.Collections.Generic;
using ADOPets.Web.Common.Helpers;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Linq.Expressions;
using Model.Tools;
using System.Globalization;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class MessageController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var userId = HttpContext.User.GetUserId();

            var model = new List<IndexViewModel>();

            UnitOfWork.MessageRepository.GetAll(m => m.UserId == userId, msgs => msgs.OrderByDescending(i => i.Date), m => m.UserFrom, m => m.UserTo)
                .GroupBy(m => m.ConversationId).ToList()
                .ForEach(g =>
                {
                    if (g.Any(i => i.MessageTypeId == MessageTypeEnum.Received))
                    {
                        var received = g.First(i => i.MessageTypeId == MessageTypeEnum.Received);
                        model.Add(new IndexViewModel(received, g.Any(i => i.Unread), g.Count()));
                    }
                    if (g.Any(i => i.MessageTypeId == MessageTypeEnum.Sent))
                    {
                        var sent = g.First(i => i.MessageTypeId == MessageTypeEnum.Sent);
                        model.Add(new IndexViewModel(sent, g.Any(i => i.Unread), g.Count()));
                    }
                });

            ViewBag.UserFullName = string.Format("{0} {1}", HttpContext.User.GetUserFirstName(), HttpContext.User.GetUserLastName());
            return View(model);
        }

        [HttpGet]
        public ActionResult SearchRecipient()
        {
            var actualUser = HttpContext.User.GetUserId();
            var userlist = UnitOfWork.UserRepository.GetAll(m => m.Id != actualUser && !m.IsNonSearchable && m.UserStatusId != UserStatusEnum.Disabled).Select(m => new SearchRecipientViewModel(m));
            return View(userlist);
        }

        [HttpGet]
        public ActionResult NewMessage(int toUserId, string requestFrom = "")
        {
            TempData["requestFrom"] = requestFrom;
            var user = UnitOfWork.UserRepository.GetSingle(u => u.Id == toUserId);
            var fullName = string.Format("{0} {1}", user.FirstName.Value, user.LastName.Value);

            return PartialView("_NewMessage", new NewMessageViewModel(toUserId, fullName));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewMessage(NewMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.GetUserId();
                var userFirstName = HttpContext.User.GetUserFirstName();
                var userLastName = HttpContext.User.GetUserLastName();

                var recipient = UnitOfWork.UserRepository.GetSingle(m => m.Id == model.ToUserId);

                var conversationKey = CreateConversationKey(userId, model.ToUserId);

                UnitOfWork.MessageRepository.Insert(model.MapSend(userId, conversationKey));
                UnitOfWork.MessageRepository.Insert(model.MapRecieve(userId, conversationKey));
                UnitOfWork.Save();

                EmailSender.SendNewMessageNotificationMail(recipient.Email, recipient.FirstName, recipient.LastName, userFirstName, userLastName, userId);
                Success(Resources.Wording.Message_NewMessage_NewMsgSuccess);
                if (string.IsNullOrEmpty(TempData["requestFrom"].ToString()))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string tempstr = TempData["requestFrom"].ToString();
                    var oId = Regex.Match(tempstr, @"(\d+)");
                    int tempId = Convert.ToInt32(oId.Groups[0].Value);
                    if (tempstr.ToLower().Contains("smo"))
                        return RedirectToAction("Edit", "SMO", new { id = tempId });
                    else if (tempstr.ToLower().Contains("econsultation") && Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()))
                        return RedirectToAction("ViewDetails", "Econsultation", new { EconsultID = tempId, IsRead = true });
                    else if (tempstr.ToLower().Contains("econsultation") && Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                        return RedirectToAction("ExpertViewDetails", "Econsultation", new { EconsultID = tempId, IsRead = true });
                    else
                        return RedirectToAction("Edit", "Pet", new { id = tempId });
                }
            }
            else
            {
                //if all is well, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult MessageDetail(string id)
        {
            var userId = HttpContext.User.GetUserId();

            var messages = UnitOfWork.MessageRepository.GetAll(m => m.UserId == userId && m.ConversationId == id, msgs => msgs.OrderBy(i => i.Date), m => m.UserFrom, m => m.UserTo).ToList();
            var message = messages.First();

            ViewBag.ConversationId = id;
            ViewBag.ConversationTitle = (string)message.Subject;
            ViewBag.RecipientName = message.FromUserId != userId
                ? string.Format("{0} {1}", message.UserFrom.FirstName.Value, message.UserFrom.LastName.Value)
                : string.Format("{0} {1}", message.UserTo.FirstName.Value, message.UserTo.LastName.Value);

            var model = messages.Select(m => new MessageDetailViewModel(m));

            UnitOfWork.MessageRepository.MarkAsRead(id, userId);

            return View(model);
        }

        [HttpGet]
        public ActionResult Invite()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Invite(InviteViewModel model, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.GetUserId();
                var userFirstName = HttpContext.User.GetUserFirstName();
                var userLastName = HttpContext.User.GetUserLastName();

                var test = fc["hdninvitetype"];
                if (test == "Friend")
                {
                    EmailSender.SendInviteMailFriend(model.Email, model.FirstName, model.LastName, userFirstName, userLastName);
                    EmailSender.SendInviteMailToSupportForFriend(model.FirstName, model.LastName, model.Email, userFirstName, userLastName, userId);
                }
                else
                {
                    EmailSender.SendInviteMailVeterinarian(model.Email, model.FirstName, model.LastName, userFirstName, userLastName);
                    EmailSender.SendInviteMailToSupportForVet(model.FirstName, model.LastName, model.Email, userFirstName, userLastName, userId);
                }

                Success(Resources.Wording.Message_Invite_InviteSuccessMessage);
                return RedirectToAction("Invite");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult ReplayMessage(string conversationId)
        {
            var model = new MessageReplyViewModel { ConversationId = conversationId };
            return PartialView("_MessageReply", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReplayMessage(MessageReplyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = UnitOfWork.MessageRepository.GetFirst(m => m.ConversationId == model.ConversationId);

                var userId = HttpContext.User.GetUserId();
                var userFirstName = HttpContext.User.GetUserFirstName();
                var userLastName = HttpContext.User.GetUserLastName();

                var userToId = message.FromUserId != userId ? message.FromUserId : message.ToUserId;
                var recipient = UnitOfWork.UserRepository.GetSingle(m => m.Id == userToId);


                UnitOfWork.MessageRepository.Insert(model.MapSend(message.ConversationId, message.Subject, userId, userToId));
                UnitOfWork.MessageRepository.Insert(model.MapRecieve(message.ConversationId, message.Subject, userId, userToId));
                UnitOfWork.Save();

                EmailSender.SendNewMessageNotificationMail(recipient.Email, recipient.FirstName, recipient.LastName, userFirstName, userLastName, userId);
                Success(Resources.Wording.Message_MessageReplay_NewMsgSuccess);

                return RedirectToAction("Index");
            }
            else
            {
                //if all is well, this will never happen
                throw new Exception("Ajax Call Failed!");
            }
        }

        /// <summary>
        /// For Side bar Message Notification Details
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult MessageNotification()
        {
            var userId = HttpContext.User.GetUserId();

            var newMessages = UnitOfWork.MessageRepository.GetAll(m => m.UserId == userId && m.Unread, msgs => msgs.OrderByDescending(i => i.Date), m => m.UserFrom)
                                  .Select(m => new MessageNotificationViewModel(m.ConversationId, m.Subject, m.FromUserId, TimeZoneHelper.TimeAgo(m.Date), m.UserFrom.FirstName, m.MessageTypeId));

            return PartialView("_MessageNotification", newMessages);
        }

        /// <summary>
        /// For Bell Notifications
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult MessageCount()
        {
            var userId = HttpContext.User.GetUserId();

            #region Message
            List<MessageNotificationViewModel> newMessages = UnitOfWork.MessageRepository.GetAll(m => m.UserId == userId && m.Unread && m.MessageTypeId == MessageTypeEnum.Received, msgs => msgs.OrderByDescending(i => i.Date), m => m.UserFrom)
                                .Select(m => new MessageNotificationViewModel(m.ConversationId, "", m.UserFrom.FirstName, "message")).ToList();
            #endregion

            #region Calender
            DateTime dt = TimeZoneHelper.GetCurrentUserLocalTime();

            var newreminder = UnitOfWork.CalendarRepository.GetAll(r => r.UserId == userId && !r.IsRead, reminder => reminder.OrderBy(i => i.Date))
                .Where(r => DateTime.Compare(r.Date.Value.Date, dt.Date) == 0)
                                 .Select(r => new ViewModels.Calender.CalenderNotificationViewModel(r.Id.ToString(), r.Reason, r.UserId, r.Date));

            foreach (var rem in newreminder)
            {
                if (rem.ReminderDateText.TimeOfDay > dt.TimeOfDay)
                {
                    var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
                    newMessages.Add(new MessageNotificationViewModel(rem.ReminderId, rem.Subject, user.FirstName, "reminder"));
                }
            }
            #endregion

            if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                #region Econsultation
                if (Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()))
                {
                    var dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.UserId == userId && !s.IsDeleted, null, s => s.Pet, s => s.User)
                        .Where(r => DateTime.Compare(r.RDVDateTime.Value.Date, dt.Date) == 0).Where(r => r.IsRead == true)
                        .Select(p => new ViewModels.Econsultation.IndexViewModel(p)).OrderByDescending(s => s.date);

                    foreach (var econ in dbECon)
                    {
                        var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
                        string sub = "", subject = "";

                        if (econ.Status.ToString() == EConsultationStatusEnum.Scheduled.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_ECSheduled;
                        }
                        else if (econ.Status.ToString() == EConsultationStatusEnum.Closed.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_ECClosed;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            subject = sub.Replace("()", "(EC" + econ.Id + ")");
                            DateTime EcRequestDate = Convert.ToDateTime(econ.ECTime);
                            newMessages.Add(new MessageNotificationViewModel(econ.Id.ToString(), subject, user.FirstName, "econsultation", EcRequestDate));
                        }
                    }
                }

                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                {
                    var dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.VetId == userId && !s.IsDeleted, null, s => s.Pet, s => s.User)
                        .Where(r => DateTime.Compare(r.RDVDateTime.Value.Date, dt.Date) == 0).Where(r => r.IsRead == true)
                        .Select(p => new ViewModels.Econsultation.IndexViewModel(p)).OrderByDescending(s => s.date);

                    foreach (var econ in dbECon)
                    {
                        var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
                        string sub = "", subject = "";

                        if (econ.Status.ToString() == EConsultationStatusEnum.Open.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_ECOpen;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            subject = sub.Replace("()", "(EC" + econ.Id + ")");
                            DateTime EcRequestDate = Convert.ToDateTime(econ.ECTime);
                            newMessages.Add(new MessageNotificationViewModel(econ.Id.ToString(), subject, user.FirstName, "econsultation", EcRequestDate));
                        }
                    }
                }


                //SMO notification created by vd/admin/va
                if (Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()))
                {
                    var dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.UserId == userId && !s.IsDeleted, null, s => s.Pet, s => s.User)
                        .Where(r => DateTime.Compare(r.RDVDateTime.Value.Date, dt.Date) == 0).Where(r => r.IsOwnerRead == false)
                        .Select(p => new ViewModels.Econsultation.IndexViewModel(p)).OrderByDescending(s => s.date);

                    //.GetAll(s => s.UserId == userId && !s.IsOwnerRead && !s.IsSMOPaymentDone && s.SMOSubmittedBy != null, null, s => s.Pet, s => s.User).OrderBy(s => s.SMORequestStatusId).OrderByDescending(s => s.RequestDate).Select(p => new ViewModels.SMO.IndexViewModel(p));

                    foreach (var econ in dbECon)
                    {
                        string sub = "", subject = "";

                        if (econ.ECSubmittedBy != null)
                        {
                            sub = Resources.Wording.Message_MesssageNotification_ECOWNEROPEN;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            int smoBy = Convert.ToInt32(econ.ECSubmittedBy);
                            string username = UnitOfWork.LoginRepository.GetSingleTracking(l => l.UserId == smoBy).UserName;
                            string usrRole = GetUserRoleByUserName(Encryption.Decrypt(username));

                            subject = sub.Replace("()", "(EC" + econ.Id + ")");
                            subject = subject + " " + usrRole;
                            newMessages.Add(new MessageNotificationViewModel(econ.Id.ToString(), subject, "econsultation", null, econ.IsPaymentDone));
                        }
                    }
                }
                #endregion

                #region SMO notifications
                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()))
                {
                    var VD_SMO = UnitOfWork.SMORequestRepository.GetAll(s => s.User.VetDirectorID == null && s.IsDeleted == false && !s.IsRead && s.IsSMOPaymentDone && s.SMOSubmittedBy == null, s => s.OrderByDescending(a => a.RequestDate), s => s.Pet, s => s.User).OrderBy(s => s.SMORequestStatusId).Select(p => new ViewModels.SMO.IndexViewModel(p));

                    foreach (var smodetail in VD_SMO)
                    {
                        string sub = "", subject = "";
                        if (smodetail.RequestStatus == SMORequestStatusEnum.Open.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOVDOPEN;
                        }
                        else if (smodetail.RequestStatus == SMORequestStatusEnum.Validated.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOVDVALIDATED;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            subject = sub.Replace("()", "(" + smodetail.SMOId + ")");
                            newMessages.Add(new MessageNotificationViewModel(smodetail.Id.ToString(), subject, "smo"));
                        }
                    }

                    //smo submitted by admin/vd/va
                    var listSMO = UnitOfWork.SMORequestRepository.GetAll(s => s.User.VetDirectorID == null && s.IsDeleted == false && !s.IsRead && s.IsSMOPaymentDone && s.SMOSubmittedBy != null, s => s.OrderByDescending(a => a.RequestDate), s => s.Pet, s => s.User).OrderBy(s => s.SMORequestStatusId).Select(p => new ViewModels.SMO.IndexViewModel(p));

                    foreach (var smodetail in listSMO)
                    {
                        string sub = "", subject = "";
                        if (smodetail.RequestStatus == SMORequestStatusEnum.Open.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOVDOPEN;
                        }
                        else if (smodetail.RequestStatus == SMORequestStatusEnum.Validated.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOVDVALIDATED;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            subject = sub.Replace("()", "(" + smodetail.SMOId + ")");
                            newMessages.Add(new MessageNotificationViewModel(smodetail.Id.ToString(), subject, "smo"));
                        }
                    }
                }

                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                {
                    var VD_SMO = UnitOfWork.SMOExpertRepository.GetAll(s => s.VetExpertID == userId && s.SMORequest.IsDeleted == false && !s.IsRead, null, s => s.SMORequest, s => s.User, s => s.SMORequest.Pet, s => s.SMORequest.User, s => s.SMORequest.Pet.Users).OrderBy(s => s.SMORequest.SMORequestStatusId).Select(p => new ViewModels.SMO.ExpertRelationViewModel(p, p.SMORequest.Pet.Users.FirstOrDefault(), p.SMORequest.ClosedOn));

                    foreach (var smodetail in VD_SMO)
                    {
                        string sub = "", subject = "";

                        if (smodetail.RequestStatus == SMORequestStatusEnum.Assigned.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOVEASSIGNED;
                        }
                        else if (smodetail.RequestStatus == SMORequestStatusEnum.Complete.ToString())
                        {
                            if (smodetail.SMOClosedOnDate != null && DateTime.Compare(DateTime.Now.Date, Convert.ToDateTime(smodetail.SMOClosedOnDate.Value)) <= 0)
                                sub = Resources.Wording.Message_MesssageNotification_SMOVECLOSED;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            subject = sub.Replace("()", "(" + smodetail.SMOId + ")");
                            newMessages.Add(new MessageNotificationViewModel(smodetail.Id.ToString(), subject, "smo"));
                        }
                    }
                }

                if (Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()))
                {
                    var dbSMO = UnitOfWork.SMORequestRepository.GetAll(s => s.UserId == userId && s.IsDeleted == false && !s.IsOwnerRead, null, s => s.Pet, s => s.User).OrderBy(s => s.SMORequestStatusId).OrderByDescending(s => s.RequestDate).Select(p => new ViewModels.SMO.IndexViewModel(p));

                    foreach (var smodetail in dbSMO)
                    {
                        string sub = "", subject = "";

                        if (smodetail.RequestStatus == SMORequestStatusEnum.Assigned.ToString() || smodetail.RequestStatus == SMORequestStatusEnum.Validated.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOINPROGRESS;
                        }
                        else if (smodetail.RequestStatus == SMORequestStatusEnum.Complete.ToString())
                        {
                            // TODO :: if (smodetail.SMOClosedOnDate != null && DateTime.Compare(DateTime.Now.Date, Convert.ToDateTime(smodetail.SMOClosedOnDate.Value)) <= 0)
                            sub = Resources.Wording.Message_MesssageNotification_SMOCLOSED;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            subject = sub.Replace("()", "(" + smodetail.SMOId + ")");
                            if (smodetail.SMOSubmittedBy == null)
                            {
                                newMessages.Add(new MessageNotificationViewModel(smodetail.Id.ToString(), subject, "smo"));
                            }
                            else
                            {
                                newMessages.Add(new MessageNotificationViewModel(smodetail.Id.ToString(), subject, "smo", null, smodetail.IsSMOPaymentDone));
                            }
                        }
                    }
                }

                //SMO notification created by vd/admin/va
                if (Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()))
                {
                    var dbSMO = UnitOfWork.SMORequestRepository.GetAll(s => s.UserId == userId && s.IsDeleted == false && !s.IsOwnerRead && !s.IsSMOPaymentDone && s.SMOSubmittedBy != null, null, s => s.Pet, s => s.User).OrderBy(s => s.SMORequestStatusId).OrderByDescending(s => s.RequestDate).Select(p => new ViewModels.SMO.IndexViewModel(p));

                    foreach (var smodetail in dbSMO)
                    {
                        string sub = "", subject = "";

                        if (smodetail.SMOSubmittedBy != null)
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOOWNEROPEN;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            int smoBy = Convert.ToInt32(smodetail.SMOSubmittedBy);
                            string username = UnitOfWork.LoginRepository.GetSingleTracking(l => l.UserId == smoBy).UserName;
                            string usrRole = GetUserRoleByUserName(Encryption.Decrypt(username));

                            subject = sub.Replace("()", "(" + smodetail.SMOId + ")");
                            subject = subject + " " + usrRole;
                            newMessages.Add(new MessageNotificationViewModel(smodetail.Id.ToString(), subject, "smo", null, smodetail.IsSMOPaymentDone));
                        }
                    }
                }

                #endregion

            }
            #region SharePetInformation
            if (Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()))
            {
                Expression<Func<SharePetInformation, object>> parames1 = v => v.User1;
                Expression<Func<SharePetInformation, object>> parames2 = v => v.User;
                Expression<Func<SharePetInformation, object>>[] paramesArray = new Expression<Func<SharePetInformation, object>>[] { parames1, parames2 };
                var shareCategoryType = UnitOfWork.SharePetInformationRepository.GetAll(r => r.IsRead == false, navigationProperties: paramesArray).Where(x => x.ContactId == userId && x.UserId != userId).Select(x => new ADOPets.Web.ViewModels.NewsFeed.IndexViewModel(x)).ToList();

                foreach (var item in shareCategoryType)
                {
                    string SharedInfo = "";
                    if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareIDInformation)
                        SharedInfo = Resources.Wording.ShareCategoryType_IdInformation;
                    if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareMedicalRecords)
                        SharedInfo = Resources.Wording.ShareCategoryType_MedicalRecords;
                    if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareVeterinarians)
                        SharedInfo = Resources.Wording.ShareCategoryType_Veterinarians;
                    if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareContacts)
                        SharedInfo = Resources.Wording.ShareCategoryType_Contacts;
                    if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareGallery)
                        SharedInfo = Resources.Wording.ShareCategoryType_Gallery;

                    string PetName = UnitOfWork.PetRepository.GetAll(x => x.Id == item.PetId).Select(x => x.Name).FirstOrDefault();
                    string SharedInfomessage1 = string.Format(Resources.Wording.NewsFeed_Notification_Message, item.FirstName + " " + item.LastName, SharedInfo, PetName);
                    newMessages.Add(new MessageNotificationViewModel(item.PetId.ToString(), SharedInfomessage1, item.FirstName, item.ShareCategoryTypeId, "shareinfo", item.CreationDate));
                }

                // share community
                Expression<Func<SharePetInfoCommunity, object>> prm1 = v => v.User1;
                Expression<Func<SharePetInfoCommunity, object>> prm2 = v => v.User;
                Expression<Func<SharePetInfoCommunity, object>>[] prms = new Expression<Func<SharePetInfoCommunity, object>>[] { prm1, prm2 };
                var shareCategoryTypeCommunity = UnitOfWork.SharePetInfoCommunityRepository.GetAll(r => r.IsRead == false, navigationProperties: prms).Where(x => x.ContactId == userId && x.UserId != userId).Select(x => new ADOPets.Web.ViewModels.NewsFeed.IndexViewModel(x)).ToList();

                foreach (var item in shareCategoryTypeCommunity)
                {
                    string SharedInfo = "";
                    if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareGallery)
                        SharedInfo = Resources.Wording.ShareCategoryType_Gallery;

                    string PetName = UnitOfWork.PetRepository.GetAll(x => x.Id == item.PetId).Select(x => x.Name).FirstOrDefault();
                    string SharedInfomessage1 = string.Format(Resources.Wording.NewsFeed_Notification_Message, item.FirstName + " " + item.LastName, SharedInfo, PetName);
                    newMessages.Add(new MessageNotificationViewModel(item.PetId.ToString(), SharedInfomessage1, item.FirstName, item.ShareCategoryTypeId, "shareinfo", item.CreationDate));
                }
            }
            #endregion

            ViewBag.NewMesssageCount = newMessages.Count();
            return PartialView("_MessageCount", newMessages);
        }

        public ActionResult NotificationHistoryIndex()
        {
            return View("NotificationHistoryIndex");
        }

        [HttpGet]
        public ActionResult NotificationHistory()
        {
            var userId = HttpContext.User.GetUserId();
            List<MessageNotificationViewModel> newMessages = getNotificationList(userId);
            List<NotificationHistoryViewModel> result = newMessages
                                                        .GroupBy(p => p.NotificationDate.Month)
                                                        .Select(g => new NotificationHistoryViewModel(g.OrderByDescending(p => p.NotificationDate).ToList(), g.Select(a => a.NotificationDate)))
                                                        .ToList();
            ViewBag.NotificationCount = newMessages.Count();
            return PartialView("_NotificationHistory", result.OrderByDescending(o => o.NotificationDate));
        }

        [HttpGet]
        public ActionResult SearchNotification(string StartDate, string EndDate)
        {
            var userId = HttpContext.User.GetUserId();
            List<MessageNotificationViewModel> newMessages = getNotificationList(userId);

            List<MessageNotificationViewModel> finalData = new List<MessageNotificationViewModel>();

            DateTime? StartDate1 = (!string.IsNullOrEmpty(StartDate)) ? (DateTime?)Convert.ToDateTime(StartDate, CultureInfo.CurrentCulture) : null;
            DateTime? EndDate1 = (!string.IsNullOrEmpty(EndDate)) ? (DateTime?)Convert.ToDateTime(EndDate, CultureInfo.CurrentCulture) : null;

            if (StartDate1 != null && EndDate1 != null)
            {
                finalData = newMessages.Where(n => n.NotificationDate.Date <= EndDate1 && n.NotificationDate >= StartDate1).ToList();
            }
            else if (StartDate1 != null && EndDate1 == null)
            {
                var maxDate = newMessages.Select(c => c.NotificationDate).Max();
                finalData = newMessages.Where(c => c.NotificationDate >= StartDate1 && c.NotificationDate <= maxDate).ToList();
            }
            else if (StartDate1 == null && EndDate1 != null)
            {
                var minDate = newMessages.Select(c => c.NotificationDate).Min();
                finalData = newMessages.Where(c => c.NotificationDate >= minDate && Convert.ToDateTime(c.NotificationDate.ToShortDateString()).Date <= EndDate1).ToList();
            }
            else { finalData = newMessages.ToList(); }

            List<NotificationHistoryViewModel> result = finalData
                                                  .GroupBy(p => p.NotificationDate.Month)
                                                  .Select(g => new NotificationHistoryViewModel(g.OrderByDescending(p => p.NotificationDate).ToList(), g.Select(a => a.NotificationDate)))
                                                  .ToList();
            ViewBag.NotificationCount = newMessages.Count();
            return PartialView("_NotificationHistory", result);

        }

        [HttpPost]
        public ActionResult DeleteMessage(List<string> conversationsKey)
        {
            if (conversationsKey != null)
            {
                var userId = HttpContext.User.GetUserId();
                UnitOfWork.MessageRepository.DeleteConversation(conversationsKey, userId);
            }
            Success(Resources.Wording.Message_Index_DeleteMessage);
            return RedirectToAction("Index");
        }

        private string CreateConversationKey(int fromUserId, int toUserId)
        {
            return string.Format("{0}-{1}-{2}", fromUserId, toUserId, Environment.TickCount);
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

        private List<MessageNotificationViewModel> getNotificationList(int userId)
        {
            #region Message
            List<MessageNotificationViewModel> newMessages = UnitOfWork.MessageRepository.GetAll(m => m.UserId == userId && !m.Unread && m.MessageTypeId == MessageTypeEnum.Received, msgs => msgs.OrderByDescending(i => i.Date), m => m.UserFrom)
                                .Select(m => new MessageNotificationViewModel(m.ConversationId, "", m.UserFrom.FirstName, "message", m.Date)).ToList();
            #endregion

            #region Calender
            DateTime dt = TimeZoneHelper.GetCurrentUserLocalTime();

            var newreminder = UnitOfWork.CalendarRepository.GetAll(r => r.UserId == userId && r.IsRead, reminder => reminder.OrderBy(i => i.Date))
                                 .Select(r => new ViewModels.Calender.CalenderNotificationViewModel(r.Id.ToString(), r.Reason, r.UserId, r.Date));

            foreach (var rem in newreminder)
            {
                var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
                newMessages.Add(new MessageNotificationViewModel(true, rem.ReminderId, rem.Subject, user.FirstName, "reminder", rem.ReminderDateText));
            }
            #endregion

            if (DomainHelper.GetDomain() == DomainTypeEnum.US)
            {
                #region Econsultation
                if (Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()))
                {
                    var dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.UserId == userId && s.IsDeleted == false, null, s => s.Pet, s => s.User)
                        //.Where(r => DateTime.Compare(r.RDVDateTime.Value.Date, dt.Date) == 0)
                        .Select(p => new ViewModels.Econsultation.IndexViewModel(p)).OrderByDescending(s => s.date);

                    foreach (var econ in dbECon)
                    {
                        var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
                        string sub = "", subject = "";

                        if (econ.Status.ToString() == EConsultationStatusEnum.Scheduled.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_ECSheduled;
                        }
                        else if (econ.Status.ToString() == EConsultationStatusEnum.Closed.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_ECClosed;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            subject = sub.Replace("()", "(EC" + econ.Id + ")");
                            DateTime EcRequestDate = Convert.ToDateTime(econ.ECTime);
                            newMessages.Add(new MessageNotificationViewModel(econ.Id.ToString(), subject, user.FirstName, "econsultation", EcRequestDate));
                        }
                    }

                    //EC notification created by vd/admin/va
                    dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.UserId == userId && s.IsDeleted == false && s.IsOwnerRead && s.CreatedBy != null && s.PaymentFlag == 1, null, s => s.Pet, s => s.User)
                        .OrderBy(s => s.EconsultationStatusId).OrderByDescending(s => s.DateConsultation).Select(p => new ViewModels.Econsultation.IndexViewModel(p)).OrderByDescending(s => s.date);

                    foreach (var dbec in dbECon)
                    {
                        string sub = "", subject = "";
                        DateTime dtSMORequest = Convert.ToDateTime(dbec.ECTime);
                        if (dbec.ECSubmittedBy != null)
                        {
                            sub = Resources.Wording.Message_MesssageNotification_ECOWNEROPEN;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            int ecBy = Convert.ToInt32(dbec.ECSubmittedBy);
                            string username = UnitOfWork.LoginRepository.GetSingleTracking(l => l.UserId == ecBy).UserName;
                            string usrRole = GetUserRoleByUserName(Encryption.Decrypt(username));

                            subject = sub.Replace("()", "(EC" + dbec.Id + ")");
                            subject = subject + " " + usrRole;

                            newMessages.Add(new MessageNotificationViewModel(dbec.Id.ToString(), subject, "econsultation", dtSMORequest, dbec.IsPaymentDone));
                        }
                    }
                }

                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                {
                    var dbECon = UnitOfWork.EConsultationRepository.GetAll(s => s.VetId == userId && s.IsDeleted == false, null, s => s.Pet, s => s.User)
                        //.Where(r => DateTime.Compare(r.RDVDateTime.Value.Date, dt.Date) == 0)
                        .Select(p => new ViewModels.Econsultation.IndexViewModel(p)).OrderByDescending(s => s.date);

                    foreach (var econ in dbECon)
                    {
                        var user = UnitOfWork.UserRepository.GetSingle(s => s.Id == userId);
                        string sub = "", subject = "";

                        if (econ.Status.ToString() == EConsultationStatusEnum.Open.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_ECOpen;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            subject = sub.Replace("()", "(EC" + econ.Id + ")");
                            DateTime EcRequestDate = Convert.ToDateTime(econ.ECTime);
                            newMessages.Add(new MessageNotificationViewModel(econ.Id.ToString(), subject, user.FirstName, "econsultation", EcRequestDate));
                        }
                    }
                }
                #endregion

                #region SMO notifications
                #region vd
                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()))
                {
                    var VD_SMO = UnitOfWork.SMORequestRepository
                        .GetAll(s => s.User.VetDirectorID == null && s.IsRead && s.IsSMOPaymentDone, s => s.OrderByDescending(a => a.RequestDate), s => s.Pet, s => s.User)
                        .OrderBy(s => s.SMORequestStatusId).Select(p => new ViewModels.SMO.IndexViewModel(p)); //&& s.SMOSubmittedBy == null

                    foreach (var smodetail in VD_SMO)
                    {
                        string sub = "", subject = "";

                        if (smodetail.RequestStatus == SMORequestStatusEnum.Open.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOVDOPEN;
                        }
                        else if (smodetail.RequestStatus == SMORequestStatusEnum.Validated.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOVDVALIDATED;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            subject = sub.Replace("()", "(" + smodetail.SMOId + ")");
                            DateTime dtSMORequest = Convert.ToDateTime(smodetail.RequestDateNotification);
                            newMessages.Add(new MessageNotificationViewModel(smodetail.Id.ToString(), subject, "", "smo", dtSMORequest));
                        }
                    }
                }
                #endregion

                #region expert

                if (Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()))
                {
                    var VD_SMO = UnitOfWork.SMOExpertRepository.GetAll(s => s.VetExpertID == userId && s.IsRead, null, s => s.SMORequest, s => s.User, s => s.SMORequest.Pet, s => s.SMORequest.User, s => s.SMORequest.Pet.Users).OrderBy(s => s.SMORequest.SMORequestStatusId).Select(p => new ViewModels.SMO.ExpertRelationViewModel(p, p.SMORequest.Pet.Users.FirstOrDefault(), p.SMORequest.ClosedOn));

                    foreach (var smodetail in VD_SMO)
                    {
                        string sub = "", subject = "";

                        if (smodetail.RequestStatus == SMORequestStatusEnum.Assigned.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOVEASSIGNED;
                        }
                        else if (smodetail.RequestStatus == SMORequestStatusEnum.Complete.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOVECLOSED;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            subject = sub.Replace("()", "(" + smodetail.SMOId + ")");
                            DateTime dtSMORequest = (smodetail.SMOClosedOnDate != null) ? Convert.ToDateTime(smodetail.SMOClosedOnDate) : Convert.ToDateTime(smodetail.RequestDateNotification);
                            newMessages.Add(new MessageNotificationViewModel(smodetail.Id.ToString(), subject, "", "smo", dtSMORequest));
                        }
                    }
                }
                #endregion

                #region owner
                if (Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()))
                {
                    var dbSMO = UnitOfWork.SMORequestRepository.GetAll(s => s.UserId == userId && s.IsDeleted == false && s.IsOwnerRead, null, s => s.Pet, s => s.User).OrderBy(s => s.SMORequestStatusId).OrderByDescending(s => s.RequestDate).Select(p => new ViewModels.SMO.IndexViewModel(p));

                    foreach (var smodetail in dbSMO)
                    {
                        string sub = "", subject = "";
                        DateTime dtSMORequest = (smodetail.SMOClosedOnDate != null) ? Convert.ToDateTime(smodetail.SMOClosedOnDate) : Convert.ToDateTime(smodetail.RequestDateNotification);

                        if (smodetail.RequestStatus == SMORequestStatusEnum.Assigned.ToString() || smodetail.RequestStatus == SMORequestStatusEnum.Validated.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOINPROGRESS;
                            var expertResponse = UnitOfWork.SMOExpertRepository.GetAll(s => s.SMORequestID == smodetail.Id, null, s => s.SMORequest, s => s.User, s => s.SMORequest.Pet, s => s.SMORequest.User, s => s.SMORequest.Pet.Users).OrderBy(s => s.SMORequest.SMORequestStatusId).Select(p => new ViewModels.SMO.ExpertRelationViewModel(p, p.SMORequest.Pet.Users.FirstOrDefault(), p.SMORequest.ClosedOn));

                            var expert = expertResponse.FirstOrDefault();
                            try
                            {
                                dtSMORequest = Convert.ToDateTime(expert.RequestDateNotification);
                            }
                            catch
                            {
                                dtSMORequest = (smodetail.SMOClosedOnDate != null) ? Convert.ToDateTime(smodetail.SMOClosedOnDate) : Convert.ToDateTime(smodetail.RequestDateNotification);
                            }
                        }
                        else if (smodetail.RequestStatus == SMORequestStatusEnum.Complete.ToString())
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOCLOSED;
                            dtSMORequest = (smodetail.SMOClosedOnDate != null) ? Convert.ToDateTime(smodetail.SMOClosedOnDate) : Convert.ToDateTime(smodetail.RequestDateNotification);
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            subject = sub.Replace("()", "(" + smodetail.SMOId + ")");
                            newMessages.Add(new MessageNotificationViewModel(smodetail.Id.ToString(), subject, "", "smo", dtSMORequest, smodetail.IsSMOPaymentDone));
                        }
                    }

                    //SMO notification created by vd/admin/va
                    var dbSMOlist = UnitOfWork.SMORequestRepository.GetAll(s => s.UserId == userId && s.IsDeleted == false && s.IsOwnerRead && s.SMOSubmittedBy != null && !s.IsSMOPaymentDone, null, s => s.Pet, s => s.User).OrderBy(s => s.SMORequestStatusId).OrderByDescending(s => s.RequestDate).Select(p => new ViewModels.SMO.IndexViewModel(p));

                    foreach (var smodetail in dbSMOlist)
                    {
                        string sub = "", subject = "";
                        DateTime dtSMORequest = Convert.ToDateTime(smodetail.RequestDateNotification);
                        if (smodetail.SMOSubmittedBy != null)
                        {
                            sub = Resources.Wording.Message_MesssageNotification_SMOOWNEROPEN;
                        }
                        if (!string.IsNullOrEmpty(sub))
                        {
                            int smoBy = Convert.ToInt32(smodetail.SMOSubmittedBy);
                            string username = UnitOfWork.LoginRepository.GetSingleTracking(l => l.UserId == smoBy).UserName;
                            string usrRole = GetUserRoleByUserName(Encryption.Decrypt(username));

                            subject = sub.Replace("()", "(" + smodetail.SMOId + ")");
                            subject = subject + " " + usrRole;

                            newMessages.Add(new MessageNotificationViewModel(smodetail.Id.ToString(), subject, "smo", dtSMORequest, smodetail.IsSMOPaymentDone));
                        }
                    }
                }
                #endregion

                #endregion
            }

            #region SharePetInformation
            if (Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()))
            {
                Expression<Func<SharePetInformation, object>> parames1 = v => v.User1;
                Expression<Func<SharePetInformation, object>> parames2 = v => v.User;
                Expression<Func<SharePetInformation, object>>[] paramesArray = new Expression<Func<SharePetInformation, object>>[] { parames1, parames2 };
                var shareCategoryType = UnitOfWork.SharePetInformationRepository.GetAll(r => r.IsRead == true, navigationProperties: paramesArray).Where(x => x.ContactId == userId && x.UserId != userId).Select(x => new ADOPets.Web.ViewModels.NewsFeed.IndexViewModel(x)).ToList();

                foreach (var item in shareCategoryType)
                {
                    string SharedInfo = "";
                    if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareIDInformation)
                        SharedInfo = Resources.Wording.ShareCategoryType_IdInformation;
                    if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareMedicalRecords)
                        SharedInfo = Resources.Wording.ShareCategoryType_MedicalRecords;
                    if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareVeterinarians)
                        SharedInfo = Resources.Wording.ShareCategoryType_Veterinarians;
                    if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareContacts)
                        SharedInfo = Resources.Wording.ShareCategoryType_Contacts;
                    if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareGallery)
                        SharedInfo = Resources.Wording.ShareCategoryType_Gallery;
                    string PetName = UnitOfWork.PetRepository.GetAll(x => x.Id == item.PetId).Select(x => x.Name).FirstOrDefault();
                    string SharedInfomessage1 = string.Format(Resources.Wording.NewsFeed_Notification_Message, item.FirstName + " " + item.LastName, SharedInfo, PetName);
                    newMessages.Add(new MessageNotificationViewModel(item.PetId.ToString(), SharedInfomessage1, item.FirstName, item.ShareCategoryTypeId, "shareinfo", TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(item.CreationDate)));
                }

                //share pet info community
                Expression<Func<SharePetInfoCommunity, object>> prm1 = v => v.User1;
                Expression<Func<SharePetInfoCommunity, object>> prm2 = v => v.User;
                Expression<Func<SharePetInfoCommunity, object>>[] prms = new Expression<Func<SharePetInfoCommunity, object>>[] { prm1, prm2 };
                var shareCategoryTypeComm = UnitOfWork.SharePetInfoCommunityRepository.GetAll(r => r.IsRead == true, navigationProperties: prms).Where(x => x.ContactId == userId && x.UserId != userId).Select(x => new ADOPets.Web.ViewModels.NewsFeed.IndexViewModel(x)).ToList();

                foreach (var item in shareCategoryTypeComm)
                {
                    string SharedInfo = "";
                    if (item.ShareCategoryTypeId == ShareCategoryTypeEnum.ShareGallery)
                        SharedInfo = Resources.Wording.ShareCategoryType_Gallery;
                    string PetName = UnitOfWork.PetRepository.GetAll(x => x.Id == item.PetId).Select(x => x.Name).FirstOrDefault();
                    string SharedInfomessage1 = string.Format(Resources.Wording.NewsFeed_Notification_Message, item.FirstName + " " + item.LastName, SharedInfo, PetName);
                    newMessages.Add(new MessageNotificationViewModel(item.PetId.ToString(), SharedInfomessage1, item.FirstName, item.ShareCategoryTypeId, "shareinfo", TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(item.CreationDate)));
                }
            }
            #endregion

            return newMessages;
        }

        private string GetUserRoleByUserName(string userName)
        {
            string result = "";
            string userrole = Roles.GetRolesForUser(userName).First();

            if (userrole == UserTypeEnum.VeterinarianAdo.ToString())
            {
                result = Resources.Enums.UserTypeEnum_VeterinarianAdo;// "Veterinarian Director";
            }
            else if (userrole == UserTypeEnum.VeterinarianExpert.ToString())
            {
                result = Resources.Enums.UserTypeEnum_VeterinarianExpert;// "Veterinarian Expert";
            }
            else if (userrole == UserTypeEnum.Admin.ToString())
            {
                result = Resources.Enums.UserTypeEnum_Admin;
            }
            else if (userrole == UserTypeEnum.VeterinarianLight.ToString())
            {
                result = Resources.Enums.UserTypeEnum_VeterinarianLight;
            }

            return result;
        }

    }
}
