<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FacilityManagementView.ascx.cs" 
    Inherits="Hess.Corporate.GHGPortal.Web.UI.FacilityManagementView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Csla" Namespace="Csla.Web" TagPrefix="csla" %>

    <asp:HiddenField ID="hfSort" runat="server" />
    
    <table width="80%" style="margin:auto;">
        <tr style="width:100%;">
            <td width="100%">
                <asp:Label ID="lblIndPrimaryOwner" runat="server" Text="No Primary Owner assigned. Click here to assign Primary User." ForeColor="Red" style="position:absolute;margin-top:-12px;cursor:pointer;" Visible="false" onclick="$find('PopAddPrimaryOwner').show();" />
                <asp:GridView ID="FacilityManagementGrid" runat="server" style="margin-top:2px;margin:auto;width:100%;" 
                AutoGenerateColumns="False" DataSourceID="cslaDSFacilityManagement" AllowSorting="True" 
                CssClass="gridView" EmptyDataText="No data for this selection." 
                BorderStyle="None" GridLines="None" CellPadding="0" AllowPaging="true" PageSize="20"
                EmptyDataRowStyle-CssClass="gridViewEmptyRow" AlternatingRowStyle-CssClass="gridViewAlternatingRow"
                    HeaderStyle-CssClass="gridViewHeader" RowStyle-CssClass="gridViewRow" PagerStyle-CssClass="gridPager" 
                    CaptionAlign="Top" ShowHeaderWhenEmpty="True" 
                    ondatabound="FacilityManagementGrid_DataBound">
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDMPK" runat="server" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnDataOwner" runat="server" CommandArgument="DataOwner" Text="Owner Name" OnClick="btnDataOwner_Click" style="display:inline-block;color:White;" />
                                <asp:Image ID="sortUpDataOwner" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownDataOwner" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDataOwner" runat="server" />
                                <asp:DropDownList ID="ddlAddDataOwner" runat="server" Width="100%" Visible ="false">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnAddedDate" runat="server" CommandArgument="AddedDate" Text="Added Date" OnClick="btnAddedDate_Click" style="display:inline-block;color:White;" />
                                <asp:Image ID="sortUpAddedDate" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownAddedDate" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAddedDate" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnLastUpdatedDate" runat="server" CommandArgument="LastUpdatedDate" Text="Last Updated Date" OnClick="btnLastUpdatedDate_Click" style="display:inline-block;color:White;" />
                                <asp:Image ID="sortUpLastUpdatedDate" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownLastUpdatedDate" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLastUpdatedDate" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnLastUpdatedBy" runat="server" CommandArgument="LastUpdatedBy" Text="Last Updated By" OnClick="btnLastUpdatedBy_Click" style="display:inline-block;color:White;" />
                                <asp:Image ID="sortUpLastUpdatedBy" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownLastUpdatedBy" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLastUpdatedBy" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:LinkButton ID="btnPrimaryOwner" runat="server" CommandArgument="PrimaryOwner" Text="Primary Owner" OnClick="btnPrimaryOwner_Click" style="display:inline-block;color:White;" />
                                <asp:Image ID="sortUpPrimaryOwner" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownPrimaryOwner" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPrimaryOwner" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="disableUsers" rowindex='<%# Container.DisplayIndex %>' onclick="return DisableUser();"></div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BorderStyle="None" CssClass="gridViewHead" />
                </asp:GridView>
            </td>
            <td style="width:1%;vertical-align:top;">
                <div class="addUsers" onclick="return AddFacilityUser('Add');"></div>
                <br />
                <div class="copyUsers" onclick="return AddFacilityUser('Import');"></div>
            </td>
        </tr>
    </table>
    <csla:CslaDataSource ID="cslaDSFacilityManagement" runat="server" TypeName="Hess.Corporate.GHGPortal.Business" TypeAssemblyName="FacilityMgmt" />
