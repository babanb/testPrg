﻿using Model;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADOPets.Web.Common.Helpers
{
    public static class UserHelper
    {
        /// <summary>
        /// check if the user is Inactive ,
        /// If yes - Restrict the admin to modify any pet details for the user
        /// if no- allow admin to perform the action
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool CanDeletePet(int petId)
        {
            bool result = true;

            using (var uow = new UnitOfWork())
            {
                var petData = uow.PetRepository.GetSingleTracking(p => p.Id == petId, p => p.Users);
                if (petData != null)
                {
                    var user = petData.Users.FirstOrDefault();
                    if (user != null)
                    {
                        var userData = uow.UsersRepository.GetSingleTracking(a => a.Id == user.Id);
                        if (userData != null)
                        {
                            if (userData.UserStatusId == UserStatusEnum.Disabled)
                            {
                                result = false;
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Get the user Id / Owner Id of Pet by pet Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int GetUserIdByPetId(int petId)
        {
            using (var uow = new UnitOfWork())
            {
                var petData = uow.PetRepository.GetSingleTracking(p => p.Id == petId, p => p.Users);
                if (petData != null)
                {
                    var user = petData.Users.FirstOrDefault();
                    if (user != null)
                    {
                        return user.Id;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Check Whether the User Plan is "Free account with limited access"
        /// if Yes : Ask for Payment for MR
        /// if No : Allow all access
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        public static bool CanAccessMR(int userId)
        {
            using (var uow = new UnitOfWork())
            {
                var user = uow.UserRepository.GetSingle(u => u.Id == userId, u => u.UserSubscription.Subscription, u => u.UserSubscription.SubscriptionService, u => u.UserSubscription.TempUserSubscription);

                if (DomainHelper.GetDomain() == DomainTypeEnum.India || DomainHelper.GetDomain() == DomainTypeEnum.US)
                {
                    if (user.UserSubscription.Subscription.Amount == null && user.UserSubscription.Subscription.IsBase && user.UserSubscription.Subscription.PlanTypeId == PlanTypeEnum.BasicFree)//==> "Free Account With Limited Access")
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}