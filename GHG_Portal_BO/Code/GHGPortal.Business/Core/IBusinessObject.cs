using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hess.Corporate.GHGPortal.Business
{
    public interface IBusinessObject
    {
        bool IsNew { get; }
        object GetIdValue();
        object Save();
        void CopyFromData(object data);
    }

}