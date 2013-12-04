using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    [Serializable()]
    public class ViewAllValidationRecord : BusinessObject<ViewAllValidationRecord>
    {
        #region Business Methods

        protected int _RowId = 0;
        protected string _FacilityName = string.Empty;
        protected string _Validated = string.Empty;
        protected string _SourceCategory = string.Empty;
        protected string _AssetNameFull = string.Empty;
        protected DateTime _ReviewedOn_Dt = DateTime.MinValue;
        protected DateTime _ChangedOn_Dt = DateTime.MinValue;
        protected string _AssetName = string.Empty;
        protected string _AssetType = string.Empty;
        protected string _ProductName = string.Empty;
        protected string _MeterID = string.Empty;
        protected long? _ThisYTDVol = null;
        protected long? _PriorYTDVol = null;
        protected long? _ThisMonthVol = null;
        protected long? _PriorYearMonthVol = null;
        protected string _UOM = string.Empty;
        protected string _UOMshort = string.Empty;
        protected string _SourceType = string.Empty;
        protected string _DataSource = string.Empty;
        protected DateTime _ExecutedOn = DateTime.MinValue;
        protected long? _OverrideVolume_Amt = null;
        protected long? _EmissionVolume_Amt = null;
        protected string _ReviewedBy_Name = string.Empty;
        protected string _ChangedBy_Name = string.Empty;
        protected string _Source_UOM = string.Empty;
        protected long? _Source_Volume = null;
        protected string _Missing_Row = null;

        #endregion

        #region Constructors

        public ViewAllValidationRecord()
        {
        }

        public ViewAllValidationRecord(SafeDataReader dataReader)
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

        #region Update fields accessors

        public string Validated
        {
	        get {
		        CanReadProperty(true);
                return _Validated;
	        }
	        set {
		        CanWriteProperty(true);
                if (_Validated != value)
                {
                    this._Validated = value;
			        PropertyHasChanged();
		        }
	        }
        }

        public long? OverrideVolume_Amt
        {
            get
            {
                CanReadProperty(true);
                return _OverrideVolume_Amt;
            }
            set
            {
                CanWriteProperty(true);
                if (_OverrideVolume_Amt != value)
                {
                    this._OverrideVolume_Amt = value;
                    PropertyHasChanged();
                }
            }
        }

        public string ReviewedBy_Name
        {
            get
            {
                CanReadProperty(true);
                return _ReviewedBy_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_ReviewedBy_Name != value)
                {
                    this._ReviewedBy_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string ChangedBy_Name
        {
            get
            {
                CanReadProperty(true);
                return _ChangedBy_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_ChangedBy_Name != value)
                {
                    this._ChangedBy_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        #endregion

        #region Read-only fields accessors

        public int RowId
        {
            get
            {
                CanReadProperty(true);
                return _RowId;
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

        public string SourceCategory
        {
            get
            {
                CanReadProperty(true);
                return _SourceCategory;
            }
        }

        public string AssetNameFull
        {
            get
            {
                CanReadProperty(true);
                return _AssetNameFull;
            }
        }

        public DateTime ReviewedOn_Dt
        {
            get
            {
                CanReadProperty(true);
                return _ReviewedOn_Dt;
            }
        }

        public DateTime ChangedOn_Dt
        {
            get
            {
                CanReadProperty(true);
                return _ChangedOn_Dt;
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

        public string AssetType
        {
            get
            {
                CanReadProperty(true);
                return _AssetType;
            }
        }

        public string ProductName
        {
            get
            {
                CanReadProperty(true);
                return _ProductName;
            }
        }

        public string MeterID
        {
            get
            {
                CanReadProperty(true);
                return _MeterID;
            }
        }

        public long? ThisYTDVol
        {
            get
            {
                CanReadProperty(true);
                return _ThisYTDVol;
            }
        }

        public long? PriorYTDVol
        {
            get
            {
                CanReadProperty(true);
                return _PriorYTDVol;
            }
        }

        public long? ThisMonthVol
        {
            get
            {
                CanReadProperty(true);
                return _ThisMonthVol;
            }
        }

        public long? PriorYearMonthVol
        {
            get
            {
                CanReadProperty(true);
                return _PriorYearMonthVol;
            }
        }

        public string UOM
        {
            get
            {
                CanReadProperty(true);
                return _UOM;
            }
        }

        public string UOMshort
        {
            get
            {
                CanReadProperty(true);
                return _UOMshort;
            }
        }

        public string SourceType
        {
            get
            {
                CanReadProperty(true);
                return _SourceType;
            }
        }

        public string DataSource
        {
            get
            {
                CanReadProperty(true);
                return _DataSource;
            }
        }

        public DateTime ExecutedOn
        {
            get
            {
                CanReadProperty(true);
                return _ExecutedOn;
            }
        }

        public long? EmissionVolume_Amt
        {
            get
            {
                CanReadProperty(true);
                return _EmissionVolume_Amt;
            }
        }

        public string Source_UOM
        {
            get
            {
                CanReadProperty(true);
                return _Source_UOM;
            }
        }

        public long? Source_Volume
        {
            get
            {
                CanReadProperty(true);
                return _Source_Volume;
            }
        }

        public string Missing_Row
        {
            get
            {
                CanReadProperty(true);
                return _Missing_Row;
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

        protected void FetchData(SafeDataReader dataReader)
        {
            _RowId = Convert.ToInt32(dataReader.GetInt64("RowKey"));
            _Validated = dataReader.GetString("Validated").Trim();
            _FacilityName = dataReader.GetString("Facility Name").Trim();
            _SourceCategory = dataReader.GetString("Source Category").Trim();
            _AssetNameFull = dataReader.GetString("Asset Name Full").Trim();
            _AssetName = dataReader.GetString("Asset Name").Trim();
            _EmissionVolume_Amt = dataReader.IsDBNull("Emission Volume") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Emission Volume"));
            _OverrideVolume_Amt = dataReader.IsDBNull("Override Volume") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Override Volume"));
            _AssetType = dataReader.GetString("Asset Type").Trim();
            _ProductName = dataReader.GetString("Product Name").Trim();
            _MeterID = dataReader.GetString("Meter ID").Trim();
            _ThisYTDVol = dataReader.IsDBNull("This YTD Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("This YTD Vol"));
            _PriorYTDVol = dataReader.IsDBNull("Prior YTD Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Prior YTD Vol"));
            _ThisMonthVol = dataReader.IsDBNull("This Month Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("This Month Vol"));
            _PriorYearMonthVol = dataReader.IsDBNull("Prior Year Month Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Prior Year Month Vol"));
            _UOM = dataReader.GetString("UOM").Trim();
            _UOMshort = dataReader.GetString("UOMShort").Trim();
            _SourceType = dataReader.GetString("Source Type").Trim();
            _DataSource = dataReader.GetString("Data Source").Trim();
            _ExecutedOn = dataReader.GetString("Executed On").Trim() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(dataReader.GetString("Executed On"));
            _ReviewedOn_Dt =dataReader.GetDateTime("Reviewed Date");
            _ChangedOn_Dt = dataReader.GetDateTime("Changed Date");
            _ChangedBy_Name = dataReader.GetString("Changed Name").Trim();
            _Source_UOM = dataReader.GetString("Source UOM").Trim();
            _Source_Volume = dataReader.IsDBNull("Source Volume") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Source Volume"));
            _Missing_Row = dataReader.GetString("Missing Row").Trim();

            base.FetchData(dataReader);
        }

        protected override void DataPortal_Update()
        {
            //this.Update(new Data.DataAccess(), "prcVPeGridStdMasterGridupdateRow", this.ChangedBy_Name, this.ReviewedBy_Name, this.Validated, this.Comments, (this.OverrideVolume_Amt == null ? null : this.OverrideVolume_Amt.ToString().Trim().Replace(",", string.Empty)), this.RowId);
        }

        #region Unused Overrides

        protected override void DataPortal_Insert()
        {
        }

        protected override void DataPortal_Create()
        {
        }
                
        protected override void DataPortal_Delete(object criteria)
        {
        }

        #endregion

        #endregion
    }

}