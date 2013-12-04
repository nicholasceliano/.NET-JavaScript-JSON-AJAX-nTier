using System;
using System.Web;
using Hess.Corporate.GHGPortal.Data;
using System.Data;

namespace Hess.Corporate.GHGPortal.Web.UI.GHGPortal
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VPC.SecurityCheck();

            DataAccess da = new DataAccess();
            string user = HttpContext.Current.User.Identity.Name;
            
            IDataReader dataReader = da.ExecuteStoredProcedure("prcVPdefaultGetFacilityCount", user);
            dataReader.Read();
            int stdCount = dataReader.GetInt32(0);
            int vesselCount = dataReader.GetInt32(1);
            dataReader.Close();

            if (vesselCount > 0 && stdCount == 0)
                Server.Transfer("~/GHGvGridVessel.aspx");
            else
                Server.Transfer("~/GHGvGridStd.aspx");
        }
    }
}