using System;
using System.Web.UI.WebControls;
using Hess.Corporate.GHGPortal.Business;

namespace Hess.Corporate.GHGPortal.Web.UI
{
    public partial class GHGvGridVessel : System.Web.UI.Page
    {
        private string saveRef = "saveRefresh";
        public string facName { get { return ddlFacilities.SelectedIndex != -1 && ddlFacilities.SelectedItem.Text.Trim() != "" ? ddlFacilities.SelectedItem.Text.Trim() : string.Empty; } }
        public int yearNum { get { return Convert.ToInt32(txtMonthYear.Text.Split('/')[1]); } }
        public int monthNum { get { return Convert.ToInt32(txtMonthYear.Text.Split('/')[0]); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                VPC.SecurityCheck();
        }

        #region Search Button Clicks

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.Session[saveRef] = "true";

            GridView VesselMasterGrid = (GridView)ctrlVessel.FindControl("VesselMasterGrid");
            VesselMasterGrid.PageIndex = 0;

            ViewAll(false);
            this.Session[saveRef] = "false";
        }

        #endregion

        #region Nav Button Clicks

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ctrlVessel.btnSave_Click(sender, e);
            setPageSize();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            setPageSize();
        }

        protected void btnViewAll_Click(object sender, EventArgs e)
        {
            ctrlVessel.btnView_Click(sender, e);
            ViewAll(true);
        }

        protected void btnView25_Click(object sender, EventArgs e)
        {
            ctrlVessel.btnView_Click(sender, e);
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
            GridView VesselMasterGrid = (GridView)ctrlVessel.FindControl("VesselMasterGrid");
            btnViewAll.Visible = !TF;
            btnView25.Visible = TF;

            if (TF)
            {
                VesselMasterGrid.AllowPaging = false;
                ctrlVessel.DataBind();
            }
            else if (!TF)
            {
                VesselMasterGrid.AllowPaging = true;
                ctrlVessel.DataBind();
            }
        }

        #endregion
    }
}
