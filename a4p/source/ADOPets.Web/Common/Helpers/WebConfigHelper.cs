using System.Configuration;

namespace ADOPets.Web.Common.Helpers
{
    public static class WebConfigHelper
    {
        private static string userFilesPath;

        private static string[] portugueseDomains;

        private static string[] frenchDomains;

        private static string[] usDomains;

        private static string[] inDomains;

        private static string logoFrenchDomains;

        private static string logoUSDomains;

        private static string logoINDomains;

        private static string loginLogoFrenchDomains;

        private static string loginLogoUSDomains;

        private static string loginLogoINDomains;

        private static string firstDataWebReferenceOrder;

        private static string firstDataClientCertificate;

        private static string firstDataNetworkCredentialUsername;

        private static string firstDataNetworkCredentialPassword;

        private static string doFakePayment;

       // private static string mailFrom;

        private static string adminEmail;

        private static string contactMail;

        private static string supportMail;

        private static string logoForEmailTemplate;

        private static string logoForEmailPathFR;

        private static string logoSignature;

        private static string mraform;

        private static string mraformfr;

        private static string termsAndConditionsUrl;

        private static string privacyPolicyUrl;

        //  private static string authorizePostURL;

        private static string authorizeLogin;

        private static string authorizeKey;

        private static string useAuthorizeTestMode;

        // PT Authorize Payment
        private static string PTauthorizeLogin;

        private static string PTauthorizeKey;

        private static string PTuseAuthorizeTestMode;

        // French payment info
        private static string cmcicVersion;

        private static string cmcicTpe;

        private static string cmcicCodeSociete;

        private static string cmcicCle;

        private static string cmcicServeur;

        private static string cmcicUrlOk;

        private static string cmcicUrlKo;

        private static string cmcicSite;

        //user guide
        private static string userGuidePDFPath;

        public static string UserGuidePDFPath
        {
            get
            {
                return string.IsNullOrEmpty(userGuidePDFPath)
                       ? (userGuidePDFPath = ConfigurationManager.AppSettings[Constants.UserGuidePDFPath])
                       : userGuidePDFPath;
            }
        }

        public static string UserFilesPath
        {
            get
            {
                return string.IsNullOrEmpty(userFilesPath)
                    ? (userFilesPath = ConfigurationManager.AppSettings[Constants.UserFilesPath])
                    : userFilesPath;
            }
        }

        public static string[] FrenchDomains
        {
            get
            {
                return frenchDomains ?? (frenchDomains = ConfigurationManager.AppSettings[Constants.FrenchDomains].Split(';'));
            }
        }

        public static string[] PortugueseDomains
        {
            get
            {
                return portugueseDomains ?? (portugueseDomains = ConfigurationManager.AppSettings[Constants.PortugueseDomains].Split(';'));
            }
        }

        public static string[] UsDomains
        {
            get
            {
                return usDomains ?? (usDomains = ConfigurationManager.AppSettings[Constants.USDomains].Split(';'));
            }
        }

        public static string[] InDomains
        {
            get
            {
                return inDomains ?? (inDomains = ConfigurationManager.AppSettings[Constants.INDomains].Split(';'));
            }
        }

        public static string LogoFrenchDomains
        {
            get
            {
                return string.IsNullOrEmpty(logoFrenchDomains)
                    ? (logoFrenchDomains = ConfigurationManager.AppSettings[Constants.LogoFrenchDomains])
                    : logoFrenchDomains;
            }
        }

        public static string LogoForEmailPath
        {
            get
            {
                return string.IsNullOrEmpty(logoForEmailTemplate)
                    ? (logoForEmailTemplate = ConfigurationManager.AppSettings[Constants.LogoForEmailPath])
                    : logoForEmailTemplate;
            }
        }

        public static string LogoForEmailPathFR
        {
            get
            {
                return string.IsNullOrEmpty(logoForEmailPathFR)
                    ? (logoForEmailPathFR = ConfigurationManager.AppSettings[Constants.LogoForEmailPathFR])
                    : logoForEmailPathFR;
            }
        }

        public static string LoginLogoFrenchDomains
        {
            get
            {
                return string.IsNullOrEmpty(loginLogoFrenchDomains)
                    ? (loginLogoFrenchDomains = ConfigurationManager.AppSettings[Constants.LoginLogoFrenchDomains])
                    : loginLogoFrenchDomains;
            }
        }

        public static string LoginLogoUSDomains
        {
            get
            {
                return string.IsNullOrEmpty(loginLogoUSDomains)
                    ? (loginLogoUSDomains = ConfigurationManager.AppSettings[Constants.LoginLogoUSDomains])
                    : loginLogoUSDomains;
            }
        }

        public static string LoginLogoINDomains
        {
            get
            {
                return string.IsNullOrEmpty(loginLogoINDomains)
                    ? (loginLogoINDomains = ConfigurationManager.AppSettings[Constants.LoginLogoINDomains])
                    : loginLogoUSDomains;
            }
        }

        public static string LogoINDomains
        {
            get
            {
                return string.IsNullOrEmpty(logoINDomains)
                    ? (logoINDomains = ConfigurationManager.AppSettings[Constants.LogoINDomains])
                    : logoUSDomains;
            }
        }

        public static string LogoUSDomains
        {
            get
            {
                return string.IsNullOrEmpty(logoUSDomains)
                    ? (logoUSDomains = ConfigurationManager.AppSettings[Constants.LogoUSDomains])
                    : logoUSDomains;
            }
        }

        public static string FirstDataWebReferenceOrder
        {
            get
            {
                return string.IsNullOrEmpty(firstDataWebReferenceOrder)
                    ? (firstDataWebReferenceOrder = ConfigurationManager.AppSettings[Constants.FirstDataWebReferenceOrder])
                    : firstDataWebReferenceOrder;
            }
        }

        public static string FirstDataClientCertificate
        {
            get
            {
                return string.IsNullOrEmpty(firstDataClientCertificate)
                    ? (firstDataClientCertificate = ConfigurationManager.AppSettings[Constants.FirstDataClientCertificate])
                    : firstDataClientCertificate;
            }
        }

        public static string FirstDataNetworkCredentialUsername
        {
            get
            {
                return string.IsNullOrEmpty(firstDataNetworkCredentialUsername)
                    ? (firstDataNetworkCredentialUsername = ConfigurationManager.AppSettings[Constants.FirstDataNetworkCredentialUsername])
                    : firstDataNetworkCredentialUsername;
            }
        }

        public static string FirstDataNetworkCredentialPassword
        {
            get
            {
                return string.IsNullOrEmpty(firstDataNetworkCredentialPassword)
                    ? (firstDataNetworkCredentialPassword = ConfigurationManager.AppSettings[Constants.FirstDataNetworkCredentialPassword])
                    : firstDataNetworkCredentialPassword;
            }
        }

        public static bool DoFakePayment
        {
            get
            {
                if (string.IsNullOrEmpty(doFakePayment))
                {
                    doFakePayment = ConfigurationManager.AppSettings[Constants.DoFakePayment];
                }
                return doFakePayment == "1";
            }
        }

        #region Email variables

        public static string SupportMail
        {
            get
            {
                return string.IsNullOrEmpty(supportMail)
                    ? (supportMail = ConfigurationManager.AppSettings[Constants.SupportMail])
                    : supportMail;
            }
        }

        public static string ContactMail
        {
            get
            {
                return string.IsNullOrEmpty(contactMail)
                    ? (contactMail = ConfigurationManager.AppSettings[Constants.ContactMail])
                    : contactMail;
            }
        }

        public static string AdminMail
        {
            get
            {
                return string.IsNullOrEmpty(adminEmail)
                    ? (adminEmail = ConfigurationManager.AppSettings[Constants.AdminEmail])
                    : adminEmail;
            }
        }

        #endregion

        public static string LogoSignatureForEmailPath
        {
            get
            {
                return string.IsNullOrEmpty(logoSignature)
                    ? (logoSignature = ConfigurationManager.AppSettings[Constants.LogoSignatureForEmailPath])
                    : logoSignature;
            }
        }

        public static string MraFormPdfPath
        {
            get
            {
                return string.IsNullOrEmpty(mraform)
                    ? (mraform = ConfigurationManager.AppSettings[Constants.MraFormPdfPath])
                    : mraform;
            }
        }

        public static string MraFormPdfPathFR
        {
            get
            {
                return string.IsNullOrEmpty(mraformfr)
                    ? (mraformfr = ConfigurationManager.AppSettings[Constants.MraFormPdfPathFR])
                    : mraformfr;
            }
        }

        public static string TermsAndConditionsUrl
        {
            get
            {
                return string.IsNullOrEmpty(mraform)
                    ? (termsAndConditionsUrl = ConfigurationManager.AppSettings[Constants.TermsAndConditionsUrl])
                    : termsAndConditionsUrl;
            }
        }

        public static string PrivacyPolicyUrl
        {
            get
            {
                return string.IsNullOrEmpty(privacyPolicyUrl)
                    ? (privacyPolicyUrl = ConfigurationManager.AppSettings[Constants.PrivacyPolicyUrl])
                    : privacyPolicyUrl;
            }
        }



        public static string AuthorizeLogin
        {
            get
            {
                return string.IsNullOrEmpty(authorizeLogin)
                    ? (authorizeLogin = ConfigurationManager.AppSettings[Constants.AuthorizeLogin])
                    : authorizeLogin;
            }
        }

        public static string AuthorizeKey
        {
            get
            {
                return string.IsNullOrEmpty(authorizeKey)
                    ? (authorizeKey = ConfigurationManager.AppSettings[Constants.AuthorizeKey])
                    : authorizeKey;
            }
        }

        public static bool UseAuthorizeTestMode
        {
            get
            {
                if (string.IsNullOrEmpty(useAuthorizeTestMode))
                {
                    useAuthorizeTestMode = ConfigurationManager.AppSettings[Constants.UseAuthorizeTestMode];
                }
                return useAuthorizeTestMode == "1";
            }
        }



        //Pt Paymemt Info
        public static string PTAuthorizeLogin
        {
            get
            {
                return string.IsNullOrEmpty(PTauthorizeLogin)
                    ? (PTauthorizeLogin = ConfigurationManager.AppSettings[Constants.PTAuthorizeLogin])
                    : PTauthorizeLogin;
            }
        }

        public static string PTAuthorizeKey
        {
            get
            {
                return string.IsNullOrEmpty(PTauthorizeKey)
                    ? (PTauthorizeKey = ConfigurationManager.AppSettings[Constants.PTAuthorizeKey])
                    : PTauthorizeKey;
            }
        }

        public static bool PTUseAuthorizeTestMode
        {
            get
            {
                if (string.IsNullOrEmpty(PTuseAuthorizeTestMode))
                {
                    PTuseAuthorizeTestMode = ConfigurationManager.AppSettings[Constants.PTUseAuthorizeTestMode];
                }
                return PTuseAuthorizeTestMode == "1";
            }
        }

        // French payment info
        public static string CmcicVersion
        {
            get
            {
                return string.IsNullOrEmpty(cmcicVersion)
                    ? (cmcicVersion = ConfigurationManager.AppSettings[Constants.CMCIC_VERSION])
                    : cmcicVersion;
            }
        }

        public static string CmcicTpe
        {
            get
            {
                return string.IsNullOrEmpty(cmcicTpe)
                    ? (cmcicTpe = ConfigurationManager.AppSettings[Constants.CMCIC_TPE])
                    : cmcicTpe;
            }
        }

        public static string CmcicCodeSociete
        {
            get
            {
                return string.IsNullOrEmpty(cmcicCodeSociete)
                    ? (cmcicCodeSociete = ConfigurationManager.AppSettings[Constants.CMCIC_CODESOCIETE])
                    : cmcicCodeSociete;
            }
        }

        public static string CmcicCle
        {
            get
            {
                return string.IsNullOrEmpty(cmcicCle)
                    ? (cmcicCle = ConfigurationManager.AppSettings[Constants.CMCIC_CLE])
                    : cmcicCle;
            }
        }

        public static string CmcicServeur
        {
            get
            {
                return string.IsNullOrEmpty(cmcicServeur)
                    ? (cmcicServeur = ConfigurationManager.AppSettings[Constants.CMCIC_SERVEUR])
                    : cmcicServeur;
            }
        }

        public static string CmcicUrlOk
        {
            get
            {
                return string.IsNullOrEmpty(cmcicUrlOk)
                    ? (cmcicUrlOk = ConfigurationManager.AppSettings[Constants.CMCIC_URLOK])
                    : cmcicUrlOk;
            }
        }

        public static string CmcicUrlKo
        {
            get
            {
                return string.IsNullOrEmpty(cmcicUrlKo)
                    ? (cmcicUrlKo = ConfigurationManager.AppSettings[Constants.CMCIC_URLKO])
                    : cmcicUrlKo;
            }
        }

        public static string CmcicSite
        {
            get
            {
                return string.IsNullOrEmpty(cmcicSite)
                    ? (cmcicSite = ConfigurationManager.AppSettings[Constants.CMCIC_SITE])
                    : cmcicSite;
            }
        }
    }
}