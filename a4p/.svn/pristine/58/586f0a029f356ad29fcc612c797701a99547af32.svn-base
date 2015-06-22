using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Message
{
    public class MessageDetailViewModel
    {
        public MessageDetailViewModel() { }

        public MessageDetailViewModel(Model.Message message)
        {
            MessageId = message.Id;
            FromUserFullName = string.Format("{0} {1}", message.UserFrom.FirstName.Value, message.UserFrom.LastName.Value);
            Date = TimeZoneHelper.ConvertDateTimeByUserTimeZoneId(message.Date);
            MessageType = message.MessageTypeId;
            Body = message.Body;
        }

        public int MessageId { get; set; }

        public string FromUserFullName { get; set; }

        public DateTime Date { get; set; }

        public MessageTypeEnum MessageType { get; set; }

        [Display(Name = "Message_MessageDetails_ReplyTo", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Message_MessageDetails_MessageRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Body { get; set; }
    }
}