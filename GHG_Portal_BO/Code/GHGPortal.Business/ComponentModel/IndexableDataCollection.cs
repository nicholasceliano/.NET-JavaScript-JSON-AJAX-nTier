using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business.ComponentModel
{
    #region Public Interfaces

    public interface ICriteria 
    {
        Configuration.SystemType SystemType { get; }

      //  Dictionary<string, string> filters { get; }        
    }

    public class DefaultCriteria : ICriteria
    {
        public DefaultCriteria() : this(Configuration.SystemType.GHGPortal)
        { 
            
        }
        public DefaultCriteria(Configuration.SystemType systemType) { this._SystemType = systemType; }
        private Configuration.SystemType _SystemType = Configuration.SystemType.GHGPortal;
        public Configuration.SystemType SystemType { get { return this._SystemType; } }
    }

    public interface IIndexableDataCollection 
    {
        void LoadData(ICriteria criteria);
        void LoadData(SafeDataReader dataReader);
        void LoadChildData(SafeDataReader dataReader);
        object FindByPrimaryKey(object keyValue);
        event MessageEventHandler MessageQueued;
        Type ItemType { get; }
    }

    #endregion

    public abstract class IndexableDataCollection<T> : IndexableCollection<T>, IIndexableDataCollection where T : BusinessBase<T>
    {
        #region Protected Members

        /// <summary>
        /// Internal structure for reflected data property attributed
        /// </summary>
        protected class DataFieldPropertyInfo
        {
            public DataFieldPropertyInfo(bool loadable, bool stored, PropertyInfo property)
            {
                this.Loadable = loadable; this.Stored = stored; this.Property = property;
            }

            public bool Loadable { get; private set; }
            public bool Stored { get; private set; }
            public PropertyInfo Property { get; private set; }
        }

        /// <summary>
        /// Local cache of reflected data properties and their attributes for the collection's item type T
        /// </summary>
        protected Dictionary<string, DataFieldPropertyInfo> DataFieldPropertyInfos = new Dictionary<string, DataFieldPropertyInfo>();


        protected Dictionary<string, DataFieldPropertyInfo> LoadChildFromDataReader = new Dictionary<string, DataFieldPropertyInfo>();

        protected override void InitializeCollection(IEnumerable<T> items, IndexSpecification<T> indexSpecification)
        {
            if (indexSpecification == null) throw new ArgumentNullException("Invalid index specification");
            // Get public properties of the collection item type
            foreach (var property in typeof(T).GetProperties())
            {
                // Check to see if this public property has been decorated as a data field using custom DataFieldAttribute
                if (!property.GetCustomAttributes(typeof(DataFieldAttribute), false).Any(p => p is DataFieldAttribute)) continue;
                DataFieldAttribute dataField = (DataFieldAttribute)property.GetCustomAttributes(typeof(DataFieldAttribute), false).First();
                // Add the property to the collection of all data bindable properties of the collection item type
                if (!string.IsNullOrEmpty(dataField.FieldName) && !this.DataFieldPropertyInfos.ContainsKey(dataField.FieldName))
                    this.DataFieldPropertyInfos.Add(dataField.FieldName, new DataFieldPropertyInfo(dataField.Loadable, dataField.Stored, property));
                // If the property is marked for indexation, add the property name to the index spec
                if (dataField.Indexed) indexSpecification.Add(property.Name);
                // Validate that only one property of the item type is defined for the primary index
                if ((dataField.IsPrimary || dataField.IsUniquePrimary) && !string.IsNullOrEmpty(this.PrimaryKeyPropertyName))
                    throw new Exception("Primary key index defined more than once");
                // If the property is marked as a primary index, set the primary index property name
                if ((dataField.IsPrimary || dataField.IsUniquePrimary)) this.PrimaryKeyPropertyName = property.Name;
                if (dataField.IsUniquePrimary) this.PrimaryKeyIsUnique = true;
                if (dataField.LoadChildFromReader) this.LoadChildFromDataReader.Add(property.Name, new DataFieldPropertyInfo(false, false, property));
            }
            // Apply index specifications and add items to the collection
            base.InitializeCollection(items, indexSpecification);
        }

        protected abstract string GetSelectQuery(ICriteria criteria);

        protected virtual void PrepareDataLoad(ICriteria criteria) { }

        protected virtual void FinalizeDataLoad(ICriteria criteria) { }

        protected void QueueException(Exception exception)
        {
            if (this.MessageQueued != null) this.MessageQueued(this, new MessageEventArgs(exception));
        }

        protected void QueueMessage(string message, EventLogEntryType messageType)
        {
            if (this.MessageQueued != null) this.MessageQueued(this, new MessageEventArgs(message, messageType));
        }

        #endregion

        #region Public Members

        public event MessageEventHandler MessageQueued;

        public virtual void LoadData(ICriteria criteria)
        {
            try
            {
                this.PrepareDataLoad(criteria); if (criteria == null) criteria = new DefaultCriteria();
                using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(new DataAccessBase().ExecuteQuery(this.GetSelectQuery(criteria))))
                {
                    while (dataReader.Read())
                    {
                        T item = System.Activator.CreateInstance<T>();
                        foreach (string fieldName in this.DataFieldPropertyInfos.Keys)
                            if (!dataReader.IsDBNull(fieldName) && this.DataFieldPropertyInfos[fieldName].Loadable)
                                item.SetValue(this.DataFieldPropertyInfos[fieldName].Property, dataReader[fieldName], this.DataFieldPropertyInfos[fieldName].Stored);
                        item.MarkOldAndClean(); this.Add(item);
                    }
                }
            }
            catch (Exception exception)
            {
                this.QueueException(exception);
            }
            finally 
            {
                this.FinalizeDataLoad(criteria);
            }
        }

        public virtual void LoadChildData(SafeDataReader dataReader)
        {
            try
            {

                T item = System.Activator.CreateInstance<T>();
                foreach (string fieldName in this.DataFieldPropertyInfos.Keys)
                    if (!dataReader.IsDBNull(fieldName) && this.DataFieldPropertyInfos[fieldName].Loadable)
                        item.SetValue(this.DataFieldPropertyInfos[fieldName].Property, dataReader[fieldName], this.DataFieldPropertyInfos[fieldName].Stored);
                item.MarkOldAndClean(); this.Add(item);

            }
            catch (Exception exception)
            {
                this.QueueException(exception);
            }
        }
        public virtual void LoadData(SafeDataReader dataReader)
        {
            try
            {
                while (dataReader.Read())
                {
                    T item = System.Activator.CreateInstance<T>();
                    foreach (string fieldName in this.DataFieldPropertyInfos.Keys)
                    {
                        
                        try
                        {
                            if (!dataReader.IsDBNull(fieldName) && this.DataFieldPropertyInfos[fieldName].Loadable)
                                item.SetValue(this.DataFieldPropertyInfos[fieldName].Property, dataReader[fieldName], this.DataFieldPropertyInfos[fieldName].Stored);
                        }
                        catch (Exception e)
                        {

                        }

                       
                    }
                    //check uniqueness of key if found return collection of items
                    List<T> items = new List<T>();
                    bool uniqueKeyExists = false;
                    var propertyValue = this.PropertyInfos[this.PrimaryKeyPropertyName].GetValue(item, null);
                    if (propertyValue != null)
                    {
                        int hashCode = propertyValue.GetHashCode();
                        if (this.Indexes[this.PrimaryKeyPropertyName].TryGetValue(hashCode, out items) && this.PrimaryKeyIsUnique) // check if unique primary key value exists so that we can roll up any duplicate keys
                        {
                            uniqueKeyExists = true;
                        }
                    }
                    item.MarkOldAndClean();
                    foreach (string fieldName in this.LoadChildFromDataReader.Keys) // Load any child properties from dataReader flat db table structure
                    {
                        IIndexableDataCollection child = (IIndexableDataCollection)Activator.CreateInstance(this.LoadChildFromDataReader[fieldName].Property.PropertyType);
                        object childPropertyValue = new object();
                        if (items != null) // child property is a collection and has elements
                        {
                            PropertyInfo propertyInfo = items[0].GetType().GetProperty(fieldName);
                            if (propertyInfo != null) child = (propertyInfo.GetValue(items[0], null) as IIndexableDataCollection); // get value of child property return a collection
                        }                        
                        child.LoadChildData(dataReader); // load child data from reader
                        item.SetValue(this.LoadChildFromDataReader[fieldName].Property, child, true); // store child data
                    }
                    if (!uniqueKeyExists) this.Add(item); //only add parent data if primary key is unique and value already exists within hash
                }

            }
            catch (Exception exception)
            {
                this.QueueException(exception);
            }
            finally
            {
            }
        }

        public IEnumerable<T> Sort(string prop, string direction)
        {
            if (direction == "ASC")
                return this.OrderBy(m => typeof(T).GetProperty(prop).GetValue(m, null));
            else
                return this.OrderByDescending(m => typeof(T).GetProperty(prop).GetValue(m, null));
        }

        public object FindByPrimaryKey(object keyValue)
        {
            return (object)this.GetByPrimaryKey(keyValue);
        }

        public Type ItemType { get { return typeof(T); } }

        #endregion
    }
}
