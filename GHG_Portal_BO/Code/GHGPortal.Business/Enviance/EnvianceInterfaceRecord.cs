using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hess.Corporate.GHGPortal.Business
{
    public class EnvianceInterfaceRecord:BusinessObject<EnvianceInterfaceRecord>
    {
      /*  private const string ID = "DMPK_Enviance";
        private const string EVENT_DATE = "Event_Dt";
        private const string TAG_NAME = "Tag_Name";
        private const string VOLUME = "EmissionVolume_Amt";
        private const string CONNECTOR_NAME = "Connector_Name";
        private const string FACILITY_NAME = "Facility_Name";
        private const string EMITTING_ASSET = "EmittingAsset_Name";
        private const string PRODUCT_NAME = "Product_Name";
        private const string UOM_TEXT = "UnitOfMeasure_Txt";
        private const string STATUS_TEXT = "StatusCode_Txt";
        private const string TABLE_NAME = "interfaceEnviance";*/

        private const string UPDATE_PROCEDURE_NAME = "prcEnvianceUpdateStatus";
        #region Constructors

        public EnvianceInterfaceRecord()
        {

        }

        public EnvianceInterfaceRecord(Csla.Data.SafeDataReader reader)
        {
            this.FetchData(reader);
        }

        #endregion

        #region Properties

        private int _id;
        private DateTime _eventDate;
        private string _tagName;
        private double _emissionVolume;
        private string _connector;
        private string _facilityName;
        private string _emmittingAsset;
        private string _productName;
        private string _uom;
        private string _status;
        private int _errorCode;

        public int Id
        { 
            get { return _id; }
            set 
            {
                this._id = value;
                this.MarkDirty();
            }
        }
        public DateTime EventDate 
        {
            get { return _eventDate; }
            set
            {
                this._eventDate = value;
                this.MarkDirty();
            }
        }
        public string ValidationPeriod { get { return this.EventDate.Month + "/" + this.EventDate.Year; } }
        public string TagName 
        {
            get { return _tagName; }
            set
            {
                this._tagName = value;
                this.MarkDirty();
            }
        }
        public double EmissionVolume 
        {
            get { return _emissionVolume; }
            set
            {
                _emissionVolume = value;
                this.MarkDirty();
            }
        }
        public string FormattedVolume
        {
            get { return String.Format("{0:n0}", this.EmissionVolume); }
        }
        public string ConnectorName 
        {
            get { return this._connector; }
            set
            {
                _connector = value;
                this.MarkDirty();
            }
        }
        public string FacilityName 
        {
            get { return _facilityName; }
            set
            {
                _facilityName = value;
                this.MarkDirty();
            }
        }
        public string EmmittingAssetName 
        {
            get { return _emmittingAsset; }
            set
            {
                _emmittingAsset = value;
                this.MarkDirty();
            }
        }
        public string ProductName 
        {
            get { return _productName; }
            set
            {
                _productName = value;
                this.MarkDirty();
            }
        }
        public string UnitOfMeasure 
        {
            get { return _uom; }
            set
            {
                _uom = value;
                this.MarkDirty();
            }
        }
        public string Status 
        {
            get { return _status; }
            set
            {
                _status = value;
                this.MarkDirty();
            }
        }

        public int ErrorCode
        {
            get { return _errorCode; }
            set
            {
                _errorCode = value;
                this.MarkDirty();
            }
        }

        protected override object GetIdValue()
        {
            return this.Id;
        }

        #endregion

        #region Data Access

        protected override void FetchData(Csla.Data.SafeDataReader reader)
        {
            if (reader != null)
            {
                this.Id = reader.GetInt32("id");
                this.EventDate = reader.GetDateTime("event_dt");
                this.TagName = reader.GetString("tag");
                this.EmissionVolume = Convert.ToDouble(reader.GetDecimal("volume"));
                this.ConnectorName = reader.GetString("connector");
                this.FacilityName = reader.GetString("facility");
                this.EmmittingAssetName = reader.GetString("asset");
                this.ProductName = reader.GetString("product");
                this.UnitOfMeasure = reader.GetString("uom");
                this.Status = reader.GetString("status");
                this.ErrorCode = reader.GetInt32("statusCode");
            }
            base.FetchData(reader);
        }

        protected override void DataPortal_Update()
        {
            string fields = String.Empty;
            if (base.IsSavable)
            {
               // AddUpdateSQL(ID, this.Id, ref fields);
               /* AddUpdateSQL(EVENT_DATE, this.EventDate, ref fields);
                AddUpdateSQL(TAG_NAME, this.TagName, ref fields);
                AddUpdateSQL(CONNECTOR_NAME, this.ConnectorName, ref fields);
                AddUpdateSQL(FACILITY_NAME, this.FacilityName, ref fields);
                AddUpdateSQL(EMITTING_ASSET, this.EmmittingAssetName, ref fields);
                AddUpdateSQL(PRODUCT_NAME, this.ProductName, ref fields);
                AddUpdateSQL(UOM_TEXT, this.UnitOfMeasure, ref fields);
                AddUpdateSQL(STATUS_TEXT, this.Status, ref fields);

                string whereClause = string.Format("{0} = {1}", ID, Id);

                this.Update(new Data.DataAccess(), TABLE_NAME, fields, whereClause);*/

                this.Update(new Data.DataAccess(), UPDATE_PROCEDURE_NAME, Id, Status, this.ErrorCode);
                
            }
        }

       
        #endregion

        
    }
}
