using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hess.Corporate.GHGPortal.Business;
using System.ComponentModel;
using System.Collections.Generic;

namespace Hess.Corporate.GHGPortal.Web.UI
{
    public partial class VesselMasterView : CommonGridViewControl
    {
        #region Private Constants

        private string gridVessel = "GridVessel";
        private string GirdStdSearch = "GridStdSearch";
        private string saveRef = "saveRefresh";

        #endregion

        #region Method Overrides

        protected override object CurrentBusinessObject
        {
            get
            {
                IBusinessObjects businessObject = (IBusinessObjects)this.Session[gridVessel];
                bool refreshBO = (businessObject == null || !(businessObject is VesselValidation));
                string searchCriteria = (string)Session[GirdStdSearch];
                bool refreshOffSearchCriteria = true;

                if (!refreshBO)
                {
                    string saveRefresh = (string)Session[saveRef];
                    VesselValidation sv = (VesselValidation)businessObject;
                    if (!(saveRefresh == null))
                    {
                        if (saveRefresh.TrimEnd() == "true")
                            refreshBO = true;
                    }

                    if (!(searchCriteria == null))
                    {
                        if (sv.Entity.Trim() != searchCriteria.Split('~')[1] || sv.Facility.Trim() != searchCriteria.Split('~')[3] || sv.YearNumber != Convert.ToInt32(searchCriteria.Split('~')[7]) || sv.MonthNumber != Convert.ToInt32(searchCriteria.Split('~')[9]) || sv.VesselName.Trim() != searchCriteria.Split('~')[11] || sv.Validated.Trim() != searchCriteria.Split('~')[13])
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
                            businessObject = (IBusinessObjects)this.GetBusinessObject(CommonGridViewControl.conBusinessObjectName, typeof(VesselValidation),
                                searchCriteria.Split('~')[1], searchCriteria.Split('~')[3], Convert.ToInt32(searchCriteria.Split('~')[7]), Convert.ToInt32(searchCriteria.Split('~')[9]), searchCriteria.Split('~')[11], searchCriteria.Split('~')[13]);
                        }
                        this.Session[gridVessel] = businessObject;
                    }
                    catch { }
                }

                if (businessObject == null || businessObject.Count == 0)
                    businessObject = new VesselValidation();

                return businessObject;
            }
        }

        protected override void GridViewRowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region Change Header Text

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (hfSort.Value.Length > 0)
                    ((Image)e.Row.FindControl(hfSort.Value)).Visible = true;
            }

            #endregion

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GHGvGridVessel parent = (GHGvGridVessel)this.Page;
                VesselValidationRecord record = (VesselValidationRecord)e.Row.DataItem;

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

                this.SetLabelText((Label)e.Row.FindControl("lblValidated"), record.Validated.Trim() != "" ? record.Validated.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblMoveNum"), record.MoveNum == null || record.MoveNum == 0 ? null : record.MoveNum.ToString() + "-" + record.SegmentNum.ToString());
                this.SetLabelText((Label)e.Row.FindControl("lblRefMove"), record.RefMove == null || record.RefMove == 0 ? null : record.RefMove.ToString());
                this.SetLabelText((Label)e.Row.FindControl("lblIR"), record.IR.Trim() != "" ? record.IR.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblStatus"), record.Status.Trim() != "" ? record.Status.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblDisport"), record.Disport.Trim() != "" ? record.Disport.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblDisDate"), record.DisportDate.ToString().Trim() != "" ? record.DisportDate.ToString("MMM d yyyy").Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblVesselName"), record.VesselName.Trim() != "" ? record.VesselName.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblHessPN"), record.HPName.Trim() != "" ? record.HPName.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblEPAPN"), record.EPAName.Trim() != "" ? record.EPAName.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblTotDisVol"), record.DischargeVol != null ? VPC.AddCommas(record.DischargeVol.ToString()) : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblBLADESVol"), VPC.AddCommas(record.Source_Volume.ToString()) != "" ? VPC.AddCommas(record.Source_Volume.ToString()) : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblUOM"), record.Source_UOM.Trim() != "" ? record.Source_UOM.Trim() : " ");
                ((Label)e.Row.FindControl("lblStatus")).Attributes.Add("tipsy", record.Status.ToString() == "A" ? "Activated" : "Pending");
                ((Label)e.Row.FindControl("lblBLADESVol")).ForeColor = System.Drawing.Color.Blue;
                ((Label)e.Row.FindControl("lblBLADESVol")).Attributes.Add("onmouseover", "this.style.textDecoration='underline'");
                ((Panel)e.Row.FindControl("pnlBulkOrder")).BorderColor = System.Drawing.Color.FromName("#1f522c");
                ((Label)e.Row.FindControl("lblBLADESVol")).Attributes.Add("bulkOrderNo", record.BulkOrderNo);

                #region Sets MoveNum HyperLink

                ((Label)e.Row.FindControl("lblMoveNum")).ForeColor = System.Drawing.Color.Blue;
                ((Label)e.Row.FindControl("lblMoveNum")).Attributes.Add("onclick", "window.open('http://stt.ihess.com/blades/FCMoveMaintenance.aspx?TranNO=" + record.MoveNum.ToString() + "')");

                #endregion

                #region Sets RefMove HyperLink

                ((Label)e.Row.FindControl("lblRefMove")).ForeColor = System.Drawing.Color.Blue;
                ((Label)e.Row.FindControl("lblRefMove")).Attributes.Add("onclick", "window.open('http://stt.ihess.com/blades/FCMoveMaintenance.aspx?TranNO=" + record.RefMove.ToString() + "')");

                #endregion

                #region Turn Rows Red

                if (record.Validated == "I" || record.MissingRow == "Y")
                {
                    ((Label)e.Row.FindControl("lblRowRed")).Visible = true;
                    ((Label)e.Row.FindControl("lblRowRedDisclaimer")).Visible = true;
                    ((Label)e.Row.FindControl("lblInfoMsg")).Visible = true;
                    ((Label)e.Row.FindControl("lblResolutionMsg")).Visible = true;
                    ((System.Web.UI.HtmlControls.HtmlTable)e.Row.FindControl("tblMainGrid")).Rows[0].BgColor = "#F7192E";

                    if (record.Validated == "I")
                    {
                        ((Label)e.Row.FindControl("lblRowRed")).Text = VPC.wrapLabel(Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.msgPreviousValidationHasBeenChanged1.ToString() + record.ChangedBy_Name.Replace("IHESS\\", string.Empty).Trim() + " on " + Convert.ToDateTime(record.ChangeOn_Date.ToString()).ToShortDateString() + Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.msgPreviousValidationHasBeenChanged2.ToString() + record.DataSource.Trim().ToString() + ".", 50);
                        ((Label)e.Row.FindControl("lblRowRedDisclaimer")).Text = VPC.wrapLabel(Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.resPreviousValidationHasBeenChanged.ToString(), 50);
                    }

                    if (record.MissingRow == "Y")
                    {
                        ((Label)e.Row.FindControl("lblRowRed")).Text = VPC.wrapLabel(Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.msgBLADESEstimatedVol, 50);
                        ((Label)e.Row.FindControl("lblRowRedDisclaimer")).Text = VPC.wrapLabel(Hess.Corporate.GHGPortal.Configuration.AppConfiguration.Current.resBLADESEstimatedVol.ToString(), 50);
                    }

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

                #region Set Datasource to from Manual Portal to Manual

                if (record.DataSource.Trim() == "MANUAL PORTAL")
                    this.SetLabelText((Label)e.Row.FindControl("lblDataSource"), "MANUAL");
                else
                    this.SetLabelText((Label)e.Row.FindControl("lblDataSource"), record.DataSource.Trim());

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

                #region Puts the "I" value in dropdown if pulled from database

                if (((Label)e.Row.FindControl("lblValidated")).Text == "I")
                {
                    ddlValidated.Items.Insert(0, new ListItem("I", "I"));
                    ddlValidated.SelectedValue = "I";
                }

                #endregion

                #region Set Panel comments background color

                string rowIndex = e.Row.RowIndex.ToString();
                int valueLast = Convert.ToInt32(rowIndex.Substring(rowIndex.Length - 1, 1));

                if (valueLast == 1 || valueLast == 3 || valueLast == 5 || valueLast == 7 || valueLast == 9)
                    ((Panel)e.Row.FindControl("pnlComments")).BackColor = System.Drawing.Color.FromName("#E9F5ED");

                #endregion

                #region Display Inspection Report Icon

                int[] moveNumber = new int[1];
                moveNumber[0] = Convert.ToInt32(record.MoveNum);

                Dictionary<string, int> collection = (Documents.GetDocumentCountForMoves(moveNumber, "Qual"));

                int documentCount = 0;
                if (collection.Count > 0)
                    documentCount = collection[record.MoveNum.ToString()];

                if (documentCount > 0)
                {
                    LinkButton imgIR = (LinkButton)e.Row.FindControl("imgIR");

                    imgIR.Style.Remove(HtmlTextWriterStyle.Display);
                    imgIR.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                    imgIR.Attributes.Add("tipsy", "Inspection Reports Available");
                }

                #endregion
            }
        }

        #endregion

        #region Search Bar Click

        public void btnSave_Click(object sender, EventArgs e)
        {
            Session[saveRef] = "true";
            SetGridViewDirty((VesselValidation)Session[gridVessel], true);
            VesselMasterGrid.DataBind();
            Session[saveRef] = "false";
        }

        public void btnRefresh_Click(object sender, EventArgs e)
        {
            VesselMasterGrid.DataBind();
        }

        public void btnView_Click(object sender, EventArgs e)
        {
            SetGridViewDirty((VesselValidation)Session[gridVessel], false);
        }

        #endregion

        #region Misc

        private void  SetGridViewDirty(VesselValidation sv, bool save)
        {
            foreach (GridViewRow row in VesselMasterGrid.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                    UpdateAndSaveRow(row, sv, save);
            }
        }

        private void UpdateAndSaveRow(GridViewRow row, VesselValidation collection, bool save)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string currentUser = HttpContext.Current.User.Identity.Name.ToString(),
                    key = VesselMasterGrid.DataKeys[row.RowIndex]["RowId"].ToString();
                Label lblValidated = (Label)row.FindControl("lblValidated"),
                    lblComments = (Label)row.FindControl("lblComments");
                DropDownList ddlValidated = (DropDownList)row.FindControl("ddlValidated");
                TextBox txtComments = (TextBox)row.FindControl("txtComments");
                VesselValidationRecord collectionItem = collection.Find(key);
                if (collectionItem == null) return;

                #region Adds Comments if comments are filled out

                if (txtComments.Text.Length > 0)
                {
                    collectionItem.Comments = txtComments.Text;
                    collectionItem.ChangedBy_Name = currentUser;
                }

                #endregion

                #region Adds ReviewedBy_Name & Validated if validation was changed

                if (!(collectionItem.Validated == ddlValidated.SelectedItem.Text))
                {
                    if (ddlValidated.SelectedItem.Text.Trim() == "N")
                    {
                        collectionItem.Validated = ddlValidated.SelectedItem.Text;
                        collectionItem.ReviewedBy_Name = null;
                    }
                    else
                    {
                        collectionItem.Validated = ddlValidated.SelectedItem.Text;
                        collectionItem.ReviewedBy_Name = currentUser;
                        collectionItem.ChangedBy_Name = currentUser;
                    }
                }

                #endregion

                if (save)
                    collectionItem.Save();
            }
        }

        #endregion

        #region Sort Clicks

        protected void btnValidated_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpValidated", "sortDownValidated", "Validated");
        }

        protected void btnMoveNum_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpMoveNum", "sortDownMoveNum", "MoveNum");
        }

        protected void btnRefMove_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpRefMove", "sortDownRefMove", "RefMove");
        }

        protected void btnIR_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpIR", "sortDownIR", "IR");
        }

        protected void btnStatus_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpStatus", "sortDownStatus", "Status");
        }

        protected void btnDisport_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpDisport", "sortDownDisport", "Disport");
        }
        
        protected void btnDisDate_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpDisDate", "sortDownDisDate", "DisportDate");
        }

        protected void btnVesselName_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpVesselName", "sortDownVesselName", "VesselName");
        }

        protected void btnHessPN_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpHessPN", "sortDownHessPN", "HPName");
        }

        protected void btnEPAPN_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpEPAPN", "sortDownEPAPN", "EPAName");
        }

        protected void btnTotDisVol_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpTotDisVol", "sortDownTotDisVol", "DischargeVol");
        }

        protected void btnBLADESVol_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpBLADESVol", "sortDownBLADESVol", "Source_Volume");
        }

        protected void btnUOM_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpUOM", "sortDownUOM", "UOM");
        }

        protected void btnNotes_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpNotes", "sortDownNotes", "Comments");
        }

        protected void SortColumn(string sortUpControlName, string sortDownControlName, string sortColumnName)
        {
            VesselValidation sv = (VesselValidation)Session[gridVessel];
            SetGridViewDirty(sv, false);

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
            VesselMasterGrid.DataBind();
            GHGvGridVessel parent = (GHGvGridVessel)this.Page;
            parent.setPageSize();
        }

        #endregion
    }
}