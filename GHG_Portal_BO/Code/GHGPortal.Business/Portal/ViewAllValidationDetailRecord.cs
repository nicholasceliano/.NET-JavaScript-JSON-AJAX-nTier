using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    [Serializable()]
    public class ViewAllValidationDetailRecord : BusinessObject<ViewAllValidationDetailRecord>
    {

        #region Member fields

        protected int _RowKey = 0;
        protected string _AssetType = string.Empty;
        protected string _AssetName = string.Empty;
        protected string _Product = string.Empty;
        protected string _Comments = string.Empty;
        protected long? _JanVol = null;
        protected long? _FebVol = null;
        protected long? _MarVol = null;
        protected long? _AprVol = null;
        protected long? _MayVol = null;
        protected long? _JunVol = null;
        protected long? _JulVol = null;
        protected long? _AugVol = null;
        protected long? _SepVol = null;
        protected long? _OctVol = null;
        protected long? _NovVol = null;
        protected long? _DecVol = null;
        protected long? _OverrideVolume = null;
        protected string _ChangedBy_Name = string.Empty;
        protected string _Validated = string.Empty;
        protected string _ReviewedBy_Name = string.Empty;
        protected string _SentToVendor = null;

        #endregion

        #region Constructors

        public ViewAllValidationDetailRecord()
        {
        }

        public ViewAllValidationDetailRecord(SafeDataReader dataReader)
        {
            FetchData(dataReader);
        }

        #endregion

        #region Interface functions

        protected override object GetIdValue()
        {
            return _RowKey;
        }

        #endregion

        #region Update fields accessors

        public long? OverrideVolume
        {
            get
            {
                CanReadProperty(true);
                return _OverrideVolume;
            }
            set
            {
                CanWriteProperty(true);
                if (_OverrideVolume != value)
                {
                    this._OverrideVolume = value;
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

        public string Validated
        {
            get
            {
                CanReadProperty(true);
                return _Validated;
            }
            set
            {
                CanWriteProperty(true);
                if (_Validated != value)
                {
                    this._Validated = value;
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

        public string SentToVendor
        {
            get
            {
                CanReadProperty(true);
                return _SentToVendor;
            }
            set
            {
                CanWriteProperty(true);
                if (_SentToVendor != value)
                {
                    this._SentToVendor = value;
                    PropertyHasChanged();
                }
            }
        }

        #endregion

        #region Read-only fields accessors

        public int RowKey { get { CanReadProperty(true); return _RowKey; } }
        public string AssetType { get { CanReadProperty(true); return _AssetType; } }
        public string AssetName { get { CanReadProperty(true); return _AssetName; } }
        public string Product { get { CanReadProperty(true); return _Product; } }
        public string Comments { get { CanReadProperty(true); return _Comments; } }
        public long? JanVol { get { CanReadProperty(true); return _JanVol; } }
        public long? FebVol { get { CanReadProperty(true); return _FebVol; } }
        public long? MarVol { get { CanReadProperty(true); return _MarVol; } }
        public long? AprVol { get { CanReadProperty(true); return _AprVol; } }
        public long? MayVol { get { CanReadProperty(true); return _MayVol; } }
        public long? JunVol { get { CanReadProperty(true); return _JunVol; } }
        public long? JulVol { get { CanReadProperty(true); return _JulVol; } }
        public long? AugVol { get { CanReadProperty(true); return _AugVol; } }
        public long? SepVol { get { CanReadProperty(true); return _SepVol; } }
        public long? OctVol { get { CanReadProperty(true); return _OctVol; } }
        public long? NovVol { get { CanReadProperty(true); return _NovVol; } }
        public long? DecVol { get { CanReadProperty(true); return _DecVol; } }

        #endregion

        #region Data Access

        protected void FetchData(SafeDataReader dataReader)
        {
            _RowKey = dataReader.GetInt32("RowKey");
            _Validated = dataReader.GetString("Validated").Trim();
            _OverrideVolume = dataReader.IsDBNull("Override Volume") ? (int?)null: Convert.ToInt32(dataReader.GetDecimal("Override Volume"));
            _AssetType = dataReader.GetString("Asset Type").Trim();
            _AssetName = dataReader.GetString("Asset Name").Trim();
            _Product = dataReader.GetString("Product").Trim();
            _JanVol = dataReader.IsDBNull("Jan Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Jan Vol"));
            _FebVol = dataReader.IsDBNull("Feb Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Feb Vol"));
            _MarVol = dataReader.IsDBNull("Mar Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Mar Vol"));
            _AprVol = dataReader.IsDBNull("Apr Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Apr Vol"));
            _MayVol = dataReader.IsDBNull("May Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("May Vol"));
            _JunVol = dataReader.IsDBNull("Jun Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Jun Vol"));
            _JulVol = dataReader.IsDBNull("Jul Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Jul Vol"));
            _AugVol = dataReader.IsDBNull("Aug Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Aug Vol"));
            _SepVol = dataReader.IsDBNull("Sep Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Sep Vol"));
            _OctVol = dataReader.IsDBNull("Oct Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Oct Vol"));
            _NovVol = dataReader.IsDBNull("Nov Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Nov Vol"));
            _DecVol = dataReader.IsDBNull("Dec Vol") ? (long?)null : Convert.ToInt64(dataReader.GetDecimal("Dec Vol"));

            base.FetchData(dataReader);
        }

        protected override void DataPortal_Update()
        {
            this.Update(new Data.DataAccess(), "prcVPeGridViewAllDetailGridupdateRow", this.RowKey, this.Validated, this.OverrideVolume, this.ReviewedBy_Name, this.ChangedBy_Name, this.SentToVendor);
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

    }
}