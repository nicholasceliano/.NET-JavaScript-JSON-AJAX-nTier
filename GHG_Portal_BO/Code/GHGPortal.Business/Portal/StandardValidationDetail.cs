using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla.Data;
using Hess.Corporate.GHGPortal.Data;

namespace Hess.Corporate.GHGPortal.Business
{
    public class StandardValidationDetail : BusinessObjects<StandardValidationDetail, StandardValidationDetailRecord>
    {
        #region Factory methods

        public static StandardValidationDetail GetStdDetail(string facName, int yearNum, int monthNum, string UOM, string UOMSource, string scName = "", string astName = "", string dsourceName = "", string astType = "", string valChk = "", string productName = "")
        {
            Criteria _criteria = new Criteria(facName, yearNum, monthNum, UOM, UOMSource, scName, astName, dsourceName, astType, valChk, productName);
            return DataPortal.Fetch<StandardValidationDetail>(_criteria);
        }

        #endregion

        #region Criteria

        [Serializable()]
        protected class Criteria
        {
            // Private fields
            private string _facName;
            private int _yearNum;
            private int _monthNum;
            private string _scName;
            private string _astName;
            private string _dsourceName;
            private string _astType;
            private string _valChk;
            private string _productName;
            private string _UOM;
            private string _UOMSource;

            // Accessors
            public string Facility { get { return _facName; } }
            public string SourceCategory { get { return _scName; } }
            public int YearNumber { get { return _yearNum; } }
            public int MonthNumber { get { return _monthNum; } }
            public string AssetName { get { return _astName; } }
            public string DataSource { get { return _dsourceName; } }
            public string AssetType { get { return _astType; } }
            public string Validated { get { return _valChk; } }
            public string ProductName { get { return _productName; } }
            public string UOM { get { return _UOM; } }
            public string UOMSource { get { return _UOMSource; } }

            public Criteria()
            {
                SetCriteria("", 0, 0, "", "");
            }

            public Criteria(string facName, int yearNum, int monthNum, string UOM, string UOMSource, string scName = "", string astName = "", string dsourceName = "", string astType = "", string valChk = "", string productName = "")
            {
                SetCriteria(facName, yearNum, monthNum, UOM, UOMSource, scName, astName, dsourceName, astType, valChk, productName);
            }

            public void SetCriteria(string facName, int yearNum, int monthNum, string UOM, string UOMSource, string scName = "", string astName = "", string dsourceName = "", string astType = "", string valChk = "", string productName = "")
            {
                _facName = facName;             // Required search criteria
                _yearNum = yearNum;             // "
                _monthNum = monthNum;           // "
                _scName = scName;
                _astName = astName;
                _dsourceName = dsourceName;
                _astType = astType;
                _valChk = valChk;
                _productName = productName;
                _UOM = UOM;
                _UOMSource = UOMSource;
            }

        }

        #endregion

        #region CSLA Overides

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria cr = (Criteria)criteria;
            StandardValidationDetail stdValidation = new StandardValidationDetail();

            if (cr.YearNumber == 0 || cr.MonthNumber == 0) return;

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(
                new DataAccess().ExecuteStoredProcedure("prcVPeGridStdDetailGrid", cr.Facility, cr.SourceCategory, cr.YearNumber, cr.MonthNumber,  cr.UOM, cr.UOMSource, cr.AssetName, cr.DataSource, cr.AssetType, cr.Validated, cr.ProductName)))
            {
                while (dataReader.Read())
                {
                    StandardValidationDetailRecord record = new StandardValidationDetailRecord(dataReader);
                    this.Add(record);
                }
            }
        }

        #endregion

    }
}