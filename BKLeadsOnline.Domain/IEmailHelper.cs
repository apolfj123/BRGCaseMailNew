using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Configuration;
using System.Net.Mail;

namespace BKLeadsOnline.Domain
{
    /// <summary>
    /// Email helper and derivatives loads the configuration files for the SMTP servers that are used in the app.  
    /// Gmail is used for text, nmotionleads (Generic SMTP) is currently used for Email.
    /// </summary>
    
    public class EmailHelper
    {   
        public string SMTPServer  { get; set; }
        public string SMTPFromAddress { get; set; }
        public int SMTPPort { get; set; }
        public bool SMTPUseSSL { get; set; }
        public string SMTPUserID { get; set; }
        public string SMTPPassword { get; set; }
        SmtpClient mailClient { get; set; }
        public byte[] _attachement;
        public string _attachmentName;

        public EmailHelper(string sMTPServer, int sMTPPort, bool sMTPUseSSL, string sMTPUserID, string sMTPPassword, string sMTPFromAddress)
        {
            SMTPServer  = sMTPServer;
            SMTPPort  = sMTPPort;
            SMTPUseSSL  = sMTPUseSSL;
            SMTPUserID  = sMTPUserID;
            SMTPPassword  = sMTPPassword;
            SMTPFromAddress = sMTPFromAddress;
            mailClient = getMailClient();
        }

        public void Send(string toAddress, string subject, string body)
        {
            MailMessage msg = new MailMessage(SMTPFromAddress, toAddress, subject, body);            
            if (_attachement != null)
            {
                Attachment att = new Attachment(new MemoryStream(_attachement), _attachmentName);
                msg.Attachments.Add(att);
            }
            
            mailClient.Send(msg);
        }

        //jsut in case we need to recreate the client
        private SmtpClient getMailClient ()
        {
            SmtpClient _mailClient = new SmtpClient(SMTPServer,  SMTPPort);
            _mailClient.Credentials = new NetworkCredential(SMTPUserID, SMTPPassword);
            _mailClient.EnableSsl = SMTPUseSSL;        
            return _mailClient;
        }
    }

    public class GMailHelper : EmailHelper
    {
        public GMailHelper()
            : base(ConfigurationManager.AppSettings["GMailSMTPServer"], Int32.Parse(ConfigurationManager.AppSettings["GMailSMTPPort"]), true, ConfigurationManager.AppSettings["GMailSMTPUserID"], ConfigurationManager.AppSettings["GMailSMTPPassword"], ConfigurationManager.AppSettings["GMailSMTPUserID"])
        {
        }
    }
    
    public class GenericMailHelper : EmailHelper
    {
        public GenericMailHelper()
            : base(ConfigurationManager.AppSettings["SMTPServer"], Int32.Parse(ConfigurationManager.AppSettings["SMTPPort"]), ConfigurationManager.AppSettings["SMTPRequireSSL"] == "true", ConfigurationManager.AppSettings["SMTPUserID"], ConfigurationManager.AppSettings["SMTPPassword"], ConfigurationManager.AppSettings["SMTPFromAddress"])
        {
        }
    }
   

}
