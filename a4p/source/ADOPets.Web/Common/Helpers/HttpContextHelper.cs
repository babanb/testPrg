using System.Web;

namespace ADOPets.Web.Common.Helpers
{
    public static class HttpContextHelper
    {
        private static string applicationPath;

        public static string GetApplicationPath()
        {
            if (string.IsNullOrEmpty(applicationPath))
            {
                applicationPath = HttpContext.Current.Request.ApplicationPath != null &&
                                  HttpContext.Current.Request.ApplicationPath.EndsWith("/")
                    ? HttpContext.Current.Request.ApplicationPath
                    : HttpContext.Current.Request.ApplicationPath + "/";
            }
            return applicationPath;
        }

        public static string GetDomainName()
        {
            return HttpContext.Current.Request.Url.Host;
        }
    }
}