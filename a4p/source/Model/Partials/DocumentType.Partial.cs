using System;
using System.Collections.Generic;

namespace Model
{
    public partial class DocumentType
    {
        public static List<DocumentSubTypeEnum> GetDocumentSubTypes(DocumentTypeEnum documentType)
        {
            switch (documentType)
            {
                case DocumentTypeEnum.Note:
                    return new List<DocumentSubTypeEnum>{ DocumentSubTypeEnum.ConsultationNote, DocumentSubTypeEnum.ProgressNote};
                case DocumentTypeEnum.ImagingServices:
                    return new List<DocumentSubTypeEnum> { DocumentSubTypeEnum.XRay, DocumentSubTypeEnum.CTScan, DocumentSubTypeEnum.MRIEndoscopy, DocumentSubTypeEnum.Ultrasound };
                case DocumentTypeEnum.Laboratory:
                    return new List<DocumentSubTypeEnum> { DocumentSubTypeEnum.DopplerStudies, DocumentSubTypeEnum.Hematology, DocumentSubTypeEnum.Panels, DocumentSubTypeEnum.Biochemistry, DocumentSubTypeEnum.Biopsy };
                case DocumentTypeEnum.Prescription:
                    return new List<DocumentSubTypeEnum> { DocumentSubTypeEnum.Prescription };
                case DocumentTypeEnum.ADOServices:
                    return new List<DocumentSubTypeEnum> { DocumentSubTypeEnum.SMO };
                case DocumentTypeEnum.Insurance:
                    return new List<DocumentSubTypeEnum> { DocumentSubTypeEnum.Insurance };
                default:
                    throw new ArgumentOutOfRangeException("documentType");
            }
        }
    }
}
