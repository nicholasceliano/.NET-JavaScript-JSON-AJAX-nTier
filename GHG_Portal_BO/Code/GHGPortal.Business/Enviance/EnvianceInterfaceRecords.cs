using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hess.Corporate.GHGPortal.Data;
using System.Xml;
using System.Xml.Serialization;

namespace Hess.Corporate.GHGPortal.Business
{

    public class EnvianceInterfaceRecords:BusinessObjects<EnvianceInterfaceRecords, EnvianceInterfaceRecord>
    {
        #region Constructor
        public EnvianceInterfaceRecords()
        {

        }
        #endregion

        #region Data Access
        public static EnvianceInterfaceRecords GetRecords()
        {
            return DataPortal.Fetch<EnvianceInterfaceRecords>(null);
        }

        protected override void DataPortal_Fetch(object criteria)
        {
            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(new DataAccess().ExecuteStoredProcedure("prcEnvianceGetInterfaceRecords")))
            {
                while (dataReader.Read())
                {
                    this.Add(new EnvianceInterfaceRecord(dataReader));
                }
            }
        }

        #endregion

    }
}