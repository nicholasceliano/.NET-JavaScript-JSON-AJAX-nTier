using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Linq;
using System.Web;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    [Serializable()]
    public class FacilityMgmt_Record : BusinessObject<FacilityMgmt_Record>
    {
        #region Business Methods

            private int _DMPK_FacilityDataOwner = 0;
            private string _DataOwner_Name = string.Empty;
            private string _Facility_Name = string.Empty;
            private string _SSO_Text = string.Empty;
            private string _AddedBy_Name = string.Empty;
            private DateTime _Added_Dt = DateTime.MinValue;
            private DateTime _LastUpdate_Dt = DateTime.MinValue;
            private string _Active_Ind = string.Empty;
            private string _LastUpdate_By = string.Empty;
            private string _PrimaryOwner = string.Empty;
            private DateTime? _ExpirationDate = null;

        #endregion

        #region Interface functions

            protected override object GetIdValue()
            {
                return _DMPK_FacilityDataOwner;
            }

        #endregion
        
        #region Constructors

            public FacilityMgmt_Record()
            {
            }

            public FacilityMgmt_Record(Csla.Data.SafeDataReader dataReader)
            {
                FetchData(dataReader);
            }

        #endregion

        #region Update fields accessors

            public string Active_Ind
            {
                get
                {
                    CanReadProperty(true);
                    return _Active_Ind;
                }
                set
                {
                    CanWriteProperty(true);
                    if (_Active_Ind != value)
                    {
                        this._Active_Ind = value;
                        PropertyHasChanged();
                    }
                }
            }

            public string LastUpdate_By
            {
                get
                {
                    CanReadProperty(true);
                    return _LastUpdate_By;
                }
                set
                {
                    CanWriteProperty(true);
                    if (_LastUpdate_By != value)
                    {
                        this._LastUpdate_By = value;
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

            public string SSO_Text
            {
                get
                {
                    CanReadProperty(true);
                    return _SSO_Text;
                }
                set
                {
                    CanWriteProperty(true);
                    if (_SSO_Text != value)
                    {
                        this._SSO_Text = value;
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

            public string PrimaryOwner
            {
                get
                {
                    CanReadProperty(true);
                    return _PrimaryOwner;
                }
                set
                {
                    CanWriteProperty(true);
                    if (_PrimaryOwner != value)
                    {
                        this._PrimaryOwner = value;
                        PropertyHasChanged();
                    }
                }
            }

            public DateTime? ExpirationDate
            {
                get
                {
                    CanReadProperty(true);
                    return _ExpirationDate;
                }
                set
                {
                    CanWriteProperty(true);
                    if (_ExpirationDate != value)
                    {
                        this._ExpirationDate = value;
                        PropertyHasChanged();
                    }
                }
            }

        #endregion

        #region Read-only fields accessors

            public int DMPK_FacilityDataOwner
            {
                get
                {
                    CanReadProperty(true);
                    return _DMPK_FacilityDataOwner;
                }
            }

            public DateTime Added_Dt
            {
                get
                {
                    CanReadProperty(true);
                    return _Added_Dt;
                }
            }

            public DateTime LastUpdate_Dt
            {
                get
                {
                    CanReadProperty(true);
                    return _LastUpdate_Dt;
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
                _DMPK_FacilityDataOwner = dataReader.GetInt32("DMPK FacilityDataOwner");
                _DataOwner_Name = dataReader.GetString("Data Owner Name");
                _Facility_Name = dataReader.GetString("Facility Name");
                _SSO_Text = dataReader.GetString("SSO Text");
                _AddedBy_Name = dataReader.GetString("Added By Name");
                _Added_Dt = dataReader.GetDateTime("Added Date");
                _Active_Ind = dataReader.GetString("Active Ind");
                _LastUpdate_Dt = dataReader.GetDateTime("Last Updated Dt");
                _LastUpdate_By = dataReader.GetString("Last Updated By");
                _PrimaryOwner = dataReader.GetString("PrimaryOwner");

                base.FetchData(dataReader);
            }

            protected override void DataPortal_Update()
            {
                this.Update(new Data.DataAccess(), "prcVPadmFacilityManagementGridUpdate", this.DMPK_FacilityDataOwner, this.Active_Ind, this.ExpirationDate, this.LastUpdate_By, this.PrimaryOwner);
            }

            protected override void DataPortal_Insert()
            {
                this.Update(new Data.DataAccess(), "prcVPadmFacilityManagementGridInsert", this.DataOwner_Name, this.Facility_Name, this.SSO_Text, this.AddedBy_Name, this.PrimaryOwner);
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