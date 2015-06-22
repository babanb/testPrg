using System;
using Model.Interfaces;

namespace Model
{
    public partial class PetDocument : IMedicalData
    {
        public DocumentTypeEnum DocumentTypeEnum
        {
            get
            {
                switch (DocumentSubTypeId)
                {
                    case DocumentSubTypeEnum.ProgressNote:
                        return DocumentTypeEnum.Note;
                    case DocumentSubTypeEnum.ConsultationNote:
                        return DocumentTypeEnum.Note;
                    case DocumentSubTypeEnum.XRay:
                        return DocumentTypeEnum.ImagingServices;
                    case DocumentSubTypeEnum.CTScan:
                        return DocumentTypeEnum.ImagingServices;
                    case DocumentSubTypeEnum.MRIEndoscopy:
                        return DocumentTypeEnum.ImagingServices;
                    case DocumentSubTypeEnum.Ultrasound:
                        return DocumentTypeEnum.ImagingServices;
                    case DocumentSubTypeEnum.DopplerStudies:
                        return DocumentTypeEnum.Laboratory;
                    case DocumentSubTypeEnum.Hematology:
                        return DocumentTypeEnum.Laboratory;
                    case DocumentSubTypeEnum.Panels:
                        return DocumentTypeEnum.Laboratory;
                    case DocumentSubTypeEnum.Biochemistry:
                        return DocumentTypeEnum.Laboratory;
                    case DocumentSubTypeEnum.Biopsy:
                        return DocumentTypeEnum.Laboratory;
                    case DocumentSubTypeEnum.Prescription:
                        return DocumentTypeEnum.Prescription;
                    case DocumentSubTypeEnum.SMO:
                        return DocumentTypeEnum.ADOServices;
                    case DocumentSubTypeEnum.Insurance:
                        return DocumentTypeEnum.Insurance;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
