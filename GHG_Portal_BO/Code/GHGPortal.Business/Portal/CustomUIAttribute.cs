using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hess.Corporate.GHGPortal.Business
{
    public enum HtmlTag
    {
        Label,
        EditableLabel,
        DateLabel,
        EditableDateLabel,
        Other
    }

    public class CustomUIAttribute : Attribute
    {
        private int _FieldOrder = 999;
        private int _FieldLength;
        private HtmlTag _Tag = HtmlTag.Label;
        private string _FieldDisplayName;
        private string _ClassName = "readonly";
        private string _LookUpField;

        public CustomUIAttribute() { }
        public int FieldOrder { get { return _FieldOrder; } set { _FieldOrder = value; } }
        public int FieldLength { get { return _FieldLength; } set { _FieldLength = value; } }
        public HtmlTag Tag { get { return _Tag; } set { _Tag = value; } }
        public string FieldDisplayName { get { return _FieldDisplayName; } set { _FieldDisplayName = value; } }
        public string ClassName { get { return _ClassName.Trim(); } set { _ClassName = value; } }
        public string LookUpField { get { return _LookUpField; } set { _LookUpField = value; } }

        public CustomUIAttribute(int fieldOrder, int fieldLength, HtmlTag tag, string fieldDisplayName)
        {
            _FieldOrder = fieldOrder;
            _FieldLength = fieldLength;
            _Tag = tag;
            _FieldDisplayName = fieldDisplayName;
        }
    }
}