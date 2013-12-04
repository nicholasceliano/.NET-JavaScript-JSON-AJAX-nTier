using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Independentsoft.Exchange;

namespace Hess.Corporate.GHGPortal.Email
{
    public class Email
    {

        #region "Constructors"

        public Email(string subject)
        {
            _Subject = subject;
        }

        #endregion

        #region "Protected variables"

        protected string _ToEmail;
        protected string _CcEmail;
        protected string _BccEmail;
        protected string _Subject;
        protected Attachments _Attachments;
        protected Boolean _isBodyHTML;
        protected string _fromEmail;
        protected Priority _priority = Priority.Normal;

        protected string _Body;
        #endregion

        #region "Public Properties"

        public string FromEmail
        {
            get
            {
                return _fromEmail;
            }
            set
            {
                _fromEmail = value;
            }
        }

        public string ToEmail
        {
            get { return _ToEmail; }
            set { _ToEmail = value; }
        }

        public string CcEmail
        {
            get { return _CcEmail; }
            set { _CcEmail = value; }
        }

        public string BccEmail
        {
            get { return _BccEmail; }
            set { _BccEmail = value; }
        }

        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        public Attachments Attachments
        {
            get
            {
                if (_Attachments == null)
                    _Attachments = new Attachments();
                return _Attachments;
            }
        }

        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }

        public Boolean IsBodyHTML
        {
            get
            {
                return _isBodyHTML;
            }
            set
            {
                _isBodyHTML = value;
            }
        }

        public Priority  Priority
        {
            get
            {
                return _priority;
            }
            set
            {
                _priority = value;
            }
        }

        #endregion

    }
}
