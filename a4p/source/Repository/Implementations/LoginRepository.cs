using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Model;
using Model.Tools;

namespace Repository.Implementations
{
    public class LoginRepository : GenericRepository<Login>
    {
        public LoginRepository(DbContext context) : base(context)
        {
        }

        public Login GetByUserName(string userName, params Expression<Func<Login, object>>[] navigationProperties)
        {
            var encrypted = Encryption.Encrypt(userName);
            return GetSingle(l => l.UserName == encrypted, navigationProperties);
        }

        public Login GetByUserNameAndPassword(string userName, string password, params Expression<Func<Login, object>>[] navigationProperties)
        {
            var encryptedUserName = Encryption.Encrypt(userName);

            var login = GetSingle(l => l.UserName == encryptedUserName && l.User.UserStatusId==UserStatusEnum.Active, navigationProperties);

            if (login != null)
            {
                var encryptedPass = Encryption.EncryptAsymetric(password + login.RandomPart);

                return encryptedPass == login.Password ? login : null;
            }
            return null;
        }
    }
}
