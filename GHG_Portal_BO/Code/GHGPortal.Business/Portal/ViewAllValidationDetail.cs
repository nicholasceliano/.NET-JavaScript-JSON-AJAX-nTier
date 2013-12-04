using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla.Data;
using Hess.Corporate.GHGPortal.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    public class ViewAllValidationDetail : BusinessObjects<ViewAllValidationDetail, ViewAllValidationDetailRecord>
    {
        #region Factory methods

        public static ViewAllValidationDetail GetViewAllDetail(int yearNum, int monthNum, string astName = "", string valCheck = "", string entName = "", string facName ="", string prodName = "", string uomName ="", string sourceUOM = "")
        {
            Criteria _criteria = new Criteria(yearNum, monthNum, astName, valCheck, entName, facName, prodName, uomName, sourceUOM);
            return DataPortal.Fetch<ViewAllValidationDetail>(_criteria);
        }

        #endregion

        #region Criteria

        [Serializable()]
        protected class Criteria
        {
            // Private fields
            private int _yearNum;
            private int _monthNum;
            private string _astName;
            private string _valCheck;
            private string _entName;
            private string _prodName;
            private string _uomName;
            private string _sourceUOM;

            // Accessors
            public int YearNumber { get { return _yearNum; } }
            public int MonthNumber { get { return _monthNum; } }
            public string AssetName { get { return _astName; } }
            public string ValCheck { get { return _valCheck; } }
            public string EntName { get { return _entName; } }
            public string ProdName { get { return _prodName; } }
            public string UOMName { get { return _uomName; } }
            public string SourceUOM { get { return _sourceUOM; } }

            public Criteria()
            {
                SetCriteria(0, 0, "", "");
            }

            public Criteria(int yearNum, int monthNum, string astName = "", string valcheck = "", string entName = "", string facName = "", string prodName = "", string uomName = "", string sourceUOM = "")
            {
                SetCriteria(yearNum, monthNum, astName, valcheck, entName, facName, prodName, uomName, sourceUOM);
            }

            public void SetCriteria(int yearNum, int monthNum, string astName = "", string valCheck = "", string entName = "", string facName = "", string prodName = "", string uomName = "", string sourceUOM = "")
            {
                _yearNum = yearNum;
                _monthNum = monthNum;
                _astName = astName;
                _valCheck = valCheck;
                _entName = entName != string.Empty ? facName.Replace(".ALL ", string.Empty) : entName;
                _prodName = prodName;
                _uomName = uomName;
                _sourceUOM = sourceUOM;
            }
        }

        #endregion

        #region CSLA Overides

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria cr = (Criteria)criteria;
            ViewAllValidationDetail viewAllValidation = new ViewAllValidationDetail();

            if (cr.YearNumber == 0 || cr.MonthNumber == 0) return;

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(
                new DataAccess().ExecuteStoredProcedure("prcVPeGridViewAllDetailGrid", cr.YearNumber, cr.MonthNumber, cr.AssetName, cr.ValCheck, cr.EntName, cr.ProdName, cr.UOMName, cr.SourceUOM)))
            {
                while (dataReader.Read())
                {
                    ViewAllValidationDetailRecord record = new ViewAllValidationDetailRecord(dataReader);
                    this.Add(record);
                }
            }
        }

        #endregion

    }
}