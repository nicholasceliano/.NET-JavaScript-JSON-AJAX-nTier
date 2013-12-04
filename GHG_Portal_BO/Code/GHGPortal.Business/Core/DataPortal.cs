using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hess.Corporate.GHGPortal.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    public class DataPortal
    {

        public static object Locker
        {
            get { return DataAccess.Locker; }
        }

        public static T Fetch<T>()
        {
            lock (Locker) { return Csla.DataPortal.Fetch<T>(); }
        }

        public static T Fetch<T>(object criteria)
        {
            lock (Locker) { return Csla.DataPortal.Fetch<T>(criteria); }
        }

        public static T Create<T>()
        {
            lock (Locker) { return Csla.DataPortal.Create<T>(); }
        }

        public static T Create<T>(object criteria)
        {
            lock (Locker) { return Csla.DataPortal.Create<T>(criteria); }
        }

        public static void Delete(object criteria)
        {
            lock (Locker) { Csla.DataPortal.Delete(criteria); }
        }

    }
}