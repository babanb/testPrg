//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class State
    {
        public State()
        {
            this.Veterinarians = new HashSet<Veterinarian>();
            this.Farmers = new HashSet<Farmer>();
            this.Pets = new HashSet<Pet>();
            this.BillingInformations = new HashSet<BillingInformation>();
            this.Users = new HashSet<User>();
            this.PetContacts = new HashSet<PetContact>();
        }
    
        public Model.StateEnum Id { get; set; }
        public Model.CountryEnum CountryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual ICollection<Veterinarian> Veterinarians { get; set; }
        public virtual ICollection<Farmer> Farmers { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
        public virtual ICollection<BillingInformation> BillingInformations { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<PetContact> PetContacts { get; set; }
    }
}
