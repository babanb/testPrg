using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.SMO
{
    public class InvestigationViewModel
    {
        public InvestigationViewModel()
        { }

        public InvestigationViewModel(SMOInvestigation i)
        {
            SMORequestID = i.SMORequestID;
            TestType = i.TestType;
            TestDate = i.TestDate;
            TestDescription = i.TestDescription;
            ID = i.ID;
            IsDeleted = i.IsDeleted;
        }
        public long ID { get; set; }

        public Nullable<long> SMORequestID { get; set; }

        public string TestType { get; set; }


        [Display(Name = "Smo_Add_TestName", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Smo_Add_TestNameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string TestDescription { get; set; }

        [Required(ErrorMessageResourceName = "Smo_Add_TestDateRequired", ErrorMessageResourceType = typeof(Wording))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Smo_Add_TestDate", ResourceType = typeof(Wording))]
        public Nullable<System.DateTime> TestDate { get; set; }

        public Nullable<bool> IsDeleted { get; set; }


        public Model.SMOInvestigation Map()
        {
            var smoinvestigation = new Model.SMOInvestigation
            {
                TestDescription = new EncryptedText(TestDescription),
                TestType = new EncryptedText(TestType),
                SMORequestID = SMORequestID,
                TestDate = (TestDate == null) ? TestDate : TestDate.Value,
                IsDeleted = IsDeleted
            };
            return smoinvestigation;

        }
    }
}