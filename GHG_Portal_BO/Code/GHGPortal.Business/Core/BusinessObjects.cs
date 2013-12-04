using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Hess.Corporate.GHGPortal.Business
{
    public abstract class BusinessObjects<T, C> : Csla.ReadOnlyListBase<T, C>, IBusinessObjects
        where T : BusinessObjects<T, C>
        where C : Csla.Core.BusinessBase
    {

        protected bool _IsSorted = false;
        protected string _SortExpression = string.Empty;
        protected System.ComponentModel.ListSortDirection _SortDirection;
        protected List<C> _OriginalList = new List<C>();

        #region Factory Members

        public System.Type GetItemType()
        {
            return typeof(C);
        }

        protected int ItemCount
        {
            get { return this.Count; }
        }
        int IBusinessObjects.Count
        {
            get { return ItemCount; }
        }

        public bool IsEmpty
        {
            get { return this.Count == 0; }
        }

        protected object AddNewItem()
        {
            return this.AddNewItem(false);
        }
        object IBusinessObjects.AddNew()
        {
            return AddNewItem();
        }

        protected object AddNewItem(bool allowMultiple)
        {
            if (!allowMultiple)
                this.CancelNewItem();
            return this.AddNew();
        }
        object IBusinessObjects.AddNew(bool allowMultiple)
        {
            return AddNewItem(allowMultiple);
        }

        public C AddNew()
          {
              RaiseListChangedEvents = false;
              this.IsReadOnly = false;
              C item = base.AddNew();
              this.IsReadOnly = true;
              RaiseListChangedEvents = true;
              return item;
          }

        public void Add(C item)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            base.Add(item);
            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }
       
        protected object InsertNewItem(int index, bool copy)
        {
            try
            {
                C item = this.Items[index];
                if (item == null)
                    throw new Exception();
                if (item.IsNew)
                    return item;
                this.CancelNewItem();
                index = this.IndexOf(item);
                C newItem = Csla.DataPortal.Create<C>();
                if (copy && newItem is IBusinessObject)
                    ((IBusinessObject)newItem).CopyFromData(item);
                this.IsReadOnly = false;
                this.Insert(index, newItem);
                this.IsReadOnly = true;
                return newItem;
            }
            catch (Exception ex)
            {
                return this.AddNewItem();
            }
        }
        object IBusinessObjects.InsertNew(int index, bool copy)
        {
            return InsertNewItem(index, copy);
        }

        protected int CancelNewItem()
        {
            int i = 0;
            this.IsReadOnly = false;
            while (i < this.Count)
            {
                C item = this.Items[i];
                if (item.IsNew && this.Remove(ref item))
                {
                    this.IsReadOnly = true;
                    return i;
                }
                i += 1;
            }
            this.IsReadOnly = true;
            return -1;
        }
        int IBusinessObjects.CancelNew()
        {
            return CancelNewItem();
        }

        public bool Delete(object key)
        {
            C item = this.Find(key);
            if ((item != null))
            {
                item.Delete();
                if (item is IBusinessObject)
                {
                    ((IBusinessObject)item).Save();
                }
            }
            return this.Remove(ref item);
        }

        public bool Remove(ref C item)
        {
            this.IsReadOnly = false;
            bool result = base.Remove(item);
            this.IsReadOnly = true;
            return result;
        }

        #endregion

        #region Data Search

        public virtual string KeyName
        {
            get
            {
                foreach (PropertyInfo propertyInfo in typeof(C).GetProperties())
                {
                    System.Attribute attribute = System.Attribute.GetCustomAttribute(propertyInfo, typeof(Business.LookupFieldAttribute), true);
                    if ((attribute != null) && attribute is Business.LookupFieldAttribute)
                    {
                        if (((Business.LookupFieldAttribute)attribute).LookupFieldType == LookupFieldType.Key)
                            return propertyInfo.Name;
                    }
                }
                return string.Empty;
            }
        }

        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            int i = 0;
            while (i < this.Count)
            {
                if (this.Items[i] is IBusinessObject)
                {
                    IBusinessObject businessObject = (IBusinessObject)this.Items[i];
                    if (businessObject.GetIdValue().ToString() == key.ToString())
                        return i;
                }
                i += 1;
            }
            return -1;
        }

        public C Find(object key)
        {
            int itemIndex = this.FindCore(null, key);
            if (itemIndex == -1)
                return null;
            else
                return this.Items[itemIndex];
        }

        protected object FindItem(object key)
        {
            return this.Find(key);
        }
        object IBusinessObjects.Find(object key)
        {
            return FindItem(key);
        }

        protected int IndexOfItem(object item)
        {
            return this.IndexOf((C)item);
        }
        int IBusinessObjects.IndexOf(object item)
        {
            return IndexOfItem(item);
        }

        #endregion

        #region Sorting Methods

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override bool IsSortedCore
        {
            get { return this._IsSorted; }
        }

        public string SortExpression
        {
            get { return this._SortExpression; }
        }

        public System.ComponentModel.ListSortDirection SortDirection
        {
            get { return this._SortDirection; }
        }

        public void Sort()
        {
            if (string.IsNullOrEmpty(this._SortExpression))
                return;
            this.Sort(this._SortExpression, this._SortDirection);
        }

        public void Sort(string sortExpression)
        {
            ListSortDirection direction = ListSortDirection.Ascending;
            if (this._SortExpression == sortExpression)
                direction = (this._SortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending);
            this.Sort(sortExpression, direction);
        }

        public void Sort(string sortExpression, System.ComponentModel.ListSortDirection direction)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(C));
            PropertyDescriptor sortProperty = properties.Find(sortExpression, false);
            if ((sortProperty != null))
                this.ApplySortCore(sortProperty, direction);
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            //Store the original order of the collection
            if (this._OriginalList.Count == 0)
                this._OriginalList.AddRange(this);
            List<C> listRef = (List<C>)this.Items;
            //Let List(Of C) do the actual sorting based on the comparer
            if ((listRef != null))
            {
                listRef.Sort(new SortComparer<C>(prop, direction));
                this._IsSorted = true;
                this._SortExpression = prop.Name;
                this._SortDirection = direction;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        protected override void RemoveSortCore()
        {
            if (!_IsSorted)
                return;
            this.Clear();
            foreach (C item in this._OriginalList)
            {
                this.Add(item);
            }
            this._OriginalList.Clear();
            this._IsSorted = false;
            this._SortExpression = null;
            this._SortDirection = ListSortDirection.Ascending;
        }


        public string RenderGrid()
        {
            StringBuilder html = new StringBuilder();
            PropertyInfo[] allProperties = typeof(C)
           .GetProperties(BindingFlags.Instance | BindingFlags.Public)
           .Select(x => new
           {
               Property = x,
               Attribute = (CustomUIAttribute)Attribute.GetCustomAttribute(x, typeof(CustomUIAttribute), true)
           })
           .OrderBy(x => x.Attribute != null ? x.Attribute.FieldOrder : 999)
           .Where(x=> x.Attribute != null && !x.Attribute.FieldOrder.Equals(999))
           .Select(x => x.Property)
           .ToArray();
            html.AppendFormat("<table id='{0}' cellpadding='0' cellspacing='0' style='width:100%;border-collapse:collapse;' class='gridView'>", typeof(T).Name);
            html.Append("<thead><tr class='gridViewHeader' style='height:18px;'>");
            //iterate through each property of the busines object and get attribute value/type and length
            foreach (PropertyInfo propertyInfo in allProperties)
            {
                if (propertyInfo.PropertyType.IsEnum)continue;
                if (!Attribute.IsDefined(propertyInfo, typeof(CustomUIAttribute))) continue;
                CustomUIAttribute attribute = (CustomUIAttribute)propertyInfo.GetCustomAttributes(typeof(CustomUIAttribute), true)[0];
                html.AppendFormat("<th style='width:{1}px;'>{0}</th>", ((CustomUIAttribute)attribute).FieldDisplayName, ((CustomUIAttribute)attribute).FieldLength);
            }
            html.Append("</thead><tbody>");

            int count = 0;
            foreach (IBusinessObject b in this)
            {
                count++;
                string rowClass = count % 2 == 0 ? "gridViewAlternatingRow" : "gridViewRow";
                html.AppendFormat("<tr class='{0}'>", rowClass);

                foreach (PropertyInfo propertyInfo in allProperties)
                {
                    if (propertyInfo.PropertyType.IsEnum) continue;
                    if (!Attribute.IsDefined(propertyInfo, typeof(CustomUIAttribute))) continue;
                    CustomUIAttribute attribute = (CustomUIAttribute)propertyInfo.GetCustomAttributes(typeof(CustomUIAttribute), true)[0];
                    Type propertyType = propertyInfo.PropertyType;
                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>)) propertyType = propertyType.GetGenericArguments()[0];
                  
                    switch (attribute.Tag)
                    {
                        case HtmlTag.Label:
                            html.AppendFormat("<td style='align:{1};text-align:{1};'><span>{0}</span></td>",  propertyInfo.GetValue(b, null),  getHtmlAlignByType(propertyType));
                            break;
                        case HtmlTag.DateLabel: html.AppendFormat("<td style='align:{1};text-align:{1};'><span>{0}</span></td>", (Convert.ToDateTime(propertyInfo.GetValue(b, null))) == DateTime.MinValue ? "" : (Convert.ToDateTime(propertyInfo.GetValue(b, null))).ToString("MM/dd/yyyy"), getHtmlAlignByType(propertyType));
                            break;
                    }
                }
                html.AppendFormat("</tr>");
            }
            html.AppendFormat("</tbody></table>");
            return html.ToString();
        }

        public string getHtmlAlignByType(Type propertyType)
        {
            string align = string.Empty;
            switch (Type.GetTypeCode(propertyType))
            {
                case TypeCode.String:
                    align = "left";
                    break;
                case TypeCode.DateTime:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    align = "right";
                    break;
                default:
                    align = "left";
                    break;
            }
            return align;
        }


        #endregion

    }

}