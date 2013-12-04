<%@ Page Title="" Language="C#" MasterPageFile="~/GHGPortal.Master" AutoEventWireup="true" CodeBehind="GHGadmAppNewAssets.aspx.cs" Inherits="Hess.Corporate.GHGPortal.Web.UI.GHGadmAppNewAssets" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Controls/ApproveNewAssetsView.ascx" TagName="ApproveNewAssets" TagPrefix="aa" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="GHGScriptManager" runat="server" AsyncPostBackTimeout="0" />
    <asp:Label ID="lblPageTitle" runat="server" Text="Approve New Assets" CssClass="SSIS_Title" />
    <asp:Label ID="lblMsg" runat="server" Class="statusMessage" />
    <div id="sBox" class="searchBar">
        <div class="searchBarText">Search</div>
        <asp:Image ID="imgTglSearch" runat="server" CssClass="searchBarImg" />
    </div>
    <asp:Panel ID="pnlSearchCriteria" runat="server" Width="100%">
        <table cellpadding="0" cellspacing="0" class="sideLegendBar">
            <tr>
                <td class="searchBarLabel">Entity: <font color="red">*</font></td>
                <td>
                    <select ID="ddlEntity" style="width:220px;" onchange="SearchEntityChange('STANDARD', false);clearMsg();" onmouseover="ToolTipShowDDL()" />
                </td>
                <td class="searchBarLabel">Facility: <font color="red">*</font></td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlFacilities" Width="520px" tipsy="" onchange="SearchFacilityChange('STANDARD');clearMsg();" onmouseover="ToolTipShowDDL()" />
                </td>
            </tr>
            <tr style="width:100%;">
                <td style="text-align: right;" colspan="2">
                    test
                    <%--<asp:LinkButton runat="server" ID="btnSearch" Text="Search" CssClass="formButton" OnClick="btnSearch_Click" style="display:inline-block;" OnClientClick="return checkSearch();" />
                    <asp:LinkButton runat="server" ID="btnClear" Text="Clear" CssClass="formButton" style="display:inline-block;" OnClientClick="ClearSearch('STANDARD');return false;" />    --%>
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
            <%--<asp:LinkButton ID="btnViewAll" runat="server" CssClass="navViewAll" onclick="btnViewAll_Click" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="Show All Records" />
            <asp:LinkButton ID="btnView25" runat="server" Visible="false" CssClass="navView25" ImageUrl="App_Themes/Hess/Images/Up.png" onclick="btnView25_Click" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="Show 25 Records" />
            <asp:LinkButton ID="btnSave" runat="server" CssClass="navSave" OnClick="btnSave_Click" ValidationGroup="Comments" causesvalidation="false" onmouseover="TipsyNav('ne');" OnClientClick="PageSave();TipsyExit();RFVIndicatorShow();MultiplePageStandardGridSave();return Validate();" tipsy="Save" />
            <asp:LinkButton ID="btnReset" runat="server" CssClass="navReset" OnClick="btnRefresh_Click" onmouseover="TipsyNav('ne');" OnClientClick="TipsyExit();" tipsy="Refresh" />
            <asp:LinkButton ID="btnViewWorkflow" runat="server" CssClass="navViewWorkflow" OnClientClick="open_Workflow(); return false;" onmouseover="TipsyNav('ne');" tipsy="Workflow History" />
            <asp:LinkButton ID="btnViewProcessLog" runat="server" CssClass="navViewProcessLog" OnClientClick="open_ProcessHistoryLog();return false;" onmouseover="TipsyNav('ne');" tipsy="Process History" />--%>
        </div>
    </div>
    <asp:UpdatePanel ID="updatePanelGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <aa:ApproveNewAssets ID="ctrlApproveNewAssets" runat="server" EnableViewState="false" />
        </ContentTemplate>
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnViewAll" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnView25" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnContinue" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveandContinue" EventName="Click" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
