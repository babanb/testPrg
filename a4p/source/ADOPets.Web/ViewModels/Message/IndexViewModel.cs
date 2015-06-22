using System;
using ADOPets.Web.Common.Helpers;
using Model;


namespace ADOPets.Web.ViewModels.Message
{
    public class IndexViewModel
    {
        public IndexViewModel() { }

        public IndexViewModel(Model.Message message, bool unread, int messageCount)
        {
            ConversationKey = message.ConversationId;
            Date = TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(message.Date);
            MessageType = message.MessageTypeId;
            SubjectSummary = message.Subject.Value.Length > 50 ? string.Format("{0} ...", message.Subject.Value.Substring(0, 49)) : message.Subject;
            BodySummary = message.Body.Value.Length > 50 ? string.Format("{0} ...", message.Body.Value.Substring(0, 49)) : message.Body;
            Unread = unread;

            var userFullName = message.MessageTypeId == MessageTypeEnum.Received
                ? string.Format("{0} {1}", message.UserFrom.FirstName.Value, message.UserFrom.LastName.Value)
                : string.Format("{0} {1}", message.UserTo.FirstName.Value, message.UserTo.LastName.Value);

            SenderTitle = messageCount > 1 ? string.Format("{0} ({1})", userFullName, messageCount) : userFullName;
        }

        public string ConversationKey { get; set; }
        public DateTime Date { get; set; }
        public MessageTypeEnum MessageType { get; set; }
        public string SubjectSummary { get; set; }
        public string BodySummary { get; set; }
        public bool Unread { get; set; }

        public string SenderTitle { get; set; }
    }
}