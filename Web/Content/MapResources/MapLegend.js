function ShowLegend(colorOptions, div) {
    colorOptions = extendColorOption(colorOptions);
    var htm = "<div class='title'>Legend</div>";
    for (var i = 0; i < colorOptions.sample; i++) {
        var color = getRGBValue(colorOptions.h, colorOptions.s, colorOptions.b1, colorOptions.b2, colorOptions.sample-1, i / colorOptions.sample);
        var style = "background:"+color +";";
        htm += "<div class='item'><i class='pallet' style='" + style + "'>&nbsp;</i> From " + i + "</div>";
    }
    $("#" + div).html(htm);
    
    
}
