using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Reflection;

namespace Hess.Corporate.GHGPortal.Business
{
    public class FacilityMgmt : BusinessObjects<FacilityMgmt, FacilityMgmt_Record>
    {
        public string Facility { get; set; }

        #region Criteria

        [Serializable()]
        protected class Criteria
        {
            // Private fields
            private string _facName;

            // Accessors
            public string FacilityName { get { return _facName; } }

            public Criteria() { }

            public Criteria(string facilityName) 
            {
                _facName = facilityName;
            }
        }

        #endregion

        #region Factory Methods
        
        public static FacilityMgmt GetUsers(string facilityName)
        {
            return DataPortal.Fetch<FacilityMgmt>(new Criteria(facilityName));
        }

        #endregion

        #region CSLA Overides

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria cr = (Criteria)criteria;

            this.Facility = cr.FacilityName;

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(
               new DataAccess().ExecuteStoredProcedure("prcVPadmFacilityManagementGridGetUsers", cr.FacilityName)))
            {
                while (dataReader.Read())
                {
                    FacilityMgmt_Record record = new FacilityMgmt_Record(dataReader);
                    this.Add(record);
                }
            }
        }

        #endregion
    }
}
