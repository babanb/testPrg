using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using ADOPets.Web.Resources;
using Model;

namespace ADOPets.Web.ViewModels.Document
{
    public class AddViewModel
    {
        public AddViewModel()
        {

        }

        public AddViewModel(int petId, DocumentTypeEnum documentType)
        {
            PetId = petId;
            DocumentType = documentType;
        }

        public int PetId { get; set; }

        [Display(Name = "Document_Add_Name", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Document_Add_NameRequired", ErrorMessageResourceType = typeof(Wording))]
        public string Name { get; set; }

        [Display(Name = "Document_Add_DocumentType", ResourceType = typeof(Wording))]
        public DocumentTypeEnum DocumentType { get; set; }

        [Display(Name = "Document_Add_DocumentSubType", ResourceType = typeof(Wording))]
        public DocumentSubTypeEnum DocumentSubType { get; set; }

        [Display(Name = "Document_Add_File", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Document_Add_FileRequired", ErrorMessageResourceType = typeof(Wording))]
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Document_Add_ServiceDate", ResourceType = typeof(Wording))]
        [Required(ErrorMessageResourceName = "Document_Add_ServiceDateRequired", ErrorMessageResourceType = typeof(Wording))]
        public DateTime? ServiceDate { get; set; }

        [Display(Name = "Document_Add_Comment", ResourceType = typeof(Wording))]
        public string Comment { get; set; }

        public PetDocument Map(string fileName)
        {
            var document = new PetDocument
            {
                DocumentPath = fileName,
                DocumentName = new EncryptedText(Name),
                DocumentSubTypeId = DocumentSubType,
                ServiceDate = ServiceDate.Value,
                Comment = new EncryptedText(Comment),
                PetId = PetId,
                UploadDate = DateTime.Today
            };

            return document;
        }
    }
}