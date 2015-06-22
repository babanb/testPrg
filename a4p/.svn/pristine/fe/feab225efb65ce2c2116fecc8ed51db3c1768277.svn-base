using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using ExpressiveAnnotations.Attributes;
using System.Collections.Generic;
using System.Globalization;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class ExperViewReportModel
    {
        public ExperViewReportModel()
        {

        }

        public ExperViewReportModel(Model.EconsultationSummary ECSummary, List<EconsultDocumentViewModel> EcDocs)
        {
            ID = ECSummary.ID;
            EcId = ECSummary.EConsultationId;
            EconsultationStatus = (EConsultationStatusEnum)ECSummary.EconsultationStatusId;
            PetName = ECSummary.PetName;
            Durations = ECSummary.EconsultationDuration;
            PetOnwerId = ECSummary.UserId;
            Diagnoses = ECSummary.Diagnoses;
            Treatment = ECSummary.Treatment;
            ExpertName = ECSummary.VeterianFirstName + " " + ECSummary.VeterianLastName;
            FileName = ECSummary.FilesName;
            FilePath = ECSummary.FilesFolder;
            ConsultationDate = ECSummary.DateSummary.Value;
            lstAttachment = EcDocs;
            VetId = ECSummary.VetId;
        }

        public ExperViewReportModel(Model.EconsultationSummary ec)
        {
            EcId = ec.EConsultationId;
            Durations = ec.EconsultationDuration;
            EconsultationStatus = (EConsultationStatusEnum) ec.EconsultationStatusId;
            PetOnwerId = ec.UserId;
            Diagnoses = ec.Diagnoses;
            Treatment = ec.Treatment;
            PetName = ec.PetName;
            VetId = ec.VetId;
        }
        
        public DateTime? ActionBeginDate { get; set; }
        public DateTime? ActionEndDate { get; set; }
        public DateTime? ConsultationDate { get; set; }
        public string Diagnoses { get; set; }
        public string Treatment { get; set; }
        public string FilePath { get; set; }
        public decimal? Durations { get; set; }
        public List<EconsultDocumentViewModel> lstAttachment { get; set; }
        public EConsultationStatusEnum? EconsultationStatus { get; set; }
        public string PetCondition { get; set; }
        public int? EcId { get; set; }
        public int? PetOnwerId { get; set; }
        public int? VetId { get; set; }
        public int Id { get; set; }
        public EConsultationStatus Status { get; set; }
        public string ExpertName { get; set; }
        public int? PetId { get; set; }
        public string FileName { get; set; }
        public string PetName { get; set; }
        public decimal ID { get; set; }
    }
}