using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hess.Corporate.GHGPortal.Business.ComponentModel;
using Hess.Corporate.GHGPortal.Configuration;
using Hess.Corporate.GHGPortal.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    public class WorkflowHistory: IndexableDataCollection<WorkflowHistoryRecord>
    {
        public static WorkflowHistory GetAll()
        {
            return IndexableCollectionExtension.Fetch<WorkflowHistory>(new DefaultCriteria(SystemType.GHGPortal));
        }

        protected override string GetSelectQuery(ICriteria criteria)
        {
            throw new NotImplementedException();
        }

        public static WorkflowHistory GetRecords(DateTime eventDate, string facilityName, string sourceCategory)
        {
            WorkflowHistory history = new WorkflowHistory();
            string query = "select distinct " +
            "f.Facility_Name," +
            "f.Event_Dt, " +
            "f.ReviewedBy_Name," +
            "f.ReviewedOn_Dt, " +
            "f.SentToVendor_Dt," +
            "e.SourceCategory_Name," +
            "COUNT(f.ReviewedOn_Dt) as approved," +
            "0 as Volume, " +
            "0 as NonApproved " +
            "from " +
            "   factEmissions f inner join MD_tblEmittingAssets e on (f.EmittingAsset_Name = e.EmittingAsset_Name and f.Facility_Name = e.Facility_Name) " +
            "   where " +
            "Event_Dt >= '1/1/2013' " +
            "and ReviewedOn_Dt is not null " +
            "group by " +
            "f.Facility_Name, " +
            "f.Event_Dt, " +
            "e.SourceCategory_Name, " +
            "f.ReviewedOn_Dt, " +
            "f.ReviewedBy_Name, " +
            "f.SentToVendor_Dt";

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(new DataAccessBase().ExecuteQuery(query)))
            {
                history = IndexableCollectionExtension.Fetch<WorkflowHistory>(dataReader);
            }

            return history;
        }

    }
}