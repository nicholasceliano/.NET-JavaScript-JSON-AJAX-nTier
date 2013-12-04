using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    [Serializable()]
    public class StandardValidationDetailRecord : BusinessObject<StandardValidationRecord>
    {

        #region Member fields

        protected string _AssetType = string.Empty;
        protected string _Product = string.Empty;
        protected string _MeterID = string.Empty;
        protected string _Comments = string.Empty;
        protected decimal? _JanVol = null;
        protected decimal? _FebVol = null;
        protected decimal? _MarVol = null;
        protected decimal? _AprVol = null;
        protected decimal? _MayVol = null;
        protected decimal? _JunVol = null;
        protected decimal? _JulVol = null;
        protected decimal? _AugVol = null;
        protected decimal? _SepVol = null;
        protected decimal? _OctVol = null;
        protected decimal? _NovVol = null;
        protected decimal? _DecVol = null;

        #endregion

        #region Constructors

        public StandardValidationDetailRecord()
        {
        }

        public StandardValidationDetailRecord(SafeDataReader dataReader)
        {
            FetchData(dataReader);
        }

        #endregion

        #region Interface functions

        protected override object GetIdValue()
        {
            return _AssetType + _Product;
        }

        #endregion

        #region Read-only fields accessors

        public string AssetType { get { CanReadProperty(true); return _AssetType; } }
        public string Product { get { CanReadProperty(true); return _Product; } }
        public string MeterID { get { CanReadProperty(true); return _MeterID; } }
        public string Comments { get { CanReadProperty(true); return _Comments; } }
        public decimal? JanVol { get { CanReadProperty(true); return _JanVol; } }
        public decimal? FebVol { get { CanReadProperty(true); return _FebVol; } }
        public decimal? MarVol { get { CanReadProperty(true); return _MarVol; } }
        public decimal? AprVol { get { CanReadProperty(true); return _AprVol; } }
        public decimal? MayVol { get { CanReadProperty(true); return _MayVol; } }
        public decimal? JunVol { get { CanReadProperty(true); return _JunVol; } }
        public decimal? JulVol { get { CanReadProperty(true); return _JulVol; } }
        public decimal? AugVol { get { CanReadProperty(true); return _AugVol; } }
        public decimal? SepVol { get { CanReadProperty(true); return _SepVol; } }
        public decimal? OctVol { get { CanReadProperty(true); return _OctVol; } }
        public decimal? NovVol { get { CanReadProperty(true); return _NovVol; } }
        public decimal? DecVol { get { CanReadProperty(true); return _DecVol; } }

        #endregion

        #region Data Access

        protected void FetchData(SafeDataReader dataReader)
        {
            _AssetType = dataReader.GetString("Asset Type").Trim();
            _Product = dataReader.GetString("Product").Trim();
            _MeterID = dataReader.GetString("Meter ID").Trim();
            _JanVol = dataReader.IsDBNull("Jan Vol") ? (decimal?)null : dataReader.GetDecimal("Jan Vol");
            _FebVol = dataReader.IsDBNull("Feb Vol") ? (decimal?)null : dataReader.GetDecimal("Feb Vol");
            _MarVol = dataReader.IsDBNull("Mar Vol") ? (decimal?)null : dataReader.GetDecimal("Mar Vol");
            _AprVol = dataReader.IsDBNull("Apr Vol") ? (decimal?)null : dataReader.GetDecimal("Apr Vol");
            _MayVol = dataReader.IsDBNull("May Vol") ? (decimal?)null : dataReader.GetDecimal("May Vol");
            _JunVol = dataReader.IsDBNull("Jun Vol") ? (decimal?)null : dataReader.GetDecimal("Jun Vol");
            _JulVol = dataReader.IsDBNull("Jul Vol") ? (decimal?)null : dataReader.GetDecimal("Jul Vol");
            _AugVol = dataReader.IsDBNull("Aug Vol") ? (decimal?)null : dataReader.GetDecimal("Aug Vol");
            _SepVol = dataReader.IsDBNull("Sep Vol") ? (decimal?)null : dataReader.GetDecimal("Sep Vol");
            _OctVol = dataReader.IsDBNull("Oct Vol") ? (decimal?)null : dataReader.GetDecimal("Oct Vol");
            _NovVol = dataReader.IsDBNull("Nov Vol") ? (decimal?)null : dataReader.GetDecimal("Nov Vol");
            _DecVol = dataReader.IsDBNull("Dec Vol") ? (decimal?)null : dataReader.GetDecimal("Dec Vol");
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