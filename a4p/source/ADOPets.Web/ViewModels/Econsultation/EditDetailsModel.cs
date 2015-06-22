using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using ExpressiveAnnotations.Attributes;
using System.Collections.Generic;
using System.Globalization;


namespace ADOPets.Web.ViewModels.Econsultation
{
    public class EditDetailsModel
    {
        public EditDetailsModel()
        {

        }
        public EditDetailsModel(Model.EConsultation ec)
        {
            Id = ec.ID;
            Date = Convert.ToDateTime(ec.RDVDate);
            Time = Convert.ToDateTime(ec.RDVDateTime);

            //ECTime = Date.Value.AddHours(Time.Value.Hour);
            if (ec.IsEditExpert == false)
            {
                Controllers.EconsultationController obj = new Controllers.EconsultationController();
                ECTime = Date.Value.AddHours(Time.Value.Hour).AddMinutes(Time.Value.Minute);
                ECTime = obj.GetEcTime(ECTime, Convert.ToInt16(ec.VetTimezoneID), Convert.ToInt16(ec.UserId));
            }
            else
            {
                Controllers.EconsultationController obj = new Controllers.EconsultationController();
                ECTime = Date.Value.AddHours(Time.Value.Hour).AddMinutes(Time.Value.Minute);
            }

            EconsultationStatus = (EConsultationStatusEnum)ec.EconsultationStatusId;
            CalenderId = ec.CalenderId.Value;
            ECTimeZone = (TimeZoneEnum)ec.VetTimezoneID;
        }

        [Display(Name = "Econsultation_Add_TimeOfEconsultation", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_ConsultationTImeRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? Time { get; set; }

        [Display(Name = "Econsultation_Add_DayOfEconsultation", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_ConsultationDayRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? Date { get; set; }

        [Display(Name = "Econsultation_Add_EconsultationStatus", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Econsultation_Add_StatusRequiredField", ErrorMessageResourceType = typeof(Wording))]
        public EConsultationStatusEnum EconsultationStatus { get; set; }

        public DateTime ECTime { get; set; }

        public int Id { get; set; }
        public EConsultationStatus Status { get; set; }
        public TimeZoneEnum ECTimeZone { get; set; }

        public int? CalenderId { get; set; }
        public void Map(Model.EConsultation econ)
        {
            econ.RDVDate = Date;
            econ.RDVDateTime = Time;
            econ.BDateConsultation = Date;
            econ.BTimeConsultation = Time;
            econ.EconsultationStatusId = EconsultationStatus;
            //econ.IsEditExpert = true;
            if (EconsultationStatus == EConsultationStatusEnum.Scheduled)
            {
                econ.IsRead = true;
            }
            else
            {
                econ.IsRead = false;
            }
        }

        public void MapEcRoom(Model.EconsultationRoom ecRoom)
        {
            ecRoom.Status = Convert.ToInt16(EconsultationStatus);
        }

        public void MapCalender(Model.Calendar calender)
        {
            calender.Date = Date.Value.Add(DateTime.ParseExact(Time.Value.ToString(), "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay);
        }
    }
}