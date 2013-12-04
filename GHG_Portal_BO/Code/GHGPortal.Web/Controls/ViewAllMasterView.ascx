<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewAllMasterView.ascx.cs" Inherits="Hess.Corporate.GHGPortal.Web.UI.ViewAllMasterView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Csla" Namespace="Csla.Web" TagPrefix="csla" %>

<asp:HiddenField ID="hfSort" runat="server" />
<asp:GridView ID="ViewAllMasterGrid" runat="server" style="margin-top:2px;" AutoGenerateColumns="False" DataKeyNames="RowId"
    DataSourceID="cslaDSViewAll" AllowSorting="True" CssClass="gridView" EmptyDataText="No data for this selection." 
    BorderStyle="None" GridLines="None" CellPadding="0" AllowPaging="false" PageSize="25" Width="100%" CaptionAlign="Top" 
    EmptyDataRowStyle-CssClass="gridViewEmptyRow" AlternatingRowStyle-CssClass="gridViewAlternatingRow" ShowHeaderWhenEmpty="True" 
    HeaderStyle-CssClass="gridViewHeader" RowStyle-CssClass="gridViewRow" PagerStyle-CssClass="gridPager">
    <Columns>
        <asp:BoundField DataField="RowId" Visible="False" />
        <asp:TemplateField>
            <HeaderTemplate>
                <table cellpadding="0" cellspacing="0" style="text-align:center">
                    <tr>
                        <th style="width:2%;">
                        </th>
                        <th style="width:3%;">
                            <asp:LinkButton ID="btnValidated" runat="server" CommandArgument="Validated" Text="Vld" OnClientClick="return DDLVIDVis('viewAll');" OnClick="btnValidated_Click" style="display:inline-block;" />
                            <asp:Image ID="sortUpValidated" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                            <asp:Image ID="sortDownValidated" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                        </th>
                        <th style="width:16%;text-align:left;">
                            <asp:LinkButton ID="btnSourceCategory" runat="server" CommandArgument="SourceCategoryName" Text="Source Category" OnClick="btnSourceCategory_Click" style="display:inline-block;" />
                            <asp:Image ID="sortUpSourceCategory" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false"/>
                            <asp:Image ID="sortDownSourceCategory" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                        </th>
                        <th style="width:25%;text-align:left;">
                            <asp:LinkButton ID="btnAssetName" runat="server" CommandArgument="AssetName" Text="Asset Name" OnClick="btnAssetName_Click" style="display:inline-block;" />
                            <asp:Image ID="sortUpAssetName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                            <asp:Image ID="sortDownAssetName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                        </th>
                        <th style="width:8%;text-align:left;">
                            <asp:LinkButton ID="btnProductName" runat="server" CommandArgument="ProductName" Text="Product Name" OnClick="btnProductName_Click" style="display:inline-block;" />
                            <asp:Image ID="sortUpProductName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                            <asp:Image ID="sortDownProductName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                        </th>
                        <th style="width:9%;">
                            <asp:LinkButton ID="btnThisYTDVol" runat="server" CommandArgument="ThisYTDVol" OnClick="btnThisYTDVol_Click" style="display:inline-block;" />
                            <asp:Image ID="sortUpThisYTDVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                            <asp:Image ID="sortDownThisYTDVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                        </th>
                        <th style="width:9%;">
                            <asp:LinkButton ID="btnPriorYTDVol" runat="server" CommandArgument="PriorYTDVol" OnClick="btnPriorYTDVol_Click" style="display:inline-block;" />
                            <asp:Image ID="sortUpPriorYTDVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                            <asp:Image ID="sortDownPriorYTDVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                        </th>
                        <th style="width:8%;">
                            <asp:LinkButton ID="btnThisMonthVol" runat="server" CommandArgument="ThisMonthVol" OnClick="btnThisMonthVol_Click" style="display:inline-block;" />
                            <asp:Image ID="sortUpThisMonthVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                            <asp:Image ID="sortDownThisMonthVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                        </th>
                        <th style="width:8%;">
                            <asp:LinkButton ID="btnPriorYearMonthVol" runat="server" CommandArgument="PriorYearMonthVol" OnClick="btnPriorYearMonthVol_Click" style="display:inline-block;" />
                            <asp:Image ID="sortUpPriorYearMonthVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                            <asp:Image ID="sortDownPriorYearMonthVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                        </th>
                        <th style="width:5%;">
                            <asp:LinkButton ID="btnUOMshort" runat="server" CommandArgument="UOM" Text="UOM" OnClick="btnUOMshort_Click" style="display:inline-block;" />
                            <asp:Image ID="sortUpUOMshort" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                            <asp:Image ID="sortDownUOMshort" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                        </th>
                        <th>
                            <asp:LinkButton ID="btnDataSource" runat="server" CommandArgument="DataSource" Text="Data Source" OnClick="btnDataSource_Click" style="display:inline-block;" />
                            <asp:Image ID="sortUpDataSource" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                            <asp:Image ID="sortDownDataSource" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                        </th>
                    </tr>
                </table>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:HiddenField ID="hfThisMonthVolume" runat="server" />
                <asp:HiddenField ID="hfUOM" runat="server" />
                <asp:HiddenField ID="hfCollpasedDetail" runat="server" Value="Open" />
                <table id="tblMainGrid" runat="server" cellpadding="0" cellspacing="0" style="border-left:none;">
                    <tr style="text-align:center;" class="rowTable">
                        <td style="width:2%;">
                            <asp:ImageButton ID="imgToggle" runat="server" ImageUrl="~/App_Themes/Hess/Images/plus.gif" OnClientClick="ViewAllChildGrid();" />
                        </td>
                        <td style="width:3%;">
                            <asp:Label ID="lblValidated" runat="server"  CssClass="validateLabel" tipsy="" onmouseover="TipsyVIDLBL();" />
                            <asp:DropDownList ID="ddlValidated" runat="server" CssClass="validateDDL disabledMode" Width="100%" RowIndex='<%# Container.DisplayIndex %>' tipsy="" onchange="StandardVIDChanged('ViewAll');bkgrndBlue();" onmouseover="TipsyVIDDDL()" >
                                <asp:ListItem Text="Y" Value="Y" />
                                <asp:ListItem Text="N" Value="N" />
                                <asp:ListItem Text="X" Value="X" />
                            </asp:DropDownList>
                        </td>
                        <td style="width:16%;text-align:left;">
                            <asp:Label ID="lblSourceCategoryName" runat="server" onmouseover="TipsyShow();" />
                        </td>
                        <td style="width:25%;text-align:left;">
                            <asp:Label ID="lblAssetNameFull" runat="server" onmouseover="TipsyShow();" />
                            <asp:Panel ID="pnlRowRed" runat="server">
                                <asp:Label ID="lblInfoMsg" runat="server" Visible="false"  Text ="Information Message" CssClass="panelTitles" />
                                <asp:Label ID="lblRowRed" runat="server" Visible="false" CssClass="panelText" />
                                <br />
                                <asp:Label ID="lblResolutionMsg" runat="server" Visible="false"  Text="Resolution Message" CssClass="panelTitles" />
                                <asp:Label ID="lblRowRedDisclaimer" runat="server" Visible="false" CssClass="panelText" />
                            </asp:Panel>
                            <ajax:PopupControlExtender ID="pceComments" runat="server" TargetControlID="lblAssetNameFull" PopupControlID="pnlRowRed" Position="Bottom" CommitProperty="value" />
                        </td>
                        <td style="width:8%;text-align:left;">
                            <asp:Label ID="lblProductName" runat="server" />
                        </td>
                        <td style="width:9%;">
                            <asp:Label ID="lblThisYTDvol" runat="server" />
                        </td>
                        <td style="width:9%;">
                            <asp:Label ID="lblPriorYTDvol" runat="server" />
                        </td>
                        <td style="width:8%;">
                            <asp:Label ID="lblThisMonthVol" runat="server" />
                        </td>
                        <td style="width:8%;">
                            <asp:Label ID="lblPriorYearMonthVol" runat="server" />
                        </td>
                        <td style="width:5%;">
                            <asp:Label ID="lblUOMshort" runat="server" tipsy="" onmouseover="TipsyNav('ne')" />
                        </td>
                        <td>
                            <asp:Label ID="lblDataSource" runat="server" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlDetail" runat="server" CssClass="gridViewExpanded" style="padding-left:26px; background-color:#EBEBEB;" />
                <ajax:CollapsiblePanelExtender ID="pnlDetailCollapsiblePanelExtender" 
                    runat="server" Enabled="True" Collapsed="true" TargetControlID="pnlDetail" ImageControlID="imgToggle"
                    CollapsedImage="~/App_Themes/Hess/Images/plus.gif" CollapsedText="Expand" 
                    ExpandedImage="~/App_Themes/Hess/Images/minus.gif" ExpandedText="Collapse" SuppressPostBack="true" 
                    CollapseControlID="imgToggle" ExpandControlID="imgToggle" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<csla:CslaDataSource ID="cslaDSViewAll" runat="server" TypeName="Hess.Corporate.GHGPortal.Business" TypeAssemblyName="ViewAllValidation" />