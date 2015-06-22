using System.Collections.Generic;
using System.Linq;
using ADOPets.Web.Common.Enums;
using Model;

namespace ADOPets.Web.ViewModels.Document
{
    public class DocumentViewModel
    {
        public DocumentViewModel()
        {
            
        }

        public DocumentViewModel(Model.Pet pet, MedicalRecordTypeEnum mRType, int docType)
        {
            PetId = pet.Id;
            Current = mRType == MedicalRecordTypeEnum.Document
                ? (DocumentTypeEnum)docType
                : DocumentTypeEnum.Note;

            var documents = pet.PetDocuments.OrderByDescending(d => d.ServiceDate);

            Notes = documents.Where(d => d.DocumentTypeEnum == DocumentTypeEnum.Note && !d.IsDeleted).Select(d => new IndexViewModel(d));
            ImagingServices = documents.Where(d => d.DocumentTypeEnum == DocumentTypeEnum.ImagingServices && !d.IsDeleted).Select(d => new IndexViewModel(d));
            Laboratories = documents.Where(d => d.DocumentTypeEnum == DocumentTypeEnum.Laboratory && !d.IsDeleted).Select(d => new IndexViewModel(d));
            Prescriptions = documents.Where(d => d.DocumentTypeEnum == DocumentTypeEnum.Prescription && !d.IsDeleted).Select(d => new IndexViewModel(d));
            AdoServices = documents.Where(d => d.DocumentTypeEnum == DocumentTypeEnum.ADOServices && !d.IsDeleted).Select(d => new IndexViewModel(d));
            Insurances = documents.Where(d => d.DocumentTypeEnum == DocumentTypeEnum.Insurance && !d.IsDeleted).Select(d => new IndexViewModel(d));
        }

        public DocumentViewModel(int petId, List<PetDocument> petDocuments, DocumentTypeEnum docType)
        {
            PetId = petId;
            Current = docType;

            Notes = petDocuments.Where(d => d.DocumentTypeEnum == DocumentTypeEnum.Note && !d.IsDeleted).Select(d => new IndexViewModel(d));
            ImagingServices = petDocuments.Where(d => d.DocumentTypeEnum == DocumentTypeEnum.ImagingServices && !d.IsDeleted).Select(d => new IndexViewModel(d));
            Laboratories = petDocuments.Where(d => d.DocumentTypeEnum == DocumentTypeEnum.Laboratory && !d.IsDeleted).Select(d => new IndexViewModel(d));
            Prescriptions = petDocuments.Where(d => d.DocumentTypeEnum == DocumentTypeEnum.Prescription && !d.IsDeleted).Select(d => new IndexViewModel(d));
            AdoServices = petDocuments.Where(d => d.DocumentTypeEnum == DocumentTypeEnum.ADOServices && !d.IsDeleted).Select(d => new IndexViewModel(d));
            Insurances = petDocuments.Where(d => d.DocumentTypeEnum == DocumentTypeEnum.Insurance && !d.IsDeleted).Select(d => new IndexViewModel(d));
        }

        public int PetId { get; set; }

        public DocumentTypeEnum Current { get; set; }

        public IEnumerable<IndexViewModel> Notes { get; set; }

        public IEnumerable<IndexViewModel> ImagingServices { get; set; }

        public IEnumerable<IndexViewModel> Laboratories { get; set; }

        public IEnumerable<IndexViewModel> Prescriptions { get; set; }

        public IEnumerable<IndexViewModel> AdoServices { get; set; }

        public IEnumerable<IndexViewModel> Insurances { get; set; }
    }
}