using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Implementations;
using ADOPets.Web.Common.Authentication;
using Model;
using System.Web.Security;

namespace ADOPets.Web.Common.Helpers
{
    public static class SMOHelper
    {
        public static string GetFormatedSMOID(string Id)
        {
            var ID = int.Parse(Id);
            var SMOString = Constants.SMOID;

            return string.Format("{0}{1}", SMOString, ID);
        }

        public static string GetOwnerName(string FirstName, string LastName)
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }

        public static decimal GetDefaultPlanAmount()
        {
            decimal amount = decimal.Parse(Constants.DefaultPlanAmount);
            return amount;
        }

        public static bool CanEditPet(int Id)
        {
            bool result = true;

            using (var uow = new UnitOfWork())
            {
                string smoIDForPet = "";
                try { smoIDForPet = HttpContext.Current.Session["EditSMORequestID"].ToString(); }
                catch { }

                if (!string.IsNullOrEmpty(smoIDForPet.ToString()))
                {
                    int smoId = Convert.ToInt32(smoIDForPet);
                    var smorequest = uow.SMORequestRepository.GetAll(a => a.PetId == Id && a.ID == smoId).Where(s => s.SMORequestStatusId != SMORequestStatusEnum.Complete && s.SMORequestStatusId != SMORequestStatusEnum.PaymentPending).FirstOrDefault();
                    if (smorequest != null)
                    {
                        if (smorequest.InCompleteMedicalRecord == false)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    var smorequest = uow.SMORequestRepository.GetAll(a => a.PetId == Id).Where(s => s.SMORequestStatusId != SMORequestStatusEnum.Complete && s.SMORequestStatusId != SMORequestStatusEnum.PaymentPending).FirstOrDefault();
                    if (smorequest != null)
                    {
                        if (smorequest.InCompleteMedicalRecord == false)
                        {
                            return false;
                        }
                    }
                }
            }
            return result;
        }

        public static bool CanDeletePet(int Id)
        {
            bool result = true;
            if (Roles.IsUserInRole(UserTypeEnum.OwnerAdmin.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianAdo.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianExpert.ToString()) || Roles.IsUserInRole(UserTypeEnum.VeterinarianLight.ToString()))
            {
                using (var uow = new UnitOfWork())
                {
                    var smorequest = uow.SMORequestRepository.GetAll(a => a.PetId == Id).Where(s => s.SMORequestStatusId != SMORequestStatusEnum.Complete && s.SMORequestStatusId != SMORequestStatusEnum.PaymentPending).FirstOrDefault();
                    if (smorequest != null)
                    {
                        result = false;
                    }

                }
            }
            return result;
        }
        public static string GetOwnerNameByPetId(int PetId)
        {
            var uow = new UnitOfWork();
            string owner = String.Empty;
            var ownername = uow.PetRepository.GetSingle(p => p.Id == PetId, p => p.Users);
            if (ownername != null)
            {
                if (ownername.IsDeleted == false)
                {
                    if (ownername.Users != null)
                    {
                        var User = ownername.Users.FirstOrDefault();
                        if (User != null)
                            owner = User.FirstName.Value + " " + User.LastName.Value;

                    }
                }
            }
            return owner;
        }

        public static bool CanAddSMO(string userId)
        {
            int id = Convert.ToInt32(userId);
            bool result = true;
            using (var uow = new UnitOfWork())
            {
                var dbPets = uow.PetRepository.GetAll(p => p.Users.Any(u => u.Id == id), navigationProperties: p => p.Users);
                if (dbPets == null || dbPets.Count() == 0)
                {
                    result = false;
                }

            }


            return result;
        }
    }
}