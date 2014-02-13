$(function () {
    init_toolbar_button();
   

});
function init_datepicker() {
    $(".cats-datepicker2").ethcal_datepicker();
    
}
function init_toolbar_button() {
    var button_styles = {
        btn_new_record: { icon: "icon-plus", tooltip: "Add New Record" },
        btn_edit: { icon: "icon-edit", tooltip: "Edit this record" },
        btn_detail: { icon: "icon-folder-open", tooltip: "View Details" },

        btn_delete: { icon: "icon-trash", tooltip: "Delete this record" },
        btn_back_to_list: { icon: "icon-list", tooltip: "Back to list" },
        btn_save: { icon: "icon-save", tooltip: "Save Change" },
        btn_cancel: { icon: "icon-off", tooltip: "Cancel Changes" },
        btn_reload: { icon: "icon-retweet", tooltip: "Reload Data" },
        btn_forward: { icon: "icon-arrow-right", tooltip: "Continue" },
        btn_back: { icon: "icon-arrow-left", tooltip: "Back" }
    }
    
    $(".toolbar-btn").each(function () {
        var $that = $(this);
        var htm = '<input/>';
        var typ = $that.data("buttontype");
        var frm = $that.data("submittedform");
        if (typ && button_styles[typ]) {
            var bs = button_styles[typ];
            $that.append("<i class=\"" + bs.icon + "\">").attr("title", bs.tooltip);
            //$that.append("<span>s" + typ + "</span>");
        }
        if (frm) {
            $that.click(function () {
                document.getElementById(frm).submit();
            });
        }

    }); /*for (var i in button_styles) {
        var bs = button_styles[i];
        $("." + i).append("<i class=\"" + bs.icon + "\">").attr("title", bs.tooltip);
    }*/
    $(".toolbar-btn").tooltip();
}