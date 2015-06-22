using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ADOPets.Web.ViewModels.SMO
{
    public class SMODocumentViewModel
    {
        public SMODocumentViewModel()
        { }
        public SMODocumentViewModel(SMODocument smoDocument)
        {
            var match = Regex.Match(smoDocument.DocumentName.Value, @"(\d+)(\w+\.+\w+)");
            DocumentName = (!string.IsNullOrEmpty(match.Groups[2].Value)) ? match.Groups[2].Value : smoDocument.DocumentName.Value;
            DocumentPath = smoDocument.DocumentPath;
            SMOId = smoDocument.SMOId;
            Id = smoDocument.Id;
            DocumentSubTypeId = smoDocument.DocumentSubTypeId;
            UploadDate = smoDocument.UploadDate;
            IsDeleted = smoDocument.IsDeleted;
            UserId = smoDocument.UserId;
            DocName = smoDocument.DocumentName.Value;
        }

        public int Id { get; set; }
        public Nullable<long> SMOId { get; set; }
        public Nullable<Model.DocumentSubTypeEnum> DocumentSubTypeId { get; set; }
        public string DocumentName { get; set; }
        public string DocumentPath { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? UserId { get; set; }
        public string DocName { get; set; }

        public SMODocument Map(string fileName)
        {
            var document = new SMODocument
            {
                DocumentPath = fileName,
                DocumentName = new EncryptedText(DocumentName),
                DocumentSubTypeId = DocumentSubTypeEnum.SMO,
                IsDeleted = false,
                SMOId = SMOId,
                UploadDate = DateTime.Today,
                UserId = UserId
            };

            return document;
        }
    }
}