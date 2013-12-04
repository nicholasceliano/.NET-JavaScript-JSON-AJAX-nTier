using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Hess.Corporate.GHGPortal.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    public class WorkflowHistory: BusinessObjects<WorkflowHistory, WorkflowHistoryRecord>
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public string Facility { get; set; }
        public string Entity { get; set; }

        [Serializable()]
        private class Criteria
        {
            public string Month { get; set; }
            public string Year { get; set; }
            public string Facility { get; set; }
            public string Entity { get; set; }
            
            public Criteria(string month, string year, string facility, string entity)
            {
                Month = month;
                Year = year;
                Facility = facility;
                Entity = entity;
            }
        }

        public WorkflowHistory()
        {
        }

        public static WorkflowHistory GetRecords(string month, string year, string facility, string entity)
        {            
            return DataPortal.Fetch<WorkflowHistory>(new Criteria(month, year, facility, entity));
        }

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria c = (Criteria)criteria;
            
            this.Month = c.Month;
            this.Year = c.Year;
            this.Facility= c.Facility;
            this.Entity = c.Entity;

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(new DataAccess().ExecuteStoredProcedure("prcVPeGridWorkflowHistory", c.Month, c.Year, c.Facility, c.Entity)))
            {
                while (dataReader.Read())
                {
                    WorkflowHistoryRecord record = new WorkflowHistoryRecord(dataReader);
                    this.Add(record);
                }
            }
        }
    }
}