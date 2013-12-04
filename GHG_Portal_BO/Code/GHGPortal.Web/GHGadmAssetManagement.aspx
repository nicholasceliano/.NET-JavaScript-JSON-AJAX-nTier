<%@ Page Title="GHG - Admin - Asset Management Page" Language="C#" MasterPageFile="GHGPortal.Master" AutoEventWireup="true" 
    CodeBehind="GHGadmAssetManagement.aspx.cs" Inherits="Hess.Corporate.GHGPortal.Web.UI.GHGadmAssetManagement" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

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
    <asp:HiddenField ID="hfNewAsset" runat="server" />
    <asp:HiddenField ID="hfChangeDetector" Value="0" runat="server" />
    <asp:HiddenField ID="hfCancelButton" Value="" runat="server" />
    <a ID="dummyLink" runat="server" href="#" style="display:none;visibility:hidden;" onclick="return false" />
    <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="100" AssociatedUpdatePanelID="updatePanelGrid" />
    <asp:Panel runat="server" ID="pnlDimming" ScrollBars="Auto" style="display: none;" CssClass="PageDimming" HorizontalAlign="Center">
        <img src="../App_Themes/Hess/Images/indicator.gif" alt="" />
        <asp:Label ID="lblSaving" runat="server" CssClass="PageDimSaving" />
    </asp:Panel>
    <asp:Label ID="lblPageTitle" runat="server" Text="Asset Management" CssClass="SSIS_Title" />
    <asp:Label ID="lblMsg" runat="server" CssClass="statusMessage" />
    <div id="sBox" class="searchBar">
        <div class="searchBarText">Search</div>
        <asp:Image ID="imgTglSearch" runat="server" CssClass="searchBarImg" />
    </div>
    <asp:Panel ID="pnlSearchCriteria" runat="server" CssClass="collapsePanel" Width="100%">
        <table cellpadding="0" cellspacing="0" class="sideLegendBar">
            <tr>
                <td class="searchBarLabel">Entity:</td>
                <td style="width:40%;">
                    <select id="ddlEntity" style="width:450px;" onchange="SearchAssetAdminEntityChange(false);"/>
                </td>
                <td class="searchBarLabel">Facility:</td>
                <td style="width:50%;">
                    <select id="ddlFacilities" style="width:550px;" onchange="SearchAssetAdminFacilityChange();" />
                </td>
            </tr>
            <tr>
                <td class="searchBarLabel">Source Category:</td>
                <td style="width:40%;">
                    <select id="ddlCategory" style="width:450px;" onchange="SearchAssetAdminCategoryChange();" />
                    
                </td>
                <td class="searchBarLabel">Asset Name:<font color="red">*</font></td>
                <td>
                    <select id="ddlAsset" style="width:550px;display:none;" onclick="AssetAdminAssetNameTextbox();" />
                    <input id="txtAsset" type="text" style="width:550px;" onclick="AssetAdminAssetNameTextbox();" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:right;">
                    <asp:LinkButton ID="btnSearch" runat="server" CssClass="formButton" OnClientClick="return AssetAdminCheckSearch();" onclick="btnSearch_Click" style="display:inline-block;" Text="Search" />
                    <asp:LinkButton ID="btnClear" runat="server" CssClass="formButton" style="display:inline-block;" Text="Clear" OnClientClick="return AssetClearAdminSearch();" />
                </td>
            </tr>
        </table>
        <ajax:CollapsiblePanelExtender ID="pnlSearchCriteria_CollapsiblePanelExtender" 
            runat="server" Enabled="True" TargetControlID="pnlSearchCriteria" ImageControlID="imgTglSearch"
            CollapsedImage="~/App_Themes/Hess/Images/snap.open.gif" CollapsedText="Expand search criteria" 
            ExpandedImage="~/App_Themes/Hess/Images/snap.close.gif" ExpandedText="Collapse search criteria"
            CollapseControlID="sBox" ExpandControlID="sBox" />
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlAbandonChanges" CssClass="panelPopUp" style="display:none;">
        <div class="searchBar">
            <div class="searchBarText">Unsaved Changes</div>
            <div class="searchBarImg"><asp:Image ID="imgAbandonChangeLoading" ImageUrl="~/App_Themes/Hess/Images/indicator.gif" runat="server" CssClass="panelPopIndicator" /></div>
        </div>
        <div style="padding:3px;">
            Are you sure you want to navigate away from this page and lose all unsaved changes?
            <div style="position:absolute; bottom:5px;">
                <asp:LinkButton ID="btnContinue" runat="server" Text="Continue without Saving" OnClick="btnContinue_Click" CssClass="formButton" style="display:inline-block;width:35%;" />
                <asp:LinkButton ID="btnSaveandContinue" runat="server" Text="Save & Continue" OnClick="btnSaveandContinue_Click" CssClass="formButton" style="display:inline-block;width:35%;" ValidationGroup="SaveAsset" OnClientClick="AssetManagementRFVIndicatorShow();return ValidateAdminMgmt('SaveAsset');" />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CssClass="formButton" style="display:inline-block;width:20%;" />
            </div>
        </div>
    </asp:Panel>
    <ajax:ModalPopupExtender ID="PopVerifyChanges" runat="server" TargetControlID="dummyLink"
        PopupControlID="pnlAbandonChanges" BackgroundCssClass="modalBg" CancelControlID="btnCancel" 
        DropShadow="false" Enabled="true" BehaviorID="PopVerifyChanges" />
    <ajax:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="updateProgress"
        PopupControlID="pnlDimming" BackgroundCssClass="modalBg" DropShadow="false" Enabled="true" />
    <asp:UpdatePanel ID="updatePanelGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hfCurrentAssetID" Value="" runat="server" />
            <div class="navMenu">
                <div class="navButton">
                    <asp:LinkButton ID="btnNewAsset" runat="server" CssClass="navNew" Visible="false" OnClick="btnNewAsset_Click" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="New Asset" />
                    <asp:LinkButton ID="btnCopyAsset" runat="server" CssClass="navCopy" Visible="false" OnClick="btnCopyAsset_Click" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="Copy" />
                    <asp:LinkButton ID="btnEditAsset" runat="server" CssClass="navEdit" Visible="false" OnClick="btnEditAsset_Click" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="Edit" />
                    <asp:LinkButton ID="btnSaveEdit" runat="server" CssClass="navSave" Visible="false" OnClick="btnSaveEditAsset_Click" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();AssetManagementRFVIndicatorShow();return ValidateAdminMgmt('SaveAsset');" tipsy="Save" ValidationGroup="SaveAsset" />
                    <asp:LinkButton ID="btnEditCancel" runat="server" CssClass="navCancelEdit" Visible="false" OnClick="btnCancelEdit_Click" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="Cancel" />
                </div>
            </div>
            <asp:Panel ID="pnlAssetInfoContainer" runat="server" class="adminInfoContainer">
                <table width="90%" style="margin:auto; margin-top:20px;">
                    <tr>
                        <td style="width:50%;">
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Facility:
                                        <asp:RequiredFieldValidator ID="rfvFacility" runat="server" ControlToValidate="ddlFacility" Text="*" ForeColor="Red" ValidationGroup="SaveAsset" style="display:none;" />
                                    </td>
                                    <td style="width:1%;">
                                        <asp:DropDownList ID="ddlFacility" runat="server" Width="350px" onchange="AdminManagementInfoChanged();TipsyExit();" />
                                        <asp:Label ID="lblFacility" runat="server" Width="350px" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Asset Name:
                                        <asp:RequiredFieldValidator ID="rfvAssetName" runat="server" ControlToValidate="txtAssetName" Text="*" ForeColor="Red" ValidationGroup="SaveAsset" style="display:none;" />
                                    </td>
                                    <td style="width:1%;">
                                        <asp:TextBox ID="txtAssetName" runat="server" Width="345px" style="display:none;" onkeyup="AdminManagementInfoChanged();TipsyExit();" />
                                        <asp:Label ID="lblAssetName" runat="server" Width="350px" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Source Category:
                                        <asp:RequiredFieldValidator ID="rfvSourceCategory" runat="server" ControlToValidate="ddlSourceCategory" Text="*" ForeColor="Red" ValidationGroup="SaveAsset" style="display:none;" />
                                    </td>
                                    <td style="width:1%;">
                                        <asp:DropDownList ID="ddlSourceCategory" runat="server" Width="350px" onchange="AdminManagementInfoChanged();TipsyExit();" style="display:none;" />
                                        <asp:Label ID="lblSourceCategory" runat="server" Width="350px" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Asset Type:
                                        <asp:RequiredFieldValidator ID="rfvAssetType" runat="server" ControlToValidate="ddlAssetType" Text="*" ForeColor="Red" ValidationGroup="SaveAsset" style="display:none;" />
                                    </td>
                                    <td style="width:1%;">
                                        <asp:DropDownList ID="ddlAssetType" runat="server" Width="350px" onchange="AdminManagementInfoChanged();TipsyExit();" />
                                        <asp:Label ID="lblAssetType" runat="server" Width="350px" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Product Name:
                                        <asp:RequiredFieldValidator ID="rfvProductName" runat="server" ControlToValidate="ddlProductName" Text="*" ForeColor="Red" ValidationGroup="SaveAsset" style="display:none;" />
                                    </td>
                                    <td style="width:1%;">
                                        <asp:DropDownList ID="ddlProductName" runat="server" Width="350px" onchange="AdminManagementInfoChanged();TipsyExit();" />
                                        <asp:Label ID="lblProductName" runat="server" Width="350px" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Unit Of Measure:
                                        <asp:RequiredFieldValidator ID="rfvUOM" runat="server" ControlToValidate="ddlUOM" Text="*" ForeColor="Red" ValidationGroup="SaveAsset" style="display:none;" />
                                    </td>
                                    <td style="width:1%;">
                                        <asp:DropDownList ID="ddlUOM" runat="server" Width="350px" onchange="AdminManagementInfoChanged();TipsyExit();" />
                                        <asp:Label ID="lblUOM" runat="server" Width="350px" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Include In Missing:</td>
                                    <td style="width:1%;">
                                        <asp:CheckBox ID="chkIncludeInMissing" runat="server" Text="Yes/No" style="display:none;" />
                                        <asp:Label ID="lblIncludeInMissing" runat="server" Width="350px" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Integration Tag:</td>
                                    <td style="width:1%;">
                                        <asp:Label ID="lblIntegrationTag" runat="server" style="display:inline-block;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width:50%;vertical-align:top;">
                            <table width="100%" style="margin-left:-40px;" cellpadding="0" cellspacing="0">
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Validated:</td>
                                    <td style="width:1%;">
                                        <asp:DropDownList ID="ddlValidatedValue" runat="server" Width="50px" style="display:none;" onchange="AdminManagementInfoChanged()">
                                            <asp:ListItem Text="Yes" Value="N" />
                                            <asp:ListItem Text="No" Value="Y" />
                                        </asp:DropDownList>
                                        <asp:Label ID="lblValidatedValue" runat="server" style="display:none;" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Status:</td>
                                    <td style="width:1%;">
                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="150px" style="display:none;">
                                            <asp:ListItem Enabled="true" Text="ENABLED" />
                                            <asp:ListItem Enabled="true" Text="DISABLED" />
                                        </asp:DropDownList>
                                        <asp:Label ID="lblStatus" runat="server" Width="350px" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Data Source:
                                        <asp:RequiredFieldValidator ID="rfvAssetDataName" runat="server" ControlToValidate="ddlAssetDataName" Text="*" ForeColor="Red" ValidationGroup="SaveAsset" style="display:none;" />
                                    </td>
                                    <td style="width:1%;">
                                        <asp:DropDownList ID="ddlAssetDataName" runat="server" Width="350px" onchange="AdminManagementInfoChanged();AdminDataSourceChanged();TipsyExit();" style="display:none;" />
                                        <asp:Label ID="lblAssetDataName" runat="server" Width="350px" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Asset Data Type:</td>
                                    <td style="width:1%;">
                                        <asp:Label ID="lblAssetDataType" runat="server" Width="350px" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Tag ID:</td>
                                    <td style="width:1%;">
                                        <asp:Label ID="lblTagID" runat="server" />
                                        <asp:TextBox ID="txtTagID" runat="server" Width="345px" style="display:none;" onkeyup="AdminManagementInfoChanged();TipsyExit();" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Added Date:</td>
                                    <td style="width:1%;">
                                        <asp:Label ID="lblAddedDate" runat="server" />
                                    </td>
                                </tr>
                                <tr class="assetSearchRow">
                                    <td class="assetSearchResults">Added By:</td>
                                    <td style="width:1%;">
                                        <asp:Label ID="lblAddedBy" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnNewAsset" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCopyAsset" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnEditAsset" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveEdit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnEditCancel" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnContinue" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveandContinue" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>