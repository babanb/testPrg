using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Allergy
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

        [Display(Name = "Allergy_Add_Allergy", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Allergy_Add_AllergyRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Allergy { get; set; }

        [Display(Name = "Allergy_Add_StartDate", ResourceType = typeof(Wording))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Allergy_Add_EndDate", ResourceType = typeof(Wording))]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Allergy_Add_Reaction", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Allergy_Add_ReactionRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Reaction { get; set; }

        [Display(Name = "Allergy_Add_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public Model.PetAllergy Map()
        {
            var petAllergy = new Model.PetAllergy
            {
                CustomAllergy = Allergy,
                StartDate = StartDate,
                EndDate = EndDate.HasValue ? EndDate.Value : (DateTime?)null,
                Reaction =Reaction,
                Comment = new EncryptedText(Comment),
                PetId = PetId
            };
            return petAllergy;
        }
    }
}