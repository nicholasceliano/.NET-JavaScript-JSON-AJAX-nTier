using System;
using System.Configuration;

namespace Hess.Corporate.GHGPortal.Configuration
{
    public enum SystemType
    {
        GHGPortal,
        DocManager
    }

    public class Systems : ConfigurationElementCollectionBase<System>
    {
        #region Public Properties

        public System this[SystemType systemType]
        {
            get { return base[Enum.GetName(typeof(SystemType), systemType)]; }
        }

        #endregion
    }

    public class System : ConfigurationElementBase
    {
        #region Configuration Properties

        [ConfigurationProperty("systemType", IsRequired = true)]
        public SystemType SystemType
        {
            get { return (SystemType)this["systemType"]; }
        }

        [ConfigurationProperty("connectionStringSystemType", IsRequired = false)]
        public SystemType ConnectionStringSystemType
        {
            get { return (SystemType)this["connectionStringSystemType"]; }
        }

        #endregion

        #region Method Overrides

        public override object GetKeyValue()
        {
            return Enum.GetName(typeof(SystemType), this.SystemType);
        }

        #endregion
    }
}