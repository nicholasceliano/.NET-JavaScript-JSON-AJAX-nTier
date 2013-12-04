using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Hess.Corporate.GHGPortal.Business;
using System.Net.Mail;
using Hess.Corporate.GHGPortal.Configuration;
using System.Text;

namespace Hess.Corporate.GHGPortal.Web.UI
{
    public partial class GHGadmAssetManagement : System.Web.UI.Page
    {
        #region Business Objects

        protected object GetBOAssets(bool update = false)
        {
            IBusinessObjects BOAssets = (IBusinessObjects)this.Session["Assets"];
            if (BOAssets == null || !(BOAssets is Assets) || update)
            {
                Session["Assets"] = null;
                BOAssets = (IBusinessObjects)Assets.GetAssets();
                this.Session["Assets"] = BOAssets;
            }
            if (BOAssets == null || BOAssets.Count == 0)
                BOAssets = new Assets();
            return BOAssets;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VPC.SecurityCheck();
                VPC.Secuirty(lblPageTitle, pnlSearchCriteria, pnlAssetInfoContainer);

                if (Session["userLevel"].ToString() == "SA")
                    btnNewAsset.Visible = true;

                ViewDDLVisible(false);
            }
        }

        #region Search Clicks

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EditMode(false);
            DatabindSelection();
            NewItemVisible(false);
        }

        #endregion

        #region Nav Bar Buttons + Methods

        protected void btnNewAsset_Click(object sender, EventArgs e)
        {
            //show the new asset things
            ddlAssetType.ClearSelection();
            ddlAssetDataName.ClearSelection();
            ddlFacility.ClearSelection();
            ddlSourceCategory.ClearSelection();
            ddlProductName.ClearSelection();
            ddlUOM.ClearSelection();

            //Make all of the ddls visible
            EditMode(true);
            NewItemVisible(true);
            hfChangeDetector.Value = "0";
            hfNewAsset.Value = "New Asset";
            ddlValidatedValue.Style.Remove(HtmlTextWriterStyle.Display);
            ddlValidatedValue.Style.Add(HtmlTextWriterStyle.Display, "none");
        }

        protected void btnCopyAsset_Click(object sender, EventArgs e)
        {
            EditMode(true);
            hfNewAsset.Value = "New Asset";
            EditAsset();
            ClearLBLText();
        }

        protected void btnEditAsset_Click(object sender, EventArgs e)
        {
            //show the edit info
            databindDLLs();
            EditMode(true);
            EditAsset();

            ddlUOM.Style.Remove(HtmlTextWriterStyle.Display);
            ddlUOM.Style.Add(HtmlTextWriterStyle.Display, "none");
            lblUOM.Style.Remove(HtmlTextWriterStyle.Display);
            lblUOM.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
        }

        protected void btnSaveEditAsset_Click(object sender, EventArgs e)
        {
            #region Save

            Asset_Record record;
            if (hfNewAsset.Value == "New Asset")
                record = new Asset_Record();
            else
                record = CurrentRecord();

            string originalAssetName = record.EmittingAsset_Name;
            record.EmittingAsset_Name = txtAssetName.Text;
            record.EmittingAssetType_Name = ddlAssetType.SelectedItem.Text;
            record.AssetDataSource_Name = ddlAssetDataName.SelectedItem.Text != "MANUAL" ? ddlAssetDataName.SelectedItem.Text : "MANUAL PORTAL";
            record.Facility_Name = ddlFacility.SelectedItem.Text;
            record.SourceCategory_Name = ddlSourceCategory.SelectedItem.Text;
            record.Product_Name = ddlProductName.SelectedItem.Text;
            record.VolumeUnit_Code = ddlUOM.SelectedItem.Text;
            record.IncludeInMissing = chkIncludeInMissing.Checked ? "Y" : "N";
            record.Tag_ID = txtTagID.Text;

            if (hfNewAsset.Value == "New Asset")
                record.Review_Ind = "Y";
            else
                record.Review_Ind = ddlValidatedValue.SelectedValue.ToString();

            if (ddlAssetDataName.SelectedItem.Text.Contains("MANUAL"))
                record.AssetDataSource_Type = "MANUAL";
            else
                record.AssetDataSource_Type = "AUTOMATIC";

            if (ddlStatus.SelectedItem.Text == "DISABLED")
                record.Decommissioned_BY = HttpContext.Current.User.Identity.Name.ToString();
            else if (ddlStatus.SelectedItem.Text == "ENABLED")
                record.Decommissioned_BY = string.Empty;

            record.Save();
            int newRecord = record.ReturnedDMPK;

            #endregion

            #region Email Message

            if (hfNewAsset.Value == "New Asset")
            {
                SmtpClient smtp = new SmtpClient(AppConfiguration.Current.SMTPServer);
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("corporatebusinesssystems@hess.com");
                msg.To.Add("GHGAdmin@Hess.com");
                //msg.To.Add("nceliano@Hess.com");
                msg.Subject = "GHG Emissions Validation Portal - New Asset Created";
                msg.IsBodyHtml = true;

                string assetLink = "http://dev-ghgemissions.ihess.com/Admin/AssetManagement?Asset=" + HttpUtility.UrlEncode(record.EmittingAsset_Name);

                StringBuilder html = new StringBuilder();
                html.AppendFormat("<p>The following asset has been created in the GHG Emissions Validation Portal.</p>");
                html.AppendFormat("<p><b>Asset Name: " + record.EmittingAsset_Name + "</b></p>");
                html.AppendFormat("<p>Please click <a href='" + assetLink + "'>here</a> to review and approve the Asset.</p>");
                html.AppendFormat("<p>This is a system generated mail, please do not reply to this email. If you have questions please contact <a href='mailto:CorporateBusinessSystems@hess.com'>CorporateBusinessSystems@hess.com</a></p>");
                html.AppendFormat("<p>Thank you,<br />System Administrator</p>");

                msg.Body = html.ToString();
                smtp.Send(msg);
            }

            #endregion

            EditMode(false);
            DatabindSelection(newRecord);
            NewItemVisible(false);
            hfChangeDetector.Value = "";
            hfNewAsset.Value = "";
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            EditMode(false);
            NewItemVisible(false);

            if (hfNewAsset.Value == "New Asset")
                NavBarVisible(true, true, false, false, false);

            hfNewAsset.Value = string.Empty;
        }

        protected void NavBarVisible(bool NewAsset, bool CopyAsset, bool Edit, bool Save, bool CancelEdit)
        {
            if (Session["userLevel"].ToString() == "SA")
            {
                btnNewAsset.Visible = NewAsset;
                btnCopyAsset.Visible = CopyAsset;
            }

            btnEditAsset.Visible = Edit;
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
            btnSaveEditAsset_Click(sender, e);
            if (hfCancelButton.Value.Contains("btnSearch"))
                btnSearch_Click(sender, e);
            else
                EditMode(false);
        }

        #endregion

        #region Page Databinding

        protected void databindDLLs()
        {
            Assets BOAssets = (Assets)GetBOAssets();
            var _facility = (from c in BOAssets
                             select c.Facility_Name).Distinct().OrderBy(name => name);
            var _assetType = (from c in BOAssets
                              select c.EmittingAssetType_Name).Distinct().OrderBy(name => name);
            var _assetDataName = (from c in BOAssets
                                  select c.AssetDataSource_Name).Distinct().OrderBy(name => name);
            var _sourceCategory = (from c in BOAssets
                                   select c.SourceCategory_Name).Distinct().OrderBy(name => name);
            var _productName = (from c in BOAssets
                                select c.Product_Name).Distinct().OrderBy(name => name);
            var _volUnitCode = (from c in BOAssets
                                select c.VolumeUnit_Code).Distinct().OrderBy(name => name);

            VPC.DDLetc(_assetDataName, ddlAssetDataName);
            VPC.DDLDatabound(ddlAssetDataName, ddlAssetDataName);
            VPC.DDLetc(_facility, ddlFacility);
            VPC.DDLDatabound(ddlFacility);
            VPC.DDLetc(_assetType, ddlAssetType);
            VPC.DDLDatabound(ddlAssetType);
            VPC.DDLetc(_sourceCategory, ddlSourceCategory);
            VPC.DDLDatabound(ddlSourceCategory);
            VPC.DDLetc(_productName, ddlProductName);
            VPC.DDLDatabound(ddlProductName);
            VPC.DDLetc(_volUnitCode, ddlUOM);
            VPC.DDLDatabound(ddlUOM);
        }

        protected Asset_Record CurrentRecord(int currentRecord = 0)
        {
            string session = (string)Session["AssetAdminSearch"];
            string assetValue = session.Substring((session.LastIndexOf("~") + 1), session.Length - (session.LastIndexOf("~") + 1));

            Assets BOAssets = (Assets)GetBOAssets(true);
            if (currentRecord > 0)
            {
                var record = (from c in BOAssets
                              where c.DMPK_EmittingAsset == currentRecord
                              select c).First();
                return record;
            }
            else if (currentRecord == -1)
            {
                var record = (from c in BOAssets
                              where c.DMPK_EmittingAsset == Convert.ToInt32(assetValue)
                              select c).First();
                return record;
            }
            else
            {
                var record = (from c in BOAssets
                              where c.DMPK_EmittingAsset == Convert.ToInt32(hfCurrentAssetID.Value)
                              select c).First();
                return record;
            }
        }

        protected void DatabindSelection(int currRecord = -1)
        {
            Asset_Record record = CurrentRecord(currRecord);
            lblAssetType.Text = record.EmittingAssetType_Name.ToString();
            lblAssetDataName.Text = record.AssetDataSource_Name.ToString() == "MANUAL PORTAL" ? "MANUAL" : record.AssetDataSource_Name.ToString();
            lblAssetDataType.Text = record.AssetDataSource_Type.ToString();
            lblFacility.Text = record.Facility_Name.ToString();
            lblSourceCategory.Text = record.SourceCategory_Name.ToString();
            lblAddedDate.Text = record.Added_DT.ToString();
            lblProductName.Text = record.Product_Name.ToString();
            lblIntegrationTag.Text = record.EmittingAsset_Name.ToString();
            //lblIntegrationTag.Text = record.Facility_Name + "_" + record.EmittingAsset_Name + "_" + record.Product_Name + "_" + record.VolumeUnit_Code;
            lblUOM.Text = record.VolumeUnit_Code.ToString();
            lblAssetName.Text = record.EmittingAsset_Name.ToString();
            lblValidatedValue.Text = record.Review_Ind == "N" ? "Yes" : "No";
            lblIncludeInMissing.Text = record.IncludeInMissing == "N" ? "No" : "Yes";
            lblTagID.Text = record.Tag_ID;
            lblAddedBy.Text = record.AddedBy_Name;
            hfCurrentAssetID.Value = record.DMPK_EmittingAsset.ToString();

            if (record.Decommissioned_BY.Length > 0 || record.Decommissioned_DT != DateTime.MinValue)
                lblStatus.Text = "DISABLED";
            else
                lblStatus.Text = "ENABLED";
        }

        #endregion

        #region Control Visibility

        protected void ViewDDLVisible(bool visible)
        {
            ddlAssetType.Style.Remove(HtmlTextWriterStyle.Display);
            ddlAssetType.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            ddlFacility.Style.Remove(HtmlTextWriterStyle.Display);
            ddlFacility.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            ddlProductName.Style.Remove(HtmlTextWriterStyle.Display);
            ddlProductName.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            ddlUOM.Style.Remove(HtmlTextWriterStyle.Display);
            ddlUOM.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            ddlStatus.Style.Remove(HtmlTextWriterStyle.Display);
            ddlStatus.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");

            if (Session["userLevel"].ToString() == "SA")
            {
                ddlSourceCategory.Style.Remove(HtmlTextWriterStyle.Display);
                ddlSourceCategory.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
                txtAssetName.Style.Remove(HtmlTextWriterStyle.Display);
                txtAssetName.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
                chkIncludeInMissing.Style.Remove(HtmlTextWriterStyle.Display);
                chkIncludeInMissing.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
                ddlAssetDataName.Style.Remove(HtmlTextWriterStyle.Display);
                ddlAssetDataName.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");

                if (lblAssetDataName.Text != "MANUAL")
                {
                    txtTagID.Style.Remove(HtmlTextWriterStyle.Display);
                    txtTagID.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
                }
            }

            if (lblValidatedValue.Text != "Yes")
            {
                ddlValidatedValue.Style.Remove(HtmlTextWriterStyle.Display);
                ddlValidatedValue.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            }
        }

        protected void ViewLBLVisible(bool visible)
        {
            lblAssetType.Style.Remove(HtmlTextWriterStyle.Display);
            lblAssetType.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblFacility.Style.Remove(HtmlTextWriterStyle.Display);
            lblFacility.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblProductName.Style.Remove(HtmlTextWriterStyle.Display);
            lblProductName.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblUOM.Style.Remove(HtmlTextWriterStyle.Display);
            lblUOM.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            lblStatus.Style.Remove(HtmlTextWriterStyle.Display);
            lblStatus.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");

            if (Session["userLevel"].ToString() == "SA")
            {
                lblSourceCategory.Style.Remove(HtmlTextWriterStyle.Display);
                lblSourceCategory.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
                lblAssetName.Style.Remove(HtmlTextWriterStyle.Display);
                lblAssetName.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
                lblIncludeInMissing.Style.Remove(HtmlTextWriterStyle.Display);
                lblIncludeInMissing.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
                lblAssetDataName.Style.Remove(HtmlTextWriterStyle.Display);
                lblAssetDataName.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");

                if (lblAssetDataName.Text != "MANUAL")
                {
                    lblTagID.Style.Remove(HtmlTextWriterStyle.Display);
                    lblTagID.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
                }
            }

            if (lblValidatedValue.Text != "Yes")
            {
                lblValidatedValue.Style.Remove(HtmlTextWriterStyle.Display);
                lblValidatedValue.Style.Add(HtmlTextWriterStyle.Display, visible ? "inline-block" : "none");
            }
        }

        protected void ClearLBLText()
        {
            lblValidatedValue.Text = string.Empty;
            lblAssetName.Text = string.Empty;
            lblFacility.Text = string.Empty;
            lblAssetType.Text = string.Empty;
            lblProductName.Text = string.Empty;
            lblUOM.Text = string.Empty;
            lblIntegrationTag.Text = string.Empty;
            lblStatus.Text = string.Empty;
            lblAddedDate.Text = string.Empty;
            lblSourceCategory.Text = string.Empty;
            lblAssetDataName.Text = string.Empty;
            lblAssetDataType.Text = string.Empty;
            lblIncludeInMissing.Text = string.Empty;
            lblTagID.Text = string.Empty;
            lblAddedBy.Text = string.Empty;
        }

        protected void NewItemVisible(bool visible)
        {
            lblValidatedValue.Style.Remove(HtmlTextWriterStyle.Display);
            lblValidatedValue.Style.Add(HtmlTextWriterStyle.Display, !visible ? "inline-block" : "none");
            lblIntegrationTag.Style.Remove(HtmlTextWriterStyle.Display);
            lblIntegrationTag.Style.Add(HtmlTextWriterStyle.Display, !visible ? "inline-block" : "none");
            lblAddedDate.Style.Remove(HtmlTextWriterStyle.Display);
            lblAddedDate.Style.Add(HtmlTextWriterStyle.Display, !visible ? "inline-block" : "none");
            lblAddedBy.Style.Remove(HtmlTextWriterStyle.Display);
            lblAddedBy.Style.Add(HtmlTextWriterStyle.Display, !visible ? "inline-block" : "none");
            lblAssetDataType.Style.Remove(HtmlTextWriterStyle.Display);
            lblAssetDataType.Style.Add(HtmlTextWriterStyle.Display, !visible ? "inline-block" : "none");
        }

        #endregion

        #region Misc Methods

        protected void EditMode(bool editMode)
        {
            ContentViewMode(editMode);
            ViewDDLVisible(editMode);
            ViewLBLVisible(!editMode);
            NavBarVisible(!editMode, !editMode, !editMode, editMode, editMode);
        }

        protected void ContentViewMode(bool EditMode)
        {
            txtAssetName.Text = string.Empty;
        }

        protected void EditAsset()
        {
            Asset_Record record = CurrentRecord();
            ddlAssetType.SelectedValue = record.EmittingAssetType_Name.ToString();
            ddlAssetDataName.SelectedValue = record.AssetDataSource_Name.ToString();
            ddlFacility.SelectedValue = record.Facility_Name.ToString();
            ddlSourceCategory.SelectedValue = record.SourceCategory_Name.ToString();
            ddlProductName.SelectedValue = record.Product_Name.ToString();
            ddlUOM.SelectedValue = record.VolumeUnit_Code.ToString();
            ddlValidatedValue.SelectedValue = record.Review_Ind == "N" ? "N" : "Y";
            chkIncludeInMissing.Checked = record.IncludeInMissing == "Y" ? true : false;
            hfCurrentAssetID.Value = record.DMPK_EmittingAsset.ToString();
            txtAssetName.Text = record.EmittingAsset_Name.ToString();

            hfChangeDetector.Value = "0";

            if (Session["userLevel"].ToString() == "SA")
            {
                txtAssetName.Style.Remove(HtmlTextWriterStyle.Display);
                txtAssetName.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
            }
        }

        #endregion
    }
}