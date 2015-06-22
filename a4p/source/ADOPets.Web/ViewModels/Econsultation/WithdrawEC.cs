using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using ExpressiveAnnotations.Attributes;
using System.Collections.Generic;
using System.Globalization;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class WithdrawEC
    {
        public WithdrawEC()
        {
        }

        public WithdrawEC(Model.EConsultation ec)
        {
            Id = ec.ID;
            Date = Convert.ToDateTime(ec.BDateConsultation);
            Time = Convert.ToDateTime(ec.BTimeConsultation);
            EconsultationStatus = ec.EconsultationStatusId;
            CalenderId = ec.CalenderId;
            PetCondition = ec.TitleConsultation;
            PetId = ec.PetId;
        }

        [Display(Name = "Econsultation_Add_TimeOfEconsultation", ResourceType = typeof(Wording))]
        public DateTime? Time { get; set; }

        [Display(Name = "Econsultation_Add_DayOfEconsultation", ResourceType = typeof(Wording))]
        public DateTime? Date { get; set; }

        [Display(Name = "Econsultation_Add_EconsultationStatus", ResourceType = typeof(Wording))]
        public EConsultationStatusEnum? EconsultationStatus { get; set; }

        public string PetCondition { get; set; }

        public int Id { get; set; }
        public EConsultationStatus Status { get; set; }

        public int? PetId { get; set; }

        public string PetName { get; set; }

        public int? CalenderId { get; set; }

        public void Map(Model.EConsultation econ)
        {
            econ.RDVDate = Date;
            econ.RDVDateTime = Time;
            econ.BDateConsultation = Date;
            econ.BTimeConsultation = Time;
            econ.EconsultationStatusId = EconsultationStatus;
        }
    }
}