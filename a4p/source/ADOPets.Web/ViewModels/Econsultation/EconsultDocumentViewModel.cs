using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ADOPets.Web.ViewModels.Econsultation
{
    public class EconsultDocumentViewModel
    {
        public EconsultDocumentViewModel()
        { }

        public EconsultDocumentViewModel(EconsultDocument EconsultDocument)
        {
            var match = Regex.Match(EconsultDocument.DocumentName.ToString() , @"(\d+)(\w+\.+\w+)");
            DocumentName = (!string.IsNullOrEmpty(match.Groups[2].Value)) ? match.Groups[2].Value : EconsultDocument.DocumentName.ToString();
            DocumentPath = EconsultDocument.DocumentPath;
            ECId = EconsultDocument.EcId;
            Id = EconsultDocument.Id;
            UploadDate = EconsultDocument.UploadDate;
            IsDeleted = EconsultDocument.IsDeleted;
            DocName = EconsultDocument.DocumentName.ToString();
        }

        public int Id { get; set; }
        public Nullable<int> ECId { get; set; }
        public Nullable<Model.DocumentSubTypeEnum> DocumentSubTypeId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? UserId { get; set; }
        public string DocName { get; set; }

        public EconsultDocument Map(string fileName)
        {
            var document = new EconsultDocument
            {
                DocumentPath = fileName,
                DocumentName = new EncryptedText(DocumentName),
                DocumentSubTypeId = DocumentSubTypeEnum.Prescription,
                IsDeleted = false,
                EcId = Convert.ToInt16(ECId),
                UploadDate = DateTime.Today                
            };

            return document;
        }
    }
}