using ErishaBusiness.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ErishaBusiness.Services.CommonServices.EmailServices
{
    public class FlexiMail
    {
        #region Constructors-Destructors
        public FlexiMail()
        {
            //set defaults 
            myEmail = new System.Net.Mail.MailMessage();
            _MailBodyManualSupply = false;
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
        }

        private string SmtpServer => SiteKeys.SmtpServer;
        private string SMTPUserName => SiteKeys.SMTPUserName;
        private string SMTPUserPassword => SiteKeys.SMTPUserPassword;
        private string SmtpPort => SiteKeys.SmtpPort;
        #endregion

        #region  Class Data
        private string _From;
        private string _FromName;
        private string _To;
        private string _ToList;
        private string _Subject;
        private string _CC;
        private string _CCList;
        private string _BCC;
        private string _ReplyTo;
        private string _TemplateDoc;
        private string[] _ArrValues;
        private string _BCCList;
        private bool _MailBodyManualSupply;
        private string _MailBody;
        //private string _Attachment;
        private string[] _Attachment;
        private System.Net.Mail.MailMessage myEmail;

        #endregion

        #region Properties
        public string From {
            set { _From = value; }
        }
        public string FromName {
            set { _FromName = value; }
        }
        public string To {
            set { _To = value; }
        }
        public string Subject {
            set { _Subject = value; }
        }
        public string CC {
            set { _CC = value; }
        }
        public string BCC {

            set { _BCC = value; }
        }
        public string ReplyTo {
            set { _ReplyTo = value; }
        }
        public bool MailBodyManualSupply {

            set { _MailBodyManualSupply = value; }
        }
        public string MailBody {
            set { _MailBody = value; }
        }
        public string EmailTemplateFileName {
            //FILE NAME OF TEMPLATE ( MUST RESIDE IN ../EMAILTEMPLAES/ FOLDER ) 
            set { _TemplateDoc = value; }
        }
        public string[] ValueArray {
            //ARRAY OF VALUES TO REPLACE VARS IN TEMPLATE 
            set { _ArrValues = value; }
        }

        public string[] AttachFile {
            set { _Attachment = value; }
        }

        #endregion

        #region SEND EMAIL

        public void Send()
        {
            myEmail.IsBodyHtml = true;

            //set mandatory properties 
            if (_FromName == "")
                _FromName = _From;
            myEmail.From = new MailAddress(_From, _FromName);
            myEmail.Subject = _Subject;

            //---Set recipients in To List 
            _ToList = _To.Replace(";", ",");
            if (_ToList != "")
            {
                string[] arr = _ToList.Split(',');
                myEmail.To.Clear();
                if (arr.Length > 0)
                {
                    foreach (string address in arr)
                    {
                        myEmail.To.Add(new MailAddress(address));
                    }
                }
                else
                {
                    myEmail.To.Add(new MailAddress(_ToList));
                }
            }

            //---Set recipients in CC List 
            if (!String.IsNullOrWhiteSpace(_CC))
            {
                _CCList = _CC.Replace(";", ",");
                if (_CCList != "")
                {
                    string[] arr = _CCList.Split(',');
                    myEmail.CC.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.CC.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.CC.Add(new MailAddress(_CCList));
                    }
                }
            }

            //---Set recipients in BCC List 
            if (!String.IsNullOrWhiteSpace(_BCC))
            {
                _BCCList = _BCC.Replace(";", ",");
                if (_BCCList != "")
                {
                    string[] arr = _BCCList.Split(',');
                    myEmail.Bcc.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.Bcc.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.Bcc.Add(new MailAddress(_BCCList));
                    }
                }
            }

            //set mail body 
            if (_MailBodyManualSupply)
            {
                myEmail.Body = _MailBody;
            }
            else
            {
                myEmail.Body = GetHtml(_TemplateDoc);
                //& GetHtml("EML_Footer.htm") 
            }

            // set attachment 
            if (_Attachment != null)
            {
                for (int i = 0; i < _Attachment.Length; i++)
                {
                    if (_Attachment[i] != null)
                        myEmail.Attachments.Add(new Attachment(_Attachment[i]));
                }

            }

            //Send mail 
            SmtpClient client = new SmtpClient();
            client.Host = SmtpServer;
            if (client.Host != "localhost")
            {
                //client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                client.Host = SmtpServer;
                client.Credentials = new System.Net.NetworkCredential(SMTPUserName, SMTPUserPassword);
            }
            client.Timeout = 0;
            client.Send(myEmail);

        }

        public async Task SendAsync(string SmtpServer, string SMTPUserName, string SMTPUserPassword, int SMTPPort)
        {
            myEmail.IsBodyHtml = true;

            //set mandatory properties 
            if (_FromName == "")
                FromName = _From;
            myEmail.From = new MailAddress(_From, "support@ErishaBusiness.in");
            myEmail.Subject = _Subject;

            //---Set recipients in To List 
            _ToList = _To.Replace(";", ",");
            if (_ToList != "")
            {
                string[] arr = _ToList.Split(',');
                myEmail.To.Clear();
                if (arr.Length > 0)
                {
                    foreach (string address in arr)
                    {
                        myEmail.To.Add(new MailAddress(address));
                    }
                }
                else
                {
                    myEmail.To.Add(new MailAddress(_ToList));
                }
            }

            //---Set recipients in CC List 
            if (!String.IsNullOrWhiteSpace(_CC))
            {
                _CCList = _CC.Replace(";", ",");
                if (_CCList != "")
                {
                    string[] arr = _CCList.Split(',');
                    myEmail.CC.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.CC.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.CC.Add(new MailAddress(_CCList));
                    }
                }
            }

            //---Set recipients in BCC List 
            if (!String.IsNullOrWhiteSpace(_BCC))
            {
                _BCCList = _BCC.Replace(";", ",");
                if (_BCCList != "")
                {
                    string[] arr = _BCCList.Split(',');
                    myEmail.Bcc.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.Bcc.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.Bcc.Add(new MailAddress(_BCCList));
                    }
                }
            }

            //set mail body 
            if (_MailBodyManualSupply)
            {
                myEmail.Body = _MailBody;
            }
            else
            {
                myEmail.Body = GetHtml(_TemplateDoc);
                //& GetHtml("EML_Footer.htm") 
            }

            // set attachment 
            if (_Attachment != null)
            {
                for (int i = 0; i < _Attachment.Length; i++)
                {
                    if (_Attachment[i] != null)
                        myEmail.Attachments.Add(new Attachment(_Attachment[i]));
                }

            }

            //Send mail 
            SmtpClient client = new SmtpClient();
            client.Host = SmtpServer;
            if (client.Host != "localhost")
            {
                //client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                client.Host = SmtpServer;
                client.Credentials = new System.Net.NetworkCredential(SMTPUserName, SMTPUserPassword);
            }
            client.Timeout = 0;
            await client.SendMailAsync(myEmail);
        }

        public void Send(string SmtpServer, string SMTPUserName, string SMTPUserPassword, int SMTPPort)
        {
            myEmail.IsBodyHtml = true;

            //set mandatory properties 
            if (_FromName == "")
                _FromName = _From;
            myEmail.From = new MailAddress(_From, "support@ErishaBusiness.in");
            myEmail.Subject = _Subject;

            //---Set recipients in To List 
            _ToList = _To.Replace(";", ",");
            if (_ToList != "")
            {
                string[] arr = _ToList.Split(',');
                myEmail.To.Clear();
                if (arr.Length > 0)
                {
                    foreach (string address in arr)
                    {
                        myEmail.To.Add(new MailAddress(address));
                    }
                }
                else
                {
                    myEmail.To.Add(new MailAddress(_ToList));
                }
            }

            //---Set recipients in CC List 
            if (!String.IsNullOrWhiteSpace(_CC))
            {
                _CCList = _CC.Replace(";", ",");
                if (_CCList != "")
                {
                    string[] arr = _CCList.Split(',');
                    myEmail.CC.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.CC.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.CC.Add(new MailAddress(_CCList));
                    }
                }
            }

            //---Set recipients in BCC List 
            if (!String.IsNullOrWhiteSpace(_BCC))
            {
                _BCCList = _BCC.Replace(";", ",");
                if (_BCCList != "")
                {
                    string[] arr = _BCCList.Split(',');
                    myEmail.Bcc.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.Bcc.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.Bcc.Add(new MailAddress(_BCCList));
                    }
                }
            }

            //set mail body 
            if (_MailBodyManualSupply)
            {
                myEmail.Body = _MailBody;
            }
            else
            {
                myEmail.Body = GetHtml(_TemplateDoc);
                //& GetHtml("EML_Footer.htm") 
            }

            // set attachment 
            if (_Attachment != null)
            {
                for (int i = 0; i < _Attachment.Length; i++)
                {
                    if (_Attachment[i] != null)
                        myEmail.Attachments.Add(new Attachment(_Attachment[i]));
                }

            }

            //Send mail 
            SmtpClient client = new SmtpClient();
            client.Host = SmtpServer;
            client.Port = SMTPPort;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
           // client.EnableSsl = false;
            //client.UseDefaultCredentials = false;
            if (client.Host != "localhost")
            {
                client.Credentials = new System.Net.NetworkCredential(SMTPUserName, SMTPUserPassword);
            }
            client.Send(myEmail);

        }

        #endregion

        #region GetHtml
        public string GetHtml(string argTemplateDocument)
        {
            int i;
            StreamReader filePtr;
            string fileData = argTemplateDocument;

            //filePtr = File.OpenText(HttpContext.Current.Server.MapPath("~/EmailTemplate/") + argTemplateDocument);

            filePtr = File.OpenText("~/EmailTemplate/" + argTemplateDocument);

            //filePtr = File.OpenText(ConfigurationSettings.AppSettings["EMLPath"] + argTemplateDocument);
            fileData = filePtr.ReadToEnd();


            filePtr.Close();
            filePtr = null;
            if ((_ArrValues == null))
            {

                return fileData;
            }
            else
            {
                //fileData = fileData.Replace("##user##", _ArrValues[0].ToString());
                //fileData = fileData.Replace("##question##", _ArrValues[1].ToString());

                for (i = _ArrValues.GetLowerBound(0); i <= _ArrValues.GetUpperBound(0); i++)
                {

                    fileData = fileData.Replace("@v" + i.ToString() + "@", (string)_ArrValues[i]);
                }
                return fileData;
            }


        }


        /// <summary>
        /// Reads contents of a URL
        /// </summary>
        /// <param name="url"></param>
        public static String GetHtmlFromURL(String url)
        {
            HttpWebRequest myWebRequest = null;
            HttpWebResponse myWebResponse = null;
            Stream receiveStream = null;
            Encoding encode = null;
            StreamReader readStream = null;
            string text = null;

            try
            {
                myWebRequest = HttpWebRequest.Create(url) as HttpWebRequest;

                // myWebRequest.Timeout = TIMEOUT;
                // myWebRequest.ReadWriteTimeout = TIMEOUT;

                myWebResponse = myWebRequest.GetResponse() as HttpWebResponse;
                receiveStream = myWebResponse.GetResponseStream();
                encode = System.Text.Encoding.GetEncoding("utf-8");
                readStream = new StreamReader(receiveStream, encode);
                text = readStream.ReadToEnd(); //.ToLower();
                if (readStream != null) readStream.Close();
                if (receiveStream != null) receiveStream.Close();
                if (myWebResponse != null) myWebResponse.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                readStream = null;
                receiveStream = null;
                myWebResponse = null;
                myWebRequest = null;
            }
            return text;
        }

        #endregion

    }
}
