using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Reflection;

namespace Hess.Corporate.GHGPortal.Business
{
    public class Packages : BusinessObjects<Packages, Package>
    {
        #region Criteria

        [Serializable()]
        protected class Criteria
        {
            public Criteria() { }
        }

        #endregion

        #region Factory Methods

        public static Packages GetPackages()
        {
            return DataPortal.Fetch<Packages>(new Criteria());
        }

        #endregion

        #region CSLA Overides

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria cr = (Criteria)criteria;

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(
                new DataAccess().ExecuteStoredProcedure("prcVPadmExecuteSSISGetPackages")))
            {
                while (dataReader.Read())
                {
                    Package record = new Package(dataReader);
                    this.Add(record);
                }
            }
        }
        #endregion
    }
}