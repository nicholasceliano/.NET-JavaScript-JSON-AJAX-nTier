using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    [Serializable()]
    public class SearchPanelFacilityRecord : BusinessObject<SearchPanelFacilityRecord>
    {

        #region Business Methods

        protected string _FacilityName = string.Empty;
        protected string _SourceCategoryName = string.Empty;
        protected string _AssetName = string.Empty;
        protected string _DataSourceName = string.Empty;
        protected string _AssetType = string.Empty;

        #endregion

        #region Constructors

        public SearchPanelFacilityRecord()
        {
        }

        public SearchPanelFacilityRecord(SafeDataReader dataReader)
        {
            FetchData(dataReader);
        }

        #endregion

        #region Interface functions

        protected override object GetIdValue()
        {
            return _FacilityName;
        }

        #endregion

        #region Read-only fields accessors

        public string FacilityName
        {
            get
            {
                CanReadProperty(true);
                return _FacilityName;
            }
        }

        public string SourceCategoryName
        {
            get
            {
                CanReadProperty(true);
                return _SourceCategoryName;
            }
        }

        public string AssetName
        {
            get
            {
                CanReadProperty(true);
                return _AssetName;
            }
        }

        public string DataSourceName
        {
            get
            {
                CanReadProperty(true);
                return _DataSourceName;
            }
        }

        public string AssetType
        {
            get
            {
                CanReadProperty(true);
                return _AssetType;
            }
        }

        #endregion

        #region Data Access

        protected void FetchData(SafeDataReader dataReader)
        {
            _FacilityName = dataReader.GetString("FacilityName").ToString();
            _SourceCategoryName = dataReader.GetString("SourceName").Trim();
            _AssetName = dataReader.GetString("AssetName").Trim();
            _DataSourceName = dataReader.GetString("DataSourceName").Trim();
            _AssetType = dataReader.GetString("AssetType").Trim();
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