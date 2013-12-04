using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    public class WorkflowHistoryRecord : BusinessObject<WorkflowHistoryRecord>
    {

        #region Business Methods

        protected string _SourceCategory = string.Empty;
        protected string _ValidationPeriod = string.Empty;
        protected string _Status = string.Empty;
        protected string _ValidationRecords = string.Empty;
        protected string _ApprovedBy = string.Empty;
        protected DateTime _ApprovedOn = DateTime.MinValue;
        protected string _SentToEnviance = string.Empty;
        protected DateTime _SentToVendor_Dt = DateTime.MinValue;

        #endregion

        #region Constructors

        public WorkflowHistoryRecord()
        {
        }

        public WorkflowHistoryRecord(Csla.Data.SafeDataReader reader)
        {
            this.FetchData(reader);
        }

        #endregion

        #region Interface Function

        protected override object GetIdValue()
        {
            return 0;
        }

        #endregion

        #region Read-only Field Accessors

        public string SourceCategory
        {
            get
            {
                CanReadProperty(true);
                return _SourceCategory;
            }
        }

        public string ValidationPeriod
        {
            get
            {
                CanReadProperty(true);
                return _ValidationPeriod;
            }
        }

        public string Status
        {
            get
            {
                CanReadProperty(true);
                return _Status;
            }
        }

        public string ValidationRecords
        {
            get
            {
                CanReadProperty(true);
                return _ValidationRecords;
            }
        }

        public string ApprovedBy
        {
            get
            {
                CanReadProperty(true);
                return _ApprovedBy;
            }
        }

        public DateTime ApprovedOn
        {
            get
            {
                CanReadProperty(true);
                return _ApprovedOn;
            }
        }

        public string SentToEnviance
        {
            get
            {
                CanReadProperty(true);
                return _SentToEnviance;
            }
        }

        public DateTime SentToVendor_Dt
        {
            get
            {
                CanReadProperty(true);
                return _SentToVendor_Dt;
            }
        }

        #endregion

        #region Data Access

        #region Criteria

        [Serializable()]
        private class Criteria
        {
        }

        #endregion
        
        protected void FetchData(Csla.Data.SafeDataReader reader)
        {
            _SourceCategory = reader.GetString("Source Category");
            _ValidationPeriod = reader.GetString("Validation Period");
            _Status = reader.GetString("Status");
            _ValidationRecords = reader.GetString("Validated Records");
            _ApprovedBy = reader.GetString("Approved By");
            _ApprovedOn = reader.GetDateTime("Approved On");
            _SentToEnviance = reader.GetString("Sent To ENVIANCE");
            _SentToVendor_Dt = reader.GetDateTime("Sent To Vendor Date");

            base.FetchData(reader);
        }

        #region Unused Overrides

        protected override void DataPortal_Update()
        {
        }

        protected override void DataPortal_Insert()
        {
        }

        protected override void DataPortal_Create()
        {
        }

        protected override void DataPortal_Delete(object criteria)
        {
        }

        protected override void DataPortal_DeleteSelf()
        {
        }

        #endregion

        #endregion
    }
}
