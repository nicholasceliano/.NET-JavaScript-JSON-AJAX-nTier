using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla;

namespace Hess.Corporate.GHGPortal.Business
{
    public interface IBusinessObjects
    {
        object AddNew();
        object AddNew(bool allowMultiple);
        object InsertNew(int index, bool copy);
        int CancelNew();
        object Find(object key);
        bool Delete(object key);
        int IndexOf(object item);
        System.Type GetItemType();
        int Count { get; }
        string KeyName { get; }
        bool IsEmpty { get; }
        string SortExpression { get; }
        ListSortDirection SortDirection { get; }
        void Sort();
        void Sort(string sortExpression);
        void Sort(string sortExpression, System.ComponentModel.ListSortDirection direction);
    }

}