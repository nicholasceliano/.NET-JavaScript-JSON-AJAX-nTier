using System.Configuration;

namespace Hess.Corporate.GHGPortal.Configuration
{
    public class ProcessConfiguration : ConfigurationSection
    {
        #region Configuration Properties

        [ConfigurationProperty("processes", IsDefaultCollection = false), ConfigurationCollection(typeof(Process), AddItemName = "add")]
        public Processes Processes
        {
            get { return (Processes)this["processes"]; }
        }

        #endregion
    }
}