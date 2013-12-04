using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

[assembly: CLSCompliant(true)]
namespace Hess.Corporate.GHGPortal.Business.ComponentModel
{
    public class IndexableCollection<T> : Collection<T>
    {
        /// <summary>
        /// Local cache of reflected properties of the collection's item type T
        /// </summary>
        protected Dictionary<string, PropertyInfo> PropertyInfos = new Dictionary<string, PropertyInfo>();

        /// <summary>
        /// This defines the indexes as a dictionary of dictionaries of lists of the collection's item type
        /// </summary>
        protected Dictionary<string, Dictionary<int, List<T>>> Indexes = new Dictionary<string, Dictionary<int, List<T>>>();

        /// <summary>
        /// This defines the primary key property name for the default index search
        /// </summary>
        public string PrimaryKeyPropertyName { get; set; }
        public Boolean PrimaryKeyIsUnique { get; set; }

        public IndexableCollection() : this(new List<T>()) { }

        public IndexableCollection(IndexSpecification<T> indexSpecification) : this(new List<T>(), indexSpecification) { }

        public IndexableCollection(IEnumerable<T> items) : this(items, new IndexSpecification<T>()) { }

        public IndexableCollection(IEnumerable<T> items, IndexSpecification<T> indexSpecification)
        {
            this.InitializeCollection(items, indexSpecification);
        }

        protected virtual void InitializeCollection(IEnumerable<T> items, IndexSpecification<T> indexSpecification)
        {
            if (indexSpecification == null) throw new ArgumentNullException("Invalid index specification");
            // Save the property descriptors of public properties of the collection item type in a dictionary
            foreach (var property in typeof(T).GetProperties()) this.PropertyInfos.Add(property.Name, property);
            // Add specified indexes to the collection
            this.ApplyIndexSpecification(indexSpecification);
            // Add items to the indexed collection
            foreach (var item in items) this.Add(item);
        }

        public IndexableCollection<T> CreateIndexFor<TParameter>(Expression<Func<T, TParameter>> propertyExpression)
        {
            return this.CreateIndexFor(propertyExpression.GetMemberName());
        }

        public bool RemoveIndexFor<TParameter>(Expression<Func<T, TParameter>> propertyExpression)
        {
            var propertyName = propertyExpression.GetMemberName();
            if (this.Indexes.ContainsKey(propertyName))
                return this.Indexes.Remove(propertyName);
            return false;
        }

        public bool ContainsIndex<TParameter>(Expression<Func<T, TParameter>> propertyExpression)
        {
            return this.ContainsIndex(propertyExpression.GetMemberName());
        }

        public bool ContainsIndex(string propertyName)
        {
            return this.Indexes.ContainsKey(propertyName);
        }

        public Dictionary<int, List<T>> GetIndexByPropertyName<TParameter>(Expression<Func<T, TParameter>> propertyExpression)
        {
            return this.GetIndexByPropertyName(propertyExpression.GetMemberName());
        }

        public Dictionary<int, List<T>> GetIndexByPropertyName(string propertyName)
        {
            return this.Indexes[propertyName];
        }

        public List<T> GetByIndexedKeyValue<TParameter>(Expression<Func<T, TParameter>> propertyExpression, object keyValue)
        {
            return this.GetByIndexedKeyValue(propertyExpression.GetMemberName(), keyValue);
        }

        public List<T> GetByIndexedKeyValue(string propertyName, object keyValue)
        {
            if (keyValue == null) throw new ArgumentNullException("Invalid index key value");
            Dictionary<int, List<T>> index = this.GetIndexByPropertyName(propertyName);
            if (index == null) throw new ArgumentNullException("Invalid index property");
            if (index.ContainsKey(keyValue.GetHashCode())) return index[keyValue.GetHashCode()]; else return null;
        }

        public T GetByPrimaryKey(object keyValue)
        {
            if (string.IsNullOrEmpty(this.PrimaryKeyPropertyName))
                throw new ArgumentNullException("Primary key index property name not set");
            List<T> items = this.GetByIndexedKeyValue(this.PrimaryKeyPropertyName, keyValue);
            if (items == null || items.Count == 0) return default(T); else return items[0];
        }

        public T this[object keyValue] { get { return this.GetByPrimaryKey(keyValue); } }

        public Type GetItemType()
        {
            return typeof(T);
        }

        public new void Add(T item)
        {
            foreach (string key in this.Indexes.Keys)
                this.AddToIndex(key, item, this.Indexes[key]);
            base.Add(item);
        }

        public new bool Remove(T item)
        {
            foreach (string key in this.Indexes.Keys)
            {
                var propertyInfo = this.PropertyInfos[key];
                if (propertyInfo != null)
                    this.RemoveFromIndex(item, key, propertyInfo.GetValue(item, null));
            }
            return base.Remove(item);
        }

        public bool IsEmpty { get { return this.Count == 0; } }

        public IndexableCollection<T> ApplyIndexSpecification(IndexSpecification<T> indexSpecification)
        {
            if (indexSpecification == null) throw new ArgumentNullException("Invalid index specification");
            foreach (string propertyName in indexSpecification.IndexedProperties) this.CreateIndexFor(propertyName);
            return this;
        }

        private IndexableCollection<T> CreateIndexFor(string propertyName)
        {
            //var newIndex = new Dictionary<int, List<T>>();
            /*for (int i = 0; i < this.Count; i++)
                this.AddToIndex(propertyName, this[i], newIndex);*/
            this.Indexes.Add(propertyName, new Dictionary<int, List<T>>());
            return this;
        }

        private void AddToIndex(string propertyName, T newItem, Dictionary<int, List<T>> index)
        {
            var propertyValue = this.PropertyInfos[propertyName].GetValue(newItem, null);
            if (propertyValue != null)
            {
                List<T> items; 
                int hashCode = propertyValue.GetHashCode();
                if (index.TryGetValue(hashCode, out items))
                    items.Add(newItem);
                else
                    index.Add(hashCode, new List<T>(1) { newItem });                
            }
        }

        public bool PropertyHasIndex(string propName)
        {
            return Indexes.ContainsKey(propName);
        }

        public Dictionary<int, List<T>> GetIndexByProperty(string propName)
        {
            return Indexes[propName];
        }

        private void RemoveFromIndex(T item, string key, object itemValue)
        {
            if (itemValue == null) return;
            int hashCode = itemValue.GetHashCode();
            Dictionary<int, List<T>> index = this.Indexes[key];
            if (index.ContainsKey(hashCode)) index[hashCode].Remove(item);
        }       
    }
}