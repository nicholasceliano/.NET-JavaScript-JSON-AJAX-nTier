using System;
using System.IO;
using System.Web;
using System.Reflection;
using System.Configuration;
using System.Web.Configuration;
using System.Security.Principal;

namespace Hess.Corporate.GHGPortal.Configuration
{
    public class AppConfiguration : ConfigurationSection
    {
        #region Static Members

        protected static AppConfiguration _Current;
        protected SystemConfiguration _SystemConfiguration;
        protected ProcessConfiguration _ProcessConfiguration;
        protected EventLogConfiguration _EventLogConfiguration;
        protected global::System.Configuration.Configuration _Configuration;

        public static AppConfiguration Current
        {
            get
            {
                if (AppConfiguration._Current == null)
                {
                    global::System.Configuration.Configuration configuration = null;
                    //Get current web or exe application configuration
                    if (HttpContext.Current == null)
                        configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    else
                        configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                    //Retrieve the application configuration section from the application configuration
                    if (configuration != null)
                    {
                        AppConfiguration._Current = (AppConfiguration)configuration.Sections["appConfiguration"];
                        AppConfiguration._Current._SystemConfiguration = (SystemConfiguration)configuration.Sections["systemConfiguration"];
                        //AppConfiguration._Current._ProcessConfiguration = (ProcessConfiguration)configuration.Sections["processConfiguration"];
                        //AppConfiguration._Current._EventLogConfiguration = (EventLogConfiguration)configuration.Sections["eventLogConfiguration"];
                    }
                    else
                    {
                        AppConfiguration._Current = (AppConfiguration)global::System.Configuration.ConfigurationManager.GetSection("appConfiguration");
                        AppConfiguration._Current._SystemConfiguration = (SystemConfiguration)ConfigurationManager.GetSection("systemConfiguration");
                        //AppConfiguration._Current._ProcessConfiguration = (ProcessConfiguration)ConfigurationManager.GetSection("processConfiguration");
                        //AppConfiguration._Current._EventLogConfiguration = (EventLogConfiguration)ConfigurationManager.GetSection("eventLogConfiguration");
                    }
                    AppConfiguration._Current._Configuration = configuration;
                }
                return AppConfiguration._Current;
            }
        }

        #endregion

        #region Configuration Properies

        [ConfigurationProperty("welcomeMessages", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        private SiteConfigurationElements WelcomeMessages
        {
            get { return (SiteConfigurationElements)this["welcomeMessages"]; }
        }

        [ConfigurationProperty("maxPercentDeviation", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        private SiteConfigurationElements MaxPercentDeviation
        {
            get { return (SiteConfigurationElements)this["maxPercentDeviation"]; }
        }

        [ConfigurationProperty("envianceDataServiceUrls", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        private SiteConfigurationElements EnvianceDataServiceURLS
        {
            get { return (SiteConfigurationElements)this["envianceDataServiceUrls"]; }
        }

        [ConfigurationProperty("envianceTreeServiceUrls", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        private SiteConfigurationElements EnvianceTreeServiceURLS
        {
            get { return (SiteConfigurationElements)this["envianceTreeServiceUrls"]; }
        }

        [ConfigurationProperty("envianceAuthenticationServiceUrls", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        private SiteConfigurationElements EnvianceAuthenticationServiceURLS
        {
            get { return (SiteConfigurationElements)this["envianceAuthenticationServiceUrls"]; }
        }

        [ConfigurationProperty("SystemAdmin", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        private SiteConfigurationElements SystemAdmins
        {
            get { return (SiteConfigurationElements)this["SystemAdmin"]; }
        }

        [ConfigurationProperty("SMTPServer", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        private SiteConfigurationElements SMTPServers
        {
            get { return (SiteConfigurationElements)this["SMTPServer"]; }
        }

        [ConfigurationProperty("envianceKickoutSubject", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        private SiteConfigurationElements EnvianceKickoutSubjects
        {
            get { return (SiteConfigurationElements)this["envianceKickoutSubject"]; }
        }

        [ConfigurationProperty("monthlyValidationReminderSubject", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        private SiteConfigurationElements MonthyValidationReminderSubjects
        {
            get { return (SiteConfigurationElements)this["monthlyValidationReminderSubject"]; }
        }


        [ConfigurationProperty("envianceTechnicalErrorSubject", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        private SiteConfigurationElements EnvianceTechnicalErrorSubjects
        {
            get { return (SiteConfigurationElements)this["envianceTechnicalErrorSubject"]; }
        }

        [ConfigurationProperty("errors", IsRequired=true)]
        public Errors ErrorCodes
        {
            get { return (Errors)this["errors"]; }
        }

        [ConfigurationProperty("envianceKickoutEmailTemplate", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        public SiteConfigurationElements EnvianceKickoutEmailTemplateFiles
        {
            get { return (SiteConfigurationElements)this["envianceKickoutEmailTemplate"]; }
        }

        [ConfigurationProperty("monthlyValidationReminderTemplate", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        public SiteConfigurationElements MonthlyValidationReminders
        {
            get { return (SiteConfigurationElements)this["monthlyValidationReminderTemplate"]; }
        }

        [ConfigurationProperty("docManagerConnection", IsDefaultCollection = false), ConfigurationCollection(typeof(SiteConfigurationElement), AddItemName = "add")]
        public SiteConfigurationElements DocManagerConnection
        {
            get { return (SiteConfigurationElements)this["docManagerConnection"]; }
        }

        #endregion

        #region Public Properties

        public string ConfigurationName
        {
            get { return ConfigurationManager.AppSettings["configurationName"]; }
        }

        public string YTDvsPYTDVariance
        {
            get { return ConfigurationManager.AppSettings["YTDvsPYTDVariance"]; }
        }

        public string CWSLoginName
        {
            get { return ConfigurationManager.AppSettings["CWSLoginName"]; }
        }

        public string CWSPassword
        {
            get { return ConfigurationManager.AppSettings["CWSPassword"]; }
        }

        public string AdditionalEnvianceEmail
        {
            get { return ConfigurationManager.AppSettings["AdditionalEnvianceEmail"]; }
        }

        public string msgDataExceedsVariance
        {
            get { return ConfigurationManager.AppSettings["msgDataExceedsVariance"]; }
        }

        public string msgProductMissing1
        {
            get { return ConfigurationManager.AppSettings["msgProductMissing1"]; }
        }

        public string msgProductMissing2
        {
            get { return ConfigurationManager.AppSettings["msgProductMissing2"]; }
        }

        public string msgPreviousValidationHasBeenChanged1
        {
            get { return ConfigurationManager.AppSettings["msgPreviousValidationHasBeenChanged1"]; }
        }

        public string msgPreviousValidationHasBeenChanged2
        {
            get { return ConfigurationManager.AppSettings["msgPreviousValidationHasBeenChanged2"]; }
        }

        public string msgBLADESEstimatedVol
        {
            get { return ConfigurationManager.AppSettings["msgBLADESEstimatedVol"]; }
        }

        public string resDataExceedsVariance
        {
            get { return ConfigurationManager.AppSettings["resDataExceedsVariance"]; }
        }

        public string resProductMissing
        {
            get { return ConfigurationManager.AppSettings["resProductMissing"]; }
        }

        public string resPreviousValidationHasBeenChanged
        {
            get { return ConfigurationManager.AppSettings["resPreviousValidationHasBeenChanged"]; }
        }

        public string resBLADESEstimatedVol
        {
            get { return ConfigurationManager.AppSettings["resBLADESEstimatedVol"]; }
        }

        public Configuration.Systems Systems
        {
            get { return this._SystemConfiguration.Systems; }
        }

        public Configuration.Processes Processes
        {
            get { return this._ProcessConfiguration.Processes; }
        }

        public string SystemSupportEmail
        {
            get
            {
                SystemSupportEmail element = this._SystemConfiguration.SystemSupportEmails[this.ConfigurationName];
                if (element != null) return element.Address; else return string.Empty;
            }
        }

        public string EnvianceDataServiceURL
        {
            get
            {
                SiteConfigurationElement element = this.EnvianceDataServiceURLS[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.Value;
                else
                    return String.Empty;
            }
        }
        public string EnvianceTreeServiceURL
        {
            get
            {
                SiteConfigurationElement element = this.EnvianceTreeServiceURLS[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.Value;
                else
                    return String.Empty;
            }
        }
        public string EnvianceAuthenticationServiceURL
        {
            get
            {
                SiteConfigurationElement element = this.EnvianceAuthenticationServiceURLS[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.Value;
                else
                    return String.Empty;
            }
        }

        public string EnvianceUsername
        {
            get
            {
                SiteConfigurationElement element = this.EnvianceAuthenticationServiceURLS[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null && element.UserName != null)
                    return element.UserName;
                else
                    return String.Empty;
            }
        }

        public string EnviancePassword
        {
            get
            {
                SiteConfigurationElement element = this.EnvianceAuthenticationServiceURLS[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null && element.Password != null)
                    return element.Password;
                else
                    return String.Empty;
            }
        }

        public string SMTPServer
        {
            get
            {
                SiteConfigurationElement element = this.SMTPServers[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.Value;
                else
                    return String.Empty;
            }
        }

        public string SystemAdmin
        {
            get
            {
                SiteConfigurationElement element = this.SystemAdmins[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.Value;
                else
                    return String.Empty;
            }
        }

        public string EnvianceKickoutReportSubject
        {
            get
            {
                SiteConfigurationElement element = this.EnvianceKickoutSubjects[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.Value;
                else
                    return String.Empty;
            }
        }

        public string MonthyValidationReminderSubject
        {
            get
            {
                SiteConfigurationElement element = this.MonthyValidationReminderSubjects[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.Value;
                else
                    return String.Empty;
            }
        }

        public int EnvianceExecutionTimeout
        {
            get
            {
                SiteConfigurationElement element = this.EnvianceDataServiceURLS[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.ExecutionTimeout;
                else
                    return 0;
            }
        }

        public int EnvianceRetryTimeout
        {
            get
            {
                SiteConfigurationElement element = this.EnvianceDataServiceURLS[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.RetryTimeout;
                else
                    return 0;
            }
        }

        public string EnvianceTechnicalErrorSubject
        {
            get
            {
                SiteConfigurationElement element = this.EnvianceTechnicalErrorSubjects[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.Value;
                else
                    return String.Empty;
            }
        }

        public string EnvianceKickoutEmailTemplateFile
        {
            get
            {
                SiteConfigurationElement element = this.EnvianceKickoutEmailTemplateFiles[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.Value;
                else
                    return String.Empty;
            }
        }

        public string MonthlyValidationReminder
        {
            get
            {
                SiteConfigurationElement element = this.MonthlyValidationReminders[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.Value;
                else
                    return String.Empty;
            }
        }

        public string DocManagerConnections
        {
            get
            {
                SiteConfigurationElement element = this.DocManagerConnection[Enum.GetName(typeof(SystemType), SystemType.GHGPortal) + this.ConfigurationName];
                if (element != null)
                    return element.Value;
                else
                    return String.Empty;
            }
        }

        #endregion

        #region Public Methods

        public SiteConfigurationElement GetConnectionString(SystemType systemType)
        {
            return this._SystemConfiguration.SystemConnections[Enum.GetName(typeof(SystemType), systemType) + this.ConfigurationName];
        }

        public string GetConnectionStringValue(SystemType systemType)
        {
            SiteConfigurationElement element = this.GetConnectionString(systemType);
            if (element != null) return element.Value; else return string.Empty;
        }

        public string GetTransactionPage(SystemType systemType)
        {
            Configuration.System system = this._SystemConfiguration.Systems[systemType];
            if (system == null) return string.Empty; else return this.GetConnectionStringValue(system.ConnectionStringSystemType);
        }

        public string GetEventLogMessage(int messageNum)
        {
            EventLogMessage element = this._EventLogConfiguration.EventLogMessages[messageNum.ToString()];
            if (element != null) return element.Value; else return string.Empty;
        }

        public string GetEventLogMessageResolution(int messageNum)
        {
            EventLogMessage element = this._EventLogConfiguration.EventLogMessages[messageNum.ToString()];
            if (element != null) return element.Resolution; else return string.Empty;
        }

        public bool EventLogMessageIsIgnored(string message)
        {
            if (string.IsNullOrEmpty(message)) return true;
            foreach (HashConfigurationElement item in this._EventLogConfiguration.IgnoredEventLogMessages)
            {
                bool ignore = true;
                foreach (string messageToken in item.Value.Split('%'))
                {
                    if (!message.Contains(messageToken.Trim())) ignore = false;
                }
                if (ignore) return true;
            }
            return false;
        }

        ///<summary>Writes the configuration settings for the current application configuration object to the XML configuration file</summary> 
        public void Save()
        {
            if (this._Configuration == null) return;
            WindowsImpersonationContext impersonationContext = null;
            if (HttpContext.Current != null) impersonationContext = 
                ((WindowsIdentity)HttpContext.Current.User.Identity).Impersonate();
            this._Configuration.Save(ConfigurationSaveMode.Modified);
            if (HttpContext.Current == null && impersonationContext == null)
            {
                SiteConfigurationElement pdmAppServer = this.GetConnectionString(SystemType.GHGPortal); if (pdmAppServer == null) return;
                impersonationContext = ServiceIdentity.Impersonate(pdmAppServer.UserName, pdmAppServer.Password);
                File.Copy(string.Format("{0}\\{1}", Path.GetDirectoryName(this._Configuration.FilePath), this.SectionInformation.ConfigSource), 
                    string.Format("{0}\\{1}", pdmAppServer.Value, this.SectionInformation.ConfigSource), true);
            }
            if (impersonationContext != null) impersonationContext.Undo();
        }

        #endregion
    }
}
