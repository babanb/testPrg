using ADOPets.Web.ViewModels.Profile;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ADOPets.Web.Resources;

namespace ADOPets.Web.ViewModels.SMO
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        { 
        }
       
        public DetailsViewModel(Model.SMORequest SMORequest)
        {
            SMOREquest = SMORequest;
            SMOREquest.PetId = SMOREquest.Pet.Id;
            Investigations = SMORequest.SMOInvestigations.Where(a => a.SMORequestID == SMORequest.ID).Select(a => a).ToList();
            SMOExpertsList = SMORequest.SMOExpertRelations.Where(a => a.SMORequestID == SMORequest.ID).Select(a => a).ToList();
            SMOId = "SM" + SMOREquest.ID;
            OwnerName = SMOREquest.User.FirstName.Value + " " + SMOREquest.User.LastName.Value;
            OwnerEmail = SMOREquest.User.Email.Value;
            ExpertCommittees = SMORequest.SMOExpertCommittees.Where(a => a.SMORequestId == SMORequest.ID).Select(a => a).ToList();
            lstExpertRel = SMORequest.SMOExpertRelations.Where(a => a.SMORequestID == SMORequest.ID).Select(a => new ExpertRelationViewModel(a)).ToList();
        }

        public DetailsViewModel(Model.SMORequest SMORequest,List<Model.Veterinarian> v)
        {
            SMOREquest = SMORequest;
            Investigations = SMORequest.SMOInvestigations.Where(a => a.SMORequestID == SMORequest.ID).Select(a => a).ToList();
            SMOExpertsList = SMORequest.SMOExpertRelations.Where(a => a.SMORequestID == SMORequest.ID).Select(a => a).ToList();
            foreach (Model.Veterinarian exp in v)
            {
                foreach (Model.SMOExpertRelation exp1 in SMOExpertsList)
                {
                    if (exp1.VetExpertID == exp.UserId)
                    {
                        exp1.User.Veterinarian = new Model.Veterinarian();
                        exp1.User.Veterinarian.VetSpecialtyID = exp.VetSpecialtyID;
                    }
                }
            }
            SMOId = "SM" + SMOREquest.ID;
            OwnerName = SMOREquest.User.FirstName.Value + " " + SMOREquest.User.LastName.Value;
            FilePath = "4timesheet.jpg";
            ExpertCommittees = SMORequest.SMOExpertCommittees.Where(a => a.SMORequestId == SMORequest.ID).Select(a => a).ToList();
        }

        public DetailsViewModel(Model.SMORequest SMORequest, string action, List<Model.Veterinarian> v)
        {
            SMOREquest = SMORequest;
            Investigations = SMORequest.SMOInvestigations.Select(a => a).ToList();
            SMOExpertsList = SMORequest.SMOExpertRelations.Select(a => a).ToList();
            foreach (Model.Veterinarian exp in v)
            {
                foreach (Model.SMOExpertRelation exp1 in SMOExpertsList)
                {
                    if (exp1.VetExpertID == exp.UserId)
                    {
                        exp1.User.Veterinarian = new Model.Veterinarian();
                        exp1.User.Veterinarian.VetSpecialtyID = exp.VetSpecialtyID;
                    }
                }
            }
            SMOId = "SM" + SMOREquest.ID;
            OwnerName = SMOREquest.User.FirstName.Value + " " + SMOREquest.User.LastName.Value;
        }
        public DetailsViewModel(Model.SMORequest SMORequest, List<SMODocumentViewModel> smoDocs)
        {
            SMOREquest = SMORequest;
            SMOREquest.PetId = SMOREquest.Pet.Id;
            Investigations = SMORequest.SMOInvestigations.Where(a => a.SMORequestID == SMORequest.ID).Select(a => a).ToList();
            SMOExpertsList = SMORequest.SMOExpertRelations.Where(a => a.SMORequestID == SMORequest.ID).Select(a => a).ToList();
            SMOId = "SM" + SMOREquest.ID;
            OwnerName = SMOREquest.User.FirstName + " " + SMOREquest.User.LastName;
            FilePath = "4timesheet.jpg";
            ExpertCommittees = SMORequest.SMOExpertCommittees.Where(a => a.SMORequestId == SMORequest.ID).Select(a => a).ToList();
            lstAttachment = smoDocs;
        }
        //public DetailsViewModel(Model.SMORequest SMORequest,List<VetExpert> listExperts)
        //{
        //    SMOREquest = SMORequest;
        //    Investigations = SMORequest.SMOInvestigations.Where(a => a.SMORequestID == SMORequest.ID).Select(a => a).ToList();
        //    SMOExpertsList = SMORequest.SMOExpertRelations.Where(a => a.SMORequestID == SMORequest.ID).Select(a => a).ToList();
        //    VetExperts = listExperts;

        //}


        public SMORequest SMOREquest { get; set; }
        public List<SMOInvestigation> Investigations { get; set; }
        public List<SMOExpertRelation> SMOExpertsList { get; set; }
        public List<VetExpert> VetExperts { get; set; }
        public SMOExpertCommittee SMOExpertCommittee { get; set; }
        public List<SMOExpertCommittee> ExpertCommittees { get; set; }
        public SMOExpertRelation SMOExpert { get; set; }
        public string SMOId { get; set; }
        public string OwnerName { get; set; }
        public string FilePath { get; set; }
        public int Expert { get; set; }
        //[Required]
        [Display(Name = "Smo_Edit_Response", ResourceType = typeof(Wording))]
        public string SMOExpertResponse { get; set; }

        //[Required]
        [Display(Name = "Smo_Edit_Conclusion", ResourceType = typeof(Wording))]
        public string SMOVetConclusion { get; set; }

        public List<SMODocumentViewModel> lstAttachment { get; set; }

        public string VetName { get; set; }

        public string RequestReason { get; set; }
        public string MedicalHistoryComment { get; set; }
        public string AdditionalInformation { get; set; }
        public List<ExpertRelationViewModel> lstExpertRel { get; set; }
        public string VetBio { get; set; }
        public string OwnerEmail { get; set; }

        public Model.SMOExpertRelation Map(DetailsViewModel model)
        {
            var smoexpert = new Model.SMOExpertRelation
            {
                ExpertResponse = new EncryptedText(model.SMOExpert.ExpertResponse),
                SMORequestID = model.SMOREquest.ID,
                VetExpertID = model.SMOExpert.VetExpertID,
                AssingedDate = (model.SMOExpert.AssingedDate == null) ? model.SMOExpert.AssingedDate : model.SMOExpert.AssingedDate.Value
                
            };
            return smoexpert;

        }
        public void Map(Model.SMOExpertRelation expert)
        {
            SMOExpertResponse = new EncryptedText(expert.ExpertResponse);
        }
        
    }
}