using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hess.Corporate.GHGPortal.Email
{
    public class Attachments : CollectionBase
    {

        public EmailAttachment Add(string originalName)
        {
            return this.Add(originalName, "");
        }

        public EmailAttachment Add(string originalName, string saveAs)
        {
            EmailAttachment o_Attachment = new EmailAttachment(originalName, saveAs);
            this.Add(o_Attachment);
            return o_Attachment;
        }

        public EmailAttachment Add(byte[] fileBytes, string saveAs)
        {
            EmailAttachment o_Attachment = new EmailAttachment(fileBytes, saveAs);
            this.Add(o_Attachment);
            return o_Attachment;
        }

        public void Add(EmailAttachment emailAttachment)
        {
            this.List.Add(emailAttachment);
        }

        public EmailAttachment this[int index]
        {
            get { return (EmailAttachment)this.List[index]; }
        }

    }
}
