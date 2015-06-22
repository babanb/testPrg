using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using System.Collections.Generic;
using ADOPets.Web.ViewModels.SMO;

namespace ADOPets.Web.ViewModels.Profile
{
    public class IndexViewModel
    {

        public IndexViewModel(Model.User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            UserType = user.UserTypeId;
        }

        //public IndexViewModel(int id, string Firstname, string Lastname, string Email, State state, Country country, bool Checkedstatus)
        //{
        //    this.Id = id;
        //   this.FirstName = Firstname;
        //   this.LastName = Lastname;
        //   this.Email = Email;
        //   this.Location = (state != null && country != null) ? state.Name + "" + country.Name:"";
        //   CheckedStatus = Checkedstatus;

        //}

        [Display(Name = "Profile_Index_Id", ResourceType = typeof(Wording))]
        public int Id { get; set; }

        [Display(Name = "Profile_Index_FirstName", ResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Profile_Index_LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "Profile_Index_Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Profile_Index_UserType", ResourceType = typeof(Wording))]
        public UserTypeEnum UserType { get; set; }

        [Display(Name = "Profile_Index_Location", ResourceType = typeof(Wording))]
        public string Location { get; set; }

        public bool CheckedStatus { get; set; }


    }

    public class VetExpertModel
    {
        public List<VetExpert> listOfVetExperts { get; set; }
        public Model.SMOExpertRelation Map(int SMOID, int expertId)
        {
            var smo = new Model.SMOExpertRelation
            {
                SMORequestID = SMOID,
                VetExpertID = expertId,
                AssingedDate = DateTime.Now

            };
            return smo;

        }
        public Model.SMOExpertRelation Map(ExpertRelationViewModel model)
        {
            var smo = new Model.SMOExpertRelation
            {
                SMORequestID = Int64.Parse(model.SMOId),
                VetExpertID = model.VetExpertID,
                AssingedDate = DateTime.Now

            };
            return smo;

        }
    }

    public class VetExpert
    {
        public VetExpert()
        {


        }

        public VetExpert(SMOExpertRelation expert)
        {
            userId = expert.User.Id;
            ExpertResponse = expert.ExpertResponse;
        }
        public VetExpert(SMOExpertRelation expert, List<SMODocumentViewModel> smoDocs)
        {
            userId = expert.User.Id;
            ExpertResponse = expert.ExpertResponse;
            lstAttachment = smoDocs;
        }
        public VetExpert(int SMOId, int id, string Firstname, string Lastname, string Email, StateEnum? state, CountryEnum? country, bool Checkedstatus, string ExpertResponse, VetSpecialityEnum? speciality)
        {
            this.SMOId = SMOId;
            this.Id = id;
            this.FirstName = Firstname;
            this.LastName = Lastname;
            this.Email = Email;
            this.Location = (state != null || country != null) ? state + "" + country : "";
            CheckedStatus = Checkedstatus;
            this.ExpertResponse = ExpertResponse;
            this.Speciality = speciality != null ? speciality : null;
        }
        [Display(Name = "Profile_Index_Id", ResourceType = typeof(Wording))]
        public int Id { get; set; }

        public int SMOId { get; set; }
        public int userId { get; set; }

        [Display(Name = "Profile_Index_FirstName", ResourceType = typeof(Wording))]
        public string FirstName { get; set; }

        [Display(Name = "Profile_Index_LastName", ResourceType = typeof(Wording))]
        public string LastName { get; set; }

        [Display(Name = "Profile_Index_Email", ResourceType = typeof(Wording))]
        public string Email { get; set; }

        [Display(Name = "Profile_Index_UserType", ResourceType = typeof(Wording))]
        public UserTypeEnum UserType { get; set; }

        [Display(Name = "Profile_Index_Location", ResourceType = typeof(Wording))]
        public string Location { get; set; }

        // [Display(Name = "Profile_Index_ExpertResponse", ResourceType = typeof(Wording))]
        public string ExpertResponse { get; set; }
        public VetSpecialityEnum? Speciality { get; set; }
        public bool CheckedStatus { get; set; }
        public List<SMODocumentViewModel> lstAttachment { get; set; }

    }


}