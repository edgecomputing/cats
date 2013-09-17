    function parseJsonDate(jsonDate) {
        var offset = new Date().getTimezoneOffset() * 60000;
        var parts = /\/Date\((-?\d+)([+-]\d{2})?(\d{2})?.*/.exec(jsonDate);

        if (parts[2] == undefined)
            parts[2] = 0;

        if (parts[3] == undefined)
            parts[3] = 0;

        return new Date(+parts[1] + offset + parts[2] * 3600000 + parts[3] * 60000);
    }

    function isReceiveNull(grnNo) {
        var params = {};
        params['grnNo'] = grnNo;

        $.getJSON("/Receive/isReceiveNull", params, function (receive) {

            if (receive.success && receive.defaultWareHouse) {
                $('#ajax_loading').show();
                //alert("There is an existing record with GRN = " + grnNo + ", and it will be loaded Automatically");
                loadReceive(grnNo); //$(this).val());
            } else if (receive.success && !receive.defaultWareHouse) {

                //alert("There is an existing record with GRN = " + grnNo + ", at another warehouse");
                loadReceive(grnNo);

            } else if ($('#ReceiveID').val() != ""){//-1) {// == !receive.success && !receive.defaultWareHouse

                $("<div id='dialog'><strong>Are you sure you want to change the GRN Number for this recipt?</strong><br/>Press Cancel to drop changes, Press Confirm to apply the change to the GRN or Press New for Creating a new Recipt.</div>").dialog({
                    autoOpen: true,
                    modal: true,
                    title: "Confirm changing GRN",
                    content: "",
                    buttons: {
                        "Yes": function () {
                            //loadReceiveData($('#GRN').val()); implement a Chaage GRN function in the controller for the Current Receive ID
                            //setGRNByByReceiveID($('#ReceiveID').val(), $('#GRN').val()) returns true if successful else return false e.g. duplicate GRN
                            //then load the JSON data        
                            $(this).dialog("close");
                        },
                        "Cancel": function () {
                            loadReceiveByReceiveID($('#ReceiveID').val()); //resets the Grn Number
                            $(this).dialog("close");
                        },
                        "Create New": function () {
                            //loadReceiveByReceiveID($('#ReceiveID').val()); //creates a new recipt
                            loadReceive(grnNo); //$(this).val());
                            $(this).dialog("close");
                        }
                    }
                });
            }
            //            else {
            //                $('#ajax_loading').show();
            //            }
        });
       


    }

    function loadReceiveByReceiveID(ReceiveID) {

        var params = {};
        params['ReceiveID'] = ReceiveID;

        $.getJSON("/Receive/GetGrnByReceiveID", params, function (grnNumber) {
            $('#GRN').val(grnNumber.receive);
        });

    }

    function setGRNByByReceiveID( ReceiveID, GRN) {

        var params = {};
        params['ReceiveID'] = ReceiveID;
        params['GRN'] = GRN;

        $.getJSON("/Receive/setGRNByByReceiveID", params, function (GRNchange) {
            $('#GRN').val(GRNchange.isTrue);
        });
        
    }

    function loadReceiveData(grnNo) {

    var params = {};
    params['grnNo'] = grnNo;

    $.getJSON('@Url.Action("JsonReceive", "Receive")', params, function (receive) {
        $('#ajax_loading').hide();
        wireUpAuditLinks();
        if (receive == null) {
            // $('#receive_form *').val("");
            return;
        }
        $('#ReceiveID').val(receive.ReceiveID);
        $('#SINumber').val(receive.SINumber);
        $('#WarehouseID').val(receive.WarehouseID);
        $('#WarehouseID').change();
        $('#TransporterID').val(receive.TransporterID);
        $('#DriverName').val(receive.DriverName);
        $('#PlateNo_Prime').val(receive.PlateNo_Prime);
        $('#PlateNo_Trailer').val(receive.PlateNo_Trailer);
        var datePicker = $("#ReceiptDate").data("tDatePicker");
        datePicker.value(parseJsonDate(receive.ReceiptDate));
        $('#CommodityTypeID').val(receive.CommodityTypeID);
        $('#CommodityID').val(receive.CommodityID);
        $('#StackNumber').val(receive.StackNumber);
        $('#StoreID').val(receive.StoreID);
        $('#ProjectNumber').val(receive.ProjectNumber);
        $('#ProjectID').val(receive.ProjectID);
        $('#CommoditySourceID').val(receive.CommoditySourceID);
        $('#SourceDonorID').val(receive.SourceDonorID);
        $('#TicketNumber').val(receive.TicketNumber);
        $('#WeightBeforeUnloading').val(receive.WeightBeforeUnloading);
        $('#WeightAfterUnloading').val(receive.WeightAfterUnloading);
        $('#WayBillNo').val(receive.WayBillNo);
        $('#ProgramID').val(receive.ProgramID);
        $('#Remark').val(receive.Remark);

        $('#Year').change();
        var gr = $('#Grid').data('tGrid');
        gr.rebind({ receiveId: receive.ReceiveID });

    });

}

function loadReceive(grnNo) {
    var params = {};
    params['grnNo'] = grnNo;
    $('#receive_partial').load("/Receive/_ReceivePartial/", { grnNo: grnNo });
}

