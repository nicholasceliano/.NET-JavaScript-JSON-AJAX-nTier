using System;
using System.Configuration;

namespace Hess.Corporate.GHGPortal.Configuration
{
    public class HashConfigurationElements : ConfigurationElementCollectionBase<HashConfigurationElement>
    {
        #region Public Methods

        public string ToSQLString()
        {
            string value = string.Empty;
            foreach (HashConfigurationElement item in this)
                value += string.Format(",'{0}'", item.Key);
            if (!string.IsNullOrEmpty(value)) value = value.Remove(0, 1);
            return value;
        }

        #endregion
    }

    public class HashConfigurationElement : ConfigurationElementBase
    {
        #region Configuration Properties

        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return Convert.ToString(this["key"]); }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return Convert.ToString(this["value"]); }
            set { this["value"] = value; }
        }

        #endregion

        #region Method Overrides

        public override object GetKeyValue()
        {
            return this.Key;
        }

        #endregion
    }
}