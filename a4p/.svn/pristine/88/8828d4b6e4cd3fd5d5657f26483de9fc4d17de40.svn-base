using System;
using System.Web;
using ADOPets.Web.Common.Authentication;

namespace ADOPets.Web.Common.Helpers
{
    public static class TimeZoneHelper
    {
        public static DateTime ConvertDateTimeByUserTimeZoneId(DateTime datetime)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(datetime, HttpContext.Current.User.GetTimeZoneInfoId());
        }

        public static DateTime GetCurrentUserLocalTime()
        {
            var dt = DateTime.Now;
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, HttpContext.Current.User.GetTimeZoneInfoId());
        }

        public static string TimeAgo(DateTime dt)
        {
            DateTime dateNew = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, HttpContext.Current.User.GetTimeZoneInfoId());
            DateTime dateNow = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, HttpContext.Current.User.GetTimeZoneInfoId());
            TimeSpan span = dateNow - dateNew;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("{0} {1} " + Resources.Wording.Message_MesssageNotification_Ago,
               years, Resources.Wording.Message_MesssageNotification_Year);
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("{0} {1} " + Resources.Wording.Message_MesssageNotification_Ago,
                  months, Resources.Wording.Message_MesssageNotification_Month);
            }
            if (span.Days > 0)
                return String.Format("{0} {1} " + Resources.Wording.Message_MesssageNotification_Ago,
                 span.Days, Resources.Wording.Message_MesssageNotification_Day);
            if (span.Hours > 0)
                return String.Format("{0} {1} " + Resources.Wording.Message_MesssageNotification_Ago,
                 span.Hours, Resources.Wording.Message_MesssageNotification_Hour);
            if (span.Minutes > 0)
                return String.Format("{0} {1} " + Resources.Wording.Message_MesssageNotification_Ago,
                 span.Minutes, Resources.Wording.Message_MesssageNotification_Minute);
            if (span.Seconds > 5)
                return String.Format("{0} ", Resources.Wording.Message_MesssageNotification_Second + " " + Resources.Wording.Message_MesssageNotification_Ago, span.Seconds);
            if (span.Seconds <= 5)
                return Resources.Wording.Message_MesssageNotification_Justnow;
            return string.Empty;
        }
    }
}