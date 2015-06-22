using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using System.Globalization;


namespace ADOPets.Web.ViewModels.Calender
{
    public class DetailsViewModel
    {
        public DetailsViewModel() { }

        public DetailsViewModel(Model.Calendar calendar)
        {
            Id = calendar.Id;
            PetId = calendar.PetId;
            UserId = calendar.UserId;
            Physician = calendar.Physician;
            Reason = calendar.Reason;
            SendNotificationMail = calendar.SendNotificationMail;
            NotificationSent = calendar.NotificationSent;
            Date = Convert.ToDateTime(calendar.Date, CultureInfo.CurrentCulture).ToShortDateString();
            Time = Convert.ToDateTime(calendar.Date, CultureInfo.CurrentCulture).ToShortTimeString(); //ToString("hh:mm tt") 
            Comment = calendar.Comment;
            

        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int? PetId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Physician { get; set; }
        public string Reason { get; set; }
        public bool SendNotificationMail { get; set; }
        public bool NotificationSent { get; set; }
        public string Comment { get; set; }
       

    }
}