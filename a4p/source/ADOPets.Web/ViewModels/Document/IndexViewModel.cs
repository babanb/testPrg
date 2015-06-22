using System;
using System.Web;
using Model;

namespace ADOPets.Web.ViewModels.Document
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            
        }

        public IndexViewModel(PetDocument document)
        {
            DocumentId = document.Id;
            Name = document.DocumentName;
            DocumentSubType = document.DocumentSubTypeId;
            ServiceDate = document.ServiceDate;
            UploadDate = document.UploadDate;
            Path = document.DocumentPath;
        }

        public int DocumentId { get; set; }

        public string Name { get; set; }

        public DocumentSubTypeEnum DocumentSubType { get; set; }

        public DateTime ServiceDate { get; set; }

        public DateTime UploadDate { get; set; }

        public string Path { get; set; }
    }
}