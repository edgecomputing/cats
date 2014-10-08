
(function ($) {
    var _ethdatepickerInitialized = false;
    $.fn.ethcal_datepicker = function (options) {
        _ethdatepicker_init();
        this.each(function () { 
            var that = this;
            
            //$(that).datepicker();
            var geezStr="";
            var eth_date = new EthDate();
            var greg_date;
            var tag=this.tagName;
            //console.log(tag);
            if(tag=="INPUT")
            {
                if (!$(that).val()) {
                    greg_date = new Date();
                    //$(that).val(greg_date.toLocaleDateString());
                }
                
                eth_date.fromGregStr($(that).val());
                if ($(that).val()) {
                    geezStr=eth_date.toString();
                }
                
                var eth_date_input = $($(that).clone()).insertAfter($(this));
                
                    eth_date_input.removeClass("cats-datepicker2").attr("name","")
                    .val(geezStr)
                    .click(function () { _ethdatepicker_show(this); })
                    .change(function(){
                        var $this=$(this);
                        var selected_date = new EthDate();
                            if ($this.val()) {
                                var $gregInput=$this.attr("data-greg_input");
                                selected_date.parse($this.val());
                                if($this.val()!=selected_date.toString())
                                {
                                   $this.tooltip("show")
                                   .val("");
                                }
                                //$this.val(selected_date.toString());
                                $gregInput.val(selected_date.toGreg().toLocaleDateString());
                            }
                    })
                    .tooltip({trigger:"hover manual",title:"dd/mm/yyyy"})
                    .attr("placeholder","dd/mm/yyyy");
   // .tooltip(options)
                eth_date_input.data("greg_input", this);
                eth_date_input.blur(function (e) { _handle_blur(e,this) });
                var dp=_build_date_picker(eth_date_input);
                dp.displayStyle="popup";
                
                $(this).data("eth_date_input", eth_date_input);
                $(this).css({opacity: 0.5,display:"none"});
                
            }
            else
            {
                var geezDate=[""];
                if(options)
                {
                    if(typeof(options)!="object")
                    {
                       geezDate=options; 
                    }
                    else if(options.date)
                    {
                        geezDate=options.date;
                        
                        
                    }
                }
                
                
                if(typeof(geezDate)!="object")
                    {
                        geezDate=[geezDate];
                    }
                 console.log(typeof(geezDate))   
                for(var i in geezDate)
                {
                    var dp=_build_date_picker($(this));
                    var eth_date_input = $("<input type='hidden' />").appendTo($(this))
                        .val(geezDate[i])
                        .data("greg_input", this);
                   // $(this).data("eth_date_input", eth_date_input);
                    $(this).data("eth_date_input", eth_date_input);
                    dp.displayStyle="inline";
                    dp.input=$(eth_date_input);
                    dp.ui.container.appendTo($(this));
                    dp.ui.container.css({position:"",display:"inline-table"});//.show();
                    eth_date_input.data("date_picker",dp)
                  //  $("<div>this is thdslkdjf</div>").appendTo($(this));
                    //console.log(dp.ui.container);
                    _ethdatepicker_show(eth_date_input);  
                }

            }

        });




        return this;

    }
    function getInputDate(options,input)
    {
        
    }
    var test_coutn = 0;
    var _handle_blur = function(evt,elem)
    {
        var input=$(elem);
        var date_picker=input.data("date_picker")
       var hide=1;
       var clicked = $(evt.relatedTarget);
       var count = 0;
       var htm = "";
      // htm += date_picker.attr("id");
       $(".hover").each(function () { hide = false; });
       if (hide) {
           date_picker.displayed = 0;
           date_picker.selected_month = "";
           date_picker.ui.container.hide();
         
       }
       else {
           input.focus();
       }
       return;
       while (clicked && count < 10) {
           htm += "<br/>" + clicked.attr("id");// + " " + clicked.val();
           if (date_picker.attr("id") == clicked.attr("id")) {
                hide = false;
            }
            clicked = clicked.parent();
            count++;
        }
        //  alert($(evt.target).parent())
        if (hide) {
            date_picker.hide();
        }
        $("#div_test").html(htm);
       // $(elem).focus();
        //alert(elem);
    }
    var _build_date_picker = function (input) {
        var containerhtm = "<div class=\"ui-ethdatepicker ui-datepicker ui-widget ui-widget-content ui-helper-clearfix ui-corner-all\" style=\"position: absolute; z-index: 1; display: block; left:900px; display:none;\" >";
        
        containerhtm += "</div>";

        var eth_date_picker = $(containerhtm).insertAfter($(input));

        var $header = $("<div id=\"ui-ethdatepicker-header\" class=\"ui-datepicker-header ui-widget-header ui-helper-clearfix ui-corner-all\"></div>").appendTo(eth_date_picker);

        var $body = $("<div  id='ui-ethdatepicker-body'>Body<br/>Body</div>").appendTo(eth_date_picker);
        var $debug = $("<div style='display:none;'>debug</div>").appendTo(eth_date_picker);

        var $prevbtn = $('<a class="ui-datepicker-prev ui-corner-all" data-handler="prev" data-event="click" title="Prev"><span class="ui-icon ui-icon-circle-triangle-w">Prev</span></a>').appendTo($header);
        var $nextbtn = $('<a class="ui-datepicker-next ui-corner-all" data-handler="next" data-event="click" title="Next"><span class="ui-icon ui-icon-circle-triangle-e">Next</span></a>').appendTo($header);

//        var $title = $(' <div class="ui-datepicker-title"><span class="ui-datepicker-month" id="ui-ethdatepicker-current-month">#Month#</span>&nbsp;<span class="ui-datepicker-year" id="ui-ethdatepicker-current-year">#Year#</span></div></div>').appendTo($header);
        var $title = $('<div class="ui-datepicker-title"></div>').appendTo($header);
        var monthhtm = _write_month_option();
        var yearhtm = _write_year_option();
        var $month = $(monthhtm).appendTo($title);
        var $year = $(yearhtm).appendTo($title);
        var yearmonth=$("<div class='year-month'>Month : Year</div>").appendTo($title);
        
        var eth_date_pickerdata = { ui: { container: eth_date_picker, body: $body, year: $year, month: $month,debug:$debug,yearmonth:yearmonth }, input: input,displayed:0 };
        $(input).data("date_picker", eth_date_pickerdata);
        $month.data("date_picker", eth_date_pickerdata);
        $year.data("date_picker", eth_date_pickerdata);

        var month_yr_changed = function (date_picker) {
            
            //---------------------
          
            var selected_date = new EthDate();
            
            //---------------------
            var m = date_picker.ui.month.val() / 1 + 1;
           // alert(m);
            var yr = date_picker.ui.year.val();
            var selected_month = new EthDate(yr, m, 1);
            date_picker.selected_month = selected_month;
         
         
            _ethdatepicker_show_month(date_picker, selected_month);
            
            _attach_eth_cal_event(date_picker);
            date_picker.input.focus();
        };

        $month.change(function () { 
            var dp = $(this).data("date_picker");
            month_yr_changed($(this).data("date_picker"));
            return;
            var m = dp.ui.month.val()/1+1;
            var yr = dp.ui.year.val();
            var selected_month = new EthDate(yr, m, 1);
            dp.selected_month = selected_month;
            dp.ui.debug.html(dp.ui.month.val());

        });
        $year.change(function () {month_yr_changed($(this).data("date_picker"));});
        return eth_date_pickerdata;
    }
    var _write_month_option=function()
    {
        var htm="";
        htm+='<select class="ui-datepicker-month" >';
        for(var i in month_name_amh)
        {
            var mi=i/1;
            htm+="<option value='" + mi + "'>" + month_name_amh[i] + "</option>"
        }
        htm+="</select>";
       
        return htm;
    }
    var _write_year_option = function () {
        var htm = "";
        htm += '<select class="ui-datepicker-year" >';
        for (var i = 1990; i < 2030;i++) {
           
            htm += "<option value='" + i + "'>" +i + "</option>"
        }
        htm += "</select>";
        return htm;
    }
    var _ethdatepicker_init = function () {
        if (_ethdatepickerInitialized) {
           return;
        }

        _ethdatepickerInitialized = true;
    }
    var _ethdatepicker_show = function (target,dispdate) {
        //var txtdtgr = $(target).data("greg_input").value;
        //var converted = new EthDate();
        var datepicker = $(target).data("date_picker");
        dispdate = datepicker.selected_month;
        if (datepicker.displayed) {
         //   return;
        }
        datepicker.displayed = 1;
        var txtdt = $(target).val();
        var top = target.offsetHeight + target.offsetTop;
        
        var selected_date = new EthDate();
        if (txtdt) {
            selected_date.parse(txtdt);
        }
         if (!dispdate) 
        {
            datepicker.ui.yearmonth.html(month_name_amh[selected_date.month-1] + " - " + selected_date.year);
            datepicker.ui.year.val(selected_date.year);
            datepicker.ui.month.val(selected_date.month - 1);
         }
         if (dispdate) {
            // datepicker.ui.year.val(dispdate.year);
            // datepicker.ui.month.val(dispdate.month - 1);
         }
         var left = target.offsetLeft;


         var container = datepicker.ui.container;
         container.css("top", top);
         container.css("left", left);
         container.show();
         container.focus();
        _ethdatepicker_show_month(datepicker, selected_date, target, dispdate);
        _attach_eth_cal_event(datepicker);
       // datepicker.ui.month.val(selected_date.month);
      //  datepicker.ui.body.html(selected_date.month);

        /*
        var $datepicker = $("#ui-ethdatepicker-div");
        $datepicker.css("top", top);
        $datepicker.css("left", left);
        $datepicker.data("target_id", $(target).attr("id"));
        var selected_date = new EthDate();
        if (txtdt) {
            selected_date.parse(txtdt);
        }
        _ethdatepicker_show_month($datepicker, selected_date, target);
        $datepicker.show();
        $(target).data("date_picker").show()
        _attach_eth_cal_event($("#ui-ethdatepicker-div"));*/
    };

    var _ethdatepicker_show_month = function ($datepicker, selected_date, target, diaplayed_date) {

       
        if (!diaplayed_date) {
            diaplayed_date = new EthDate(selected_date.year, selected_date.month, 1);
        }

        
        var eth_date = new EthDate(diaplayed_date.year, diaplayed_date.month, 1);
        var greg_date = eth_date.toGreg();
        var startdayofmonth = eth_date;
        var startdayofweek = greg_date.mGetDay();

        if (startdayofweek > 0) {
            greg_date.setTime(greg_date.getTime() - (startdayofweek) * day);
        }
        else {
            greg_date.setTime(greg_date.getTime() - 7 * day);
        }

        var headHtm = '<thead><tr>';
        for (var i in day_name_amh_short) {
           
            headHtm += '<th><span title="' + day_name_amh[i] + '">' + day_name_amh_short[i] + '</span></th>';
        }
        headHtm += '</tr></thead>';

        var d = 1;
        var md = d - startdayofweek;
        var today=(new Date()).toLocaleDateString();
        var monthCount = 0;
        var weeksCount=0;
        var bodyHtm = '<tbody>';
        while (monthCount < 2) {
            var rowhtm = "";
            var displayedWeek=0;
            rowhtm += '<tr>';
            for (var i in day_name_amh_short) {
                eth_date.fromGreg(greg_date);
                md = d - startdayofweek;
                var cellHtm = "";
                var tdcssclass = "";
                var ancclass = "";
                if (eth_date.month == diaplayed_date.month) {
                    ancclass += " ui-state-default";
                    monthCount = 1;
                    displayedWeek=1;
                }
                else {
                    tdcssclass += "  ui-datepicker-other-month ui-datepicker-unselectable ui-state-disabled";

                }
                if(eth_date.toGreg().toLocaleDateString()==today)
                {
                    tdcssclass +="ui-state-highlight"
                }
                if (selected_date.toString() == eth_date.toString()) {
                    tdcssclass += " ui-datepicker-current-day "// ui-datepicker-today";
                    ancclass += " ui-state-active";
                }
                cellHtm += '<td class="' + tdcssclass + '" data-month="' + eth_date.month + '" data-year="' + eth_date.year + '" >';
                if (eth_date.month == diaplayed_date.month) {
                    cellHtm += '    <a class="' + ancclass + '" href="javascript:">';
                    cellHtm += eth_date.date;
                    cellHtm += '    </a>';
                }
                cellHtm += '</td>';
                greg_date.setTime(greg_date.getTime() + day);



                rowhtm += cellHtm;
            }
            rowhtm += '</tr>';
            eth_date.fromGreg(greg_date);
            if (eth_date.month == diaplayed_date.month) {
                monthCount = 1;
            }
            else {
                monthCount = monthCount == 1 ? 2 : monthCount;
                
            }
            if(displayedWeek)
            {
                weeksCount++;
                bodyHtm += rowhtm;
            }
            
            
        }
        if(weeksCount==5)
        {
            bodyHtm+="<tr><td style='visibility:hidden;'><a class='ui-state-default' href='#'>&nbsp;</a></td></tr>";
        }
        bodyHtm += '</tbody>';

        var dtpicker_htm = headHtm + bodyHtm;
        var className="ui-datepicker-calendar ";
        var dtpicker_htm = '<table class="' + className + '">' + headHtm + bodyHtm + '</table>';
        //					'<th class="ui-datepicker-week-end"><span title="Sunday">Su</span></th>'
        $datepicker.ui.container.addClass("calendar_" + $datepicker.displayStyle)
        $datepicker.ui.body.html(dtpicker_htm);
        $datepicker.ui.body.find("td >a")
				//.data("year", selected_date.year)
                .hover(function () { $(this).addClass("ui-state-hover"); }, function () { $(this).removeClass("ui-state-hover"); })
				.parent()
               // .click(function () { alert(this);})
				.data("target", $(target))
                .data("date_picker", $datepicker)
                
       
       // var m = $("#ui-ethdatepicker-current-month").val();
     //   $("#ui-ethdatepicker-debug").html(selected_date.toString() + "<br/>" + m);

        //$("#ui-ethdatepicker-current-month").html(month_name_amh[selected_date.month - 1]);
     //   $("#ui-ethdatepicker-current-month").val(diaplayed_date.month);
      //  $("#ui-ethdatepicker-current-year").html(selected_date.year);

        //	$datepicker.find("table").html(dtpicker_htm);
    }
    
    var _attach_eth_cal_event = function (date_picker) {
        var cal = date_picker.ui.container;
        cal.hover(function() {$(this).addClass("hover");},function() {$(this).removeClass("hover");});
     //   date_picker.input.blur(function (e) { alert(e.target) });
        cal.find("td > a").parent().click(
															function (e) {
															    var _this = $(this);
															    var date = _this.find("a").html();
															    var selected_date = new EthDate(_this.data("year")/1, _this.data("month")/1, date/1);
															    $("#ui-ethdatepicker-debug").html(selected_date.toString());
															    var target = _this.data("target");
															    target.val(selected_date);
															    date_picker.input.val(selected_date);
															    //  new Date().toLocaleString
															    var greg_date = selected_date.toGreg();
															    $(target.data("greg_input")).val(greg_date.toLocaleDateString());
															    cal.removeClass("hover");
															 //   datepicker.displayed = 0;
                                                            // console.log(selected_date.toString());
                                                             cal.find(".ui-state-active").removeClass("ui-state-active");
                                                             _this.find("a").addClass("ui-state-active")
                                                                if(date_picker.displayStyle!="inline")
                                                                {
                                                                    
                                                                    cal.hide();
                                                                }
															    
															    
															    //_ethdatepicker_show_month(cal);
															}
															)
    }

})(jQuery);