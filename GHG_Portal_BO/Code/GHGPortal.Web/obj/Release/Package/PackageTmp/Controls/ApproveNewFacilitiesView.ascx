<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ApproveNewFacilitiesView.ascx.cs" Inherits="Hess.Corporate.GHGPortal.Web.UI.ApproveNewFacilitiesView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Csla" Namespace="Csla.Web" TagPrefix="csla" %>

    <asp:HiddenField ID="hfSort" runat="server" />
    <asp:GridView ID="ApproveNewFacilitiesGrid" runat="server" style="margin-top:2px;" 
        AutoGenerateColumns="False" DataSourceID="cslaDSFacilities" AllowSorting="True" 
        CssClass="gridView" EmptyDataText="No data for this selection." 
        BorderStyle="None" GridLines="None" CellPadding="0" AllowPaging="true" PageSize="35"
        EmptyDataRowStyle-CssClass="gridViewEmptyRow" AlternatingRowStyle-CssClass="gridViewAlternatingRow"
        HeaderStyle-CssClass="gridViewHeader" RowStyle-CssClass="gridViewRow" PagerStyle-CssClass="gridPager" 
        CaptionAlign="Top" ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="DMPK_Facility">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <table style="width:100%;">
                        <tr>
                            <th style="width:5%;text-align:center;">
                                <asp:LinkButton ID="btnValidated" runat="server" CommandArgument="Review_Ind" Text="Validated" OnClientClick="return DDLVIDVis('standard');" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpValidated" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownValidated" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:16%;text-align:left;">
                                <asp:LinkButton ID="btnEntity" runat="server" CommandArgument="Entity_Name" Text="Entity" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpEntity" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false"/>
                                <asp:Image ID="sortDownEntity" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:16%;text-align:left;">
                                <asp:LinkButton ID="btnFacilityName" runat="server" CommandArgument="Facility_Name" Text="Facility Name" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpFacilityName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownFacilityName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:16%;text-align:left;">
                                <asp:LinkButton ID="btnBusinessUnit" runat="server" CommandArgument="BusinessUnit_Name" Text="Business Unit" OnClick="HeaderColumn_Click" style="display:inline-block;" /></asp:LinkButton>
                                <asp:Image ID="sortUpBusinessUnit" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownBusinessUnit" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:7%;text-align:center;">
                                <asp:LinkButton ID="btnCostCenter" runat="server" CommandArgument="CostCenter" Text="Cost Center" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpCostCenter" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownCostCenter" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:6%;text-align:center;">
                                <asp:LinkButton ID="btnRollupCostCenter" runat="server" CommandArgument="RollupCostCenter" Text="Rollup CC" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpRollupCostCenter" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownRollupCostCenter" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:5%;text-align:center;">
                                <asp:LinkButton ID="btnStatus" runat="server" CommandArgument="Decommissioned_DT" Text="Status" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpStatus" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownStatus" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:5%;text-align:center;">
                                <asp:LinkButton ID="btnVisibility" runat="server" CommandArgument="Visible" Text="Visibility" OnClick="HeaderColumn_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpVisibility" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownVisibility" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:7%;text-align:center;">
                                <asp:LinkButton ID="btnPortalPage" runat="server" CommandArgument="PortalPage_Name" Text="Portal Page" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpPortalPage" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownPortalPage" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:5%;text-align:center">
                                <asp:LinkButton ID="btnAddedBy" runat="server" CommandArgument="AddedBy_Name" Text="Added By" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpAddedBy" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownAddedBy" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="text-align:center;">
                                <asp:LinkButton ID="btnAddedDate" runat="server" CommandArgument="Added_Dt" Text="Added Date" OnClick="HeaderColumn_Click" style="display:inline-block;" />
                                <asp:Image ID="sortUpAddedDate" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownAddedDate" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table id="tblMainGrid" runat="server" cellpadding="0" cellspacing="0" style="border-left:none;">
                        <tr style="text-align:center;" class="rowTable">
                            <td style="width:5%;">
                                <asp:Label ID="lblDMPKFacility" runat="server" style="display:none;" />
                                <asp:Label ID="lblValidated" runat="server" CssClass="validateLabel" tipsy="" onmouseover="TipsyVIDLBL();" />
                                <asp:DropDownList ID="ddlValidated" runat="server" CssClass="validateDDL disabledMode" RowIndex='<%# Container.DisplayIndex %>' tipsy="" onchange="StandardVIDChanged('Standard');bkgrndBlue();" onmouseover="TipsyVIDDDL()">
                                    <asp:ListItem Text="Y" Value="Y" />
                                    <asp:ListItem Text="N" Value="N" />
                                </asp:DropDownList>
                            </td>
                            <td style="width:16%;text-align:left;">
                                <asp:Label ID="lblEntity" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:16%;text-align:left;">
                                <asp:Label ID="lblFacilityName" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:16%;text-align:left;">
                                <asp:Label ID="lblBusinessUnit" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:7%;text-align:left;">
                                <asp:Label ID="lblCostCenter" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:6%;text-align:left;">
                                <asp:Label ID="lblRollupCostCenter" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:5%;text-align:left;">
                                <asp:Label ID="lblStatus" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:5%;text-align:center;">
                                <asp:Label ID="lblVisibility" runat="server" tipsy="" onmouseover="TipsyShow();" />
                            </td>
                            <td style="width:7%;text-align:left;">
                                <asp:Label ID="lblPortalPage" runat="server" tipsy="" onmouseover="TipsyShow();" />
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
    <csla:CslaDataSource ID="cslaDSFacilities" runat="server" TypeName="Hess.Corporate.GHGPortal.Business" TypeAssemblyName="Facilities" />