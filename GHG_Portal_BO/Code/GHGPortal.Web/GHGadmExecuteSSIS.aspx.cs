using System;

namespace Hess.Corporate.GHGPortal.Web.UI.GHGPortal
{
    public partial class GHGadmExecuteSSIS : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region Search Panel Buttons

        protected void btnExecuteSSISPackage_Click(object sender, EventArgs e)
        {
            ctrlExecuteSSIS.SaveAllVariables(true);
        }

        protected void btnSaveExecuteSSISVariables_Click(object sender, EventArgs e)
        {
            ctrlExecuteSSIS.SaveAllVariables(false);
        }

        protected void btnApplyStartEndDate_Click(object sender, EventArgs e)
        {
            if (txtStartRange.Text.Length > 0 && txtEndRange.Text.Length > 0)
                ctrlExecuteSSIS.ApplyStartEndDate(Convert.ToDateTime(txtStartRange.Text), Convert.ToDateTime(txtEndRange.Text));
        }

        protected void btnProcessPackageY_Click(object sender, EventArgs e)
        {
            ctrlExecuteSSIS.UpdateProcessPackage("Y");
        }

        protected void btnProcessPackageN_Click(object sender, EventArgs e)
        {
            ctrlExecuteSSIS.UpdateProcessPackage("N");
        }

        protected void btnClearTablesY_Click(object sender, EventArgs e)
        {
            ctrlExecuteSSIS.UpdateClearTables("Y");
        }

        protected void btnClearTablesN_Click(object sender, EventArgs e)
        {
            ctrlExecuteSSIS.UpdateClearTables("N");
        }

        #endregion

    }
}