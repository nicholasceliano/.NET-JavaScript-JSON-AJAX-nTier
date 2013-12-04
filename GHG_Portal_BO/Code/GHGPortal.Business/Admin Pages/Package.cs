using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Web;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    [Serializable()]
    public class Package : BusinessObject<Package>
    {
        #region Business Methods

        private int _DMPK_ExecutionRun = 0;
        private string _Package_Name = string.Empty;
        private DateTime _Start_Date = DateTime.MinValue;
        private DateTime _End_Date = DateTime.MinValue;
        private string _ProcessPack_Ind = string.Empty;
        private string _ClearSupp_Ind = string.Empty;
        private string _DateProcess_Name = string.Empty;
        private int _DMPK_PackageExecution = 0;

        #endregion

        #region Interface Function

        protected override object GetIdValue()
        {
            return this._DMPK_ExecutionRun;
        }

        #endregion

        #region Constructors

        public Package()
        {
        }

        public Package(SafeDataReader dataReader)
        {
            FetchData(dataReader);
        }

        #endregion

        #region Update fields accessors

        public DateTime Start_Date
        {
            get
            {
                CanReadProperty(true);
                return _Start_Date;
            }
            set
            {
                CanWriteProperty(true);
                if (_Start_Date != value)
                {
                    this._Start_Date = value;
                    PropertyHasChanged();
                }
            }
        }

        public DateTime End_Date
        {
            get
            {
                CanReadProperty(true);
                return _End_Date;
            }
            set
            {
                CanWriteProperty(true);
                if (_End_Date != value)
                {
                    this._End_Date = value;
                    PropertyHasChanged();
                }
            }
        }

        public string ProcessPack_Ind
        {
            get
            {
                CanReadProperty(true);
                return _ProcessPack_Ind;
            }
            set
            {
                CanWriteProperty(true);
                if (_ProcessPack_Ind != value)
                {
                    this._ProcessPack_Ind = value;
                    PropertyHasChanged();
                }
            }
        }

        public string ClearSupp_Ind
        {
            get
            {
                CanReadProperty(true);
                return _ClearSupp_Ind;
            }
            set
            {
                CanWriteProperty(true);
                if (_ClearSupp_Ind != value)
                {
                    this._ClearSupp_Ind = value;
                    PropertyHasChanged();
                }
            }
        }

        public int DMPK_PackageExecution
        {
            get
            {
                CanReadProperty(true);
                return _DMPK_PackageExecution;
            }
            set
            {
                CanWriteProperty(true);
                if (_DMPK_PackageExecution != value)
                {
                    this._DMPK_PackageExecution = value;
                    PropertyHasChanged();
                }
            }
        }

        #endregion

        #region ReadOnly field accessors

        public string Package_Name
        {
            get
            {
                CanReadProperty(true);
                return _Package_Name;
            }
        }

        public string DateProcess_Name
        {
            get
            {
                CanReadProperty(true);
                return _DateProcess_Name;
            }
        }

        public int DMPK_ExecutionRun
        {
            get
            {
                CanReadProperty(true);
                return _DMPK_ExecutionRun;
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
            _Package_Name = dataReader.GetString("Package Name").Trim();
            _ProcessPack_Ind = dataReader.GetString("ProcessPack Ind").Trim();
            _ClearSupp_Ind = dataReader.GetString("ClearSupp Ind").Trim();
            _DateProcess_Name = dataReader.GetString("DateProcess Name").Trim();
            _DMPK_ExecutionRun = dataReader.GetInt32("DMPK ExecutionRun");
            _Start_Date = dataReader.GetDateTime("ParameterStart_Dt");
            _End_Date = dataReader.GetDateTime("ParameterEnd_Dt");
            _DMPK_PackageExecution = dataReader.GetInt32("DMPK PackageExecution");

            base.FetchData(dataReader);
        }

        protected override void DataPortal_Update()
        {
            this.Update(new Data.DataAccess(), "prcVPadmExecuteSSISUpdate", this.Start_Date, this.End_Date, this.ClearSupp_Ind, this.ProcessPack_Ind, this.DMPK_PackageExecution, this.DMPK_ExecutionRun);
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