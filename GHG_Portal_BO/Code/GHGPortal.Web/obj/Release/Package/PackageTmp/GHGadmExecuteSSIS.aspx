<%@ Page Title="GHG - Admin - Execute SSIS" Language="C#" MasterPageFile="~/GHGPortal.Master" AutoEventWireup="true" 
    CodeBehind="GHGadmExecuteSSIS.aspx.cs" Inherits="Hess.Corporate.GHGPortal.Web.UI.GHGPortal.GHGadmExecuteSSIS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/Controls/ExecuteSSISView.ascx" TagName="ExecuteSSIS" TagPrefix="ex" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="GHGScriptManager" runat="server" AsyncPostBackTimeout="0" />
    <asp:HiddenField ID="chkOptionsCollapsed" runat="server" Value="true" /> 
    <asp:UpdatePanel ID="GHGUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="width:100%;">
                <div class="SSIS_Title">SSIS Admininstration</div>
                <asp:UpdateProgress ID="updateProgress" runat="server" DisplayAfter="100">
                    <ProgressTemplate>
                        <img src="../App_Themes/Hess/Images/indicator.gif" alt="" />
                        Loading. Please wait...
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div id="sBox" class="searchBar">
                <div class="searchBarText">Search</div>
                <asp:Image ID="imgTglSearch" runat="server" CssClass="searchBarImg" />
            </div>
            <asp:Panel ID="pnlSearchCriteria" runat="server" CssClass="collapsePanel"  Width="100%">
                <table cellspacing="0" cellpadding="0" class="sideLegendBar" width="100%">
                    <tr>
                        <td style="text-align: right;width:10%;white-space:nowrap;padding-left:100px;">
                            Start Range:
                        </td>
                        <td style="width:1%;" colspan="2">
                            <asp:textbox ID="txtStartRange" CssClass="datepicker" runat="server"></asp:textbox>
                        </td>
                        <td style="text-align: right;width:15%;padding-left:100px;">
                            Process Package?
                        </td>
                        <td style="width:1%;padding:2px;">
                            <asp:LinkButton ID="btnProcessPackageY" runat="server" Text="Yes" CssClass="formButton" OnClick="btnProcessPackageY_Click" />
                        </td>
                        <td style="width:1%;padding:2px;" colspan="3">
                            <asp:LinkButton ID="btnProcessPackageN" runat="server" Text="No" CssClass="formButton" OnClick="btnProcessPackageN_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;white-space:nowrap;">
                            End Range:
                        </td>
                        <td style="width:1%;">
                            <asp:Textbox ID="txtEndRange" CssClass="datepicker" runat="server"></asp:Textbox>
                        </td>
                        <td style="width:1%;">
                            <asp:LinkButton ID="btnApplyEndRange" runat="server" Text="Apply Dates" CssClass="formButton" OnClick="btnApplyStartEndDate_Click" />
                        </td>
                        <td style="text-align: right">
                            Clear Tables?
                        </td>
                        <td style="padding:2px;">
                            <asp:LinkButton ID="btnClearTablesY" runat="server" Text="Yes" CssClass="formButton" OnClick="btnClearTablesY_Click" />
                        </td>
                        <td style="padding:2px;">
                            <asp:LinkButton ID="btnClearTablesN" runat="server" Text="No" CssClass="formButton" OnClick="btnClearTablesN_Click" />
                        </td>
                    </tr>
                    <tr>
                    <td colspan="6"></td>
                        <td style="text-align: right;float:right;width:1%;padding:2px;">
                            <asp:LinkButton ID="lnkbtnSaveExecuteSSISVariables" runat="server" Text ="Save Variables" CssClass="formButton" Width="150px" OnClick="btnSaveExecuteSSISVariables_Click" />
                        </td>
                        <td style="text-align: right;width:1%;padding:2px;">
                            <asp:LinkButton ID="lnkbtnExecuteSSISPackage" runat="server" Text="Re-Run Package" CssClass="formButton" Width="150px" OnClick="btnExecuteSSISPackage_Click" />
                        </td>
                    </tr> 
                </table> 
            </asp:Panel>
            <ajax:CollapsiblePanelExtender ID="pnlSearchCriteria_CollapsiblePanelExtender" 
                runat="server" Enabled="True" TargetControlID="pnlSearchCriteria" ImageControlID="imgTglSearch"
                CollapsedImage="~/App_Themes/Hess/Images/snap.open.gif" CollapsedText="Expand search criteria" 
                ExpandedImage="~/App_Themes/Hess/Images/snap.close.gif" ExpandedText="Collapse search criteria"
                CollapseControlID="sBox" ExpandControlID="sBox" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="updatePanelGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <ex:ExecuteSSIS ID="ctrlExecuteSSIS" runat="server" EnableViewState="false" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkbtnExecuteSSISPackage" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnApplyEndRange" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnProcessPackageY" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnProcessPackageN" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearTablesY" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClearTablesN" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnSaveExecuteSSISVariables" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkbtnExecuteSSISPackage" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
