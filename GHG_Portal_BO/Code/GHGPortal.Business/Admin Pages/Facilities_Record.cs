using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Linq;
using System.Web;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    [Serializable()]
    public class Facilities_Record : BusinessObject<Facilities_Record>
    {
        #region Business Methods

        private int _DMPK_Facility = 0;
        private string _Facility_Name = string.Empty;
        private string _BusinessUnit_Name = string.Empty;
        private string _Entity_Name = string.Empty;
        private string _Profit_Center = string.Empty;
        private string _AddedBy_Name = string.Empty;
        private string _PortalPage_Name = string.Empty;
        private DateTime _Added_DT = DateTime.MinValue;
        private DateTime _Decomissioned_DT = DateTime.MinValue;
        private string _Deomissioned_By = string.Empty;
        private string _Review_Ind = string.Empty;
        private int _ReturnedDMPK;
        private string _Original_Facility = string.Empty;
        private string _DataOwner_Name = string.Empty;
        private string _Visible = string.Empty;
        private string _CostCenter = string.Empty;
        private string _RollupCostCenter = string.Empty;

        #endregion

        #region Interface functions

        protected override object GetIdValue()
        {
            return _DMPK_Facility;
        }

        #endregion

        #region Constructors

        public Facilities_Record()
        {
        }

        public Facilities_Record(Csla.Data.SafeDataReader dataReader)
        {
            FetchData(dataReader);
        }

        #endregion

        #region Update fields accessors

        public string Facility_Name
        {
            get
            {
                CanReadProperty(true);
                return _Facility_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_Facility_Name != value)
                {
                    this._Facility_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string BusinessUnit_Name
        {
            get
            {
                CanReadProperty(true);
                return _BusinessUnit_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_BusinessUnit_Name != value)
                {
                    this._BusinessUnit_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Entity_Name
        {
            get
            {
                CanReadProperty(true);
                return _Entity_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_Entity_Name != value)
                {
                    this._Entity_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string AddedBy_Name
        {
            get
            {
                CanReadProperty(true);
                return _AddedBy_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_AddedBy_Name != value)
                {
                    this._AddedBy_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string PortalPage_Name
        {
            get
            {
                CanReadProperty(true);
                return _PortalPage_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_PortalPage_Name != value)
                {
                    this._PortalPage_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Decommissioned_By
        {
            get
            {
                CanReadProperty(true);
                return _Deomissioned_By;
            }
            set
            {
                CanWriteProperty(true);
                if (_Deomissioned_By != value)
                {
                    this._Deomissioned_By = value;
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

        public string Original_Facility
        {
            get
            {
                CanReadProperty(true);
                return _Original_Facility;
            }
            set
            {
                CanWriteProperty(true);
                if (_Original_Facility != value)
                {
                    this._Original_Facility = value;
                    PropertyHasChanged();
                }
            }
        }

        public string DataOwner_Name
        {
            get
            {
                CanReadProperty(true);
                return _DataOwner_Name;
            }
            set
            {
                CanWriteProperty(true);
                if (_DataOwner_Name != value)
                {
                    this._DataOwner_Name = value;
                    PropertyHasChanged();
                }
            }
        }

        public string Visible
        {
            get
            {
                CanReadProperty(true);
                return _Visible;
            }
            set
            {
                CanWriteProperty(true);
                if (_Visible != value)
                {
                    this._Visible = value;
                    PropertyHasChanged();
                }
            }
        }

        public string CostCenter
        {
            get
            {
                CanReadProperty(true);
                return _CostCenter;
            }
            set
            {
                CanWriteProperty(true);
                if (_CostCenter != value)
                {
                    this._CostCenter = value;
                    PropertyHasChanged();
                }
            }
        }

        public string RollupCostCenter
        {
            get
            {
                CanReadProperty(true);
                return _RollupCostCenter;
            }
            set
            {
                CanWriteProperty(true);
                if (_RollupCostCenter != value)
                {
                    this._RollupCostCenter = value;
                    PropertyHasChanged();
                }
            }
        }

        #endregion

        #region Read-only fields accessors

        public int DMPK_Facility
        {
            get
            {
                CanReadProperty(true);
                return _DMPK_Facility;
            }
        }

        public string Profit_Center
        {
            get
            {
                CanReadProperty(true);
                return _Profit_Center;
            }
        }

        public DateTime Added_Dt
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
            _DMPK_Facility = dataReader.GetInt32("DMPK Facility");
            _Facility_Name = dataReader.GetString("Facility Name").ToString().Trim();
            _BusinessUnit_Name= dataReader.GetString("BU Name").Trim();
            _Entity_Name = dataReader.GetString("Entity Name").Trim();
            _Profit_Center = dataReader.GetString("Profit Center").Trim();
            _AddedBy_Name = dataReader.GetString("Added By Name").Trim();
            _PortalPage_Name = dataReader.GetString("Portal Page Name").Trim();
            _Added_DT = dataReader.GetDateTime("Added Date");
            _Decomissioned_DT = dataReader.GetDateTime("Decom Date");
            _Deomissioned_By = dataReader.GetString("Decom By").Trim();
            _Review_Ind = dataReader.GetString("Review Ind").Trim();
            _Visible = dataReader.GetString("Visible").Trim();
            _CostCenter = dataReader.GetString("Cost Center").Trim();
            _RollupCostCenter = dataReader.GetString("Rollup Cost Center").Trim();

            base.FetchData(dataReader);
        }

        protected override void DataPortal_Update()
        {
            this.Update(new Data.DataAccess(), "prcVPadmFacilityManagementUpdate", this.Facility_Name, this.Original_Facility, this.BusinessUnit_Name, this.Entity_Name, this.PortalPage_Name, this.AddedBy_Name, this.Decommissioned_By, this.Review_Ind, this.Visible, this.DMPK_Facility, this.CostCenter, this.RollupCostCenter);
        }

        protected override void DataPortal_Insert()
        {
            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(
                new Data.DataAccess().ExecuteStoredProcedure("prcVPadmFacilityManagementInsert", this.Facility_Name, this.BusinessUnit_Name, this.Entity_Name, this.PortalPage_Name, this.AddedBy_Name, this.Decommissioned_By, this.Review_Ind, this.Visible, this.DataOwner_Name, this.CostCenter, this.RollupCostCenter)))
            {
                while (dataReader.Read())
                {
                    _ReturnedDMPK = dataReader.GetInt32("DMPK_Facility");
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