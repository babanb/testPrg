using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.SharePetInfo
{
    public class AddCommunityContactsModel
    {
        public AddCommunityContactsModel() { }

        public AddCommunityContactsModel(Model.SharePetInfoCommunity shareCommunity)
        {
            Id = shareCommunity.Id;
            ShareCategoryTypeId = shareCommunity.ShareCategoryTypeId;
            ContactId = shareCommunity.ContactId;
            PetId = shareCommunity.PetId;
            UserId = shareCommunity.UserId;
            CreationDate = shareCommunity.CreationDate;
            IsRead = shareCommunity.IsRead;
        }

        public int Id { get; set; }
        public Model.ShareCategoryTypeEnum ShareCategoryTypeId { get; set; }
        public int ContactId { get; set; }
        public int PetId { get; set; }
        public int UserId { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public bool IsRead { get; set; }

        public Model.SharePetInfoCommunity Map()
        {
            var shareCommunity = new Model.SharePetInfoCommunity
            {
                Id = Id,
                ShareCategoryTypeId = ShareCategoryTypeId,
                ContactId = ContactId,
                PetId = PetId,
                UserId = UserId,
                CreationDate = CreationDate,
                IsRead = IsRead
            };
            return shareCommunity;
        }
    }
}