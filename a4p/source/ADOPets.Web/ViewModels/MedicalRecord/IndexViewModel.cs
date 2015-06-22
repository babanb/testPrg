using System.Linq;
using ADOPets.Web.Common.Enums;

namespace ADOPets.Web.ViewModels.MedicalRecord
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            
        }

        public IndexViewModel(Model.Pet pet, MedicalRecordTypeEnum mRType, int mRSubType)
        {
            PetId = pet.Id;
            CurrentMedicalRecord = mRType;
            CurrentMedicalRecordSubType = mRSubType;

            HealthHistory = new HealthHistory.HealthHistoryViewModel(pet, mRType, mRSubType);
            HealthMeasureTracker = new HealthMeasureTracker.HealthMeasureTrackerViewModel(pet, mRType, mRSubType);
            Document = new Document.DocumentViewModel(pet, mRType, mRSubType);
        }


        public int PetId { get; set; }

        public MedicalRecordTypeEnum CurrentMedicalRecord { get; set; }

        public int CurrentMedicalRecordSubType { get; set; }


        public HealthHistory.HealthHistoryViewModel HealthHistory { get; set; }

        public HealthMeasureTracker.HealthMeasureTrackerViewModel HealthMeasureTracker { get; set; }

        public Document.DocumentViewModel Document { get; set; }
    }
}