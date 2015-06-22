using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;
using ADOPets.Web.Common.Helpers;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class StartEConsultationModel
    {
        public StartEConsultationModel()
        {

        }

        public StartEConsultationModel(Model.EConsultation ec)
        {
            Id = ec.ID;
            PetId = ec.Pet.PetId;
            Name = ec.Pet.Name;
            PetType = ec.Pet == null
                ? System.String.Empty
                : EnumHelper.GetResourceValueForEnumValue(ec.Pet.PetTypeId);
            Condition = ec.TitleConsultation;
            date = Convert.ToDateTime(ec.BDateConsultation.Value.ToShortDateString());
            time = Convert.ToDateTime(ec.BTimeConsultation.Value.ToShortTimeString());
            Status = ec.EconsultationStatusId == null
                    ? System.String.Empty
                    : EnumHelper.GetResourceValueForEnumValue(ec.EconsultationStatusId);
            StatusId = (EConsultationStatusEnum)ec.EconsultationStatusId;
            TimeZone = (TimeZoneEnum)ec.VetTimezoneID;
            Symtom1 = ec.Symptoms1;
            Symtom2 = ec.Symptoms2;
            Symtom3 = ec.Symptoms3;
            VetId = ec.VetId;
            if (ec.EConsultationContactTypeId.HasValue)
            {
                ContactType = (EConsultationContactTypeEnum)ec.EConsultationContactTypeId;
                ContactValue = ec.EConsultationContactValue;
            }
            if (ec.User.Veterinarian != null)
            {
                VetSpecility = (VetSpecialityEnum)ec.User.Veterinarian.VetSpecialtyID;
            }
            else
            {
                VetSpecility = VetSpecialityEnum.Surgery;
            }                        
        }


        public int Id { get; set; }
        public int? PetId { get; set; }
        public string PetType { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }
        public DateTime date { get; set; }
        public DateTime time { get; set; }
        public string Status { get; set; }
        public EConsultationStatusEnum StatusId { get; set; }
        public TimeZoneEnum TimeZone { get; set; }
        public string Symtom1 { get; set; }
        public string Symtom2 { get; set; }
        public string Symtom3 { get; set; }
        public int? VetId { get; set; }
        public string VetFirstName { get; set; }
        public string VetLastName { get; set; }
        public string VetEmail { get; set; }
        public VetSpecialityEnum? VetSpecility { get; set; }
        public EConsultationContactTypeEnum? ContactType { get; set; }
        public string ContactValue { get; set; }
        public decimal? RoomId { get; set; }

        public void Map(Model.EConsultation econ)
        {
            econ.RDVDate = date;
            econ.RDVDateTime = time;
            econ.BDateConsultation = date;
            econ.BTimeConsultation = time;
            econ.EconsultationStatusId = StatusId;
        }

    }
}