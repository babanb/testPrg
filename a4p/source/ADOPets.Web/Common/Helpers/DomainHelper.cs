using System.Collections.Generic;
using System.IO;
using System.Linq;
using Model;
using System.Web;

namespace ADOPets.Web.Common.Helpers
{
    public static class DomainHelper
    {
        private static string logoPath;
        private static string loginLogoPath;
        private static string logoSignatureForEmailPath;
        private static string mraformPath;
        private static string logoForEmailPath;

        public static string GetLogoPath()
        {
            if (string.IsNullOrEmpty(logoPath))
            {
                logoPath = GetDomain() == DomainTypeEnum.French ? WebConfigHelper.LogoFrenchDomains : WebConfigHelper.LogoUSDomains;
            }

            return logoPath;
        }
        public static string GetLoginLogoPath()
        {
            if (string.IsNullOrEmpty(logoPath))
            {
                loginLogoPath = GetDomain() == DomainTypeEnum.French ? WebConfigHelper.LoginLogoFrenchDomains : WebConfigHelper.LoginLogoUSDomains;
            }

            return loginLogoPath;
        }
        public static string GetLogoPathForEmail()
        {
            return HttpContext.Current.Server.MapPath(GetLogoPath());
        }

        public static string GetMraFormPdfPath()
        {
            if (string.IsNullOrEmpty(mraformPath))
            {
                mraformPath = GetDomain() == DomainTypeEnum.French ? WebConfigHelper.MraFormPdfPathFR : WebConfigHelper.MraFormPdfPath;
            }

            return mraformPath;
        }

        public static string GetLogoForEmailPath()
        {
            if (string.IsNullOrEmpty(logoForEmailPath))
            {
                logoForEmailPath = GetDomain() == DomainTypeEnum.French ? HttpContext.Current.Server.MapPath(WebConfigHelper.LogoForEmailPathFR) : HttpContext.Current.Server.MapPath(WebConfigHelper.LogoForEmailPath);
            }
            return logoForEmailPath;
        }

        public static string GetLogoSignatureForEmailPath()
        {
            if (string.IsNullOrEmpty(logoSignatureForEmailPath))
            {
                logoSignatureForEmailPath = HttpContext.Current.Server.MapPath(WebConfigHelper.LogoSignatureForEmailPath);
            }

            return logoSignatureForEmailPath;
        }

        public static DomainTypeEnum GetDomain()
        {
            var domain = HttpContextHelper.GetDomainName();
            return WebConfigHelper.FrenchDomains.Contains(domain) ? DomainTypeEnum.French : WebConfigHelper.PortugueseDomains.Contains(domain) ? DomainTypeEnum.Portuguese : WebConfigHelper.InDomains.Contains(domain) ? DomainTypeEnum.India : DomainTypeEnum.US;
        }

        public static string GetCurrency()
        {
            var domain = HttpContextHelper.GetDomainName();
            var Currency = "$";
            if (WebConfigHelper.InDomains.Contains(domain))
            {
                Currency = "₹";
            }
            if (WebConfigHelper.FrenchDomains.Contains(domain) || WebConfigHelper.PortugueseDomains.Contains(domain))
            {
                Currency = "€";
            }            
            return Currency;
        }

        public static bool IsProductionDomain()
        {
            var domain = HttpContextHelper.GetDomainName();
            var productionDomains = new List<string> { "login.activ4pets.com", "www.login.activ4pets.com", "login.activanimo.fr", "www.login.activanimo.fr", "login.activ4pets.in", "www.login.activ4pets.in" };
            return productionDomains.Contains(domain);
        }

        public static HealthMeasureUnitEnum GetHeightMeasureUnitDefault()
        {
            var unit = GetDomain() == DomainTypeEnum.US
                ? HealthMeasureUnitEnum.Feet
                : HealthMeasureUnitEnum.Meter;
            return unit;
        }

        public static HealthMeasureUnitEnum GetWeightMeasureUnitDefault()
        {
            var unit = GetDomain() == DomainTypeEnum.US
                ? HealthMeasureUnitEnum.Pounds
                : HealthMeasureUnitEnum.Kilogram;
            return unit;
        }

        public static HealthMeasureUnitEnum GetTemperaturetMeasureUnitDefault()
        {
            var unit = GetDomain() == DomainTypeEnum.US
                ? HealthMeasureUnitEnum.Fahrenheit
                : HealthMeasureUnitEnum.Celsius;
            return unit;
        }

    }
}