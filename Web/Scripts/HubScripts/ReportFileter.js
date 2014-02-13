/* File Created: May 6, 2012 */
/// <reference path="jquery-1.5.1-vsdoc.js" />

function onReportFileterChange(e) {
    //$('#form0').submit();
}

function onCodesReportFileterChange(e) {
    var domId = $('#CodesId');
    if (domId.val() === '1') {
        $('#codeDetail').contents().remove();
        $('#codeDetail').load('/StockManagement/ShippingInstruction');
    }
    if (domId.val() === '2') {
        $('#codeDetail').contents().remove();
        $('#codeDetail').load('/StockManagement/ProjectCode');
    }
    if (domId.val() === '0') {
        $('#codeDetail').contents().remove();
    }
}

function onPeriodReportFilterChange(e) {
    var domId = $('#PeriodId');
    $('#dateDetail').contents().remove();
    if(domId.val() == '8') {
        $('#dateDetail').load('/StockManagement/CustomeDate');
    }
    if (domId.val() == '1') {
        $('#form0').submit();
    }
    if (domId.val() == '6') {
        $('#form0').submit();
    }
}

