using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace GHGPortal.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteTable.Routes.MapPageRoute("Default", "Default", "~/default.aspx");
            RouteTable.Routes.MapPageRoute("Standard", "Standard", "~/GHGvGridStd.aspx");
            RouteTable.Routes.MapPageRoute("Vessel", "Vessel", "~/GHGvGridVessel.aspx");
            RouteTable.Routes.MapPageRoute("AssetManagement", "AssetManagement", "~/GHGadmAssetManagement.aspx");
            RouteTable.Routes.MapPageRoute("FacilityManagement", "FacilityManagement", "~/GHGadmFacilityManagement.aspx");
            RouteTable.Routes.MapPageRoute("ExecuteSSIS", "ExecuteSSIS", "~/GHGadmExecuteSSIS.aspx");
        }
    }
}
