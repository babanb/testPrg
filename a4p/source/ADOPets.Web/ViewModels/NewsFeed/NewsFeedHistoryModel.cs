using ADOPets.Web.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.NewsFeed
{
    public class NewsFeedHistoryModel
    {
        public NewsFeedHistoryModel() { }

        public NewsFeedHistoryModel(List<NewsFeedNotificationsViewModel> list, IEnumerable<DateTime> enumerable)
        {
            lstNotification = list;
            Month = Resources.Wording.ResourceManager.GetString("Shared_Layout_m" + Convert.ToDateTime(enumerable.First()).ToString("MMMM"));
            Year = Convert.ToDateTime(enumerable.First()).Year.ToString();
            NotificationDate = TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(enumerable.First());
        }

        public DateTime? NotificationDate { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public List<NewsFeedNotificationsViewModel> lstNotification { get; set; }
    }
}