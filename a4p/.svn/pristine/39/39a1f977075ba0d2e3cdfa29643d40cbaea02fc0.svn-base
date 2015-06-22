using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.Calender
{
    public class CalenderNotificationViewModel
    {
        public CalenderNotificationViewModel(string remId, string subject, int fromUserId, DateTime? remDate)
        {
            FromUserId = fromUserId;
            ReminderId = remId;
            Subject = subject.Length > 25 ? string.Format("{0} ...", subject.Substring(0, 24)) : subject;
            ReminderDateText = Convert.ToDateTime(remDate);
        }

        public string ReminderId { get; set; }

        public string Subject { get; set; }

        public int FromUserId { get; set; }

        public DateTime ReminderDateText { get; set; }
    }
}