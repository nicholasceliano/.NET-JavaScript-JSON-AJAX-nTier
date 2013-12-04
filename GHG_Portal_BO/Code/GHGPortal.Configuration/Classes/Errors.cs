using System;
using System.Configuration;

namespace Hess.Corporate.GHGPortal.Configuration
{
    public class Errors: ConfigurationElement
    {
        [ConfigurationProperty("EnvianceSubmissionErrors", IsDefaultCollection=false), ConfigurationCollection(typeof(HashConfigurationElement), AddItemName="add")]
        public HashConfigurationElements EnvianceSubmissionErrors
        {
            get { return (HashConfigurationElements)this["EnvianceSubmissionErrors"]; }
        }
    }
}