using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hess.Corporate.GHGPortal.Business;
using System.Net.Mail;
using System.Text;
using Hess.Corporate.GHGPortal.Configuration;

namespace Hess.Corporate.GHGPortal.Web.UI
{
    public partial class GHGadmFacilityManagement : System.Web.UI.Page
    {
        public string facName { get { return ddlFacilities.SelectedIndex != -1 && ddlFacilities.SelectedItem.Text.Trim() != "" ? ddlFacilities.SelectedItem.Text.Trim() : string.Empty; } }

        #region Business Objects

        protected object GetBOFacilities(bool update = false)
        {
            IBusinessObjects BOFacilities = (IBusinessObjects)this.Session["Facilities"];
            if (BOFacilities == null || !(BOFacilities is Facilities) || update)
            {
                Session["Facilities"] = null;
                BOFacilities = (IBusinessObjects)Facilities.GetFacilities();
                this.Session["Facilities"] = BOFacilities;
            }
            if (BOFacilities == null || BOFacilities.Count == 0)
                BOFacilities = new Facilities();
            return BOFacilities;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VPC.SecurityCheck();
                VPC.Secuirty(lblPageTitle, pnlSearchCriteria, pnlFacilityInfoContainer);

                if (Session["userLevel"].ToString() == "SA")
                    btnNewFacility.Visible = true;

                ViewDDLVisible(false);
            }
        }

        #region Search Bar Data Methods

        protected void FacilitySearch(DropDownList ddl, Facilities BOFacilties, bool where = false)
        {
            if (where)
            {
                var dataSource = (from c in BOFacilties
                                  where !c.Facility_Name.Contains(".ALL")
                                  select c);
                ddl.DataSource = dataSource;
            }
            else
            {
                var dataSource = (from c in BOFacilties
                                  where !c.Facility_Name.Contains(".ALL")
                                  select new { c.Facility_Name, c.DMPK_Facility });
                ddl.DataSource = dataSource;
            }

            ddl.DataTextField = "Facility_Name";
            ddl.DataValueField = "DMPK_Facility";
            ddl.DataBind();
        }

        protected void ddl_DataBound(object sender, EventArgs e)
        {
            DropDownList current = (DropDownList)sender;
            VPC.DDLDatabound(current);
        }

        #endregion

        #region Search Clicks

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int test = ddlFacilities.Items.Count;
            string test2 = ddlFacilities.SelectedValue;

            EditMode(false);
            DatabindSelection();
            hfNewFacility.Value = "";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Facilities BOFacilties = (Facilities)GetBOFacilities();
            Session["FacAdminSearch"] = null;
            ClearLBLText();
            ViewLBLVisible(true);
            ViewDDLVisible(false);
            NavBarVisible(true, false, false, false);
            ctrlFacilityManagement.DataBind();
            hfNewFacility.Value = "";
        }

        #endregion

        #region Nav Bar Buttons + Methods

        protected void btnNewFacility_Click(object sender, EventArgs e)
        {
            Facilities BOFacilties = (Facilities)GetBOFacilities();
            Session["FacAdminSearch"] = null;
            hfNewFacility.Value = "New Facility";
            databindDDLs();
            ddlEntity.ClearSelection();
            ddlBusinessUnit.ClearSelection();
            ddlPortalPage.ClearSelection();
            ddlFacilities.ClearSelection();
            ctrlFacilityManagement.DataBind();
            txtFacilityName.Text = string.Empty;
            txtCostCenter.Text = string.Empty;
            txtRollupCostCenter.Text = string.Empty;
            
            EditMode(true);
            NewItemVisible(true);
            ClearLBLText();
            hfChangeDetector.Value = "0";
            ddlValidatedValue.Style.Remove(HtmlTextWriterStyle.Display);
            ddlValidatedValue.Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        protected void btnEditFacility_Click(object sender, EventArgs e)
        {
            databindDDLs();
            EditMode(true);
            Facilities_Record record = (Facilities_Record)CurrentRecord();
            ddlEntity.SelectedValue = record.Entity_Name.ToString();
            ddlBusinessUnit.SelectedValue = record.BusinessUnit_Name.ToString();
            ddlPortalPage.SelectedValue = record.PortalPage_Name.ToString();
            ddlValidatedValue.SelectedValue = record.Review_Ind == "N" ? "N" : "Y";
            ddlVisible.SelectedValue = record.Visible == "N" ? "N" : "Y";
            hfCurrentFacilityID.Value = record.DMPK_Facility.ToString();
            txtCostCenter.Text = record.CostCenter;
            txtRollupCostCenter.Text = record.RollupCostCenter;

            hfChangeDetector.Value = "0";
            txtFacilityName.Style.Remove(HtmlTextWriterStyle.Display);
            txtFacilityName.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
            txtFacilityName.Text = record.Facility_Name.ToString();
        }

        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                #region Save

                Facilities_Record record;
                if (lblDateAdded.Text == string.Empty)
                    record = new Facilities_Record();
                else
                    record = CurrentRecord();

                string originalFacilityName = record.Facility_Name,
                    currentUser = HttpContext.Current.User.Identity.Name.ToString();
                record.Facility_Name = txtFacilityName.Text;
                record.Entity_Name = ddlEntity.SelectedItem.Text;
                record.BusinessUnit_Name = ddlBusinessUnit.SelectedItem.Text;
                record.PortalPage_Name = ddlPortalPage.SelectedItem.Text;

                record.Original_Facility = originalFacilityName;
                record.Visible = ddlVisible.SelectedValue.ToString();
                record.CostCenter = txtCostCenter.Text;
                record.RollupCostCenter = txtRollupCostCenter.Text;

                if (hfNewFacility.Value == "New Facility")
                    record.Review_Ind = "Y";
                else
                    record.Review_Ind = ddlValidatedValue.SelectedValue.ToString();

                #region Checks if Facility Name exists or not

                Facilities BOFacilties = (Facilities)GetBOFacilities();
                var facilityCountQuery = (from c in BOFacilties
                                          where c.Facility_Name == record.Facility_Name.Trim().ToUpper()
                                          select c.Facility_Name);
                int facilityCount = facilityCountQuery.Count();

                //if ((originalFacilityName == string.Empty) && facilityCount != 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "SymbolError", "alert('Facility with that name already exists.')", true);
                //    return;
                //}

                #endregion

                if (record.AddedBy_Name.Length == 0)
                {
                    EmployeeInfo ei = new EmployeeInfo();
                    record.AddedBy_Name = currentUser;
                    record.DataOwner_Name = ei.FirstLastName(currentUser);
                }

                if (ddlStatus.SelectedItem.Text == "DISABLED")
                    record.Decommissioned_By = currentUser;
                else if (ddlStatus.SelectedItem.Text == "ENABLED")
                    record.Decommissioned_By = string.Empty;

                record.Save();
                int newRecord = record.ReturnedDMPK;

                #endregion

                #region Email Message

                //if its staging
                if (AppConfiguration.Current.ConfigurationName == "Production")
                {
                    if (lblDateAdded.Text == string.Empty)
                    {
                        SmtpClient smtp = new SmtpClient(AppConfiguration.Current.SMTPServer);
                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress("corporatebusinesssystems@hess.com");
                        msg.To.Add("GHGAdmin@Hess.com");
                        msg.Subject = "GHG Emissions Validation Portal - New Facility Created";
                        msg.IsBodyHtml = true;
                        string facilityLink = "http://dev-ghgemissions.ihess.com/Admin/FacilityManagement?Facility=" + HttpUtility.UrlEncode(record.Facility_Name);

                        StringBuilder html = new StringBuilder();
                        html.AppendFormat("<p>The following facilty has been created in the GHG Emissions Validation Portal.</p>");
                        html.AppendFormat("<p><b>Facility Name: " + record.Facility_Name + "</b></p>");
                        html.AppendFormat("<p>Please click <a href='" + facilityLink + "'>here</a> to review and approve the Facility.</p>");
                        html.AppendFormat("<p>This is a system generated mail, please do not reply to this email. If you have questions please contact <a href='mailto:CorporateBusinessSystems@hess.com'>CorporateBusinessSystems@hess.com</a></p>");
                        html.AppendFormat("<p>Thank you,<br />System Administrator</p>");

                        msg.Body = html.ToString();
                        smtp.Send(msg);
                    }
                }

                #endregion

                EditMode(false);
                DatabindSelection(newRecord);
                NewItemVisible(false);
                hfNewFacility.Value = "";
                hfChangeDetector.Value = "";
            }
            catch (Exception ex)
            {
                PopAddFacilityUser.Hide();
                PopAddPrimaryOwner.Hide();
                PopErrorMsg.Hide();
                PopImportFacilityUsers.Hide();
                PopVerifyChanges.Hide();
                PopVerifyDisable.Hide();
                PopDuplicateFacility.Show();
                //handles duplicate key error
            }
        }

        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            if (hfNewFacility.Value == "New Facility")
            {
                EditMode(false);
                ClearLBLText();
                hfNewFacility.Value = string.Empty;
            }
            else
                EditMode(false);
        }

        protected void NavBarVisible(bool NewFacility, bool Edit, bool Save, bool CancelEdit)
        {
            if (Session["userLevel"].ToString() == "SA")
                btnNewFacility.Visible = NewFacility;

            btnEditFacility.Visible = Edit;
            btnSaveEdit.Visible = Save;
            btnEditCancel.Visible = CancelEdit;
        }

        #endregion

        #region Popout Menu Button Clicks

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            hfChangeDetector.Value = "";
            if (hfCancelButton.Value.Contains("btnSearch"))
                btnSearch_Click(sender, e);
            else
                EditMode(false);
        }

        protected void btnSaveandContinue_Click(object sender, EventArgs e)
        {
            hfChangeDetector.Value = "";
            btnSaveEdit_Click(sender, e);
            if (hfCancelButton.Value.Contains("btnSearch"))
                btnSearch_Click(sender, e);
            else
                EditMode(false);
        }

        #endregion

        #region Page Databinding

        protected void databindDDLs()
        {
            Facilities BOFacilties = (Facilities)GetBOFacilities();

            var _businessUnits = (from c in BOFacilties
                                  select c.BusinessUnit_Name).Distinct().OrderBy(name => name);

            var _portalPage = (from c in BOFacilties
                               select c.PortalPage_Name).Distinct().OrderBy(name => name);

            var _entity = (from c in BOFacilties
                           select c.Entity_Name).Distinct().OrderBy(name => name);

            string session = (string)Session["FacAdminSearch"];
            string facName = session != null ? session.Substring((session.IndexOf("Name~") + 5), (session.IndexOf("~ID~")) - (session.IndexOf("Name~") + 5)) : string.Empty;

            var _facilities = (from c in BOFacilties
                               where !c.Facility_Name.Contains(".ALL") && c.Facility_Name != facName
                               select c.Facility_Name).Distinct().OrderBy(name => name);

            VPC.DDLetc(_businessUnits, ddlBusinessUnit);
            VPC.DDLDatabound(ddlBusinessUnit);
            VPC.DDLetc(_portalPage, ddlPortalPage);
            VPC.DDLDatabound(ddlPortalPage);
            VPC.DDLetc(_entity, ddlEntity);
            VPC.DDLDatabound(ddlEntity);
            VPC.DDLetc(_facilities, ddlFacilitiesList);
        }

        protected Facilities_Record CurrentRecord(int currentRecord = 0)
        {
            string session = (string)Session["FacAdminSearch"];
            string facValue = session != null ? session.Substring((session.LastIndexOf("~") + 1), session.Length - (session.LastIndexOf("~") + 1)): string.Empty;
            Facilities BOFacilities = (Facilities)GetBOFacilities(true);
            if (currentRecord > 0)
            {
                var record = (from c in BOFacilities
                              where c.DMPK_Facility == currentRecord
                              select c).First();
                return record;
            }
            else if (currentRecord == -1)
            {
                var record = (from c in BOFacilities
                              where c.DMPK_Facility == Convert.ToInt32(facValue)
                              select c).First();
                return record;
            }
            else
            {
                var record = (from c in BOFacilities
                              where c.DMPK_Facility == Convert.ToInt32(hfCurrentFacilityID.Value)
                              select c).First();
                return record;
            }
        }

        protected void DatabindSelection(int currRecord = -1)
        {
            Facilities_Record record = CurrentRecord(currRecord);
            lblFacilityName.Text = record.Facility_Name;
            lblBusinessUnit.Text = record.BusinessUnit_Name;
            lblAddedByName.Text = record.AddedBy_Name;
            lblEntity.Text = record.Entity_Name;
            lblPortalPage.Text = record.PortalPage_Name;
            lblDateAdded.Text = record.Added_Dt.ToString("MMM d yyyy");
            lblValidatedValue.Text = record.Review_Ind == "N" ? "Yes" : "No";
            lblVisible.Text = record.Visible == "N" ? "NOT VISIBLE" : "VISIBLE";
            lblCostCenter.Text = record.CostCenter;
            lblRollupCostCenter.Text = record.RollupCostCenter;
            hfCurrentFacilityID.Value = record.DMPK_Facility.ToString();

            if (record.Decommissioned_By.Length > 0 || record.Decommissioned_DT != DateTime.MinValue)
                lblStatus.Text = "DISABLED";
            else
                lblStatus.Text = "ENABLED";
        }

        #endregion

        #region Control Visibility

        protected void ViewDDLVisible(bool visible)
        {
            txtFacilityName.Style.Remove(HtmlTextWriterStyle.Display);
            txtFacilityName.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            ddlEntity.Style.Remove(HtmlTextWriterStyle.Display);
            ddlEntity.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            ddlBusinessUnit.Style.Remove(HtmlTextWriterStyle.Display);
            ddlBusinessUnit.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            ddlPortalPage.Style.Remove(HtmlTextWriterStyle.Display);
            ddlPortalPage.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            ddlStatus.Style.Remove(HtmlTextWriterStyle.Display);
            ddlStatus.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            ddlVisible.Style.Remove(HtmlTextWriterStyle.Display);
            ddlVisible.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            txtCostCenter.Style.Remove(HtmlTextWriterStyle.Display);
            txtCostCenter.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            txtRollupCostCenter.Style.Remove(HtmlTextWriterStyle.Display);
            txtRollupCostCenter.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblBusinessUnitValidator.Visible = visible;
            lblFacNameValidator.Visible = visible;
            lblPortalPageValidator.Visible = visible;
            lblEntityValidator.Visible = visible;

            if (lblValidatedValue.Text != "Yes")
            {
                ddlValidatedValue.Style.Remove(HtmlTextWriterStyle.Display);
                ddlValidatedValue.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            }
        }

        protected void ViewLBLVisible(bool visible)
        {
            lblFacilityName.Style.Remove(HtmlTextWriterStyle.Display);
            lblFacilityName.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblEntity.Style.Remove(HtmlTextWriterStyle.Display);
            lblEntity.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblBusinessUnit.Style.Remove(HtmlTextWriterStyle.Display);
            lblBusinessUnit.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblPortalPage.Style.Remove(HtmlTextWriterStyle.Display);
            lblPortalPage.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblStatus.Style.Remove(HtmlTextWriterStyle.Display);
            lblStatus.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblVisible.Style.Remove(HtmlTextWriterStyle.Display);
            lblVisible.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblCostCenter.Style.Remove(HtmlTextWriterStyle.Display);
            lblCostCenter.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblRollupCostCenter.Style.Remove(HtmlTextWriterStyle.Display);
            lblRollupCostCenter.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblBusinessUnitValidator.Visible = !visible;
            lblFacNameValidator.Visible = !visible;
            lblPortalPageValidator.Visible = !visible;
            lblEntityValidator.Visible = !visible;

            if (lblValidatedValue.Text != "Yes")
            {
                lblValidatedValue.Style.Remove(HtmlTextWriterStyle.Display);
                lblValidatedValue.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            }
        }

        protected void ClearLBLText()
        {
            lblValidatedValue.Text = string.Empty;
            lblFacilityName.Text = string.Empty;
            lblBusinessUnit.Text = string.Empty;
            lblAddedByName.Text = string.Empty;
            lblStatus.Text = string.Empty;
            lblDateAdded.Text = string.Empty;
            lblEntity.Text = string.Empty;
            lblPortalPage.Text = string.Empty;
            lblVisible.Text = string.Empty;
            lblCostCenter.Text = string.Empty;
            lblRollupCostCenter.Text = string.Empty;
        }

        protected void NewItemVisible(bool visible)
        {
            lblValidatedValue.Style.Remove(HtmlTextWriterStyle.Display);
            lblValidatedValue.Style.Add(HtmlTextWriterStyle.Display, !visible ? "inline-block" : "none");
            lblDateAdded.Style.Remove(HtmlTextWriterStyle.Display);
            lblDateAdded.Style.Add(HtmlTextWriterStyle.Display, !visible ? "inline-block" : "none");
            ddlVisible.SelectedIndex = 0;
        }

        #endregion

        #region Grid View Methods

        protected void btnDisable_Click(object sender, EventArgs e)
        {
            ctrlFacilityManagement.DisableUser(Convert.ToInt32(hfDisableRowIndex.Value));
            databindddlAddUserList();
        }

        protected void btnAddFacilityUser_Click(object sender, EventArgs e)
        {
            ctrlFacilityManagement.AddUser(ddlAddUserList.SelectedValue, lblFacilityName.Text);
            databindddlAddUserList();
        }

        protected void btnAddPrimaryOwner_Click(object sender, EventArgs e)
        {
            ctrlFacilityManagement.SetPrimaryUser(ddlPrimaryOwnerList.SelectedValue, lblFacilityName.Text);
            databindddlPrimaryOwnersList();
        }

        protected void btnImportUsers_Click(object sender, EventArgs e)
        {
            ctrlFacilityManagement.ImportUsers(lblFacilityName.Text, ddlFacilitiesList.SelectedItem.Text, HttpContext.Current.User.Identity.Name.ToString());
        }

        public void databindUsers()
        {
            databindddlAddUserList();
            databindddlPrimaryOwnersList();

        }

        protected void databindddlAddUserList()
        {
            ddlAddUserList.DataSource = ctrlFacilityManagement.dt;
            ddlAddUserList.DataTextField = "DataOwners";
            ddlAddUserList.DataBind();
        }

        protected void databindddlPrimaryOwnersList()
        {
            ddlPrimaryOwnerList.DataSource = ctrlFacilityManagement.dtPrimaryOwners;
            ddlPrimaryOwnerList.DataTextField = "DataOwners";
            ddlPrimaryOwnerList.DataBind();
        }

        #endregion

        #region Misc Methods

        protected void EditMode(bool editMode)
        {
            ViewDDLVisible(editMode);
            ViewLBLVisible(!editMode);
            NavBarVisible(!editMode, !editMode, editMode, editMode);
        }

        #endregion
    }
}
