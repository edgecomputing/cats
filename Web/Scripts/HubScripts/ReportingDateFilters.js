function parseJsonDate(jsonDate) {
    var offset = new Date().getTimezoneOffset() * 60000;
    var parts = /\/Date\((-?\d+)([+-]\d{2})?(\d{2})?.*/.exec(jsonDate);

    if (parts[2] == undefined)
        parts[2] = 0;

    if (parts[3] == undefined)
        parts[3] = 0;

    return new Date(+parts[1] + offset + parts[2] * 3600000 + parts[3] * 60000);
}



function GetEthiopianDate(gSelectedYear, gSelectedMonth, gSelectedDay) {
    var eCalendar = $.calendars.instance('ethiopian', 'am');
    var gCalendar = $.calendars.instance();
    var jdate = gCalendar.newDate(gSelectedYear, gSelectedMonth + 1, gSelectedDay + 1).toJD(); //gSelectedMonth, gSelectedDay)
    return eCalendar.fromJD(jdate);
}

//        function OnShowReport() {
//            $('#report_area').html('Loading ...');
//            
//          //  var period = $('input:radio[name=period]:checked').val();period dateSelection.SelectionMode
//            var operation = $('input:radio[name=operation]:checked').val();
//            var from = $("#FromDate").val();
//            var to = $("#ToDate").val();
//            $('#report_area').html('Loading ... <b></b> ' + operation + ' from <b>' + from + '</b> to <b>' + to + '</b>');

//        }



// var SelectionMode = "Year";
$(function () {
    if (localeLang == "am") {
        $('#ToDateAm').calendarsPicker({ calendar: $.calendars.instance('ethiopian', 'am') });
        $('#FromDateAm').calendarsPicker({ calendar: $.calendars.instance('ethiopian', 'am') });
        $.calendars.picker.setDefaults({ dateFormat: 'dd-MMM-yyyy' });

        $('.calendarspicker').calendarsPicker({
            calendar: $.calendars.instance('ethiopian', 'am'),
            disableInput: true
        });

        $.calendars.picker.setDefaults({ dateFormat: 'dd-MMM-yyyy' });
    }
});


$(document).ready(function () {

    var dateSelection = {
        SelectionMode: 'Year',
        LeftNavControl: $('#datePrev'),
        RightNavControl: $('#dateNext'),
        LabelControl: $('#dateFilter'),
        StartDate: new Date(new Date(Date.now()).getFullYear(), 0, 1), //
        EndDate: new Date(new Date(Date.now()).getFullYear(), 11, 31), //
        SelectedYear: new Date(Date.now()).getFullYear(),
        SelectedMonth: new Date(Date.now()).getMonth(),
        SelectedQuarter: Math.floor(new Date(Date.now()).getMonth() / 3),
        SelectedDay: new Date(Date.now()).getDate(),
        MonthNames: new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"),
        MonthAbbreviations: new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"),
        MonthNamesAm: new Array('መስከረም', 'ጥቅምት', 'ኅዳር', 'ታህሣሥ', 'ጥር', 'የካቲት', 'መጋቢት', 'ሚያዝያ', 'ግንቦት', 'ሰኔ', 'ሐምሌ', 'ነሐሴ', 'ጳጉሜ'),
        MonthAbbreviationsAm: new Array('መስከረም', 'ጥቅምት', 'ኅዳር', 'ታህሣሥ', 'ጥር', 'የካቲት', 'መጋቢት', 'ሚያዝያ', 'ግንቦት', 'ሰኔ', 'ሐምሌ', 'ነሐሴ', 'ጳጉሜ'),
        Quarters: new Array("Q1", "Q2", "Q3", "Q4"),
        QuartersAm: new Array("ጸደይ", "በጋ", "በልግ", "ክረምት"), //መጸው
        Days: new Array("1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"),
        LeftClick: function () {
            if (dateSelection.SelectionMode == 'Year') {
                //if month is set 
                if (dateSelection.SelectedMonth == 1 && dateSelection.SelectedDay >= 27) {
                    if (dateSelection.SelectedYear - 1 % 4 == 0) {
                        dateSelection.SelectedDay = 28;
                    } else {
                        dateSelection.SelectedDay = 27;
                    }
                }
                dateSelection.SelectedYear -= 1;
            }
            if (dateSelection.SelectionMode == 'Month') {
                if (dateSelection.SelectedMonth == 9 && localeLang == "am") {
                    //the return date will fall in the 13th ethipan date month
                    //                            if (dateSelection.SelectedDay == 8) {//TODO going back from 13 month 
                    //                                //dateSelection.SelectedMonth = 1;
                    //                                dateSelection.SelectedMonth -= 1;
                    //                                dateSelection.SelectedDay = 2;
                    //                            } else {
                    dateSelection.SelectedDay = 8;
                    dateSelection.SelectedMonth -= 1;
                    //                            }
                    //                        
                } else if (dateSelection.SelectedMonth == 8) {
                    if (dateSelection.SelectedDay == 8 && localeLang == "am") {
                        dateSelection.SelectedDay = 2;
                        //      dateSelection.SelectedMonth = 8;
                    } else {
                        dateSelection.SelectedMonth -= 1;
                        dateSelection.SelectedDay = 2;
                    }
                }
                else {
                    if (dateSelection.SelectedMonth == 2 && dateSelection.SelectedDay >= 27) {
                        if (dateSelection.SelectedYear % 4 == 0) {
                            dateSelection.SelectedDay = 28;
                        } else {
                            dateSelection.SelectedDay = 27;
                        }
                    }
                    if (dateSelection.SelectedMonth == 0) {
                        dateSelection.SelectedMonth = 11; //TODO add a check not to show feb 29,30,31 on month change
                        dateSelection.SelectedYear -= 1;
                    } else {
                        dateSelection.SelectedMonth -= 1;
                    }
                }
            }
            if (dateSelection.SelectionMode == 'Quarter') {
                if (dateSelection.SelectedQuarter == 0) {
                    dateSelection.SelectedQuarter = 3;
                    dateSelection.SelectedYear -= 1;
                }
                else {
                    dateSelection.SelectedQuarter -= 1;
                }
            }
            if (dateSelection.SelectionMode == 'Daily') {
                if (dateSelection.SelectedMonth == 0 || dateSelection.SelectedMonth == 1 || dateSelection.SelectedMonth == 3 || dateSelection.SelectedMonth == 5 || dateSelection.SelectedMonth == 7 || dateSelection.SelectedMonth == 8 || dateSelection.SelectedMonth == 10) {
                    if (dateSelection.SelectedDay == 0) {
                        if (dateSelection.SelectedMonth == 0) {
                            dateSelection.SelectedDay = 30;
                            dateSelection.SelectedMonth = 11;
                            dateSelection.SelectedYear -= 1;
                        } else {
                            dateSelection.SelectedDay = 30;
                            dateSelection.SelectedMonth -= 1;
                        }
                    } else {
                        dateSelection.SelectedDay -= 1;
                    }
                } else if (dateSelection.SelectedMonth == 2) { //going back to february add leap year here
                    if (dateSelection.SelectedDay == 0) {
                        if (dateSelection.SelectedYear % 4 == 0) {
                            dateSelection.SelectedDay = 28;
                        } else {
                            dateSelection.SelectedDay = 27;
                        }
                        dateSelection.SelectedMonth -= 1;

                    } else {
                        dateSelection.SelectedDay -= 1;
                    }
                } else { //with 30  3,5,8,10
                    if (dateSelection.SelectedDay == 0) {
                        dateSelection.SelectedDay = 29;
                        dateSelection.SelectedMonth -= 1;

                    } else {
                        dateSelection.SelectedDay -= 1;
                    }
                }
            }

            dateSelection.show();
            setDates(dateSelection);
        },

        RightClick: function () {
            if (dateSelection.SelectionMode == 'Year') {
                if (dateSelection.SelectedMonth == 1 && dateSelection.SelectedDay >= 27) {
                    if (dateSelection.SelectedYear + 1 % 4 == 0) {
                        dateSelection.SelectedDay = 28;
                    } else {
                        dateSelection.SelectedDay = 27;
                    }

                }
                dateSelection.SelectedYear += 1;
            }
            if (dateSelection.SelectionMode == 'Month') {

                if (dateSelection.SelectedMonth == 8 && localeLang == "am") {
                    //the return date will fall in the 13th ethipan date month
                    if (dateSelection.SelectedDay == 8) {
                        dateSelection.SelectedMonth += 1;
                        dateSelection.SelectedDay = 2;
                    } else {
                        dateSelection.SelectedDay = 8;
                    }
                }
                else {
                    if (dateSelection.SelectedMonth == 0 && dateSelection.SelectedDay >= 27) {
                        if (dateSelection.SelectedYear % 4 == 0) {
                            dateSelection.SelectedDay = 28;
                        } else {
                            dateSelection.SelectedDay = 27;
                        }

                    }
                    if (dateSelection.SelectedMonth == 11) {
                        dateSelection.SelectedMonth = 0;
                        dateSelection.SelectedYear += 1;
                    } else {
                        dateSelection.SelectedMonth += 1;
                    }
                }
            }
            if (dateSelection.SelectionMode == 'Quarter') {
                if (dateSelection.SelectedQuarter == 3) {
                    dateSelection.SelectedQuarter = 0;
                    dateSelection.SelectedYear += 1;
                }
                else {
                    dateSelection.SelectedQuarter += 1;
                }
            }
            if (dateSelection.SelectionMode == 'Daily') {
                if (dateSelection.SelectedMonth == 0 || dateSelection.SelectedMonth == 2 || dateSelection.SelectedMonth == 4 || dateSelection.SelectedMonth == 6 || dateSelection.SelectedMonth == 7 || dateSelection.SelectedMonth == 9 || dateSelection.SelectedMonth == 11) {

                    if (dateSelection.SelectedDay == 30) {
                        if (dateSelection.SelectedMonth == 11) {
                            dateSelection.SelectedMonth = 0;
                            dateSelection.SelectedDay = 0;
                            dateSelection.SelectedYear += 1;
                        } else {
                            dateSelection.SelectedDay = 0;
                            dateSelection.SelectedMonth += 1;
                        }
                    } else {
                        dateSelection.SelectedDay += 1;
                    }
                } else if (dateSelection.SelectedMonth == 1) { //february add leap year here
                    if (dateSelection.SelectedYear % 4 == 0) {
                        if (dateSelection.SelectedDay >= 28) { //buggy if jumping from month with > 28  so we need this check
                            dateSelection.SelectedMonth += 1;
                            dateSelection.SelectedDay = 0;
                        } else {
                            dateSelection.SelectedDay += 1;
                        }

                    } else {

                        if (dateSelection.SelectedDay >= 27) { //buggy if jumping from month with > 28  so we need this check
                            dateSelection.SelectedMonth += 1;
                            dateSelection.SelectedDay = 0;
                        } else {
                            dateSelection.SelectedDay += 1;
                        }
                    }
                } else {
                    //the same logic is repeated down in the show function why? it may show date before reaching here 
                    if (dateSelection.SelectedDay >= 29) {
                        dateSelection.SelectedDay = 0;
                        dateSelection.SelectedMonth += 1;

                    } else {
                        dateSelection.SelectedDay += 1;
                    }
                }
            }

            dateSelection.show();
            setDates(dateSelection);
        },
        init: function () {
            dateSelection.LeftNavControl.bind('click', dateSelection.LeftClick);
            dateSelection.RightNavControl.bind('click', dateSelection.RightClick);
        },

        show: function () {
            if (dateSelection.SelectionMode == 'Year') {
                if (localeLang == "am") {
                    var eDateYearY = GetEthiopianDate(dateSelection.SelectedYear, dateSelection.SelectedMonth, dateSelection.SelectedDay);
                    dateSelection.LabelControl.val('መስክረም - ጳግሜ ' + eDateYearY._year);
                } else {
                    dateSelection.LabelControl.val('Jan - Dec ' + dateSelection.SelectedYear);
                }
            }
            if (dateSelection.SelectionMode == 'Month') {
                if (localeLang == "am") {

                    //show the 13th month in eth in month nav 
                    var eDateYearM = GetEthiopianDate(dateSelection.SelectedYear, dateSelection.SelectedMonth, dateSelection.SelectedDay);
                    //                            if (dateSelection.SelectedMonth == 9) {
                    //                                dateSelection.LabelControl.val(dateSelection.MonthNamesAm[12] + ' ' + eDateYearM._year);
                    //                            } else {
                    dateSelection.LabelControl.val(dateSelection.MonthNamesAm[eDateYearM._month - 1] + ' ' + eDateYearM._year);
                    //                            }
                } else {
                    dateSelection.LabelControl.val(dateSelection.MonthNames[dateSelection.SelectedMonth] + ' ' + dateSelection.SelectedYear);
                }
            }
            if (dateSelection.SelectionMode == 'Quarter') {
                if (localeLang == "am") {
                    var eDateYearQ = GetEthiopianDate(dateSelection.SelectedYear, dateSelection.SelectedMonth, dateSelection.SelectedDay);
                    dateSelection.LabelControl.val(dateSelection.QuartersAm[dateSelection.SelectedQuarter] + ' ' + eDateYearQ._year);
                } else {
                    dateSelection.LabelControl.val(dateSelection.Quarters[dateSelection.SelectedQuarter] + ' ' + dateSelection.SelectedYear);
                }
            }
            if (dateSelection.SelectionMode == 'Daily') {
                if (localeLang == "am") {
                    var eDateYearD = GetEthiopianDate(dateSelection.SelectedYear, dateSelection.SelectedMonth, dateSelection.SelectedDay);
                    //dateSelection.LabelControl.val(dateSelection.QuartersAm[dateSelection.SelectedQuarter] + ' ' + eDateYearQ._year);
                    dateSelection.LabelControl.val(dateSelection.Days[eDateYearD._day - 1] + ' ' + dateSelection.MonthNamesAm[eDateYearD._month - 1] + ' ' + eDateYearD._year);
                } else {
                    //TODO move the lines below to logic part check if this date is allowed in this month for gregorian dates this Works if today is a month with 30 dates and today is 30
                    if (dateSelection.SelectedMonth == 3 || dateSelection.SelectedMonth == 5 || dateSelection.SelectedMonth == 8 || dateSelection.SelectedMonth == 10) {
                        if (dateSelection.SelectedDay >= 29) {
                            //display the last date from the selected month or the first date from the next day
                            dateSelection.SelectedDay = 29; //dateSelection.SelectedDay = 0; 
                            // dateSelection.SelectedMonth -= 1; //dateSelection.SelectedMonth += 1;
                        }
                    } //this a gadamn repitition for testing
                    if ((dateSelection.SelectedMonth == 1) && (dateSelection.SelectedDay >= 27)) {
                        if (dateSelection.SelectedYear % 4 == 0) {
                            dateSelection.SelectedDay = 27;
                        } else {
                            dateSelection.SelectedDay = 26;
                        }
                    }
                    dateSelection.LabelControl.val(dateSelection.Days[dateSelection.SelectedDay] + ' ' + dateSelection.MonthNames[dateSelection.SelectedMonth] + ' ' + dateSelection.SelectedYear); //dateSelection.SelectedYear);
                }
            }

            // alert(dateSelection.getStartDate());
            // alert(dateSelection.getEndDate());
        },
        getStartDate: function () {

            if (dateSelection.SelectionMode == 'Year') {
                dateSelection.StartDate.setFullYear(dateSelection.SelectedYear);
                dateSelection.StartDate.setMonth(0, 1);
                dateSelection.StartDate.setDate(1);
            }
            if (dateSelection.SelectionMode == 'Month') {
                dateSelection.StartDate.setFullYear(dateSelection.SelectedYear);
                dateSelection.StartDate.setMonth(dateSelection.SelectedMonth, 1);
                dateSelection.StartDate.setDate(1);
            }
            if (dateSelection.SelectionMode == 'Quarter') {
                dateSelection.StartDate.setFullYear(dateSelection.SelectedYear);
                dateSelection.StartDate.setDate(1);
                dateSelection.StartDate.setMonth(dateSelection.SelectedQuarter * 3);
            }
            if (dateSelection.SelectionMode == 'Daily') {
                dateSelection.StartDate.setFullYear(dateSelection.SelectedYear);
                dateSelection.StartDate.setMonth(dateSelection.SelectedMonth, 1);
                dateSelection.StartDate.setDate(dateSelection.SelectedDay);
            }
            return dateSelection.StartDate;
        },
        getEndDate: function () {
            if (dateSelection.SelectionMode == 'Year') {
                dateSelection.EndDate.setFullYear(dateSelection.SelectedYear);
                dateSelection.EndDate.setMonth(11);
                dateSelection.EndDate.setDate(dateSelection.EndDate.getDaysInMonth());
            }
            if (dateSelection.SelectionMode == 'Month') {
                dateSelection.EndDate.setFullYear(dateSelection.SelectedYear);
                dateSelection.EndDate.setMonth(dateSelection.SelectedMonth, 1);
                dateSelection.EndDate.setDate(dateSelection.EndDate.getDaysInMonth());
            }
            if (dateSelection.SelectionMode == 'Quarter') {
                dateSelection.EndDate.setFullYear(dateSelection.SelectedYear);
                dateSelection.EndDate.setMonth(dateSelection.SelectedQuarter * 3 + 2, 1);
                dateSelection.EndDate.setDate(dateSelection.EndDate.getDaysInMonth());
            }
            if (dateSelection.SelectionMode == 'Daily') {

                dateSelection.EndDate.setFullYear(dateSelection.SelectedYear);
                dateSelection.EndDate.setMonth(dateSelection.SelectedMonth, 1);
                dateSelection.EndDate.setDate(dateSelection.SelectedDay + 1); //TODO add 11:59:59 ra
            }
            return dateSelection.EndDate;
        }
    };

    dateSelection.init();
    dateSelection.show();
    setDates(dateSelection);

    $('.classDateMode li a').click(function () {
        dateSelection.SelectionMode = $(this).attr('id');
        dateSelection.show();
        $('.classDateMode li').removeClass('selectedDateMode');
        $(this).parent().addClass('selectedDateMode');
        setDates(dateSelection);

    });

    $('#Show').click(function () {
        //alert('Start Date: ' + dateSelection.getStartDate() + '\nEnd Date:' + dateSelection.getEndDate());
        //$('#FromDate').val(dateSelection.getStartDate());
        //$('#ToDate').val(dateSelection.getEndDate());
        setDates(dateSelection);
    });
});

function setDates(dateSelection) {
    //change the date's to other cal here
    if (localeLang == "am") {




        var eCalendar = $.calendars.instance('ethiopian', 'am');
        //var formatEth = $.calendars.picker.setDefaults({ dateFormat: 'dd-MMM-yyyy' });

        //display the dates in eth
        //star tdate
        var start = dateSelection.getStartDate();
        var jstartdate = eCalendar.fromJSDate(start).toJD();
        var starte = eCalendar.fromJD(jstartdate);

        //end date
        var end = dateSelection.getEndDate();
        var jenddate = eCalendar.fromJSDate(end).toJD();
        var ende = eCalendar.fromJD(jenddate);

        if (dateSelection.SelectedMonth == 8 && dateSelection.SelectedDay == 8) {
            starte._month += 1;
            ende._month += 1;
        }
        if (dateSelection.SelectionMode == 'Year') {
            starte._day = 1;
            starte._month = 1;

            ende._year = starte._year; //ende._year - 1;
            ende._month = 13;
            ende._day = eCalendar.daysInMonth(ende._year, ende._month);


        } else if (dateSelection.SelectionMode == 'Month') {
            starte._day = 1;
            ende._year = starte._year; //ende._year - 1;
            ende._month = starte._month; //ende._month - 1;
            ende._day = eCalendar.daysInMonth(ende._year, ende._month);
        }

        $('#FromDateAm').val(starte.formatDate('dd-MMM-yyyy'));
        $('#ToDateAm').val(ende.formatDate('dd-MMM-yyyy'));

        var gCalendar = $.calendars.instance();

        var eDate = eCalendar.newDate(starte._year, starte._month, starte._day);
        var cjstartdate = eDate.toJD();
        var firstDateOfyear = gCalendar.fromJD(cjstartdate);
        var test = firstDateOfyear.toJSDate();

        var sDate = eCalendar.newDate(ende._year, ende._month, ende._day);
        var cjenddate = sDate.toJD();
        var endDateOfyear = gCalendar.fromJD(cjenddate);
        var test2 = endDateOfyear.toJSDate();

        $('#FromDate').val(($.datepicker.formatDate(datePattern, test)).toString());
        $('#ToDate').val(($.datepicker.formatDate(datePattern, test2)).toString());


    } else {
        $('#FromDate').val(($.datepicker.formatDate(datePattern, dateSelection.getStartDate())).toString());
        $('#ToDate').val(($.datepicker.formatDate(datePattern, dateSelection.getEndDate())).toString());
    }
}

Date.prototype.getDaysInMonth =
            function (utc) {
                var m = utc ? this.getUTCMonth() : this.getMonth();
                // If feb.
                if (m == 1)
                    return this.getYear() % 4 == 0 ? 29 : 28;
                // If Apr, Jun, Sep or Nov return 30; otherwise 31
                return (m == 3 || m == 5 || m == 8 || m == 10) ? 30 : 31;
            };

$('.classDateMode li a').click(function () {
    SelectionMode = $(this).attr('id');
    $('.classDateMode li').removeClass('selectedDateMode');
    $(this).parent().addClass('selectedDateMode');

});