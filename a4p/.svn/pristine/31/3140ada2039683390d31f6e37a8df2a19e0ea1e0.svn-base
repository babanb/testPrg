using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Surgery
{
    public class AddViewModel
    {
        public AddViewModel()
        {

        }

        public AddViewModel(int petid)
        {
            PetId = petid;
        }

        public int PetId { get; set; }

        [Display(Name = "Surgery_Add_Surgery", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Surgery_Add_SurgeryRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Surgery { get; set; }

        [Display(Name = "Surgery_Add_DateofSurgery", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Surgery_Add_DateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? DateofSurgery { get; set; }

        [Display(Name = "Surgery_Add_Reason", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Surgery_Add_ReasonRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Reason { get; set; }

        [Display(Name = "Surgery_Add_Physician", ResourceType = typeof(Wording))]
        public string Physician { get; set; }

        [Display(Name = "Surgery_Add_Comments", ResourceType = typeof(Wording))]
        public string Comments { get; set; }

        public Model.PetSurgery Map()
        {
            var petSurgery = new Model.PetSurgery
            {
                CustomSurgery = Surgery,
                SurgeryDate = DateofSurgery.HasValue ? DateofSurgery.Value : (DateTime?)null,
                Reason = Reason,
                Physician = new EncryptedText(Physician),
                Comment = new EncryptedText(Comments),

                PetId = PetId
            };

            return petSurgery;
        }
    }
}