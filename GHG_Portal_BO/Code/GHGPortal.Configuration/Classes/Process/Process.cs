using System;
using System.Configuration;

namespace Hess.Corporate.GHGPortal.Configuration
{
    public class Processes : ConfigurationElementCollectionBase<Process> { }

    public class Process : ConfigurationElementBase
    {
        #region Protected Members

        protected bool _IsForced = false;

        #endregion

        #region Configuration Properties

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return Convert.ToString(this["name"]); }
        }

        [ConfigurationProperty("code", IsRequired = true)]
        public string Code
        {
            get { return Convert.ToString(this["code"]); }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return Convert.ToString(this["type"]); }
        }

        #endregion

        #region Public Properties

        public string AssemblyName
        {
            get
            {
                string[] arr = this.Type.Split(',');
                if (arr.Length > 1) return arr[1];
                return string.Empty;
            }
        }

        public string TypeName
        {
            get
            {
                string[] arr = this.Type.Split(',');
                if (arr.Length > 0) return arr[0];
                return string.Empty;
            }
        }

        #endregion

        #region Method Overrides

        public override object GetKeyValue()
        {
            return this.Code;
        }

        #endregion
    }
}