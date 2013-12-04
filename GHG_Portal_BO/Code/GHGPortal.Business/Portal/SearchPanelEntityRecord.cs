using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    [Serializable()]
    public class SearchPanelEntityRecord : BusinessObject<SearchPanelEntityRecord>
    {

        #region Business Methods
        
        protected string _RowId = string.Empty;
        protected string _EntityName = string.Empty;
        protected string _FacilityName = string.Empty;
        protected DateTime _Event_Dt = DateTime.MinValue;
        
        #endregion

        #region Constructors

        public SearchPanelEntityRecord()
        {
        }

        public SearchPanelEntityRecord(SafeDataReader dataReader)
        {
            FetchData(dataReader);
        }

        #endregion

        #region Interface functions

        protected override object GetIdValue()
        {
            return _RowId;
        }

        #endregion

        #region Read-only fields accessors

        public string RowId
        {
            get
            {
                CanReadProperty(true);
                return _RowId;
            }
        }

        public string EntityName
        {
            get
            {
                CanReadProperty(true);
                return _EntityName;
            }
        }

        public string FacilityName
        {
            get
            {
                CanReadProperty(true);
                return _FacilityName;
            }
        }

        public DateTime Event_Dt
        {
            get
            {
                CanReadProperty(true);
                return _Event_Dt;
            }
        }

        #endregion

        #region Data Access

        protected void FetchData(SafeDataReader dataReader)
        {
            _RowId = dataReader.GetInt32("RowID").ToString();
            _EntityName = dataReader.GetString("EntityName").Trim();
            _FacilityName = dataReader.GetString("FacilityName").Trim();
            _Event_Dt = dataReader.GetDateTime("Event_Dt");
            base.FetchData(dataReader);
        }

        protected override void DataPortal_Insert()
        {
        }

        protected override void DataPortal_Create()
        {
        }

        protected override void DataPortal_Update()
        {
        }

        protected override void DataPortal_Delete(object criteria)
        {
        }

        #endregion
    }

}