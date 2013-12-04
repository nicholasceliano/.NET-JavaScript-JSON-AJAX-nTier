using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    [Serializable()]
    public class VesselValidationRecord : BusinessObject<VesselValidationRecord>
    {
        #region Business Methods

        protected int _RowId = 0;
        protected string _Validated = string.Empty;
        protected string _MissingRow = string.Empty;
        protected int? _MoveNum = null;
        protected int? _RefMove = null;
        protected string _IR = string.Empty;
        protected int? _SegmentNum = 0;
        protected string _BulkOrderNo = string.Empty;
        protected string _Status = string.Empty;
        protected string _Disport = string.Empty;
        protected DateTime _DisportDate = DateTime.MinValue;
        protected string _VesselName = string.Empty;
        protected string _HPName = string.Empty;
        protected string _EPAName = string.Empty;
        protected int? _DischargeVol = 0;
        protected string _UOM = string.Empty;
        protected string _UOMShort = string.Empty;
        protected string _Source_UOM = string.Empty;
        protected int? _Source_Volume = 0;
        protected string _DataSource = string.Empty;
        protected string _FacilityName = string.Empty;
        protected string _Comments = string.Empty;
        protected string _ReviewedBy_Name = string.Empty;
        protected DateTime _ReviewedOn_Date = DateTime.MinValue;
        protected string _ChangedBy_Name = string.Empty;
        protected DateTime _ChangeOn_Date = DateTime.MinValue;
        
        #endregion

        #region Constructors

        public VesselValidationRecord()
        {
        }

        public VesselValidationRecord(SafeDataReader dataReader)
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

        public string Comments
        {
            get
            {
                CanReadProperty(true);
                return _Comments;
            }
            set
            {
                CanWriteProperty(true);
                if (_Comments != value)
                {
                    this._Comments = value;
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

        public string MissingRow
        {
            get
            {
                CanReadProperty(true);
                return _MissingRow;
            }
        }
        
        public int? MoveNum
        {
            get
            {
                CanReadProperty(true);
                return _MoveNum;
            }
        }

        public int? RefMove
        {
            get
            {
                CanReadProperty(true);
                return _RefMove;
            }
        }

        public string IR
        {
            get
            {
                CanReadProperty(true);
                return _IR;
            }
        }

        public int? SegmentNum
        {
            get
            {
                CanReadProperty(true);
                return _SegmentNum;
            }
        }

        public string BulkOrderNo
        {
            get
            {
                CanReadProperty(true);
                return _BulkOrderNo;
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

        public string Disport
        {
            get
            {
                CanReadProperty(true);
                return _Disport;
            }
        }

        public DateTime DisportDate
        {
            get
            {
                CanReadProperty(true);
                return _DisportDate;
            }
        }

        public string VesselName
        {
            get
            {
                CanReadProperty(true);
                return _VesselName;
            }
        }

        public string HPName
        {
            get
            {
                CanReadProperty(true);
                return _HPName;
            }
        }

        public string EPAName
        {
            get
            {
                CanReadProperty(true);
                return _EPAName;
            }
        }

        public int? DischargeVol
        {
            get
            {
                CanReadProperty(true);
                return _DischargeVol;
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

        public string UOMShort
        {
            get
            {
                CanReadProperty(true);
                return _UOMShort;
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

        public int? Source_Volume
        {
            get
            {
                CanReadProperty(true);
                return _Source_Volume;
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

        public string FacilityName
        {
            get
            {
                CanReadProperty(true);
                return _FacilityName;
            }
        }

        public DateTime ReviewedOn_Date
        {
            get
            {
                CanReadProperty(true);
                return _ReviewedOn_Date;
            }
        }

        public DateTime ChangeOn_Date
        {
            get
            {
                CanReadProperty(true);
                return _ChangeOn_Date;
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
            _RowId = dataReader.GetInt32("RowKey");
            _Validated = dataReader.GetString("Validated").Trim();
            _MissingRow = dataReader.GetString("MissingRow").Trim();
            _MoveNum = dataReader.GetInt32("Move Number");
            _RefMove = dataReader.GetInt32("Reference Number");
            _IR = dataReader.GetString("IR");
            _SegmentNum = dataReader.GetInt32("Segment Number");
            _BulkOrderNo = dataReader.GetString("Bulk Order Number");
            _Status = dataReader.GetString("Status");
            _Disport = dataReader.GetString("Disport").Trim();
            _DisportDate = dataReader.GetDateTime("Disport Date");
            _VesselName = dataReader.GetString("Vessel Name").Trim();
            _HPName = dataReader.GetString("Hess Product Name").Trim();
            _EPAName = dataReader.GetString("EPA Product Name").Trim();
            _DischargeVol = dataReader.GetInt32("Total Discharged");
            _UOM = dataReader.GetString("UOM").Trim();
            _UOMShort = dataReader.GetString("UOMShort").Trim();
            _Source_UOM = dataReader.GetString("Source UOM").Trim();
            _Source_Volume = dataReader.GetInt32("Source Volume");
            _DataSource = dataReader.GetString("Data Source").Trim();
            _FacilityName = dataReader.GetString("Facility Name").Trim();
            _Comments = dataReader.GetString("Comments").Trim();
            _ReviewedBy_Name = dataReader.GetString("Reviewed Name").Trim();
            _ReviewedOn_Date = dataReader.GetDateTime("Reviewed Date");
            _ChangedBy_Name = dataReader.GetString("Changed Name").Trim();
            _ChangeOn_Date = dataReader.GetDateTime("Changed Date");

            base.FetchData(dataReader);
        }

        protected override void DataPortal_Update()
        {
            if (base.IsSavable)
            {
                this.Update(new Data.DataAccess(), "prcVPeGridVesselMasterGridupdateRow", this.ChangedBy_Name, this.ReviewedBy_Name, this.Validated, this.Comments, this.RowId);
            }
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