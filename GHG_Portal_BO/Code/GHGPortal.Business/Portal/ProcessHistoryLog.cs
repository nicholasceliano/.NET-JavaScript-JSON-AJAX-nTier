using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using System.Linq;
using System.Web;

namespace Hess.Corporate.GHGPortal.Business
{
    public class ProcessHistoryLog : BusinessObject<ProcessHistoryLog>
    {
        #region Public Constants

        //Field Names
        public const string DMPK_PACKAGEEXECUTION = "DMPK_PackageExecution";
        public const string PACKAGE_NAME = "Package_Name";
        public const string START_YEAR = "Start_Year";
        public const string START_MONTH = "Start_Month";
        public const string SUCCESSFULCOMPLETION_IND = "SuccessfulCompletion_Ind";
        public const string PACKAGESTART_DT = "PackageStart_Dt";
 
        #endregion

        #region Constructors

        public ProcessHistoryLog()
        {
        }

        public ProcessHistoryLog(Csla.Data.SafeDataReader dataReader)
        {
            _DMPK_PACKAGEEXECUTION = dataReader.GetInt32(DMPK_PACKAGEEXECUTION).ToString().Trim();
            _PACKAGE_NAME = dataReader.GetString(PACKAGE_NAME).ToString().Trim();
            _START_YEAR = dataReader.GetString(START_YEAR).ToString().Trim();
            _START_MONTH = dataReader.GetString(START_MONTH).ToString().Trim();
            _SUCCESSFULCOMPLETION_IND = dataReader.GetString(SUCCESSFULCOMPLETION_IND).ToString().Trim();
            _PACKAGESTART_DT = dataReader.GetDateTime(PACKAGESTART_DT).ToString().Trim();
        }

        #endregion

        #region Business Methods

        private string _DMPK_PACKAGEEXECUTION = string.Empty;
            private string _PACKAGE_NAME = string.Empty;
            private string _START_YEAR = string.Empty;
            private string _START_MONTH = string.Empty;
            private string _SUCCESSFULCOMPLETION_IND = string.Empty;
            private string _PACKAGESTART_DT = string.Empty;

            public string DMPK_PackageExecution
            {
                get
                {
                    CanReadProperty(true);
                    return _DMPK_PACKAGEEXECUTION;
                }
            }


            [CustomUI(FieldDisplayName = "Data Source", FieldOrder = 1, FieldLength = 100)]
            public string Package_Name
            {
                get
                {
                    CanReadProperty(true);
                    return _PACKAGE_NAME;
                }
            }

            public string Start_Year
            {
                get
                {
                    CanReadProperty(true);
                    return _START_YEAR;
                }
            }

            public string Start_Month
            {
                get
                {
                    CanReadProperty(true);
                    return _START_MONTH;
                }
            }

            [CustomUI(FieldDisplayName = "Validation Period", FieldOrder = 2, FieldLength = 110)]
            public string Period
            {
                get
                {
                    return string.Format("{0} {1}", _START_MONTH, _START_YEAR);
                }
            }

            [CustomUI(FieldDisplayName = "Status", FieldOrder = 4, FieldLength = 100)]
            public string SucessfulCompletion_Ind
            {
                get
                {
                    CanReadProperty(true);
                    return _SUCCESSFULCOMPLETION_IND;
                }
            }

            [CustomUI(FieldDisplayName = "Executed On", FieldOrder = 3, FieldLength = 150)]
            public string PackageStart_Dt
            {
                get
                {
                    CanReadProperty(true);
                    return _PACKAGESTART_DT;
                }
            }

            protected override object GetIdValue()
            {
                return this._DMPK_PACKAGEEXECUTION;
            }

        #endregion
    }
}