using System;
using System.Configuration;

namespace Hess.Corporate.GHGPortal.Configuration
{
    public class SiteConfigurationElements : ConfigurationElementCollectionBase<SiteConfigurationElement> { }

    public class SiteConfigurationElement : CommonConfigurationElement
    {
        #region Configuration Properties

        [ConfigurationProperty("proxyHost")]
        public string ProxyHost
        {
            get { return Convert.ToString(this["proxyHost"]); }
        }

        [ConfigurationProperty("proxyPort")]
        public string ProxyPort
        {
            get { return Convert.ToString(this["proxyPort"]); }
        }

        [ConfigurationProperty("userName")]
        public string UserName
        {
            get { return Convert.ToString(this["userName"]); }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get { return Convert.ToString(this["password"]); }
            set { this["password"] = value; }
        }

        [ConfigurationProperty("passwordExpiration")]
        public DateTime PasswordExpiration
        {
            get { return Convert.ToDateTime(this["passwordExpiration"]); }
            set { this["passwordExpiration"] = value; }
        }

        [ConfigurationProperty("executionTimeout")]
        public int ExecutionTimeout
        {
            get { return Convert.ToInt32(this["executionTimeout"]); }
        }

       [ConfigurationProperty("value")]
        public string Value
        {
            get { return Convert.ToString(this["value"]); }
        }

        [ConfigurationProperty("systemType")]
        public SystemType SystemType
        {
            get { return GHGPortal.Configuration.SystemType.GHGPortal; }
        }

        [ConfigurationProperty("_configurationName")]
        public string ConfigurationName
        {
            get { return Convert.ToString(this["_configurationName"]); }
        }

        [ConfigurationProperty("retryTimeout")]
        public int RetryTimeout
        {
            get { return Convert.ToInt32(this["retryTimeout"]); }
        }

        #endregion
    }
}