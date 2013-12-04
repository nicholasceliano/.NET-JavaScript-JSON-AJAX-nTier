using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Linq;
using Hess.Corporate.GHGPortal.Business;
using System.ComponentModel;
using AjaxControlToolkit;

namespace Hess.Corporate.GHGPortal.Web.UI
{
    public partial class StandardMasterView : CommonGridViewControl
    {
        #region Private Constants

        public int error = 0;
        private string GridStd = "GridStd";
        private string GirdStdSearch = "GridStdSearch";
        private string saveRef = "saveRefresh";

        #endregion

        #region Method Overrides

        protected override object CurrentBusinessObject
        {
            get
            {
                IBusinessObjects businessObject = (IBusinessObjects)this.Session[GridStd];
                bool refreshBO = (businessObject == null || !(businessObject is StandardValidation));
                string searchCriteria = (string)Session[GirdStdSearch];

                if (!refreshBO)
                {
                    if (hfRefreshOffSearchCriteria.Value == "true")
                    {
                        string saveRefresh = (string)Session[saveRef];
                        StandardValidation sv = (StandardValidation)businessObject;
                        if (!(saveRefresh == null))
                        {
                            if (saveRefresh.TrimEnd() == "true")
                                refreshBO = true;
                        }

                        if (!(searchCriteria == null))
                        {
                            if (sv.Entity.Trim() != searchCriteria.Split('~')[1] || sv.Facility.Trim() != searchCriteria.Split('~')[3] || sv.SourceCategory.Trim() != searchCriteria.Split('~')[5] || sv.YearNumber != Convert.ToInt32(searchCriteria.Split('~')[7]) || sv.MonthNumber != Convert.ToInt32(searchCriteria.Split('~')[9]) || sv.AssetName.Trim() != searchCriteria.Split('~')[11] || sv.DataSource.Trim() != searchCriteria.Split('~')[13] || sv.AssetType.Trim() != searchCriteria.Split('~')[15] || sv.Validated.Trim() != searchCriteria.Split('~')[17])
                                refreshBO = true;
                        }
                    }
                }

                if (refreshBO)
                {
                    try
                    {
                        Session[conBusinessObjectName] = null;
                            businessObject = (IBusinessObjects)this.GetBusinessObject(CommonGridViewControl.conBusinessObjectName, typeof(StandardValidation),
                                Convert.ToInt32(searchCriteria.Split('~')[7]), Convert.ToInt32(searchCriteria.Split('~')[9]), searchCriteria.Split('~')[1], searchCriteria.Split('~')[3], searchCriteria.Split('~')[5], searchCriteria.Split('~')[11], searchCriteria.Split('~')[13], searchCriteria.Split('~')[15], searchCriteria.Split('~')[17]);

                        businessObject.Sort("RowId", ListSortDirection.Descending);
                        this.Session[GridStd] = businessObject;
                    }
                    catch { }
                }

                if (businessObject == null || businessObject.Count == 0)
                    businessObject = new StandardValidation();

                return businessObject;
            }
        }

        protected override void GridViewRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region Change Header Text

            if (e.Row.RowType == DataControlRowType.Header)
            {
                TextBox txtMonthYear = (TextBox)this.Parent.FindControl("txtMonthYear");
                if (txtMonthYear.Text.Length > 0)
                {
                    string searchDate = txtMonthYear.Text.Split('/')[0] + "/1/" + txtMonthYear.Text.Split('/')[1];
                    DateTime date = Convert.ToDateTime(searchDate);
                    ((LinkButton)e.Row.FindControl("btnThisYTDVol")).Text = date.Year.ToString() + " YTD";
                    ((LinkButton)e.Row.FindControl("btnPriorYTDVol")).Text = date.AddYears(-1).Year.ToString() + " YTD";
                    ((LinkButton)e.Row.FindControl("btnThisMonthVol")).Text = date.ToString("MMM") + " " + date.Year.ToString();
                    ((LinkButton)e.Row.FindControl("btnPriorYearMonthVol")).Text = date.ToString("MMM") + " " + date.AddYears(-1).Year.ToString();
                }

                if (hfSort.Value.Length > 0)
                    ((Image)e.Row.FindControl(hfSort.Value)).Visible = true;
            }

            #endregion

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GHGvGridStd parent = (GHGvGridStd)this.Page;
                StandardValidationRecord record = (StandardValidationRecord)e.Row.DataItem;

                string vid = record.Validated.Trim();
                DropDownList ddlValidated = (DropDownList)e.Row.FindControl("ddlValidated");
                ddlValidated.ClearSelection();
                if (record.Validated.Trim() == string.Empty)
                    ddlValidated.SelectedValue = "N";
                else if (vid == "Y") { ddlValidated.SelectedValue = "Y"; }
                else if (vid == "N") { ddlValidated.SelectedValue = "N"; }
                else if (vid == "X") { ddlValidated.SelectedValue = "X"; }
                else if (vid == "I") { ddlValidated.SelectedValue = "I"; }

                this.SetLabelText((Label)e.Row.FindControl("lblDMPKEmissions"), record.RowId.ToString().Trim() != "" ? record.RowId.ToString().Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblValidated"), record.Validated.Trim() != "" ? record.Validated.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblSourceCategoryName"), record.SourceCategory.Trim() != "" ? record.SourceCategory.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblAssetNameFull"), record.AssetNameFull.Trim() != "" ? record.AssetNameFull.Trim() : " ");
                this.SetLabelAttribute((Label)e.Row.FindControl("lblAssetNameFull"), "tipsy", record.AssetName.Trim() != "" ? record.AssetName.Trim() : "");
                this.SetLabelText((Label)e.Row.FindControl("lblProductName"), record.ProductName.Trim() != "" ? record.ProductName.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblMeterId"), record.MeterID != "0" ? record.MeterID : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblThisYTDvol"), record.ThisYTDVol != null ? VPC.AddCommas(record.ThisYTDVol.ToString()) : null);
                this.SetLabelText((Label)e.Row.FindControl("lblPriorYTDvol"), record.PriorYTDVol != null ? VPC.AddCommas(record.PriorYTDVol.ToString()) : null);
                this.SetLabelText((Label)e.Row.FindControl("lblPriorYearMonthVol"), record.PriorYearMonthVol != null ? VPC.AddCommas(record.PriorYearMonthVol.ToString()) : null);
                this.SetLabelText((Label)e.Row.FindControl("lblUOM"), record.UOM.Trim());
                this.SetLabelText((Label)e.Row.FindControl("lblUOMshort"), record.UOMshort.Trim());
                ((TextBox)e.Row.FindControl("txtThisMonthVol")).Text = record.ThisMonthVol != null ? VPC.AddCommas(record.ThisMonthVol.ToString()) : null;
                ((HiddenField)e.Row.FindControl("hfThisMonthVolume")).Value = record.ThisMonthVol != null ? record.ThisMonthVol.ToString() : null;
                ((HiddenField)e.Row.FindControl("hfUOM")).Value = "UOM~" + record.UOM.Trim() + "~Source~" + record.Source_UOM.Trim() + "~End";

                #region Sets the UOM Tooltip if the Source UOM and UOM are different

                if (record.UOM.Trim() != record.Source_UOM.Trim())
                    this.SetLabelAttribute((Label)e.Row.FindControl("lblUOMshort"), "tipsy", "ORIGINAL VOLUME OF " + VPC.AddCommas(record.Source_Volume.ToString()) + " IN " + record.Source_UOM.ToString());

                #endregion

                #region Set Datasource to from Manual Portal to Manual

                if (record.DataSource.Trim() == "MANUAL PORTAL")
                    this.SetLabelText((Label)e.Row.FindControl("lblDataSource"), "MANUAL");
                else
                    this.SetLabelText((Label)e.Row.FindControl("lblDataSource"), record.DataSource.Trim());

                #endregion
               
                #region Turn Rows Red

                double thisYearvol, priorYearvol, variance, YTDvsPYTDVariance = Convert.ToDouble(Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.YTDvsPYTDVariance);
                thisYearvol = record.ThisYTDVol == null ? 0 : Convert.ToDouble(record.ThisYTDVol);
                try { priorYearvol = Convert.ToDouble(record.PriorYTDVol == null ? 0 : record.PriorYTDVol); }
                catch { priorYearvol = 0; }
                variance = (YTDvsPYTDVariance / 100);

                bool checkDate = false;
                try
                {
                    if (Convert.ToDateTime(record.ReviewedOn_Dt) != DateTime.MinValue)
                    {
                        if (Convert.ToDateTime(record.ReviewedOn_Dt) < Convert.ToDateTime(record.ChangedOn_Dt))
                            checkDate = true;
                    }
                }
                catch
                {
                    checkDate = false;
                }

                if ((((thisYearvol - priorYearvol) > (priorYearvol * (variance)) || (thisYearvol - priorYearvol) < (priorYearvol * (-variance))) && record.PriorYTDVol != null) || (record.Missing_Row.Contains("Y") && record.OverrideVolume_Amt == null) || checkDate == true)
                {
                    ((Label)e.Row.FindControl("lblRowRed")).Visible = true;
                    ((Label)e.Row.FindControl("lblRowRedDisclaimer")).Visible = true;
                    ((Label)e.Row.FindControl("lblInfoMsg")).Visible = true;
                    ((Label)e.Row.FindControl("lblResolutionMsg")).Visible = true;
                    ((System.Web.UI.HtmlControls.HtmlTable)e.Row.FindControl("tblMainGrid")).Rows[0].BgColor = "#F7192E";

                    #region Row Red - Variance

                    if (((thisYearvol - priorYearvol) > (priorYearvol * (variance)) || (thisYearvol - priorYearvol) < (priorYearvol * (-variance))) && record.PriorYTDVol != null)
                    {
                        ((Label)e.Row.FindControl("lblRowRed")).Text = VPC.wrapLabel(Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.msgDataExceedsVariance.ToString(), 50);
                        ((Label)e.Row.FindControl("lblRowRedDisclaimer")).Text = VPC.wrapLabel(Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.resDataExceedsVariance.ToString(), 50);
                    }

                    #endregion

                    #region Row Red - Missing

                    if (record.Missing_Row.Contains("Y") && record.OverrideVolume_Amt == null)
                    {
                        ((Label)e.Row.FindControl("lblRowRed")).Text = VPC.wrapLabel(Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.msgProductMissing1.ToString() + record.DataSource.Trim().ToString() + Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.msgProductMissing2.ToString() + parent.monthNum + "/" + parent.yearNum + ".", 50);
                        string resProductMissing = VPC.wrapLabel(Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.resProductMissing.ToString(), 50);
                        ((Label)e.Row.FindControl("lblRowRedDisclaimer")).Text = VPC.AddEmailHyperlink(resProductMissing, "CorporateBusinessSystems@hess.com");
                    }

                    #endregion

                    #region Row Red - Value changed after it has been validated

                    if (Convert.ToDateTime(record.ReviewedOn_Dt) != DateTime.MinValue)
                    {
                        if (Convert.ToDateTime(record.ReviewedOn_Dt) < Convert.ToDateTime(record.ChangedOn_Dt))
                        {
                            ((Label)e.Row.FindControl("lblRowRed")).Text = VPC.wrapLabel(Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.msgPreviousValidationHasBeenChanged1.ToString() + record.ChangedBy_Name.Replace("IHESS\\", string.Empty).Trim() + " on " + Convert.ToDateTime(record.ChangedOn_Dt.ToString()).ToShortDateString() + Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.msgPreviousValidationHasBeenChanged2.ToString() + record.DataSource.Trim().ToString() + ".", 50);
                            ((Label)e.Row.FindControl("lblRowRedDisclaimer")).Text = VPC.wrapLabel(Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.resPreviousValidationHasBeenChanged.ToString(), 50);
                        }
                    }

                    #endregion

                    ((Panel)e.Row.FindControl("pnlRowRed")).BorderColor = System.Drawing.Color.FromName("#1f522c");
                    VPC.PanelStlying((Panel)e.Row.FindControl("pnlRowRed"));

                    RequiredFieldValidator rfvMonthVol = (RequiredFieldValidator)e.Row.FindControl("rfvMonthVol");
                    rfvMonthVol.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    ((Label)e.Row.FindControl("lblRowRed")).Visible = false;
                    ((Label)e.Row.FindControl("lblRowRedDisclaimer")).Visible = false;
                    ((Label)e.Row.FindControl("lblInfoMsg")).Visible = false;
                    ((Label)e.Row.FindControl("lblResolutionMsg")).Visible = false;
                }

                #endregion

                #region Set Notes img if there is a note entered

                this.SetLabelText((Label)e.Row.FindControl("lblComments"), VPC.wrapLabel(record.Comments.Trim(), 50));
                ((TextBox)e.Row.FindControl("txtComments")).Text = record.Comments.Trim();
                if (record.Comments.Trim() == string.Empty)
                {
                    ((LinkButton)e.Row.FindControl("imgNote")).Attributes.Add("class", "addNotes");
                    ((LinkButton)e.Row.FindControl("imgNote")).Attributes.Add("tipsy", "Click to add comments");
                }
                else
                {
                    ((LinkButton)e.Row.FindControl("imgNote")).Attributes.Add("class", "notes");
                    ((LinkButton)e.Row.FindControl("imgNote")).Attributes.Add("tipsy", "Click to edit comments");
                }

                #endregion

                #region Sets Override Volume if there is one

                if (record.OverrideVolume_Amt != null)
                    ((TextBox)e.Row.FindControl("txtThisMonthVol")).Text = VPC.AddCommas(record.OverrideVolume_Amt.ToString()).Trim();

                #endregion

                #region Sets Prior Year Month invisible unless it has a value

                if (record.PriorYearMonthVol == null)
                    ((Label)e.Row.FindControl("lblPriorYearMonthVol")).Visible = false;

                #endregion

                #region Sets the Cell Row Yellow if there is an overriden value

                if (record.OverrideVolume_Amt != null && !record.DataSource.Contains("MANUAL"))
                {
                    ((System.Web.UI.HtmlControls.HtmlTable)e.Row.FindControl("tblMainGrid")).Rows[0].Cells[7].BgColor = "#FFFF99";
                    string formattedEmissionVol_Amt = record.EmissionVolume_Amt == null ? "N/A" : Convert.ToDecimal(record.EmissionVolume_Amt.ToString().Trim()).ToString("#,##0 ;(#,##0)");
                    this.SetLabelText((Label)e.Row.FindControl("lblOverRideValue"), "Value changed from " + formattedEmissionVol_Amt + " to " + Convert.ToDecimal(record.OverrideVolume_Amt.ToString().Trim()).ToString("#,##0 ;(#,##0)") + " by " + record.ChangedBy_Name.Replace("IHESS\\", string.Empty).Trim() + " on " + record.ChangedOn_Dt.ToString("d"));
                    Label lblInfoMessagepnlOverRide = (Label)e.Row.FindControl("lblInfoMessagepnlOverRide");
                    lblInfoMessagepnlOverRide.Visible = true;
                    VPC.PanelStlying((Panel)e.Row.FindControl("pnlOverRideValue"));
                }

                #endregion

                #region Depress Collapse Panel on load

                ((CollapsiblePanelExtender)e.Row.FindControl("pnlDetailCollapsiblePanelExtender")).Collapsed = true;

                #endregion

                #region Puts the "I" value in dropdown if pulled from database

                if (((Label)e.Row.FindControl("lblValidated")).Text == "I")
                {
                    ddlValidated.Items.Insert(0, new ListItem("I", "I"));
                    ddlValidated.SelectedValue = "I";
                }

                #endregion

                #region Hides Product Name if it's UNKNOWN || Production Volume

                if (record.ProductName.Trim() == "UNKNOWN" || record.ProductName.Trim() == "PRODUCTION VOLUME")
                    ((Label)e.Row.FindControl("lblProductName")).Visible = false;

                #endregion
            }
        }

        #endregion

        #region Search Bar Click

        public void btnSave_Click(object sender, EventArgs e)
        {
            Session[saveRef] = "true";
            GHGvGridStd parent = (GHGvGridStd)this.Page;

            if (parent.lb25Visible == false)
            {
                foreach (GridViewRow gvr in StandardMasterGrid.Rows)
                {
                    if (gvr.RowIndex < 25)
                    {
                        TextBox txtThisMonthVol = (TextBox)gvr.FindControl("txtThisMonthVol"),
                            txtComments = (TextBox)gvr.FindControl("txtComments");
                        Label rowKey = (Label)gvr.FindControl("lblDMPKEmissions");
                        DropDownList ddlValidated = (DropDownList)gvr.FindControl("ddlValidated");

                        UpdateAndSaveRow(txtThisMonthVol.Text, txtComments.Text, ddlValidated.SelectedValue, Convert.ToInt32(rowKey.Text), true);
                    }
                }
                hfRefreshOffSearchCriteria.Value = "true";
            }
            else if (parent.lb25Visible == true)
            {
                int rowCount = hfRowCount.Value.Length == 0 ? 0 : Convert.ToInt32(hfRowCount.Value);
                string vid = hfVID.Value,
                    monthlyVolume = hfMonthlyVolume.Value,
                    comments = hfComments.Value,
                    key = hfRowKey.Value;

                for (int i = 0; i < rowCount; i++)
                {
                    string rowVID = vid.Split('|')[i],
                        rowMonthlyValue = monthlyVolume.Split('|')[i],
                        rowComments = comments.Split('|')[i],
                        rowKey = key.Split('|')[i];

                    UpdateAndSaveRow(rowMonthlyValue, rowComments, rowVID, Convert.ToInt32(rowKey), true);

                    hfVID.Value = string.Empty;
                    hfMonthlyVolume.Value = string.Empty;
                    hfComments.Value = string.Empty;
                    hfRowKey.Value = string.Empty;
                }
            }
            StandardMasterGrid.DataBind();
            Session[saveRef] = "false";
        }

        public void btnRefresh_Click(object sender, EventArgs e)
        {
            StandardMasterGrid.DataBind();
        }

        public void btnView_Click(object sender, EventArgs e)
        {
            SetGridViewDirty(false);
        }

        #endregion

        #region Misc

        private void SetGridViewDirty(bool save)
        {
            foreach (GridViewRow row in StandardMasterGrid.Rows)
            {
                Label rowKey = (Label)row.FindControl("lblDMPKEmissions");
                TextBox txtThisMonthVol = (TextBox)row.FindControl("txtThisMonthVol"),
                    txtComments = (TextBox)row.FindControl("txtComments");
                DropDownList ddlValidated = (DropDownList)row.FindControl("ddlValidated");

                UpdateAndSaveRow(txtThisMonthVol.Text, txtComments.Text, ddlValidated.SelectedValue, Convert.ToInt32(rowKey.Text), save);
            }
        }

        private void UpdateAndSaveRow(string rowMonthlyValue, string rowComments, string rowVID, int rowKey, bool save)
        {
            GHGvGridStd parent = (GHGvGridStd)this.Page;
            StandardValidation collection = (StandardValidation)Session[GridStd];
            StandardValidationRecord collectionItem = collection.Find(Convert.ToInt32(rowKey));
            string currentUser = HttpContext.Current.User.Identity.Name.ToString();
            rowMonthlyValue = rowMonthlyValue.Replace(",", string.Empty);

            if (collectionItem == null) return;

            if ((rowMonthlyValue.Length > 0) || (rowMonthlyValue.Length == 0 || collectionItem.ThisMonthVol.ToString().Length > 0))
            {
                #region Adds ChangeBy_Name & OverrideVolume_Amt if text has been changed

                if (collectionItem.ThisMonthVol.ToString().Trim() != rowMonthlyValue)
                {
                    decimal outputNum;
                    bool isNum = decimal.TryParse(rowMonthlyValue, out outputNum);

                    if (isNum)
                    {
                        collectionItem.OverrideVolume_Amt = rowMonthlyValue == string.Empty ? 0 : Convert.ToDecimal(rowMonthlyValue);
                        collectionItem.ChangedBy_Name = currentUser;
                    }
                    else if (rowMonthlyValue == string.Empty)
                    {
                        collectionItem.OverrideVolume_Amt = null;
                        collectionItem.ChangedBy_Name = currentUser;
                    }

                    collectionItem.SentToVendor = null;
                }

                #endregion

                #region Adds Comments if comments are filled out

                if (rowComments.Length > 0)
                    collectionItem.Comments = rowComments;

                #endregion

                #region Adds ReviewedBy_Name & Validated if validation was changed

                if (!(collectionItem.Validated == rowVID))
                {
                    collectionItem.Validated = rowVID;
                    if (rowVID == "N")
                        collectionItem.ReviewedBy_Name = "";
                    else
                        collectionItem.ReviewedBy_Name = currentUser;
                }

                #endregion

                #region Logic for .ALL record Update

                if (collectionItem.AssetName.Contains(".ALL"))
                {
                    StandardValidation sv = StandardValidation.GetStdMaster(parent.yearNum, parent.monthNum, parent.facName.Replace(" - .ALL", string.Empty).Replace("STATION:", "RETAIL STATE:"));

                    var updateItems = (from c in sv
                                       where c.SourceCategory == collectionItem.SourceCategory
                                       select c);

                    if (rowVID == "Y")
                    {
                        foreach (StandardValidationRecord svr in updateItems)
                        {
                            if (svr.ThisMonthVol == null)
                                error = error + 1;
                        }
                    }

                    if (error == 0)
                    {
                        foreach (StandardValidationRecord svr in updateItems)
                        {
                            if (svr.Validated != collectionItem.Validated)
                            {
                                if ((svr.Validated == "I" || svr.Validated == "X") && collection.Validated != "Y")
                                    return;

                                svr.Validated = collectionItem.Validated;
                                if (collectionItem.Validated == "N")
                                    svr.ReviewedBy_Name = "";
                                else
                                    svr.ReviewedBy_Name = currentUser;

                                svr.Save();
                            }
                        }
                    }
                    goto Finish;
                }

                #endregion

                if (save)
                    collectionItem.Save();
            }
        Finish: return;
        }

        #endregion
                
        #region Sort Clicks

        protected void btnValidated_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpValidated", "sortDownValidated", "Validated");
        }

        protected void btnSourceCategory_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpSourceCategory", "sortDownSourceCategory", "SourceCategory");
        }

        protected void btnAssetName_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpAssetName", "sortDownAssetName", "AssetNameFull");
        }

        protected void btnProductName_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpProductName", "sortDownProductName", "ProductName");
        }

        protected void btnThisYTDVol_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpThisYTDVol", "sortDownThisYTDVol", "ThisYTDVol");
        }

        protected void btnPriorYTDVol_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpPriorYTDVol", "sortDownPriorYTDVol", "PriorYTDVol");
        }

        protected void btnThisMonthVol_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpThisMonthVol", "sortDownThisMonthVol", "ThisMonthVol");
        }

        protected void btnPriorYearMonthVol_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpPriorYearMonthVol", "sortDownPriorYearMonthVol", "PriorYearMonthVol");
        }

        protected void btnUOMshort_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpUOMshort", "sortDownUOMshort", "UOMshort");
        }

        protected void btnDataSource_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpDataSource", "sortDownDataSource", "DataSource");
        }

        protected void btnNotes_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpNotes", "sortDownNotes", "Comments");
        }

        protected void SortColumn(string sortUpControlName, string sortDownControlName, string sortColumnName)
        {
            SetGridViewDirty(false);

            StandardValidation sv = (StandardValidation)Session[GridStd];
            if (sv.SortDirection == ListSortDirection.Ascending)
                ColumnSortDirection(sortUpControlName, sortColumnName, ListSortDirection.Descending);
            else
                ColumnSortDirection(sortDownControlName, sortColumnName, ListSortDirection.Ascending);
        }

        protected void ColumnSortDirection(string sortControlName, string sortColumnName, ListSortDirection direction)
        {
            hfSort.Value = sortControlName;
            object businessObject = CurrentBusinessObject;
            base.Sort(ref businessObject, sortColumnName, direction);
            StandardMasterGrid.DataBind();
            GHGvGridStd parent = (GHGvGridStd)this.Page;
            parent.setPageSize();
        }

        #endregion
    }
}
