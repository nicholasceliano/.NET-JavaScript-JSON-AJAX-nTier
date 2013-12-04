using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Csla;
using Hess.Corporate.GHGPortal.Data;

namespace Hess.Corporate.GHGPortal.Business
{

    public class VesselValidation : BusinessObjects<VesselValidation, VesselValidationRecord>
    {
        public string Entity { get; set; }
        public string Facility { get; set; }
        public int YearNumber { get; set; }
        public int MonthNumber { get; set; }
        public string VesselName { get; set; }
        public string Validated { get; set; }

        #region Criteria

        [Serializable()]
        protected class Criteria
        {
            // Private fields
            private string _entName;
            private string _facName;
            private int _yearNum;
            private int _monthNum;
            private string _astName;
            private string _valChk;

            // Accessors
            public string Entity { get { return _entName; } }
            public string Facility { get { return _facName; } }
            public int YearNumber { get { return _yearNum; } }
            public int MonthNumber { get { return _monthNum; } }
            public string VesselName { get { return _astName; } }
            public string Validated { get { return _valChk; } }

            public Criteria()
            {
                SetCriteria("", "", 0, 0, "", "");
            }

            public Criteria(string entName, string facName, int yearNum, int monthNum, string astName, string valChk)
            {
                SetCriteria(entName, facName, yearNum, monthNum, astName, valChk);
            }

            public void SetCriteria(string entName, string facName, int yearNum, int monthNum, string astName, string valChk)
            {
                _yearNum = yearNum;
                _monthNum = monthNum;
                _entName = entName;
                _facName = facName;
                _astName = astName;
                _valChk = valChk;
            }
        }

        #endregion

        #region Factory methods

        public static VesselValidation GetStdMaster(string entName, string facName, int yearNum, int monthNum, string astName, string valChk)
        {
            Criteria _criteria = new Criteria(entName, facName, yearNum, monthNum, astName, valChk);
            return DataPortal.Fetch<VesselValidation>(_criteria);
        }

        public void Save()
        {
            foreach (VesselValidationRecord svr in this)
            {
                svr.Save();
            }
        }

        #endregion

        #region CSLA Overides

        protected override void DataPortal_Fetch(object criteria)
        {
            Criteria cr = (Criteria)criteria;
            VesselValidation stdValidation = new VesselValidation();

            this.Entity = cr.Entity;
            this.Facility = cr.Facility;
            this.YearNumber = cr.YearNumber;
            this.MonthNumber = cr.MonthNumber;
            this.VesselName = cr.VesselName;
            this.Validated = cr.Validated;

            if ((cr.Facility.Trim() == "" && cr.Entity.Trim() == "") || cr.YearNumber == 0 || cr.MonthNumber == 0) return;

            using (Csla.Data.SafeDataReader dataReader = new Csla.Data.SafeDataReader(
                new DataAccess().ExecuteStoredProcedure("prcVPeGridVesselMasterGrid", cr.Entity, cr.Facility, cr.YearNumber, cr.MonthNumber, cr.VesselName, cr.Validated)))
            {
                while (dataReader.Read())
                {
                    VesselValidationRecord record = new VesselValidationRecord(dataReader);
                    this.Add(record);
                }
            }
        }

        #endregion
    }
}