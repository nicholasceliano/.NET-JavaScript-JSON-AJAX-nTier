using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla;
using Hess.Corporate.GHGPortal.Data;

namespace Hess.Corporate.GHGPortal.Business
{

    public class SearchPanelEntity : BusinessObjects<SearchPanelEntity, SearchPanelEntityRecord>
    {
        #region Criteria

        [Serializable()]
        protected class Criteria
        {
            // Private fields
            private string _usr;            // Facility Name
            private string _ppage;             // Source Category

            // Accessors
            public string Usr { get { return _usr; } }
            public string PPage { get { return _ppage; } }

            public Criteria()
            { }

            public Criteria(string usr, string ppage)
            {
                _usr = usr;
                _ppage = ppage;
            }
        }

        #endregion

        #region Factory methods

        public static SearchPanelEntity getSearch(string usr, string ppage)
        {
            Criteria _criteria = new Criteria(usr, ppage);
            return DataPortal.Fetch<SearchPanelEntity>(_criteria);
        }

        #endregion

        #region CSLA Overides

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria cr = (Criteria)criteria;
            DataAccess da = new DataAccess();

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(da.ExecuteStoredProcedure("prcVPeGridSearchEntity", cr.Usr, cr.PPage)))
            {
                while (dataReader.Read())
                {
                    this.Add(new SearchPanelEntityRecord(dataReader));
                }
            }
        }

        #endregion

    }
}