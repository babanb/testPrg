using System;
using System.Linq;
using ADOPets.Web.Common.Helpers;
using Model;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using System.IO;
using ADOPets.Web.Common;
using System.Drawing;
using System.Linq.Expressions;

namespace ADOPets.Web.ViewModels.SharePetInfo
{
    public class IndexViewModel
    {

        public IndexViewModel() { }

        public IndexViewModel(ShareCategoryType shareCategoryType, int AllCount)
        {
            ShareCategoryType = shareCategoryType;
            ShareContactsCount = AllCount;
            IsShareContactsExist = false;
            PetId = 0;
        }


        //public IndexViewModel(ShareCategoryType s, int AllCount, int PetCount, int petId)
        //{
        //    ShareCategoryType = s;
        //    if (petId == 0)
        //    {
        //        ShareContactsCount = AllCount;
        //        IsShareContactsExist = false;
        //    }
        //    else
        //    {
        //        ShareContactsCount = PetCount;
        //        IsShareContactsExist = PetCount > 0 ? true : false;
        //    }
        //    PetId = petId;
        //}

        public IndexViewModel(ShareCategoryType shareCategoryType, int shareCount, int petId)
        {
            ShareCategoryType = shareCategoryType;
            ShareContactsCount = shareCount;
            IsShareContactsExist = shareCount > 0 ? true : false;
            PetId = petId;
        }

        public ShareCategoryType ShareCategoryType { get; set; }
        public int ShareContactsCount { get; set; }
        public int? PetId { get; set; }
        public bool IsShareContactsExist { get; set; }
    }
}