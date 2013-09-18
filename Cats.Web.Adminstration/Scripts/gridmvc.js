/***
* Grid.Mvc
* Examples and documentation at: http://gridmvc.codeplex.com
* Version: 2.2.0
* Requires: jQuery v1.3+
* LGPL license: http://gridmvc.codeplex.com/license
*/

$.fn.extend({
    gridmvc: function (options) {
        var aObj = [];
        $(this).each(function () {
            if (!$(this).hasClass("grid-done")) {
                aObj.push(new GridMvc(this, options));
                $(this).addClass("grid-done");
            }
        });
        if (aObj.length == 1)
            return aObj[0];
        return aObj;
    }
});

GridMvc = (function () {

    function gridMvc(container, options) {
        this.jqContainer = $(container);
        if (typeof (options) == 'undefined') options = {};
        if (typeof (options.lang) == 'undefined') {
            options.lang = this.jqContainer.find("table.grid-table").attr("data-lang");
        }
        this.options = $.extend({}, this.defaults(), options);
        this.init();
    }

    gridMvc.prototype.init = function () {
        //load current lang options:
        this.lang = GridMvc.lang[this.options.lang];
        if (typeof (this.lang) == 'undefined')
            this.lang = GridMvc.lang.en;
        this.events = [];
        if (this.options.selectable)
            this.initGridRowsEvents();
        //Register default filter widgets:
        this.filterWidgets = [];
        this.addFilterWidget(new TextFilterWidget());
        this.addFilterWidget(new NumberFilterWidget());
        this.addFilterWidget(new DateTimeFilterWidget());
        this.addFilterWidget(new BooleanFilterWidget());

        this.openedMenuBtn = null;
        this.initFilters();
    };
    /***
    * Handle Grid row events
    */
    gridMvc.prototype.initGridRowsEvents = function () {
        var $this = this;
        this.jqContainer.find(".grid-row").click(function () {
            $this.rowClicked.call(this, $this);
        });
    };
    /***
    * Trigger on Grid row click
    */
    gridMvc.prototype.rowClicked = function ($context) {
        var row = $(this).closest(".grid-row");
        if (row.length <= 0)
            return;
        var gridRow = {};
        row.find(".grid-cell").each(function () {
            var columnName = $(this).attr("data-name");
            if (columnName.length > 0)
                gridRow[columnName] = $(this).text();
        });
        $context.markRowSelected(row);
        $context.notifyOnRowSelect(gridRow);
    };
    /***
    * Mark Grid row as selected
    */
    gridMvc.prototype.markRowSelected = function (row) {
        this.jqContainer.find(".grid-row.grid-row-selected").removeClass("grid-row-selected");
        row.addClass("grid-row-selected");
    };
    /***
    * Default Grid.Mvc options
    */
    gridMvc.prototype.defaults = function () {
        return {
            selectable: true,
            lang: 'en'
        };
    };
    /***
    * ============= EVENTS =============
    * Methods that provides functionality for grid events
    */
    gridMvc.prototype.onRowSelect = function (func) {
        this.events.push({ name: "onRowSelect", callback: func });
    };

    gridMvc.prototype.notifyOnRowSelect = function (row) {
        this.notifyEvent("onRowSelect", row);
    };

    gridMvc.prototype.notifyEvent = function (eventName, e) {
        for (var i = 0; i < this.events.length; i++) {
            if (this.events[i].name == eventName)
                if (!this.events[i].callback(e)) break;
        }
    };
    /***
    * ============= FILTERS =============
    * Methods that provides functionality for filtering
    */
    /***
    * Method search all filter buttons and register 'onclick' event handlers:
    */
    gridMvc.prototype.initFilters = function () {
        var filterHtml = this.filterMenuHtml();
        var $context = this;
        this.jqContainer.find(".grid-filter").each(function () {
            $(this).click(function () {
                return $context.openFilterPopup.call(this, $context, filterHtml);
            });
        });
    };
    /***
    * Shows filter popup window and render filter widget
    */
    gridMvc.prototype.openFilterPopup = function ($context, html) {
        //retrive all column filter parameters from html attrs:
        var columnType = $(this).attr("data-type") || "";
        var columnName = $(this).attr("data-name") || "";
        var filterType = $(this).attr("data-filtertype") || "";
        var filterValue = $(this).attr("data-filtervalue") || "";
        var filterUrl = $(this).attr("data-url");

        //determine widget
        var widget = $context.getFilterWidgetForType(columnType);
        //if widget for specified column type not found - do nothing
        if (widget == null)
            return false;

        //if widget allready onRendered - just open popup menu:
        if (this.hasAttribute("data-onRendered")) {
            var openResult = $context.openMenuOnClick.call(this, $context);
            if (!openResult && typeof (widget.onShow) != 'undefined')
                widget.onShow();
            return openResult;
        }
        //mark filter as onRendered
        $(this).attr("data-onRendered", "1");
        //append base popup layout:
        $(this).append(html);
        //determine widget container:
        var widgetContainer = $(this).find(".menu-popup-widget");
        //onRender target widget
        if (typeof (widget.onRender) != 'undefined')
            widget.onRender(widgetContainer, $context.lang, columnType, filterType, filterValue, function (type, value) {
                $context.closeOpenedPopups();
                $context.applyFilterValue(filterUrl, columnName, type, value);
            });
        //adding 'clear filter' button if needed:
        if ($(this).find(".grid-filter-btn").hasClass("filtered") && widget.showClearFilterButton()) {
            var inner = $(this).find(".menu-popup-additional");
            inner.append($context.getClearFilterButton(filterUrl));
        }
        var openResult = $context.openMenuOnClick.call(this, $context);
        if (typeof (widget.onShow) != 'undefined')
            widget.onShow();
        return openResult;
    };
    /***
    * Returns layout of filter popup menu
    */
    gridMvc.prototype.filterMenuHtml = function () {
        return '<div class="menu-popup" style="display: none;">\
                    <div class="menu-popup-wrap">\
                        <div class="menu-popup-arrow"></div>\
                        <div class="menu-popup-inner">\
                            <div class="menu-popup-widget"></div>\
                            <div class="menu-popup-additional"></div>\
                        </div>\
                    </div>\
                </div>';
    };
    /***
    * Returns layout of 'clear filter' button
    */
    gridMvc.prototype.getClearFilterButton = function (url) {
        if (url.length == 0)
            url = "?";
        return '<ul class="menu-list">\
                    <li><a class="grid-filter-clear" href="' + url + '">' + this.lang.clearFilterLabel + '</a></li>\
                </ul>';
    };
    /***
    * Register filter widget in widget collection
    */
    gridMvc.prototype.addFilterWidget = function (widget) {
        this.filterWidgets.push(widget);
    };
    /***
    * Return registred widget for specific column type name
    */
    gridMvc.prototype.getFilterWidgetForType = function (typeName) {
        for (var i = 0; i < this.filterWidgets.length; i++) {
            if ($.inArray(typeName, this.filterWidgets[i].getAssociatedTypes()) >= 0)
                return this.filterWidgets[i];
        }
        return null;
    };
    /***
    * Replace existed filter widget
    */
    gridMvc.prototype.replaceFilterWidget = function (typeNameToReplace, widget) {
        for (var i = 0; i < this.filterWidgets.length; i++) {
            if ($.inArray(typeNameToReplace, this.filterWidgets[i].getAssociatedTypes()) >= 0) {
                this.filterWidgets.splice(i, 1);
                this.addFilterWidget(widget);
                return true;
            }
        }
        return false;
    };
    /***
    * Applies selected filter value by redirecting to another url:
    */
    gridMvc.prototype.applyFilterValue = function (initialUrl, columnName, filterType, filterValue) {
        var columnParameterName = "grid-filter-col";
        var typeParameterName = "grid-filter-type";
        var valueParameterName = "grid-filter-val";
        if (initialUrl.length > 0)
            initialUrl += "&";
        window.location.search = initialUrl + columnParameterName + "=" + encodeURIComponent(columnName) + "&" + typeParameterName + "=" + encodeURIComponent(filterType) + "&" + valueParameterName + "=" + encodeURIComponent(filterValue);
    };
    /***
    * ============= POPUP MENU =============
    * Methods that provides base functionality for popup menus
    */
    gridMvc.prototype.openMenuOnClick = function ($context) {
        if ($(this).hasClass("clicked")) return true;
        $context.closeOpenedPopups();
        $(this).addClass("clicked");
        var popup = $(this).find(".menu-popup");
        if (popup.length == 0) return true;
        popup.show();
        popup.addClass("opened");
        $context.openedMenuBtn = $(this);
        $(document).bind("click.gridmvc", function (e) {
            $context.documentCallback(e, $context);
        });
        return false;
    };

    gridMvc.prototype.documentCallback = function (e, $context) {
        e = e || event;
        var target = e.target || e.srcElement;
        var box = $(".menu-popup.opened").get(0);
        if (typeof box != "undefined") {
            do {
                if (box == target) {
                    // Click occured inside the box, do nothing.
                    return;
                }
                target = target.parentNode;
            } while (target); // Click was outside the box, hide it.
            box.style.display = "none";
            $(box).removeClass("opened");
        }
        if ($context.openedMenuBtn != null)
            $context.openedMenuBtn.removeClass("clicked");
        $(document).unbind("click.gridmvc");
    };

    gridMvc.prototype.closeOpenedPopups = function () {
        var openedPopup = $(".menu-popup.opened");
        openedPopup.hide();
        openedPopup.removeClass("opened");
        if (this.openedMenuBtn != null)
            this.openedMenuBtn.removeClass("clicked");
    };

    return gridMvc;
})();

/***
* ============= LOCALIZATION =============
* You can localize Grid.Mvc by creating localization files and include it on the page after this script file
* This script file provides localization only for english language.
* For more documentation see: http://gridmvc.codeplex.com/documentation
*/
if (typeof (GridMvc.lang) == 'undefined')
    GridMvc.lang = {};
GridMvc.lang.en = {
    filterTypeLabel: "Type: ",
    filterValueLabel: "Value:",
    applyFilterButtonText: "Apply",
    filterSelectTypes: {
        Equals: "Equals",
        StartsWith: "StartsWith",
        Contains: "Contains",
        EndsWith: "EndsWith",
        GreaterThan: "Greater than",
        LessThan: "Less than"
    },
    code: 'en',
    boolTrueLabel: "Yes",
    boolFalseLabel: "No",
    clearFilterLabel: "Clear filter"
};
/***
* ============= FILTER WIDGETS =============
* Filter widget allows onRender custom filter user interface for different columns. 
* For example if your added column is of type "DateTime" - widget can onRender calendar for picking filter value.
* This script provider base widget for default .Net types: System.String, System.Int32, System.Decimal etc.
* If you want to provide custom filter functionality - you can assign your own widget type for column and write widget for this types.
* For more documentation see: http://gridmvc.codeplex.com/documentation
*/

/***
* TextFilterWidget - Provides filter interface for text columns (of type "System.String")
* This widget onRenders filter interface with conditions, which specific for text types: contains, starts with etc.
*/
TextFilterWidget = (function () {
    function textFilterWidget() { }
    /***
    * This method must return type of columns that must be associated with current widget
    * If you not specify your own type name for column (see 'SetFilterWidgetType' method), GridMvc setup column type name from .Net type ("System.DateTime etc.)
    */
    textFilterWidget.prototype.getAssociatedTypes = function () { return ["System.String"]; };
    /***
    * This method invokes when filter widget was shown on the page
    */
    textFilterWidget.prototype.onShow = function () {
        var textBox = this.container.find(".grid-filter-input");
        if (textBox.length <= 0) return;
        textBox.focus();
    };
    /***
    * This method specify whether onRender 'Clear filter' button for this widget.
    */
    textFilterWidget.prototype.showClearFilterButton = function () { return true; };
    /***
    * This method will invoke when user first clicked on filter button.
    * container - html element, which must contain widget layout;
    * lang - current language settings;
    * typeName - current column type (if widget assign to multipile types, see: getAssociatedTypes);
    * filterType - current filter type (like equals, starts with etc);
    * filterValue - current filter value;
    * cb - callback function that must invoked when user want to filter this column. Widget must pass filter type and filter value.
    */
    textFilterWidget.prototype.onRender = function (container, lang, typeName, filterType, filterValue, cb) {
        this.cb = cb;
        this.container = container;
        this.lang = lang;
        this.filterValue = filterValue;
        this.filterType = filterType;
        this.renderWidget();
        this.registerEvents();
    };
    /***
    * Internal method that build widget layout and append it to the widget container
    */
    textFilterWidget.prototype.renderWidget = function () {
        var html = '<div class="grid-filter-type-label">' + this.lang.filterTypeLabel + '</div>\
                    <select class="grid-filter-type">\
                        <option value="1" ' + (this.filterType == "1" ? "selected=\"selected\"" : "") + '>' + this.lang.filterSelectTypes.Equals + '</option>\
                        <option value="2" ' + (this.filterType == "2" ? "selected=\"selected\"" : "") + '>' + this.lang.filterSelectTypes.Contains + '</option>\
                        <option value="3" ' + (this.filterType == "3" ? "selected=\"selected\"" : "") + '>' + this.lang.filterSelectTypes.StartsWith + '</option>\
                        <option value="4" ' + (this.filterType == "4" ? "selected=\"selected\"" : "") + '>' + this.lang.filterSelectTypes.EndsWith + '</option>\
                    </select>\
                    <div class="grid-filter-type-label">' + this.lang.filterValueLabel + '</div>\
                    <input type="text" class="grid-filter-input" value="' + this.filterValue + '" />\
                    <div class="grid-filter-buttons">\
                        <input type="button" class="btn btn-apply" value="' + this.lang.applyFilterButtonText + '" />\
                    </div>';
        this.container.append(html);
    };
    /***
    * Internal method that register event handlers for 'apply' button.
    */
    textFilterWidget.prototype.registerEvents = function () {
        //get apply button from:
        var applyBtn = this.container.find(".btn-apply");
        //save current context:
        var $context = this;
        //register onclick event handler
        applyBtn.click(function () {
            //get selected filter type:
            var type = $context.container.find(".grid-filter-type").val();
            //get filter value:
            var value = $context.container.find(".grid-filter-input").val();
            //invoke callback with selected filter values:
            $context.cb(type, value);
        });
        //register onEnter event for filter text box:
        this.container.find(".grid-filter-input").keyup(function (event) {
            if (event.keyCode == 13) { applyBtn.click(); }
            if (event.keyCode == 27) { GridMvc.closeOpenedPopups(); }
        });
    };

    return textFilterWidget;
})();

/***
* NumberFilterWidget - Provides filter interface for number columns
* This widget onRenders filter interface with conditions, which specific for number types: great than, less that etc.
* Also validates user's input for digits
*/
NumberFilterWidget = (function () {

    function numberFilterWidget() { }

    numberFilterWidget.prototype.showClearFilterButton = function () { return true; };

    numberFilterWidget.prototype.getAssociatedTypes = function () {
        return ["System.Int32", "System.Double", "System.Decimal", "System.Byte", "System.Single", "System.Float", "System.Int64"];
    };

    numberFilterWidget.prototype.onShow = function () {
        var textBox = this.container.find(".grid-filter-input");
        if (textBox.length <= 0) return;
        textBox.focus();
    };

    numberFilterWidget.prototype.onRender = function (container, lang, typeName, filterType, filterValue, cb) {
        this.cb = cb;
        this.container = container;
        this.lang = lang;
        this.typeName = typeName;
        this.filterValue = filterValue;
        this.filterType = filterType;
        this.renderWidget();
        this.registerEvents();
    };

    numberFilterWidget.prototype.renderWidget = function () {
        var html = '<div class="grid-filter-type-label">' + this.lang.filterTypeLabel + '</div>\
                    <select class="grid-filter-type">\
                        <option value="1" ' + (this.filterType == "1" ? "selected=\"selected\"" : "") + '>' + this.lang.filterSelectTypes.Equals + '</option>\
                        <option value="5" ' + (this.filterType == "5" ? "selected=\"selected\"" : "") + '>' + this.lang.filterSelectTypes.GreaterThan + '</option>\
                        <option value="6" ' + (this.filterType == "6" ? "selected=\"selected\"" : "") + '>' + this.lang.filterSelectTypes.LessThan + '</option>\
                    </select>\
                    <div class="grid-filter-type-label">' + this.lang.filterValueLabel + '</div>\
                    <input type="text" class="grid-filter-input" value="' + this.filterValue + '" />\
                    <div class="grid-filter-buttons">\
                        <input type="button" class="btn btn-apply" value="' + this.lang.applyFilterButtonText + '" />\
                    </div>';
        this.container.append(html);
    };

    numberFilterWidget.prototype.registerEvents = function () {
        var $context = this;
        var applyBtn = this.container.find(".btn-apply");
        applyBtn.click(function () {
            var type = $context.container.find(".grid-filter-type").val();
            var value = $context.container.find(".grid-filter-input").val();
            $context.cb(type, value);
        });
        var txt = this.container.find(".grid-filter-input");
        txt.keyup(function (event) {
            if (event.keyCode == 13) { applyBtn.click(); }
            if (event.keyCode == 27) { GridMvc.closeOpenedPopups(); }
        })
            .keypress(function (event) { return $context.validateInput.call($context, event); });
        if (this.typeName == "System.Byte")
            txt.attr("maxlength", "3");
    };

    numberFilterWidget.prototype.validateInput = function (evt) {
        var $event = evt || window.event;
        var key = $event.keyCode || $event.which;
        key = String.fromCharCode(key);
        var regex;
        switch (this.typeName) {
            case "System.Byte":
            case "System.Int32":
            case "System.Int64":
                regex = /[0-9]/;
                break;
            default:
                regex = /[0-9]|\.|\,/;
        }
        if (!regex.test(key)) {
            $event.returnValue = false;
            if ($event.preventDefault) $event.preventDefault();
        }
    };

    return numberFilterWidget;
})();

/***
* DateTimeFilterWidget - Provides filter interface for date columns (of type "System.DateTime").
* If jQueryUi datepicker script included, this widget onRender calendar for pick filter values
* In other case he onRender textbox field for specifing date value (more info at http://jqueryui.com/)
*/
DateTimeFilterWidget = (function () {

    function dateTimeFilterWidget() { }

    dateTimeFilterWidget.prototype.getAssociatedTypes = function () { return ["System.DateTime"]; };

    dateTimeFilterWidget.prototype.showClearFilterButton = function () { return true; };

    dateTimeFilterWidget.prototype.onRender = function (container, lang, typeName, filterType, filterValue, cb) {
        this.jqUiIncluded = typeof ($.datepicker) != 'undefined';
        this.cb = cb;
        this.container = container;
        this.lang = lang;
        this.filterValue = filterValue;
        this.filterType = filterType;
        this.renderWidget();
        this.registerEvents();
    };

    dateTimeFilterWidget.prototype.renderWidget = function () {
        var html = '<div class="grid-filter-type-label">' + this.lang.filterTypeLabel + '</div>\
                    <select class="grid-filter-type">\
                        <option value="1" ' + (this.filterType == "1" ? "selected=\"selected\"" : "") + '>' + this.lang.filterSelectTypes.Equals + '</option>\
                        <option value="5" ' + (this.filterType == "5" ? "selected=\"selected\"" : "") + '>' + this.lang.filterSelectTypes.GreaterThan + '</option>\
                        <option value="6" ' + (this.filterType == "6" ? "selected=\"selected\"" : "") + '>' + this.lang.filterSelectTypes.LessThan + '</option>\
                    </select>' +
                        (this.jqUiIncluded ?
                            '<div class="grid-filter-datepicker"></div>'
                            :
                            '<div class="grid-filter-type-label">' + this.lang.filterValueLabel + '</div>\
                             <input type="text" class="grid-filter-input" value="' + this.filterValue + '" />\
                             <div class="grid-filter-buttons">\
                                <input type="button" class="btn btn-apply" value="' + this.lang.applyFilterButtonText + '" />\
                             </div>');
        this.container.append(html);
        //if jQueryUi included:
        if (this.jqUiIncluded) {
            var $context = this;
            var dateContainer = this.container.find(".grid-filter-datepicker");
            dateContainer.datepicker({
                onSelect: function (dateText) {
                    var type = $context.container.find(".grid-filter-type").val();
                    $context.cb(type, dateText);
                },
                defaultDate: this.filterValue,
                changeMonth: true,
                changeYear: true
            });
            if (typeof ($.datepicker.regional[this.lang.code]) != 'undefined') {
                dateContainer.datepicker("option", $.datepicker.regional[this.lang.code]);
            }
        }
    };

    dateTimeFilterWidget.prototype.registerEvents = function () {
        var $context = this;
        var applyBtn = this.container.find(".btn-apply");
        applyBtn.click(function () {
            var type = $context.container.find(".grid-filter-type").val();
            var value = $context.container.find(".grid-filter-input").val();
            $context.cb(type, value);
        });
        this.container.find(".grid-filter-input").keyup(function (event) {
            if (event.keyCode == 13) {
                applyBtn.click();
            }
        });
    };

    return dateTimeFilterWidget;
})();

/***
* BooleanFilterWidget - Provides filter interface for boolean columns (of type "System.Boolean").
* Renders two button for filter - true and false
*/
BooleanFilterWidget = (function () {

    function booleanFilterWidget() { }

    booleanFilterWidget.prototype.getAssociatedTypes = function () { return ["System.Boolean"]; };

    booleanFilterWidget.prototype.showClearFilterButton = function () { return true; };

    booleanFilterWidget.prototype.onRender = function (container, lang, typeName, filterType, filterValue, cb) {
        this.cb = cb;
        this.container = container;
        this.lang = lang;
        this.filterValue = filterValue;
        this.filterType = filterType;
        this.renderWidget();
        this.registerEvents();
    };

    booleanFilterWidget.prototype.renderWidget = function () {
        var html = '<div class="grid-filter-type-label">' + this.lang.filterValueLabel + '</div>\
                    <ul class="menu-list">\
                        <li><a class="grid-filter-choose ' + (this.filterValue == "true" ? "choose-selected" : "") + '" data-value="true" href="javascript:void(0);">' + this.lang.boolTrueLabel + '</a></li>\
                        <li><a class="grid-filter-choose ' + (this.filterValue == "false" ? "choose-selected" : "") + '" data-value="false" href="javascript:void(0);">' + this.lang.boolFalseLabel + '</a></li>\
                    </ul>';
        this.container.append(html);
    };

    booleanFilterWidget.prototype.registerEvents = function () {
        var $context = this;
        var applyBtn = this.container.find(".grid-filter-choose");
        applyBtn.click(function () {
            $context.cb("1" /* Equals */, $(this).attr("data-value"));
        });
    };

    return booleanFilterWidget;
})();