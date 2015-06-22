using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using System;
using System.Globalization;



namespace ADOPets.Web.ViewModels.Calender
{
    public class AddViewModel
    {

        public AddViewModel() { }

        public AddViewModel(int userId, string redirectToUrl, DateTime? clickDate)
        {
            UserId = userId;
            RedirectToActionName = redirectToUrl;
            if (clickDate != null) { Date = clickDate.Value; }
        }

        [Required(ErrorMessageResourceName = "Calendar_Add_ReasonRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Reason { get; set; }

        [Required(ErrorMessageResourceName = "Calendar_Add_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? Date { get; set; }

        public string Time { get; set; }

        [Display(Name = "Calendar_Add_Physician", ResourceType = typeof(Wording))]
        public string Physician { get; set; }

        public string Comment { get; set; }

        public bool SendNotification { get; set; }

        public int UserId { get; set; }

        public string RedirectToActionName { get; set; }

        public int Id { get; set; }
        public Model.Calendar Map()
        {
            var calender = new Model.Calendar
            {
                Reason = new EncryptedText(Reason),
                Date = Date.Value.Add(DateTime.ParseExact(Time, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay),
                Physician = new EncryptedText(Physician),
                Comment = new EncryptedText(Comment),
                SendNotificationMail = SendNotification,
                UserId = UserId
            };
            return calender;
        }
        public Model.Calendar MapFr()
        {
            DateTime dt = DateTime.ParseExact(Date.Value.ToShortDateString(), "d/M/yyyy", null);
            var datetime = dt.ToString("dd/MM/yyyy");
            var date1 = Convert.ToDateTime(datetime);
            string _time = Time.Trim();
            var date2 = date1.Add(DateTime.ParseExact(_time, "H:mm", CultureInfo.InvariantCulture).TimeOfDay);

            var calender = new Model.Calendar
            {
                Reason = new EncryptedText(Reason),
                Date = date2,
                Physician = new EncryptedText(Physician),
                Comment = new EncryptedText(Comment),
                SendNotificationMail = SendNotification,
                UserId = UserId
            };
            return calender;
        }
        public Model.Calendar MapIn()
        {
            DateTime dt = DateTime.ParseExact(Date.Value.ToShortDateString(), "d/M/yyyy", null);
            var datetime = dt.ToString("dd-MM-yyyy");
            var date1 = Convert.ToDateTime(datetime);
            string _time = Time.Trim();
            var date2 = date1.Add(DateTime.ParseExact(_time, "HH:mm", CultureInfo.InvariantCulture).TimeOfDay);

            var calender = new Model.Calendar
            {
                Reason = new EncryptedText(Reason),
                Date = date2,
                Physician = new EncryptedText(Physician),
                Comment = new EncryptedText(Comment),
                SendNotificationMail = SendNotification,
                UserId = UserId
            };
            return calender;
        }
    }
}

