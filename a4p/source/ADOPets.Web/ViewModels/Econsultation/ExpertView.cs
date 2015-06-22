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
    public class ExpertView
    {
        public ExpertView()
        {
        }
        public ExpertView(Model.EConsultation ec)
        {

            Id = ec.ID;
            CreatedBy = ec.CreatedBy;
            VetId = ec.VetId;

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
            Status = ec.EconsultationStatusId == null
                    ? System.String.Empty
                    : EnumHelper.GetResourceValueForEnumValue(ec.EconsultationStatusId);

            PetOwner = ec.User.FirstName + " " + ec.User.LastName;
            Controllers.EconsultationController obj = new Controllers.EconsultationController();
            ECTime = date.AddHours(time.Hour);
            ECTime = ECTime.AddMinutes(time.Minute);
            ECTime = obj.GetEcTime(ECTime, Convert.ToInt16(ec.VetTimezoneID), Convert.ToInt32(ec.VetId));
            TimeSpan timespan;
            if (TimeZoneHelper.GetCurrentUserLocalTime() < ECTime)
            {
                timespan = ECTime.Subtract(TimeZoneHelper.GetCurrentUserLocalTime());
            }
            else
            {
                timespan = TimeZoneHelper.GetCurrentUserLocalTime().Subtract(ECTime);
            }
            Duration = Convert.ToInt16(timespan.Minutes);
        }
        public bool IsPetDeleted { get; set; }
        public int Id { get; set; }
        public int? PetId { get; set; }
        public string PetType { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }
        public DateTime date { get; set; }
        public DateTime time { get; set; }
        public string Status { get; set; }
        public string PetOwner { get; set; }
        public TimeZoneEnum? VetTimeZoneId { get; set; }
        public DateTime ECTime { get; set; }
        public int? Duration { get; set; }
        public int? CreatedBy { get; set; }
        public int? VetId { get; set; }

    }
}