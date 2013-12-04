using System;
namespace Hess.Corporate.GHGPortal.Business
{
    public class FacilityDataOwner:BusinessObject<FacilityDataOwner>
    {
        #region Private Constants
        private const string ID = "DMPK_FacilityDataOwner";
        private const string DATA_OWNER_NAME = "DataOwner_Name";
        private const string SSO_TEXT = "SSO_Text";
        private const string ADDED_BY_NAME = "AddedBy_Name";
        private const string ADDED_DATE = "Added_Dt";
        private const string EFFECTIVE_DATE = "Effective_Dt";
        private const string EXPIRATION_DATE = "Expiration_Dt";
        private const string ACTIVE_IND = "Active_Ind";
        private const string LAST_UPDATE_DATE = "LastUpdate_Dt";
        private const string LAST_UPDATE_BY = "LastUpdate_By";
        private const string FACILITY_NAME = "Facility_Name";
        #endregion

        #region Constructors
        public FacilityDataOwner()
        {

        }

        public FacilityDataOwner(Csla.Data.SafeDataReader reader)
        {
            this.FetchData(reader);
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string FacilityName { get; set; }
        //owner's windows login id
        public string OwnerSSO { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Active { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }

        protected override object GetIdValue()
        {
            return Id;
        }
        #endregion

        #region Data Access
        protected override void FetchData(Csla.Data.SafeDataReader reader)
        {
            if (reader != null)
            {
                this.Id = reader.GetInt32(ID);
                this.OwnerName = reader.GetString(DATA_OWNER_NAME);
                this.FacilityName = reader.GetString(FACILITY_NAME);
                this.OwnerSSO = reader.GetString(SSO_TEXT);
                this.AddedBy = reader.GetString(ADDED_BY_NAME);
                this.AddedDate = reader.GetDateTime(ADDED_DATE);
                this.EffectiveDate = reader.GetDateTime(EFFECTIVE_DATE);
                this.ExpirationDate = reader.GetDateTime(EXPIRATION_DATE);
                this.Active = reader.GetString(ACTIVE_IND) == "Y" ?  true : false;
                this.UpdateBy = reader.GetString(LAST_UPDATE_BY);
                this.UpdateDate = reader.GetDateTime(LAST_UPDATE_DATE);

                base.FetchData(reader);
            }
           
        }
        #endregion
    }
}