using ADOPets.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ADOPets.Web.Common.Authentication;
using System.Drawing;

namespace ADOPets.Web.Handler
{
    /// <summary>
    /// Summary description for FileUploadHandler
    /// </summary>
    public class FileUploadHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    HttpPostedFile file = null;
                    var uid = HttpContext.Current.User.ToCustomPrincipal().CustomIdentity.UserId.ToString();
                    var infoPath = HttpContext.Current.User.ToCustomPrincipal().CustomIdentity.InfoPath;
                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        file = context.Request.Files[i];
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            // SecurityExtentions sec = new SecurityExtentions();

                            var dirPath = Path.Combine(HttpContext.Current.User.GetUserAbsoluteInfoPath(infoPath, uid), Constants.GalleryPhotoFolderName);
                            if (!Directory.Exists(dirPath))
                            {
                                Directory.CreateDirectory(dirPath);
                            }
                            string newfileName = DateTime.Now.Day + "" + DateTime.Now.Ticks + DateTime.Now.Month + fileName.Trim().Replace(" ","_");
                            var path = Path.Combine(dirPath, newfileName);
                            file.SaveAs(path);

                            Image thumb = Image.FromStream(file.InputStream, true, true);
                            Image thumbGalleryPhoto = thumb.GetThumbnailImage(300, 225, null, IntPtr.Zero);
                            var thumbdirectoryPath = Path.Combine(HttpContext.Current.User.GetUserAbsoluteInfoPath(infoPath, uid), Constants.ThumbnailGalleryPhotoFolderName);
                            if (!Directory.Exists(thumbdirectoryPath))
                            {
                                Directory.CreateDirectory(thumbdirectoryPath);
                            }
                            string thumbImgName = "Thumb_" + newfileName;
                            path = Path.Combine(thumbdirectoryPath, thumbImgName);
                            thumbGalleryPhoto.Save(path);
                            thumbGalleryPhoto.Dispose();
                            context.Response.Write(newfileName);
                        }
                    }
                }
            }
            catch { }


            context.Response.End();

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}