using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Reflection;

namespace Hess.Corporate.GHGPortal.Business
{
    public class ProcessHistoryLogs : BusinessObjects<ProcessHistoryLogs, ProcessHistoryLog>
    {

        #region Factory Methods

        public static ProcessHistoryLogs GetList(string month, string year)
        {
            return DataPortal.Fetch<ProcessHistoryLogs>(new Criteria(month,year));
        }


        #endregion

        #region Data Access

        [Serializable()]
        protected class Criteria
        {
            private string _month;
            private string _year;

            public string Month { get { return _month; } set { _month = value; } }

            public string Year { get { return _year; } set { _year = value; } }

            public Criteria() { }

            public Criteria(string month,string year)
            {
                _month = month;
                _year = year;
            }
        }

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria cr = (Criteria)criteria;
            DataAccess da = new DataAccess();

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(da.ExecuteStoredProcedure("prcVPeGridProcessLogHistory", cr.Month, cr.Year)))
                {
                    while (dataReader.Read())
                    {
                        this.Add(new ProcessHistoryLog(dataReader));
                    }
                }
            
        }
        #endregion
    }
}