using System;
using ADOPets.Web.Common.Helpers;

namespace ADOPets.Web.ViewModels.Surgery
{
    public class IndexViewModel
    {
        public IndexViewModel(Model.PetSurgery surgery)
        {
            Id = surgery.Id;
            Name = surgery.SurgeryId == null
                ? surgery.CustomSurgery
                : EnumHelper.GetResourceValueForEnumValue(surgery.SurgeryId);
            Date = surgery.SurgeryDate;
            Reason = surgery.Reason;
            PhysicianName = surgery.Physician;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Date { get; set; }

        public string Reason { get; set; }

        public string PhysicianName { get; set; }
    }
}