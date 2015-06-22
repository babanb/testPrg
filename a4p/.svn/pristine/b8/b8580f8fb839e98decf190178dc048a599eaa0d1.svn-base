using System;
using Model;
using Repository.Implementations;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        void Track(int userId, string ipAddress);

        LoginRepository LoginRepository { get; }
        PetRepository PetRepository { get; }
        IGenericRepository<PetType> PetTypeRepository { get; }
        UserRepository UserRepository { get; }

        IGenericRepository<PetDocument> PetDocumentRepository { get; }

        IGenericRepository<PetHealthMeasure> PetHealthMeasureTrackerRepository { get; }

        IGenericRepository<PetCondition> PetConditionRepository { get; }
        IGenericRepository<PetHospitalization> PetHospitalizationRepository { get; }
        IGenericRepository<PetConsultation> PetConsultationRepository { get; }
        IGenericRepository<PetAllergy> PetAllergyRepository { get; }
        IGenericRepository<PetFoodPlan> PetFoodPlanRepository { get; }
        IGenericRepository<PetVaccination> PetVaccinationRepository { get; }
        IGenericRepository<PetMedication> PetMedicationRepository { get; }
        IGenericRepository<PetSurgery> PetSurgeryRepository { get; }

        IGenericRepository<State> StateRepository { get; }
        IGenericRepository<Farmer> FarmerRepository { get; }

        IGenericRepository<Insurance> InsuranceRepository { get; }
        IGenericRepository<Veterinarian> VeterinarianRepository { get; }

        IGenericRepository<Contact> ContactRepository { get; }

        IGenericRepository<BloodGroupType> BloodGroupTypeRepository { get; }
        IGenericRepository<Subscription> SubscriptionRepository { get; }

        IGenericRepository<Calendar> CalendarRepository { get; }
        MesssageRepository MessageRepository { get; }

        TimeZoneRepository TimeZoneRepository { get; }

        IGenericRepository<EConsultation> EConsultationRepository { get; }
        IGenericRepository<EconsultationData> EConsultationDataRepository { get; }
        IGenericRepository<EconsultationMsg> EConsultationMsgRepository { get; }
        IGenericRepository<EconsultationRoom> EConsultationRoomRepository { get; }
        IGenericRepository<EConsultationStatus> EConsultationStatuRepository { get; }
        IGenericRepository<EconsultationSummary> EConsultationSummaryRepository { get; }
        IGenericRepository<EconsultationUser> EConsultationUserRepository { get; }

        IGenericRepository<Notification> NotificationRepository { get; }

        IGenericRepository<SMORequest> SMORequestRepository { get; }
        IGenericRepository<PetContact> PetContactRepository { get; }
        IGenericRepository<SMOInvestigation> SMOInvestigationRepository { get; }
        IGenericRepository<UserSubscription> UserSubscriptionRepository { get; }
        IGenericRepository<BillingInformation> BillingInformationRepository { get; }
        IGenericRepository<Gallery> GalleryRepository { get; }

        IGenericRepository<ShareContact> ShareContactRepository { get; }

        IGenericRepository<VideoGallery> VideoGalleryRepository { get; }
        IGenericRepository<SMOExpertRelation> SMOExpertRepository { get; }
        IGenericRepository<AlbumGallery> AlbumGalleryRepository { get; }
        IGenericRepository<UserSubscriptionHistory> UserSubscriptionHistoryRepository { get; }
        IGenericRepository<SMOExpertCommittee> SMOExpertCommitteeRepository { get; }
        IGenericRepository<SMODocument> SMODocumentRepository { get; }
        IGenericRepository<ExpertBioData> ExpertBioDataRepository { get; }
        IGenericRepository<TempUserSubscription> TempUserSubscriptionRepository { get; }
        IGenericRepository<ShareCategoryType> ShareCategoryTypeRepository { get; }
        IGenericRepository<User> UsersRepository { get; }
        IGenericRepository<VetSpeciality> VetSpecialityRepository { get; }
        IGenericRepository<UserType> UserTypeRepository { get; }
        IGenericRepository<EconsultDocument> EconsultDocumentRepository { get; }
        IGenericRepository<Center> CenterRepository { get; }
        IGenericRepository<ShareCategoryTypeContact> ShareCategoryTypeContactRepository { get; }
        IGenericRepository<SharePetInformation> SharePetInformationRepository { get; }
        IGenericRepository<GalleryComment> GalleryCommentRepository { get; }
        IGenericRepository<GalleryCommentReply> GalleryCommentReplyRepository { get; }
        IGenericRepository<GalleryLike> GalleryLikeRepository { get; }
        IGenericRepository<SharePetInfoCommunity> SharePetInfoCommunityRepository { get; }
    }
}
