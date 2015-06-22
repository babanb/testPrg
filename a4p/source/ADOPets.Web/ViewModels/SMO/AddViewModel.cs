using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using System.Linq;
using System.Web;
using Model;
using System.Web.Security;

namespace ADOPets.Web.ViewModels.SMO
{
    public class AddViewModel
    {
        public AddViewModel()
        {
        }

        public SMORequestStatusEnum smoRequestStatus {get;set;}
        public bool IsSMOPaymentDone { get; set; }
        public int? SMOSubmittedBy { get; set; }

        public long Id { get; set; }

        [Display(Name = "Smo_Add_Plan", ResourceType = typeof(Wording))]
        public string Plan { get; set; }

        public AddSetupViewModel objSetup { get; set; }

        public AddBillingViewModel objBilling { get; set; }

        [Display(Name = "Smo_Add_VetComment", ResourceType = typeof(Wording))]
        public string VetComment { get; set; }

        [Display(Name = "Smo_Add_ValidatedOn", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ValidatedOn { get; set; }

        [Display(Name = "Smo_Add_ClosedOn", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> ClosedOn { get; set; }

        [Display(Name = "Smo_Add_InCompleteMedicalRecord", ResourceType = typeof(Wording))]
        public Nullable<bool> InCompleteMedicalRecord { get; set; }

        [Display(Name = "Smo_Add_IsDeleted", ResourceType = typeof(Wording))]
        public Nullable<bool> IsDeleted { get; set; }

        [Display(Name = "Smo_Add_UserId", ResourceType = typeof(Wording))]
        public Nullable<int> UserId { get; set; }

        [Display(Name = "Smo_Add_RequestDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> RequestDate { get; set; }

        [Display(Name = "Smo_Add_RequestStatus", ResourceType = typeof(Wording))]
        public string RequestStatus { get; set; }


        public Model.SMORequest Map()
        {
            var smo = new Model.SMORequest
            {
                ID = Id,
                ClosedOn = (ClosedOn == null) ? ClosedOn : ClosedOn.Value,
                Comments = new EncryptedText(objSetup.Comments),
                DateOfOnSet = (objSetup.DateOfOnSet == null) ? objSetup.DateOfOnSet : objSetup.DateOfOnSet.Value,
                Diagnosis = new EncryptedText(objSetup.Diagnosis),
                FirstOpinion = new EncryptedText(objSetup.FirstOpinion),
                InCompleteMedicalRecord = InCompleteMedicalRecord,
                IsDeleted = IsDeleted,
                PetId = objSetup.PetId,
                Question = new EncryptedText(objSetup.Question),
                SMORequestStatusId = smoRequestStatus,
                Symptoms1 = new EncryptedText(objSetup.Symptoms1),
                Symptoms2 = new EncryptedText(objSetup.Symptoms2),
                Symptoms3 = new EncryptedText(objSetup.Symptoms3),
                Title = objSetup.Title,
                UserId = UserId,
                RequestDate = DateTime.Now,
                ValidatedOn = (ValidatedOn == null) ? ValidatedOn : ValidatedOn.Value,
                VetComment = new EncryptedText(VetComment),
                SMOSubmittedBy = objSetup.SMOSubmittedBy,
                IsSMOPaymentDone = IsSMOPaymentDone
            };
            return smo;
        }

        public void Map(Model.SMORequest request)
        {
            VetComment = new EncryptedText(request.VetComment);
            ClosedOn = request.ClosedOn;
            RequestStatus = request.SMORequestStatusId.Value.ToString();
            RequestDate = request.DateOfOnSet;
        }

        public void MapSmo(Model.SMORequest request)
        {
            request.SMORequestStatusId = SMORequestStatusEnum.Validated;
            request.ValidatedOn = DateTime.Now;
            request.DateOfOnSet = RequestDate;
        }
    }
}