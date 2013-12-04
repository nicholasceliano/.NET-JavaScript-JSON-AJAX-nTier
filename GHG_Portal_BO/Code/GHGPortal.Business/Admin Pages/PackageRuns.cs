using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Reflection;

namespace Hess.Corporate.GHGPortal.Business
{
    public class PackageRuns : BusinessObjects<PackageRuns, PackageRun>
    {
        #region Criteria

        [Serializable()]
        protected class Criteria
        {
            private string _packageName;

            // Accessors
            public string PackageName { get { return _packageName; } }

            public Criteria() { }

            public Criteria(string packageName) 
            {
                _packageName = packageName;
            }
        }

        #endregion

        #region Factory Methods

        public static PackageRuns GetRuns(string packageName)
        {
            return DataPortal.Fetch<PackageRuns>(new Criteria(packageName));
        }

        #endregion

        #region CSLA Overides

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria cr = (Criteria)criteria;

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(
                new DataAccess().ExecuteStoredProcedure("prcVPadmExecuteSSISGetPackageRuns", cr.PackageName)))
            {
                while (dataReader.Read())
                {
                    PackageRun record = new PackageRun(dataReader);
                    this.Add(record);
                }
            }
        }
        #endregion
    }
}