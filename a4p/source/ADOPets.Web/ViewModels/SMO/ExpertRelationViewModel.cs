using System.ComponentModel.DataAnnotations;
using Model;
using ADOPets.Web.Resources;
using ADOPets.Web.Common.Helpers;
using System;
using System.Collections.Generic;

namespace ADOPets.Web.ViewModels.SMO
{
    public class ExpertRelationViewModel
    {
        public ExpertRelationViewModel()
        {

        }
        public ExpertRelationViewModel(Model.SMOExpertRelation smoExpert)
        {
            IsFinal = smoExpert.IsFinal;
            ExpertRelId = smoExpert.ID;
            Id = smoExpert.SMORequest.ID;
            SMOId = SMOHelper.GetFormatedSMOID(Id.ToString());
            Title = smoExpert.SMORequest.Title;
            PetId = smoExpert.SMORequest.Pet.Id;
            PetName = smoExpert.SMORequest.Pet.Name;
            PetType = smoExpert.SMORequest.Pet == null
                ? System.String.Empty
                : EnumHelper.GetResourceValueForEnumValue(smoExpert.SMORequest.Pet.PetTypeId);

            RequestDate = smoExpert.SMORequest.RequestDate == null
                ? System.String.Empty : smoExpert.SMORequest.RequestDate.Value.ToShortDateString();
            RequestStatus = smoExpert.SMORequest.SMORequestStatusId
                == null
                ? System.String.Empty
                : EnumHelper.GetResourceValueForEnumValue(smoExpert.SMORequest.SMORequestStatusId);
            InCompleteMedicalRecord = smoExpert.SMORequest.InCompleteMedicalRecord;
            ExpertResponse = smoExpert.ExpertResponse;
            VetExpertID = smoExpert.VetExpertID;
            User = smoExpert.User;
        }

        public ExpertRelationViewModel(Model.SMOExpertRelation smoExpert, Model.User smoUser)
        {
            ExpertRelId = smoExpert.ID;
            Id = smoExpert.SMORequest.ID;
            SMOId = SMOHelper.GetFormatedSMOID(Id.ToString());
            Title = smoExpert.SMORequest.Title;
            PetId = smoExpert.SMORequest.Pet.Id;
            PetName = smoExpert.SMORequest.Pet.Name;
            PetType = smoExpert.SMORequest.Pet == null
                ? System.String.Empty
                : EnumHelper.GetResourceValueForEnumValue(smoExpert.SMORequest.Pet.PetTypeId);

            RequestDate = smoExpert.SMORequest.RequestDate == null
                ? System.String.Empty : smoExpert.SMORequest.RequestDate.Value.ToShortDateString();
            OwnerName = smoUser != null ? SMOHelper.GetOwnerName(smoUser.FirstName, smoUser.LastName) : String.Empty;
            RequestStatus = smoExpert.SMORequest.SMORequestStatusId
                == null
                ? System.String.Empty
                : EnumHelper.GetResourceValueForEnumValue(smoExpert.SMORequest.SMORequestStatusId);
            InCompleteMedicalRecord = smoExpert.SMORequest.InCompleteMedicalRecord;
        }
   
        public ExpertRelationViewModel(Model.SMOExpertRelation smoExpert, List<SMODocumentViewModel> smoDocs)
        {
            ExpertRelId = smoExpert.ID;
            Id = smoExpert.SMORequest.ID;
            SMOId = SMOHelper.GetFormatedSMOID(Id.ToString());
            Title = smoExpert.SMORequest.Title;
            PetId = smoExpert.SMORequest.Pet.Id;
            PetName = smoExpert.SMORequest.Pet.Name;
            PetType = smoExpert.SMORequest.Pet == null
                ? System.String.Empty
                : EnumHelper.GetResourceValueForEnumValue(smoExpert.SMORequest.Pet.PetTypeId);

            RequestDate = smoExpert.SMORequest.RequestDate == null
                ? System.String.Empty : smoExpert.SMORequest.RequestDate.Value.ToShortDateString();
            RequestStatus = smoExpert.SMORequest.SMORequestStatusId
                == null
                ? System.String.Empty
                : EnumHelper.GetResourceValueForEnumValue(smoExpert.SMORequest.SMORequestStatusId);
            InCompleteMedicalRecord = smoExpert.SMORequest.InCompleteMedicalRecord;
            ExpertResponse = smoExpert.ExpertResponse;
            lstAttachment = smoDocs;
        }

        public ExpertRelationViewModel(Model.SMOExpertRelation smoExpert, Model.User smoUser, DateTime? closedDate)
        {
            ExpertRelId = smoExpert.ID;
            Id = smoExpert.SMORequest.ID;
            SMOId = SMOHelper.GetFormatedSMOID(Id.ToString());
            Title = smoExpert.SMORequest.Title;
            PetId = smoExpert.SMORequest.Pet.Id;
            PetName = smoExpert.SMORequest.Pet.Name;
            PetType = smoExpert.SMORequest.Pet == null
                ? System.String.Empty
                : EnumHelper.GetResourceValueForEnumValue(smoExpert.SMORequest.Pet.PetTypeId);

            RequestDateNotification = smoExpert.AssingedDate == null
            ? (smoExpert.SMORequest.RequestDate == null
            ? System.String.Empty : smoExpert.SMORequest.RequestDate.Value.ToString()) : smoExpert.AssingedDate.Value.ToString();

            RequestDate = smoExpert.SMORequest.RequestDate == null
                ? System.String.Empty : smoExpert.SMORequest.RequestDate.Value.ToShortDateString();
            OwnerName = smoUser != null ? SMOHelper.GetOwnerName(smoUser.FirstName, smoUser.LastName) : String.Empty;
            RequestStatus = smoExpert.SMORequest.SMORequestStatusId
                == null
                ? System.String.Empty
                : EnumHelper.GetResourceValueForEnumValue(smoExpert.SMORequest.SMORequestStatusId);
            InCompleteMedicalRecord = smoExpert.SMORequest.InCompleteMedicalRecord;

            SMOClosedOnDate = closedDate;
        }

        public Boolean IsFinal { get; set; }
        
        public string RequestDateNotification { get; set; }
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

        [Display(Name = "Profile_Index_lblExpertResponse", ResourceType = typeof(Wording))]
        public string ExpertResponse { get; set; }

        public Nullable<bool> InCompleteMedicalRecord { get; set; }

        public long ExpertRelId { get; set; }

        public List<SMODocumentViewModel> lstAttachment { get; set; }

        public ExpertBioData expertBioData { get; set; }

        public Nullable<int> VetExpertID { get; set; }

        public virtual User User { get; set; }

        public void Map(Model.SMOExpertRelation expertRel)
        {
            expertRel.ExpertResponse = new EncryptedText(ExpertResponse);
            expertRel.ID = ExpertRelId;
        }

    }



}