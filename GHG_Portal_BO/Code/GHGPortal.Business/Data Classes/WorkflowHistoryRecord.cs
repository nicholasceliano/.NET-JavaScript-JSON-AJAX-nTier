using System;
using System.Collections.Generic;
using Hess.Corporate.GHGPortal.Data;
using Hess.Corporate.GHGPortal.Business.ComponentModel;
using System.Data;
namespace Hess.Corporate.GHGPortal.Business
{
    public class WorkflowHistoryRecord : BusinessBase<WorkflowHistoryRecord>
    {
        [DataField("Facility_Name")]
        public string FacilityName { get; set; }

        [DataField("SourceCategory")]
        public string SourceCategory { get; set; }

        [DataField("Volume")]
        public double Volume { get; set; }

        [DataField("approved")]
        public string ValidRecords { get; set; }

        [DataField("NonApproved")]
        public string InvalidRecords { get; set; }

        [DataField("ReviewedOn_Dt")]
        public DateTime ApprovalDate { get; set; }

        [DataField("ReviewedBy_Name")]
        public string Approver { get; set; }

        [DataField("Event_Dt")]
        public DateTime EventDate { get; set; }

        [DataField("SentToVendor_Dt")]
        public DateTime? SentDate { get; set; }

        [DataField(Indexed = true, IsUniquePrimary = true)]
        public string PrimaryKey { get { return SourceCategory + FacilityName; } }

        public string ValidationYear { get; set; }
        public string ValidationMonth { get; set; }
        public string UnitOfMeasure { get; set; }
        public bool Status { get { return ValidRecords == InvalidRecords; } }

        public bool SentToVendor { get { return this.SentDate.HasValue; } }


    }
}
