<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StandardMasterView.ascx.cs" 
    Inherits="Hess.Corporate.GHGPortal.Web.UI.StandardMasterView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Csla" Namespace="Csla.Web" TagPrefix="csla" %>
    <asp:HiddenField ID="hfSort" runat="server" />
    <asp:HiddenField ID="hfRowCount" runat="server" />
    <asp:HiddenField ID="hfVID" runat="server" />
    <asp:HiddenField ID="hfMonthlyVolume" runat="server" />
    <asp:HiddenField ID="hfComments" runat="server" />
    <asp:HiddenField ID="hfRowKey" runat="server" />
<asp:HiddenField ID="hfRefreshOffSearchCriteria" runat="server" Value="true" />
    <asp:GridView ID="StandardMasterGrid" runat="server" style="margin-top:2px;" 
    AutoGenerateColumns="False" DataSourceID="cslaDSstd" AllowSorting="True" 
    CssClass="gridView" EmptyDataText="No data for this selection." 
    BorderStyle="None" GridLines="None" CellPadding="0" AllowPaging="true" PageSize="25"
    EmptyDataRowStyle-CssClass="gridViewEmptyRow" AlternatingRowStyle-CssClass="gridViewAlternatingRow"
        HeaderStyle-CssClass="gridViewHeader" RowStyle-CssClass="gridViewRow" PagerStyle-CssClass="gridPager" 
        CaptionAlign="Top" ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="RowId">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <table style="text-align:left">
                        <tr>
                            <th style="width:2%;">
                            </th>
                            <th style="width:3%;text-align:center;">
                                <asp:LinkButton ID="btnValidated" runat="server" CommandArgument="Validated" Text="Vld" OnClientClick="return DDLVIDVis('standard');" OnClick="btnValidated_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpValidated" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownValidated" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:16%;text-align:left;">
                                <asp:LinkButton ID="btnSourceCategory" runat="server" CommandArgument="SourceCategoryName" Text="Source Category" OnClick="btnSourceCategory_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpSourceCategory" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false"/>
                                <asp:Image ID="sortDownSourceCategory" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:25%;text-align:left;">
                                <asp:LinkButton ID="btnAssetName" runat="server" CommandArgument="AssetName" Text="Asset Name" OnClick="btnAssetName_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpAssetName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownAssetName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:10%;text-align:left;">
                                <asp:LinkButton ID="btnProductName" runat="server" CommandArgument="ProductName" Text="Product Name" OnClick="btnProductName_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpProductName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownProductName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:6%;text-align:center;">
                                <asp:LinkButton ID="btnThisYTDVol" runat="server" CommandArgument="ThisYTDVol" OnClick="btnThisYTDVol_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpThisYTDVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownThisYTDVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:6%;text-align:center;">
                                <asp:LinkButton ID="btnPriorYTDVol" runat="server" CommandArgument="PriorYTDVol" OnClick="btnPriorYTDVol_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpPriorYTDVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownPriorYTDVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:6%;text-align:center;">
                                <asp:LinkButton ID="btnThisMonthVol" runat="server" CommandArgument="ThisMonthVol" OnClick="btnThisMonthVol_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpThisMonthVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownThisMonthVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:6%;text-align:center;">
                                <asp:LinkButton ID="btnPriorYearMonthVol" runat="server" CommandArgument="PriorYearMonthVol" OnClick="btnPriorYearMonthVol_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpPriorYearMonthVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownPriorYearMonthVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:6%;text-align:center;">
                                <asp:LinkButton ID="btnUOMshort" runat="server" CommandArgument="UOM" Text="UOM" OnClick="btnUOMshort_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpUOMshort" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownUOMshort" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:10%;text-align:center">
                                <asp:LinkButton ID="btnDataSource" runat="server" CommandArgument="DataSource" Text="Data Source" OnClick="btnDataSource_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpDataSource" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownDataSource" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="text-align:center;">
                                <asp:LinkButton ID="btnNotes" runat="server" CommandArgument="Note" Text="Note" OnClick="btnNotes_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpNotes" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownNotes" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:HiddenField ID="hfThisMonthVolume" runat="server" />
                    <asp:HiddenField ID="hfUOM" runat="server" />
                    <asp:HiddenField ID="hfCollpasedDetail" runat="server" Value="Open" />
                    <table id="tblMainGrid" runat="server" cellpadding="0" cellspacing="0" style="border-left:none;"><%--onmousedown="VIDHideDDL('Standard');"--%>
                        <tr style="text-align:center;" class="rowTable">
                            <td style="width:2%;">
                                <asp:ImageButton ID="imgToggle" runat="server" ImageUrl="~/App_Themes/Hess/Images/plus.gif" OnClientClick="imgToggle_Click();StandardChildGrid();" />
                            </td>
                            <td style="width:3%;">
                                <asp:Label ID="lblDMPKEmissions" runat="server" style="display:none;" />
                                <asp:Label ID="lblValidated" runat="server" CssClass="validateLabel" tipsy="" onmouseover="TipsyVIDLBL();" /> <%--ondblclick="VIDShowDDL('Standard');"--%>
                                <asp:DropDownList ID="ddlValidated" runat="server" CssClass="validateDDL disabledMode" RowIndex='<%# Container.DisplayIndex %>' tipsy="" onchange="StandardVIDChanged('Standard');bkgrndBlue();" onmouseover="TipsyVIDDDL()">
                                    <asp:ListItem Text="Y" Value="Y" />
                                    <asp:ListItem Text="N" Value="N" />
                                    <asp:ListItem Text="X" Value="X" />
                                </asp:DropDownList>
                            </td>
                            <td style="width:16%;text-align:left;">
                                <asp:Label ID="lblSourceCategoryName" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:25%;text-align:left;">
                                <asp:Label ID="lblAssetNameFull" runat="server" onmouseover="TipsyNav('nw')" />
                                <asp:Panel ID="pnlRowRed" runat="server" style="display:none;">
                                    <asp:Label ID="lblInfoMsg" runat="server" Visible="false"  Text ="Information Message" CssClass="panelTitles" />
                                    <asp:Label ID="lblRowRed" runat="server" Visible="false" CssClass="panelText" />
                                    <br />
                                    <asp:Label ID="lblResolutionMsg" runat="server" Visible="false"  Text="Resolution Message" CssClass="panelTitles" />
                                    <asp:Label ID="lblRowRedDisclaimer" runat="server" Visible="false" CssClass="panelText" />
                                </asp:Panel>
                                <ajax:PopupControlExtender ID="pceComments" runat="server" TargetControlID="lblAssetNameFull" PopupControlID="pnlRowRed" Position="Bottom" CommitProperty="value" />
                            </td>
                            <td style="width:10%;text-align:left;">
                                <asp:Label ID="lblProductName" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:6%;">
                                <asp:Label ID="lblThisYTDvol" runat="server" />
                            </td>
                            <td style="width:6%;">
                                <asp:Label ID="lblPriorYTDvol" runat="server" />
                            </td>
                            <td style="width:6%;">
                                <asp:RequiredFieldValidator ID="rfvMonthVol" runat="server" ValidationGroup="MonthVol" ForeColor="Red" controltovalidate="txtThisMonthVol" errormessage="*" Enabled="false" style="display:none;" />
                                <asp:TextBox ID ="txtThisMonthVol" runat="server" onclick="txtSelected();" onkeydown="bkgrndBlue();RFVEnable();TipsyExit();" ValidationGroup="MonthVol" BorderStyle="None" CssClass="InvisTxtBox" Width="100%" RowIndex='<%# Container.DisplayIndex %>' />
                                <asp:Panel ID="pnlOverRideValue" runat="server" style="text-align:left;display:none;"> 
                                    <asp:Label ID="lblInfoMessagepnlOverRide" runat="server" Visible="false" Text ="Information Message" CssClass="panelTitles" />
                                    <br />
                                    <asp:Label ID="lblOverRideValue" runat="server" CssClass="panelText" />
                                </asp:Panel>
                                <ajax:PopupControlExtender ID="pceOverRideValue" runat="server" TargetControlID="txtThisMonthVol" PopupControlID="pnlOverRideValue" OffsetY="15" Position="Left" CommitProperty="value" />
                            </td>
                            <td style="width:6%;">
                                <asp:Label ID="lblPriorYearMonthVol" runat="server" />
                            </td>
                            <td style="width:6%;">
                                <asp:Label ID="lblUOMshort" runat="server" onmouseover="TipsyNav('ne')" tipsy="" />
                            </td>
                            <td style="width:10%;">
                                <asp:Label ID="lblDataSource" runat="server" />
                            </td>
                            <td>
                                <asp:LinkButton ID="imgNote" runat="server" OnClientClick="return false;" RowIndex='<%# Container.DisplayIndex %>' style="display:inline-block;" />
                                <asp:Panel ID="pnlComments" CssClass="notesPopupPanel"  runat="server" style="width:260px;text-align:left;white-space:nowrap;display:none;">  
                                    <asp:TextBox ID="txtComments"  CssClass="txtComments TextBox"  runat="server" Text='<%# Bind("Comments") %>' Width="220px" BorderStyle="None"  RowIndex='<%# Container.DisplayIndex %>' ValidationGroup="Comments" onkeyup="TipsyExit(); boxExpand(this);" tipsy="" style="overflow:hidden;padding-left:5px;" onfocus="javascript:boxExpand(this);" onkeydown="javascript:boxExpand(this);" TextMode="MultiLine"  />
                                    <asp:Label ID="lblComments" runat="server" CssClass="disabledMode" />
                                    <asp:RequiredFieldValidator ID="rfvComments" display="Static" runat="server" ValidationGroup="Comments" ForeColor="Red" controltovalidate="txtComments" errormessage="*" Enabled="false" />
                                    <span style="float:right;">
                                        <input type="button" class="cancelNotes" value="Cancel" />
                                        <input type="button" class="okNotes" value="OK" />
                                    </span>
                                </asp:Panel>
                                <ajax:PopupControlExtender ID="pceCommentsNotes"  runat="server" TargetControlID="imgNote" PopupControlID="pnlComments" OffsetX="-285" Position="Right" CommitProperty="value" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="pnlDetail" runat="server" CssClass="gridViewExpanded" style="background-color:#EBEBEB;display:none;" />
                    <ajax:CollapsiblePanelExtender ID="pnlDetailCollapsiblePanelExtender" 
                        runat="server" Enabled="True" Collapsed="true" TargetControlID="pnlDetail" ImageControlID="imgToggle"
                        CollapsedImage="~/App_Themes/Hess/Images/plus.gif" CollapsedText="Expand" 
                        ExpandedImage="~/App_Themes/Hess/Images/minus.gif" ExpandedText="Collapse" SuppressPostBack="true" 
                        CollapseControlID="imgToggle" ExpandControlID="imgToggle" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <csla:CslaDataSource ID="cslaDSstd" runat="server" TypeName="Hess.Corporate.GHGPortal.Business" TypeAssemblyName="StandardValidation" />