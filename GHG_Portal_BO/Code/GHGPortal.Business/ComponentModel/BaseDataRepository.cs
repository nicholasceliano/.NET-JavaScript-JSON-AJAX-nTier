using System;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

namespace Hess.Corporate.GHGPortal.Business.ComponentModel
{
    public abstract class BaseDataRepository
    {
        /// <summary>
        /// Local cache of the reflected indexed data collection properties of this data repository
        /// </summary>
        private Dictionary<string, PropertyInfo> _IndexableDataCollectionPropertyInfos = new Dictionary<string, PropertyInfo>();

        public BaseDataRepository()
        {
            foreach (var property in this.GetType().GetProperties())
            {
                if (!property.PropertyType.IsSubclassOfGeneric(typeof(IndexableDataCollection<>))) continue;
                if (!property.PropertyType.GetInterfaces().Any(x => x == typeof(IIndexableDataCollection)))
                    throw new InvalidOperationException("Invalid base type for indexable data collection");
                this._IndexableDataCollectionPropertyInfos.Add(property.Name, property);
            }
        }

        public event MessageEventHandler MessageQueued;

        protected void OnMessageQueued(object sender, MessageEventArgs e)
        {
            if (this.MessageQueued != null) this.MessageQueued(this, e);
        }

        /// <summary>
        /// Loads the data for all public indexable data collection properties decorated as loadable
        /// </summary>
        /// <param name="criteria">Contains the EOD process selection criteria</param>
        public virtual void LoadData(ICriteria criteria)
        {
            try
            {
                // Find all loadable public properties of the indexable data collection type
                foreach (var propertyName in this._IndexableDataCollectionPropertyInfos.Keys)
                {
                    PropertyInfo property = this._IndexableDataCollectionPropertyInfos[propertyName];
                    if (property.GetCustomAttributes(typeof(LoadableAttribute), false).Length == 0) continue;
                    // Create an instance of the indexable data collection and cast it to IIndexableDataCollection interface, which it implements
                    IIndexableDataCollection indexableDataCollection = (IIndexableDataCollection)Activator.CreateInstance(property.PropertyType);
                    indexableDataCollection.MessageQueued += new MessageEventHandler(OnMessageQueued);
                    if (indexableDataCollection != null) indexableDataCollection.LoadData(criteria);
                    property.SetValue(this, indexableDataCollection, null);
                }
            }
            catch (Exception exception)
            {
                this.OnMessageQueued(this, new MessageEventArgs(exception));
            }
        }
    }
}
