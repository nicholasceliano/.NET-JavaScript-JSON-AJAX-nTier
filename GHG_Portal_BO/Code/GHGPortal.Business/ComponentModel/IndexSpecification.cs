using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Hess.Corporate.GHGPortal.Business.ComponentModel
{
    public class IndexSpecification<T>
    {
        public Collection<string> IndexedProperties { get; private set; }

        public IndexSpecification()
        {
            IndexedProperties = new Collection<string>();
        }

        public IndexSpecification<T> Add<TProperty>(Expression<Func<T, TProperty>> propertyExpressions)
        {
            return this.Add(propertyExpressions.GetMemberName());
        }

        public IndexSpecification<T> Add(string propertyName)
        {
            if (!IndexedProperties.Contains(propertyName))
                this.IndexedProperties.Add(propertyName);
            return this;
        }

        public IndexSpecification<T> Remove<TProperty>(Expression<Func<T, TProperty>> propertyExpressions)
        {
            return this.Remove(propertyExpressions.GetMemberName());
        }

        public IndexSpecification<T> Remove(string propertyName)
        {
            this.IndexedProperties.Remove(propertyName); return this;
        }
    }
}
