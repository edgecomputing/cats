function ShowLegend(colorOptions, div, data) {
    console.log("showLegend", data);
    colorOptions = extendColorOption(colorOptions);
    var lastVal = "";
    var htm = "<div class='title'>Legend</div>";
    for (var i = 0; i < colorOptions.sample; i++) {
        var color = getRGBValue(colorOptions.h, colorOptions.s, colorOptions.b1, colorOptions.b2, colorOptions.sample-1, i / colorOptions.sample);
        var style = "background:" + color + ";";
        var txt = "From to ";
        var val = data.minVal + (data.maxVal - data.minVal) * i / (colorOptions.sample - i);
        val = Math.round(val);
        
        txt = "Less Than " + val;
        if (lastVal) {
            txt="From " + lastVal + " to " + val;
        }
        lastVal = val;
        htm += "<div class='item'><i class='pallet' style='" + style + "'>&nbsp;</i> " + txt + "</div>";
    }
    htm += "<div class='item'><i class='pallet' style='background:" + colorOptions.noValColor  + ";'>&nbsp;</i> No Data </div>";
    $("#" + div).html(htm);
    
    
}
