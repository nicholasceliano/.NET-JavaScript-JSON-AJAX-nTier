using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hess.Corporate.GHGPortal.Business
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredFieldAttribute : Attribute
    {

        public RequiredFieldAttribute(string displayFieldName)
        {
            _DisplayFieldName = displayFieldName;
        }

        private string _DisplayFieldName;
        public string DisplayFieldName
        {
            get { return _DisplayFieldName; }
        }

    }
}