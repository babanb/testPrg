using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADOPets.Web.ViewModels.Message;
using ADOPets.Web.Common.Helpers;
using System.Globalization;

namespace ADOPets.Web.ViewModels.Message
{
    public class NotificationHistoryViewModel
    {
        public NotificationHistoryViewModel() { }

        public NotificationHistoryViewModel(List<MessageNotificationViewModel> list, IEnumerable<DateTime> enumerable)
        {
            lstNotification = list;
            var mnth = Resources.Wording.ResourceManager.GetString("Shared_Layout_m" + Convert.ToDateTime(enumerable.First()).ToString("MMMM"));

            if (mnth == null)
            {
                mnth = Convert.ToDateTime(enumerable.First()).ToString("MMMM");
            }
            Month = mnth;
            Year = Convert.ToDateTime(enumerable.First()).Year.ToString();
            NotificationDate = Convert.ToDateTime(enumerable.First(), CultureInfo.CurrentCulture);
          //  NotificationDate = Convert.ToDateTime(TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(enumerable.First()), CultureInfo.CurrentCulture);
        }

        public DateTime NotificationDate { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public List<MessageNotificationViewModel> lstNotification { get; set; }
    }
}