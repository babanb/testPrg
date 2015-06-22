using ADOPets.Web.Common.Helpers;
using Model;
using System;
using System.Globalization;

namespace ADOPets.Web.ViewModels.Message
{
    public class MessageNotificationViewModel
    {
        public MessageNotificationViewModel()
        {

        }
        //calender
        public MessageNotificationViewModel( bool flag,string id, string subject, string fromUserName, string notificationType, DateTime dt)
        {
            ConversationId = id;
            FromUserName = fromUserName;
            TypeOfNotification = notificationType;
            Subject = subject;//.Length > 25 ? string.Format("{0} ...", subject.Substring(0, 24)) : subject;
            NotificationDate = dt;
        }
        public MessageNotificationViewModel(string id, string subject, string fromUserName, string notificationType, DateTime dt)
        {
            ConversationId = id;
            FromUserName = fromUserName;
            TypeOfNotification = notificationType;
            Subject = subject;//.Length > 25 ? string.Format("{0} ...", subject.Substring(0, 24)) : subject;
            NotificationDate = TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(dt);
        }
        //history page
        public MessageNotificationViewModel(string id, string subject, string fromUserName, string notificationType, DateTime dt, bool isSMOPaymentDone = false)
        {
            ConversationId = id;
            FromUserName = fromUserName;
            TypeOfNotification = notificationType;
            Subject = subject;//.Length > 25 ? string.Format("{0} ...", subject.Substring(0, 24)) : subject;
            NotificationDate =TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(dt);
            IsSMOPaymentDone = isSMOPaymentDone;
        }

        //smo 
        public MessageNotificationViewModel(string id, string subject, string notificationType)
        {
            ConversationId = id;
            TypeOfNotification = notificationType;
            Subject = subject;
            //NotificationDate = dt;
        }
        public MessageNotificationViewModel(string id, string subject, string notificationType, DateTime? dt, bool IsSmoPaymentDone)
        {
            ConversationId = id;
            TypeOfNotification = notificationType;
            Subject = subject;
            IsSMOPaymentDone = IsSmoPaymentDone;
            if (dt != null)
                NotificationDate = TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(Convert.ToDateTime(dt));
        }


        public MessageNotificationViewModel(string id, string subject, string fromUserName, string notificationType)
        {
            ConversationId = id;
            FromUserName = fromUserName;
            TypeOfNotification = notificationType;
            Subject = subject.Length > 25 ? string.Format("{0} ...", subject.Substring(0, 24)) : subject;
            //NotificationDate = dt;
        }

        //for message notification on layout right sidebar
        public MessageNotificationViewModel(string conversationId, string subject, int fromUserId, string msgDate, string fromUserName, MessageTypeEnum messageTypeId)
        {
            FromUserId = fromUserId;
            ConversationId = conversationId;
            Subject = subject.Length > 25 ? string.Format("{0} ...", subject.Substring(0, 24)) : subject;
            MessageDateText = msgDate;
            FromUserName = fromUserName;
            MessageTypeId = messageTypeId;
        }


        //ShareInfo
        public MessageNotificationViewModel(string id, string subject, string fromUserName, ShareCategoryTypeEnum sharecateorytypeid, string notificationType, DateTime creationdate)
        {
            ConversationId = id;
            FromUserName = fromUserName;
            TypeOfNotification = notificationType;
            Subject = subject;
            ShareCategoryTypeId = sharecateorytypeid;
            NotificationDate = creationdate;
        }

        public bool IsSMOPaymentDone { get; set; }

        public string ConversationId { get; set; }

        public string Subject { get; set; }

        public int FromUserId { get; set; }

        public string MessageDateText { get; set; }

        public string FromUserName { get; set; }

        public MessageTypeEnum MessageTypeId { get; set; }

        public string TypeOfNotification { get; set; }

        public DateTime NotificationDate { get; set; }

        public ShareCategoryTypeEnum ShareCategoryTypeId { get; set; }
    }
}