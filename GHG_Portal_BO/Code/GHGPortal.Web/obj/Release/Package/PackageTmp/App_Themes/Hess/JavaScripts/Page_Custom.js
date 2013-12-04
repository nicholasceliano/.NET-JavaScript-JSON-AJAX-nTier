var StandardMasterGrid,
    VesselMasterGrid,
    ViewAllMasterGrid;

var jobRunDay = 16;

function SetGrids() {
        StandardMasterGrid = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid'),
        VesselMasterGrid = document.getElementById('MainContent_ctrlVessel_VesselMasterGrid'),
        ViewAllMasterGrid = document.getElementById('MainContent_ctrlViewAll_ViewAllMasterGrid');
}

$(document).ready(function () {
    $('ul.sf-menu').superfish();

    if (document.URL.indexOf('AssetManagement') !== -1) {
        SearchPanelLoadAssetManagement();
    }
    else if(document.URL.indexOf('FacilityManagement') !== -1) {
        SearchPanelLoadFacilityManagement();
    } 
    else {
        if (document.URL.indexOf('Vessel') == -1) {
            SearchPanelLoad('STANDARD');
        } else {
            SearchPanelLoad('VESSEL');
        }
    }
});

var GHGCore = {
    warningIcon: "<span class=\"ui-state-error\"><span class=\"ui-icon ui-icon-alert\" style=\"float: left; margin-right: .3em;\"></span></span>"
};

//#region Collapse/Expand Functions

function fnToggleAll() {
    var e = event.srcElement || event.target;       // Get source element (IE) or Target (other browsers)
    var expanding = (e.src.indexOf('plus.gif') >= 0);

    var colExp = $("[id*=imgToggle]");              // get collection of +/- pictures on all rows
    var i = 0;
    while (colExp[i]) {
        if (expanding && (colExp[i].src.indexOf('plus.gif') >= 0) || !expanding && (colExp[i].src.indexOf('minus.gif') >= 0))
            colExp[i].click();                      // click all of them
        i++;
    }
    $("[id*=imgAllRows]").toggle();                 // hide/show appropriate +/- picture
}

function imgToggle_Click() {
    var e = event.srcElement || event.target,
        rowNum = e.id.replace('MainContent_ctrlStandard_StandardMasterGrid_imgToggle_', ''),
        imgNote = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_imgNote_' + rowNum);

    if (imgNote !== null)
    {
        if (imgNote.style.display == 'block') {
            var pnlCommentsCPE = $find('MainContent_ctrlStandard_StandardMasterGrid_pnlCommentsCPE_' + rowNum),
                pnlDetailCollapsiblePanelExtender = $find('MainContent_ctrlStandard_StandardMasterGrid_pnlDetailCollapsiblePanelExtender_' + rowNum);

            if (pnlCommentsCPE.get_Collapsed() && pnlDetailCollapsiblePanelExtender.get_Collapsed()) {
                pnlCommentsCPE._doOpen();
            } else if (pnlCommentsCPE.get_Collapsed() == false && pnlDetailCollapsiblePanelExtender.get_Collapsed()) {
                return;
            } else if (pnlCommentsCPE.get_Collapsed() && pnlDetailCollapsiblePanelExtender.get_Collapsed() == false) {
                pnlCommentsCPE._doClose();
            }
            else {
                pnlCommentsCPE._doClose();
            }
        }
    }
}

//#endregion

//#region Save Functions for VID

function StandardVIDChanged(page) {
    var DDL = event.srcElement || event.target,
        rowNum = DDL.id.replace('MainContent_ctrl' + page + '_' + page + 'MasterGrid_ddlValidated_', ''),
        lbl = document.getElementById('MainContent_ctrl' + page + '_' + page + 'MasterGrid_lblValidated_' + rowNum + ''),
        RFVname = 'MainContent_ctrl' + page + '_' + page + 'MasterGrid_rfvMonthVol_' + rowNum,
        RFV = document.getElementById(RFVname),
        sortValue = document.getElementById('MainContent_hfSortValue'),
        ddlFacilities = document.getElementById('MainContent_ddlFacilities');
    if (rowNum == "0") {
        sortValue.value = DDL.value;
    }

    if (DDL.value !== lbl.innerText) {
        enableLeaveFunctions(true);
    } else {
        enableLeaveFunctions(false);
    }

        //if the ddl is not equal to n the RFV for the amount must be filled in
    if (ddlFacilities.options[ddlFacilities.selectedIndex].innerText.indexOf('.ALL') == -1) {
        if (DDL.value !== 'N') {
            ValidatorEnable(RFV, true);
        } else {
            ValidatorEnable(RFV, false);
        }
    }
}

function VesselVIDChanged() {
    var DDL = event.srcElement || event.target,
        rowNum = DDL.id.replace('MainContent_ctrlVessel_VesselMasterGrid_ddlValidated_', ''),
        lbl = document.getElementById('MainContent_ctrlVessel_VesselMasterGrid_lblValidated_' + rowNum),
        sortValue = document.getElementById('MainContent_hfSortValue');
    if (rowNum == "0") {
        sortValue.value = DDL.value;
    }

    if (DDL.value !== lbl.innerText) {
        enableLeaveFunctions(true);
    } else {
        enableLeaveFunctions(false);
    }
}

//#endregion

function AdminManagementInfoChanged() {
    var DDL = event.srcElement || event.target,
        control = DDL.id.indexOf('ddl') != -1 ? DDL.id.replace('MainContent_ddl', '') : DDL.id.replace('MainContent_txt', ''),
        lblName = 'MainContent_lbl' + control,
        lbl = document.getElementById(lblName);
        
        if (DDL.value !== lbl.innerText){
            enableLeaveFunctions(true);
        } else {
            enableLeaveFunctions(false);
        }
}

//#region RVF Functions
function RFVEnable() {
    var textbox = event.srcElement || event.target,
    rowNum = textbox.id.replace('MainContent_ctrlStandard_StandardMasterGrid_txtThisMonthVol_', ''),
    comments = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_txtComments_' + rowNum),
    dataSource = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_lblDataSource_' + rowNum),
    hiddenField = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_hfThisMonthVolume_' + rowNum),
    RFV = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_rfvComments_' + rowNum);

    if ($.trim(hiddenField.value) != textbox.value && dataSource.innerText == "MANUAL") {
        $('#' + textbox.id).tipsy('hide');
        $('#' + comments.id).tipsy('hide');
        ValidatorEnable(RFV, false);
        RFV.disabled = true;
        enableLeaveFunctions(true);
    } else if ($.trim(hiddenField.value) != textbox.value && dataSource.innerText != "MANUAL" && (textbox.value.length >= 0 && $.trim(hiddenField.value) != "0")) {
        ValidatorEnable(RFV, true);
        RFV.disabled = false;
        enableLeaveFunctions(true);
    } else if ($.trim(hiddenField.value) != textbox.value) {
        enableLeaveFunctions(true);
    } else {
        $('#' + textbox.id).tipsy('hide');
        $('#' + comments.id).tipsy('hide');
        ValidatorEnable(RFV, false);
        RFV.disabled = true;
        enableLeaveFunctions(false);
    }

    var DDL = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_ddlValidated_' + rowNum),
        RFVMonthVol = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_rfvMonthVol_' + rowNum);

    if (DDL.value !== 'N') {
        ValidatorEnable(RFVMonthVol, true);
        enableLeaveFunctions(true);
    }
}

function RFVIndicatorShow() {
    if (StandardMasterGrid == null) {
    }
    else if (StandardMasterGrid !== null) {
        StdIndicatorShow();
    }
}

function StdIndicatorShow() {
    var errorCount = 0;
    SetGrids();
    for (var i = 0; i < gridRowCount(StandardMasterGrid.rows.length) ; i++) {
        var imgNote = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_imgNote_' + i),
            thisMonthVol = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_txtThisMonthVol_' + i),
            hfThisMonthVol = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_hfThisMonthVolume_' + i),
            comments = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_txtComments_' + i),
            commentsIcon = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_imgNote_' + i),
            commentsRFV = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_rfvComments_' + i),
            thisMonthVolRFV = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_rfvMonthVol_' + i),
            hfMonthlyVol = document.getElementById('MainContent_hfMonthlyVol'),
            hfComments = document.getElementById('MainContent_hfComments'),
            hfDDL = document.getElementById('MainContent_hfDDL');

        if (commentsRFV !== null && thisMonthVol !== null) {
        //if the VID DDL is NOT N
            var DDL = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_ddlValidated_' + i),
                RFVMonthVol = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_rfvMonthVol_' + i),
                textbox = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_txtThisMonthVol_' + i),
                dataSource = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_lblDataSource_' + i);

            if (DDL.value !== 'N' && $.trim(dataSource.innerText) !== 'MANUAL' && replaceAll(thisMonthVol.value, ',', '') !== hfThisMonthVol.value && comments.value.length == 0) {
                ValidatorEnable(commentsRFV, true);
                //imgNote.className = 'noteRequired'
                enableLeaveFunctions(true);
            }
            else {
                ValidatorEnable(commentsRFV, false);
                enableLeaveFunctions(false);
            }

            if (DDL.value !== 'N' && textbox.value.length == 0) {
                ValidatorEnable(RFVMonthVol, true);
                enableLeaveFunctions(true);
            }
            else {
                ValidatorEnable(RFVMonthVol, false);
                enableLeaveFunctions(false);
            }

            if (commentsRFV.enabled == true) {
                $('#' + commentsIcon.id).attr('tipsy', GHGCore.warningIcon + 'Comment Required').tipsy({ trigger: 'manual', gravity: 'e' });
                $('#' + commentsIcon.id).tipsy('show');
                //if comments length is nothing
                if (comments.value.length == 0) {
                    errorCount = errorCount + 1;
                }
            }
            else {
                $('#' + comments.id).tipsy('hide');
            }

            if (thisMonthVolRFV.enabled == true) {
                $('#' + thisMonthVol.id).attr('tipsy', GHGCore.warningIcon + 'Value Required').tipsy({ trigger: 'manual', gravity: 'w' });
                $('#' + thisMonthVol.id).tipsy('show');

                //if monthvol lenght is nothing
                if (thisMonthVol.value.length ==  0){
                    errorCount = errorCount + 1;
                }
            } 
            else {
                $('#' + thisMonthVol.id).tipsy('hide');
            }
        }
    }
    if (errorCount > 0) {
        document.getElementById("MainContent_btnCancel").click();
    }
}

function VesselRFVIndicatorShow() {
    var errorCount = 0;
    SetGrids();
    for (var i = 0; i < gridRowCount(VesselMasterGrid.rows.length) ; i++) {
        var DDL = document.getElementById('MainContent_ctrlVessel_VesselMasterGrid_ddlValidated_' + i),
            comments = document.getElementById('MainContent_ctrlVessel_VesselMasterGrid_txtComments_' + i),
            commentsRFV = document.getElementById('MainContent_ctrlVessel_VesselMasterGrid_rfvComments_' + i);

        if (commentsRFV !== null) {

            if (DDL.value == 'X') {
                ValidatorEnable(commentsRFV, true);
                enableLeaveFunctions(true);
            } else {
                ValidatorEnable(commentsRFV, false);
                enableLeaveFunctions(false);
            }

            if (commentsRFV.enabled == true) {
                $('#' + comments.id).attr('tipsy', GHGCore.warningIcon + 'Comment Required').tipsy({ trigger: 'manual', gravity: 'e' });
                $('#' + comments.id).tipsy('show');
                //if comments length is nothing
                if (comments.value.length == 0) {
                    errorCount = errorCount + 1;
                }
            }
            else {
                $('#' + comments.id).tipsy('hide');
            }
        }
    }
    if (errorCount > 0) {
        document.getElementById("MainContent_btnCancel").click();
    }
}

function AssetManagementRFVIndicatorShow() {
    var rfvAssetName = document.getElementById('MainContent_rfvAssetName'),
        txtAssetName = document.getElementById('MainContent_txtAssetName'),
        rfvFacility = document.getElementById('MainContent_rfvFacility'),
        ddlFacility = document.getElementById('MainContent_ddlFacility'),
        rfvAssetType = document.getElementById('MainContent_rfvAssetType'),
        ddlAssetType = document.getElementById('MainContent_ddlAssetType'),
        rfvProductName = document.getElementById('MainContent_rfvProductName'),
        ddlProductName = document.getElementById('MainContent_ddlProductName'),
        rfvUOM = document.getElementById('MainContent_rfvUOM'),
        ddlUOM = document.getElementById('MainContent_ddlUOM'),
        rfvSourceCategory = document.getElementById('MainContent_rfvSourceCategory'),
        ddlSourceCategory = document.getElementById('MainContent_ddlSourceCategory'),
        rfvMeasurementType = document.getElementById('MainContent_rfvMeasurementType'),
        ddlMeasurementType = document.getElementById('MainContent_ddlMeasurementType'),
        rfvMeasurementMethod = document.getElementById('MainContent_rfvMeasurementMethod'),
        ddlMeasurementMethod = document.getElementById('MainContent_ddlMeasurementMethod'),
        rfvAssetDataName = document.getElementById('MainContent_rfvAssetDataName'),
        ddlAssetDataName = document.getElementById('MainContent_ddlAssetDataName'),
        errorCount = 0;

    errorCount = AdminMgmtLoop(txtAssetName, rfvAssetName, errorCount);
    errorCount = AdminMgmtLoop(ddlFacility, rfvFacility, errorCount);
    errorCount = AdminMgmtLoop(ddlAssetType, rfvAssetType, errorCount);
    errorCount = AdminMgmtLoop(ddlProductName, rfvProductName, errorCount);
    errorCount = AdminMgmtLoop(ddlUOM, rfvUOM, errorCount);
    errorCount = AdminMgmtLoop(ddlSourceCategory, rfvSourceCategory, errorCount);
    errorCount = AdminMgmtLoop(ddlMeasurementType, rfvMeasurementType, errorCount);
    errorCount = AdminMgmtLoop(ddlMeasurementMethod, rfvMeasurementMethod, errorCount);
    errorCount = AdminMgmtLoop(ddlAssetDataName, rfvAssetDataName, errorCount);

    if (errorCount > 0) {
        document.getElementById("MainContent_btnCancel").click();
    }
}

function AdminDataSourceChanged() {
    var DDL = event.srcElement || event.target,
        txtTagID = document.getElementById('MainContent_txtTagID');

    if (DDL.value == 'MANUAL PORTAL') {
        txtTagID.style.display = 'none';
    }
    else if (DDL.value !== 'MANUAL PORTAL') {
        txtTagID.style.display = 'inline-block';
    }
}

function FacilityManagementRFVIndicatorShow() {
    var rfvFacilityName = document.getElementById('MainContent_rfvFacilityName'),
        txtFacilityName = document.getElementById('MainContent_txtFacilityName'),
        rfvBusinessUnit = document.getElementById('MainContent_rfvBusinessUnit'),
        ddlBusinessUnit = document.getElementById('MainContent_ddlBusinessUnit'),
        rfvEntity = document.getElementById('MainContent_rfvEntity'),
        ddlEntity = document.getElementById('MainContent_ddlEntity'),
        rfvPortalPage = document.getElementById('MainContent_rfvPortalPage'),
        ddlPortalPage = document.getElementById('MainContent_ddlPortalPage'),
        errorCount = 0;

    errorCount = AdminMgmtLoop(txtFacilityName, rfvFacilityName, errorCount);
    errorCount = AdminMgmtLoop(ddlBusinessUnit, rfvBusinessUnit, errorCount);
    errorCount = AdminMgmtLoop(ddlEntity, rfvEntity, errorCount);
    errorCount = AdminMgmtLoop(ddlPortalPage, rfvPortalPage, errorCount);

    if (errorCount > 0) {
        document.getElementById("MainContent_btnCancel").click();
    }
}

function AdminMgmtLoop(control, rfv, errorCount) {

    if (control !== null || rfv !== null) {
        if (control.value == '' && control.style.display !== "none"){
            ValidatorEnable(rfv, true);
            rfv.style.display = 'inline-block';
            $('#' + control.id).attr('tipsy', GHGCore.warningIcon + 'Value Required').tipsy({ trigger: 'manual', gravity: 'nw' });
            $('#' + control.id).tipsy('show');
            errorCount = errorCount + 1;
        }
        return errorCount;
    }
    else {
        return errorCount;
    }
}
//#endregion

//#region Validation Functions

function Validate() {
    if (StandardMasterGrid == null)
        return true;
    else if (StandardMasterGrid !== null)
        return StdValidate();
}

function StdValidate() {
    var isValid = false;
    
    isValid = Page_ClientValidate('Comments');
    if (isValid) {
        isValid = Page_ClientValidate('MonthVol');
    }

    if (isValid)
    {
        var hfChangeDetector = document.getElementById('MainContent_hfChangeDetector');
        hfChangeDetector.value = 0;
    }
    return isValid;
}

function ValidateAdminMgmt(ValidationGroup) {
    var hfNewAsset = document.getElementById('MainContent_hfNewAsset'),
        txtAssetName = document.getElementById('MainContent_txtAssetName'),
        ddlAssets = document.getElementById('MainContent_ddlAsset');

    if (hfNewAsset.value == 'New Asset') {
        for (i = 1; i < ddlAssets.length; i++) {
            if (ddlAssets.options[i].text == txtAssetName.value) {
                $('#' + txtAssetName.id).attr('tipsy', GHGCore.warningIcon + ' Unable to Save - Asset already exists').tipsy({ trigger: 'manual', gravity: 'nw' });
                $('#' + txtAssetName.id).tipsy('show');
                return false;
            }
        }
    }

    var isValid = false;
    isValid = Page_ClientValidate(ValidationGroup);
    return isValid;
}

function enableLeaveFunctions(truefalse) {
    var hfChangeDetector = document.getElementById('MainContent_hfChangeDetector'),
        hfNumberic = parseInt(hfChangeDetector.value);

    if (hfChangeDetector.value = 'NaN')
        hfNumberic = 0;

    if (truefalse) {
        hfChangeDetector.value = hfNumberic + 1;
    } else if (!truefalse) {
        hfChangeDetector.value = hfNumberic - 1;
    }
}

//#endregion

//#region Misc Click Functions

function CancelEdit() {
    var hfChangeDetector = document.getElementById('MainContent_hfChangeDetector'),
        hfCancelButton = document.getElementById('MainContent_hfCancelButton'),
        hfNumberic = parseInt(hfChangeDetector.value);
    if (hfNumberic > 0 || hfNumberic < 0) {
        
        $find('PopVerifyChanges').show();
        hfCancelButton.value = event.srcElement;

        SetSearch();
        return false;
    }
    hfChangeDetector.value = '';
    SetSearch();
    return true;
}

function ClearErrors() {
    var hfChangeDetector = document.getElementById('MainContent_hfChangeDetector');
    hfChangeDetector.value = '';
}

function replaceAll(txt, replace, with_this) {
    return txt.replace(new RegExp(replace, 'g'), with_this);
}

//#endregion

//#region Save Page Functions

function DisableUser() {
    var disableBtn = event.srcElement || event.target,
        rowIndex = disableBtn.rowindex,
        hfDisableRowIndex = document.getElementById('MainContent_hfDisableRowIndex');

    hfDisableRowIndex.value = rowIndex;

    $find('PopVerifyDisable').show();
    return false;
}

function AddFacilityUser(popType) {
    var hfNewFacility = document.getElementById('MainContent_hfNewFacility'),
        lblFacilityName = document.getElementById('MainContent_lblFacilityName');
    
    if (hfNewFacility.value == 'New Facility' || lblFacilityName.innerText.length == 0)
        $find('PopErrorMsg').show();
    else {
        if (popType == 'Add') {
            $find('PopAddFacilityUser').show();
        }
        else if (popType == 'Import') {
            $find('PopImportFacilityUsers').show();
        }
    }
    return false;
}

function DDLVIDVis(page) {
    var VID = event.srcElement || event.target,
        grid = document.getElementById('MainContent_ctrl' + page + '_' + page + 'MasterGrid'),
        rowZeroDDL = document.getElementById('MainContent_ctrl' + page + '_' + page + 'MasterGrid_ddlValidated_0');

    if (rowZeroDDL != null) {
        for (var i = 0; i < gridRowCount(grid.rows.length); i++) {
            var DDL = document.getElementById('MainContent_ctrl' + page + '_' + page + 'MasterGrid_ddlValidated_' + i),
                VID = document.getElementById('MainContent_ctrl' + page + '_' + page + 'MasterGrid_lblValidated_' + i);
            DDL.value = rowZeroDDL.value;
            VID.innerText = rowZeroDDL.value;
            VID.parentNode.style.background = '#CEE6FF';
        }
        return false;
    }
    else {
        return false;//don't let the VID ever sort
    }
}

function CheckSaveChanges() {
    var ddlFacilities = document.getElementById('MainContent_ddlFacilities');
    if (ddlFacilities.value.indexOf('.ALL') !== -1) {
        $find('PopCannotSaveChanges').show();
        return false;
    }
    else {
        return true;
    }
}

//#endregion

//#region Link Bar - Main Vailidation Page

function open_Workflow() {
    var width = 1100,
        height = 875,
        left = parseInt((screen.availWidth / 2) - (width / 2)),
        top = parseInt((screen.availHeight / 2) - (height / 2));
    var windowFeatures = "width=" + width + ",height=" + height + ",status,resizable,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
    $("#auditHistory").remove();
    var diag = $('<div id="auditHistory" title="Workflow History"><span class="dialogMsg"></span></div>');
    diag.dialog({
        resizable: true,
        minWidth: 1220,
        minHeight: 600,
        autoOpen: true,
        modal: true,
        dialogClass: "no-close",
        create: function (event, ui) {
            var widget = $(this).dialog("widget");
            $(".ui-dialog-titlebar-close span", widget)
          .removeClass("ui-icon-closethick")
          .addClass("ui-icon-close");
        },
        closeButton: "<div class='iWindowTitle'><div class='closeMsg inlineBlock' onclick='$(\"#auditHistory\").fadeOut();'>X</div></div>",
        closeOnEscape: true,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        },
        close: function (event, ui) {
            $(this).dialog("destroy");
            $("#auditHistory").remove();

        }
    });

    var jsondata = {
        'month': monthSC,
        'year': yearSC,
        'facility': facNameSC,
        'entity': entNameSC
    };

    $.ajax({
        type: "POST",
        url: "/WebServices/GHGUtilServices.asmx/RenderWorkFlowHistory",
        data: JSON.stringify(jsondata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#tempDiv').html(msg.d);
            createPager();
        }
    });
    return true;
}

function open_ProcessHistoryLog() {
    var txtMonthYear = document.getElementById("MainContent_txtMonthYear");
    var month = txtMonthYear.value.split('/')[0];
    var year = txtMonthYear.value.split('/')[1];
    var width = 1100;
    var height = 875;
    var left = parseInt((screen.availWidth / 2) - (width / 2));
    var top = parseInt((screen.availHeight / 2) - (height / 2));
    var windowFeatures = "width=" + width + ",height=" + height + ",status,resizable,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;

    $("#auditHistory").remove();
    var diag = $('<div id="auditHistory" title="Process Log History"><span class="dialogMsg"></span></div>');
    diag.dialog({
        resizable: true,
        minWidth: 700,
        minHeight: 600,
        autoOpen: true,
        modal: true,
        create: function (event, ui) {
            var widget = $(this).dialog("widget");
            $(".ui-dialog-titlebar-close span", widget)
          .removeClass("ui-icon-closethick")
          .addClass("ui-icon-close");
        },
        dialogClass: "no-close",
        closeButton: "<div class='iWindowTitle'><div class='closeMsg inlineBlock' onclick='$(\"#auditHistory\").fadeOut();'>X</div></div>",
        closeOnEscape: true,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        },
        close: function (event, ui) {
            $(this).dialog("destroy");
            $("#auditHistory").remove();
        }
    });

    var jsondata = {
        'year': year,
        'month': month
    };

    $.ajax({
        type: "POST",
        url: "/WebServices/GHGUtilServices.asmx/RenderProcessLogHistory",
        data: JSON.stringify(jsondata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#tempDiv').html(msg.d);
            createPager();
        }
    });
}

function createPager() {
    var pager = [];
    var pageSize = 25;
    var $tempDiv = $('#tempDiv');
    var $auditHistory = $('#auditHistory');
    var numEntries = $tempDiv.find('tbody tr').length;
    var numPages = Math.ceil(numEntries / pageSize);
    pager.push('<div id="searchPager" class="gridPager"><table><tr>');
    for (var i = 1; i <= numPages; i++) {
        pager.push('<td><a class="page" href="javascript:void(0);">' + i + '</a></td>');
    }
    pager.push('</tr></table></div>');
    pager = pager.join('');
    $auditHistory.append(pager);
    $auditHistory.find('.dialogMsg').append("<table cellpadding='0' cellspacing='0' style='table-layout:fixed;width:100%;border-collapse:collapse;' class='gridView'><thead></thead><tbody></tbody></table>");
    $auditHistory.find('.dialogMsg thead').replaceWith($tempDiv.find('thead').clone());
    $('#searchPager a').bind('click', function () {
        var pageIndex = $(this).text() * 1;
        var offset = (pageIndex - 1) * pageSize;
        var newContent = $tempDiv.find('tbody tr').slice(offset, offset + pageSize).clone();
        $auditHistory.find('.dialogMsg tbody').empty().append(newContent);
        $('#auditHistory .dialogMsg tbody td').css({ 'border-bottom': '1px solid buttonface' });
    });
    $('#searchPager a').first().click();
}

//#endregion

function open_IRList() {
    var width = 1100,
        height = 875,
        left = parseInt((screen.availWidth / 2) - (width / 2)),
        top = parseInt((screen.availHeight / 2) - (height / 2)),
        windowFeatures = "width=" + width + ",height=" + height + ",status,resizable,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top,
        imgIR = event.srcElement || event.target,
        rowNumber = imgIR.id.replace('MainContent_ctrlVessel_VesselMasterGrid_imgIR_', ''),
        lblMoveNum = document.getElementById('MainContent_ctrlVessel_VesselMasterGrid_lblMoveNum_' + rowNumber),
        moveNumber = lblMoveNum.innerText.substring(0, 6);

    $("#auditHistory").remove();
    var diag = $('<div id="auditHistory" title="Inspection Report List"><span class="dialogMsg"></span></div>');
    diag.dialog({
        resizable: true,
        minWidth: 1220,
        minHeight: 600,
        autoOpen: true,
        modal: true,
        create: function (event, ui) {
            var widget = $(this).dialog("widget");
            $(".ui-dialog-titlebar-close span", widget)
          .removeClass("ui-icon-closethick")
          .addClass("ui-icon-close");
        },
        dialogClass: "no-close",
        closeButton: "<div class='iWindowTitle'><div class='closeMsg inlineBlock' onclick='$(\"#auditHistory\").fadeOut();'>X</div></div>",
        closeOnEscape: true,
        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        },
        close: function (event, ui) {
            $(this).dialog("destroy");
            $("#auditHistory").remove();
        }
    });

    var jsondata = {
        'moveNumber': moveNumber
    };
    
    $.ajax({
        type: "POST",
        url: "/WebServices/DocManagerServices.asmx/GetDocuments",
        data: JSON.stringify(jsondata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#tempDiv').html(msg.d);
            createPager();
            
        }
    });
}

//#region Calendar Functions

function SetCalendarModeMonth() {
    var CalendarBehavior = $find("MonthCalendarBehavior");

    CalendarBehavior._switchMode("months", true);

    if (CalendarBehavior._monthsBody) {
        for (var i = 0; i < CalendarBehavior._monthsBody.rows.length; i++) {
            var row = CalendarBehavior._monthsBody.rows[i];
            for (var j = 0; j < row.cells.length; j++) {
                Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", callMonth);
            }
        }
    }
}

function HideCalendar(calendar) {
    calendar.hide();
    calendar.get_element().blur();
}

function callMonth(eventElement) {
    var target = eventElement.target;
    var cal = $find("MonthCalendarBehavior");
    switch (target.mode) {
        case "month":
            //if today is the 15th target.date can't be more than -2 months
            var today = new Date();

            if (today.getDate() < jobRunDay) {
                if (target.date < today.setMonth(today.getMonth() - 2)) {
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                }
            }
            else {
                if (target.date < today.setMonth(today.getMonth() - 1)) {
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                }
            }
            cal.raiseDateSelectionChanged();
            break;
    }
}

//#endregion

//#region Tipsy Custom

//#region DDL ToolTips

function ToolTipShowDDL() {
    var e = event.srcElement || event.target;       // Get source element (IE) or Target (other browsers)
    var t = (e.selectedIndex != -1) ? $.trim(e[e.selectedIndex].text) : null;
    e.tipsy = (e.selectedIndex != -1) ? $.trim(e[e.selectedIndex].text) : null;
    if (t && t.length * 6.75 > parseInt(e.style.width) - 15) {
        $('#' + e.id + '').tipsy();
    }
}

function TipsyVIDDDL() {
    var e = event.srcElement || event.target;
    if (e.value == 'N') {
        e.tipsy = 'No';
    } else if (e.value == 'Y') {
        e.tipsy = 'Yes';
    } else if (e.value == 'X') {
        e.tipsy = 'Yes, but do not send to ENVIANCE';
    } else if (e.value == 'I') {
        e.tipsy = 'Yes, but received new value';
    } else {
        e.tipsy = 'error'
    }
    $('#' + e.id + '').tipsy({ gravity: 'nw' });
}

//#endregion

//#region Specific Location ToolTips
function TipsyVIDLBL() {
    var e = event.srcElement || event.target;
    if (e.innerText == 'N') {
        e.tipsy = 'No';
    } else if (e.innerText == 'Y') {
        e.tipsy = 'Yes';
    } else if (e.innerText == 'X') {
        e.tipsy = 'Yes, but do not send to ENVIANCE';
    } else if (e.innerText == 'I') {
        e.tipsy = 'Yes, but received new value';
    } else {
        e.tipsy = 'error'
    }
    $('#' + e.id + '').tipsy({ gravity: 'nw' });
}

function TipsyShowTipsy() {
    var e = event.srcElement || event.target;
    $('#' + e.id + '').tipsy({ gravity: 'nw' });
}

function TipsyNav(location) {
    var e = event.srcElement || event.target;
    $('#' + e.id + '').tipsy({ gravity: location });
}

//#endregion

//#region Generic Tipsy Functions

function TipsyShow() {
    var e = event.srcElement || event.target;
    e.tipsy = e.innerText;
    $('#' + e.id + '').tipsy({ gravity: 'n' });
}

function TipsyShowPrev() {
    var e = event.srcElement || event.target;
    e.tipsy = $.trim(e.parentNode.previousSibling.innerText);
    $('#' + e.id + '').tipsy({ gravity: 'nw' });
}

function TipsyExit() {
    var e = event.srcElement || event.target;
    $('#' + e.id + '').tipsy('hide');
}

//#endregion

//#endregion

function AssetAdminCheckSearch() {
    var ddlAsset = document.getElementById('ddlAsset'),
        txtAsset = $('#txtAsset')[0],
        lblMsg = document.getElementById('MainContent_lblMsg');

    if (ddlAsset.disabled == true || txtAsset.disabled == true || (((ddlAsset.options.length > 0 ? ddlAsset.options[ddlAsset.selectedIndex].innerText : '') == '' && ddlAsset.style.display != 'none') || (txtAsset.value == '' && txtAsset.style.display != 'none'))) {
        lblMsg.innerText = 'Please select an Asset Name parameter(marked with red asterisk)';
        lblMsg.className = "errorMessage";
        return false;
    }
    else {
        lblMsg.innerText = "";
        AssetAdminSetSearch();
        return true;
    }
}

function FacAdminCheckSearch() {
    var ddlFacility = document.getElementById('MainContent_ddlFacilities'),
        lblMsg = document.getElementById('MainContent_lblMsg');

    if (ddlFacility.disabled == true || ddlFacility.options[ddlFacility.selectedIndex].innerText == '') {
        lblMsg.innerText = 'Please select a Facility Name parameter(marked with red asterisk)';
        lblMsg.className = "errorMessage";
        return false;
    }
    else {
        lblMsg.innerText = "";
        FacAdminSetSearch();
        return true;
    }
}

function checkSearch() {
    var lblMsg = document.getElementById('MainContent_lblMsg');
    var ddlFacValue = ddlFacility.value == '' ? 0 : ddlFacility.value;

    if (ddlFacility.options[ddlFacValue].innerText == '' && ddlEntity.options[ddlEntity.value].innerText == '') {
        if (ddlEntity.options[ddlEntity.value].innerText == 'NORTH DAKOTA')
            lblMsg.innerText = 'Please select a Facility parameter (marked with red asterisk)';
        else
            lblMsg.innerText = 'Please select an Entity or Facility parameters (marked with red asterisk)';

        lblMsg.className = "errorMessage";
        return false;
    }
    else {
        lblMsg.innerText = "";
        return CancelEdit();
    }
}

function clearMsg() {
    var lblMsg = document.getElementById('MainContent_lblMsg');
    lblMsg.innerText = "";
}

function MultiplePageStandardGridSave() {
    var hfRowCount = document.getElementById('MainContent_ctrlStandard_hfRowCount'),
        hfVID = document.getElementById('MainContent_ctrlStandard_hfVID'),
        hfMonthlyVolume = document.getElementById('MainContent_ctrlStandard_hfMonthlyVolume'),
        hfComments = document.getElementById('MainContent_ctrlStandard_hfComments'),
        hfRowKey = document.getElementById('MainContent_ctrlStandard_hfRowKey'),
        lbViewAll = document.getElementById('MainContent_lbViewAll'),
        comments = '',
        vid = '',
        monthlyValue = '',
        rowKey = '';
    SetGrids();

    if (StandardMasterGrid !== null) {
        for (var i = 0; i < gridRowCount(StandardMasterGrid.rows.length); i++) {
            var txtComments = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_txtComments_' + i),
                ddlValidated = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_ddlValidated_' + i),
                txtThisMonthVol = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_txtThisMonthVol_' + i),
                lblDMPKEmissions = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_lblDMPKEmissions_' + i);

            comments = comments + txtComments.value + '|';
            vid = vid + ddlValidated.value + '|';
            monthlyValue = monthlyValue + replaceAll(txtThisMonthVol.value, ',', '') + '|';
            rowKey = rowKey + lblDMPKEmissions.innerText + '|';
        }

        hfRowCount.value = gridRowCount;
        hfVID.value = vid;
        hfComments.value = comments;
        hfMonthlyVolume.value = monthlyValue;
        hfRowKey.value = rowKey;
    }
}

function ViewAllChildGrid() {
    var imgToggle = event.srcElement || event.target,
        rowNumber = imgToggle.id.replace('MainContent_ctrlViewAll_ViewAllMasterGrid_imgToggle_', ''),
        detailsGrid = document.getElementById('MainContent_ctrlViewAll_ViewAllMasterGrid_pnlDetail_' + rowNumber);

    if (imgToggle.title == 'Expand' && detailsGrid.innerText == '') {
        var assetName = document.getElementById('MainContent_ctrlViewAll_ViewAllMasterGrid_lblAssetNameFull_' + rowNumber),
            productName = document.getElementById('MainContent_ctrlViewAll_ViewAllMasterGrid_lblProductName_' + rowNumber),
            hfUOM = document.getElementById('MainContent_ctrlViewAll_ViewAllMasterGrid_hfUOM_' + rowNumber),
            uomName = hfUOM.value.substring(hfUOM.value.indexOf('UOM~') + 4, hfUOM.value.indexOf('~Source')),
            sourceUOM = hfUOM.value.substring(hfUOM.value.indexOf('Source~') + 7, hfUOM.value.indexOf('~End'));

        var jsondata = {
            'yearNum': yearSC,
            'monthNum': monthSC,
            'astName': assetName.innerText,
            'entName': entNameSC,
            'facName': facNameSC,
            'prodName': (productName == null ? 'UNKNOWN' : productName.innerText),
            'uomName': uomName,
            'sourceUOM': sourceUOM,
            'rowNum': rowNumber
        };

        $.ajax({
            type: "POST",
            url: "/WebServices/DetailRowServices.asmx/ViewAllChild",
            data: JSON.stringify(jsondata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                detailsGrid.innerHTML = msg.d;
            }
        });
    }
}

function PageSave() {
    if (ViewAllMasterGrid != null)
        ViewAllSave();
    else if (StandardMasterGrid != null)
        StandardSave();
}

function StandardSave() {
    SetGrids();

    var hfRefreshOffSearchCriteria = document.getElementById('MainContent_ctrlStandard_hfRefreshOffSearchCriteria');
    hfRefreshOffSearchCriteria.value = "false";

    for (a = 0; a < gridRowCount(StandardMasterGrid.rows.length); a++) {
        var ddlValidated = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_ddlValidated_' + a),
            lblValidated = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_lblValidated_' + a);

        if (ddlValidated.style.display != 'none')
        {
            lblValidated.value = ddlValidated.options[ddlValidated.selectedIndex].value;
            ddlValidated.style.display = 'none';
            lblValidated.style.display = 'inline-block';
        }
    }
}

function ViewAllSave() {
    SetGrids();
    for (a = 0; a < gridRowCount(ViewAllMasterGrid.rows.length); a++) {
        var grid = document.getElementById('vGridDetails_' + a.toString()),
            hfUOM = document.getElementById('MainContent_ctrlViewAll_ViewAllMasterGrid_hfUOM_' + a),
            uomName = hfUOM.value.substring(hfUOM.value.indexOf('UOM~') + 4, hfUOM.value.indexOf('~Source')),
            sourceUOM = hfUOM.value.substring(hfUOM.value.indexOf('Source~') + 7, hfUOM.value.indexOf('~End')),
            VID = document.getElementById('MainContent_ctrlViewAll_ViewAllMasterGrid_lblValidated_' + a),
            ddlVID = document.getElementById('MainContent_ctrlViewAll_ViewAllMasterGrid_ddlValidated_' + a),
            assetName = document.getElementById('MainContent_ctrlViewAll_ViewAllMasterGrid_lblAssetNameFull_' + a),
            productName = document.getElementById('MainContent_ctrlViewAll_ViewAllMasterGrid_lblProductName_' + a);

        SetSearchVariables();

        if (grid != null) {
            for (var i = 0; i < grid.rows.length; i++) {
                if (i != 0)// skip header
                {
                    var rowKey = grid.rows[i].cells[0].innerText;
                    var monthlyValue = grid.rows[i].cells[parseInt(monthSC, 10) + 2].firstChild.value;

                    var jsondata = {
                        'yearNum': yearSC,
                        'monthNum': monthSC,
                        'astName': assetName.innerText,
                        'entName': entNameSC,
                        'facName': facNameSC,
                        'prodName': (productName == null ? 'UNKNOWN' : productName.innerText),
                        'uomName': uomName,
                        'sourceUOM': sourceUOM,
                        'RowKey': rowKey,
                        'VID': ddlVID.style.display == 'none' ? VID.innerText : ddlVID.options[ddlVID.selectedIndex].value,
                        'monthlyValue': monthlyValue.replace(',',''),
                        'rowNum': a
                    };

                    $.ajax({
                        type: "POST",
                        url: "/WebServices/DetailRowServices.asmx/SaveAllChild",
                        data: JSON.stringify(jsondata),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json"
                    });
                }
            }
        }
        else {
            var jsondata = {
                'yearNum': yearSC,
                'monthNum': monthSC,
                'astName': assetName.innerText,
                'entName': entNameSC,
                'facName': facNameSC,
                'prodName': (productName == null ? 'UNKNOWN' : productName.innerText),
                'uomName': uomName,
                'sourceUOM': sourceUOM,
                'RowKey': null,
                'VID': ddlVID.style.display == 'none' ? VID.innerText : ddlVID.options[ddlVID.selectedIndex].value,
                'monthlyValue': null,
                'rowNum': a
            };

            $.ajax({
                type: "POST",
                url: "/WebServices/DetailRowServices.asmx/SaveAllChild",
                data: JSON.stringify(jsondata),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        }
    }
}

function StandardChildGrid()
{
    var imgToggle = event.srcElement || event.target,
        rowNumber = imgToggle.id.replace('MainContent_ctrlStandard_StandardMasterGrid_imgToggle_', ''),
        detailsGrid = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_pnlDetail_' + rowNumber);

    if (imgToggle.title == 'Expand' && detailsGrid.innerText == '') {
        var productName = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_lblProductName_' + rowNumber),
            hfUOM = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_hfUOM_' + rowNumber),
            uomName = hfUOM.value.substring(hfUOM.value.indexOf('UOM~') + 4, hfUOM.value.indexOf('~Source')),
            sourceUOM = hfUOM.value.substring(hfUOM.value.indexOf('Source~') + 7, hfUOM.value.indexOf('~End')),
            assetName = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_lblAssetNameFull_' + rowNumber);

        var jsondata = {
            'facName': facNameSC.length == 0 ? '' : facNameSC,
            'yearNum': yearSC,
            'monthNum': monthSC,
            'UOM': uomName,
            'UOMSource': sourceUOM,
            'scName': scNameSC,
            'astName': assetName.tipsy,
            'dsourceName': dsourceNameSC,
            'astType': astTypeSC,
            'productName': (productName == null ? 'UNKNOWN' : productName.innerText)
        };

        $.ajax({
            type: "POST",
            url: "/WebServices/DetailRowServices.asmx/StandardChild",
            data: JSON.stringify(jsondata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                detailsGrid.innerHTML = msg.d;
            }
        });
    }
}

//#region Search Bar Functions

var searchCriteria,
    entNameSC,
    facNameSC,
    yearSC,
    monthSC,
    scNameSC,
    astNameSC,
    dsourceNameSC,
    astTypeSC;

var ddlEntity,
    ddlFacility,
    ddlSC,
    ddlAssetName,
    ddlAssetType,
    ddlDataSource,
    txtMonthYear;

var clear;

var assetAdminSearchCriteria,
    aEntNameSC,
    aFacNameSC,
    aSCNameSC,
    aAstNameSC;

var facAdminSearchCriteria,
    fFacIDSC;

var ajaxType = 'POST',
    ajaxContentType = 'application/json; charset=utf-8',
    ajaxDataType = 'json';

function SetSearchControls() {
    ddlEntity = document.getElementById('ddlEntity'),
    ddlFacility = document.getElementById('MainContent_ddlFacilities') || document.getElementById('ddlFacilities'),
    ddlSC = document.getElementById('ddlCategory'),
    ddlAssetName = document.getElementById('ddlAsset'),
    ddlAssetType = document.getElementById('ddlAssetType'),
    ddlDataSource = document.getElementById('ddlSource'),
    txtMonthYear = document.getElementById('MainContent_txtMonthYear');
}

function SearchPanelLoadFacilityManagement() {
    GetFacAdminSearchSession();
    SetSearchControls();
    FacilityFacAdminPageLoad();
}
function SetFacAdminSearchVariables() {
    if (facAdminSearchCriteria != null && facAdminSearchCriteria != undefined && facAdminSearchCriteria.length != 0) {
        fFacIDSC = facAdminSearchCriteria.substring(facAdminSearchCriteria.indexOf('~ID~') + 4, facAdminSearchCriteria.length);
    }
}
function SetFacAdminSearchCriteria() {
    SetFacAdminSearchVariables();
    if (facAdminSearchCriteria !== null && facAdminSearchCriteria !== undefined && facAdminSearchCriteria.length !== 0) {
        if (fFacIDSC.length > 0) {
            $(ddlFacility).val(fFacIDSC);
        }
    }
}

function SearchPanelLoadAssetManagement() {
    GetAssetAdminSearchSession();
    SetSearchControls();
    EntityAssetAdminPageLoad();
    FacilityAssetAdminPageLoad();
}
function SetAssetAdminSearchVariables() {
    if (assetAdminSearchCriteria != null && assetAdminSearchCriteria != undefined && assetAdminSearchCriteria.length != 0) {
        aEntNameSC = assetAdminSearchCriteria.substring(assetAdminSearchCriteria.indexOf('Entity~') + 7, assetAdminSearchCriteria.indexOf('~Facility')),
        aFacNameSC = assetAdminSearchCriteria.substring(assetAdminSearchCriteria.indexOf('Facility~') + 9, assetAdminSearchCriteria.indexOf('~Category')),
        aSCNameSC = assetAdminSearchCriteria.substring(assetAdminSearchCriteria.indexOf('Category~') + 9, assetAdminSearchCriteria.indexOf('~AssetName')),
        aAstNameSC = assetAdminSearchCriteria.substring(assetAdminSearchCriteria.indexOf('AssetName~') + 10, assetAdminSearchCriteria.length);
    }
}
function SetAssetAdminSearchCriteria() {
    SetAssetAdminSearchVariables();
    //Need to set the Search Criteria from the Variables
    if (assetAdminSearchCriteria !== null && assetAdminSearchCriteria !== undefined && assetAdminSearchCriteria.length !== 0) {
        if (aEntNameSC.length > 0) {
            var value = $('#' + ddlEntity.id + ' option:contains(' + aEntNameSC + ')').attr('selected', 'selected');
            $(ddlEntity).val(value);
            SearchAssetAdminEntityChange(true);
        }
    }
}
function SearchAssetAdminCategoryChange() {
    ddlAssetName.disabled = true;
    AssetAdminAssetNamePageLoad();
}
function ClearAssetAdminSearch() {
    $(ddlEntity).val(0);
    $(ddlFacility).val(0);
    $(ddlSC).empty();
    $(ddlSC).append($('<option value=0></option>'));
    $(ddlSC).val(0);
    $(ddlAssetName).empty();
    $(ddlAssetName).append($('<option value=0></option>'));
    $(ddlAssetName).val(0);

    //I need to work on popuilating the Facility DropdownList

    ddlAssetName.disabled = true;
    clear = true;
}

function SearchPanelLoad(page) {
    GetSearchSession();
    SetSearchControls();
    EntityPageLoad(page);
    FacilityPageLoad(page);
    if (txtMonthYear !== null) {
        if (txtMonthYear.value.length == 0) {
            SetMonthYear(page);
        }
    }
}
function SetSearchVariables() {
    if (searchCriteria != null || searchCriteria != undefined) {
        entNameSC = searchCriteria.substring(searchCriteria.indexOf('Entity~') + 7, searchCriteria.indexOf('~Facility')),
        facNameSC = searchCriteria.substring(searchCriteria.indexOf('Facility~') + 9, searchCriteria.indexOf('~Category')),
        yearSC = searchCriteria.substring(searchCriteria.indexOf('Year~') + 5, searchCriteria.indexOf('~Month')),
        monthSC = searchCriteria.substring(searchCriteria.indexOf('Month~') + 6, searchCriteria.indexOf('~AssetName')),
        scNameSC = searchCriteria.substring(searchCriteria.indexOf('Category~') + 9, searchCriteria.indexOf('~Year')),
        astNameSC = searchCriteria.substring(searchCriteria.indexOf('AssetName~') + 10, searchCriteria.indexOf('~DataSource')),
        dsourceNameSC = searchCriteria.substring(searchCriteria.indexOf('DataSource~') + 11, searchCriteria.indexOf('~AssetType')),
        astTypeSC = searchCriteria.substring(searchCriteria.indexOf('AssetType~') + 10, searchCriteria.indexOf('~ValCheck'));
    }
}
function SetSearchCriteria(page) {
    SetSearchVariables();
    if (searchCriteria != null || searchCriteria != undefined) {
        if (entNameSC.length > 0) {
            var value = $('#' + ddlEntity.id + ' option:contains(' + entNameSC + ')').attr('selected', 'selected');
            $(ddlEntity).val(value);
            //if (facNameSC.length > 0) {
            //    SearchEntityChange(page, true);
            //}
        }
        if (facNameSC.length > 0) {
            var value = $('#' + ddlFacility.id + ' option:contains(' + facNameSC + ')').attr('selected', 'selected');
            $(ddlFacility).val(value);
        }
        if (monthSC.length > 0 && yearSC.length > 0) {
            txtMonthYear.value = monthSC + '/' + yearSC;
        }
        if (scNameSC.length > 0) {
            var value = $('#' + ddlSC.id + ' option:contains(' + scNameSC + ')').attr('selected', 'selected');
            $(ddlSC).val(value);
        }
        if (astNameSC.length > 0) {
            var value = $('#' + ddlAssetName.id + ' option:contains(' + astNameSC + ')').attr('selected', 'selected');
            $(ddlAssetName).val(value);
        }
        if (astTypeSC.length > 0) {
            var value = $('#' + ddlAssetType.id + ' option:contains(' + astTypeSC + ')').attr('selected', 'selected');
            $(ddlAssetType).val(value);
        }
        if (dsourceNameSC.length > 0) {
            var value = $('#' + ddlEntity.id + ' option:contains(' + dsourceNameSC + ')').attr('selected', 'selected');
            $(ddlDataSource).val(value);
        }
    }
}
function SearchCategoryChange(page) {
    var facName = ddlFacility.options[ddlFacility.selectedIndex].innerHTML;

    SearchAssetNameDataBind(facName);
    SearchAssetTypeDataBind(facName);
    SearchDataSourceDatabind(facName);
}
function ClearSearch(page) {
    $(ddlEntity).val(0);
    $(ddlFacility).val(0);
    $(ddlSC).val(0);
    $(ddlAssetName).val(0);
    $(ddlAssetType).val(0);
    $(ddlDataSource).val(0);
    SetMonthYear(page);
    clear = true;
}
function AssetClearAdminSearch() {
    $(ddlEntity).val(0);
    $(ddlFacility).val(0);
    $(ddlSC).val(0);
    $(ddlAssetName).val(0);
    $('#txtAsset').val('');
    return false;
}

//#region Ajax Calls

function FacAdminSetSearch() {
    var jsondata = {
        'facName': ddlFacility.options.length > 0 && ddlFacility.selectedIndex !== -1 ? ddlFacility.options[ddlFacility.selectedIndex].innerHTML : '',
        'facID': ddlFacility.options.length > 0 && ddlFacility.selectedIndex !== -1 ? ddlFacility.options[ddlFacility.selectedIndex].value : ''
    }
    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/SetFacAdminSearchSession",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType
    });
}
function GetFacAdminSearchSession() {
    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/GetFacAdminSearchSession",
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            facAdminSearchCriteria = msg.d;
        }
    });
}
function FacilityFacAdminPageLoad() {
    var jsondata = {
        'page': 'STANDARD'
    };

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/FacAdminFacilityLoad",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            $(ddlFacility).empty();
            $(ddlFacility).append($('<option value=0></option>'));
            var i = 1;
            $.each(msg.d, function () {
                $(ddlFacility).append($('<option value=' + this.substring(this.lastIndexOf('|') + 1, this.length) + '>' + this.substring(0, this.lastIndexOf('|')) + '</option>'));
                i = i + 1;
            });
            if (!clear) {
                SetFacAdminSearchCriteria();
            }
        }
    });
}



var doop = '';

function AssetAdminAssetNameTextbox() {
    
    if (($(ddlEntity).val() == 0 || $(ddlEntity).val() == null) && ($(ddlFacility).val() == 0 || $(ddlFacility).val() == null) && ($(ddlSC).val() == 0 || $(ddlSC).val() == null)) {

        //show the textbox???
        $("#txtAsset").show();
        $("#ddlAsset").hide();

        if (doop.length == 0) {
            $("#txtAsset").attr("disabled", true);
            var jsondata = {
                'entName': '',
                'facName': '',
                'scName': ''
            };

            $.ajax({
                type: ajaxType,
                url: "/WebServices/SearchBarServices.asmx/AssetAdminAssetNameLoad",
                data: JSON.stringify(jsondata),
                contentType: ajaxContentType,
                dataType: ajaxDataType,
                success: function (msg) {
                    doop = msg.d
                    $("#txtAsset").autocompleteArray(doop);
                    $("#txtAsset").attr("disabled", false);
                    $("#txtAsset").focus();
                }
            });
        }

        $("#txtAsset").autocompleteArray(doop);
    }
    else {
        $("#txtAsset").hide();
        $("#ddlAsset").show();
    }
}


function AssetAdminSetSearch() {
    var jsondata = {
        'entName': ddlEntity.options.length > 0 ? ddlEntity.options[ddlEntity.selectedIndex].innerHTML : '',
        'facName': ddlFacility.options.length > 0 && ddlFacility.selectedIndex !== -1 ? ddlFacility.options[ddlFacility.selectedIndex].innerHTML : '',
        'scName': ddlSC.options.length > 0 && ddlSC.selectedIndex !== -1 ? ddlSC.options[ddlSC.selectedIndex].innerHTML : '',
        'assetName': (ddlAssetName.options.length > 0 && ddlAssetName.style.display != 'none') ? ddlAssetName.options[ddlAssetName.selectedIndex].value : $('#txtAsset')[0].value.split('|').pop()
    }

    clear = false;

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/SetAssetAdminSearchSession",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType
    });
}
function GetAssetAdminSearchSession() {
    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/GetAssetAdminSearchSession",
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            assetAdminSearchCriteria = msg.d;
        }
    });
}
function EntityAssetAdminPageLoad() {
    var jsondata = {
        'page': 'STANDARD'
    };

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/EntityLoad",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            $(ddlEntity).empty();
            $(ddlEntity).append($('<option value=0></option>'));
            var i = 1;
            $.each(msg.d, function () {
                $(ddlEntity).append($('<option value=' + i + '>' + this + '</option>'));
                i = i + 1;
            });
        }
    });
}
function FacilityAssetAdminPageLoad() {
    var jsondata = {
        'page': 'STANDARD'
    };

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/FacilityLoad",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            $(ddlFacility).empty();
            $(ddlFacility).append($('<option value=0></option>'));
            var i = 1;
            $.each(msg.d, function () {
                $(ddlFacility).append($('<option value=' + i + '>' + this + '</option>'));
                i = i + 1;
            });
            if (!clear) {
                SetAssetAdminSearchCriteria();
            }
        }
    });
}
function AssetAdminAssetNamePageLoad() {
    var jsondata = {
        'entName': ddlEntity.length == 0 ? aEntNameSC : (ddlEntity.selectedIndex !== -1 ? ddlEntity.options[ddlEntity.selectedIndex].innerHTML : ''),
        'facName': ddlFacility.length == 0 ? aFacNameSC : (ddlFacility.selectedIndex !== -1 ? ddlFacility.options[ddlFacility.selectedIndex].innerHTML : ''),
        'scName': ddlSC.selectedIndex !== -1 ? ddlSC.options[ddlSC.selectedIndex].innerHTML : ''
    };

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/AssetAdminAssetNameLoad",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            $(ddlAssetName).empty();
            $(ddlAssetName).append($('<option value=0></option>'));
            var i = 1;
            $.each(msg.d, function () {
                $(ddlAssetName).append($('<option value=' + this.substring(this.lastIndexOf('|') + 1, this.length) + '>' + this.substring(0, this.lastIndexOf('|')) + '</option>'));
                i = i + 1;
            });

            if (ddlAssetName.length > 0) {
                ddlAssetName.disabled = false;
            } else {
                ddlAssetName.disabled = true;
            }

            if (aAstNameSC != undefined) {
                if (aAstNameSC.length > 0) {
                    $(ddlAssetName).val(aAstNameSC);
                }
            }
        }
    });
}
function SearchAssetAdminEntityChange(test) {
    if (ddlEntity.selectedIndex !== 0) {
        AssetAdminAssetNameTextbox();
        ddlAssetName.disabled = true;
        var jsondata = {
            'entName': ddlEntity.length == 0 ? aEntNameSC : (ddlEntity.selectedIndex !== -1 ? ddlEntity.options[ddlEntity.selectedIndex].innerHTML : ''),
            'page': 'STANDARD'
        };

        $.ajax({
            type: ajaxType,
            url: "/WebServices/SearchBarServices.asmx/EntityChange",
            data: JSON.stringify(jsondata),
            contentType: ajaxContentType,
            dataType: ajaxDataType,
            success: function (msg) {
                $(ddlFacility).empty();
                $(ddlSC).empty();
                $(ddlAssetName).empty();
                $(ddlFacility).append($('<option value=0></option>'));
                var i = 1;
                $.each(msg.d, function () {
                    $(ddlFacility).append($('<option value=' + i + '>' + this + '</option>'));
                    i = i + 1;
                });
                if (aFacNameSC != undefined) {
                    if (aFacNameSC.length > 0) {
                        var value = $('#' + ddlFacility.id + ' option:contains(' + aFacNameSC + ')').attr('selected', 'selected');
                        $(ddlFacility).val(value);
                        if (test == true) {
                            var valueEnt = $('#' + ddlEntity.id + ' option:contains(' + aEntNameSC + ')').attr('selected', 'selected');
                            $(ddlEntity).val(valueEnt);
                        }
                    }
                    if (ddlEntity.length > 0)//a little buggy. needs to be down this far so it doesn't bug out 
                    {
                        if (ddlEntity.selectedIndex !== -1) {
                            if (ddlEntity.options[ddlEntity.selectedIndex].innerHTML !== aEntNameSC) {
                                entNameSC = null;
                                facNameSC = null;
                                scNameSC = null;
                                astNameSC = null;
                            }
                        }
                    }
                    SearchAssetAdminFacilityChange();
                }
                AssetAdminAssetNamePageLoad();
            }
        });
    }
    else {
        AssetAdminAssetNameTextbox();
    }
}
function SearchAssetAdminFacilityChange() {
    if (ddlFacility.selectedIndex !== 0) {
        AssetAdminAssetNameTextbox();
        ddlAssetName.disabled = true;
        var jsondata = {
            'facName': ddlFacility.length == 0 ? aFacNameSC : (ddlFacility.selectedIndex !== -1 ? ddlFacility.options[ddlFacility.selectedIndex].innerHTML : '')
        };

        $.ajax({
            type: ajaxType,
            url: "/WebServices/SearchBarServices.asmx/FacilityChange",
            data: JSON.stringify(jsondata),
            contentType: ajaxContentType,
            dataType: ajaxDataType,
            success: function (msg) {
                $(ddlSC).empty();
                $(ddlAssetName).empty();
                $(ddlSC).append($('<option value=0></option>'));
                var i = 1;
                $.each(msg.d, function () {
                    $(ddlSC).append($('<option value=' + i + '>' + this + '</option>'));
                    i = i + 1;
                });
                if (aSCNameSC != undefined) {
                    if (aSCNameSC.length > 0) {
                        var value = $('#' + ddlSC.id + ' option:contains(' + aSCNameSC + ')').attr('selected', 'selected');
                        $(ddlSC).val(value);
                    }
                }
                AssetAdminAssetNamePageLoad();
            }
        });
    }
    else {
        AssetAdminAssetNameTextbox();
    }
}

function SetSearch() {
    var jsondata = {
        'entName': ddlEntity.options.length > 0 ? ddlEntity.options[ddlEntity.selectedIndex].innerHTML : '',
        'facName': ddlFacility.options.length > 0 && ddlFacility.selectedIndex !== -1 ? ddlFacility.options[ddlFacility.selectedIndex].innerHTML : '',
        'scName': ddlSC.options.length > 0 ? ddlSC.options[ddlSC.selectedIndex].innerHTML : '',
        'year': txtMonthYear.value.substring(txtMonthYear.value.indexOf('/') + 1, txtMonthYear.value.length),
        'month': txtMonthYear.value.substring(0, txtMonthYear.value.indexOf('/')),
        'assetName': ddlAssetName.options.length > 0 ? ddlAssetName.options[ddlAssetName.selectedIndex].innerHTML : '',
        'assetType': ddlAssetType.options.length > 0 ? ddlAssetType.options[ddlAssetType.selectedIndex].innerHTML : '',
        'dsName': ddlDataSource.options.length > 0 ? ddlDataSource.options[ddlDataSource.selectedIndex].innerHTML : ''
    };

    clear = false;

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/SetSearchSession",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function () {
            GetSearchSession();
        }
    });
}
function SetMonthYear(page) {
    var jsondata = {
        'page': page
    };

    var today = new Date();
    if (today.getDate() < jobRunDay)
        today.setMonth(today.getMonth() - 2);
    else
        today.setMonth(today.getMonth() - 1);

    var month = parseInt(today.getMonth());
    year = parseInt(today.getYear()),
    cal = $find("MonthCalendarBehavior"),
    date = new Date(year, month, 1);
    if (cal !== null) {
        cal.set_selectedDate(date);
    }

    //$.ajax({
    //    type: ajaxType,
    //    url: "/WebServices/SearchBarServices.asmx/SetMonthYear",
    //    data: JSON.stringify(jsondata),
    //    contentType: ajaxContentType,
    //    dataType: ajaxDataType,
    //    success: function (msg) {
    //        if (txtMonthYear !== null) {
    //            txtMonthYear.value = msg.d;

    //            //sets the selection in the ajax Calendar Extender
    //            var month = parseInt(txtMonthYear.value.substring(0, txtMonthYear.value.indexOf('/'))),
    //                year = parseInt(txtMonthYear.value.substring(txtMonthYear.value.indexOf('/') + 1, txtMonthYear.length)),
    //                cal = $find("MonthCalendarBehavior"),
    //                date = new Date(year, month - 1);

    //            cal.set_selectedDate(date);
    //        }
    //    }
    //});
}
function GetSearchSession() {
    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/GetSearchSession",
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            searchCriteria = msg.d;
            SetSearchVariables();
        }
    });
}
function EntityPageLoad(page) {
    var jsondata = {
        'page': page
    };

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/EntityLoad",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            $(ddlEntity).empty();
            $(ddlEntity).append($('<option value=0></option>'));
            var i = 1;
            $.each(msg.d, function () {
                $(ddlEntity).append($('<option value=' + i + '>' + this + '</option>'));
                i = i + 1;
            });
        }
    });
}
function FacilityPageLoad(page) {
    var jsondata = {
        'page': page
    };

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/FacilityLoad",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            $(ddlFacility).empty();
            $(ddlFacility).append($('<option value=0></option>'));
            var i = 1;
            $.each(msg.d, function () {
                $(ddlFacility).append($('<option value=' + i + '>' + this + '</option>'));
                i = i + 1;
            });
            if (!clear) {
                SetSearchCriteria(page);
            }
        }
    });
}
function SearchEntityChange(page, test) {
    var jsondata = {
        'entName': ddlEntity.length == 0 ? entNameSC : (ddlEntity.selectedIndex !== -1 ? ddlEntity.options[ddlEntity.selectedIndex].innerHTML : ''),
        'page': page
    };

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/EntityChange",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            $(ddlSC).empty();
            $(ddlAssetName).empty();
            $(ddlAssetType).empty();
            $(ddlDataSource).empty();
            $(ddlFacility).empty();
            $(ddlFacility).append($('<option value=0></option>'));
            var i = 1;
            $.each(msg.d, function () {
                $(ddlFacility).append($('<option value=' + i + '>' + this + '</option>'));
                i = i + 1;
            });
            if (facNameSC != undefined) {
                if (facNameSC.length > 0) {
                    var value = $('#' + ddlFacility.id + ' option:contains(' + facNameSC + ')').attr('selected', 'selected');
                    $(ddlFacility).val(value);
                    if (test == true) {
                        var valueEnt = $('#' + ddlEntity.id + ' option:contains(' + entNameSC + ')').attr('selected', 'selected');
                        $(ddlEntity).val(valueEnt);
                    }
                }
                if (ddlEntity.length > 0)//a little buggy. needs to be down this far so it doesn't bug out 
                {
                    if (ddlEntity.selectedIndex !== -1) {
                        if (ddlEntity.options[ddlEntity.selectedIndex].innerHTML !== entNameSC) {
                            //clear search critieria
                            entNameSC = null;
                            facNameSC = null;
                            yearSC = null;
                            monthSC = null;
                            scNameSC = null;
                            astNameSC = null;
                            dsourceNameSC = null;
                            astTypeSC = null;
                        }
                    }
                }
                SearchFacilityChange();
            }
        }
    });
}
function SearchFacilityChange() {
    var jsondata = {
        'facName': ddlFacility.length == 0 ? facNameSC : (ddlFacility.selectedIndex !== -1 ? ddlFacility.options[ddlFacility.selectedIndex].innerHTML : '')
    };

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/FacilityChange",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            $(ddlSC).empty();
            $(ddlSC).append($('<option value=0></option>'));
            var i = 1;
            $.each(msg.d, function () {
                $(ddlSC).append($('<option value=' + i + '>' + this + '</option>'));
                i = i + 1;
            });
            if (scNameSC != undefined) {
                if (scNameSC.length > 0) {
                    var value = $('#' + ddlSC.id + ' option:contains(' + scNameSC + ')').attr('selected', 'selected');
                    $(ddlSC).val(value);
                }
            }
            SearchAssetNameDataBind(facNameSC);
            SearchAssetTypeDataBind(facNameSC);
            SearchDataSourceDatabind(facNameSC);
        }
    });
}

function SearchAssetNameDataBind(facName) {
    var jsondata = {
        'facName': facName,
        'scName': ddlSC.length == 0 ? scNameSC : (ddlSC.selectedIndex !== -1 ? ddlSC.options[ddlSC.selectedIndex].innerHTML : '') //ddlSC.options[ddlSC.selectedIndex].innerHTML
    };

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/SearchAssetNameDataBind",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            $(ddlAssetName).empty();
            $(ddlAssetName).append($('<option value=0></option>'));
            var i = 1;
            $.each(msg.d, function () {
                $(ddlAssetName).append($('<option value=' + i + '>' + this + '</option>'));
                i = i + 1;
            });
            if (astNameSC != undefined) {
                if (astNameSC.length > 0) {
                    var value = $('#' + ddlAssetName.id + ' option:contains(' + astNameSC + ')').attr('selected', 'selected');
                    $(ddlAssetName).val(value);
                }
            }
        }
    });
}
function SearchAssetTypeDataBind(facName) {
    var jsondata = {
        'facName': facName,
        'scName': ddlSC.length == 0 ? scNameSC : (ddlSC.selectedIndex !== -1 ? ddlSC.options[ddlSC.selectedIndex].innerHTML : '') //ddlSC.options[ddlSC.selectedIndex].innerHTML
    };

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/SearchAssetTypeDataBind",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            $(ddlAssetType).empty();
            $(ddlAssetType).append($('<option value=0></option>'));
            var i = 1;
            $.each(msg.d, function () {
                $(ddlAssetType).append($('<option value=' + this + '>' + this + '</option>'));
                i = i + 1;
            });
            if (astTypeSC != undefined) {
                if (astTypeSC.length > 0) {
                    var value = $('#' + ddlAssetType.id + ' option:contains(' + astTypeSC + ')').attr('selected', 'selected');
                    $(ddlAssetType).val(value);
                }
            }
        }
    });
}
function SearchDataSourceDatabind(facName) {
    var jsondata = {
        'facName': facName,
        'scName': ddlSC.length == 0 ? scNameSC : (ddlSC.selectedIndex !== -1 ? ddlSC.options[ddlSC.selectedIndex].innerHTML : '') //ddlSC.options[ddlSC.selectedIndex].innerHTML
    };

    $.ajax({
        type: ajaxType,
        url: "/WebServices/SearchBarServices.asmx/SearchDataSourceDatabind",
        data: JSON.stringify(jsondata),
        contentType: ajaxContentType,
        dataType: ajaxDataType,
        success: function (msg) {
            $(ddlDataSource).empty();
            $(ddlDataSource).append($('<option value=0></option>'));
            var i = 1;
            $.each(msg.d, function () {
                $(ddlDataSource).append($('<option value=' + this + '>' + this + '</option>'));
                i = i + 1;
            });
            if (dsourceNameSC != undefined) {
                if (dsourceNameSC.length > 0) {
                    var value = $('#' + ddlDataSource.id + ' option:contains(' + dsourceNameSC + ')').attr('selected', 'selected');
                    $(ddlDataSource).val(value);
                }
            }
        }
    });
}

//#endregion

//#endregion

function bkgrndBlue() {
    var txt = event.srcElement || event.target;
    txt.parentNode.style.background = '#CCCC99';
}

function boxExpand(ta) {
    if (ta == null)
        return;
    if (ta.scrollHeight > 0) {
        ta.style.height = (ta.scrollHeight + 6) + "px";
    }
}

function gridRowCount(gridLength) {
    
    if (gridLength == 27) {
        gridLength = gridLength - 2;
    }
    else if (gridLength > 27 || gridLength < 27) {
        gridLength = gridLength - 1;
    }
    return gridLength;
}

function txtSelectedViewAll() {
    SetGrids();
    for (i = 0; i < gridRowCount(ViewAllMasterGrid.rows.length) ; i++) {
        var childGrid = document.getElementById('vGridDetails_' + i);

        if (childGrid !== null) {
            for (a = 0; a < gridRowCount(childGrid.rows.length) ; a++) {
                var rowNum = parseInt(event.srcElement.id.replace(childGrid.id + '_txtEdit_', ''));
                var td = document.getElementById(childGrid.id + '_txtEdit_' + a).parentElement;
                if (a !== rowNum) {
                    td.style.border = 'none';
                }
                else {
                    td.style.border = '1px solid black';
                    td.style.height = '5px';
                }
            }
        }
    }
}

function txtSelected() {
    var rowNum = parseInt(event.srcElement.id.replace('MainContent_ctrlStandard_StandardMasterGrid_txtThisMonthVol_', ''));

    SetGrids();

    for (i = 0; i < gridRowCount(StandardMasterGrid.rows.length); i++) {
        var txt = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_txtThisMonthVol_' + i);
        var td = txt.parentElement;
        if (i !== rowNum) {
            td.style.border = 'none';
        }
        else {
            td.style.border = '1px solid black';
            td.style.height = '5px';
        }
    }
}

function pageLoad() {
    var dt = {
        numberOfMonths: 1,
        maxDate: '0'
    };
    $('.datepicker').datepicker(dt);

    var $table = $('.gridView');
    $table.on("mouseover", "tbody tr.rowTable", function (e) {
        $row = $(this);
        if ($(this).hasClass('selectedRow')) return false;
        $('.rowTable.selectedRow').not($(this)).removeClass('selectedRow');
        $('.validateLabel').removeClass('disabledMode');
        $('.validateDDL').addClass('disabledMode');
        $(this).toggleClass('selectedRow');
    });
    $table.on('mouseover', '.validateLabel', function (e) {
        $(this).addClass('disabledMode');
        $(this).next().removeClass('disabledMode');
    });

    $table.on("click", "td", function (event) {
        var grid = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid') == null ? document.getElementById('MainContent_ctrlStandard_ViewAllMasterGrid') : document.getElementById('MainContent_ctrlStandard_StandardMasterGrid');
        if (grid !== null) {
            for (i = 0; i < gridRowCount(grid.rows.length) ; i++) {
                var txt = document.getElementById('MainContent_ctrlStandard_StandardMasterGrid_txtThisMonthVol_' + i);
                var td = txt.parentElement;
                td.style.border = 'none';
            }
        }

        if ($(this).parent().hasClass('selectedRow')) {
            $(this).find('.validateLabel').dblclick();
        }
    });

    $table.on('change', '.validateDDL', function (e) {
        $(this).prev().text($(this).val());
    });

    $('.notes').tipsy({ gravity: 'e' });
    $('.addNotes').tipsy({ gravity: 'e' });

    $('.cancelNotes').click(function () {
        $panel = $(this).parent().parent();
        $txtComments = $panel.find('.txtComments');
        $lblComments = $panel.find('.lblComments');
        $txtComments.val($txtComments.data('original'));
        $panel.attr('visiblity', 'hidden').hide();
    });

    $('.notes,.addNotes').click(function () {
        $panel = $(this).next();
        $txtComments = $panel.find('.txtComments');
        boxExpand($txtComments[0]);
        $txtComments.data('original', $txtComments.val());
        $panel.show();
    });

    $('.okNotes').click(function () {
        $panel = $(this).parent().parent();
        $txtComments = $panel.find('.txtComments');
        if ($.trim($txtComments.val()).length > 0) {
            $panel.prev().removeClass('addNotes').addClass('notes').attr('tipsy', 'Click to edit comments');
        }
        if ($.trim($txtComments.val()).length == 0 && $.trim($txtComments.data('original')).length > 0) {
            $txtComments.val($txtComments.data('original'));
        }
        $panel.attr('visiblity', 'hidden').hide();
    });
}

function CloseErrorPanel() {
    var e = document.getElementById('MainContent_pnlError');
    //$('PopError').show();
    //e.style.display = '';
    //e.style.display = 'none';
}