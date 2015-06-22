using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using System.Globalization;

namespace ADOPets.Web.ViewModels.Calender
{
    public class EditViewModel
    {
        public EditViewModel()
        {

        }

        public EditViewModel(Model.Calendar calender, string redirectToUrl)
        {
            string timeFormat = CultureInfo.CurrentCulture.Name == "en-US" ? "h:mm tt" : "H:mm";
            Id = calender.Id;
            Reason = calender.Reason;
            Date = calender.Date.Value;
            Time = calender.Date.Value.ToString(timeFormat);
            Physician = calender.Physician;
            Comment = calender.Comment;
            SendNotification = calender.SendNotificationMail;
            UserId = calender.UserId;
            RedirectToActionName = redirectToUrl;
        }


        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "Calendar_Add_ReasonRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceName = "Calendar_Add_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime Date { get; set; }

        public string Time { get; set; }
        [Display(Name = "Calendar_Edit_Physician", ResourceType = typeof(Wording))]
        public string Physician { get; set; }

        public string Comment { get; set; }

        public bool SendNotification { get; set; }

        public int UserId { get; set; }

        public string RedirectToActionName { get; set; }

        public void Map(Model.Calendar calender)
        {
            calender.UserId = UserId;
            calender.Date = Date.Add(DateTime.ParseExact(Time, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay);
            calender.Physician = new EncryptedText(Physician);
            calender.Reason = new EncryptedText(Reason);
            calender.Comment = new EncryptedText(Comment);
            calender.SendNotificationMail = SendNotification;
        }
        public void MapFr(Model.Calendar calender)
        {
            calender.UserId = UserId;


            DateTime dt = DateTime.ParseExact(Date.ToShortDateString(), "d/M/yyyy", null);
            var datetime = dt.ToString("dd/MM/yyyy");
            var date1 = Convert.ToDateTime(datetime);
            var date2 = date1.Add(DateTime.ParseExact(Time, "H:mm", CultureInfo.InvariantCulture).TimeOfDay);

            calender.Date = date2;
            calender.Physician = new EncryptedText(Physician);
            calender.Reason = new EncryptedText(Reason);
            calender.Comment = new EncryptedText(Comment);
            calender.SendNotificationMail = SendNotification;
        }
        public void MapIn(Model.Calendar calender)
        {
            calender.UserId = UserId;


            DateTime dt = DateTime.ParseExact(Date.ToShortDateString(), "d/M/yyyy", null);
            var datetime = dt.ToString("dd-MM-yyyy");
            var date1 = Convert.ToDateTime(datetime);
            var date2 = date1.Add(DateTime.ParseExact(Time, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay);

            calender.Date = date2;
            calender.Physician = new EncryptedText(Physician);
            calender.Reason = new EncryptedText(Reason);
            calender.Comment = new EncryptedText(Comment);
            calender.SendNotificationMail = SendNotification;
        }
    }
}