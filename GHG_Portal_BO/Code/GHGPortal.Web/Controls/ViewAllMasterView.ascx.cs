using System;
using System.Web.UI.WebControls;
using Hess.Corporate.GHGPortal.Business;
using System.ComponentModel;
using AjaxControlToolkit;

namespace Hess.Corporate.GHGPortal.Web.UI
{
    public partial class ViewAllMasterView : CommonGridViewControl
    {
        #region Private Constants

        public int error = 0;
        private string GridViewAll = "GridViewAll";
        private string GirdStdSearch = "GridStdSearch";
        private string saveRef = "saveRefresh";

        #endregion

        #region Method Overrides

        protected override object CurrentBusinessObject
        {
            get
            {
                IBusinessObjects businessObject = (IBusinessObjects)this.Session[GridViewAll];
                bool refreshBO = (businessObject == null || !(businessObject is ViewAllValidation));
                string sc = (string)Session[GirdStdSearch];
                bool refreshOffSearchCriteria = true;

                if (!refreshBO)
                {
                    string saveRefresh = (string)Session[saveRef];
                    ViewAllValidation sv = (ViewAllValidation)businessObject;
                    if (!(saveRefresh == null))
                    {
                        if (saveRefresh.TrimEnd() == "true")
                            refreshBO = true;
                    }

                    if (!(sc == null))
                    {
                        if (sv.Entity.Trim() != sc.Split('~')[1] || sv.Facility.Trim() != sc.Split('~')[3] || sv.SourceCategory.Trim() != sc.Split('~')[5] || sv.YearNumber != Convert.ToInt32(sc.Split('~')[7]) || sv.MonthNumber != Convert.ToInt32(sc.Split('~')[9]) || sv.AssetName.Trim() != sc.Split('~')[11] || sv.DataSource.Trim() != sc.Split('~')[13] || sv.AssetType.Trim() != sc.Split('~')[15] || sv.Validated.Trim() != sc.Split('~')[17])
                            refreshBO = true;
                    }
                }

                if (refreshBO)
                {
                    try
                    {
                        Session[conBusinessObjectName] = null;
                        if (refreshOffSearchCriteria == true)
                        {
                            businessObject = (IBusinessObjects)this.GetBusinessObject(CommonGridViewControl.conBusinessObjectName, typeof(ViewAllValidation),
                                Convert.ToInt32(sc.Split('~')[7]), Convert.ToInt32(sc.Split('~')[9]), sc.Split('~')[1], sc.Split('~')[3], sc.Split('~')[5], sc.Split('~')[11], sc.Split('~')[13], sc.Split('~')[15], sc.Split('~')[17]);
                        }

                        // Get new object and save it in Session
                        businessObject.Sort("RowId", ListSortDirection.Descending);
                        this.Session[GridViewAll] = businessObject;
                    }
                    catch { }
                }

                if (businessObject == null || businessObject.Count == 0)
                    businessObject = new ViewAllValidation();

                return businessObject;
            }
        }

        protected override void GridViewRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region Change Header Text to Specific Mo/Yr

            if (e.Row.RowType == DataControlRowType.Header)
            {
                TextBox txtMonthYear = (TextBox)this.Parent.FindControl("txtMonthYear");
                string searchDate = txtMonthYear.Text.Split('/')[0] + "/1/" + txtMonthYear.Text.Split('/')[1];
                DateTime date = Convert.ToDateTime(searchDate);
                ((LinkButton)e.Row.FindControl("btnThisYTDVol")).Text = date.Year.ToString() + " YTD";
                ((LinkButton)e.Row.FindControl("btnPriorYTDVol")).Text = date.AddYears(-1).Year.ToString() + " YTD";
                ((LinkButton)e.Row.FindControl("btnThisMonthVol")).Text = date.ToString("MMM") + " " + date.Year.ToString();
                ((LinkButton)e.Row.FindControl("btnPriorYearMonthVol")).Text = date.ToString("MMM") + " " + date.AddYears(-1).Year.ToString();

                if (hfSort.Value.Length > 0)
                    ((Image)e.Row.FindControl(hfSort.Value)).Visible = true;
            }

            #endregion

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ViewAllValidationRecord record = (ViewAllValidationRecord)e.Row.DataItem;

                //Sets the selected dropdown value
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
                this.SetLabelText((Label)e.Row.FindControl("lblProductName"), record.ProductName.Trim() != "" ? record.ProductName.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblMeterId"), record.MeterID != "0" ? record.MeterID : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblThisYTDvol"), record.ThisYTDVol != null ? VPC.AddCommas(record.ThisYTDVol.ToString()) : null);
                this.SetLabelText((Label)e.Row.FindControl("lblPriorYTDvol"), record.PriorYTDVol != null ? VPC.AddCommas(record.PriorYTDVol.ToString()) : null);
                this.SetLabelText((Label)e.Row.FindControl("lblThisMonthVol"), record.ThisMonthVol != null ? VPC.AddCommas(record.ThisMonthVol.ToString()) : null);
                this.SetLabelText((Label)e.Row.FindControl("lblPriorYearMonthVol"), record.PriorYearMonthVol != null ? VPC.AddCommas(record.PriorYearMonthVol.ToString()) : null);
                this.SetLabelText((Label)e.Row.FindControl("lblUOMshort"), record.UOMshort.Trim());
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

                if (((thisYearvol - priorYearvol) > (priorYearvol * (variance)) || (thisYearvol - priorYearvol) < (priorYearvol * (-variance))) && record.PriorYTDVol != null)
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

                    ((Panel)e.Row.FindControl("pnlRowRed")).BorderColor = System.Drawing.Color.FromName("#1f522c");
                    VPC.PanelStlying((Panel)e.Row.FindControl("pnlRowRed"));
                }
                else
                {
                    ((Label)e.Row.FindControl("lblRowRed")).Visible = false;
                    ((Label)e.Row.FindControl("lblRowRedDisclaimer")).Visible = false;
                    ((Label)e.Row.FindControl("lblInfoMsg")).Visible = false;
                    ((Label)e.Row.FindControl("lblResolutionMsg")).Visible = false;
                }

                #endregion

                #region Sets Override Volume if there is one

                if (record.OverrideVolume_Amt != null)
                    this.SetLabelText((Label)e.Row.FindControl("lblThisMonthVol"), VPC.AddCommas(record.OverrideVolume_Amt.ToString()).Trim());

                #endregion

                #region Makes Monthly Value Invisible if 0

                if (record.ThisMonthVol == null && record.DataSource.Contains("MANUAL"))
                    ((Label)e.Row.FindControl("lblThisMonthVol")).Visible = false;

                #endregion

                #region Sets Prior Year Month invisible unless it has a value

                if (record.PriorYearMonthVol == null)
                    ((Label)e.Row.FindControl("lblPriorYearMonthVol")).Visible = false;

                #endregion

                #region Depress Collapse Panel on load

                ((CollapsiblePanelExtender)e.Row.FindControl("pnlDetailCollapsiblePanelExtender")).Collapsed = true;

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
            ViewAllMasterGrid.DataBind();
            Session[saveRef] = "false";
        }

        public void btnRefresh_Click(object sender, EventArgs e)
        {
            ViewAllMasterGrid.DataBind();
        }

        public void btnView_Click(object sender, EventArgs e)
        {
            SetGridViewDirty((ViewAllValidation)Session[GridViewAll]);
        }

        #endregion

        #region Misc
        
        private void SetGridViewDirty(ViewAllValidation sv)
        {
            foreach (GridViewRow row in ViewAllMasterGrid.Rows)
            {
                DropDownList ddlValidated = (DropDownList)row.FindControl("ddlValidated");
                ViewAllValidationRecord rec = sv.Find(ViewAllMasterGrid.DataKeys[row.RowIndex]["RowId"].ToString());
                rec.Validated = ddlValidated.SelectedValue;
            }
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

        protected void SortColumn(string sortUpControlName, string sortDownControlName, string sortColumnName)
        {
            ViewAllValidation sv = (ViewAllValidation)Session[GridViewAll];
            SetGridViewDirty(sv);

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
            ViewAllMasterGrid.DataBind();
            GHGvGridStd parent = (GHGvGridStd)this.Page;
            parent.setPageSize();
        }

        #endregion
    }
}
