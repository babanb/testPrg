using ADOPets.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ADOPets.Web.Common.Authentication;
using System.Drawing;
using ADOPets.Web.Common.Helpers;
using Repository.Implementations;

namespace ADOPets.Web.Handler
{
    /// <summary>
    /// Summary description for FileUploadHandler
    /// </summary>
    public class SmoFileUploadHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    HttpPostedFile file = null;

                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        file = context.Request.Files[i];
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var uid = HttpContext.Current.User.ToCustomPrincipal().CustomIdentity.UserId.ToString();
                            var infoPath = HttpContext.Current.User.ToCustomPrincipal().CustomIdentity.InfoPath;

                            var dirPath = Path.Combine(WebConfigHelper.UserFilesPath, infoPath, uid, Constants.PetsFolderName, context.Request.Form[1].ToString(), Constants.DocumentFolderName, Constants.SMOFolderName, context.Request.Form[2].ToString(), Constants.DocumentFolderName);
                            if (!Directory.Exists(dirPath))
                            {
                                Directory.CreateDirectory(dirPath);
                            }
                            string newfileName = DateTime.Now.Day + "" + DateTime.Now.Ticks + DateTime.Now.Month + fileName.Trim().Replace(" ", "_");
                            var path = Path.Combine(dirPath, newfileName);
                            file.SaveAs(path);
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