using Microsoft.Extensions.Configuration;
using System;

namespace ErishaBusiness.Services
{
    public class SiteKeys
    {
        private static IConfigurationSection _configuration;
        public static void Configure(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }
        public static string SmtpServer => _configuration["SmtpServer"];

        public static string SMTPUserName => _configuration["SMTPUserName"];

        public static string SMTPUserPassword => _configuration["SMTPUserPassword"];

        public static string SmtpPort => _configuration["SmtpPort"];

        public static string Domain => _configuration["Domain"];
        public static string DisplayDateFormat => _configuration["DisplayDateFormat"];

        public static string MailFromEmail => _configuration["MailFromEmail"];

        public static string MailBCC => _configuration["MailBCC"];

        public static string MailFromName => _configuration["MailFromName"];

        public static string EmailTemplatePath => _configuration["EmailTemplatePath"];

        public static string ApiDomain => _configuration["ApiDomain"];
        public static string Token => _configuration["Secret"];

        public static string AssetsDomain => _configuration["AssetsDomain"];
        public static string SharedFolder => _configuration["SharedFolder"];
        public static string SharedReviewFolder => _configuration["SharedReviewFolder"];
        public static string Chippertext => _configuration["Chippertext"];
        public static string DefaultTimeZoneId => _configuration["DefaultTimeZoneId"];
        public static string DefaultCurrencySymbol => _configuration["DefaultCurrencySymbol"];

        public static string StripeKey => _configuration["StripeKey"];
        public static long ApplicationFee => Convert.ToInt64(_configuration["ApplicationFee"]);
        public static string StripeMCC => _configuration["StripeMCC"];
        public static string DomainAdmin => _configuration["DomainAdmin"];
        public static int SettingId => Convert.ToInt32(_configuration["SettingId"]);
        public static string ImageAccessDomain => _configuration["ImageAccessDomain"];
        public static string RazorPayKeyId => _configuration["RazorPayKeyId"];
        public static string RazorPaySecret => _configuration["RazorPaySecret"];
        public static string shiprocketUserName => _configuration["shiprocketUserName"];
        public static string shiprocketPassword => _configuration["shiprocketPassword"];
        public static string ImageReviewAccessDomain => _configuration["ImageReviewAccessDomain"];
        public static int CacheTimeout => Convert.ToInt32(_configuration["CacheTimeout"]);
        public static string ErishaBusinessLiveImageFolder => _configuration["ErishaBusinessLiveImageFolder"];
        public static string ErishaBusinessAdminLiveImageFolder => _configuration["ErishaBusinessAdminLiveImageFolder"];
    }
}
