using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.Calender
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {

        }

        public DeleteViewModel(Model.Calendar calender)
        {
            Id = calender.Id;
            UserId = calender.UserId;
            Date = calender.Date.Value;
            Time = calender.Date.Value.ToString("hh:mm tt");
            Physician = calender.Physician;
            Reason = calender.Reason;
            Comment = calender.Comment;
            SendNotification = calender.SendNotificationMail;

        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public System.DateTime Date { get; set; }
        public string Time { get; set; }
        [Display(Name = "Calendar_Delete_Physician", ResourceType = typeof(Wording))]
        public string Physician { get; set; }

        public string Reason { get; set; }

        public string Comment { get; set; }
        public bool SendNotification { get; set; }
    }
}