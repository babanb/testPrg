using Model;
using Model.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.SharePetInfo
{
    public class MyContactViewModel
    {
        public MyContactViewModel()
        {

        }
        public MyContactViewModel(User usr, int id, int userId)
        {
            Id = id;
            UserId = userId;
            FirstName = usr.FirstName;
            LastName = usr.LastName;
            Email = usr.Email;
            ContactId = usr.Id;
            //UserName = (login != null) ? Encryption.Decrypt(login.UserName) : "";
            IsContactExist = usr.Id == 0 ? false : true;
        }

        public MyContactViewModel(User usr, int id, int userId, Login login)
        {
            Id = id;
            UserId = userId;
            FirstName = usr.FirstName;
            LastName = usr.LastName;
            Email = usr.Email;
            ContactId = usr.Id;
            UserName = (login != null) ? Encryption.Decrypt(login.UserName) : "";
            IsContactExist = usr.Id == 0 ? false : true;
        }

        public MyContactViewModel(User usr, int id, int userId, int count,Login login)
        {
            Id = id;
            UserId = userId;
            FirstName = usr.FirstName;
            LastName = usr.LastName;
            Email = usr.Email;
            ContactId = usr.Id;
            UserName = (login != null) ? Encryption.Decrypt(login.UserName) : "";
            IsContactExist = count > 0 ? true : false;
        }

        public MyContactViewModel(AddContactViewModel addContactInfo)
        {
            UserId = UserId;
            ContactId = ContactId;
            GetUserDetails(addContactInfo);
            // ContactId = shareContact.Id;
            // UserId = shareContact.UserId;
          //  UserName = (login != null) ? Encryption.Decrypt(login.UserName) : "";
        }

        private void GetUserDetails(AddContactViewModel addContactInfo)
        {
            FirstName = addContactInfo.FirstName;
            LastName = addContactInfo.LastName;
            Email = addContactInfo.Email;
            ContactId = addContactInfo.ContactId;
            UserId = addContactInfo.UserId;
        }

        public int Id { get; set; }
        public int ContactId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsContactExist { get; set; }

        public string UserName { get; set; }

        //public Model.User Map()
        //{
        //    var user = new Model.User
        //    {
        //        Id = UserId,
        //        // ContactId = user.Id,
        //        FirstName = new EncryptedText(FirstName),
        //        LastName = new EncryptedText(LastName),
        //        Email = new EncryptedText(Email)
        //    };
        //    return user;
        //}
    }
}