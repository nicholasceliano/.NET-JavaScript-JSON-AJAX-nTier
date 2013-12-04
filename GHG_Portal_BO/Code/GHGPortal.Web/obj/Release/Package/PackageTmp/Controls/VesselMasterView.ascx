<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VesselMasterView.ascx.cs" 
    Inherits="Hess.Corporate.GHGPortal.Web.UI.VesselMasterView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="Csla" Namespace="Csla.Web" TagPrefix="csla" %>
    <asp:HiddenField ID="hfSort" runat="server" />
    <asp:GridView ID="VesselMasterGrid" runat="server" style="margin-top:2px;" 
    AutoGenerateColumns="False" DataSourceID="cslaDSVessel" AllowSorting="True" 
    CssClass="gridView" EmptyDataText="No data for this selection." 
    BorderStyle="None" GridLines="None" CellPadding="0" AllowPaging="true" PageSize="25"
    EmptyDataRowStyle-CssClass="gridViewEmptyRow" AlternatingRowStyle-CssClass="gridViewAlternatingRow"
        HeaderStyle-CssClass="gridViewHeader" RowStyle-CssClass="gridViewRow" PagerStyle-CssClass="gridPager" 
        CaptionAlign="Top" ShowHeaderWhenEmpty="True" Width="100%" DataKeyNames="RowId">
        <Columns>
            <asp:BoundField DataField="RowId" Visible="False" />
            <asp:TemplateField>
                <HeaderTemplate>
                    <table cellpadding="0" cellspacing="0" style="text-align:left;width:100%; ">
                        <tr>
                            <th style="width:3%;text-align:center;">
                                <asp:LinkButton ID="btnValidated" runat="server" CommandArgument="Validated" Text="Vld" ValidationGroup="Comments" OnClientClick="return DDLVIDVis('vessel');" OnClick="btnValidated_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpValidated" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownValidated" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                             <th style="width:3%;text-align:center;">
                                <asp:LinkButton ID="btnNotes" runat="server" CommandArgument="Note" Text="Note" OnClick="btnNotes_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpNotes" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownNotes" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:5%;text-align:center;">
                                <asp:LinkButton ID="btnMoveNum" runat="server" CommandArgument="MoveNum" Text="Move #" OnClick="btnMoveNum_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpMoveNum" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownMoveNum" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:5%;text-align:center;">
                                <asp:LinkButton ID="btnRefMove" runat="server" CommandArgument="RefMoveNum" Text="Ref Move" OnClick="btnRefMove_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpRefMove" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownRefMove" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:3%;text-align:center;">
                                <asp:LinkButton ID="btnIR" runat="server" CommandArgument="IR" Text="IR" OnClick="btnIR_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpIR" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownIR" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:4%;text-align:center;">
                                <asp:LinkButton ID="btnStatus" runat="server" CommandArgument="Status" Text="Status" OnClick="btnStatus_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpStatus" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownStatus" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:12%;text-align:center;">
                                <asp:LinkButton ID="btnDisport" runat="server" CommandArgument="Disport" Text="Disport" OnClick="btnDisport_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpDisport" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownDisport" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:7%;text-align:center;">
                                <asp:LinkButton ID="btnDisDate" runat="server" CommandArgument="DisDate" Text="Disport Date" OnClick="btnDisDate_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpDisDate" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownDisDate" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:9%;text-align:center;">
                                <asp:LinkButton ID="btnTotDisVol" runat="server" CommandArgument="TotDisVol" Text="Discharge Vol(Gal)" OnClick="btnTotDisVol_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpTotDisVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownTotDisVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:8%;text-align:center;">
                                <asp:LinkButton ID="btnBLADESVol" runat="server" CommandArgument="Source_Volume" Text="SAP Volumes" OnClick="btnBLADESVol_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpBLADESVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownBLADESVol" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:3%;text-align:center;">
                                <asp:LinkButton ID="btnUOM" runat="server" CommandArgument="UOM" Text="UOM" OnClick="btnUOM_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpUOM" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownUOM" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:11%;text-align:left;">
                                <asp:LinkButton ID="btnVesselName" runat="server" CommandArgument="VesselName" Text="Vessel Name" OnClick="btnVesselName_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpVesselName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownVesselName" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:13%;text-align:left;">
                                <asp:LinkButton ID="btnHessPN" runat="server" CommandArgument="HessPN" Text="Hess Product Name" OnClick="btnHessPN_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpHessPN" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownHessPN" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                            <th style="width:13%;text-align:left;">
                                <asp:LinkButton ID="btnEPAPN" runat="server" CommandArgument="EPAPN" Text="EPA Product Name" OnClick="btnEPAPN_Click" style="display:inline-block;"></asp:LinkButton>
                                <asp:Image ID="sortUpEPAPN" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortup.gif" Visible="false" />
                                <asp:Image ID="sortDownEPAPN" runat="server" ImageUrl="~/App_Themes/Hess/Images/sortdown.gif" Visible="false" />
                            </th>
                           
                        </tr>
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table id="tblMainGrid"  runat="server" cellpadding="0" cellspacing="0" width="100%">
                        <tr class="rowTable">
                            <td style="width:3%;text-align:center;">
                                <asp:Label ID="lblValidated" CssClass="validateLabel" runat="server" tipsy="" onmouseover="TipsyVIDLBL();" />
                                <asp:DropDownList ID="ddlValidated" CssClass="validateDDL disabledMode" runat="server"  Width="100%" RowIndex='<%# Container.DisplayIndex %>' tipsy="" onchange="VesselVIDChanged();bkgrndBlue();" onmouseover="TipsyVIDDDL()">
                                    <asp:ListItem Text="Y" Value="Y"></asp:ListItem>
                                    <asp:ListItem Text="N" Value="N"></asp:ListItem>
                                    <asp:ListItem Text="X" Value="X"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:center;width:3%;">
                                <asp:LinkButton ID="imgNote" runat="server" OnClientClick="return false;" RowIndex='<%# Container.DisplayIndex %>' style="display:inline-block;" />
                                 <asp:Panel ID="pnlComments" CssClass="notesPopupPanel"  runat="server" style="width:260px;text-align:left;white-space:nowrap;display:none;">  
                                    <asp:TextBox ID="txtComments"  CssClass="txtComments TextBox"  runat="server" Text='<%# Bind("Comments") %>' Width="220px" BorderStyle="None"  RowIndex='<%# Container.DisplayIndex %>' ValidationGroup="Comments" onkeyup="TipsyExit(); boxExpand(this);" tipsy="" style="overflow:hidden;padding-left:5px;" onfocus="javascript:boxExpand(this);" onkeydown="javascript:boxExpand(this);" TextMode="MultiLine"  />
                                    <asp:Label ID="lblComments" runat="server" CssClass="disabledMode" />
                                    <span style="float:right;">
                                        <input type="button" class="cancelNotes" value="Cancel" />
                                        <input type="button" class="okNotes" value="OK" />
                                    </span>
                                </asp:Panel>
                                <ajax:PopupControlExtender ID="pceCommentsNotes"  runat="server" TargetControlID="imgNote" PopupControlID="pnlComments" Position="Right" CommitProperty="value"></ajax:PopupControlExtender>
                            </td>
                            <td style="width:5%;text-align:left;">
                                <asp:Label ID="lblMoveNum" runat="server" style="padding:0;" onmouseover="this.style.textDecoration='underline'" onmouseout="this.style.textDecoration='none'" />
                            </td>
                            <td style="width:5%;text-align:left;">
                                <asp:Label ID="lblRefMove" runat="server" style="padding:0;" onmouseover="this.style.textDecoration='underline'" onmouseout="this.style.textDecoration='none'" />
                            </td>
                            <td style="width:3%;text-align:center;">
                                <asp:LinkButton ID="imgIR" runat="server" CssClass="report" RowIndex='<%# Container.DisplayIndex %>' OnClientClick="open_IRList(); return false;" onmouseover="TipsyNav('n');" style="display: inline-block;" />
                            </td>
                            <td style="width:4%;text-align:center;"><asp:Label ID="lblStatus" runat="server" onmouseover="TipsyNav('ne')" style="padding:0;" /></td>
                            <td style="width:12%;text-align:center;"><asp:Label ID="lblDisport" runat="server" style="padding:0;" /></td>
                            <td style="width:7%;text-align:center;"><asp:Label ID="lblDisDate" runat="server" style="padding:0;" /></td>
                            <td style="width:9%;text-align:center;"><asp:Label ID="lblTotDisVol" runat="server" style="padding:0;" />
                                <asp:Panel ID="pnlRowRed" runat="server" style="text-align:left;">
                                    <asp:Label ID="lblInfoMsg" runat="server" Visible="false"  Text ="Information Message" CssClass="panelTitles"></asp:Label>
                                    <asp:Label ID="lblRowRed" runat="server" Visible="false" CssClass="panelText" />
                                    <br />
                                    <asp:Label ID="lblResolutionMsg" runat="server" Visible="false"  Text="Resolution Message" CssClass="panelTitles"></asp:Label>
                                    <asp:Label ID="lblRowRedDisclaimer" runat="server" Visible="false" CssClass="panelText" />
                                </asp:Panel>
                                <ajax:PopupControlExtender ID="pceComments" runat="server" TargetControlID="lblTotDisVol" PopupControlID="pnlRowRed" Position="Bottom" CommitProperty="value"></ajax:PopupControlExtender>
                            </td>
                            <td style="width:8%; text-align:center;"><asp:Label ID="lblBLADESVol" CssClass="sapBulkOrderVol" runat="server" onmouseout="this.style.textDecoration='none'" />
                                <asp:Panel ID="pnlBulkOrder" Width="600px" CssClass="notesPopupPanel" runat="server" style="text-align:left;">
                                    <asp:Label ID="lblGridResults" CssClass="lblGridResults" runat="server"></asp:Label>
                                </asp:Panel>
                                <ajax:PopupControlExtender ID="pceBulkOrder"  runat="server" OffsetX="-50" TargetControlID="lblBLADESVol" PopupControlID="pnlBulkOrder" Position="Bottom" CommitProperty="value"></ajax:PopupControlExtender>
                            </td>
                            <td style="width:3%; text-align:center;"><asp:Label ID="lblUOM" runat="server" /></td>
                            <td style="width:11%;text-align:left;"><asp:Label ID="lblVesselName" runat="server" style="padding:0;" /></td>
                            <td style="width:13%;text-align:left;"><asp:Label ID="lblHessPN" runat="server" onmouseover="TipsyShow()" tipsy="" style="padding:0;" /></td>
                            <td style="width:13%;text-align:left;"><asp:Label ID="lblEPAPN" runat="server" onmouseover="TipsyShow()" tipsy="" style="padding:0;" /></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <csla:CslaDataSource ID="cslaDSVessel" runat="server" TypeName="Hess.Corporate.GHGPortal.Business" TypeAssemblyName="VesselValidation" />
<script type="text/javascript" >

    function boxExpand(ta) {
        if (ta == null)
            return;
        if (ta.scrollHeight > 0) {
            ta.style.height = (ta.scrollHeight + 6) + "px";
        }
    }

    function pageLoad(){
        var $table = $('.gridView');

        $table.on("click", "td", function (event) {
            if ($(this).parent().hasClass('selectedRow')) {
                $(this).find('.validateLabel').dblclick();
            }
        });

        $('.sapBulkOrderVol').click(function(){
            var $bulkOrderVol = $(this);
            var jsondata = {
                'bulkOrder': $bulkOrderVol.attr('bulkOrderNo')
            };

            $.ajax({
                type: "POST",
                url: "/WebServices/GHGUtilServices.asmx/GetBulkOrderTable",
                data: JSON.stringify(jsondata),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $bulkOrderVol.next().find('.lblGridResults').html(msg.d).find('td:nth-child(2),td:nth-child(4),td:nth-child(9),th:nth-child(2),th:nth-child(4),th:nth-child(9)').hide();
                }
            });
        });
    }
</script>