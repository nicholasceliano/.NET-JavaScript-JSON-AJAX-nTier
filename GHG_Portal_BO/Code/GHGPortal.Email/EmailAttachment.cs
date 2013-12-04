using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hess.Corporate.GHGPortal.Email
{
    public class EmailAttachment
    {

        public EmailAttachment(string originalName)
        {
            _OriginalName = originalName;
        }

        public EmailAttachment(string originalName, string saveAs)
            : this(originalName)
        {
            _SaveAs = saveAs;
        }
        public EmailAttachment(byte[] FileBytes, string saveAs)
        {
            _FileBytes = FileBytes;
            _SaveAs = saveAs;
        }

        protected string _OriginalName = string.Empty;
        protected byte[] _FileBytes = null;

        protected string _SaveAs = string.Empty;
        public string OriginalName
        {
            get { return _OriginalName; }
            set { _OriginalName = value; }
        }

        public string SaveAs
        {
            get { return _SaveAs; }
            set { _SaveAs = value; }
        }

        public byte[] FileBytes
        {
            get { return _FileBytes; }
            set { _FileBytes = value; }
        }

        public string Ext
        {
            get
            {
                string s_FileName = _SaveAs;
                if (s_FileName == string.Empty)
                    s_FileName = _OriginalName;
                if (s_FileName.IndexOf(".") > -1)
                {
                    return s_FileName.Substring(s_FileName.IndexOf(".") + 1).ToLower();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

    }
}
