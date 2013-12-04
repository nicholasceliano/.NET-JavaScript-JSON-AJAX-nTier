using System;
using System.Web;
using System.Web.UI;
using Hess.Corporate.GHGPortal.Configuration;

namespace Hess.Corporate.GHGPortal.Web.UI
{
    public partial class GHGPortalMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VPC.SecurityCheck();

                string userName = (string)HttpContext.Current.Session["userName"],
                    userLevel = (string)HttpContext.Current.Session["userLevel"];

                lblWelcomeMessage.Text = "Welcome " + userName;

                if (userLevel == "A" || userLevel == "SA")
                {
                    adminMenu.Visible = true;
                    if (userLevel == "SA") 
                        executeSSISMenu.Visible = true;
                }

                if (AppConfiguration.Current.ConfigurationName != "Production")
                    pageHeader.Style.Add(HtmlTextWriterStyle.BackgroundColor, "Maroon");// Show that this is not Production
            }
        }
    }
}
