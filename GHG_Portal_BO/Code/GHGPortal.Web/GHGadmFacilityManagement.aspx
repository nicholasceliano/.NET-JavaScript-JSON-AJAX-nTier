<%@ Page Title="GHG - Admin - Facility Management Page" Language="C#" MasterPageFile="GHGPortal.Master" AutoEventWireup="true" CodeBehind="GHGadmFacilityManagement.aspx.cs" Inherits="Hess.Corporate.GHGPortal.Web.UI.GHGadmFacilityManagement" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Controls/FacilityManagementView.ascx" TagName="FacilityManagement" TagPrefix="fm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="GHGScriptManager" runat="server" AsyncPostBackTimeout="0" />
    <script type="text/javascript">
        Sys.Application.add_init(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(showPopup);
        });
        Sys.Application.add_init(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(hidePopup);
        });

        function showPopup(sender, args) {
            if ($('#__EVENTTARGET').val().indexOf('Continue') !== -1) {
                $('#MainContent_imgAbandonChangeLoading').css('display', 'inline-block');
                $('#MainContent_btnSaveandContinue').prop('disabled', true);
                $('#MainContent_btnContinue').prop('disabled', true);
                $('#MainContent_btnCancel').prop('disabled', true);
            } else {
                $('#MainContent_lblSaving').text($('#__EVENTTARGET').val().indexOf('Save') !== -1 ? 'Saving ...' : 'Processing ...');
                $('#MainContent_modalExtender').show();
                $find('<%= modalExtender.ClientID %>').show();
            }
            $(".tipsy").hide();
        }

        function hidePopup(sender, args) {
            $('#MainContent_imgAbandonChangeLoading').css('display', 'none');
            $('#MainContent_btnSaveandContinue').prop('disabled', false);
            $('#MainContent_btnContinue').prop('disabled', false);
            $('#MainContent_btnCancel').prop('disabled', false);
            $find('<%= modalExtender.ClientID %>').hide();
            $(".tipsy").hide();
        }
    </script>
        <asp:HiddenField ID="hfChangeDetector" Value="0" runat="server" />
        <asp:HiddenField ID="hfConfirmDisable" runat="server" />
        <asp:HiddenField ID="hfNewFacility" runat="server" />
        <asp:HiddenField ID="hfCancelButton" Value="" runat="server" />
        <asp:HiddenField ID="hfDisableRowIndex" runat="server" />
        <ajax:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="updateProgress"
            PopupControlID="pnlDimming" BackgroundCssClass="modalBg" DropShadow="false" Enabled="true" />
        <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="100" AssociatedUpdatePanelID="updatePanelGrid" />
        <asp:Panel runat="server" ID="pnlDimming" ScrollBars="Auto" style="display: none;" CssClass="PageDimming" HorizontalAlign="Center">
            <img src="../App_Themes/Hess/Images/indicator.gif" alt="" />
            <asp:Label ID="lblSaving" runat="server" CssClass="PageDimSaving" />
        </asp:Panel>
        <asp:Label ID="lblPageTitle" runat="server" Text="Facility Management" CssClass="SSIS_Title" />
        <asp:Label ID="lblMsg" runat="server" CssClass="statusMessage" />
        <div id="sBox" class="searchBar">
            <div class="searchBarText">Search</div>
            <asp:Image ID="imgTglSearch" runat="server" CssClass="searchBarImg" />
        </div>
        <asp:Panel ID="pnlSearchCriteria" runat="server" Width="100%">
            <table cellspacing="0" cellpadding="0" class="sideLegendBar" width="100%">
                <tr>
                    <td style="text-align:center;width:25%;">
                        Choose a Facility: <font color="red">*</font>
                        <br />
                        <asp:DropDownList ID="ddlFacilities" runat="server" Width="400px" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right;" colspan="4">
                        <asp:LinkButton ID="btnSearch" runat="server" Text="Search" CssClass="formButton" style="display:inline-block;" onclick="btnSearch_Click" OnClientClick="return FacAdminCheckSearch();" />
                        <asp:LinkButton ID="btnClear" runat="server" Text="Clear" CssClass="formButton" 
                            style="display:inline-block;" onclick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        <ajax:CollapsiblePanelExtender ID="pnlSearchCriteria_CollapsiblePanelExtender" 
        runat="server" Enabled="True" TargetControlID="pnlSearchCriteria" ImageControlID="imgTglSearch"
        CollapsedImage="~/App_Themes/Hess/Images/snap.open.gif" CollapsedText="Expand search criteria" 
        ExpandedImage="~/App_Themes/Hess/Images/snap.close.gif" ExpandedText="Collapse search criteria"
        CollapseControlID="sBox" ExpandControlID="sBox" />
        </asp:Panel>
    <asp:UpdatePanel ID="updatePanelGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <a ID="A1" runat="server" href="#" style="display:none;visibility:hidden;" onclick="return false" />
        <a ID="A2" runat="server" href="#" style="display:none;visibility:hidden;" onclick="return false" />
        <a ID="A3" runat="server" href="#" style="display:none;visibility:hidden;" onclick="return false" />
        <a ID="A4" runat="server" href="#" style="display:none;visibility:hidden;" onclick="return false" />
        <a ID="A5" runat="server" href="#" style="display:none;visibility:hidden;" onclick="return false" />
        <a ID="A6" runat="server" href="#" style="display:none;visibility:hidden;" onclick="return false" />
        <a ID="A7" runat="server" href="#" style="display:none;visibility:hidden;" onclick="return false" />
        <asp:HiddenField ID="hfCurrentFacilityID" Value="" runat="server" />
        <div class="navMenu">
            <div class="navButton">
                <asp:LinkButton ID="btnNewFacility" runat="server" CssClass="navNew" Visible="false" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="New Facility" onclick="btnNewFacility_Click" />
                <asp:LinkButton ID="btnEditFacility" runat="server" CssClass="navEdit" Visible="false" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="Edit" onclick="btnEditFacility_Click" />
                <asp:LinkButton ID="btnSaveEdit" runat="server" CssClass="navSave" Visible="false" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();FacilityManagementRFVIndicatorShow();" tipsy="Save" ValidationGroup="SaveFacility" onclick="btnSaveEdit_Click" />
                <asp:LinkButton ID="btnEditCancel" runat="server" CssClass="navCancelEdit" Visible="false" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="Cancel" onclick="btnEditCancel_Click" />
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlAbandonChanges" CssClass="panelPopUp" style="display:none;">
            <div class="searchBar">
                <div class="searchBarText">Unsaved Changes</div>
                <div class="searchBarImg"><asp:Image ID="imgAbandonChangeLoading" ImageUrl="~/App_Themes/Hess/Images/indicator.gif" runat="server" CssClass="panelPopIndicator" /></div>
            </div>
            <div style="padding:3px;">
                Are you sure you want to navigate away from this page and lose all unsaved changes?
                <div style="position:absolute; bottom:5px;">
                    <asp:LinkButton ID="btnContinue" runat="server" Text="Continue without Saving" CssClass="popOutButton" OnClick="btnContinue_Click" />
                    <asp:LinkButton ID="btnSaveandContinue" runat="server" Text="Save & Continue" CssClass="popOutButton" OnClick="btnSaveandContinue_Click" ValidationGroup="SaveFacility" OnClientClick="FacilityManagementRFVIndicatorShow();return ValidateAdminMgmt('SaveFacility');" />
                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CssClass="popOutButton" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlDisableUser" CssClass="panelPopUp" style="display:none;">
            <div class="searchBar">
                <div class="searchBarText">Disable User</div>
                <div class="searchBarImg"><asp:Image ID="imgDisableUserLoading" ImageUrl="~/App_Themes/Hess/Images/indicator.gif" runat="server" CssClass="panelPopIndicator" /></div>
            </div>
            <div style="padding:3px;">
                <div style="width:100%;">Are you sure you want to disable this user?</div>
                <div style="position:absolute; bottom:5px; padding-left:40%;">
                    <asp:LinkButton ID="btnDisable" runat="server" Text="Yes" CssClass="popOutButton" OnClick="btnDisable_Click" />
                    <asp:LinkButton ID="btnCancelDisable" runat="server" Text="Cancel" CssClass="popOutButton" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAddFacilityUser" CssClass="panelPopUp" style="display:none;">
            <div class="searchBar">
                <div class="searchBarText">Add User to Facility</div>
                <div class="searchBarImg"><asp:Image ID="imgAddUserLoading" ImageUrl="~/App_Themes/Hess/Images/indicator.gif" runat="server" CssClass="panelPopIndicator" /></div>
            </div>
            <div style="padding:3px; text-align:center;width:100%;">
                Please select a user from the list to add?
                <br />
                <asp:DropDownList ID="ddlAddUserList" runat="server" />
                <div style="position:absolute; bottom:5px;left:42%;">
                    <asp:LinkButton ID="btnAddFacilityUser" runat="server" Text="Add" CssClass="popOutButton" OnClick="btnAddFacilityUser_Click" />
                    <asp:LinkButton ID="btnCancelAdd" runat="server" Text="Cancel" CssClass="popOutButton" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlImportFacilityUsers" CssClass="panelPopUp" style="display:none;">
            <div class="searchBar">
                <div class="searchBarText">Import Facility Users</div>
                <div class="searchBarImg"><asp:Image ID="imgImportUsersLoading" ImageUrl="~/App_Themes/Hess/Images/indicator.gif" runat="server" CssClass="panelPopIndicator" /></div>
            </div>
            <div style="padding:3px;text-align:center;width:100%;">
                Please select a facility to import users from:
                <br />
                <asp:DropDownList ID="ddlFacilitiesList" runat="server" />
                <div style="position:absolute; bottom:5px;left:37%;">
                    <asp:LinkButton ID="btnImportUsers" runat="server" Text="Import Users" CssClass="popOutButton" OnClick="btnImportUsers_Click" />
                    <asp:LinkButton ID="btnSkip" runat="server" Text="Cancel" CssClass="popOutButton"  />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlErrorMsg" CssClass="panelPopUp" style="display:none;">
            <div class="searchBar">
                <div class="searchBarText">Error Message</div>
                <div class="searchBarImg"><asp:Image ID="Image1" ImageUrl="~/App_Themes/Hess/Images/indicator.gif" runat="server" CssClass="panelPopIndicator" /></div>
            </div>
            <div style="padding:3px;text-align:center;width:100%;">
                Must create facility before adding users.
                <br />
                <div style="padding-top:40px;">
                    <asp:LinkButton ID="btnOK" runat="server" Text="OK" CssClass="popOutButton" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlError" CssClass="panelPopUp" style="display:none;">
            <div class="searchBar">
                <div class="searchBarText">Error Message</div>
                <div class="searchBarImg"><asp:Image ID="Image3" ImageUrl="~/App_Themes/Hess/Images/indicator.gif" runat="server" CssClass="panelPopIndicator" /></div>
            </div>
            <div style="padding:3px;text-align:center;width:100%;">
                Duplicate Facility Name. Cannot create record.
                <br />
                <div style="padding-top:40px;">
                    <asp:LinkButton ID="btnOKDupFacility" runat="server" Text="OK" CssClass="popOutButton" /> <%--onclick="CloseErrorPanel();"--%>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlAddPrimaryOwner" CssClass="panelPopUp" style="display:none;">
            <div class="searchBar">
                <div class="searchBarText">Add Primary Owner</div>
                <div class="searchBarImg"><asp:Image ID="Image2" ImageUrl="~/App_Themes/Hess/Images/indicator.gif" runat="server" CssClass="panelPopIndicator" /></div>
            </div>
            <div style="padding:3px; text-align:center;width:100%;">
                Please select a Primary Owner from the list below?
                <br />
                <asp:DropDownList ID="ddlPrimaryOwnerList" runat="server" />
                <div style="position:absolute; bottom:5px;left:42%;">
                    <asp:LinkButton ID="btnAddPrimaryOwner" runat="server" Text="Add" CssClass="popOutButton" OnClick="btnAddPrimaryOwner_Click" />
                    <asp:LinkButton ID="btnPrimaryOwnerCancel" runat="server" Text="Cancel" CssClass="popOutButton" />
                </div>
            </div>
        </asp:Panel>
        <ajax:ModalPopupExtender ID="PopVerifyDisable" runat="server" TargetControlID="A1"
            PopupControlID="pnlDisableUser" BackgroundCssClass="modalBg" CancelControlID="btnCancelDisable" 
            DropShadow="false" Enabled="true" BehaviorID="PopVerifyDisable" />
        <ajax:ModalPopupExtender ID="PopVerifyChanges" runat="server" TargetControlID="A2"
            PopupControlID="pnlAbandonChanges" BackgroundCssClass="modalBg" CancelControlID="btnCancel" 
            DropShadow="false" Enabled="true" BehaviorID="PopVerifyChanges" />
        <ajax:ModalPopupExtender ID="PopAddPrimaryOwner" runat="server" TargetControlID="A3"
            PopupControlID="pnlAddPrimaryOwner" BackgroundCssClass="modalBg" CancelControlID="btnPrimaryOwnerCancel" 
            DropShadow="false" Enabled="true" BehaviorID="PopAddPrimaryOwner" />
        <ajax:ModalPopupExtender ID="PopAddFacilityUser" runat="server" TargetControlID="A4"
            PopupControlID="pnlAddFacilityUser" BackgroundCssClass="modalBg" CancelControlID="btnCancelAdd" 
            DropShadow="false" Enabled="true" BehaviorID="PopAddFacilityUser" />
        <ajax:ModalPopupExtender ID="PopImportFacilityUsers" runat="server" TargetControlID="A5"
            PopupControlID="pnlImportFacilityUsers" BackgroundCssClass="modalBg" CancelControlID="btnSkip" 
            DropShadow="false" Enabled="true" BehaviorID="PopImportFacilityUsers" />
        <ajax:ModalPopupExtender ID="PopErrorMsg" runat="server" TargetControlID="A6"
            PopupControlID="pnlErrorMsg" BackgroundCssClass="modalBg" CancelControlID="btnOK" 
            DropShadow="false" Enabled="true" BehaviorID="PopErrorMsg" />
        <ajax:ModalPopupExtender ID="PopDuplicateFacility" runat="server" TargetControlID="A7"
            PopupControlID="pnlError" BackgroundCssClass="modalBg" CancelControlID="btnOKDupFacility" 
            DropShadow="false" Enabled="true" BehaviorID="PopDuplicateFacility" />
            <asp:Panel ID="pnlFacilityInfoContainer" runat="server" class="adminInfoContainer">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" style="margin:auto; padding-top:15px;" width="90%">
                                <tr>
                                    <td style="width:50%;vertical-align:top;">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr class="assetSearchRow">
                                                <td class="assetSearchResults">
                                                    <b>Validated:</b>
                                                </td>
                                                <td style="width:1%;">
                                                    <asp:DropDownList ID="ddlValidatedValue" runat="server" 
                                                        onchange="AdminManagementInfoChanged()" style="display:none;" Width="50px">
                                                        <asp:ListItem Text="Yes" Value="N" />
                                                        <asp:ListItem Text="No" Value="Y" />
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblValidatedValue" runat="server" style="display:none;" />
                                                </td>
                                            </tr>
                                            <tr class="assetSearchRow">
                                                <td class="assetSearchResults">
                                                    <b>Facility Name:</b>
                                                    <asp:Label ID="lblFacNameValidator" runat="server" Text="*" ForeColor="Red" Visible="false" />
                                                    <asp:RequiredFieldValidator ID="rfvFacilityName" runat="server" 
                                                        ControlToValidate="txtFacilityName" ValidationGroup="SaveFacility" />
                                                </td>
                                                <td style="width:1%;">
                                                    <asp:TextBox ID="txtFacilityName" runat="server" 
                                                        onkeyup="AdminManagementInfoChanged();TipsyExit();" style="display:none;" 
                                                        ValidationGroup="SaveFacility" Width="345px" />
                                                    <asp:Label ID="lblFacilityName" runat="server" Width="100%" />
                                                </td>
                                            </tr>
                                            <tr class="assetSearchRow">
                                                <td class="assetSearchResults">
                                                    <b>Business Unit:</b>
                                                    <asp:Label ID="lblBusinessUnitValidator" runat="server" Text="*" ForeColor="Red" Visible="false" />
                                                    <asp:RequiredFieldValidator ID="rfvBusinessUnit" runat="server"
                                                        ControlToValidate="ddlBusinessUnit" ValidationGroup="SaveFacility" />
                                                </td>
                                                <td style="width:1%;">
                                                    <asp:DropDownList ID="ddlBusinessUnit" runat="server" 
                                                        onchange="AdminManagementInfoChanged();TipsyExit();" Width="350px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblBusinessUnit" runat="server" Width="350px" />
                                                </td>
                                            </tr>
                                            <tr class="assetSearchRow">
                                                <td class="assetSearchResults">
                                                    <b>Cost Center:</b>
                                                </td>
                                                <td style="width:1%;">
                                                    <asp:TextBox ID="txtCostCenter" runat="server" 
                                                        onkeyup="AdminManagementInfoChanged();TipsyExit();" style="display:none;" 
                                                        ValidationGroup="SaveFacility" Width="345px" />
                                                    <asp:Label ID="lblCostCenter" runat="server" Width="350px" />
                                                </td>
                                            </tr>
                                            <tr class="assetSearchRow">
                                                <td class="assetSearchResults">
                                                    <b>Rollup Cost Center:</b>
                                                </td>
                                                <td style="width:1%;">
                                                    <asp:TextBox ID="txtRollupCostCenter" runat="server" 
                                                        onkeyup="AdminManagementInfoChanged();TipsyExit();" style="display:none;" 
                                                        ValidationGroup="SaveFacility" Width="345px" />
                                                    <asp:Label ID="lblRollupCostCenter" runat="server" Width="350px" />
                                                </td>
                                            </tr>
                                            <tr class="assetSearchRow">
                                                <td class="assetSearchResults">
                                                    <b>Added By:</b>
                                                </td>
                                                <td style="width:1%;">
                                                    <asp:Label ID="lblAddedByName" runat="server" Width="350px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width:50%;vertical-align:top;">
                                        <table cellpadding="0" cellspacing="0" width="100%" style="margin-left:-80px;">
                                            <tr class="assetSearchRow">
                                                <td class="assetSearchResults">
                                                    <b>Status:</b>
                                                </td>
                                                <td style="width:1%;">
                                                    <asp:DropDownList ID="ddlStatus" runat="server" 
                                                        onchange="AdminManagementInfoChanged()" Width="150px">
                                                        <asp:ListItem Enabled="true" Text="ENABLED" />
                                                        <asp:ListItem Enabled="true" Text="DISABLED" />
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblStatus" runat="server" Width="350px" />
                                                </td>
                                            </tr>
                                            <tr class="assetSearchRow">
                                                <td class="assetSearchResults">
                                                    <b>Visibility:</b>
                                                </td>
                                                <td style="width:1%;">
                                                    <asp:DropDownList ID="ddlVisible" runat="server" 
                                                        onchange="AdminManagementInfoChanged()" Width="150px">
                                                        <asp:ListItem Enabled="true" Text="VISIBLE" Value="Y" />
                                                        <asp:ListItem Enabled="true" Text="NOT VISIBLE" Value="N" />
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblVisible" runat="server" Width="350px" />
                                                </td>
                                            </tr>
                                            <tr class="assetSearchRow">
                                                <td class="assetSearchResults">
                                                    <b>Portal Page:</b>
                                                    <asp:Label ID="lblPortalPageValidator" runat="server" Text="*" ForeColor="Red" Visible="false" />
                                                    <asp:RequiredFieldValidator ID="rfvPortalPage" runat="server" 
                                                        ControlToValidate="ddlPortalPage" ValidationGroup="SaveFacility" />
                                                </td>
                                                <td style="width:1%;">
                                                    <asp:DropDownList ID="ddlPortalPage" runat="server" 
                                                        onchange="AdminManagementInfoChanged();TipsyExit();" Width="350px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblPortalPage" runat="server" Width="350px" />
                                                </td>
                                            </tr>
                                            <tr class="assetSearchRow">
                                                <td class="assetSearchResults">
                                                    <b>Entity:</b>
                                                    <asp:Label ID="lblEntityValidator" runat="server" Text="*" ForeColor="Red" Visible="false" />
                                                    <asp:RequiredFieldValidator ID="rfvEntity" runat="server" 
                                                        ControlToValidate="ddlEntity" ValidationGroup="SaveFacility" />
                                                </td>
                                                <td style="width:1%;">
                                                    <asp:DropDownList ID="ddlEntity" runat="server" 
                                                        onchange="AdminManagementInfoChanged();TipsyExit();" Width="350px">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblEntity" runat="server" Width="350px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="assetSearchResults">
                                                    <b>Date Added:</b>
                                                </td>
                                                <td style="width:1%;">
                                                    <asp:Label ID="lblDateAdded" runat="server" Width="350px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-top:20px;">
                            <fm:FacilityManagement ID="ctrlFacilityManagement" runat="server" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblGrdViewFooter" runat="server" style="text-align:center;" 
                                Text="Please finish creating facility to add new owners." visible="false" 
                                Width="100%"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNewFacility" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnEditFacility" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveEdit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnEditCancel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnContinue" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveandContinue" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnDisable" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnAddFacilityUser" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnImportUsers" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>