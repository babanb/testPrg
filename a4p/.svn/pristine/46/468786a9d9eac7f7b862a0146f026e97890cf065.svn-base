using System.Collections.Generic;
using System.Linq;

namespace ADOPets.Web.ViewModels.Pet
{
    public class EditViewModel
    {
        public EditViewModel(Model.Pet pet)
        {
            //int petOwnerId = 0;
            //var petOwner = (pet.Users != null) ? pet.Users.FirstOrDefault() : null;
            //if (petOwner != null)
            //{ petOwnerId = petOwner.Id; }
            Card = new CardViewModel(pet);
            BreederEdit = new BreederEditViewModel(pet);
            PetContact = pet.PetContacts.Select(o => new PetContact.IndexViewModel(o)).ToList();
            Insurances = pet.Insurances.Select(i => new Insurance.IndexViewModel(i)).ToList();
            Veterinarians = pet.Veterinarians.Select(v => new Veterinarian.IndexViewModel(v)).ToList();
        }

        public CardViewModel Card { get; set; }

        public BreederEditViewModel BreederEdit { get; set; }

        public List<PetContact.IndexViewModel> PetContact { get; set; }

        public List<Insurance.IndexViewModel> Insurances { get; set; }

        public List<Veterinarian.IndexViewModel> Veterinarians { get; set; }
    }
}