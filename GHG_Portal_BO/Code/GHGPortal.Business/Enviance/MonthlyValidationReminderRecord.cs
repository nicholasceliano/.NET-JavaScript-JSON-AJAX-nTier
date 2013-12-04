using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hess.Corporate.GHGPortal.Business
{
    public class MonthlyValidationReminderRecord:BusinessObject<MonthlyValidationReminderRecord>
    {
        #region Constructors

        public MonthlyValidationReminderRecord()
        {

        }

        public MonthlyValidationReminderRecord(Csla.Data.SafeDataReader reader)
        {
            this.FetchData(reader);
        }

        #endregion

        #region Properties

        private string _facilityName;
        private string _emmittingAsset;
        private DateTime _eventDate;
        private double _emissionVolume;
        private string _uom;

        public DateTime EventDate 
        {
            get { return _eventDate; }
            set
            {
                this._eventDate = value;
                this.MarkDirty();
            }
        }
        public string ValidationPeriod { get { return this.EventDate.Month + "/" + this.EventDate.Year; } }
        public double EmissionVolume 
        {
            get { return _emissionVolume; }
            set
            {
                _emissionVolume = value;
                this.MarkDirty();
            }
        }
        public string FormattedVolume
        {
            get { return String.Format("{0:n0}", this.EmissionVolume); }
        }
        public string FacilityName 
        {
            get { return _facilityName; }
            set
            {
                _facilityName = value;
                this.MarkDirty();
            }
        }
        public string EmmittingAssetName 
        {
            get { return _emmittingAsset; }
            set
            {
                _emmittingAsset = value;
                this.MarkDirty();
            }
        }
        public string UnitOfMeasure 
        {
            get { return _uom; }
            set
            {
                _uom = value;
                this.MarkDirty();
            }
        }

        protected override object GetIdValue()
        {
            return 0;
        }

        #endregion

        #region Data Access

        protected override void FetchData(Csla.Data.SafeDataReader reader)
        {
            if (reader != null)
            {
                this.EventDate = reader.GetDateTime("event_dt");
                this.EmissionVolume = Convert.ToDouble(reader.GetDecimal("volume"));
                this.FacilityName = reader.GetString("facility");
                this.EmmittingAssetName = reader.GetString("asset");
                this.UnitOfMeasure = reader.GetString("uom");
            }
            base.FetchData(reader);
        }
        #endregion
    }
}
