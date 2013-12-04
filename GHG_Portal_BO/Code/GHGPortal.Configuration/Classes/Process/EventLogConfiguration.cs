using System.Configuration;

namespace Hess.Corporate.GHGPortal.Configuration
{
    public class EventLogConfiguration : ConfigurationSection
    {
        #region Configuration Properties

        [ConfigurationProperty("eventLogMessages", IsDefaultCollection = false), ConfigurationCollection(typeof(EventLogMessage), AddItemName = "add")]
        public EventLogMessages EventLogMessages
        {
            get { return (EventLogMessages)this["eventLogMessages"]; }
        }

        [ConfigurationProperty("ignoredEventLogMessages", IsDefaultCollection = false), ConfigurationCollection(typeof(HashConfigurationElement), AddItemName = "add")]
        public HashConfigurationElements IgnoredEventLogMessages
        {
            get { return (HashConfigurationElements)this["ignoredEventLogMessages"]; }
        }

        #endregion
    }
}