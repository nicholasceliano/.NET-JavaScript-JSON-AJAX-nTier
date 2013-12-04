﻿<%@ Page Title="GHG - Vessel Approval" Language="C#" MasterPageFile="GHGPortal.Master" AutoEventWireup="true"
    CodeBehind="GHGvGridVessel.aspx.cs" Inherits="Hess.Corporate.GHGPortal.Web.UI.GHGvGridVessel" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Controls/VesselMasterView.ascx" TagName="VesselMaster" TagPrefix="vm" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id='tempDiv' style="display:none"></div>
    <asp:ScriptManager ID="GHGScript" runat="server" AsyncPostBackTimeout="0" />
    <script language="javascript" type="text/javascript">
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
        <asp:HiddenField ID="hfCancelButton" Value="" runat="server" />
        <asp:Panel runat="server" ID="pnlDimming" ScrollBars="Auto" style="display: none;" CssClass="PageDimming" HorizontalAlign="Center">
            <img src="App_Themes/Hess/Images/indicator.gif" alt="" />
            <asp:Label ID="lblSaving" runat="server" CssClass="PageDimSaving" />
        </asp:Panel>
        <div class="SSIS_Title">Validation Portal - Vessel</div>
        <asp:Label ID="lblMsg" runat="server" CssClass="statusMessage" />
        <div id="sBox" class="searchBar">
            <div class="searchBarText">Search</div>
            <asp:Image ID="imgTglSearch" runat="server" CssClass="searchBarImg" />
        </div>
    <asp:Panel ID="pnlSearchCriteria" runat="server" CssClass="collapsePanel" Width="100%">
        <table cellspacing="0" cellpadding="0" class="sideLegendBar">
            <tr>
                <td class="searchBarLabel">Entity: <font color="red">*</font></td>
                <td style="width:25%;">
                    <select ID="ddlEntity" style="width:220px;" onchange="SearchEntityChange('VESSEL', false);clearMsg();" onmouseover="ToolTipShowDDL()" />
                </td>
                <td class="searchBarLabel">Facility: <font color="red">*</font></td>
                <td style="width:50%;">
                    <asp:DropDownList runat="server" ID="ddlFacilities" Width="520px" tipsy="" onchange="SearchFacilityChange('VESSEL');clearMsg();" onmouseover="ToolTipShowDDL()" />
                </td>
                <td class="searchBarLabel">Validation Period: <font color="red">*</font></td>
                <td style="width:25%;">
                    <asp:TextBox ID="txtMonthYear" runat="server" Width="50px" Height="16px" />
                    <ajax:CalendarExtender runat="server" ID="calendar" TargetControlID="txtMonthYear" CssClass="cal_Theme1" Format="M/yyyy" OnClientDateSelectionChanged="HideCalendar" BehaviorID="MonthCalendarBehavior" OnClientShown="SetCalendarModeMonth" PopupPosition="BottomRight" />
                </td>
            </tr>
            <tr>
                <td class="searchBarLabel">Source Category:</td>
                <td>
                    <select id="ddlCategory" style="width:220px;" onmouseover="ToolTipShowDDL()" onchange="SearchCategoryChange('VESSEL');" onmouseout="TipsyExit()" />
                </td>
                <td class="searchBarLabel">Asset Name:</td>
                <td>
                    <select ID="ddlAsset" style="width:520px;" onmouseover="ToolTipShowDDL()" onmouseout="TipsyExit()" />
                </td>
                <td class="searchBarLabel">Asset Type:</td>
                <td>
                    <select ID="ddlAssetType" style="width:190px;" onmouseover="ToolTipShowDDL()" onmouseout="TipsyExit()" />
                </td>
            </tr>
            <tr style="width:100%;">
                <td class="searchBarLabel">Data Source:</td>
                <td>
                    <select ID="ddlSource" style="width:220px;" onmouseover="ToolTipShowDDL()" />
                </td>
                <td class="searchBarLabel">Show Data:</td>
                <td>
                    <select id="ddlValidated">
                        <option></option>
                        <option value="Y">Validated</option>
                        <option value="N">Not Validated</option>
                        <option value="X">Validate but don't send to Enviance</option>
                        <option value="I">Validated but we got a new volume</option>
                        <option value="E">Missing Values</option>
                    </select>
                </td>
                <td style="text-align: right;" colspan="2">
                    <asp:LinkButton runat="server" ID="btnSearch" Text="Search" CssClass="formButton" OnClick="btnSearch_Click" style="display:inline-block;" OnClientClick="return checkSearch();" />
                    <asp:LinkButton runat="server" ID="btnClear" Text="Clear" CssClass="formButton" style="display:inline-block;" OnClientClick="ClearSearch('VESSEL');return false;" />
                </td>
            </tr>
        </table> 
        <ajax:CollapsiblePanelExtender ID="pnlSearchCriteria_CollapsiblePanelExtender" 
            runat="server" Enabled="True" TargetControlID="pnlSearchCriteria" ImageControlID="imgTglSearch"
            CollapsedImage="~/App_Themes/Hess/Images/snap.open.gif" CollapsedText="Expand search criteria" 
            ExpandedImage="~/App_Themes/Hess/Images/snap.close.gif" ExpandedText="Collapse search criteria"
            CollapseControlID="sBox" ExpandControlID="sBox" />
    </asp:Panel>
    <div class="navMenu">
        <div class="navButton">
            <asp:LinkButton ID="btnViewAll" runat="server" CssClass="navViewAll" onclick="btnViewAll_Click" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="Show All Records" />
            <asp:LinkButton ID="btnView25" runat="server" Visible="false" CssClass="navView25" ImageUrl="App_Themes/Hess/Images/Up.png" onclick="btnView25_Click" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="Show 25 Records" />
            <asp:LinkButton ID="btnSave" runat="server" CssClass="navSave" OnClick="btnSave_Click" Visible="true" ValidationGroup="Comments" causesvalidation="true" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();VesselRFVIndicatorShow();return Validate();" tipsy="Save" />
            <asp:LinkButton ID="btnReset" runat="server" CssClass="navReset" OnClick="btnRefresh_Click" Visible="true" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="Refresh" />
            <asp:LinkButton ID="btnViewProcessLog" runat="server" CssClass="navViewProcessLog" OnClientClick="open_ProcessHistoryLog();return false;" onmouseover="TipsyNav('ne');" tipsy="Process History" />
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
                <asp:LinkButton ID="btnContinue" runat="server" Text="Continue without Saving" CssClass="popOutButton" onclick="btnContinue_Click" OnClientClick="ClearErrors();" />
                <asp:LinkButton ID="btnSaveandContinue" runat="server" Text="Save & Continue" CssClass="popOutButton" ValidationGroup="Comments" OnClientClick="ClearErrors();VesselRFVIndicatorShow();return Validate();" onclick="btnSaveandContinue_Click"  />
                <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CssClass="popOutButton" />
            </div>
        </div>
    </asp:Panel>
    <asp:UpdatePanel ID="updatePanelGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <a ID="dummyLink" runat="server" href="#" style="display:none;visibility:hidden;" onclick="return false" />
            <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="100" AssociatedUpdatePanelID="updatePanelGrid" />
            <ajax:ModalPopupExtender ID="modalExtender" runat="server" TargetControlID="updateProgress"
                PopupControlID="pnlDimming" BackgroundCssClass="modalBg" DropShadow="false" Enabled="true" />
            <ajax:ModalPopupExtender ID="PopVerifyChanges" runat="server" TargetControlID="dummyLink"
                PopupControlID="pnlAbandonChanges" BackgroundCssClass="modalBg" CancelControlID="btnCancel" 
                DropShadow="false" Enabled="true" BehaviorID="PopVerifyChanges" />
            <vm:VesselMaster ID="ctrlVessel" runat="server" EnableViewState="false" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnViewAll" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnView25" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnContinue" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveandContinue" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>