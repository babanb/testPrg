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
    
    public partial class PetContact
    {
        public PetContact()
        {
            this.Contacts = new HashSet<Contact>();
            this.FirstName = new EncryptedText();
            this.LastName = new EncryptedText();
            this.Email = new EncryptedText();
            this.Address1 = new EncryptedText();
            this.Address2 = new EncryptedText();
            this.City = new EncryptedText();
            this.Zip = new EncryptedText();
            this.PhoneHome = new EncryptedText();
            this.PhoneOffice = new EncryptedText();
            this.PhoneCell = new EncryptedText();
            this.Fax = new EncryptedText();
            this.Comments = new EncryptedText();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<Model.StateEnum> StateId { get; set; }
        public Nullable<Model.CountryEnum> CountryId { get; set; }
        public bool IsEmergencyContact { get; set; }
        public Model.ContactTypeEnum ContactTypeId { get; set; }
        public int PetId { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public EncryptedText FirstName { get; set; }
        public EncryptedText LastName { get; set; }
        public EncryptedText Email { get; set; }
        public EncryptedText Address1 { get; set; }
        public EncryptedText Address2 { get; set; }
        public EncryptedText City { get; set; }
        public EncryptedText Zip { get; set; }
        public EncryptedText PhoneHome { get; set; }
        public EncryptedText PhoneOffice { get; set; }
        public EncryptedText PhoneCell { get; set; }
        public EncryptedText Fax { get; set; }
        public EncryptedText Comments { get; set; }
    
        public virtual ContactType ContactType { get; set; }
        public virtual Country Country { get; set; }
        public virtual Pet Pet { get; set; }
        public virtual State State { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
