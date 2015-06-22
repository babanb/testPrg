﻿using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using ExpressiveAnnotations.Attributes;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class DocumentUpload
    {
        public DocumentUpload() 
        {

        }
        public DocumentUpload(Model.EConsultation ec)
        {
            EcId = ec.ID;

            Controllers.EconsultationController EcControl = new Controllers.EconsultationController();
            ActionBeginDate = EcControl.GetExpertEcTime(Convert.ToDateTime(ec.ActionDateTimeBegin), Convert.ToInt16(ec.VetTimezoneID), Convert.ToInt16(ec.VetId));
            ActionEndDate = EcControl.GetExpertEcTime(Convert.ToDateTime(ec.ActionDateTimeEnd), Convert.ToInt16(ec.VetTimezoneID), Convert.ToInt16(ec.VetId));
            Durations = Math.Ceiling(Convert.ToDecimal((Convert.ToDateTime(ec.ActionDateTimeEnd) - Convert.ToDateTime(ec.ActionDateTimeBegin)).TotalMinutes)); //ec.Periods;
            EconsultationStatus = ec.EconsultationStatusId;
            PetCondition = ec.TitleConsultation;
            PetId = ec.PetId;
            PetOnwerId = ec.UserId;
        }

        public DateTime? ActionBeginDate { get; set; }
        public DateTime? ActionEndDate { get; set; }
        public DateTime? ConsultationDate { get; set; }

        [Required(ErrorMessageResourceName = "Econsultation_SubmitReport_DiagnosisRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Diagnosis { get; set; }

        [Required(ErrorMessageResourceName = "Econsultation_SubmitReport_TreatmentRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Treatment { get; set; }

        public string FilePath { get; set; }
        public decimal? Durations { get; set; }
        public List<EconsultDocumentViewModel> lstAttachment { get; set; }
        public EConsultationStatusEnum? EconsultationStatus { get; set; }
        public string PetCondition { get; set; }
        public int EcId { get; set; }
        public int? PetOnwerId { get; set; }
        public int Id { get; set; }
        public EConsultationStatus Status { get; set; }
        public int? FlagSave { get; set; }
        public int? PetId { get; set; }
        public string PetName { get; set; }
        public DateTime? PetDOB { get; set; }
        public string VetFirstName { get; set; }
        public string VetLastName { get; set; }
        public int? VetId { get; set; }
        public int MyProperty { get; set; }
        public string FileName { get; set; } 
        public string FilesToBeUploaded { get; set; }
        public List<HttpPostedFileBase> FilesList { get; set; }
        public int? SaveClick { get; set; }

        public Model.EconsultationSummary Map()
        {
            var eCSummary = new Model.EconsultationSummary
            {
                ID = Id,
                EConsultationId = EcId,
                VetId = VetId,
                UserId = PetOnwerId,
                DateSummary = DateTime.Now.Date,
                EconsultationStatusId = Convert.ToInt16(EconsultationStatus),
                PetName = PetName,
                PetDOB = PetDOB.Value.ToShortDateString(),
                EconsultationDateTime = ActionBeginDate.Value.ToShortDateString(),
                EconsultationDuration = Durations,
                Diagnoses = Diagnosis,
                Treatment = Treatment,
                VeterianFirstName = VetFirstName,
                VeterianLastName = VetLastName,
                FilesFolder = FilePath,
                FilesName = FileName
            };
            return eCSummary;
        }
    }
}