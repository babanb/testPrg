using System.Data.Entity;
using System.Linq;
using Model;
using Model.Tools;

namespace Repository.Implementations
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }

        public int? GetUserIdByEmail(string email)
        {
            var query = "SELECT top 1 Id FROM [User] WHERE UserStatusId=1 and Email = @p0";
            var encrypted = Encryption.Encrypt(email.ToLower());
            return context.Database.SqlQuery<int?>(query, encrypted).FirstOrDefault();
        }

        public bool MailAlreadyExist(string email)
        {
            var encrypted = Encryption.Encrypt(email.ToLower());
            var query = "SELECT COUNT(*) FROM [User] WHERE Email = @p0";
            return context.Database.SqlQuery<int>(query, encrypted).First() != 0;
        }

        public bool IfMailAlreadyExist(string email, int userId)
        {
            var encrypted = Encryption.Encrypt(email.ToLower());
            var query = "SELECT COUNT(*) FROM [User] WHERE Email = @p0 and UserStatusId=1 and Id <>" + userId;
            return context.Database.SqlQuery<int>(query, encrypted).First() == 0;
        }
    }
}
