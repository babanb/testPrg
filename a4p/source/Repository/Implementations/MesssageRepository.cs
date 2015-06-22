using System.Collections.Generic;
using System.Data.Entity;
using Model;

namespace Repository.Implementations
{
    public class MesssageRepository : GenericRepository<Message>
    {
        public MesssageRepository(DbContext context) : base(context)
        {
        }

        public void MarkAsRead(string conversationKey, int userId)
        {
            ExecuteSqlCommand("Update [Message] Set Unread = 0 Where UserId = {0} and ConversationId = {1}", userId, conversationKey);
        }

        public void DeleteConversation(List<string> conversationsKey, int userId)
        {
            foreach (var key in conversationsKey)
            {
                ExecuteSqlCommand("Delete From [Message] Where UserId = {0} and ConversationId = {1}", userId, key);
            }
        }
    }
}
