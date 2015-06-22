using System.Web;
using ADOPets.Web.Common.Authentication;
using Repository.Implementations;

namespace ADOPets.Web.Common.Helpers
{
    public static class SubscriptionConstraintHelper
    {
        public static bool CanAddNewPet(int userId = 0, int maxPetCount = 0)
        {
            var usrId = (userId == 0) ? HttpContext.Current.User.GetUserId() : userId;
            var currentPetCount = HttpContext.Current.Session[Constants.SessionCurrentUserPetCount];
            if (currentPetCount == null)
            {
                using (var uow = new UnitOfWork())
                {
                    var subscriptionID = uow.UserRepository.GetSingleTracking(u => u.Id == usrId, navigationProperties: u => u.UserSubscription).UserSubscription.SubscriptionId;
                    currentPetCount = uow.SubscriptionRepository.GetSingleTracking(s => s.Id == subscriptionID).MaxPetCount;
                    HttpContext.Current.Session[Constants.SessionCurrentUserPetCount] = currentPetCount;
                }
            }
            var petCount = (userId == 0) ? HttpContext.Current.User.GetUserMaxPetCount() : maxPetCount;
            return (int)currentPetCount < petCount;
        }
    }
}