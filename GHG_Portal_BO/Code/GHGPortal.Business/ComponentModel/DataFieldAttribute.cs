using System;
using System.Collections.ObjectModel;

namespace Hess.Corporate.GHGPortal.Business.ComponentModel
{
    public enum LookupType { None, Key, Result }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DataFieldAttribute : Attribute
    {
        public DataFieldAttribute() : this(null) { this.Loadable = false; }

        public DataFieldAttribute(string fieldName) 
        { 
            this.FieldName = fieldName;
            this.Indexed = false;
            this.IsPrimary = false;
            this.Loadable = true;
            this.Required = false;
            this.Stored = false;
            this.IsUniquePrimary = false;
            this.Lookup = LookupType.None;
            this.LoadChildFromReader = false;
            this.Savable = true;
        }

        public string FieldName { get; private set; }

        public string DisplayName { get; private set; }

        public bool Indexed { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsUniquePrimary { get; set; }

        public bool LoadChildFromReader { get; set; }

        public bool Loadable { get; set; }

        public bool Required { get; set; }

        public bool Stored { get; set; }

        public bool Savable { get; set; }

        public LookupType Lookup { get; set; }
    }
}
