using System.ComponentModel.DataAnnotations;
using Model;
using ADOPets.Web.Resources;
using ADOPets.Web.Common.Helpers;
using System;

namespace ADOPets.Web.ViewModels.SMO
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {

        }
        public IndexViewModel(Model.SMORequest smoRequest)
        {
            Id = smoRequest.ID;
            SMOId = SMOHelper.GetFormatedSMOID(Id.ToString());
            Title = smoRequest.Title;
            PetId = smoRequest.Pet.Id;
            PetName = smoRequest.Pet.Name;
            PetType = smoRequest.Pet == null
                ? System.String.Empty
                : EnumHelper.GetResourceValueForEnumValue(smoRequest.Pet.PetTypeId);
            RequestDateNotification = smoRequest.RequestDate == null
             ? System.String.Empty : smoRequest.RequestDate.Value.ToString();//.ToShortDateString();

            RequestDate = smoRequest.RequestDate == null
                ? System.String.Empty : smoRequest.RequestDate.Value.ToShortDateString();

            OwnerName = smoRequest.Pet == null
                ? System.String.Empty : SMOHelper.GetOwnerNameByPetId(smoRequest.Pet.Id);
            // SMOHelper.GetOwnerName(smoRequest.User.FirstName, smoRequest.User.LastName);
            RequestStatus = smoRequest.SMORequestStatusId
                == null
                ? System.String.Empty
                : EnumHelper.GetResourceValueForEnumValue(smoRequest.SMORequestStatusId);
            InCompleteMedicalRecord = smoRequest.InCompleteMedicalRecord;
            SMOClosedOnDate = smoRequest.ClosedOn;
            IsSMOPaymentDone = smoRequest.IsSMOPaymentDone;
            SMOSubmittedBy = smoRequest.SMOSubmittedBy;
        }
        public string RequestDateNotification { get; set; }
        public int? SMOSubmittedBy { get; set; }

        public bool IsSMOPaymentDone { get; set; }

        public DateTime? SMOClosedOnDate { get; set; }

        public long Id { get; set; }

        public string SMOId { get; set; }

        [Display(Name = "Smo_Index_Title", ResourceType = typeof(Wording))]
        public string Title { get; set; }

        public int PetId { get; set; }

        [Display(Name = "Smo_Index_PetType", ResourceType = typeof(Wording))]
        public string PetType { get; set; }

        [Display(Name = "Smo_Index_PetName", ResourceType = typeof(Wording))]
        public string PetName { get; set; }

        [Display(Name = "Smo_Index_RequestDate", ResourceType = typeof(Wording))]
        public string RequestDate { get; set; }

        [Display(Name = "Smo_Index_RequestStatus", ResourceType = typeof(Wording))]
        public string RequestStatus { get; set; }

        [Display(Name = "Smo_Index_OwnerName", ResourceType = typeof(Wording))]
        public string OwnerName { get; set; }

        public Nullable<bool> InCompleteMedicalRecord { get; set; }
    }
}