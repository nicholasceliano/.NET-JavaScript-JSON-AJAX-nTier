using System;
using System.Configuration;

namespace Hess.Corporate.GHGPortal.Configuration
{
    public class SystemSupportEmails : ConfigurationElementCollectionBase<SystemSupportEmail> { }

    public class SystemSupportEmail : ConfigurationElementBase
    {
        #region Configuration Properties

        [ConfigurationProperty("_configurationName", IsRequired = true)]
        public string ConfigurationName
        {
            get { return Convert.ToString(this["_configurationName"]); }
        }

        [ConfigurationProperty("address", IsRequired = true)]
        public string Address
        {
            get { return Convert.ToString(this["address"]); }
        }

        #endregion

        #region Method Overrides

        public override object GetKeyValue()
        {
            return this.ConfigurationName;
        }

        #endregion
    }
}