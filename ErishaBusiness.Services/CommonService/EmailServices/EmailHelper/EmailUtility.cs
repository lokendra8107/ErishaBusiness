using ErishaBusiness.Core;
using ErishaBusiness.Services;
using System;
using System.IO;

namespace ErishaBusiness.Services.CommonService.EmailServices.EmailHelper
{
    public static class EmailUtility
    {
        public static string GetTokenValues(string content, EmailToken objToken)
        {
            string str = string.Empty;
            if (!string.IsNullOrWhiteSpace(content))
            {
                str = content.Replace("##Name##", objToken.Name)
                    .Replace("##Url##", objToken.Url)
                    .Replace("##Message##", objToken.Message)
                    .Replace("##SessionName##", objToken.SessionName)
                    .Replace("##SessionDate##", objToken.SessionDate)
                    .Replace("##SessionDuration##", objToken.SessionDuration)
                    .Replace("##SessionTime##", objToken.SessionTime)
                    .Replace("##Email##", objToken.Email)
                    .Replace("##Phone##", objToken.Phone)
                    .Replace("##UserName##", objToken.UserName)
                    .Replace("##Password##", objToken.Password)
                    .Replace("##ClassSizeName##", objToken.ClassSizeName)
                    .Replace("##ShortMessage##", objToken.ShortMessage)
                    .Replace("##twitterurl##", objToken.TwitterUrl)
                    .Replace("##facebookurl##", objToken.FacebookUrl)
                    .Replace("##youtubeurl##", objToken.YoutubeUrl)
                    .Replace("##Address##", objToken.Address)
                    .Replace("##Description##", objToken.Description)
                    .Replace("##OrderId##", objToken.OrderId)
                    .Replace("##UserEmail##", objToken.UserEmail)
                    ;
            }
            return str;
        }
        public static string GetHtmlTemplatePath(string TemplateName, EmailToken objToken)
        {
            string fileData = string.Empty;
            string templatePath = SiteKeys.EmailTemplatePath + TemplateName;
            if (!String.IsNullOrEmpty(templatePath) && File.Exists(templatePath))
            {
                using (StreamReader streamReader = File.OpenText(templatePath))
                {
                    fileData = streamReader.ReadToEnd();
                    fileData = GetTokenValues(fileData, objToken);
                }
            }
            return fileData;
        }
    }

}
