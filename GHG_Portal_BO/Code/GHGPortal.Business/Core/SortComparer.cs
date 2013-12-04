using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.ComponentModel;

namespace Hess.Corporate.GHGPortal.Business
{
    public class SortComparer<T> : IComparer<T> where T : Csla.Core.BusinessBase
    {

        private PropertyDescriptor _PropertyDescriptor = null;

        private ListSortDirection _SortDirection = ListSortDirection.Ascending;
        public SortComparer(PropertyDescriptor propertyDescriptor, ListSortDirection sortDirection)
        {
            _PropertyDescriptor = propertyDescriptor;
            _SortDirection = sortDirection;
        }

        public int Compare(T x, T y)
        {
            int retValue = 0;
            object xValue = this._PropertyDescriptor.GetValue(x);
            object yValue = this._PropertyDescriptor.GetValue(y);
            if (xValue == null)
                return (yValue == null) ? 0 : 1 * (this._SortDirection == ListSortDirection.Ascending ? 1 : -1);
            if (yValue == null)
                return -1 *(this._SortDirection == ListSortDirection.Ascending ? 1 : -1);


            if (xValue is IComparable)
            {
                retValue = ((IComparable)xValue).CompareTo(yValue);
            }
            else if (yValue is IComparable)
            {
                retValue = ((IComparable)yValue).CompareTo(xValue);
                //If not comparable, compare String representations
            }
            else if (!x.Equals(yValue))
            {
                retValue = xValue.ToString().CompareTo(y.ToString());
            }
            return retValue * (this._SortDirection == ListSortDirection.Ascending ? 1 : -1);
        }

    }

}