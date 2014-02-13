function showDialogWithYesNo(myid, title, msg) {
    $(myid).dialog({
        modal: true,
        resizable: false,
        width: "500px",
        buttons: {
            'Yes': function () {
                $(this).dialog('close');
            },
            'No': function () {
                window.location = '';
            }
        },
        closeOnEscape: false,
        title: title
    });
    $(".ui-dialog-titlebar-close").hide(); // a workaround to remove the x button
    $(".ui-widget-overlay").css({ 'opacity': '0.85' });
    $('.ui-dialog-content').append(msg);
    return false;
}

function showDialogError(myid, msg) {
    $(myid).dialog({
        modal: true,
        resizable: false,
        width: "500px",
        buttons: {
            'OK': function () {
                $(this).dialog('close');
            }
        },
        closeOnEscape: false,
        title: "Error!",
        focus: function (event, ui) {
            $(this).parents(".ui-dialog:first").find(".ui-dialog-titlebar").addClass("ui-state-error");
        }
    });
    $(".ui-widget-overlay").css({ 'opacity': '0.85' });
    $('.ui-dialog-content').html('<div style="float: left; padding-right: 30px"><img src="../../images/messagebox_warning.png" /></div>').append(msg);
    return false;
}

function showDialog2(myid, url, title, msg) {
    $(myid).load(url).dialog({
        modal: true,
        resizable: false,
        width: "500px",
        title: title

    });
    $(".ui-widget-overlay").css({ 'opacity': '0.85' });
    $('.ui-dialog-content').append(msg);
    return false;
}

function showDialog(myid, title, msg) {
    $(myid).dialog({
        modal: true,
        resizable: false,
        width: "500px",
        title: title
    });
    $(".ui-widget-overlay").css({ 'opacity': '0.85' });
    $('.ui-dialog-content').append(msg);
    return false;
}