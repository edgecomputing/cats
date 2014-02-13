var lastTarget;
function show_state_editor(elem) {
    var editor = $("#popover_state_editor");
    //$(elem).popover('show')
    $target = $(elem);
    var htm = "<div class='small_form' id='frm_current'>" + $("#frm_state_template").clone().html() + "</div>";
    
    if (lastTarget) {
        if ($target.attr("id") == lastTarget.attr("id")) {
            return;
        }
        else {
            lastTarget.popover('destroy');
        }
    }

    $target.popover({ html: true, trigger: "manual", title: "Edit Title", content: htm,placement:'bottom' });
    $target.popover('show');
    lastTarget = $target;
    $(".btn_popup_cancel").click(function () { lastTarget.popover('destroy'); });
    return;
}

function show_popup(popup, left, top) {
    popup.css("left", left).css("top", top).show();
}