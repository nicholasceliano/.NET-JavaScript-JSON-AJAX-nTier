using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business.ComponentModel
{
    public static class IndexableCollectionExtension
    {
        public static IndexableCollection<T> ToIndexableCollection<T>(this IEnumerable<T> enumerable)
        {
            return new IndexableCollection<T>(enumerable);
        }

        public static IndexableCollection<T> ToIndexableCollection<T>(this IEnumerable<T> enumerable, IndexSpecification<T> indexSpecification) where T : class
        {
            return new IndexableCollection<T>(enumerable).ApplyIndexSpecification(indexSpecification);
        }

        /// <summary>
        /// Do a string Comparison of each property of a collection of Items checking for partial matches
        /// </summary>
        public static IndexableCollection<T> Contains<T>(this IndexableCollection<T> enumerable, string key)
        {
            IndexableCollection<T> tempList = new IndexableCollection<T>();
            foreach (T resultItem in enumerable)
            {
                bool itemMatch = false;
                PropertyInfo[] allProperties = resultItem.GetType().GetProperties();
                foreach (PropertyInfo prop in allProperties)
                {
                    if (prop.GetValue(resultItem, null) != null && prop.GetValue(resultItem, null).ToString().ToUpper().Contains(key.ToUpper()))
                    {
                        itemMatch = true;                       
                        break;
                    }
                }
                if (itemMatch) tempList.Add(resultItem);
            }
            return tempList;
        }
        
        public static IEnumerable<TResult> Join<T, TInner, TKey, TResult>(this IndexableCollection<T> outer, IndexableCollection<TInner> inner, Expression<Func<T, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Func<T, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            bool haveIndex = false;
            if (outer == null || inner == null || outerKeySelector == null || innerKeySelector == null || resultSelector == null) throw new ArgumentNullException();
            if (innerKeySelector.NodeType == ExpressionType.Lambda && innerKeySelector.Body.NodeType == ExpressionType.MemberAccess && 
                outerKeySelector.NodeType == ExpressionType.Lambda && outerKeySelector.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression membExpInner = (MemberExpression)innerKeySelector.Body;
                MemberExpression membExpOuter = (MemberExpression)outerKeySelector.Body;
                Dictionary<int, List<TInner>> innerIndex = new Dictionary<int, List<TInner>>();
                Dictionary<int, List<T>> outerIndex = new Dictionary<int, List<T>>();
                if (inner.ContainsIndex(membExpInner.Member.Name) && outer.ContainsIndex(membExpOuter.Member.Name))
                {
                    innerIndex = inner.GetIndexByPropertyName(membExpInner.Member.Name);
                    outerIndex = outer.GetIndexByPropertyName(membExpOuter.Member.Name);
                    haveIndex = true;
                }
                if (haveIndex)
                {
                    foreach (int outerKey in outerIndex.Keys)
                    {
                        List<T> outerGroup = outerIndex[outerKey];
                        List<TInner> innerGroup;
                        if (innerIndex.TryGetValue(outerKey, out innerGroup))
                        {
                            // Join the groups based on key result
                            IEnumerable<TInner> innerEnum = innerGroup.AsEnumerable<TInner>();
                            IEnumerable<T> outerEnum = outerGroup.AsEnumerable<T>();
                            IEnumerable<TResult> result = outerEnum.Join<T, TInner, TKey, TResult>(innerEnum, outerKeySelector.Compile(), innerKeySelector.Compile(), resultSelector, comparer);
                            foreach (TResult resultItem in result) yield return resultItem;
                        }
                    }
                }
            }
            if (!haveIndex)
            {
                // This will happen if the keys aren't in the right places
                IEnumerable<TInner> innerEnum = inner.AsEnumerable<TInner>();
                IEnumerable<T> outerEnum = outer.AsEnumerable<T>();
                IEnumerable<TResult> result = outerEnum.Join<T, TInner, TKey, TResult>(innerEnum, outerKeySelector.Compile(), innerKeySelector.Compile(), resultSelector, comparer);
                foreach (TResult resultItem in result) yield return resultItem;
            }
        }

        public static IEnumerable<TResult> Join<T, TInner, TKey, TResult>(this IndexableCollection<T> outer, IndexableCollection<TInner> inner, Expression<Func<T, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Func<T, TInner, TResult> resultSelector)
        {
            return outer.Join<T, TInner, TKey, TResult>(inner, outerKeySelector, innerKeySelector, resultSelector, EqualityComparer<TKey>.Default);
        }

        private static bool HasIndexablePropertyOnLeft<T>(Expression leftSide, IndexableCollection<T> sourceCollection, out MemberExpression member)
        {
            MemberExpression mex = leftSide as MemberExpression; member = null;
            if (leftSide.NodeType == ExpressionType.Call)
            {
                var call = leftSide as System.Linq.Expressions.MethodCallExpression;
                if (call.Method.Name == "CompareString") mex = call.Arguments[0] as MemberExpression;
            }
            if (mex == null) return false; else member = mex;
            return sourceCollection.ContainsIndex(((MemberExpression)mex).Member.Name);

        }

        private static int? GetHashRight(Expression leftSide, Expression rightSide)
        {
            if (leftSide.NodeType == ExpressionType.Call)
            {
                var call = leftSide as System.Linq.Expressions.MethodCallExpression;
                if (call.Method.Name == "CompareString")
                {
                    LambdaExpression evalRight = Expression.Lambda(call.Arguments[1], null);
                    // Compile and invoke it, then get the resulting hash
                    return (evalRight.Compile().DynamicInvoke(null).GetHashCode());
                }
            }
            // Rightside is where we get our hash...
            switch (rightSide.NodeType)
            {
                // Shortcut constants, dont eval, will be faster
                case ExpressionType.Constant:
                    return ((ConstantExpression)rightSide).Value.GetHashCode();
                // If not constant (which is provably terminal in a tree), convert back to Lambda and eval to get the hash.
                default:
                    // Lambdas can be created from expressions
                    LambdaExpression evalRight = Expression.Lambda(rightSide, null);
                    // Compile and invoke it, then get the resulting hash
                    return (evalRight.Compile().DynamicInvoke(null).GetHashCode());
            }
        }

        // Extend the where when we are working with indexable collections! 
        public static IEnumerable<T> Where<T>(this IndexableCollection<T> sourceCollection, Expression<Func<T, bool>> expression)
        {
            // Indexes work from the hash values of that which is indexed, regardless of type
            int? hashRight = null; bool noIndex = true;
            // Indexes only work on equality expressions here
            if (expression.Body.NodeType == ExpressionType.Equal)
            {
                // Equality is a binary expression
                BinaryExpression binaryExpression = (BinaryExpression)expression.Body;
                // Get some aliases for either side
                Expression leftSide = binaryExpression.Left;
                Expression rightSide = binaryExpression.Right;
                hashRight = GetHashRight(leftSide, rightSide);
                // If we were able to create a hash from the right side (likely)...
                MemberExpression returnedExpression = null;
                if (hashRight.HasValue && HasIndexablePropertyOnLeft<T>(leftSide, sourceCollection, out returnedExpression))
                {
                    // ... cast to MemberExpression - it allows us to get the property
                    Dictionary<int, List<T>> index = sourceCollection.GetIndexByPropertyName(returnedExpression.Member.Name);
                    if (index.ContainsKey(hashRight.Value))
                    {
                        IEnumerable<T> sourceEnum = index[hashRight.Value].AsEnumerable<T>();
                        IEnumerable<T> result = sourceEnum.Where<T>(expression.Compile());
                        foreach (T item in result) yield return item;
                    } 
                    noIndex = false; // Index was found, whether it had values or not is another matter
                }
            }
            if (noIndex) // If there's no index found, then proceed the normal slow way...
            {
                IEnumerable<T> sourceEnum = sourceCollection.AsEnumerable<T>();
                IEnumerable<T> result = sourceEnum.Where<T>(expression.Compile());
                foreach (T resultItem in result) yield return resultItem;
            }
        }

        public static C Fetch<C>(ICriteria criteria)
        {
            IIndexableDataCollection indexableDataCollection = (IIndexableDataCollection)Activator.CreateInstance(typeof(C));
            if (indexableDataCollection != null) indexableDataCollection.LoadData(criteria); return (C)indexableDataCollection;
        }

        public static C Fetch<C>(SafeDataReader dataReader)
        {
            IIndexableDataCollection indexableDataCollection = (IIndexableDataCollection)Activator.CreateInstance(typeof(C));
            if (indexableDataCollection != null) indexableDataCollection.LoadData(dataReader); return (C)indexableDataCollection;
        }
       

    }
}
