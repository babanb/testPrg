using System.Collections.Generic;
using System.Linq;
using ADOPets.Web.Common.Enums;


namespace ADOPets.Web.ViewModels.HealthHistory
{
    public class HealthHistoryViewModel
    {
        public HealthHistoryViewModel()
        {
            
        }

        public HealthHistoryViewModel(Model.Pet pet, MedicalRecordTypeEnum mRType, int hHType)
        {
            PetId = pet.Id;
            Current = mRType == MedicalRecordTypeEnum.HealthHistory
                ? (HealthHistoryTypeEnum) hHType
                : HealthHistoryTypeEnum.Condition;

            Conditions = pet.PetConditions.Where(c => c.IsDeleted == false).OrderByDescending(c => c.VisitDate).Select(c => new Condition.IndexViewModel(c));
            Surgeries = pet.PetSurgeries.Where(c => c.IsDeleted == false).OrderByDescending(c => c.SurgeryDate).Select(s => new Surgery.IndexViewModel(s));
            Medications = pet.PetMedications.Where(c => c.IsDeleted == false).OrderByDescending(c => c.VisitDate).Select(s => new Medication.IndexViewModel(s));
            Allergies = pet.PetAllergies.Where(c => c.IsDeleted == false).OrderByDescending(c => c.StartDate).Select(s => new Allergy.IndexViewModel(s));
            Immunizations = pet.PetVaccinations.Where(c => c.IsDeleted == false).OrderByDescending(c => c.InjectionDate).Select(s => new Immunization.IndexViewModel(s));
            Hospitalizations = pet.PetHospitalizations.Where(c => c.IsDeleted == false).OrderByDescending(c => c.StartDate).Select(s => new Hospitalization.IndexViewModel(s));
            FoodPlans = pet.PetFoodPlans.Where(c => c.IsDeleted == false).OrderByDescending(c => c.FoodName).Select(s => new FoodPlan.IndexViewModel(s));
            Consultations = pet.PetConsultations.Where(c => c.IsDeleted == false).OrderByDescending(c => c.VisitDate).Select(s => new Consultation.IndexViewModel(s));
        }

        public int PetId { get; set; }
        public HealthHistoryTypeEnum Current { get; set; }
        public IEnumerable<Condition.IndexViewModel> Conditions { get; set; }
        public IEnumerable<Surgery.IndexViewModel> Surgeries { get; set; }
        public IEnumerable<Medication.IndexViewModel> Medications { get; set; }
        public IEnumerable<Allergy.IndexViewModel> Allergies { get; set; }
        public IEnumerable<Immunization.IndexViewModel> Immunizations { get; set; }
        public IEnumerable<Hospitalization.IndexViewModel> Hospitalizations { get; set; }
        public IEnumerable<FoodPlan.IndexViewModel> FoodPlans { get; set; }
        public IEnumerable<Consultation.IndexViewModel> Consultations { get; set; }
        
    }

}