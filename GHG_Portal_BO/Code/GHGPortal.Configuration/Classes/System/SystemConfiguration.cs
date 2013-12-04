using System.Configuration;

namespace Hess.Corporate.GHGPortal.Configuration
{
    public class SystemConfiguration : ConfigurationSection
    {
        #region Configuration Properties

        [ConfigurationProperty("systems", IsDefaultCollection = false), ConfigurationCollection(typeof(Configuration.System), AddItemName = "add")]
        public Configuration.Systems Systems
        {
            get { return (Configuration.Systems)this["systems"]; }
        }

        [ConfigurationProperty("systemConnections", IsDefaultCollection = false), ConfigurationCollection(typeof(Configuration.SiteConfigurationElement), AddItemName = "add")]
        public Configuration.SiteConfigurationElements SystemConnections
        {
            get { return (Configuration.SiteConfigurationElements)this["systemConnections"]; }
        }

        [ConfigurationProperty("systemSupportEmails", IsDefaultCollection = false), ConfigurationCollection(typeof(Configuration.SystemSupportEmail), AddItemName = "add")]
        public Configuration.SystemSupportEmails SystemSupportEmails
        {
            get { return (Configuration.SystemSupportEmails)this["systemSupportEmails"]; }
        }

        #endregion
    }
}