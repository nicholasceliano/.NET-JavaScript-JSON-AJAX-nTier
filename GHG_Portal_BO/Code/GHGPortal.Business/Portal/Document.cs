using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hess.Corporate.GHGPortal.Business
{
    public class Document : BusinessObject<Document>
    {
        public int Id { get; set; }
        public int HeaderId { get; set; }
        public string Source { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ConfirmFileName { get; set; }
        public bool LastUploadAttempt { get; set; }
        public bool Obsolete { get; set; }
        public DateTime AgreementDate { get; set; }

        protected override object GetIdValue()
        {
            return Id;
        }

        public Document(Csla.Data.SafeDataReader reader)
        {
            this.Fetch(reader);
        }

        private void Fetch(Csla.Data.SafeDataReader reader)
        {
            this.Id = reader.GetInt32("Id");
            this.HeaderId = reader.GetInt32("HeaderId");
            this.Source = reader.GetString("Source");
            this.Status = reader.GetString("Status");
            this.Reason = reader.GetString("Reason");
            this.Notes = reader.GetString("Notes");
            this.CreatedOn = reader.GetDateTime("CreatedOn");
            this.CreatedBy = reader.GetString("CreatedBy");
            this.ConfirmFileName = reader.GetString("ConfirmFileName");
            this.LastUploadAttempt = reader.GetBoolean("LastUploadAttempt");
            this.Obsolete = reader.GetBoolean("Obsolete");
            this.AgreementDate = reader.GetDateTime("AgreementDate");
        }
    }
}
