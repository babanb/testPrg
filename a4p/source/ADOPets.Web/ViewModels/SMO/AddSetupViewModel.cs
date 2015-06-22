using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using System.Linq;
using System.Web;
using Model;

namespace ADOPets.Web.ViewModels.SMO
{
    public class AddSetupViewModel
    {
        public AddSetupViewModel()
        {

        }

        public AddSetupViewModel(SMORequest s,List<InvestigationViewModel> investigation)
        {
            Id = s.ID;
            Title = s.Title;
            PetId = Convert.ToInt32(s.PetId);
            OwnerId = Convert.ToInt32(s.UserId);
            Diagnosis = s.Diagnosis;
            DateOfOnSet = s.DateOfOnSet;
            Comments = s.Comments;
            Symptoms1 = s.Symptoms1;
            Symptoms2 = s.Symptoms2;
            Symptoms3 = s.Symptoms3;
            FirstOpinion = s.FirstOpinion;
            Question = s.Question;
            Investigations = investigation;
            SMOSubmittedBy = s.SMOSubmittedBy;
            
        }
        public int? SMOSubmittedBy { get; set; }

        public long Id { get; set; }

        [Display(Name = "Smo_Add_Title", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Smo_Add_TitleRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Title { get; set; }

        [Display(Name = "Smo_Add_SelectPet", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Smo_Add_SelectPetRequired", ErrorMessageResourceType = typeof(Wording))]
        public int PetId { get; set; }

        [Display(Name = "Smo_Add_SelectOwner", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Smo_Add_SelectOwnerRequired", ErrorMessageResourceType = typeof(Wording))]
        public int OwnerId { get; set; }

        [Display(Name = "Smo_Add_PetType", ResourceType = typeof(Wording))]
        public string PetType { get; set; }

        [Display(Name = "Smo_Add_PetName", ResourceType = typeof(Wording))]
        public string PetName { get; set; }

        [Required(ErrorMessageResourceName = "Smo_Add_DiagnosisRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Smo_Add_Diagnosis", ResourceType = typeof(Wording))]
        public string Diagnosis { get; set; }

        [Display(Name = "Smo_Add_DateofOnset", ResourceType = typeof(Wording))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> DateOfOnSet { get; set; }

        [Display(Name = "Smo_Add_Comments", ResourceType = typeof(Wording))]
        public string Comments { get; set; }

        public string Symptoms1 { get; set; }

        public string Symptoms2 { get; set; }

        public string Symptoms3 { get; set; }

        [Required(ErrorMessageResourceName = "Smo_Add_FirstMedicalOpioinRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Smo_Add_FirstMedicalOpioin", ResourceType = typeof(Wording))]
        public string FirstOpinion { get; set; }

        [Required(ErrorMessageResourceName = "Smo_Add_QuestionRequired", ErrorMessageResourceType = typeof(Wording))]
        [Display(Name = "Smo_Add_Question", ResourceType = typeof(Wording))]
        public string Question { get; set; }

        [Required]
        public bool authorizeveterinarians { get; set; }

        [Required]
        public bool certify { get; set; }

        public List<InvestigationViewModel> Investigations { get; set; }

        public List<Pet.IndexViewModel> Pets { get; set; }
    }
}