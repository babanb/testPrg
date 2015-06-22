using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Message
{
    public class MessageReplyViewModel
    {
        public MessageReplyViewModel()
        {
            
        }

        public MessageReplyViewModel(string conversationId)
        {
            ConversationId = conversationId;
        }

        public string ConversationId { get; set; }

        [Required(ErrorMessageResourceName = "Message_MessageReplay_MessageRequired", ErrorMessageResourceType = typeof(Wording))]
        public string MessageBody { get; set; }

        public Model.Message MapSend(string conversationId, string subject, int userId, int userToId)
        {
            return new Model.Message
            {
                UserId = userId,
                FromUserId = userId,
                ToUserId = userToId,
                Date = DateTime.Now,
                Subject = new EncryptedText(subject),
                Body = new EncryptedText(MessageBody),
                MessageTypeId = MessageTypeEnum.Sent,
                Unread = false,
                ConversationId = conversationId
            };
        }

        public Model.Message MapRecieve(string conversationId, string subject, int userId, int userToId)
        {
            return new Model.Message
            {
                UserId = userToId,
                FromUserId = userId,
                ToUserId = userToId,
                Date = DateTime.Now,
                Subject = new EncryptedText(subject),
                Body = new EncryptedText(MessageBody),
                MessageTypeId = MessageTypeEnum.Received,
                Unread = true,
                ConversationId = conversationId
            };
        }
    }
}