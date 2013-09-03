$(function () {
    init_toolbar_button();
    hide_double_breadcrumb();

});
function init_datepicker() {
    $(".cats-datepicker2").ethcal_datepicker();

   // $(".cats-datepicker").calendarsPicker({calendar: $.calendars.instance('ethiopian', 'am') }).attr('style', 'background-color : #fff');
    //$.calendars.picker.setDefaults({ dateFormat: 'dd-MMM-yyyy' });
}
function init_toolbar_button() {
    var button_styles = {
        btn_new_record: { icon: "icon-plus", tooltip: "Add New Record" },
        btn_edit: { icon: "icon-pencil", tooltip: "Edit this record" },
        btn_detail: { icon: "icon-folder-open", tooltip: "View Details" },

        btn_delete: { icon: "icon-trash", tooltip: "Delete this record" },
        btn_back_to_list: { icon: "icon-list", tooltip: "Back to list" },
        btn_save: { icon: "icon-ok", tooltip: "Save Change" },
        btn_cancel: { icon: "icon-off", tooltip: "Cancel Changes" },
        
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

    })
    $(".toolbar-btn").tooltip();
}
function hide_double_breadcrumb() {
    $(".breadcrumb").each(function()
    {
        var $that = $(this);
        if ($that.data("role") != "main") {
            $that.hide();
        }
    });

}