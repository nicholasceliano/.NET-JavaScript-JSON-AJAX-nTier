using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    [Serializable()]
    public class Asset_Record : BusinessObject<Asset_Record>
    {
        #region Business Methods

        private int _DMPK_EmittingAsset = 0;
        private string _EmittingAsset_Name = string.Empty;
        private string _EmittingAssetType_Name = string.Empty;
        private string _FacilityName_NAME = string.Empty;
        private string _EntityName = string.Empty;
        private string _SourceCategory_Name = string.Empty;
        private string _MeasurementType_Name = string.Empty;
        private string _MeasurementMethod_Name = string.Empty;
        private string _AssetDateSource_Name = string.Empty;
        private string _AssetDateSource_Type = string.Empty;
        private string _Asset_ID = string.Empty;
        private string _Tag_ID = string.Empty;
        private string _Product_Name = string.Empty;
        private string _VolumeUnit_Code = string.Empty;
        private string _AddedBy_Name = string.Empty;
        private DateTime _Added_DT = DateTime.MinValue;
        private DateTime _Decomissioned_DT = DateTime.MinValue;
        private string _Decomissioned_By = string.Empty;
        private string _Review_Ind = string.Empty;
        private string _IncludeInMissing = string.Empty;
        private int _ReturnedDMPK;

        #endregion

        #region Interface functions

        protected override object GetIdValue()
        {
            return _DMPK_EmittingAsset;
        }

        #endregion

        #region Constructors

        public Asset_Record()
        {
        }

        public Asset_Record(SafeDataReader dataReader)
        {
            FetchData(dataReader);
        }

        #endregion

        #region Update fields accessors

        public string EmittingAsset_Name
        {
            get
            {
                CanReadProperty(true);
                return _EmittingAsset_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_EmittingAsset_Name != value)
                {
                    this._EmittingAsset_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string EmittingAssetType_Name
        {
            get
            {
                CanReadProperty(true);
                return _EmittingAssetType_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_EmittingAssetType_Name != value)
                {
                    this._EmittingAssetType_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string AssetDataSource_Name
        {
            get
            {
                CanReadProperty(true);
                return _AssetDateSource_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_AssetDateSource_Name != value)
                {
                    this._AssetDateSource_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string AssetDataSource_Type
        {
            get
            {
                CanReadProperty(true);
                return _AssetDateSource_Type;
            }
            set
            {
                CanWriteProperty(true);
                if (_AssetDateSource_Type != value)
                {
                    this._AssetDateSource_Type = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Facility_Name
        {
            get
            {
                CanReadProperty(true);
                return _FacilityName_NAME;
            }
            set
            {
                CanWriteProperty(true);
                if (_FacilityName_NAME != value)
                {
                    this._FacilityName_NAME = value;
                    PropertyHasChanged();
                }
            }
        }

        public string MeasurementType_Name
        {
            get
            {
                CanReadProperty(true);
                return _MeasurementType_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_MeasurementType_Name != value)
                {
                    this._MeasurementType_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string MeasurementMethod_Name
        {
            get
            {
                CanReadProperty(true);
                return _MeasurementMethod_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_MeasurementMethod_Name != value)
                {
                    this._MeasurementMethod_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string SourceCategory_Name
        {
            get
            {
                CanReadProperty(true);
                return _SourceCategory_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_SourceCategory_Name != value)
                {
                    this._SourceCategory_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Decommissioned_BY
        {
            get
            {
                CanReadProperty(true);
                return _Decomissioned_By;
            }
            set
            {
                CanWriteProperty(true);
                if (_Decomissioned_By != value)
                {
                    this._Decomissioned_By = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Product_Name
        {
            get
            {
                CanReadProperty(true);
                return _Product_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_Product_Name != value)
                {
                    this._Product_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string VolumeUnit_Code
        {
            get
            {
                CanReadProperty(true);
                return _VolumeUnit_Code;
            }
            set
            {
                CanWriteProperty(true);
                if (_VolumeUnit_Code != value)
                {
                    this._VolumeUnit_Code = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Review_Ind
        {
            get
            {
                CanReadProperty(true);
                return _Review_Ind;
            }
            set
            {
                CanWriteProperty(true);
                if (_Review_Ind != value)
                {
                    this._Review_Ind = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Tag_ID
        {
            get
            {
                CanReadProperty(true);
                return _Tag_ID;
            }
            set
            {
                CanWriteProperty(true);
                if (_Tag_ID != value)
                {
                    this._Tag_ID = value;
                    PropertyHasChanged();
                }
            }
        }

        public string IncludeInMissing
        {
            get
            {
                CanReadProperty(true);
                return _IncludeInMissing;
            }
            set
            {
                CanWriteProperty(true);
                if (_IncludeInMissing != value)
                {
                    this._IncludeInMissing = value;
                    PropertyHasChanged();
                }
            }
        }

        #endregion

        #region Read-only fields accessors

        public int DMPK_EmittingAsset
        {
            get
            {
                CanReadProperty(true);
                return _DMPK_EmittingAsset;
            }
        }

        public string Entity_Name
        {
            get
            {
                CanReadProperty(true);
                return _EntityName;
            }
        }

        public string Asset_ID
        {
            get
            {
                CanReadProperty(true);
                return _Asset_ID;
            }
        }

        public string AddedBy_Name
        {
            get
            {
                CanReadProperty(true);
                return _AddedBy_Name;
            }
        }

        public DateTime Added_DT
        {
            get
            {
                CanReadProperty(true);
                return _Added_DT;
            }
        }

        public DateTime Decommissioned_DT
        {
            get
            {
                CanReadProperty(true);
                return _Decomissioned_DT;
            }
        }

        public int ReturnedDMPK
        {
            get
            {
                CanReadProperty(true);
                return _ReturnedDMPK;
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
            _DMPK_EmittingAsset = dataReader.GetInt32("DMPK_EmittingAsset");
            _EmittingAsset_Name = dataReader.GetString("EmittingAsset_Name").Trim();
            _EmittingAssetType_Name = dataReader.GetString("EmittingAssetType_Name").Trim();
            _FacilityName_NAME = dataReader.GetString("Facility_Name").Trim();
            _EntityName = dataReader.GetString("Entity_Name").Trim();
            _SourceCategory_Name = dataReader.GetString("SourceCategory_Name").Trim();
            _MeasurementType_Name = dataReader.GetString("MeasurementType_Name").Trim();
            _MeasurementMethod_Name = dataReader.GetString("MeasurementMethod_Name").Trim();
            _AssetDateSource_Name = dataReader.GetString("AssetDataSource_Name").Trim();
            _AssetDateSource_Type = dataReader.GetString("AssetDataSource_Type").Trim();
            _Asset_ID = dataReader.GetString("Asset_Id").Trim();
            _Tag_ID = dataReader.GetString("Tag_Id").Trim();
            _Product_Name = dataReader.GetString("Product_Name").Trim();
            _VolumeUnit_Code = dataReader.GetString("VolumeUnit_Code").Trim();
            _AddedBy_Name = dataReader.GetString("AddedBy_Name").Trim();
            _Added_DT = dataReader.GetDateTime("Added_Dt");
            _Decomissioned_DT = dataReader.GetDateTime("Decommissioned_Dt");
            _Decomissioned_By = dataReader.GetString("Decommissioned_By").Trim();
            _Review_Ind = dataReader.GetString("Review_Ind").Trim();
            _IncludeInMissing = dataReader.GetString("IncludeInMissing_Ind").Trim();
            
            base.FetchData(dataReader);
        }

        protected override void DataPortal_Update()
        {
            this.Update(new Data.DataAccess(), "prcVPadmAssetManagementUpdate", this.EmittingAsset_Name, this.VolumeUnit_Code, this.EmittingAssetType_Name, this.Facility_Name, this.SourceCategory_Name, this.MeasurementType_Name, this.MeasurementMethod_Name, this.AssetDataSource_Name, this.Product_Name, this.Decommissioned_BY, this.Review_Ind, this.DMPK_EmittingAsset, this.IncludeInMissing, this.Tag_ID);
        }
        
        protected override void DataPortal_Insert()
        {
            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(
                new Data.DataAccess().ExecuteStoredProcedure("prcVPadmAssetManagementInsert", this.EmittingAsset_Name, this.VolumeUnit_Code, this.EmittingAssetType_Name, this.Facility_Name, this.SourceCategory_Name, this._MeasurementType_Name, this.MeasurementMethod_Name, this.AssetDataSource_Name, this.AssetDataSource_Type, this.Product_Name, this.IncludeInMissing, this.Tag_ID, this.Review_Ind)))
            {
                while (dataReader.Read())
                {
                    _ReturnedDMPK = dataReader.GetInt32("DMPK_EmittingAsset");
                }
            }
        }

        #region Unused Overrides

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