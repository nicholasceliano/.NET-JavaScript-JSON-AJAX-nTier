using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla;
using Hess.Corporate.GHGPortal.Data;

namespace Hess.Corporate.GHGPortal.Business
{

    public class ViewAllValidation : BusinessObjects<ViewAllValidation, ViewAllValidationRecord>
    {
        public string Entity { get; set; }
        public string Facility { get; set; }
        public string SourceCategory { get; set; }
        public int YearNumber { get; set; }
        public int MonthNumber { get; set; }
        public string AssetName { get; set; }
        public string DataSource { get; set; }
        public string AssetType { get; set; }
        public string Validated { get; set; }

        #region Criteria

        [Serializable()]
        protected class Criteria
        {
            // Private fields
            private string _entName;
            private string _facName;
            private string _scName;
            private int _yearNum;
            private int _monthNum;
            private string _astName;
            private string _dsourceName;
            private string _astType;
            private string _valChk;

            // Accessors
            public string Entity { get { return _entName; } }
            public string Facility { get { return _facName; } }
            public string SourceCategory { get { return _scName; } }
            public int YearNumber { get { return _yearNum; } }
            public int MonthNumber { get { return _monthNum; } }
            public string AssetName { get { return _astName; } }
            public string DataSource { get { return _dsourceName; } }
            public string AssetType { get { return _astType; } }
            public string Validated { get { return _valChk; } }

            public Criteria()
            {
                SetCriteria(0, 0);
            }

            public Criteria(int yearNum, int monthNum, string entName = "", string facName = "", string scName = "", string astName = "", string dsourceName = "", string astType = "", string valChk = "")
            {
                SetCriteria(yearNum, monthNum, entName, facName, scName, astName, dsourceName, astType, valChk);
            }

            public void SetCriteria(int yearNum, int monthNum, string entName = "", string facName = "", string scName = "", string astName = "", string dsourceName = "", string astType = "", string valChk = "")
            {
                _yearNum = yearNum;
                _monthNum = monthNum;
                _entName = entName == string.Empty ? facName.Replace(".ALL ", string.Empty) : entName;
                _facName = facName;
                _scName = scName;
                _astName = astName;
                _dsourceName = dsourceName;
                _astType = astType;
                _valChk = valChk;
            }
        }

        #endregion

        #region Factory methods

        public static ViewAllValidation GetViewAllMaster(int yearNum, int monthNum, string entName = "", string facName = "", string scName = "", string astName = "", string dsourceName = "", string astType = "", string valChk = "")
        {
            Criteria _criteria = new Criteria(yearNum, monthNum, entName, facName, scName, astName, dsourceName, astType, valChk);
            return DataPortal.Fetch<ViewAllValidation>(_criteria);
        }

        public void Save()
        {
            foreach (ViewAllValidationRecord svr in this)
            {
                svr.Save();
            }
        }

        #endregion

        #region CSLA Overides

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria cr = (Criteria)criteria;
            ViewAllValidation stdValidation = new ViewAllValidation();

            this.Entity = cr.Entity;
            this.Facility = cr.Facility;
            this.SourceCategory = cr.SourceCategory;
            this.YearNumber = cr.YearNumber;
            this.MonthNumber = cr.MonthNumber;
            this.AssetName = cr.AssetName;
            this.DataSource = cr.DataSource;
            this.AssetType = cr.AssetType;
            this.Validated = cr.Validated;

            if ((cr.Facility.Trim() == "" && cr.Entity.Trim() == "") || cr.YearNumber == 0 || cr.MonthNumber == 0) return;

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(
                new DataAccess().ExecuteStoredProcedure("prcVPeGridViewAllMasterGrid", cr.Entity, cr.Facility, cr.SourceCategory, cr.YearNumber, cr.MonthNumber, cr.AssetName, cr.DataSource, cr.AssetType, cr.Validated)))
            {
                while (dataReader.Read())
                {
                    ViewAllValidationRecord record = new ViewAllValidationRecord(dataReader);
                    this.Add(record);
                }
            }
        }

        #endregion
    }
}