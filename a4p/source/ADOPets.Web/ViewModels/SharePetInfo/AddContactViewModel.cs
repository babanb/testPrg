
using Model.Tools;
namespace ADOPets.Web.ViewModels.SharePetInfo
{
    public class AddContactViewModel
    {

        public AddContactViewModel()
        {

        }
        public AddContactViewModel(Model.User user, Model.Login login)
        {
            ContactId = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            UserName = (login != null) ? Encryption.Decrypt(login.UserName) : "";
        }
        public AddContactViewModel(Model.User user, int count, Model.Login login)
        {
            ContactId = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            IsAlreadyShared = count == 0 ? false : true;

            UserName = (login != null) ? Encryption.Decrypt(login.UserName) : "";
        }

        public int ContactId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsAlreadyShared { get; set; }

        public string UserName { get; set; }

        public Model.ShareContact Map()
        {
            var shareContact = new Model.ShareContact
            {
                UserId = UserId,
                ContactId = ContactId
            };
            return shareContact;
        }


    }
}