using System;
using System.Linq;
using Hess.Corporate.GHGPortal.Business;
using AjaxControlToolkit;
using System.Data;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web;
using System.Collections.Generic;

namespace Hess.Corporate.GHGPortal.Web.UI
{
    public partial class FacilityManagementView : CommonGridViewControl
    {
        public DataTable dt { get { return table; } }
        DataTable table = new DataTable();
        public DataTable dtPrimaryOwners { get { return tblPrimaryOwners; } }
        DataTable tblPrimaryOwners = new DataTable();
        bool refreshBOManual = false;
        int primaryOwnerCount = 0;
        string facNameManual;
        #region Method Overrides

        protected override object CurrentBusinessObject
        {
            get
            {
                IBusinessObjects businessObject = (IBusinessObjects)this.Session["GridFacMgmt"];
                bool refreshBO = (businessObject == null || !(businessObject is FacilityMgmt));

                string session = (string)Session["FacAdminSearch"];
                string facName = session != null ? session.Substring((session.IndexOf("Name~") + 5), (session.IndexOf("~ID~")) - (session.IndexOf("Name~") + 5)) : facNameManual;

                FacilityMgmt fm = (FacilityMgmt)businessObject;
                GHGadmFacilityManagement parent = (GHGadmFacilityManagement)this.Page;
                if (!refreshBO)
                {
                    if (facName != fm.Facility || refreshBOManual)
                        refreshBO = true;
                }

                if (refreshBO)
                {
                    Session[conBusinessObjectName] = null;
                    businessObject = (IBusinessObjects)this.GetBusinessObject(CommonGridViewControl.conBusinessObjectName, typeof(FacilityMgmt), facName);

                    // Get new object and save it in Session
                    this.Session["GridFacMgmt"] = businessObject;
                }

                if (businessObject == null || businessObject.Count == 0)
                    businessObject = new FacilityMgmt();

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
                {
                    ((Image)e.Row.FindControl(hfSort.Value)).Visible = true;
                }
            }
            #endregion

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                FacilityMgmt_Record record = (FacilityMgmt_Record)e.Row.DataItem;
                this.SetLabelText((Label)e.Row.FindControl("lblDMPK"), record.DMPK_FacilityDataOwner.ToString().Trim() != "" ? record.DMPK_FacilityDataOwner.ToString().Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblDataOwner"), record.DataOwner_Name.Trim() != "" ? record.DataOwner_Name.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblAddedDate"), record.Added_Dt != DateTime.MinValue ? record.Added_Dt.ToString().Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblLastUpdatedDate"), record.LastUpdate_Dt != DateTime.MinValue ? record.LastUpdate_Dt.ToString().Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblLastUpdatedBy"), record.LastUpdate_By.Trim() != "" ? record.LastUpdate_By.Trim() : " ");
                this.SetLabelText((Label)e.Row.FindControl("lblPrimaryOwner"), record.PrimaryOwner.Trim() != "" ? record.PrimaryOwner.Trim() : " ");

                if (record.PrimaryOwner == "Y")
                {
                    ((Label)e.Row.FindControl("lblPrimaryOwner")).ForeColor = System.Drawing.Color.Blue;
                    ((Label)e.Row.FindControl("lblPrimaryOwner")).Style.Add("cursor", "pointer");
                    ((Label)e.Row.FindControl("lblPrimaryOwner")).Attributes.Add("onclick", "$find('PopAddPrimaryOwner').show();");
                    primaryOwnerCount++;
                }
            }
        }

        #endregion

        #region Gird Management

        public void DisableUser(int rowIndex)
        {
            Label lblDMPK = (Label)FacilityManagementGrid.Rows[rowIndex].FindControl("lblDMPK");
            
            //Update Row
            FacilityMgmt collection = (FacilityMgmt)CurrentBusinessObject;
            FacilityMgmt_Record record = collection.Find(lblDMPK.Text);

            record.Active_Ind = "N";
            record.ExpirationDate = DateTime.Now;
            record.LastUpdate_By = HttpContext.Current.User.Identity.Name.ToString();
            record.Save();
            refreshBOManual = true;
            facNameManual = record.Facility_Name;
            FacilityManagementGrid.DataBind();
        }

        public void AddUser(string userName, string facility)
        {
            refreshBOManual = true;
            facNameManual = facility;
            FacilityManagementGrid.DataBind();
            FacilityMgmt collection = (FacilityMgmt)CurrentBusinessObject;
            EmployeeInfo ei = new EmployeeInfo();
            string SSO = ei.GetUserID(userName);

            FacilityMgmt_Record record = new FacilityMgmt_Record();
            record.DataOwner_Name = userName;
            record.Facility_Name = facility;
            record.SSO_Text = "IHESS\\" + SSO;
            record.AddedBy_Name = HttpContext.Current.User.Identity.Name.ToString();
            record.LastUpdate_By = HttpContext.Current.User.Identity.Name.ToString();
            record.PrimaryOwner = collection.Count == 0 ? "Y" : "N";
            record.Save();
            FacilityManagementGrid.DataBind();
        }

        public void SetPrimaryUser(string userName, string facility)
        {
            facNameManual = facility;
            refreshBOManual = true;
            FacilityManagementGrid.DataBind();

            foreach (GridViewRow row in FacilityManagementGrid.Rows)
            {
                if (userName.ToUpper() == ((Label)row.FindControl("lblDataOwner")).Text.ToUpper())
                {
                    FacilityMgmt_Record record = ((FacilityMgmt)CurrentBusinessObject).Find(Convert.ToInt32(((Label)row.FindControl("lblDMPK")).Text));
                    record.LastUpdate_By = HttpContext.Current.User.Identity.Name.ToString();
                    record.PrimaryOwner = "Y";
                    record.ExpirationDate = null;
                    record.Save();
                }
                else
                {
                    FacilityMgmt_Record record = ((FacilityMgmt)CurrentBusinessObject).Find(Convert.ToInt32(((Label)row.FindControl("lblDMPK")).Text));
                    if (record.PrimaryOwner == "Y")
                    {
                        record.LastUpdate_By = HttpContext.Current.User.Identity.Name.ToString();
                        record.PrimaryOwner = "N";
                        record.ExpirationDate = null;
                        record.Save();
                    }
                }
            }
            FacilityManagementGrid.DataBind();
        }

        public void ImportUsers(string ImportFacility, string ExportFacility, string AddedBy)
        {
            Data.DataAccess da = new Data.DataAccess();
            da.ExecuteStoredProcedure("prcVPadmFacilityManagementGridImportUsers", ImportFacility, ExportFacility, AddedBy);
            facNameManual = ImportFacility;
            refreshBOManual = true;
            FacilityManagementGrid.DataBind();
        }

        #endregion

        protected void FacilityManagementGrid_DataBound(object sender, EventArgs e)
        {
            #region Primary Owner Indicator Logic
            
            if (primaryOwnerCount == 0 && FacilityManagementGrid.Rows.Count > 0)
                lblIndPrimaryOwner.Visible = true;
            else
                lblIndPrimaryOwner.Visible = false;
            primaryOwnerCount = 0;

            #endregion

            List<string> primaryList = new List<string>();
            EmployeeInfo ie = new EmployeeInfo();
            string[] userList = ie.GetAllUserListNames();
            Array.Sort(userList);
            for (int i = 0; i < userList.Length; i++)
            {
                for (int a = 0; a < FacilityManagementGrid.Rows.Count; a++)
                {
                    try
                    {
                        if (userList[i].ToUpper() == ((Label)FacilityManagementGrid.Rows[a].FindControl("lblDataOwner")).Text.ToUpper())
                        {
                            primaryList.Add(userList[i]);
                            userList = userList.Where(w => w != userList[i]).ToArray();
                        }
                        Array.Sort(userList);
                    }
                    catch { }
                }
            }

            AddUsersToDataTable(dt, userList);
            AddUsersToDataTable(dtPrimaryOwners, primaryList.ToArray());

            GHGadmFacilityManagement parent = (GHGadmFacilityManagement)this.Page;
            parent.databindUsers();
        }

        private void AddUsersToDataTable(DataTable dt, string[] list)
        {
            dt.Clear();
            if (dt.Columns.Count == 0)
                dt.Columns.Add("DataOwners");

            foreach (string s in list)
            { dt.Rows.Add(s); }
        }

        #region Sort Clicks

        protected void btnDataOwner_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpDataOwner", "sortDownDataOwner", "DataOwner_Name");
        }

        protected void btnAddedDate_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpAddedDate", "sortDownAddedDate", "Added_Dt");
        }

        protected void btnLastUpdatedDate_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpLastUpdatedDate", "sortDownLastUpdatedDate", "LastUpdate_Dt");
        }

        protected void btnLastUpdatedBy_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpLastUpdatedBy", "sortDownLastUpdatedBy", "LastUpdate_By");
        }

        protected void btnPrimaryOwner_Click(object sender, EventArgs e)
        {
            SortColumn("sortUpPrimaryOwner", "sortDownPrimaryOwner", "PrimaryOwner");
        }

        protected void SortColumn(string sortUpControlName, string sortDownControlName, string sortColumnName)
        {
            FacilityMgmt fm = (FacilityMgmt)Session["GridFacMgmt"];
            if (fm.SortDirection == ListSortDirection.Ascending)
                ColumnSortDirection(sortUpControlName, sortColumnName, ListSortDirection.Descending);
            else
                ColumnSortDirection(sortDownControlName, sortColumnName, ListSortDirection.Ascending);
        }

        protected void ColumnSortDirection(string sortControlName, string sortColumnName, ListSortDirection direction)
        {
            hfSort.Value = sortControlName;
            object businessObject = CurrentBusinessObject;
            base.Sort(ref businessObject, sortColumnName, direction);
            FacilityManagementGrid.DataBind();
        }

        #endregion
    }
}
