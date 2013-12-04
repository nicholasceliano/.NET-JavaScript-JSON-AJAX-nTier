<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApproveNewAssetsView.ascx.cs" Inherits="Hess.Corporate.GHGPortal.Web.UI.ApproveNewAssetsView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Csla" Namespace="Csla.Web" TagPrefix="csla" %>

    <asp:HiddenField ID="hfSort" runat="server" />
    <asp:GridView ID="ApproveNewAssetsGrid" runat="server" style="margin-top:2px;" 
        AutoGenerateColumns="False" DataSourceID="cslaDSAssets" AllowSorting="True" 
        CssClass="gridView" EmptyDataText="No data for this selection." 
        BorderStyle="None" GridLines="None" CellPadding="0" AllowPaging="true" PageSize="35"
        EmptyDataRowStyle-CssClass="gridViewEmptyRow" AlternatingRowStyle-CssClass="gridViewAlternatingRow"
        HeaderStyle-CssClass="gridViewHeader" RowStyle-CssClass="gridViewRow" PagerStyle-CssClass="gridPager" 
        CaptionAlign="Top" ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="DMPK_EmittingAsset">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <table style="width:100%;">
                        <tr>
                            <th style="width:3%;text-align:center;">
                                <asp:LinkButton ID="btnValidated" runat="server" CommandArgument="Review_Ind" Text="Validated" OnClientClick="return DDLVIDVis('standard');" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpValidated" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownValidated" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:10%;text-align:left;">
                                <asp:LinkButton ID="btnFacilityName" runat="server" CommandArgument="Facility_Name" Text="Facility Name" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpFacilityName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false"/>
                                <asp:Image ID="sortDownFacilityName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:10%;text-align:left;">
                                <asp:LinkButton ID="btnAssetName" runat="server" CommandArgument="EmittingAsset_Name" Text="Asset Name" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpAssetName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownAssetName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:10%;text-align:left;">
                                <asp:LinkButton ID="btnSourceCategory" runat="server" CommandArgument="SourceCategory_Name" Text="Source Category" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpSourceCategory" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownSourceCategory" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:10%;text-align:center;">
                                <asp:LinkButton ID="btnAssetType" runat="server" CommandArgument="EmittingAssetType_Name" Text="Asset Type" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpAssetType" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownAssetType" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:6%;text-align:center;">
                                <asp:LinkButton ID="btnProductName" runat="server" CommandArgument="Product_Name" Text="Product Name" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpProductName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownProductName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:6%;text-align:center;">
                                <asp:LinkButton ID="btnUOM" runat="server" CommandArgument="" Text="UOM" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpUOM" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownUOM" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:5%;text-align:center;">
                                <asp:LinkButton ID="btnStatus" runat="server" CommandArgument="Decommissioned_DT" Text="Status" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpStatus" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownStatus" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:6%;text-align:center;">
                                <asp:LinkButton ID="btnDataSource" runat="server" CommandArgument="AssetDataSource_Name" Text="Data Source" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpDataSource" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownDataSource" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:6%;text-align:center">
                                <asp:LinkButton ID="btnAssetDataType" runat="server" CommandArgument="AssetDataSource_Type" Text="Asset Data Type" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpAssetDataType" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownAssetDataType" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:6%;text-align:center">
                                <asp:LinkButton ID="btnTagID" runat="server" CommandArgument="Tag_ID" Text="Tag ID" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpTagID" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownTagID" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:3%;text-align:center">
                                <asp:LinkButton ID="btnIncludeInMissing" runat="server" CommandArgument="IncludeInMissing" Text="Include In Missing" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpIncludeInMissing" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownIncludeInMissing" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:6%;text-align:center">
                                <asp:LinkButton ID="btnIntegrationTag" runat="server" CommandArgument="" Text="Integration Tag" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpIntegrationTag" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownIntegrationTag" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:5%;text-align:center">
                                <asp:LinkButton ID="btnAddedBy" runat="server" CommandArgument="AddedBy_Name" Text="Added By" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpAddedBy" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownAddedBy" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="text-align:center">
                                <asp:LinkButton ID="btnAddedDate" runat="server" CommandArgument="Added_DT" Text="Added Date" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpAddedDate" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownAddedDate" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table id="tblMainGrid" runat="server" cellpadding="0" cellspacing="0" style="border-left:none;width:100%;">
                        <tr style="text-align:center;" class="rowTable">
                            <td style="width:3%;">
                                <asp:Label ID="lblDMPKEmittingAsset" runat="server" style="display:none;" />
                                <asp:Label ID="lblValidated" runat="server" CssClass="validateLabel" tipsy="" onmouseover="TipsyVIDLBL();" />
                                <asp:DropDownList ID="ddlValidated" runat="server" CssClass="validateDDL disabledMode" RowIndex='<%# Container.DisplayIndex %>' tipsy="" onchange="StandardVIDChanged('Standard');bkgrndBlue();" onmouseover="TipsyVIDDDL()">
                                    <asp:ListItem Text="Y" Value="Y" />
                                    <asp:ListItem Text="N" Value="N" />
                                </asp:DropDownList>
                            </td>
                            <td style="width:10%;text-align:left;">
                                <asp:Label ID="lblFacilityName" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:10%;text-align:left;">
                                <asp:Label ID="lblAssetName" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:10%;text-align:left;">
                                <asp:Label ID="lblSourceCategory" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:10%;text-align:left;">
                                <asp:Label ID="lblAssetType" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:6%;text-align:left;">
                                <asp:Label ID="lblProductName" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:6%;text-align:left;">
                                <asp:Label ID="lblUOM" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:5%;text-align:left;">
                                <asp:Label ID="lblStatus" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:6%;text-align:left;">
                                <asp:Label ID="lblDataSource" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:6%;text-align:left;">
                                <asp:Label ID="lblAssetDataType" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:6%;text-align:left;">
                                <asp:Label ID="lblTagID" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:3%;text-align:left;">
                                <asp:Label ID="lblIncludeInMissing" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:6%;text-align:left;">
                                <asp:Label ID="lblIntegrationTag" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:5%;text-align:left;">
                                <asp:Label ID="lblAddedBy" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="text-align:left;">
                                <asp:Label ID="lblAddedDate" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                        </tr>
                    </table>                
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <csla:CslaDataSource ID="cslaDSAssets" runat="server" TypeName="Hess.Corporate.GHGPortal.Business" TypeAssemblyName="Assets" />