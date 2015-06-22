using System;
using ADOPets.Web.Common.Helpers;

namespace ADOPets.Web.ViewModels.Hospitalization
{
    public class IndexViewModel
    {
        public IndexViewModel()
        { }

        public IndexViewModel(Model.PetHospitalization hospitalization)
        {
            Id = hospitalization.Id;
            HospitalName = hospitalization.HospitalName;
            Reason = hospitalization.Reason;
            AdmissionDate = hospitalization.StartDate;
            EndDate = hospitalization.EndDate;
            UrgentCareVisit = hospitalization.Urgent;
            Comment = hospitalization.Comment;

        }

        public int Id { get; set; }
        public string HospitalName { get; set; }
        public string Reason { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? UrgentCareVisit { get; set; }
        public string Comment { get; set; }
    }
}