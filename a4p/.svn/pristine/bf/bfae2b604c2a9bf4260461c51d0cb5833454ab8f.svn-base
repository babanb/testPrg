using Model;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ADOPets.Web.Common.Helpers
{
    public static class ShareInfoHelper
    {
        public static bool IsSharedUser(int Id, int userId)
        {
            bool result = true;
            if (Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()) || Roles.IsUserInRole(UserTypeEnum.Admin.ToString()))
            {
                using (var uow = new UnitOfWork())
                {
                    int Count = uow.SharePetInformationRepository.GetAll(a => a.PetId == Id).Where(s => s.ContactId == userId && s.UserId != userId).Count();
                    if (Count > 0)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        public static bool IsCommunityUser(int Id, int userId)
        {
            bool result = true;
            using (var uow = new UnitOfWork())
            {
                int Count = uow.SharePetInformationRepository.GetAll(a => a.PetId == Id).Where(s => s.ContactId == userId && s.UserId != userId).Count();
                if (Count > 0)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}