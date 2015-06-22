using System;
using ADOPets.Web.Common.Helpers;

namespace ADOPets.Web.ViewModels.Immunization
{
    public class IndexViewModel
    {
        public IndexViewModel(Model.PetVaccination immunization)
        {
            Id = immunization.Id;
            Immunization = immunization.VaccinationId == null
                ? immunization.CustomVaccination
                : EnumHelper.GetResourceValueForEnumValue(immunization.VaccinationId);
            InjectionDate = immunization.InjectionDate;
            HospitalName = immunization.HospitalName;
            SerialNumber = immunization.SerialNumber;
            Comment = immunization.Comment;
        }

        public int Id { get; set; }
        public string Immunization { get; set; }
        public DateTime InjectionDate { get; set; }
        public string HospitalName { get; set; }
        public string SerialNumber { get; set; }
        public string Comment { get; set; }
    }
}