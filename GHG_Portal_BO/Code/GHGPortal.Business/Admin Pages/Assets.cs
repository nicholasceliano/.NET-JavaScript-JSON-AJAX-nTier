using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Reflection;

namespace Hess.Corporate.GHGPortal.Business
{
    public class Assets : BusinessObjects<Assets, Asset_Record>
    {
        #region Criteria

        [Serializable()]
        protected class Criteria
        {
            public Criteria() { }
        }

        #endregion

        #region Factory Methods

        public static Assets GetAssets()
        {
            return DataPortal.Fetch<Assets>(new Criteria());
        }

        #endregion

        #region CSLA Overides

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria cr = (Criteria)criteria;

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(
                new DataAccess().ExecuteStoredProcedure("prcVPadmAssetManagementGetAssets")))
            {
                while (dataReader.Read())
                {
                    Asset_Record record = new Asset_Record(dataReader);
                    this.Add(record);
                }
            }
        }
        #endregion
    }
}