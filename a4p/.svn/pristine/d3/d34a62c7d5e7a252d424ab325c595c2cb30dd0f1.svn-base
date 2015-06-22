using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Message
{
    public class NewMessageViewModel
    {
        public NewMessageViewModel()
        {

        }

        public NewMessageViewModel(int toUserId, string fullName)
        {
            ToUserId = toUserId;
            ToUserFullName = fullName;
        }

        public int ToUserId { get; set; }

        public string ToUserFullName { get; set; }

        [Display(Name = "Message_NewMessage_Subject", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Message_NewMessage_SubjectRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Subject { get; set; }

        [Display(Name = "Message_NewMessage_Message", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Message_NewMessage_MessageRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Body { get; set; }

        public Model.Message MapSend(int userId, string conversationKey)
        {
            return new Model.Message
            {
                UserId = userId,
                FromUserId = userId,
                ToUserId = ToUserId,
                Date = DateTime.Now,
                MessageTypeId = MessageTypeEnum.Sent,
                Subject = new EncryptedText(Subject),
                Body = new EncryptedText(Body),
                Unread = false,
                ConversationId = conversationKey
            };
        }

        public Model.Message MapRecieve(int userId, string conversationKey)
        {
            return new Model.Message
            {
                UserId = ToUserId,
                FromUserId = userId,
                ToUserId = ToUserId,
                Date = DateTime.Now,
                MessageTypeId = MessageTypeEnum.Received,
                Subject = new EncryptedText(Subject),
                Body = new EncryptedText(Body),
                Unread = true,
                ConversationId = conversationKey
            };
        }

    }
}