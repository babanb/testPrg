using System.Configuration;

namespace EmailSender
{
    public static class WebConfigHelper
    {
        private static string contactMail, supportMail;

        private static string logoForEmailTemplate;

        private static string logoSignature;

        private static string termsAndConditionsUrl,privacyPolicyUrl;

        private static string termsAndConditionsUrlIN, privacyPolicyUrlIN;

        private static string noreplyEmailFr, noreplyEmailPT;

        public static string Domain;

        private static string supportMail1;

        // private static string supportMailIN;

        private static string supportPhoneIN;

        //  private static string loginURL, websiteURL;

        private static string loginURL, websiteURL;

        public static string ContactMail
        {
            get
            {
                return string.IsNullOrEmpty(contactMail)
                    ? (contactMail = ConfigurationManager.AppSettings[Constants.ContactMail])
                    : contactMail;
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

        public static string SupportMail
        {
            get
            {
                return string.IsNullOrEmpty(supportMail)
                    ? (supportMail = ConfigurationManager.AppSettings[Constants.SupportMail])
                    : supportMail;

                //supportMail= string.IsNullOrEmpty(Domain)
                //    ? ConfigurationManager.AppSettings[Constants.SupportMail] : ((Domain == "French") ? ConfigurationManager.AppSettings[Constants.SupportMailFR] :
                //    (Domain == "India") ? ConfigurationManager.AppSettings[Constants.SupportMailIN] : ConfigurationManager.AppSettings[Constants.SupportMail]);
            }
        }

        public static string SupportMail1
        {
            get
            {
                return string.IsNullOrEmpty(supportMail1)
                      ? (supportMail1 = ConfigurationManager.AppSettings[Constants.SupportMail1])
                      : supportMail1;
            }
        }

        //public static string SupportMailIN
        //{
        //    get
        //    {
        //        return string.IsNullOrEmpty(supportMailIN)
        //              ? (supportMailIN = ConfigurationManager.AppSettings[Constants.SupportMailIN])
        //              : supportMailIN;
        //    }
        //}

        public static string SupportPhoneIN
        {
            get
            {
                return string.IsNullOrEmpty(supportPhoneIN)
                      ? (supportPhoneIN = ConfigurationManager.AppSettings[Constants.SupportPhoneIN])
                      : supportPhoneIN;
            }
        }

        public static string LogoSignatureForEmailPath
        {
            get
            {
                return string.IsNullOrEmpty(logoSignature)
                    ? (logoSignature = ConfigurationManager.AppSettings[Constants.LogoSignatureForEmailPath])
                    : logoSignature;
            }
        }

        public static string TermsAndConditionsUrl
        {
            get
            {
                if (string.IsNullOrEmpty(Domain))
                {
                    return string.IsNullOrEmpty(termsAndConditionsUrl)
                        ? (termsAndConditionsUrl = ConfigurationManager.AppSettings[Constants.TermsAndConditionsUrl])
                        : termsAndConditionsUrl;
                }
                else
                {
                    var tncURL = Domain == "French" ? ConfigurationManager.AppSettings[Constants.TermsAndConditionsUrlFR] : Domain == "India" ? ConfigurationManager.AppSettings[Constants.TermsAndConditionsUrlIN] : ConfigurationManager.AppSettings[Constants.TermsAndConditionsUrl];
                    return tncURL;
                }
            }
        }

        public static string TermsAndConditionsUrlIN
        {
            get
            {
                if (string.IsNullOrEmpty(Domain))
                {
                    return string.IsNullOrEmpty(termsAndConditionsUrlIN)
                        ? (termsAndConditionsUrlIN = ConfigurationManager.AppSettings[Constants.TermsAndConditionsUrlIN])
                        : termsAndConditionsUrlIN;
                }
                else
                {
                    var tncURL = Domain == "India" ? ConfigurationManager.AppSettings[Constants.TermsAndConditionsUrlIN] : ConfigurationManager.AppSettings[Constants.TermsAndConditionsUrl];
                    return tncURL;
                }
            }
        }

        public static string PrivacyPolicyUrl
        {
            get
            {
                if (string.IsNullOrEmpty(Domain))
                {
                    return string.IsNullOrEmpty(privacyPolicyUrl)
                        ? (privacyPolicyUrl = ConfigurationManager.AppSettings[Constants.PrivacyPolicyUrl])
                        : privacyPolicyUrl;
                }
                else
                {
                    var policyURL = Domain == "French" ? ConfigurationManager.AppSettings[Constants.PrivacyPolicyUrlFR] : Domain == "India" ? ConfigurationManager.AppSettings[Constants.PrivacyPolicyUrlIN] : ConfigurationManager.AppSettings[Constants.PrivacyPolicyUrl];
                    return policyURL;
                }
            }
        }

        public static string PrivacyPolicyUrlIN
        {
            get
            {
                if (string.IsNullOrEmpty(Domain))
                {
                    return string.IsNullOrEmpty(privacyPolicyUrlIN)
                        ? (privacyPolicyUrlIN = ConfigurationManager.AppSettings[Constants.PrivacyPolicyUrlIN])
                        : privacyPolicyUrlIN;
                }
                else
                {
                    var policyURL = Domain == "India" ? ConfigurationManager.AppSettings[Constants.PrivacyPolicyUrlIN] : ConfigurationManager.AppSettings[Constants.PrivacyPolicyUrl];
                    return policyURL;
                }
            }
        }

        public static string NoReplyEmailFr
        {
            get
            {
                return string.IsNullOrEmpty(noreplyEmailFr)
                    ? (noreplyEmailFr = ConfigurationManager.AppSettings[Constants.NoReplyEmailFr])
                    : noreplyEmailFr;
            }
        }

        public static string NoReplyEmailPT
        {
            get
            {
                return string.IsNullOrEmpty(noreplyEmailPT)
                    ? (noreplyEmailPT = ConfigurationManager.AppSettings[Constants.NoReplyEmailPT])
                    : noreplyEmailPT;
            }
        }

        public static string LoginURL
        {
            get
            {
                return string.IsNullOrEmpty(loginURL)
                   ? (loginURL = ConfigurationManager.AppSettings[Constants.LoginURL])
                   : loginURL;
            }
        }

        public static string WebsiteURL
        {
            get
            {
                return string.IsNullOrEmpty(websiteURL)
                   ? (websiteURL = ConfigurationManager.AppSettings[Constants.WebsiteURL])
                   : websiteURL;
            }
        }
    }
}