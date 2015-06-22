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
    
    public partial class PetType
    {
        public PetType()
        {
            this.Pets = new HashSet<Pet>();
            this.BloodGroupTypes = new HashSet<BloodGroupType>();
        }
    
        public Model.PetTypeEnum Id { get; set; }
        public int PetGroupTypeId { get; set; }
        public string Name { get; set; }
        public bool IsDomestic { get; set; }
        public string Description { get; set; }
    
        public virtual PetGroupType PetGroupType { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
        public virtual ICollection<BloodGroupType> BloodGroupTypes { get; set; }
    }
}
