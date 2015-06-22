﻿using System;
using System.IO;
using System.Net.Mail;
using System.Threading;

namespace EmailSender
{
    public class MailSenderHelper
    {
        #region Constructor

        public MailSenderHelper(string logoPath, string signaturePath, string mraFormPdfPath, string domain = "")
        {
            LogoPath = logoPath;
            SignaturePath = signaturePath;
            MraFormPdfPath = mraFormPdfPath;
            Domain = domain;
            WebConfigHelper.Domain = domain;
        }

        public MailSenderHelper(string logoPath, string signaturePath)
        {
            LogoPath = logoPath;
            SignaturePath = signaturePath;
        }

        #endregion

        #region Properties

        public string LogoPath { get; set; }

        public string SignaturePath { get; set; }

        public string MraFormPdfPath { get; set; }

        public string Domain { get; set; }

        #endregion

        #region HSBC

        public void SendHsbcWelcomeMail(string email, string firstName, string lastName, string planName, string promoCode)
        {
            var subject = Resources.HsbcMails.WelcomeSubject;
            var body = string.Format(Resources.HsbcMails.WelcomeBody, Constants.LogoForEmailCid, firstName, lastName, Constants.LogoSignatureForEmailCid,
                WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendHsbcAccountActivationMail(string email, string firstName, string lastName, string username, string password)
        {
            var subject = Resources.HsbcMails.AccountActivationSubject;
            var body = string.Format(Resources.HsbcMails.AccountActivationBody, Constants.LogoForEmailCid, firstName, lastName, username, password,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendHsbcToSupportSubscriptionMail(string userFirstName, string userLastName, string userEmail, string promocode, string plan, string amount, string referralSource)
        {
            var subject = Resources.HsbcMails.SupportUserSubscriptionByA4PMailSubject;
            var body = string.Format(Resources.HsbcMails.SupportUserSubscriptionBody, Constants.LogoForEmailCid, subject, userFirstName, userLastName, userEmail, promocode, plan, amount, referralSource,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }

        #endregion

        #region Notfication Mails (Insurance, Medication, Calendar)

        public bool SendMedicationNotification(string email, string firstName, string lastName, string medicationName, string status, DateTime visitDate, int duration,
                    string howOften, string dosage, string veterinarian)
        {
            var subject = Resources.NotificationMails.MedicationSubject;

            var messageText = Resources.NotificationMails.MedicationBody;
            if (Domain == "French")
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
                {
                    messageText = Resources.NotificationMails.MedicationBody_langFR;
                    subject = Resources.NotificationMails.MedicationSubject_langFR;
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
                {
                    messageText = Resources.NotificationMails.MedicationBody_langFR;
                    subject = Resources.NotificationMails.MedicationSubject_langFR;
                }
            }

            var body = string.Format(messageText, Constants.LogoForEmailCid, firstName, lastName, medicationName, status, visitDate.ToString("d"),
                duration, howOften, dosage, veterinarian, Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            return SendMailMessage(email, null, null, subject, body, null);
        }

        public bool SendInsuranceNotification(string email, string firstName, string lastName, string insuranceName, string insuranceNumber, DateTime startDate,
            DateTime endDate, string phone, string comments)
        {
            var subject = Resources.NotificationMails.InsuranceSubject;

            var messageText = Resources.NotificationMails.InsuranceBody;
            if (Domain == "French")
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
                {
                    messageText = Resources.NotificationMails.InsuranceBody_langFR;
                    subject = Resources.NotificationMails.InsuranceSubject_langFR;
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
                {
                    messageText = Resources.NotificationMails.InsuranceBody_langFR;
                    subject = Resources.NotificationMails.InsuranceSubject_langFR;
                }
            }

            var body = string.Format(messageText, Constants.LogoForEmailCid, firstName, lastName, insuranceName, insuranceNumber,
                    startDate.ToString("d"), endDate.ToString("d"), phone, comments, Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            return SendMailMessage(email, null, null, subject, body, null);
        }

        public bool SendReminderNotification(string email, string firstName, string lastName, string reason, DateTime date, string veterinarian, string comments)
        {
            var subject = Resources.NotificationMails.ReminderSubject;

            var messageText = Resources.NotificationMails.ReminderBody;
            if (Domain == "French")
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
                {
                    messageText = Resources.NotificationMails.ReminderBody_langFR;
                    subject = Resources.NotificationMails.ReminderSubject_langFR;
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
                {
                    messageText = Resources.NotificationMails.ReminderBody_langFR;
                    subject = Resources.NotificationMails.ReminderSubject_langFR;
                }
            }

            var body = string.Format(messageText, Constants.LogoForEmailCid, firstName, lastName, reason,
                date.ToString("D"), date.ToString("hh:mm tt"), veterinarian, comments, Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            return SendMailMessage(email, null, null, subject, body, null);
        }

        public bool SendHsbcSubscriptionExpirationAlert(string email, string firstName, string lastName)
        {
            var subject = Resources.HsbcMails.SubscriptionExpirationAlertSubject;
            var body = string.Format(Resources.HsbcMails.SubscriptionExpirationAlertBody, Constants.LogoForEmailCid, firstName, lastName,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            return SendMailMessage(email, null, null, subject, body, null);
        }

        #endregion

        #region AdoPets.Web Mails

        public void SendWelcomeMailFreeUser(string email, string firstName, string lastName, string planName, string promoCode)
        {
            var subject = Resources.AdoPets_Web_Mails.WelcomeSubjectFreeUser;
            var messageText = Resources.AdoPets_Web_Mails.WelcomeBodyFreeUser;
            //if (Domain == "French")
            //{
            //    if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
            //    {
            //        messageText = Resources.AdoPets_Web_Mails.WelcomeBody_langFR;
            //        subject = Resources.AdoPets_Web_Mails.WelcomeSubject_langFR;
            //    }
            //}
            //else
            //{
            //    if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
            //    {
            //        messageText = Resources.AdoPets_Web_Mails.WelcomeBody_langFR;
            //        subject = Resources.AdoPets_Web_Mails.WelcomeSubject_langFR;
            //    }
            //}

            var body = string.Format(messageText, Constants.LogoForEmailCid, subject, firstName, lastName, Constants.LogoSignatureForEmailCid,
                WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            body = string.Format(messageText, Constants.LogoForEmailCid, subject, firstName, lastName, Constants.LogoSignatureForEmailCid,
              WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendWelcomeMail(string email, string firstName, string lastName, string planName, string promoCode)
        {
            var subject = Resources.AdoPets_Web_Mails.WelcomeSubject;
            var messageText = Resources.AdoPets_Web_Mails.WelcomeBody;
            if (Domain == "French")
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
                {
                    messageText = Resources.AdoPets_Web_Mails.WelcomeBody_langFR;
                    subject = Resources.AdoPets_Web_Mails.WelcomeSubject_langFR;
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
                {
                    messageText = Resources.AdoPets_Web_Mails.WelcomeBody_langFR;
                    subject = Resources.AdoPets_Web_Mails.WelcomeSubject_langFR;
                }
            }

            var body = string.Format(messageText, Constants.LogoForEmailCid, subject, firstName, lastName, Constants.LogoSignatureForEmailCid,
                WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            var attachment = new Attachment(new FileStream(MraFormPdfPath, FileMode.Open), Resources.AdoPets_Web_Mails.WelcomePdfName, "application/pdf");
            SendMailMessage(email, null, null, subject, body, attachment);
            attachment.Dispose();
        }

        public void SendSubscriptionMail(string email, string firstName, string lastName, DateTime userDateTime, DateTime expirationDate, string planName = null, string promoCode = null, string paymentOrderId = null,
            string billingAddress1 = null, string billingAddress2 = null, string billingCity = null, string billingState = null, string billingZip = null, string billingCountry = null, string userEmail = null)
        {
            var subject = Resources.AdoPets_Web_Mails.SubscriptionSubject;
            promoCode = promoCode ?? "N/A";
            var address = string.Format("{0} {1}", billingAddress1, billingAddress2);
            var city = string.Format("{0} {1} {2}", billingCity, billingState, billingZip);

            var messageText = Resources.AdoPets_Web_Mails.SubscriptionBody;
            if (Domain == "French")
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
                {
                    messageText = Resources.AdoPets_Web_Mails.SubscriptionBody_langFR;
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
                {
                    messageText = Resources.AdoPets_Web_Mails.SubscriptionBody_langFR;
                }
            }

            var body = string.Format(messageText, Constants.LogoForEmailCid, firstName, lastName,
                  DateTime.Now.ToString("D"), planName, promoCode, userDateTime.ToString("f"), paymentOrderId, address, city, expirationDate.ToString("D"), billingCountry, userEmail,
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendMemberEconsultSubscription(string email, string firstName, string lastName, string paymentOrderId = null, string Price = null, string ECID = null,
         string billingAddress1 = null, string billingAddress2 = null, string billingCity = null, string billingState = null, string billingZip = null, string billingCountry = null, string userEmail = null)
        {

            var subject = Resources.AdoPets_Web_Mails.MemberEconsultSubscriptionSubject;
            var address = string.Format("{0} {1}", billingAddress1, billingAddress2);
            var city = string.Format("{0} {1} {2}", billingCity, billingState, billingZip);

            var body = string.Format(Resources.AdoPets_Web_Mails.SendMemberEconsultSubscriptionBody, Constants.LogoForEmailCid, subject, firstName, lastName, DateTime.Now.ToString("D"), paymentOrderId, Price, ECID,
                       address, city, billingCountry, userEmail, Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(email, null, null, subject, body, null);

        }

        public void SendToVetExpertEconsultation(string email, string vetfirstName, string vetlastName, string userfirstName, string userlastName, string userEmail, string petName, string petType, DateTime consultationDate, DateTime consultationTime, int econsultId, string petCondition, string UserId)
        {
            var subject = Resources.AdoPets_Web_Mails.EconsultationAssinedVetSubject;
            var dateTime = string.Format("{0} {1}", consultationDate.ToLongDateString(), consultationTime.ToShortTimeString());
            var body = string.Format(Resources.AdoPets_Web_Mails.EconsultationAssinedVetBody, Constants.LogoForEmailCid, subject, vetfirstName, vetlastName, econsultId, petName,
                petType, petCondition, dateTime, userfirstName, userlastName, userEmail, UserId,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendToUserEconsultationSchedule(string email, string vetfirstName, string vetlastName, string userfirstName, string userlastName, string userEmail, string petName, string petType, DateTime consultationDate, DateTime consultationTime, int econsultId, string PetCondition)
        {
            var subject = Resources.AdoPets_Web_Mails.EconsultationScheduleConfirmationSubject;
            var dateTime = string.Format("{0} {1}", consultationDate.ToLongDateString(), consultationTime.ToShortTimeString());
            var body = string.Format(Resources.AdoPets_Web_Mails.EconsultationScheduleConfirm, Constants.LogoForEmailCid, subject, vetfirstName, vetlastName, "EC" + econsultId, petName, petType, PetCondition, userfirstName, userlastName, consultationDate.ToShortDateString(), consultationTime.ToShortTimeString(),
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendToVetEconsultationProposedDate(string email, string vetfirstName = null, string vetlastName = null, string userfirstName = null, string userlastName = null,
            string userEmail = null, string petName = null, string petType = null, string consultationDate = null, string consultationTime = null, int econsultId = 1, string PetCondition = null)
        {
            var subject = Resources.AdoPets_Web_Mails.EconsultationUserProposedTimeSubject;
            //var dateTime = string.Format("{0} {1}", consultationDate.ToLongDateString(), consultationTime.ToShortTimeString());
            var body = string.Format(Resources.AdoPets_Web_Mails.EconsultationUserProposedTime, Constants.LogoForEmailCid, subject, vetfirstName, vetlastName, userfirstName + ' ' + userlastName, "EC" + econsultId, petName, petType, PetCondition, consultationDate, consultationTime,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendToUserEconsultationReportReady(string email, string userfirstName, string userlastName, string userEmail, string petName, string petType, DateTime consultationDate, DateTime consultationTime, int econsultId, string Ecstatus)
        {
            var subject = Resources.AdoPets_Web_Mails.EconsultationReportReadySubject;
            var dateTime = string.Format("{0} {1}", consultationDate.ToLongDateString(), consultationTime.ToShortTimeString());
            var EcIDString = "EC" + econsultId;
            var body = string.Format(Resources.AdoPets_Web_Mails.EconsultationReportReady, Constants.LogoForEmailCid, subject, userfirstName, userlastName, EcIDString, Ecstatus,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendAccountActivationMail(string email, string firstName, string lastName, string username, string password)
        {
            var subject = Resources.AdoPets_Web_Mails.AccountActivationSubject;

            var messageText = Resources.AdoPets_Web_Mails.AccountActivationBody;
            if (Domain == "French")
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
                {
                    messageText = Resources.AdoPets_Web_Mails.AccountActivationBody_langFR;
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
                {
                    messageText = Resources.AdoPets_Web_Mails.AccountActivationBody_langFR;
                }
            }

            var body = string.Format(messageText, Constants.LogoForEmailCid, subject, firstName, lastName, username, password,
                    Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendInviteMailFriend(string recipientEmail, string recipientFirstName, string recipientLastName, string senderFirstName, string senderLastName)
        {
            var subject = string.Format("{0} {1} {2}", senderFirstName, senderLastName, Resources.AdoPets_Web_Mails.SendInviteMailFriendSubject);

            var messageText = "";
            messageText = Resources.AdoPets_Web_Mails.SendInviteMailFriendBody;
            if (Domain == "French")
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
                {
                    messageText = Resources.AdoPets_Web_Mails.SendInviteMailFriendBody_langFR;
                    subject = string.Format("{0} {1} {2}", senderFirstName, senderLastName, Resources.AdoPets_Web_Mails.SendInviteMailFriendSubject_langFR);
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
                {
                    messageText = Resources.AdoPets_Web_Mails.SendInviteMailFriendBody_langFR;
                    subject = string.Format("{0} {1} {2}", senderFirstName, senderLastName, Resources.AdoPets_Web_Mails.SendInviteMailFriendSubject_langFR);
                }
            }

            var body = string.Format(messageText, Constants.LogoForEmailCid, recipientFirstName, recipientLastName,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(recipientEmail, null, null, subject, body, null);
        }

        public void SendInviteMailVeterinarian(string recipientEmail, string recipientFirstName, string recipientLastName, string senderFirstName, string senderLastName)
        {
            var subject = string.Format("{0} {1} {2}", senderFirstName, senderLastName, Resources.AdoPets_Web_Mails.SendInviteMailVeterinarianSubject);

            var messageText = "";

            messageText = Resources.AdoPets_Web_Mails.SendInviteMailVeterinarianBody;
            if (Domain == "French")
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
                {
                    messageText = Resources.AdoPets_Web_Mails.SendInviteMailVeterinarianBody_langFR;
                    subject = string.Format("{0} {1} {2}", senderFirstName, senderLastName, Resources.AdoPets_Web_Mails.SendInviteMailVeterinarianSubject_langFR);
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
                {
                    messageText = Resources.AdoPets_Web_Mails.SendInviteMailVeterinarianBody_langFR;
                    subject = string.Format("{0} {1} {2}", senderFirstName, senderLastName, Resources.AdoPets_Web_Mails.SendInviteMailVeterinarianSubject_langFR);
                }
            }

            var body = string.Format(messageText, Constants.LogoForEmailCid, recipientFirstName, recipientLastName,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(recipientEmail, null, null, subject, body, null);
        }

        public void SendNewMessageNotificationMail(string recipientEmail, string recipientFirstName, string recipientLastName, string senderFirstName, string senderLastName, int senderUserId)
        {
            var subject = Resources.AdoPets_Web_Mails.NewMessageNotificationSubject;

            var messageText = Resources.AdoPets_Web_Mails.NewMessageNotificationBody;
            if (Domain == "French")
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
                {
                    messageText = Resources.AdoPets_Web_Mails.NewMessageNotificationBody_langFR;
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
                {
                    messageText = Resources.AdoPets_Web_Mails.NewMessageNotificationBody_langFR;
                }
            }

            var body = string.Format(messageText, Constants.LogoForEmailCid, subject, recipientFirstName, recipientLastName, senderFirstName, senderLastName, senderUserId,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(recipientEmail, null, null, subject, body, null);
        }

        public void SendForgotPasswordMail(string email, string firstName, string lastName, string userName, string newPassword)
        {
            var subject = Resources.AdoPets_Web_Mails.ForgotPasswordSubject;
            var messageText = Resources.AdoPets_Web_Mails.ForgotPasswordBody;
            if (Domain == "French")
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
                {
                    messageText = Resources.AdoPets_Web_Mails.ForgotPasswordBody_langFR;
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
                {
                    messageText = Resources.AdoPets_Web_Mails.ForgotPasswordBody_langFR;
                }
            }

            var body = string.Format(messageText, Constants.LogoForEmailCid, subject, firstName, lastName, userName, newPassword, Constants.LogoSignatureForEmailCid,
                WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendAdminUpdatesLoginDetailsMail(string email, string firstName, string lastName, string userName, string newPassword)
        {
            var subject = Resources.AdoPets_Web_Mails.AdminAddUserSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.AdminAddUserWelcomeBody, Constants.LogoForEmailCid, subject, firstName, lastName, userName, newPassword, Constants.LogoSignatureForEmailCid,
                WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendSMONotificationToVetDirectorMail(string recipientEmail, string recipientFirstName, string recipientLastName, string smoTitle)
        {
            var subject = Resources.AdoPets_Web_Mails.NewSMONotificationToVetDirectorSubject;

            var body = string.Format(Resources.AdoPets_Web_Mails.NewSMONotificationToVetDirectorBody, Constants.LogoForEmailCid, subject, recipientFirstName, recipientLastName, smoTitle,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(recipientEmail, null, null, subject, body, null);
        }

        public void SendSMODetailsToOwnerMail(string recipientEmail, string recipientFirstName, string recipientLastName, string smoTitle, string petName)
        {
            var subject = Resources.AdoPets_Web_Mails.NewSMODetailsToOwnerSubject;

            var body = string.Format(Resources.AdoPets_Web_Mails.NewSMODetailsToOwnerBody, Constants.LogoForEmailCid, subject, recipientFirstName, recipientLastName, smoTitle,
               petName, Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(recipientEmail, null, null, subject, body, null);
        }

        public void SendNewSMORequestMail(string email, string firstName, string lastName, string paymentOrderId = null, string Price = null, string SMOID = null,
            string billingAddress1 = null, string billingAddress2 = null, string billingCity = null, string billingState = null, string billingZip = null, string billingCountry = null, string userEmail = null)
        {
            var subject = Resources.AdoPets_Web_Mails.NewSMORequestSubject;
            var address = string.Format("{0} {1}", billingAddress1, billingAddress2);
            var city = string.Format("{0} {1} {2}", billingCity, billingState, billingZip);

            var body = string.Format(Resources.AdoPets_Web_Mails.NewSMORequestBody, Constants.LogoForEmailCid, subject, firstName, lastName,
                  DateTime.Now.ToString("D"), paymentOrderId, Price, SMOID, address, city, billingCountry, userEmail,
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendToVetDirectorNewSMORequestMail(string email, string firstName, string lastName, string PetName = null, string PetType = null, string SMOID = null,
           string Title = null, string MemberID = null, string userEmail = null)
        {
            var subject = Resources.AdoPets_Web_Mails.SendToVDNewSMORequestSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SendToVDNewSMORequestBody, Constants.LogoForEmailCid, subject, SMOID, PetName, PetType, Title, DateTime.Now.ToString("D"), firstName, lastName, userEmail, MemberID,
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendSMOAssignedVetMail(string email, string VetfirstName, string VetlastName, string firstName, string lastName, string PetName = null, string PetType = null, string SMOID = null,
          string Title = null, string MemberID = null, string userEmail = null)
        {
            var subject = Resources.AdoPets_Web_Mails.SMOAssignedVetSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SMOAssignedVetBody, Constants.LogoForEmailCid, subject, VetfirstName, VetlastName, SMOID, PetName, PetType, Title, DateTime.Now.ToString("D"), firstName, lastName, userEmail, MemberID,
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendSMORequestPaymentPendingMail(string userRole, string email, string firstName, string lastName, string SMOID = null)
        {
            var subject = Resources.AdoPets_Web_Mails.SMORequestPaymentPendingSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SMORequestPaymentPendingBody, Constants.LogoForEmailCid, subject, firstName, lastName, userRole, SMOID,
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendSMORequestInProcessMail(string email, string firstName, string lastName, string SMOID = null)
        {
            var subject = Resources.AdoPets_Web_Mails.SMOInProcessSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SMOInProcessBody, Constants.LogoForEmailCid, subject, firstName, lastName, SMOID,
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendSMOExpertResponseMail(string email, string vdName, string expertName, string PetName = null, string PetType = null, string SMOID = null,
         string Title = null)
        {
            var subject = Resources.AdoPets_Web_Mails.SMOResponseFromExpertSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SMOResponseFromExpertBody, Constants.LogoForEmailCid, subject, vdName, expertName, SMOID, PetName, PetType, Title,
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendSMOReportReadyMail(string email, string firstName, string lastName, string SMOID = null, string Status = null)
        {
            var subject = Resources.AdoPets_Web_Mails.SMOReportReadySubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SMOReportReadyBody, Constants.LogoForEmailCid, subject, firstName, lastName, SMOID, Status,
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendMyPlanConfirmationMail(string email, string firstName, string lastName, DateTime StartDate, DateTime expirationDate, string planName = null, string promoCode = null, string AdditionalPets = null, string Price = null, string paymentOrderId = null,
            string billingAddress1 = null, string billingAddress2 = null, string billingCity = null, string billingState = null, string billingZip = null, string billingCountry = null, string userEmail = null)
        {
            var subject = Resources.AdoPets_Web_Mails.MyPlanConfirmationSubject;
            promoCode = promoCode ?? "N/A";
            var address = string.Format("{0} {1}", billingAddress1, billingAddress2);
            var city = string.Format("{0} {1} {2}", billingCity, billingState, billingZip);

            var messageText = Resources.AdoPets_Web_Mails.MyPlanConfirmationBody;
            if (Domain == "French")
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
                {
                    messageText = Resources.AdoPets_Web_Mails.MyPlanConfirmationBody_langFR;
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
                {
                    messageText = Resources.AdoPets_Web_Mails.MyPlanConfirmationBody_langFR;
                }
            }

            var body = string.Format(messageText, Constants.LogoForEmailCid, firstName, lastName,
                  DateTime.Now.ToString("D"), paymentOrderId, planName, promoCode, AdditionalPets, Price, StartDate.ToString("D"), expirationDate.ToString("D"), address, city, billingCountry, userEmail,
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendAdminUpdatesPlanMail(string strSubject, string strPlanInfo, string email, string firstName, string lastName, string CreatedUser, DateTime StartDate, DateTime expirationDate, string planName = null, string promoCode = null, string MaxPets = null, string Price = null)
        {
            var subject = string.Format(Resources.AdoPets_Web_Mails.AdminUpdatePlanPendingPaymentSubject, strSubject);
            promoCode = promoCode ?? "N/A";
            var body = string.Format(Resources.AdoPets_Web_Mails.AdminUpdatePlanPendingPaymentBody, Constants.LogoForEmailCid, subject, firstName, lastName,
                  CreatedUser, strPlanInfo.ToLower(), planName, promoCode, MaxPets, Price, StartDate.ToString("D"), expirationDate.ToString("D"),
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendWelcomeMailAdmin(string email, string firstName, string lastName, string username, string password, string planName, string promoCode, bool flagUserTypeOwner)
        {
            var subject = Resources.AdoPets_Web_Mails.AdminAddUserSubject;
            var body = "";
            if (flagUserTypeOwner)
            {
                body = string.Format(Resources.AdoPets_Web_Mails.AdminAddOwnerWelcomeBody, Constants.LogoForEmailCid, subject, firstName, lastName, username, password,
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            }
            else
            {
                body = string.Format(Resources.AdoPets_Web_Mails.AdminAddUserWelcomeBody, Constants.LogoForEmailCid, subject, firstName, lastName, username, password,
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            }

            var attachment = new Attachment(new FileStream(MraFormPdfPath, FileMode.Open), Resources.AdoPets_Web_Mails.WelcomePdfName, "application/pdf");
            SendMailMessage(email, null, null, subject, body, attachment);
            attachment.Dispose();
        }

        public void SendStatusChangeToOwner(string email, string firstName, string lastName, string userName, string newPassword)
        {
            var subject = Resources.AdoPets_Web_Mails.StatusChangedToActiveSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.StatusChangedToActiveBody, Constants.LogoForEmailCid, subject, firstName, lastName, userName, newPassword, Constants.LogoSignatureForEmailCid,
                WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        public void SendSharedPetInformationToContacts(string email, string SharedContactName, string petOwner, string userID)
        {
            var subject = Resources.AdoPets_Web_Mails.SharePetInformationSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SharePetInformationBody, Constants.LogoForEmailCid, subject, SharedContactName, petOwner, userID, Constants.LogoSignatureForEmailCid,
                WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }

        #region Mails to Support

        public void SendSupportUserEconsultSubscription(string firstName, string lastName, DateTime date, int userid, int EcoslId, string OrderId, string Product, string Amount, string PaymentResult)
        {
            var subject = Resources.AdoPets_Web_Mails.SupportUserEconsultSubscriptionSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SendSupportUserEconsultSubscriptionBody, Constants.LogoForEmailCid, subject, DateTime.Now.ToString("D"), OrderId, userid,
                firstName, lastName, Product, Amount, EcoslId, PaymentResult,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }

        public void SendSupportUserEconsultWithdraw(int ECID, string PetName, string PetType, string PetCondition, DateTime ConsultationDate, string firstName, string lastName, string email, int userid)
        {
            var subject = Resources.AdoPets_Web_Mails.SendToSupportEconsultationWithdrawSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SendToSupportEconsultationWithdrawBody, Constants.LogoForEmailCid, subject, ECID, PetName, PetType,
                PetCondition, ConsultationDate, DateTime.Now.ToString("D"), firstName, lastName, email, userid,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }

        public void SendSupportUserSubscription(string userFirstName, string userLastName, string userEmail, string promocode, string plan, string amount, string referralSource)
        {
            var subject = Resources.AdoPets_Web_Mails.SupportUserSubscriptionSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SupportUserSubscriptionBody, Constants.LogoForEmailCid, subject, userFirstName, userLastName, userEmail, promocode, plan, amount, referralSource,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }

        public void SendToSupportLoginRecoveryFailed(string userEmail)
        {
            var subject = Resources.AdoPets_Web_Mails.SendToSupportLoginRecoveryFailedSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SendToSupportLoginRecoveryFailedBody, Constants.LogoForEmailCid, subject, userEmail,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }

        public void SendToSupportLoginRecoverySuccess(string userEmail, int userId)
        {
            var subject = Resources.AdoPets_Web_Mails.SendToSupportLoginRecoverySuccessSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SendToSupportLoginRecoverySuccessBody, Constants.LogoForEmailCid, subject, userEmail, userId,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }

        public void SendToSupportSubscriptionMail(string userFirstName, string userLastName, string userEmail, string promocode, string plan, string amount, string referralSource)
        {
            var subject = Resources.AdoPets_Web_Mails.SupportUserSubscriptionByA4PMailSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SupportUserSubscriptionBody, Constants.LogoForEmailCid, subject, userFirstName, userLastName, userEmail, promocode, plan, amount, referralSource,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }

        public void SendInviteMailToSupportForFriend(string recipientFirstName, string recipientLastName, string recipientEmail, string senderFirstName, string senderLastName, int userId)
        {
            var subject = Resources.AdoPets_Web_Mails.SendInviteMailToSupportForFriendSubject;
            var messageText = Resources.AdoPets_Web_Mails.SendInviteMailToSupportForFriendBody;
            //if (Domain == "French")
            //{
            //    if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
            //    {
            //        messageText = Resources.AdoPets_Web_Mails.SendInviteMailToSupportForFriendBody_langFR;
            //        subject = Resources.AdoPets_Web_Mails.SendInviteMailToSupportForFriendSubject_langFR;
            //    }
            //}
            //else
            //{
            //    if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
            //    {
            //        messageText = Resources.AdoPets_Web_Mails.SendInviteMailToSupportForFriendBody_langFR;
            //        subject = Resources.AdoPets_Web_Mails.SendInviteMailToSupportForFriendSubject_langFR;
            //    }
            //}

            var body = string.Format(messageText, Constants.LogoForEmailCid, subject, recipientFirstName, recipientLastName, recipientEmail, senderFirstName, senderLastName, userId,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);

        }

        public void SendInviteMailToSupportForVet(string recipientFirstName, string recipientLastName, string recipientEmail, string senderFirstName, string senderLastName, int userId)
        {
            var subject = Resources.AdoPets_Web_Mails.SendInviteMailToSupportForVetSubject;
            var messageText = Resources.AdoPets_Web_Mails.SendInviteMailToSupportForVeteBody;
            //if (Domain == "French")
            //{
            //    if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "en-us")
            //    {
            //        messageText = Resources.AdoPets_Web_Mails.SendInviteMailToSupportForVeteBody_langFR;
            //        subject = Resources.AdoPets_Web_Mails.SendInviteMailToSupportForVetSubject_langFR;
            //    }
            //}
            //else
            //{
            //    if (Thread.CurrentThread.CurrentUICulture.ToString().ToLower() == "fr-fr")
            //    {
            //        messageText = Resources.AdoPets_Web_Mails.SendInviteMailToSupportForVeteBody_langFR;
            //        subject = Resources.AdoPets_Web_Mails.SendInviteMailToSupportForVetSubject_langFR;
            //    }
            //}

            var body = string.Format(messageText, Constants.LogoForEmailCid, subject, recipientFirstName, recipientLastName, recipientEmail, senderFirstName, senderLastName, userId,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }

        public void SendSMORequestToSupport(string userFirstName, string userLastName, string userEmail, string amount, string referralSource)
        {
            var subject = Resources.AdoPets_Web_Mails.SupportUserSubscriptionSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SupportUserSubscriptionBody, Constants.LogoForEmailCid, subject, userFirstName, userLastName, userEmail, null, null, amount, referralSource,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }

        public void SendSupportNewSMORequest(string userFirstName, string userLastName, string PaymentOrderID, string UserID, string Product, string amount, string SMOID, string result = null)
        {
            var subject = Resources.AdoPets_Web_Mails.SendToSupportNewSMORequestSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SendToSupportNewSMORequestBody, Constants.LogoForEmailCid, subject, DateTime.Now.ToString("D"), PaymentOrderID, UserID, userFirstName, userLastName, Product, SMOID, amount, result,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }

        public void SendToSupportPlanChange(string userFirstName, string userID, string userLastName, string transactionNo, string promocode, string plan, string additionalPets, DateTime startDate, DateTime endDate, string amount, string results)
        {
            var subject = Resources.AdoPets_Web_Mails.SendToSupportPlanChangedSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SendToSupportPlanChangedBody, Constants.LogoForEmailCid, subject, DateTime.Now.ToString("D"), transactionNo, userID, userFirstName, userLastName, plan, promocode, additionalPets, amount, startDate.ToString("D"), endDate.ToString("D"), results,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }

        /// <summary>
        /// send email to support on send email for newly registered users by a4p team
        /// </summary>   
        public void SendToSupportAccountActivationMail(string creationDate, string userId, string email, string usrFirstName, string usrLastName,
            string usrPlanName, string usrPromocode, string usrAdditionalPets, string usrStartDate, string usrExpirationDate)
        {
            var subject = Resources.AdoPets_Web_Mails.SupportAccountActivationSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.SupportAccountActivationBody, Constants.LogoForEmailCid, subject, creationDate, userId, usrFirstName, usrLastName, usrPlanName, usrPromocode, usrAdditionalPets,
                   usrStartDate, usrExpirationDate, Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }
        #endregion

        #endregion

        #region EC
        public void SendECRequestPaymentPendingMail(string userRole, string email, string firstName, string lastName, string ECID = null)
        {
            var subject = Resources.AdoPets_Web_Mails.ECRequestPaymentPendingSubject;
            var body = string.Format(Resources.AdoPets_Web_Mails.ECRequestPaymentPendingBody, Constants.LogoForEmailCid, subject, firstName, lastName, userRole, ECID,
                  Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);
            SendMailMessage(email, null, null, subject, body, null);
        }
        #endregion

        #region Support emails  For Import HSBC Users

        public void SendToSupportNoMailOrAttachmentMail(string message, string subject)
        {
            var body = string.Format(message, Constants.LogoForEmailCid, subject,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }

        public void SendToSupportFileFormatIncorrectMail(string message, string subject, string filePath, string filename)
        {
            var body = string.Format(message, Constants.LogoForEmailCid, subject, filename, filePath,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, WebConfigHelper.SupportMail1, subject, body, null);
        }
        public void SendToSupportInvalidFileExtensionMail(string message, string subject, string filename)
        {
            var body = string.Format(message, Constants.LogoForEmailCid, subject, filename,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }
        public void SendToSupportImportSuccessMail(string message, string subject, string filename, string filePath)
        {
            var body = string.Format(message, Constants.LogoForEmailCid, subject, filename, filePath,
                Constants.LogoSignatureForEmailCid, WebConfigHelper.PrivacyPolicyUrl, WebConfigHelper.TermsAndConditionsUrl);

            SendMailMessage(WebConfigHelper.SupportMail, null, null, subject, body, null);
        }
        #endregion

        private bool SendMailMessage(string to, string bcc, string cc, string subject, string body, Attachment attachment, bool isHtmlView = true)
        {
            try
            {
                var mail = new MailMessage();

                mail.To.Add(new MailAddress(to));

                if (!string.IsNullOrEmpty(bcc))
                {
                    mail.Bcc.Add(new MailAddress(bcc));
                }

                if (!string.IsNullOrEmpty(cc))
                {
                    string[] CCId = cc.Split(',');

                    foreach (string CCEmail in CCId)
                    {
                        mail.CC.Add(new MailAddress(CCEmail));
                    }
                }

                mail.Subject = subject;

                if (attachment != null)
                {
                    mail.Attachments.Add(attachment);
                }
             
                // // TODO :: Uncomment only for login and website and support urls

                //body = body.Replace("https://login.activ4pets.com", WebConfigHelper.LoginURL);
                //body = body.Replace("http://login.activ4pets.com", WebConfigHelper.LoginURL);
                //body = body.Replace("http://www.activ4pets.com", WebConfigHelper.WebsiteURL);
                //body = body.Replace("support@activ4pets.com", WebConfigHelper.SupportMail);

                //if (Domain == "India")
                //{
                //    body = body.Replace("1-888-51-ACTIV (22848)", WebConfigHelper.SupportPhoneIN);
                //    body = body.Replace("support@activ4pets.com", WebConfigHelper.SupportMail);
                //}

                //if (Domain == "French")
                //{
                //    mail.From = new MailAddress(WebConfigHelper.NoReplyEmailFr);

                //    // TODO :: Uncomment only for login and website and support urls

                //    body = body.Replace("support@activanimo.fr", WebConfigHelper.SupportMail);
                //    body = body.Replace("https://login.activanimo.fr", WebConfigHelper.LoginURL);
                //    body = body.Replace("http://login.activanimo.fr", WebConfigHelper.LoginURL);
                //    body = body.Replace("http://www.activanimo.fr", WebConfigHelper.WebsiteURL);
                //}
                //else if (Domain == "Portuguese")
                //{
                //    mail.From = new MailAddress(WebConfigHelper.NoReplyEmailPT);
                //}

                //mail.To.Add(new MailAddress("activeto <nmedhane@activdoctors.com>"));
                //mail.From = new MailAddress("nitu8921@gmail.com");

                if (isHtmlView)
                {
                    var htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                    var emailLogoImage = new LinkedResource(LogoPath);
                    emailLogoImage.ContentId = Constants.LogoForEmailId;
                    htmlView.LinkedResources.Add(emailLogoImage);


                    var emailLogoSignatureImage = new LinkedResource(SignaturePath);
                    emailLogoSignatureImage.ContentId = Constants.LogoSignatureForEmailId;
                    htmlView.LinkedResources.Add(emailLogoSignatureImage);

                    mail.AlternateViews.Add(htmlView);
                }

                mail.Body = body;
                mail.IsBodyHtml = isHtmlView;
                mail.Priority = MailPriority.Normal;

                var mSmtpClient = new SmtpClient();

                mSmtpClient.Send(mail);
            }
            catch (Exception)
            {
                //write in logs ex.Message
                return false;
            }

            return true;
        }
    }
}