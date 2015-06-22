using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using Model;
using System.Globalization;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class ConfirmationViewModel1
    {
        public ConfirmationViewModel1()
        {

        }

        public ConfirmationViewModel1(AddSetupViewModel usersetupInfo, AddBillingViewModel billingInfo)
        {
            CreditCardType = billingInfo.CreditCardType;
            CreditCardNumber = billingInfo.CreditCardNumber;
            ExpirationDate = billingInfo.ExpirationDate;
            CVV = billingInfo.CVV;

            Symptoms1 = usersetupInfo.Symptoms1;
            Symptoms2 = usersetupInfo.Symptoms2;
            Symptoms3 = usersetupInfo.Symptoms3;
            VetID = usersetupInfo.VetID;
            PetID = usersetupInfo.PetId;
            ContactType = usersetupInfo.ContactType;
            UserId = usersetupInfo.UserId;
            ConsultationDate = Convert.ToDateTime(usersetupInfo.Date);
            ConsultationTime = Convert.ToDateTime(usersetupInfo.Time);
            EconsultationStatusId = EConsultationStatusEnum.Open;

            BillingAddress1 = billingInfo.Address1;
            BillingAddress2 = billingInfo.Address2;
            BillingZip = billingInfo.Zip;
            BillingCity = billingInfo.City;
            BillingState = billingInfo.BillingState;
            BillingCountry = billingInfo.BillingCountry;
            CalenderEDate = Convert.ToDateTime(usersetupInfo.Date.Value);
            CalenderEDate = CalenderEDate.AddHours(usersetupInfo.Time.Value.Hour);
            CalenderEDate = CalenderEDate.AddMinutes(usersetupInfo.Time.Value.Minute);
            CalenderEDate = CalenderEDate.AddSeconds(usersetupInfo.Time.Value.Second);
            Controllers.EconsultationController obj = new Controllers.EconsultationController();
            CalenderEDate = obj.GetEcTime(CalenderEDate, Convert.ToInt16(usersetupInfo.TimeZone), Convert.ToInt32(usersetupInfo.UserId));
        }

        public CreditCardTypeEnum? CreditCardType { get; set; }

        public int PetID { get; set; }

        public string CreditCardNumber { get; set; }

        public string ExpirationDate { get; set; }

        public int? CVV { get; set; }

        public string BillingAddress1 { get; set; }

        public string BillingAddress2 { get; set; }

        public string BillingCity { get; set; }

        public CountryEnum? BillingCountry { get; set; }

        public StateEnum? BillingState { get; set; }

        public string BillingZip { get; set; }


        public string Plan { get; set; }

        public string Email { get; set; }

        public decimal Price { get; set; }


        public string FirstName { get; set; }


        public string MiddleName { get; set; }


        public string LastName { get; set; }

        public EConsultationStatusEnum EconsultationStatusId { get; set; }

        public PaymentTypeEnum PaymentTypeId { get; set; }

        public int UserId { get; set; }

        public AddSetupViewModel objSetup { get; set; }
        public AddBillingViewModel objBilling { get; set; }


        public string PetName { get; set; }

        public CountryEnum Country { get; set; }

        public DateTime ConsultationDate { get; set; }

        public DateTime ConsultationTime { get; set; }


        public EConsultationContactTypeEnum? ContactType { get; set; }

        public string Phone { get; set; }


        public string PetCondition { get; set; }

        public string Symptoms1 { get; set; }

        public string Symptoms2 { get; set; }

        public string Symptoms3 { get; set; }

        public int VetID { get; set; }

        public decimal PaymentCharged { get; set; }

        public string PaymentTransferNum { get; set; }

        public DateTime PaymentTransferDateTime { get; set; }

        public string PaymentTransferMsg { get; set; }

        public string PaymentRefNum { get; set; }

        public bool ConfirmMedicalRecords { get; set; }

        public bool TermsAndConditions { get; set; }

        public bool SendInfoToCurrentVet { get; set; }

        public int? CalenderId { get; set; }

        public int Id { get; set; }

        public DateTime CalenderEDate { get; set; }

        public Model.EConsultation Map()
        {
            var eConsul = new Model.EConsultation
            {
                TitleConsultation = objSetup.PetCondition,
                DateConsultation = objSetup.Date,
                TypeConsultation = 0,
                UserId = objSetup.UserId,
                VetId = objSetup.VetID,
                PetId = objSetup.PetId,
                EconsultationStatusId = EConsultationStatusEnum.Open,
                BDateConsultation = objSetup.Date,
                BTimeConsultation = Convert.ToDateTime(objSetup.Time),
                RDVDate = objSetup.Date,
                RDVDateTime = Convert.ToDateTime(objSetup.Time),
                RequestedTimeRange = null,
                VetTimezoneID = objSetup.TimeZone,
                EConsultationContactTypeId = objSetup.ContactType,
                EConsultationContactValue = (objSetup.ContactType.HasValue) ? ((objSetup.ContactType.Value == EConsultationContactTypeEnum.Email) ? objSetup.Email : objSetup.Phone) : null,
                CountryId = objSetup.Country,
                PaymentType = PaymentTypeEnum.SaleTransaction,
                Symptoms1 = objSetup.Symptoms1,
                Symptoms2 = objSetup.Symptoms2,
                Symptoms3 = objSetup.Symptoms3,
                UnitPrice = 25,
                Shared = 1,
                PurchaseFlag = "1",
                PaymentFlag = 1,
                Survey = "N",
                PaymentCharged = PaymentCharged,
                PaymentRefNum = PaymentRefNum,
                PaymentTransferDateTime = DateTime.Now,
                PaymentTransferMsg = PaymentTransferMsg,
                PaymentTransferNum = PaymentTransferNum,
                VetNotificationFlag = 1,
                VetNotificationDateTime = DateTime.Now,
                ID = Id,
                CalenderId = CalenderId,
                IsRead = true
            };
            return eConsul;
        }

        public Model.EConsultation MapUpdate(EConsultation eConsul)
        {
            eConsul.TitleConsultation = objSetup.PetCondition;
            eConsul.DateConsultation = objSetup.Date;
            eConsul.TypeConsultation = 0;
            eConsul.UserId = objSetup.UserId;
            eConsul.VetId = objSetup.VetID;
            eConsul.PetId = objSetup.PetId;
            eConsul.EconsultationStatusId = EConsultationStatusEnum.Open;
            eConsul.BDateConsultation = objSetup.Date;
            eConsul.BTimeConsultation = Convert.ToDateTime(objSetup.Time);
            eConsul.RDVDate = objSetup.Date;
            eConsul.RDVDateTime = Convert.ToDateTime(objSetup.Time);
            eConsul.RequestedTimeRange = null;
            eConsul.VetTimezoneID = objSetup.TimeZone;
            eConsul.EConsultationContactTypeId = objSetup.ContactType;
            eConsul.EConsultationContactValue = (objSetup.ContactType.HasValue) ? ((objSetup.ContactType.Value == EConsultationContactTypeEnum.Email) ? objSetup.Email : objSetup.Phone) : null;
            eConsul.CountryId = objSetup.Country;
            eConsul.PaymentType = PaymentTypeEnum.SaleTransaction;
            eConsul.Symptoms1 = objSetup.Symptoms1;
            eConsul.Symptoms2 = objSetup.Symptoms2;
            eConsul.Symptoms3 = objSetup.Symptoms3;
            eConsul.UnitPrice = 25;
            eConsul.Shared = 1;
            eConsul.PurchaseFlag = "1";
            eConsul.PaymentFlag = 1;
            eConsul.Survey = "N";
            eConsul.PaymentCharged = PaymentCharged;
            eConsul.PaymentRefNum = PaymentRefNum;
            eConsul.PaymentTransferDateTime = DateTime.Now;
            eConsul.PaymentTransferMsg = PaymentTransferMsg;
            eConsul.PaymentTransferNum = PaymentTransferNum;
            eConsul.VetNotificationFlag = 1;
            eConsul.VetNotificationDateTime = DateTime.Now;
            eConsul.ID = Id;
            eConsul.CalenderId = CalenderId;
            eConsul.IsRead = true;

            return eConsul;
        }

        public Model.EconsultationRoom MapEcRoom()
        {
            var eConsulRoom = new Model.EconsultationRoom
            {
                Status = Convert.ToInt16(EConsultationStatusEnum.Open),
                EConsultationId = Id
            };
            return eConsulRoom;
        }

        public Model.Calendar MapCalendar()
        {
            var eConsulCalender = new Model.Calendar
            {
                Reason = new EncryptedText(objSetup.PetCondition),
                Date = CalenderEDate,
                Physician = new EncryptedText(objSetup.VetName),
                Comment = new EncryptedText("EC ID" + Id),
                SendNotificationMail = true,
                UserId = UserId,
                PetId = PetID
            };
            return eConsulCalender;
        }
    }
}