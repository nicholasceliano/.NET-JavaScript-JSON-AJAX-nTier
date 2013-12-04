using System;
using System.Configuration;

namespace Hess.Corporate.GHGPortal.Configuration
{
    public class EventLogMessages : ConfigurationElementCollectionBase<EventLogMessage> { }

    public class EventLogMessage : HashConfigurationElement
    {
        #region Configuration Properties

        [ConfigurationProperty("resolution", IsRequired = false)]
        public string Resolution
        {
            get { return Convert.ToString(this["resolution"]); }
            set { this["resolution"] = value; }
        }

        #endregion
    }
}