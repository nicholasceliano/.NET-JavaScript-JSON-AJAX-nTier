using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Reflection;

namespace Hess.Corporate.GHGPortal.Business
{
    public class Facilities : BusinessObjects<Facilities, Facilities_Record>
    {
        #region Criteria

        [Serializable()]
        protected class Criteria
        {
            public Criteria() { }
        }

        #endregion

        #region Factory Methods

        public static Facilities GetFacilities()
        {
            return DataPortal.Fetch<Facilities>(new Criteria());
        }
        
        #endregion

        #region CSLA Overides

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria cr = (Criteria)criteria;

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(
                new DataAccess().ExecuteStoredProcedure("prcVPadmFacilityManagementGetFacilities")))
            {
                while (dataReader.Read())
                {
                    Facilities_Record record = new Facilities_Record(dataReader);
                    this.Add(record);
                }
            }
        }
        #endregion
    }
}
