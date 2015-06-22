using System;
using Infrastructure;
using Model;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private ADOPetsEntities context = new ADOPetsEntities();

        private PetRepository petRepository;
        private IGenericRepository<PetType> petTypeRepository;
        private UserRepository userRepository;
        private IGenericRepository<PetDocument> petDocumentRepository;
        private IGenericRepository<PetCondition> petConditionRepository;
        private IGenericRepository<PetSurgery> petSurgeryRepository;
        private IGenericRepository<PetHospitalization> petHospitalizationRepository;
        private IGenericRepository<PetConsultation> petConsultationRepository;
        private IGenericRepository<PetAllergy> petAllergyRepository;
        private IGenericRepository<PetFoodPlan> petFoodPlanRepository;
        private IGenericRepository<PetVaccination> petVaccinationRepository;
        private IGenericRepository<PetMedication> petMedicationRepository;
        private IGenericRepository<State> stateRepository;
        private IGenericRepository<Farmer> farmerRepository;
        private IGenericRepository<PetHealthMeasure> petHealthMeasureTrackerRepository;
        private IGenericRepository<Insurance> insuranceRepository;
        private IGenericRepository<Veterinarian> veterinarianRepository;
        private IGenericRepository<Contact> contactRepository;
        private LoginRepository loginRepository;
        private IGenericRepository<BloodGroupType> bloodGroupTypeRepository;
        private IGenericRepository<Subscription> subscriptionRepository;
        private IGenericRepository<Calendar> calendarRepository;
        private MesssageRepository messageRepository;
        private TimeZoneRepository timeZoneRepository;

        private IGenericRepository<EConsultation> eConsultationRepository;
        private IGenericRepository<EconsultationData> eConsultationDataRepository;
        private IGenericRepository<EconsultationMsg> eConsultationMsgRepository;
        private IGenericRepository<EconsultationRoom> eConsultationRoomRepository;
        private IGenericRepository<EConsultationStatus> eConsultationStatuRepository;
        private IGenericRepository<EconsultationSummary> eConsultationSummaryRepository;
        private IGenericRepository<EconsultationUser> eConsultationUserRepository;
        private IGenericRepository<Notification> notificationRepository;
        private IGenericRepository<SMORequest> smoRequestRepository;
        private IGenericRepository<PetContact> petContactRepository;
        private IGenericRepository<Gallery> galleryRepository;
        private IGenericRepository<ShareContact> shareContactRepository;
        private IGenericRepository<VideoGallery> videoGalleryRepository;

        private IGenericRepository<SMOInvestigation> smoInvestigationRepository;
        private IGenericRepository<UserSubscription> userSubscriptionRepository;
        private IGenericRepository<BillingInformation> billingInformationRepository;
        private IGenericRepository<UserSubscriptionHistory> userSubscriptionHistoryRepository;

        private IGenericRepository<SMOExpertRelation> smoExpertRepository;
        private IGenericRepository<AlbumGallery> albumGalleryRepository;
        private IGenericRepository<SMOExpertCommittee> smoexpertCommitteeRepository;
        private IGenericRepository<SMODocument> smoDocumentRepository;
        private IGenericRepository<ExpertBioData> expertBioDataRepository;

        private IGenericRepository<TempUserSubscription> tempUserSubscriptionRepository;
        private IGenericRepository<User> usersRepository;

        private IGenericRepository<ShareCategoryType> shareCategoryTypeRepository;
        private IGenericRepository<VetSpeciality> vetSpecialityRepository;
        private IGenericRepository<UserType> userTypeRepository;

        private IGenericRepository<EconsultDocument> econsultDocumentRepository;
        private IGenericRepository<Center> centerRepository;
        private IGenericRepository<ShareCategoryTypeContact> shareCategoryTypeContactRepository;
        private IGenericRepository<SharePetInformation> sharePetInformationRepository;
        private IGenericRepository<GalleryComment> galleryCommentRepository;
        private IGenericRepository<GalleryCommentReply> galleryCommentReplyRepository;
        private IGenericRepository<GalleryLike> galleryLikeRepository;
        private IGenericRepository<SharePetInfoCommunity> sharePetInfoCommunityRepository;

        private bool disposed = false;
        public IGenericRepository<GalleryComment> GalleryCommentRepository
        {
            get { return galleryCommentRepository ?? (galleryCommentRepository = new GenericRepository<GalleryComment>(context)); }
        }
        public IGenericRepository<GalleryCommentReply> GalleryCommentReplyRepository
        {
            get { return galleryCommentReplyRepository ?? (galleryCommentReplyRepository = new GenericRepository<GalleryCommentReply>(context)); }
        }
        public IGenericRepository<GalleryLike> GalleryLikeRepository
        {
            get { return galleryLikeRepository ?? (galleryLikeRepository = new GenericRepository<GalleryLike>(context)); }
        }
        public IGenericRepository<SharePetInfoCommunity> SharePetInfoCommunityRepository
        {
            get { return sharePetInfoCommunityRepository ?? (sharePetInfoCommunityRepository = new GenericRepository<SharePetInfoCommunity>(context)); }
        }

        public IGenericRepository<ShareCategoryTypeContact> ShareCategoryTypeContactRepository
        {
            get { return shareCategoryTypeContactRepository ?? (shareCategoryTypeContactRepository = new GenericRepository<ShareCategoryTypeContact>(context)); }
        }

        public IGenericRepository<User> UsersRepository
        {
            get { return usersRepository ?? (usersRepository = new GenericRepository<User>(context)); }
        }

        public PetRepository PetRepository
        {
            get { return petRepository ?? (petRepository = new PetRepository(context)); }
        }

        public IGenericRepository<PetType> PetTypeRepository
        {
            get { return petTypeRepository ?? (petTypeRepository = new GenericRepository<PetType>(context)); }
        }

        public UserRepository UserRepository
        {
            get { return userRepository ?? (userRepository = new UserRepository(context)); }
        }

        public IGenericRepository<PetDocument> PetDocumentRepository
        {
            get { return petDocumentRepository ?? (petDocumentRepository = new GenericRepository<PetDocument>(context)); }
        }

        public IGenericRepository<PetHealthMeasure> PetHealthMeasureTrackerRepository
        {
            get { return petHealthMeasureTrackerRepository ?? (petHealthMeasureTrackerRepository = new GenericRepository<PetHealthMeasure>(context)); }
        }

        public IGenericRepository<PetCondition> PetConditionRepository
        {
            get { return petConditionRepository ?? (petConditionRepository = new GenericRepository<PetCondition>(context)); }
        }

        public IGenericRepository<PetSurgery> PetSurgeryRepository
        {
            get { return petSurgeryRepository ?? (petSurgeryRepository = new GenericRepository<PetSurgery>(context)); }
        }

        public IGenericRepository<PetHospitalization> PetHospitalizationRepository
        {
            get { return petHospitalizationRepository ?? (petHospitalizationRepository = new GenericRepository<PetHospitalization>(context)); }
        }

        public IGenericRepository<PetConsultation> PetConsultationRepository
        {
            get { return petConsultationRepository ?? (petConsultationRepository = new GenericRepository<PetConsultation>(context)); }
        }

        public IGenericRepository<PetAllergy> PetAllergyRepository
        {
            get { return petAllergyRepository ?? (petAllergyRepository = new GenericRepository<PetAllergy>(context)); }
        }

        public IGenericRepository<PetFoodPlan> PetFoodPlanRepository
        {
            get { return petFoodPlanRepository ?? (petFoodPlanRepository = new GenericRepository<PetFoodPlan>(context)); }
        }

        public IGenericRepository<PetVaccination> PetVaccinationRepository
        {
            get { return petVaccinationRepository ?? (petVaccinationRepository = new GenericRepository<PetVaccination>(context)); }
        }

        public IGenericRepository<PetMedication> PetMedicationRepository
        {
            get { return petMedicationRepository ?? (petMedicationRepository = new GenericRepository<PetMedication>(context)); }
        }

        public IGenericRepository<State> StateRepository
        {
            get { return stateRepository ?? (stateRepository = new GenericRepository<State>(context)); }
        }

        public IGenericRepository<Farmer> FarmerRepository
        {
            get { return farmerRepository ?? (farmerRepository = new GenericRepository<Farmer>(context)); }
        }

        public IGenericRepository<Veterinarian> VeterinarianRepository
        {
            get { return veterinarianRepository ?? (veterinarianRepository = new GenericRepository<Veterinarian>(context)); }
        }

        public IGenericRepository<Contact> ContactRepository
        {
            get { return contactRepository ?? (contactRepository = new GenericRepository<Contact>(context)); }
        }

        public IGenericRepository<BloodGroupType> BloodGroupTypeRepository
        {
            get { return bloodGroupTypeRepository ?? (bloodGroupTypeRepository = new GenericRepository<BloodGroupType>(context)); }
        }

        public IGenericRepository<Insurance> InsuranceRepository
        {
            get { return insuranceRepository ?? (insuranceRepository = new GenericRepository<Insurance>(context)); }
        }

        public IGenericRepository<Subscription> SubscriptionRepository
        {
            get { return subscriptionRepository ?? (subscriptionRepository = new GenericRepository<Subscription>(context)); }
        }

        public IGenericRepository<Calendar> CalendarRepository
        {
            get { return calendarRepository ?? (calendarRepository = new GenericRepository<Calendar>(context)); }
        }

        public MesssageRepository MessageRepository
        {
            get { return messageRepository ?? (messageRepository = new MesssageRepository(context)); }
        }

        public LoginRepository LoginRepository
        {
            get { return loginRepository ?? (loginRepository = new LoginRepository(context)); }
        }

        public TimeZoneRepository TimeZoneRepository
        {
            get { return timeZoneRepository ?? (timeZoneRepository = new TimeZoneRepository(context)); }
        }

        public IGenericRepository<EConsultation> EConsultationRepository
        {
            get { return eConsultationRepository ?? (eConsultationRepository = new GenericRepository<EConsultation>(context)); }
        }

        public IGenericRepository<EconsultationData> EConsultationDataRepository
        {
            get { return eConsultationDataRepository ?? (eConsultationDataRepository = new GenericRepository<EconsultationData>(context)); }
        }

        public IGenericRepository<EconsultationMsg> EConsultationMsgRepository
        {
            get { return eConsultationMsgRepository ?? (eConsultationMsgRepository = new GenericRepository<EconsultationMsg>(context)); }
        }

        public IGenericRepository<EconsultationRoom> EConsultationRoomRepository
        {
            get { return eConsultationRoomRepository ?? (eConsultationRoomRepository = new GenericRepository<EconsultationRoom>(context)); }
        }

        public IGenericRepository<EConsultationStatus> EConsultationStatuRepository
        {
            get { return eConsultationStatuRepository ?? (eConsultationStatuRepository = new GenericRepository<EConsultationStatus>(context)); }
        }

        public IGenericRepository<EconsultationSummary> EConsultationSummaryRepository
        {
            get { return eConsultationSummaryRepository ?? (eConsultationSummaryRepository = new GenericRepository<EconsultationSummary>(context)); }
        }

        public IGenericRepository<EconsultationUser> EConsultationUserRepository
        {
            get { return eConsultationUserRepository ?? (eConsultationUserRepository = new GenericRepository<EconsultationUser>(context)); }
        }

        public IGenericRepository<Notification> NotificationRepository
        {
            get { return notificationRepository ?? (notificationRepository = new GenericRepository<Notification>(context)); }
        }

        public IGenericRepository<SMORequest> SMORequestRepository
        {
            get { return smoRequestRepository ?? (smoRequestRepository = new GenericRepository<SMORequest>(context)); }
        }

        public IGenericRepository<PetContact> PetContactRepository
        {
            get { return petContactRepository ?? (petContactRepository = new GenericRepository<PetContact>(context)); }
        }

        public IGenericRepository<SMOInvestigation> SMOInvestigationRepository
        {
            get { return smoInvestigationRepository ?? (smoInvestigationRepository = new GenericRepository<SMOInvestigation>(context)); }
        }

        public IGenericRepository<UserSubscription> UserSubscriptionRepository
        {
            get { return userSubscriptionRepository ?? (userSubscriptionRepository = new GenericRepository<UserSubscription>(context)); }
        }

        public IGenericRepository<BillingInformation> BillingInformationRepository
        {
            get { return billingInformationRepository ?? (billingInformationRepository = new GenericRepository<BillingInformation>(context)); }
        }
        public IGenericRepository<Gallery> GalleryRepository
        {
            get { return galleryRepository ?? (galleryRepository = new GenericRepository<Gallery>(context)); }
        }

        public IGenericRepository<ShareContact> ShareContactRepository
        {
            get { return shareContactRepository ?? (shareContactRepository = new GenericRepository<ShareContact>(context)); }
        }

        public IGenericRepository<VideoGallery> VideoGalleryRepository
        {
            get { return videoGalleryRepository ?? (videoGalleryRepository = new GenericRepository<VideoGallery>(context)); }
        }

        public IGenericRepository<SMOExpertRelation> SMOExpertRepository
        {
            get { return smoExpertRepository ?? (smoExpertRepository = new GenericRepository<SMOExpertRelation>(context)); }
        }
        public IGenericRepository<AlbumGallery> AlbumGalleryRepository
        {
            get { return albumGalleryRepository ?? (albumGalleryRepository = new GenericRepository<AlbumGallery>(context)); }
        }

        public IGenericRepository<UserSubscriptionHistory> UserSubscriptionHistoryRepository
        {
            get { return userSubscriptionHistoryRepository ?? (userSubscriptionHistoryRepository = new GenericRepository<UserSubscriptionHistory>(context)); }
        }

        public IGenericRepository<SMOExpertCommittee> SMOExpertCommitteeRepository
        {
            get { return smoexpertCommitteeRepository ?? (smoexpertCommitteeRepository = new GenericRepository<SMOExpertCommittee>(context)); }
        }

        public IGenericRepository<SMODocument> SMODocumentRepository
        {
            get { return smoDocumentRepository ?? (smoDocumentRepository = new GenericRepository<SMODocument>(context)); }
        }

        public IGenericRepository<ExpertBioData> ExpertBioDataRepository
        {
            get { return expertBioDataRepository ?? (expertBioDataRepository = new GenericRepository<ExpertBioData>(context)); }
        }

        public IGenericRepository<TempUserSubscription> TempUserSubscriptionRepository
        {
            get { return tempUserSubscriptionRepository ?? (tempUserSubscriptionRepository = new GenericRepository<TempUserSubscription>(context)); }
        }

        public IGenericRepository<ShareCategoryType> ShareCategoryTypeRepository
        {
            get { return shareCategoryTypeRepository ?? (shareCategoryTypeRepository = new GenericRepository<ShareCategoryType>(context)); }
        }
        public IGenericRepository<VetSpeciality> VetSpecialityRepository
        {
            get { return vetSpecialityRepository ?? (vetSpecialityRepository = new GenericRepository<VetSpeciality>(context)); }
        }

        public IGenericRepository<UserType> UserTypeRepository
        {
            get { return userTypeRepository ?? (userTypeRepository = new GenericRepository<UserType>(context)); }
        }

        public IGenericRepository<EconsultDocument> EconsultDocumentRepository
        {
            get { return econsultDocumentRepository ?? (econsultDocumentRepository = new GenericRepository<EconsultDocument>(context)); }
        }

        public IGenericRepository<Center> CenterRepository
        {
            get { return centerRepository ?? (centerRepository = new GenericRepository<Center>(context)); }
        }

        public IGenericRepository<SharePetInformation> SharePetInformationRepository
        {
            get { return sharePetInformationRepository ?? (sharePetInformationRepository = new GenericRepository<SharePetInformation>(context)); }
        }

    public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Track(int userId, string ipAddress)
        {
            context.UserId = userId;
            context.IpAddress = ipAddress;
        }
    }
}
