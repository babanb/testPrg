using System.Linq;
using System.Web.Mvc;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.Common.Enums;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.ViewModels.MedicalRecord;
using Model;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class MedicalRecordController : BaseController
    {
        public ActionResult Index(int id, MedicalRecordTypeEnum mRType, int mRSubType)
        {
         
            var pet = UnitOfWork.PetRepository.GetSingle(p => p.Id == id,
                p => p.PetConditions, 
                p => p.PetSurgeries, 
                p => p.PetMedications,
                p => p.PetAllergies,
                p => p.PetVaccinations,
                p => p.PetFoodPlans,
                p => p.PetConsultations,
                p => p.PetHospitalizations,
                p => p.PetHealthMeasures,
                p => p.PetDocuments);

            ViewBag.PetId = pet.Id;
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            return PartialView("_Index", new IndexViewModel(pet, mRType, mRSubType));
        }

    }
}
