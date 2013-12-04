<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExecuteSSISView.ascx.cs" 
    Inherits="Hess.Corporate.GHGPortal.Web.UI.ExecuteSSISView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Csla" Namespace="Csla.Web" TagPrefix="csla" %>

    <asp:HiddenField ID="hfSort" runat="server" />
    <asp:GridView ID="ExecuteSSISGrid" runat="server" style="margin-top:2px;margin:auto;width:100%;" 
    AutoGenerateColumns="False" DataSourceID="cslaDSPackages" AllowSorting="True" 
    CssClass="gridView" EmptyDataText="No data for this selection." BorderStyle="None" GridLines="None" 
    CellPadding="0" AllowPaging="true" PageSize="15" EmptyDataRowStyle-CssClass="gridViewEmptyRow" 
    AlternatingRowStyle-CssClass="gridViewAlternatingRow" HeaderStyle-CssClass="gridViewHeader" 
    RowStyle-CssClass="gridViewRow" PagerStyle-CssClass="gridPager" CaptionAlign="Top" ShowHeaderWhenEmpty="True" DataKeyNames="DMPK_ExecutionRun">
        <Columns>
            <asp:BoundField DataField="DMPK_ExecutionRun" Visible="False" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="lblPackageName" runat="server" Text="Package Name" style="display:inline-block;color:White;" onmouseover="TipsyNav('nw');" Tipsy="The name of the data source that the data will be ran for." />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPackageName" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="lblStartRange" runat="server" Text="Start Range" style="display:inline-block;color:White;" onmouseover="TipsyNav('nw');" Tipsy="The start date for the data run." />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Textbox ID="txtStartRange" runat="server" CssClass="datepicker" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="lblEndRange" runat="server" Text="End Range" style="display:inline-block;color:White;" onmouseover="TipsyNav('nw');" Tipsy="The end date for the data run." />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Textbox ID="txtEndRange" runat="server" CssClass="datepicker" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="lblDateProcess" runat="server" Text="Date Process" style="display:inline-block;color:White;" onmouseover="TipsyNav('nw');" Tipsy="The date range format the data can be run. Either monthly or for a speicifc day range." />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkbtnDateProcess" runat="server" Visible ="false" OnClick="lnkbtnDateProcess_OnClick" style="margin-left:5px;" />
                    <asp:Label ID="lblDateProcess" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-VerticalAlign="Middle">
                <HeaderTemplate>
                    <asp:Label ID="lblProcessPackage" runat="server" Text="Process Package" style="display:inline-block;color:White;" onmouseover="TipsyNav('nw');" Tipsy="Determines whether the data source will extract data." />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkProcessPackage" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-VerticalAlign="Middle">
                <HeaderTemplate>
                    <asp:Label ID="lblClearTables" runat="server" Text="Clear Tables?" style="display:inline-block;color:White;" onmouseover="TipsyNav('nw');" Tipsy="Determines if and what previous package execution data will be backed out from the database." />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkClearTables" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:Label ID="lblClearPrevPackage" runat="server" Text="Clear out Previous Package?" style="display:inline-block;color:White;" onmouseover="TipsyNav('nw');" Tipsy="Determines whether the log history for previuos datasource executions will be cleared out." />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="ddlClearPrevPackage" runat="server" Width="100%">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BorderStyle="None" CssClass="gridViewHead" />
    </asp:GridView>

    <csla:CslaDataSource ID="cslaDSPackages" runat="server" TypeName="Hess.Corporate.GHGPortal.Business" TypeAssemblyName="Packages" />
