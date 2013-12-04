using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla;
using Hess.Corporate.GHGPortal.Data;

namespace Hess.Corporate.GHGPortal.Business
{

    public class SearchPanelFacility : BusinessObjects<SearchPanelFacility, SearchPanelFacilityRecord>
    {
        #region Criteria

        [Serializable()]
        protected class Criteria
        {
        }

        #endregion

        #region Factory methods

        public static SearchPanelFacility getSearch()
        {
            Criteria _criteria = new Criteria();
            return DataPortal.Fetch<SearchPanelFacility>(_criteria);
        }

        #endregion

        #region CSLA Overides

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria cr = (Criteria)criteria;
            DataAccess da = new DataAccess();

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(da.ExecuteStoredProcedure("prcVPeGridSearchAsset")))
            {
                while (dataReader.Read())
                {
                    this.Add(new SearchPanelFacilityRecord(dataReader));
                }
            }
        }

        #endregion

    }
}