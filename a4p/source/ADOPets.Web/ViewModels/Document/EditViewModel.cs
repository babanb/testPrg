using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Document
{
    public class EditViewModel
    {
        public EditViewModel()
        {
            
        }

        public EditViewModel(PetDocument document)
        {
            DocumentId = document.Id;
            PetId = document.PetId;
            Name = document.DocumentName;
            DocumentType = document.DocumentTypeEnum;
            DocumentSubType = document.DocumentSubTypeId;
            ServiceDate = document.ServiceDate;
            Comment = document.Comment;
        }

        public int DocumentId { get; set; }

        public int PetId { get; set; }

        [Display(Name = "Document_Edit_Name", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Document_Edit_NameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "Document_Edit_DocumentType", ResourceType = typeof(Wording))]
        public DocumentTypeEnum DocumentType { get; set; }

        [Display(Name = "Document_Edit_DocumentSubType", ResourceType = typeof(Wording))]
        public DocumentSubTypeEnum DocumentSubType { get; set; }

        [Display(Name = "Document_Edit_ServiceDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Document_Edit_ServiceDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime ServiceDate { get; set; }

        [Display(Name = "Document_Edit_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public void Map(PetDocument document)
        {
            document.DocumentName = new EncryptedText(Name);
            document.DocumentSubTypeId = DocumentSubType;
            document.ServiceDate = ServiceDate;
            document.Comment = new EncryptedText(Comment);
        }
    }
}