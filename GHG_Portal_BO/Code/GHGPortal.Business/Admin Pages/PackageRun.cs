using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Web;
using Csla.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    [Serializable()]
    public class PackageRun : BusinessObject<PackageRun>
    {
        #region Business Methods

        private int _DMPK_PackageExecution = 0;
        private string _Package_Name = string.Empty;
        private DateTime _Run_Date = DateTime.MinValue;
        private int _Rows = 0;

        #endregion

        #region Interface Function

        protected override object GetIdValue()
        {
            return this._DMPK_PackageExecution;
        }

        #endregion

        #region Constructors

        public PackageRun()
        {
        }

        public PackageRun(SafeDataReader dataReader)
        {
            FetchData(dataReader);
        }

        #endregion

        #region Update fields accessors

        #endregion

        #region ReadOnly field accessors

        public int DMPK_PackageExecution
        {
            get
            {
                CanReadProperty(true);
                return _DMPK_PackageExecution;
            }
        }

        public string Package_Name
        {
            get
            {
                CanReadProperty(true);
                return _Package_Name;
            }
        }

        public DateTime Run_Date
        {
            get
            {
                CanReadProperty(true);
                return _Run_Date;
            }
        }

        public int Rows
        {
            get
            {
                CanReadProperty(true);
                return _Rows;
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
            _DMPK_PackageExecution = dataReader.GetInt32("DMPK PackageExecution");
            _Package_Name = dataReader.GetString("Package Name").Trim();
            _Run_Date = dataReader.GetDateTime("RunDate");
            _Rows = dataReader.GetInt32("Rows");

            base.FetchData(dataReader);
        }

        #region Unused Overrides

        protected override void DataPortal_Update()
        {
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

        #endregion

        #endregion
    }
}