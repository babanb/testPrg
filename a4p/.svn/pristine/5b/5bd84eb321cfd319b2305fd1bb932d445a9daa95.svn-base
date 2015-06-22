using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADOPets.Web.Common;
using ADOPets.Web.Common.Authentication;
using ADOPets.Web.Common.Helpers;
using ADOPets.Web.ViewModels.Document;
using Model;

namespace ADOPets.Web.Controllers
{
    [Authorize]
    [UserRoleAuthorize(UserTypeEnum.Admin, UserTypeEnum.OwnerAdmin, UserTypeEnum.VeterinarianAdo, UserTypeEnum.VeterinarianLight, UserTypeEnum.VeterinarianExpert)]
    public class DocumentController : BaseController
    {
        public ActionResult Index(int petId, DocumentTypeEnum documentType)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var documents = UnitOfWork.PetDocumentRepository.GetAll(d => d.PetId == petId && !d.IsDeleted, d => d.OrderByDescending(doc => doc.ServiceDate)).ToList();

            ViewBag.PetId = petId;

            return PartialView("_Index", new DocumentViewModel(petId, documents, documentType));
        }

        [HttpGet]
        public ActionResult Add(int petId, DocumentTypeEnum documentType)
        {
            return PartialView("_Add", new AddViewModel(petId, documentType));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var fileName = string.Empty;

                if (model.File != null && model.File.ContentLength > 0)
                {
                    var userInfo = UnitOfWork.UsersRepository.GetSingle(u => u.Pets.Any(p => p.Id == model.PetId), navigationProperties: u => u.Pets);

                    fileName = DateTime.Now.ToString("yyyymmddhhmmss") + "_" + Path.GetFileName(model.File.FileName);
                    var directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(userInfo.InfoPath, userInfo.Id.ToString()), Constants.PetsFolderName, model.PetId.ToString(), Constants.DocumentFolderName);

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var path = Path.Combine(directoryPath, fileName);
                    model.File.SaveAs(path);
                }

                UnitOfWork.PetDocumentRepository.Insert(model.Map(fileName));
                UnitOfWork.Save();
                Success(Resources.Wording.Document_Add_AddSuccessfullMessage);
                return RedirectToAction("Index", new { petId = model.PetId, documentType = model.DocumentType });
            }
            else
            {
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var document = UnitOfWork.PetDocumentRepository.GetSingle(d => d.Id == id);
            return PartialView("_Edit", new EditViewModel(document));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var document = UnitOfWork.PetDocumentRepository.GetSingle(d => d.Id == model.DocumentId);

                model.Map(document);

                UnitOfWork.PetDocumentRepository.Update(document);
                UnitOfWork.Save();
                Success(Resources.Wording.Document_Edit_EditSuccessMessage);
                return RedirectToAction("Index", new { petId = model.PetId, documentType = document.DocumentTypeEnum });
            }
            else
            {
                throw new Exception("Ajax Call Failed!");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirm(int documentId, int petId)
        {
            var document = UnitOfWork.PetDocumentRepository.GetSingle(d => d.Id == documentId);

            ViewBag.PetId = petId;

            return PartialView("_Delete", new DeleteViewModel(document));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int documentId, int petId)
        {
            var document = UnitOfWork.PetDocumentRepository.GetSingle(c => c.Id == documentId);

            var directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(), Constants.PetsFolderName, petId.ToString(), Constants.DocumentFolderName);
            var path = Path.Combine(directoryPath, document.DocumentPath);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            document.IsDeleted = true;
            UnitOfWork.PetDocumentRepository.Update(document);
            UnitOfWork.Save();
            Success(Resources.Wording.Document_Delete_DeleteSuccessMessage);
            return RedirectToAction("Index", new { petId, documentType = document.DocumentTypeEnum });
        }

        [HttpGet]
        public ActionResult GetDocumentSubtypes(string documentType)
        {
            var documentTypeEnum = Enum.Parse(typeof(DocumentTypeEnum), documentType);

            var documentSubTypes = DocumentType.GetDocumentSubTypes((DocumentTypeEnum)documentTypeEnum);

            var items = documentSubTypes.Select(i => new SelectListItem { Text = EnumHelper.GetResourceValueForEnumValue(i), Value = i.ToString() });

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Details(int documentId, int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;

            var document = UnitOfWork.PetDocumentRepository.GetSingle(c => c.Id == documentId);
            var userInfo = UnitOfWork.UsersRepository.GetSingle(u => u.Pets.Any(p => p.Id == petId), navigationProperties: u => u.Pets);

            var directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(userInfo.InfoPath, userInfo.Id.ToString()), Constants.PetsFolderName, petId.ToString(), Constants.DocumentFolderName);
            var path = Path.Combine(directoryPath, document.DocumentPath);

            var displayExtensions = new List<string> { ".txt", ".jpg", ".jpeg", ".pdf", ".png", ".gif" };
            var extension = Path.GetExtension(path);

            ViewBag.CanDisplay = displayExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
            ViewBag.IsPdfFile = string.Compare(extension, ".pdf", true, CultureInfo.InvariantCulture) == 0;
            ViewBag.DocumentId = documentId;
            ViewBag.PetId = petId;

            return PartialView("_Detail");
        }

        public FileResult LoadDocument(int documentId, int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var document = UnitOfWork.PetDocumentRepository.GetSingle(c => c.Id == documentId);
            var userInfo = UnitOfWork.UsersRepository.GetSingle(u => u.Pets.Any(p => p.Id == petId), navigationProperties: u => u.Pets);
            var directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(userInfo.InfoPath, userInfo.Id.ToString()), Constants.PetsFolderName, petId.ToString(), Constants.DocumentFolderName);
            var path = Path.Combine(directoryPath, document.DocumentPath);
            var fs = new FileStream(path, FileMode.Open);
            var contentType = MimeMapping.GetMimeMapping(document.DocumentPath);

            return new FileStreamResult(fs, contentType);
        }

        public FileResult Download(int documentId, int petId)
        {
            var userId = HttpContext.User.GetUserId();
            ViewBag.UserId = userId;
            var document = UnitOfWork.PetDocumentRepository.GetSingle(c => c.Id == documentId);
            var userInfo = UnitOfWork.UsersRepository.GetSingle(u => u.Pets.Any(p => p.Id == petId), navigationProperties: u => u.Pets);
            var directoryPath = Path.Combine(HttpContext.User.GetUserAbsoluteInfoPath(userInfo.InfoPath, userInfo.Id.ToString()), Constants.PetsFolderName, petId.ToString(), Constants.DocumentFolderName);
            var path = Path.Combine(directoryPath, document.DocumentPath);

            var extention = Path.GetExtension(document.DocumentPath);

            var fs = new FileStream(path, FileMode.Open);

            var contentType = MimeMapping.GetMimeMapping(document.DocumentPath);

            return File(fs, contentType, document.DocumentName + extention);
        }
    }
}
