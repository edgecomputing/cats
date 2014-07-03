function ShowLegend(colorOptions, div,data) {
    colorOptions = extendColorOption(colorOptions);
    var htm = "<div class='title'>Legend</div>";
    for (var i = 0; i < colorOptions.sample; i++) {
        var color = getRGBValue(colorOptions.h, colorOptions.s, colorOptions.b1, colorOptions.b2, colorOptions.sample-1, i / colorOptions.sample);
        var style = "background:" + color + ";";
        var txt = "From to ";
        var val = data.maxValue;// data.minValue + (data.maxValue - data.minValue) * i / (colorOptions.sample - i);
        val = Math.round(val);
        var valFrom = 0;
        var valTo = i;
        if (i > 0) {
            valFrom = i - 1;
        }
        
        htm += "<div class='item'><i class='pallet' style='" + style + "'>&nbsp;</i> From " + val + "</div>";
    }
    $("#" + div).html(htm);
    
    
}
