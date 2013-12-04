using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using Hess.Corporate.GHGPortal.Business;

namespace GHGPortal.Web
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class SearchBarServices : System.Web.Services.WebService
    {
        public string currentUser = HttpContext.Current.User.Identity.Name.ToString();

        #region Page Load

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public string SetMonthYear(string page)
        {
            SearchPanelEntity spe = SearchPanelEntity.getSearch(currentUser, page);

            var latestDate = (from c in spe
                              select c.Event_Dt).Max();
            DateTime dt = Convert.ToDateTime(latestDate);
            return dt.Month.ToString() + "/" + dt.Year.ToString();
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public IOrderedEnumerable<string> EntityLoad(string page)
        {
            SearchPanelEntity spe = SearchPanelEntity.getSearch(currentUser, page);

            var entityList = (from c in spe
                              select c.EntityName).Distinct().OrderBy(name => name);

            return entityList;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public IOrderedEnumerable<string> FacilityLoad(string page)
        {
            SearchPanelEntity spe = SearchPanelEntity.getSearch(currentUser, page);

            var facilityList = (from c in spe
                                where !c.FacilityName.Contains(".ALL ")
                                select c.FacilityName).Distinct().OrderBy(name => name);
            return facilityList;
        }

        [WebMethod (EnableSession=true)]
        [System.Web.Script.Services.ScriptMethod()]
        public IOrderedEnumerable<string> FacAdminFacilityLoad()
        {
            IBusinessObjects BOFacilities = (IBusinessObjects)this.Session["Facilities"];
            if (BOFacilities == null || !(BOFacilities is Facilities))
            {
                Session["Facilities"] = null;
                BOFacilities = (IBusinessObjects)Facilities.GetFacilities();
                this.Session["Facilities"] = BOFacilities;
            }
            if (BOFacilities == null || BOFacilities.Count == 0)
                BOFacilities = new Facilities();

            var facilityList = (from c in (Facilities)Session["Facilities"]
                                where !c.Facility_Name.Contains(".ALL ")
                                select c.Facility_Name + "|" + c.DMPK_Facility).Distinct().OrderBy(name => name);

            return facilityList;
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public IOrderedEnumerable<string> AssetAdminAssetNameLoad(string entName, string facName, string scName)
        {
            IBusinessObjects BOAssets = (IBusinessObjects)this.Session["Assets"];
            if (BOAssets == null || !(BOAssets is Assets))
            {
                Session["Assets"] = null;
                BOAssets = (IBusinessObjects)Assets.GetAssets();
                this.Session["Assets"] = BOAssets;
            }
            if (BOAssets == null || BOAssets.Count == 0)
                BOAssets = new Assets();

            if (entName == string.Empty && entName == string.Empty && facName == string.Empty)
                return (from c in (Assets)Session["Assets"]
                        select c.EmittingAsset_Name + "|" + c.DMPK_EmittingAsset).Distinct().OrderBy(name => name);
            else
                return (from c in (Assets)Session["Assets"]
                                 where (entName.Length > 0 ? c.Entity_Name == entName : c.Entity_Name.Length > 0) && (facName.Length > 0 ? c.Facility_Name == facName : c.Facility_Name.Length > 0) && (scName.Length > 0 ? c.SourceCategory_Name == scName : c.SourceCategory_Name.Length > 0)
                                 select c.EmittingAsset_Name + "|" + c.DMPK_EmittingAsset).Distinct().OrderBy(name => name);
        }

        #endregion

        #region Search Session

        [WebMethod (EnableSession=true)]
        [System.Web.Script.Services.ScriptMethod()]
        public void SetSearchSession(string entName, string facName, string scName, string year, string month, string assetName, string dsName, string assetType)
        {
            string searchCriteria = "Entity~" + entName + "~Facility~" + facName + "~Category~" + scName + "~Year~" + year + "~Month~" + month + "~AssetName~" + assetName + "~DataSource~" + dsName + "~AssetType~" + assetType + "~ValCheck~";
            Session["GridStdSearch"] = searchCriteria;
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public void SetAssetAdminSearchSession(string entName, string facName, string scName, string assetName)
        {
            string searchCriteria = "Entity~" + entName + "~Facility~" + facName + "~Category~" + scName + "~AssetName~" + assetName;
            Session["AssetAdminSearch"] = searchCriteria;
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public void SetFacAdminSearchSession(string facID, string facName)
        {
            string searchCriteria = "Name~" + facName + "~ID~" + facID;
            Session["FacAdminSearch"] = searchCriteria;
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string GetSearchSession()
        {
            if (Session["GridStdSearch"] != null)
                return (string)Session["GridStdSearch"];
            else
                return string.Empty;
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string GetAssetAdminSearchSession()
        {
            if (Session["AssetAdminSearch"] != null)
                return (string)Session["AssetAdminSearch"];
            else
                return string.Empty;
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public string GetFacAdminSearchSession()
        {
            if (Session["FacAdminSearch"] != null)
                return (string)Session["FacAdminSearch"];
            else
                return string.Empty;
        }

        #endregion

        #region Entity Change

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public IOrderedEnumerable<string> EntityChange(string entName, string page)
        {
            SearchPanelEntity spe = SearchPanelEntity.getSearch(currentUser, page);
            
            var facDataSource = (from c in spe
                                 where c.EntityName == entName
                                 select c.FacilityName).Distinct().OrderBy(name => name);

            return facDataSource;
        }

        #endregion

        #region Facilty Change

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public IOrderedEnumerable<string> FacilityChange(string facName)
        {
            SearchPanelFacility spf = SearchPanelFacility.getSearch();

            var SCDataSource = (from c in spf
                                where c.FacilityName == facName
                                select c.SourceCategoryName).Distinct().OrderBy(name => name);

            return SCDataSource;
        }

        //[WebMethod(EnableSession = true)]
        //[System.Web.Script.Services.ScriptMethod()]
        //public IOrderedEnumerable<string> test(string facName)
        //{
        //    SearchPanelFacility spf = SearchPanelFacility.getSearch();

        //    var SCDataSource = (from c in spf
        //                        where c.EntityName == facName
        //                        select c.SourceCategoryName).Distinct().OrderBy(name => name);

        //    return SCDataSource;
        //}

        #endregion

        #region Category Change

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public IOrderedEnumerable<string> SearchAssetNameDataBind(string facName, string scName)
        {
            SearchPanelFacility spf = SearchPanelFacility.getSearch();

            var assetNameDataSource = (from c in spf
                                   where ((c.FacilityName == facName) && (c.SourceCategoryName == scName))
                                   select c.AssetName).Distinct().OrderBy(name => name);

            return assetNameDataSource;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public IOrderedEnumerable<string> SearchAssetTypeDataBind(string facName, string scName)
        {
            SearchPanelFacility spf = SearchPanelFacility.getSearch();

            var assetTypeDataSource = (from c in spf
                                   where ((c.FacilityName == facName) && (c.SourceCategoryName == scName))
                                   select c.AssetType).Distinct().OrderBy(name => name);

            return assetTypeDataSource;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public IOrderedEnumerable<string> SearchDataSourceDatabind(string facName, string scName)
        {
            SearchPanelFacility spf = SearchPanelFacility.getSearch();

            var DSDataSource = (from c in spf
                            where ((c.FacilityName == facName) && (c.SourceCategoryName == scName))
                            select c.DataSourceName).Distinct().OrderBy(name => name);

            return DSDataSource;
        }

        #endregion
    }
}
