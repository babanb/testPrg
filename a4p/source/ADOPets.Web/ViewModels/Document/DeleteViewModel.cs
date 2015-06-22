using System;
using System.ComponentModel.DataAnnotations;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Document
{
    public class DeleteViewModel
    {
        public DeleteViewModel()
        {
            
        }

        public DeleteViewModel(PetDocument document)
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

        [Display(Name = "Document_Delete_Name", ResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "Document_Delete_DocumentType", ResourceType = typeof(Wording))]
        public DocumentTypeEnum DocumentType { get; set; }

        [Display(Name = "Document_Delete_DocumentSubType", ResourceType = typeof(Wording))]
        public DocumentSubTypeEnum DocumentSubType { get; set; }

        [Display(Name = "Document_Delete_ServiceDate", ResourceType = typeof(Wording))]
        public DateTime ServiceDate { get; set; }

        [Display(Name = "Document_Delete_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }
    }
}