using System;
using System.Web.Services;
using GHGPortal.Web.com.ihess.DocManager;
using Hess.Corporate.GHGPortal.Configuration;

namespace GHGPortal.Web
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class DocManagerServices : System.Web.Services.WebService
    {
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public string GetDocuments(string moveNumber)
        {
            string docGroup = "Blades Docs",
                docType = "Quality";
            int docNumber = 0;

            DocManagerWebServices dm = new DocManagerWebServices();
            AppConfiguration config = AppConfiguration.Current;
            dm.UseDefaultCredentials = true;
            dm.Url = config.DocManagerConnections;
            ConfirmTrackingDetailItem[] items = dm.GetDocumentDetails(moveNumber, string.Empty, string.Empty, string.Empty, string.Empty, docGroup, docType, string.Empty, string.Empty);

            System.Text.StringBuilder html = new System.Text.StringBuilder();
            html.AppendFormat("<table id='tblDocManager' cellpadding='0' cellspacing='0' style='width:100%;border-collapse:collapse;' class='gridView'>");
            html.AppendFormat("<tr class='gridViewHeader' style='height:18px;'>");
            html.AppendFormat("<th style='width:10%;'>Status</th>");
            html.AppendFormat("<th>FileName</th>");
            html.AppendFormat("<th style='width:10%;'>Filed On Date</th>");
            html.AppendFormat("<th style='width:10%;'>Filed By</th>");
            html.AppendFormat("<th>Notes</th>");
            html.AppendFormat("</tr>");

            foreach (ConfirmTrackingDetailItem doc in items)
            {
                string rowClass = docNumber % 2 == 0 ? "gridViewAlternatingRow" : "gridViewRow";
                html.AppendFormat("<tr class={0} style='height:18px;'>", rowClass);
                html.AppendFormat("<td style='width:10%;text-align:center;'>{0}</td>", doc.Status);
                html.AppendFormat("<td><a href='{1}' style='color:Blue;' target='_blank'>{0}</a></td>", doc.ConfirmFilename, "http://stt.ihess.com/bladestrack/FileViewer.aspx?DetailId=" + doc.Id);
                html.AppendFormat("<td style='width:10%;text-align:center;'>{0}</td>", doc.CreatedOn.ToShortDateString());
                html.AppendFormat("<td style='width:10%;text-align:left;'>{0}</td>", doc.CreatedBy);
                html.AppendFormat("<td>{0}</td>", doc.Notes);
                html.AppendFormat("</tr>");
                docNumber = docNumber + 1;
            }

            html.AppendFormat("</table>");
            return html.ToString();
        }
    }
}
