using System;
using Hess.Corporate.GHGPortal.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    public class FacilityDataOwners:BusinessObjects<FacilityDataOwners, FacilityDataOwner>
    {
        [Serializable]
        private class Criteria
        {
            public string FacilityName { get; set; }

            public Criteria(string _facilityName)
            {
                FacilityName = _facilityName;
            }
        }

        public static FacilityDataOwners GetOwners(string facilityName)
        {
           
            return DataPortal.Fetch<FacilityDataOwners>(new Criteria(facilityName));
        }

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria c = (Criteria)criteria;
            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(new DataAccess().ExecuteStoredProcedure("prcEnvianceGetFacilityOwners", c.FacilityName)))
            {
                while(dataReader.Read())
                    this.Add(new FacilityDataOwner(dataReader));
            }
            //base.DataPortal_Fetch(criteria);
        }
    }
}