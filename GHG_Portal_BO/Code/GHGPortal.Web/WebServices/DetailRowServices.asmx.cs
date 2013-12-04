using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using Hess.Corporate.GHGPortal.Business;
using System.Text;

namespace GHGPortal.Web
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class DetailRowServices : System.Web.Services.WebService
    {
        public string currentUser = HttpContext.Current.User.Identity.Name.ToString();

        #region View All Page

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public string ViewAllChild(int yearNum, int monthNum, string astName, string entName, string facName, string prodName, string uomName, string sourceUOM, int rowNum)
        {
            ViewAllValidationDetail results = ViewAllValidationDetail.GetViewAllDetail(yearNum, monthNum, astName, "", entName, facName, prodName, uomName, sourceUOM);
            
            StringBuilder html = new StringBuilder();
            html.AppendFormat("<table ID='vGridDetails_" + rowNum.ToString() + "' runat='server' cellpadding='0' cellspacing='0' class='ChildGrid'>");
            html.AppendFormat("<tr class='ChildGridHeader'>");
            html.AppendFormat("<th style='display:none;'>RowKey</th>");
            html.AppendFormat("<th class='VID'>VID</th>");
            html.AppendFormat("<th class='AssetName'>Facility/Asset Name</th>");
            html.AppendFormat("<th>Jan</th>");
            html.AppendFormat("<th>Feb</th>");
            html.AppendFormat("<th>Mar</th>");
            html.AppendFormat("<th>Apr</th>");
            html.AppendFormat("<th>May</th>");
            html.AppendFormat("<th>Jun</th>");
            html.AppendFormat("<th>Jul</th>");
            html.AppendFormat("<th>Aug</th>");
            html.AppendFormat("<th>Sep</th>");
            html.AppendFormat("<th>Oct</th>");
            html.AppendFormat("<th>Nov</th>");
            html.AppendFormat("<th>Dec</th>");
            html.AppendFormat("</tr>");

            int i = 0;
            foreach (ViewAllValidationDetailRecord rec in results)
            {
                if (i % 2 == 0)
                    html.AppendFormat("<tr class='ChildRow'>");
                else
                    html.AppendFormat("<tr class='ChildAlternatingRow'>");

                #region Columns
                html.AppendFormat("<td style=display:none;'>{0}</td>", rec.RowKey);
                html.AppendFormat("<td class='VID'><div id='VID_" + rec.RowKey.ToString() + "' onmouseover='TipsyVIDLBL();' tipsy=''>{0}</div></td>", rec.Validated);
                html.AppendFormat("<td class='AssetName'><div id='AssetName_" + rec.RowKey.ToString() + "' onmouseover='TipsyShowTipsy();' tipsy='{1}'>{0}</div></td>", rec.AssetType, rec.AssetName);

                if (monthNum == 1)
                    html.AppendFormat("<td><input id='vGridDetails_" + rowNum.ToString() + "_txtEdit_" + i.ToString() + "' type='text' value='{0}' onclick='txtSelectedViewAll();' class='InvisTxtBox' onkeydown='bkgrndBlue();enableLeaveFunctions(true);' /></td>", VPC.AddCommas(rec.JanVol));
                else
                    html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.JanVol));

                if (monthNum == 2)
                    html.AppendFormat("<td><input id='vGridDetails_" + rowNum.ToString() + "_txtEdit_" + i.ToString() + "' type='text' value='{0}' onclick='txtSelectedViewAll();' class='InvisTxtBox' onkeydown='bkgrndBlue();enableLeaveFunctions(true);' /></td>", VPC.AddCommas(rec.FebVol));
                else
                    html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.FebVol));

                if (monthNum == 3)
                    html.AppendFormat("<td><input id='vGridDetails_" + rowNum.ToString() + "_txtEdit_" + i.ToString() + "' type='text' value='{0}' onclick='txtSelectedViewAll();' class='InvisTxtBox' onkeydown='bkgrndBlue();enableLeaveFunctions(true);' /></td>", VPC.AddCommas(rec.MarVol));
                else
                    html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.MarVol));

                if (monthNum == 4)
                    html.AppendFormat("<td><input id='vGridDetails_" + rowNum.ToString() + "_txtEdit_" + i.ToString() + "' type='text' value='{0}' onclick='txtSelectedViewAll();' class='InvisTxtBox' onkeydown='bkgrndBlue();enableLeaveFunctions(true);' /></td>", VPC.AddCommas(rec.AprVol));
                else
                    html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.AprVol));

                if (monthNum == 5)
                    html.AppendFormat("<td><input id='vGridDetails_" + rowNum.ToString() + "_txtEdit_" + i.ToString() + "' type='text' value='{0}' onclick='txtSelectedViewAll();' class='InvisTxtBox' onkeydown='bkgrndBlue();enableLeaveFunctions(true);' /></td>", VPC.AddCommas(rec.MayVol));
                else
                    html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.MayVol));

                if (monthNum == 6)
                    html.AppendFormat("<td><input id='vGridDetails_" + rowNum.ToString() + "_txtEdit_" + i.ToString() + "' type='text' value='{0}' onclick='txtSelectedViewAll();' class='InvisTxtBox' onkeydown='bkgrndBlue();enableLeaveFunctions(true);' /></td>", VPC.AddCommas(rec.JunVol));
                else
                    html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.JunVol));

                if (monthNum == 7)
                    html.AppendFormat("<td><input id='vGridDetails_" + rowNum.ToString() + "_txtEdit_" + i.ToString() + "' type='text' value='{0}' onclick='txtSelectedViewAll();' class='InvisTxtBox' onkeydown='bkgrndBlue();enableLeaveFunctions(true);' /></td>", VPC.AddCommas(rec.JulVol));
                else
                    html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.JulVol));

                if (monthNum == 8)
                    html.AppendFormat("<td><input id='vGridDetails_" + rowNum.ToString() + "_txtEdit_" + i.ToString() + "' type='text' value='{0}' onclick='txtSelectedViewAll();' class='InvisTxtBox' onkeydown='bkgrndBlue();enableLeaveFunctions(true);' /></td>", VPC.AddCommas(rec.AugVol));
                else
                    html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.AugVol));

                if (monthNum == 9)
                    html.AppendFormat("<td><input id='vGridDetails_" + rowNum.ToString() + "_txtEdit_" + i.ToString() + "' type='text' value='{0}' onclick='txtSelectedViewAll();' class='InvisTxtBox' onkeydown='bkgrndBlue();enableLeaveFunctions(true);' /></td>", VPC.AddCommas(rec.SepVol));
                else
                    html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.SepVol));

                if (monthNum == 10)
                    html.AppendFormat("<td><input id='vGridDetails_" + rowNum.ToString() + "_txtEdit_" + i.ToString() + "' type='text' value='{0}' onclick='txtSelectedViewAll();' class='InvisTxtBox' onkeydown='bkgrndBlue();enableLeaveFunctions(true);' /></td>", VPC.AddCommas(rec.OctVol));
                else
                    html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.OctVol));

                if (monthNum == 11)
                    html.AppendFormat("<td><input id='vGridDetails_" + rowNum.ToString() + "_txtEdit_" + i.ToString() + "' type='text' value='{0}' onclick='txtSelectedViewAll();' class='InvisTxtBox' onkeydown='bkgrndBlue();enableLeaveFunctions(true);' /></td>", VPC.AddCommas(rec.NovVol));
                else
                    html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.NovVol));

                if (monthNum == 12)
                    html.AppendFormat("<td><input id='vGridDetails_" + rowNum.ToString() + "_txtEdit_" + i.ToString() + "' type='text' value='{0}' onclick='txtSelectedViewAll();' class='InvisTxtBox' onkeydown='bkgrndBlue();enableLeaveFunctions(true);' /></td>", VPC.AddCommas(rec.DecVol));
                else
                    html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.DecVol));

                #endregion

                html.AppendFormat("</tr>");

                i++;
            }
            html.AppendFormat("</table>");

            return html.ToString();
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public void SaveAllChild(int yearNum, int monthNum, string astName, string entName, string facName, string prodName, string uomName, string sourceUOM, int? RowKey, string VID, int? monthlyValue, int rowNum)
        {
            ViewAllValidationDetail results = ViewAllValidationDetail.GetViewAllDetail(yearNum, monthNum, astName, "", entName, facName, prodName, uomName, sourceUOM);

            if (RowKey == null)
            {
                foreach (ViewAllValidationDetailRecord rec in results)
                    SaveViewAllChild(rec, monthlyValue, monthNum, VID);
            }
            else 
            {
                ViewAllValidationDetailRecord rec = (from c in results
                                                     where c.RowKey == RowKey
                                                     select c).FirstOrDefault();

                SaveViewAllChild(rec, monthlyValue, monthNum, VID);
            }
        }

        private void SaveViewAllChild(ViewAllValidationDetailRecord rec, int? monthlyValue, int monthNum, string VID)
        {
            long? thisMonthValue = monthlyValue;

            switch (monthNum)
            {
                case 1:
                    thisMonthValue = rec.JanVol;
                    break;
                case 2:
                    thisMonthValue = rec.FebVol;
                    break;
                case 3:
                    thisMonthValue = rec.MarVol;
                    break;
                case 4:
                    thisMonthValue = rec.AprVol;
                    break;
                case 5:
                    thisMonthValue = rec.MayVol;
                    break;
                case 6:
                    thisMonthValue = rec.JunVol;
                    break;
                case 7:
                    thisMonthValue = rec.JulVol;
                    break;
                case 8:
                    thisMonthValue = rec.AugVol;
                    break;
                case 9:
                    thisMonthValue = rec.SepVol;
                    break;
                case 10:
                    thisMonthValue = rec.OctVol;
                    break;
                case 11:
                    thisMonthValue = rec.NovVol;
                    break;
                case 12:
                    thisMonthValue = rec.DecVol;
                    break;
            }

            if (monthlyValue != thisMonthValue)
            {
                if (monthlyValue != null)
                    rec.OverrideVolume = monthlyValue;
                else
                    rec.OverrideVolume = null;

                rec.ChangedBy_Name = currentUser;
            }

            if (VID != rec.Validated)
            {
                if (VID == "N" || monthlyValue.ToString().Length > 0)
                    rec.Validated = VID;

                if (VID == "N")
                    rec.ReviewedBy_Name = null;
                else
                    rec.ReviewedBy_Name = currentUser;
            }

            rec.Save();
        }

        #endregion

        #region Standard Page

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public string StandardChild(string facName, int yearNum, int monthNum, string UOM, string UOMSource, string scName, string astName, string dsourceName, string astType, string productName)
        {
            StandardValidationDetail results = StandardValidationDetail.GetStdDetail(facName, yearNum, monthNum, UOM, UOMSource, scName, astName, dsourceName, astType, "", productName);

            StringBuilder html = new StringBuilder();
            html.AppendFormat("<table cellpadding='0' cellspacing='0' class='ChildGrid'>");
            html.AppendFormat("<tr class='ChildGridHeader'>");
            html.AppendFormat("<th class='AssetName'>Asset Type</th>");
            html.AppendFormat("<th>Jan</th>");
            html.AppendFormat("<th>Feb</th>");
            html.AppendFormat("<th>Mar</th>");
            html.AppendFormat("<th>Apr</th>");
            html.AppendFormat("<th>May</th>");
            html.AppendFormat("<th>Jun</th>");
            html.AppendFormat("<th>Jul</th>");
            html.AppendFormat("<th>Aug</th>");
            html.AppendFormat("<th>Sep</th>");
            html.AppendFormat("<th>Oct</th>");
            html.AppendFormat("<th>Nov</th>");
            html.AppendFormat("<th>Dec</th>");
            html.AppendFormat("</tr>");

            int i = 0;
            foreach (StandardValidationDetailRecord rec in results)
            {
                if (i % 2 == 0)
                    html.AppendFormat("<tr class='ChildRow'>");
                else
                    html.AppendFormat("<tr class='ChildAlternatingRow'>");

                #region Columns

                if (rec.MeterID.Length > 0)
                    html.AppendFormat("<td class='AssetName'><div id='AssetName_" + rec.MeterID + "' onmouseover='TipsyShowTipsy();' tipsy='{1}'>{0}</div></td>", rec.AssetType, "METER ID:" + rec.MeterID);
                else
                    html.AppendFormat("<td class='AssetName'>{0}</td>", rec.AssetType);
                html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.JanVol));
                html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.FebVol));
                html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.MarVol));
                html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.AprVol));
                html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.MayVol));
                html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.JunVol));
                html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.JulVol));
                html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.AugVol));
                html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.SepVol));
                html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.OctVol));
                html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.NovVol));
                html.AppendFormat("<td>{0}</td>", VPC.AddCommas(rec.DecVol));

                #endregion

                html.AppendFormat("</tr>");
                i++;
            }
            html.AppendFormat("</table>");

            return html.ToString();
        }

        #endregion
    }
}
