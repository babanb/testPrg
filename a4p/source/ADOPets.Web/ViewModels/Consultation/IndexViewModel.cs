using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADOPets.Web.Common.Helpers;
namespace ADOPets.Web.ViewModels.Consultation
{
    public class IndexViewModel
    {
        public IndexViewModel()
        { }

        public IndexViewModel(Model.PetConsultation consultation)
        {
            Id = consultation.Id;
            Name = consultation.ConsultationId == null
                ? consultation.CustomConsultation
                : EnumHelper.GetResourceValueForEnumValue(consultation.ConsultationId);
            Reason = consultation.Reason;
            VisitDate = consultation.VisitDate;
            Comment = consultation.Comment;
           
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime VisitDate { get; set; }

        public string Reason { get; set; }
        public string Comment { get; set; }

       
        
    }
}