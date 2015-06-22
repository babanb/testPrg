using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.SharePetInfo
{
    public class AddShareCategoryTypeContactModel
    {
        public AddShareCategoryTypeContactModel() { }

        public AddShareCategoryTypeContactModel(Model.ShareCategoryTypeContact shareCategoryTypeContact)
        {
            ShareContactId = shareCategoryTypeContact.ShareContactId;
            ShareCategoryTypeId = shareCategoryTypeContact.ShareCategoryTypeId;
            Id = shareCategoryTypeContact.Id;
        }

        public int Id { get; set; }
        public int ShareContactId { get; set; }
        public Model.ShareCategoryTypeEnum ShareCategoryTypeId { get; set; }
        public int? PetId { get; set; }

        public Model.ShareCategoryTypeContact Map()
        {
            var shareCategoryTypeContact = new Model.ShareCategoryTypeContact
            {
                Id=Id,
                ShareContactId = ShareContactId,
                ShareCategoryTypeId = ShareCategoryTypeId,
                PetId = PetId
            };
            return shareCategoryTypeContact;
        }
    }
}