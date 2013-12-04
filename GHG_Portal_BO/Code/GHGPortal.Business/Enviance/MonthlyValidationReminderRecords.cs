using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hess.Corporate.GHGPortal.Data;
using System.Xml;
using System.Xml.Serialization;

namespace Hess.Corporate.GHGPortal.Business
{

    public class MonthlyValidationReminderRecords:BusinessObjects<MonthlyValidationReminderRecords, MonthlyValidationReminderRecord>
    {
        #region Constructor
        public MonthlyValidationReminderRecords()
        {

        }
        #endregion

        #region Data Access
        public static MonthlyValidationReminderRecords GetRecords()
        {
            return DataPortal.Fetch<MonthlyValidationReminderRecords>(null);
        }

        protected override void DataPortal_Fetch(object criteria)
        {
            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(new DataAccess().ExecuteStoredProcedure("prcEnvianceGetUnapprovedInterfaceRecords")))
            {
                while (dataReader.Read())
                {
                    this.Add(new MonthlyValidationReminderRecord(dataReader));
                }
            }
        }

        #endregion

    }
}