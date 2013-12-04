using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hess.Corporate.GHGPortal.Business;

namespace Hess.Corporate.GHGPortal.Web.UI
{
    public partial class GHGvGridStd : System.Web.UI.Page
    {
        #region Private Constants
        
        private string GridStdSearch = "GridStdSearch";
        private string stdTitle = "Validation Portal - Main";
        private string viewAllTitle = "Validation Portal - All";

        #endregion

        #region Accessors for Search controls

            public string facName { get { return ddlFacilities.SelectedIndex != -1 && ddlFacilities.SelectedItem.Text.Trim() != "" ? ddlFacilities.SelectedItem.Text.Trim() : string.Empty; } }
            public int yearNum { get { return txtMonthYear.Text.Length > 0 ? Convert.ToInt32(txtMonthYear.Text.Split('/')[1]) : 0;} }
            public int monthNum { get { return txtMonthYear.Text.Length > 0 ? Convert.ToInt32(txtMonthYear.Text.Split('/')[0]) : 0;  } }
            public bool lb25Visible { get { return btnView25.Visible; } }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                VPC.SecurityCheck();
        }

        #region Search Button Clicks

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchCriteria = (string)this.Session[GridStdSearch],
                facSearch = searchCriteria.Split('~')[3];

            if (facSearch.Contains(".ALL") && !facSearch.Contains("STATION:"))
                ((GridView)ctrlViewAll.FindControl("ViewAllMasterGrid")).PageIndex = 0;
            else
                ((GridView)ctrlStandard.FindControl("StandardMasterGrid")).PageIndex = 0;

            ViewAll(false);
        }

        #endregion

        #region Nav Button Clicks
        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblPageTitle.Text == stdTitle)
                ctrlStandard.btnSave_Click(sender, e);
            else if (lblPageTitle.Text == viewAllTitle)
                ctrlViewAll.btnSave_Click(sender, e);

            setPageSize();

            if (ctrlStandard.error > 0 || ctrlViewAll.error > 0)
            {
                PopCannotSaveChanges.Show();
                PopVerifyChanges.Hide();
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            setPageSize();
        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            if (ctrlStandard.Visible)
                ctrlStandard.btnView_Click(sender, e);
            else if (ctrlViewAll.Visible)
                ctrlViewAll.btnView_Click(sender, e);

            ViewAll(true);
        }

        protected void btnView25_Click(object sender, EventArgs e)
        {
            if (ctrlStandard.Visible)
                ctrlStandard.btnView_Click(sender, e);
            else if (ctrlViewAll.Visible)
                ctrlViewAll.btnView_Click(sender, e);

            ViewAll(false);
        }

        #endregion

        #region Popup Menu Button Clicks

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            hfChangeDetector.Value = "";
            if (hfCancelButton.Value.Contains("btnSearch"))
                btnSearch_Click(sender, e);
        }

        protected void btnSaveandContinue_Click(object sender, EventArgs e)
        {
            btnSave_Click(sender, e);

            hfChangeDetector.Value = "";
            if (hfCancelButton.Value.Contains("btnSearch"))
                btnSearch_Click(sender, e);
        }

        #endregion

        #region Misc

        public void setPageSize()
        {
            if (btnView25.Visible == true)
                ViewAll(true);
            else if (btnViewAll.Visible == true)
                ViewAll(false);
        }

        protected void ViewAll(bool TF)
        {
            string searchCriteria = (string)this.Session[GridStdSearch],
                facSearch = searchCriteria.Split('~')[3];

            btnViewAll.Visible = !TF;
            btnView25.Visible = TF;

            GridView viewAllMasterGrid = (GridView)ctrlViewAll.FindControl("viewAllMasterGrid");
            GridView standardMasterGrid = (GridView)ctrlStandard.FindControl("StandardMasterGrid");

            if (facSearch.Contains(".ALL") && !facSearch.Contains("STATION:"))
            {
                ctrlStandard.Visible = false;
                ctrlViewAll.Visible = true;
                btnViewWorkflow.Style.Remove(HtmlTextWriterStyle.Display);
                btnViewWorkflow.Style.Add(HtmlTextWriterStyle.Display, "none");
                lblPageTitle.Text = viewAllTitle;
            }
            else
            {
                ctrlStandard.Visible = true;
                ctrlViewAll.Visible = false;
                btnViewWorkflow.Style.Remove(HtmlTextWriterStyle.Display);
                btnViewWorkflow.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                lblPageTitle.Text = stdTitle;
            }

            if (!TF)
            {
                if (facSearch.Contains(".ALL") && !facSearch.Contains("STATION:"))
                {
                    viewAllMasterGrid.AllowPaging = true;
                    ctrlViewAll.DataBind();
                }
                else
                {
                    standardMasterGrid.AllowPaging = true;
                    ctrlStandard.DataBind();
                }
            }
            else if (TF)
            {
                if (facSearch.Contains(".ALL") && !facSearch.Contains("STATION:"))
                {
                    viewAllMasterGrid.AllowPaging = false;
                    ctrlViewAll.DataBind();
                }
                else
                {
                    standardMasterGrid.AllowPaging = false;
                    ctrlStandard.DataBind();
                }
            }
        }

        #endregion
    }
}
