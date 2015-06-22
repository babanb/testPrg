using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.ViewModels.NewsFeed
{
    public class IndexViewModel
    {
        public IndexViewModel(SharePetInformation model)
        {
            Id = model.Id;
            ContactId = model.ContactId;
            PetId = model.PetId;
            UserId = model.UserId;
            ShareCategoryTypeId = model.ShareCategoryTypeId;
            FirstName = model.User.FirstName;
            LastName = model.User.LastName;
            CreationDate =Convert.ToDateTime(model.CreationDate);
            HoursAgo = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(model.CreationDate)).TotalHours);
        }

        public IndexViewModel(SharePetInfoCommunity model)
        {
            Id = model.Id;
            ContactId = model.ContactId;
            PetId = model.PetId;
            UserId = model.UserId;
            ShareCategoryTypeId = model.ShareCategoryTypeId;
            FirstName = model.User1.FirstName;
            LastName = model.User1.LastName;
            CreationDate = Convert.ToDateTime(model.CreationDate);
            HoursAgo = Convert.ToInt32((DateTime.Now - Convert.ToDateTime(model.CreationDate)).TotalHours);
        }

        public int Id { get; set; }
        public int ContactId { get; set; }
        public int PetId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int HoursAgo { get; set; }
        public DateTime CreationDate { get; set; }
        public ShareCategoryTypeEnum ShareCategoryTypeId { get; set; }      
    }
}