using System;
using System.Net;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Hess.Corporate.GHGPortal.Email
{
    public class EmailSender
    {

        #region "Protected variables"

        protected NetworkCredential _objCredential;
        protected SmtpClient _objSmtpMailClient;

        #endregion

        #region "Constructors"

        public EmailSender(string smtpClient)
        {
            _objSmtpMailClient =new System.Net.Mail.SmtpClient(smtpClient);
        }


        public EmailSender(string username, string password, string domain, string mailClient):
            this(new NetworkCredential(username, password, domain), new SmtpClient(mailClient))
        {
        }
        
        public EmailSender(NetworkCredential credential,SmtpClient smtpClient)
        {
            _objCredential = credential;
            _objSmtpMailClient = smtpClient;
        }

        #endregion

        #region "Public Methods"

        /// <summary>
        /// Send email using SMTP client
        /// </summary>
        /// <param name="email"></param>
        public void  SendEmailViaSMTP(Email email)
        {
            MailMessage objMailMessage = new MailMessage();
            objMailMessage.From = new MailAddress(email.FromEmail);
            objMailMessage.Subject = email.Subject;

            //To
            string[] toEmailAddresses = email.ToEmail.Split(';');
            foreach (string toEmail in toEmailAddresses)
            {
                objMailMessage.To.Add(toEmail);
            }

            //cc
            if (email.CcEmail != null && email.CcEmail.Trim() != string.Empty)
            {
                string[] ccEmailAddresses = email.CcEmail.Split(';');
                foreach (string ccEmail in ccEmailAddresses)
                {
                    objMailMessage.CC.Add(ccEmail);
                }
            }

            //Bcc
            if (email.BccEmail != null && email.BccEmail.Trim() != string.Empty)
            {
                string[] bccEmailAddresses = email.BccEmail.Split(';');
                foreach (string bccEmail in bccEmailAddresses)
                {
                    objMailMessage.Bcc.Add(bccEmail);
                }
            }
            if (email.IsBodyHTML)
            {
                objMailMessage.Body = System.Web.HttpUtility.HtmlDecode(email.Body);
            }
            else
            {
                objMailMessage.Body = System.Web.HttpUtility.HtmlEncode(email.Body);
            }
            

            foreach (EmailAttachment objAttachment in email.Attachments)
            {
                if (objAttachment.FileBytes != null){
                    System.IO.MemoryStream stream = new System.IO.MemoryStream(objAttachment.FileBytes);
                    objMailMessage.Attachments.Add(new System.Net.Mail.Attachment(stream, objAttachment.SaveAs));
                }
                else
                    objMailMessage.Attachments.Add(new System.Net.Mail.Attachment(objAttachment.OriginalName));
            }

            objMailMessage.IsBodyHtml = email.IsBodyHTML;

            switch (email.Priority)
            {
                case Priority.High :
                    objMailMessage.Priority = MailPriority.High;
                    break;
                case Priority.Low:
                    objMailMessage.Priority = MailPriority.Low;
                    break;
                default:
                    objMailMessage.Priority = MailPriority.Normal;
                    break;
            }
            
            try
            {
                _objSmtpMailClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                //return false;
                throw ex;
            }
        }

        #endregion

    }
}
