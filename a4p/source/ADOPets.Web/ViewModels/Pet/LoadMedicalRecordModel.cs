using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using ExpressiveAnnotations.Attributes;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace ADOPets.Web.ViewModels.Pet
{
    public class LoadMedicalRecordModel
    {
        public LoadMedicalRecordModel()
        {

        }

        public LoadMedicalRecordModel(Model.Pet pet)
        {
            Id = pet.Id;
            PetName = pet.Name;
            PetType = pet.PetTypeId; 
            Conditions = pet.PetConditions.Select(i => new Condition.IndexViewModel(i)).ToList();
            Surgeries = pet.PetSurgeries.Select(i => new Surgery.IndexViewModel(i)).ToList();
            Medications = pet.PetMedications.Select(i => new Medication.IndexViewModel(i)).ToList();
            Allergies = pet.PetAllergies.Select(i => new Allergy.IndexViewModel(i)).ToList();

            Immunizations = pet.PetVaccinations.Select(i => new Immunization.IndexViewModel(i)).ToList();
            FoodPlans = pet.PetFoodPlans.Select(i => new FoodPlan.IndexViewModel(i)).ToList();
            Hospitalizations = pet.PetHospitalizations.Select(i => new Hospitalization.IndexViewModel(i)).ToList();
            Consultations = pet.PetConsultations.Select(i => new Consultation.IndexViewModel(i)).ToList();
        }

        public int Id { get; set; }
        public string PetName { get; set; }
        public PetTypeEnum PetType { get; set; }
        public List<Condition.IndexViewModel> Conditions { get; set; }
        public List<Surgery.IndexViewModel> Surgeries { get; set; }
        public List<Medication.IndexViewModel> Medications { get; set; }
        public List<Allergy.IndexViewModel> Allergies { get; set; }
        public List<Immunization.IndexViewModel> Immunizations { get; set; }
        public List<FoodPlan.IndexViewModel> FoodPlans { get; set; }
        public List<Hospitalization.IndexViewModel> Hospitalizations { get; set; }
        public List<Consultation.IndexViewModel> Consultations { get; set; }
    }
}