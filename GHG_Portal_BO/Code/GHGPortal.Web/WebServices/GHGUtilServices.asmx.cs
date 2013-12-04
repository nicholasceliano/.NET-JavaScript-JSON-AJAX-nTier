using System;
using System.Web.Services;
using Hess.Corporate.GHGPortal.Business;
using GHGPortal.Web.BulkOrderMove;

namespace GHGPortal.Web
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class GHGUtilServices : System.Web.Services.WebService
    {
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public string RenderWorkFlowHistory(string month, string year, string facility, string entity)
        {
            int recordNumber = 0;
            WorkflowHistory history = WorkflowHistory.GetRecords(month, year, facility, entity);

            System.Text.StringBuilder html = new System.Text.StringBuilder();
            html.AppendFormat("<table id='tblWorkFlowHistory' cellpadding='0' cellspacing='0' style='width:100%;border-collapse:collapse;' class='gridView'>");
            html.AppendFormat("<tr class='gridViewHeader' style='height:18px;'>");
            html.AppendFormat("<th style='width:18%;'>Facility</th>");
            html.AppendFormat("<th style='width:17%;'>Source Category</th>");
            html.AppendFormat("<th style='width:9%;'>Validation Period</th>");
            html.AppendFormat("<th style='width:7%;'>Status</th>");
            html.AppendFormat("<th style='width:9%;'>Validated Records</th>");
            html.AppendFormat("<th style='width:7%;'>Approved By</th>");
            html.AppendFormat("<th style='width:7%;'>Approved On</th>");
            html.AppendFormat("<th style='width:9%;'>Sent To ENVIANCE</th>");
            html.AppendFormat("<th style='width:7%;'>Date Sent</th>");
            html.AppendFormat("</tr>");

            foreach (WorkflowHistoryRecord record in history)
            {
                string rowClass = recordNumber % 2 == 0 ? "gridViewAlternatingRow" : "gridViewRow";
                html.AppendFormat("<tr class={0} style='height:18px;'>", rowClass);
                html.AppendFormat("<td style='white-space:nowrap;width:18%;'>{0}</td>", facility);
                html.AppendFormat("<td style='white-space:nowrap;width:17%;'>{0}</td>", record.SourceCategory);
                html.AppendFormat("<td style='text-align:center;width:9%;'>{0}</td>", record.ValidationPeriod);
                html.AppendFormat("<td style='text-align:center;width:7%;'>{0}</td>", record.Status);
                html.AppendFormat("<td style='text-align:center;width:9%;'>{0}</td>", record.ValidationRecords);
                html.AppendFormat("<td style='text-align:center;width:7%;'>{0}</td>", record.ApprovedBy);
                html.AppendFormat("<td style='text-align:center;width:7%;'>{0}</td>", record.ApprovedOn == DateTime.MinValue ? string.Empty : record.ApprovedOn.ToShortDateString());
                html.AppendFormat("<td style='text-align:center;width:9%;'>{0}</td>", record.SentToEnviance);
                html.AppendFormat("<td style='text-align:center;width:7%;'>{0}</td>", record.SentToVendor_Dt == DateTime.MinValue ? string.Empty : record.SentToVendor_Dt.ToShortDateString());
                html.AppendFormat("</tr>");
                recordNumber = recordNumber + 1;
            }

            html.AppendFormat("</table>");
            return html.ToString();
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public string RenderProcessLogHistory(string year, string month)
        {
            DateTime validationStartDate = Convert.ToDateTime(month + "/1/" + year);
            string fullMonth = validationStartDate.ToString("MMMM");
            ProcessHistoryLogs logs = ProcessHistoryLogs.GetList(fullMonth,year);
            return logs.RenderGrid();
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public string GetBulkOrderTable(string bulkOrder)
        {
            CommonWebService cws = new CommonWebService();
            cws.Credentials = new System.Net.NetworkCredential(Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.CWSLoginName, Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.CWSPassword, "ihess");
            string bulkInfo= cws.GetGoodsMovementDetails(bulkOrder);
            if (string.IsNullOrEmpty(bulkInfo)) bulkInfo = "No Data!";
            return bulkInfo;
        }
    }
}
