using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hess.Corporate.GHGPortal.Business
{
    public enum LookupFieldType
    {
        Key,
        Result
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LookupFieldAttribute : Attribute
    {
        public LookupFieldAttribute(LookupFieldType lookupFieldType)
        {
            _LookupFieldType = lookupFieldType;
        }

        private LookupFieldType _LookupFieldType;
        public LookupFieldType LookupFieldType
        {
            get { return _LookupFieldType; }
        }

    }

}