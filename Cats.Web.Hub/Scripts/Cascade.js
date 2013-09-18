function wireUpAuditLinks() {
    $('.auditLink').live('click', function () {
        var auditDialog = $('#auditDialog');
        $(auditDialog).load(this.href, function () {
            $(this).dialog({
                modal: true,
                resizable: false,
                title: 'Audits',
                width: 'auto'
            });
        }); 
        return false;
    });
}

function tabSelected(e) {
    var x = e;
//    window.alert(item.find('> .t-link'));
}
function onRegionChange(e) {
    if (e.value != "") {
        var grid = $('#woredas').data('tGrid');
        grid.rebind({ regionId: e.value });
    } else {
        var createLink = $('#woredaList').find('hidden-command');
        $(createLink).hide();
    }
}


function onZoneChange(e) {
   
    if (e.value != "") {
        var createLink = $('#woredaList').find('.hidden-command');
        createLink.each(function(){$(this).show()});

        var url = "/AdminUnit/CreateWoreda?zoneId=" + e.value;
        createLink.find('a').attr('href', url);

        var grid = $('#woredas').data('tGrid');
        grid.rebind({ zoneId: e.value });
    } else {
        var createLink = $('#woredaList').find('.hidden-command');
        createLink.hide();
    }
}
function onZonesRegionChange(e) {

    if (e.value != "") {
        var createLink = $('#zoneList').find(".hidden-command");
        var url = "/AdminUnit/CreateZone?regionId=" + e.value;
        createLink.find('a').attr('href', url);
        createLink.show();
        var grid = $('#zones').data('tGrid');
        grid.rebind({ regionId: e.value });
    } else {
        var createLink = $('#zoneList').find('.hidden-command');
        createLink.hide();
    }
}
function onFDPWoredaChange(e) {

    if (e.value != "") {
        $('.hidden-command').show();
        var createLink = $('.hidden-command .dialogLink');
        var url = "/FDP/Create?woredaId=" + e.value;
        $(createLink).attr('href', url);
        createLink.show();
        var woredaGrid = $('#fdpGrid').data('tGrid');
        woredaGrid.rebind({ woredaId: e.value });
    } else {
        $('.hidden-command').hide();
    }
}
function onFDPRegionChange(e) {

    if (e.value != "") {
        var woredaGrid = $('#fdpGrid').data('tGrid');
        woredaGrid.rebind({ regionId: e.value });
    }
}
function onFDPZoneChange(e) {

    if (e.value != "") {
        var woredaGrid = $('#fdpGrid').data('tGrid');
        woredaGrid.rebind({ zoneId: e.value });
    }
} 

(function ($) {
    $.fn.cascade = function (options) {
        var defaults = {};
        var opts = $.extend(defaults, options);

        return this.each(function () {
            $(this).change(function () {   
                var selectedValue = $(this).val();
                var params = {};
                if (selectedValue != opts.ignoreValue) {
                    params[opts.paramName] = selectedValue;
                    $.getJSON(opts.url, params, function (items) {
                        opts.childSelect.empty();
                        $.each(items, function (index, item) {
                            opts.childSelect.append(
                            $('<option/>')
                                .attr('value', item.Id)
                                .text(item.Name)
                        );
                        });
                        opts.childSelect.change();
                    });
                    

                }
               
                
            });
        });
    };
})(jQuery);


function wireRegion() {

//    $('#SelectedRegionId').cascade({
//        url: 'AdminUnit/GetChildren',
//        paramName: 'unitId',
//        childSelect: $('#SelectedZoneId')
    //    });
    $('#SelectedRegionId').change(function () {
        alert('Changed');
    });
}
function submitData() {

    var insertData = $.grep($('#dispatchCommoditiesGrid').data().tGrid.changeLog.inserted, function (i) { return (i); });
    var deleteData = $.grep($('#dispatchCommoditiesGrid').data().tGrid.changeLog.deleted, function (d) { return (d); });
    var updateData = $.grep($('#dispatchCommoditiesGrid').data().tGrid.changeLog.updated, function (u) { return (u); });

    //$('#JSONInsertedCommodities').val(JSON.stringify(insertData));
    //$('#JSONDeletedCommodities').val(JSON.stringify(deleteData));
    //$('#JSONUpdatedCommodities').val(JSON.stringify(updateData));

    //$('#rowCount').val($('#dispatchCommoditiesGrid').data().tGrid.total + insertData.length - deleteData.length);
    
    var prevData = [];
    var idArray = [];
    var delIndex = [];
    var gr = $('#dispatchCommoditiesGrid').data('tGrid').data;


    for (var i = 0; i < updateData.length; i++) {
        // check if this element has already been added
        prevData[prevData.length] = updateData[i];
        idArray[idArray.length] = updateData[i].DispatchDetailCounter;

    }



    for (var i = 0; i < gr.length; i++) {
        // Do not repeat what has alrady been entered as edited 
        if (gr[i].DispatchDetailCounter == 0 || ($.inArray(gr[i].DispatchDetailCounter, idArray) == -1)) {
            prevData[prevData.length] = gr[i];
        }
    }

    for (var i = 0; i < insertData.length; i++) {
        prevData[prevData.length] = insertData[i];
    }

    if (prevData.length == 0) {
        prevData = gr;
    }

    /**
    * THE ORDERING MATTERS DO NOT MOVE THE LINES BELOW SINCE THEY SHOULD REMOVE WHAT HAVE BEEN INSERTED ABOVE
    *
    */

    for (var i = 0; i < deleteData.length; i++) {
        // collect the elements to be deleted # N.B. we don't support deletion of (Id > 0)
        if (deleteData[i].DispatchDetailCounter <= 0)
            delIndex[delIndex.length] = deleteData[i].DispatchDetailCounter;

    }

    for (var i = 0; i < prevData.length; i++) {
        // remove the data from the remove list 
        if (!($.inArray(prevData[i].DispatchDetailCounter, delIndex) == -1)) {
            //prevData.splice(i, 1); //may cause modification ogf the array 
            prevData[i] = null;
        }
    }

    prevData = $.grep(prevData, function (prev) { return (prev); });



   // $('#rowCount').val($('#Grid').data().tGrid.total + insertData.length - deleteData.length);

    var i = 0;
    var validationResult = true;
    $.each(prevData, function (i) {

        $('#CommodityID').val(prevData[i].CommodityID);
        $('#DispatchedQuantity').val(prevData[i].DispatchedQuantity);
        $('#RequestedQuantity').val(prevData[i].RequestedQuantity);
        if ($('#CommodityTypeID').val() == 2) { //just for the validation of non comms
            prevData[i].RequestedQuantityMT = 1;
            prevData[i].DispatchedQuantityMT = 1;
        } 

        $('#RequestedQuantityMT').val(prevData[i].RequestedQuantityMT);
        
        $('#DispatchedQuantityMT').val(prevData[i].DispatchedQuantityMT);
        
        $('#Unit').val(prevData[i].Unit); 
       // $('#CommodityGradeID').val(prevData[i].CommodityGradeID);
        $('#Description').val(prevData[i].Description);




        var container = $('#div.error');

        var formValid = $("#dispatchDetail").validate({
            errorContainer: container,
            errorLabelContainer: $("ul", container),
            ignore: ":not(:visible)",
            wrapper: 'li'
        }).form();

        $('#dispatchDetail').validate({
            errorContainer: container,
            errorLabelContainer: '#div.error', //'#validtionSummary',
            wrapper: 'li'
        });

        var groupNum = prevData[i].DispatchDetailCounter;
        if (!formValid) {
            validationResult = false;
            $('#grid-row-dispatch' + groupNum).attr('style', 'background : none repeat scroll 0 0 #ffCCCC');
        } else {
            $('#grid-row-dispatch' + groupNum).attr('style', 'background : none');
        }
        $.validator.unobtrusive.parse('#receiveDetail');

    });
    if (prevData.length == 0) {
        validationResult = false;
        $('#gridEmptyError').show();
        $('form').valid();
    }else {
        $('#gridEmptyError').hide();
    }
    
    $('#JSONPrev').val(JSON.stringify(prevData));
    $('#JSONInsertedCommodities').val(JSON.stringify(insertData));
    $('#JSONDeletedCommodities').val(JSON.stringify(deleteData));
    $('#JSONUpdatedCommodities').val(JSON.stringify(updateData));

    $('form').valid();
    return validationResult;
    
}
function submitGridandData() {

    var insertData = $.grep($('#Grid').data().tGrid.changeLog.inserted, function (toins) { return (toins); });
    var deleteData = $.grep($('#Grid').data().tGrid.changeLog.deleted, function (todel) { return (todel); });
    var updateData = $.grep($('#Grid').data().tGrid.changeLog.updated, function (toup) { return (toup); });
    var prevData = [];
    var idArray = [];
    var delIndex = [];
    var gr = $('#Grid').data('tGrid').data;


    for (var i = 0; i < updateData.length; i++) {
        // check if this element has already been added
        prevData[prevData.length] = updateData[i];
        idArray[idArray.length] = updateData[i].ReceiveDetailCounter;

    }



    for (var i = 0; i < gr.length; i++) {
        // Do not repeat what has alrady been entered as edited 
        if (gr[i].ReceiveDetailCounter == 0 || ($.inArray(gr[i].ReceiveDetailCounter, idArray) == -1)) {
            prevData[prevData.length] = gr[i];
        }
    }

    for (var i = 0; i < insertData.length; i++) {
        prevData[prevData.length] = insertData[i];
    }

    if (prevData.length == 0) {
        prevData = gr;
    }

    /**
    * THE ORDERING MATTERS DO NOT MOVE THE LINES BELOW SINCE THEY SHOULD REMOVE WHAT HAVE BEEN INSERTED ABOVE
    *
    */

    for (var i = 0; i < deleteData.length; i++) {
        // collect the elements to be deleted # N.B. we don't support deletion of (ReceiveDetailCounter //ReceiveDetailID > 0)
        if (deleteData[i].ReceiveDetailCounter <= 0)
            delIndex[delIndex.length] = deleteData[i].ReceiveDetailCounter;

    }

    for (var i = 0; i < prevData.length; i++) {
        // remove the data from the remove list 
        if (!($.inArray(prevData[i].ReceiveDetailCounter, delIndex) == -1)) {
            //prevData.splice(i, 1); //may cause modification ogf the array 
            prevData[i] = null;
        }
    }

    prevData = $.grep(prevData, function (prev) { return (prev); });


    $('#rowCount').val($('#Grid').data().tGrid.total + insertData.length - deleteData.length);

    var i = 0;
    var validationResult = true;
    $.each(prevData, function (i) {

        $('#CommodityID').val(prevData[i].CommodityID); //.trigger('change');
        $('#SentQuantityInUnit').val(prevData[i].SentQuantityInUnit);
        $('#ReceivedQuantityInUnit').val(prevData[i].ReceivedQuantityInUnit);

        if ($('#CommodityTypeID').val() == 2) { //just for the validation of non comms
            prevData[i].SentQuantityInMT = 1;
            prevData[i].ReceivedQuantityInMT = 1;
        } 
        
            $('#SentQuantityInMT').val(prevData[i].SentQuantityInMT);
            $('#ReceivedQuantityInMT').val(prevData[i].ReceivedQuantityInMT);
        
        //$('#UnitID').val(prevData[i].UnitID); //.trigger('change');

        if (prevData[i].UnitID != "" && parseInt(prevData[i].UnitID) != null) {
            $('#UnitID').val(parseInt(prevData[i].UnitID));
        } else {
            prevData[i].UnitID = null;
            $('#UnitID').val(null);
        }

        if (prevData[i].CommodityGradeID != "" && parseInt(prevData[i].CommodityGradeID) != null) {
            $('#CommodityGradeID').val(parseInt(prevData[i].CommodityGradeID));
        } else {
            prevData[i].CommodityGradeID = null;
            $('#CommodityGradeID').val(null);
        }


        $('#Description').val(prevData[i].Description);

        var container = $('#div.error');

        var formValid = $("#receiveDetail").validate({
            errorContainer: container,
            errorLabelContainer: $("ul", container),
            ignore: ":not(:visible)",
            wrapper: 'li'
        }).form();

        $('#receiveDetail').validate({
            errorContainer: container,
            errorLabelContainer: '#div.error', //'#validtionSummary',
            wrapper: 'li'
        });

        var groupNum = prevData[i].ReceiveDetailCounter;
        if (!formValid) {
            validationResult = false;
            $('#grid-row-receive' + groupNum).attr('style', 'background : none repeat scroll 0 0 #ffCCCC');
        } else {
            $('#grid-row-receive' + groupNum).attr('style', 'background : none');
        }
        $.validator.unobtrusive.parse('#receiveDetail');

    });
    if (prevData.length == 0 ) {
        validationResult = false;
        $('#gridEmptyError').show();
        $('form').valid();
    } else {
        $('#gridEmptyError').hide();
    }

    $('#JSONPrev').val(JSON.stringify(prevData));
    $('#JSONInsertedCommodities').val(JSON.stringify(insertData));
    $('#JSONDeletedCommodities').val(JSON.stringify(deleteData));
    $('#JSONUpdatedCommodities').val(JSON.stringify(updateData));

    $('form').valid();
    return validationResult;
}

function submitGiftCertificateData() {

    var insertData = $.grep( $('#Grid').data().tGrid.changeLog.inserted, function(ins) { return (ins);});
    var deleteData = $.grep( $('#Grid').data().tGrid.changeLog.deleted, function(del) { return (del); });
    var updateData = $.grep( $('#Grid').data().tGrid.changeLog.updated, function (upd) { return (upd);});

    $('#JSONInsertedGiftCertificateDetails').val(JSON.stringify(insertData));
    $('#JSONUpdatedGiftCertificateDetails').val(JSON.stringify(updateData));
    $('#JSONDeletedGiftCertificateDetails').val(JSON.stringify(deleteData));
    //$('#Grid').data().tGrid.total

    $('#rowCount').val($('#Grid').data().tGrid.total + insertData.length - deleteData.length);

    return true;
}




function dump(obj) {
    var result = [];
    $.each(obj, function (key, value) { result.push('"' + key + '":"' + value + '"'); });
    return '{' + result.join(',') + '}';
}
function onEdit(e) {
    //$('#console').append("OnEdit :: " + dump(e.dataItem) + "<br/>");
    $('#UnitID').hide();
    // $console.log("OnEdit :: " + dump(e.dataItem));
}
function onSave(e) {
    $('#console').append("OnSave :: " + dump(e.values) + "<br/>");
    //            $console.log("OnSave :: " + dump(e.values));
}
