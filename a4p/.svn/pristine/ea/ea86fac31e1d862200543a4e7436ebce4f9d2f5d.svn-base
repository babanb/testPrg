using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using ExpressiveAnnotations.Attributes;
using System.Collections.Generic;
using System.Globalization;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class SubmitReportModel
    {
        public SubmitReportModel()
        {

        }

        public SubmitReportModel(Model.EConsultation ec)
        {
            EcId = ec.ID;
            ActionBeginDate = Convert.ToDateTime(ec.ActionDateTimeBegin);
            ActionEndDate = Convert.ToDateTime(ec.ActionDateTimeEnd);
            Durations = ec.Periods;
            EconsultationStatus = ec.EconsultationStatusId;
            PetCondition = ec.TitleConsultation;
            PetId = ec.PetId;
            PetOnwerId = ec.UserId;
        }
        
        public DateTime? ActionBeginDate { get; set; }
        public DateTime? ActionEndDate { get; set; }
        public string Diagnosis { get; set; }
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
        public int? PetId { get; set; }
        public string PetName { get; set; }
        public DateTime? PetDOB { get; set; }
        public PetTypeEnum PetType { get; set; }
        public string PetBreadType { get; set; }
        public string VetFirstName { get; set; }
        public string VetLastName { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerEmail { get; set; }
    }
}