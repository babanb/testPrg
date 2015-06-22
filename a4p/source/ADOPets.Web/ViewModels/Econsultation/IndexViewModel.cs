﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADOPets.Web.Resources;
using ADOPets.Web.Common.Helpers;
using Model;
using System.ComponentModel.DataAnnotations;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
        }

        public IndexViewModel(Model.EConsultation ec)
        {
            Id = ec.ID;
            IsPetDeleted = false;
            if (ec.Pet != null)
            {
                if (ec.Pet.IsDeleted) { IsPetDeleted = true; }

                PetId = ec.Pet.PetId;
                Name = ec.Pet.Name;
                PetType = ec.Pet == null
                    ? System.String.Empty
                    : EnumHelper.GetResourceValueForEnumValue(ec.Pet.PetTypeId);
            }
            Condition = ec.TitleConsultation;
            date = Convert.ToDateTime(ec.RDVDate);
            time = Convert.ToDateTime(ec.RDVDateTime);
            Controllers.EconsultationController obj = new Controllers.EconsultationController();
            ECTime = date.AddHours(time.Hour);
            ECTime = ECTime.AddMinutes(time.Minute);
            ECTime = obj.GetEcTime(ECTime, Convert.ToInt16(ec.VetTimezoneID), Convert.ToInt32(ec.UserId));
            TimeSpan timespan;
            if (TimeZoneHelper.GetCurrentUserLocalTime() < ECTime)
            {
                timespan = ECTime.Subtract(TimeZoneHelper.GetCurrentUserLocalTime());
                Duration = Convert.ToInt16(timespan.TotalHours);
            }

            Status = ec.EconsultationStatusId == null
                    ? System.String.Empty
                    : EnumHelper.GetResourceValueForEnumValue(ec.EconsultationStatusId);

            IsPaymentDone = true;

            if (ec.EconsultationStatusId == EConsultationStatusEnum.PaymentPending)
            {
                IsPaymentDone = false;
            }

            ECSubmittedBy = ec.CreatedBy;

        }
        public bool IsPetDeleted { get; set; }
        public int? ECSubmittedBy { get; set; }

        public bool IsPaymentDone { get; set; }
        public int Id { get; set; }
        public int? PetId { get; set; }
        public string PetType { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }
        public DateTime date { get; set; }
        public DateTime time { get; set; }
        public string Status { get; set; }
        public DateTime ECTime { get; set; }
        public int? Duration { get; set; }
    }

}