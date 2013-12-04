using System;
using System.Linq.Expressions;

namespace Hess.Corporate.GHGPortal.Business.ComponentModel
{
    static class ExpressionExtension
    {
        public static string GetMemberName<T, TProperty>(this Expression<Func<T, TProperty>> propertyExpression)
        {
            return ((MemberExpression)(((LambdaExpression)(propertyExpression)).Body)).Member.Name;
        }

        public static Type GetGenericTypeByDefinition(this Type type, Type definition)
        {
            while (type != typeof(object))
            {
                var current = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (current == definition) { return type; } type = type.BaseType;
            }
            return null;
        }

        public static bool IsSubclassOfGeneric(this Type type, Type generic)
        {
            return GetGenericTypeByDefinition(type, generic) != null;
        }
    }
}
