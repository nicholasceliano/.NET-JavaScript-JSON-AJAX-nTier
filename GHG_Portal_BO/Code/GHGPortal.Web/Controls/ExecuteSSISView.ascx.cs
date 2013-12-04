using System;
using Hess.Corporate.GHGPortal.Business;
using AjaxControlToolkit;
using System.Data;
using System.Web.UI.WebControls;

namespace Hess.Corporate.GHGPortal.Web.UI
{
    public partial class ExecuteSSISView : CommonGridViewControl
    {
        #region Method Overrides

        protected override object CurrentBusinessObject
        {
            get
            {
                IBusinessObjects businessObject = (IBusinessObjects)this.Session["Packages"];
                bool refreshBO = (businessObject == null || !(businessObject is Packages));
                
                if (!refreshBO)
                    refreshBO = true;

                if (refreshBO)
                {
                    Session[conBusinessObjectName] = null;
                    businessObject = (IBusinessObjects)this.GetBusinessObject(CommonGridViewControl.conBusinessObjectName, typeof(Packages));

                    // Get new object and save it in Session
                    this.Session["Packages"] = businessObject;
                }

                if (businessObject == null || businessObject.Count == 0)
                    businessObject = new Packages();

                return businessObject;
            }
        }

        protected override void GridViewRowDataBound(object sender, GridViewRowEventArgs e)
        {
            base.GridViewRowDataBound(sender, e);

            #region Change Header Text
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (hfSort.Value.Length > 0)
                    ((Image)e.Row.FindControl(hfSort.Value)).Visible = true;
            }
            #endregion

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Package record = (Package)e.Row.DataItem;
                this.SetLabelText((Label)e.Row.FindControl("lblPackageName"), record.Package_Name.ToString().Trim() != "" ? record.Package_Name.ToString().Trim() : " ");
                ((TextBox)e.Row.FindControl("txtStartRange")).Text = record.Start_Date != DateTime.MinValue ? record.Start_Date.ToString("MM/dd/yyyy") : "";
                ((TextBox)e.Row.FindControl("txtEndRange")).Text = record.End_Date != DateTime.MinValue ? record.End_Date.ToString("MM/dd/yyyy") : "";
                this.SetLabelText((Label)e.Row.FindControl("lblDateProcess"), record.DateProcess_Name.Trim() != "" ? record.DateProcess_Name.Trim() : " ");
                ((CheckBox)e.Row.FindControl("chkProcessPackage")).Checked = record.ProcessPack_Ind == "Y" ? true : false;
                ((CheckBox)e.Row.FindControl("chkClearTables")).Checked = record.ClearSupp_Ind == "Y" ? true : false;
                
                #region Dropdownlist Databind
                
                DropDownList ddlClearPrevPackage = (DropDownList)e.Row.FindControl("ddlClearPrevPackage");

                if(((Label)e.Row.FindControl("lblPackageName")).Text.Contains("sendToEnviance"))
                    ddlClearPrevPackage.Visible = false;
                else
                {
                    PackageRuns pkgRuns = PackageRuns.GetRuns(record.Package_Name);
                    ddlClearPrevPackage.DataSource = pkgRuns;
                    ddlClearPrevPackage.DataTextField = "Run_Date";
                    ddlClearPrevPackage.DataValueField = "DMPK_PackageExecution";
                    ddlClearPrevPackage.DataBind();

                    if (ddlClearPrevPackage.Items.Count > 0)
                        ddlClearPrevPackage.Items.Insert(0, new ListItem("Select Date...", string.Empty));

                    if (record.DMPK_PackageExecution > 0)
                        ddlClearPrevPackage.SelectedValue = record.DMPK_PackageExecution.ToString();

                    if ((((CheckBox)e.Row.FindControl("chkClearTables")).Checked == false) && (((CheckBox)e.Row.FindControl("chkProcessPackage")).Checked == false) && (record.Package_Name.Contains("sendToEnviance")) && !(ddlClearPrevPackage.SelectedItem.ToString() == "Select Date..."))
                    {
                        ((Label)e.Row.FindControl("lblDateProcess")).Visible = false;
                        ((LinkButton)e.Row.FindControl("lnkbtnDateProcess")).Text = record.DateProcess_Name.Trim();
                        ((LinkButton)e.Row.FindControl("lnkbtnDateProcess")).Visible = true;
                    }
                }

                #endregion
            }
        }

        #endregion

        #region Gird Management

        public void ApplyStartEndDate(DateTime startDate, DateTime endDate)
        {
            foreach (GridViewRow row in ExecuteSSISGrid.Rows)
            {
                string key = ExecuteSSISGrid.DataKeys[row.RowIndex]["DMPK_ExecutionRun"].ToString();
                Packages collection = (Packages)CurrentBusinessObject;
                Package record = (Package)collection.Find(key);
                
                record.Start_Date = startDate;
                record.End_Date = endDate;
                record.Save();
            }
            ExecuteSSISGrid.DataBind();
        }

        public void UpdateProcessPackage(string ProcessPackage_Ind)
        {
            foreach (GridViewRow row in ExecuteSSISGrid.Rows)
            {
                string key = ExecuteSSISGrid.DataKeys[row.RowIndex]["DMPK_ExecutionRun"].ToString();
                Packages collection = (Packages)CurrentBusinessObject;
                Package record = (Package)collection.Find(key);

                record.ProcessPack_Ind = ProcessPackage_Ind;
                record.Save();
            }
            ExecuteSSISGrid.DataBind();
        }

        public void UpdateClearTables(string ClearTables_Ind)
        {
            foreach (GridViewRow row in ExecuteSSISGrid.Rows)
            {
                string key = ExecuteSSISGrid.DataKeys[row.RowIndex]["DMPK_ExecutionRun"].ToString();
                Packages collection = (Packages)CurrentBusinessObject;
                Package record = (Package)collection.Find(key);

                record.ClearSupp_Ind = ClearTables_Ind;
                record.Save();
            }
            ExecuteSSISGrid.DataBind();
        }

        public void SaveAllVariables(bool ExecuteMasterController)
        {
            foreach (GridViewRow row in ExecuteSSISGrid.Rows)
            {
                string key = ExecuteSSISGrid.DataKeys[row.RowIndex]["DMPK_ExecutionRun"].ToString();
                Packages collection = (Packages)CurrentBusinessObject;
                Package record = (Package)collection.Find(key);

                record.Start_Date = ((TextBox)row.FindControl("txtStartRange")).Text.Length > 0 ? Convert.ToDateTime(((TextBox)row.FindControl("txtStartRange")).Text) : DateTime.MinValue;
                record.End_Date = ((TextBox)row.FindControl("txtEndRange")).Text.Length > 0 ? Convert.ToDateTime(((TextBox)row.FindControl("txtEndRange")).Text) : DateTime.MinValue;
                record.ProcessPack_Ind = ((CheckBox)row.FindControl("chkProcessPackage")).Checked ? "Y" : "N";
                record.ClearSupp_Ind = ((CheckBox)row.FindControl("chkClearTables")).Checked ? "Y" : "N";
                record.DMPK_PackageExecution = ((DropDownList)row.FindControl("ddlClearPrevPackage")).SelectedValue != string.Empty ? Convert.ToInt32(((DropDownList)row.FindControl("ddlClearPrevPackage")).SelectedValue) : 0;
                record.Save();
            }

            if (ExecuteMasterController)
                new Data.DataAccess().ExecuteStoredProcedure("prcVPadmExecuteSSISExecuteMasterController");

            ExecuteSSISGrid.DataBind();
        }

        public void lnkbtnDateProcess_OnClick(object sender, EventArgs e)
        {
            LinkButton lnkbtnDateProcess = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtnDateProcess.NamingContainer;
            DropDownList ddlClearPrevPackage = (DropDownList)ExecuteSSISGrid.Rows[row.RowIndex].FindControl("ddlClearPrevPackage");
            DateTime date = Convert.ToDateTime(ddlClearPrevPackage.SelectedItem.ToString());
            string startDate = Convert.ToString(date.AddHours(-12)),
                endDate = Convert.ToString(date.AddHours(12));
            
            Data.DataAccess da = new Data.DataAccess();
            da.ExecuteStoredProcedure("prcVPadmExecuteSSISupdateResetSentToVendorDate", startDate, endDate);
        }

        #endregion
    }
}
