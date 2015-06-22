using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.SharePetInfo
{
    public class DeleteContactViewModel
    {
        public DeleteContactViewModel() { }

        public DeleteContactViewModel(Model.ShareContact shareContact)
        {
            ContactId = shareContact.ContactId;
            UserId = shareContact.UserId;
            Id = shareContact.Id;
        }
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int UserId { get; set; }

        public Model.ShareContact Map()
        {
            var sharecontact = new Model.ShareContact
            {
                UserId = UserId,
                ContactId = ContactId
            };
            return sharecontact;
        }
    }
}