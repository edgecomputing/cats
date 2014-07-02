function get_attribute_value(obj, attrib, def) {
    if (obj[attrib]) {
        return obj[attrib];
    }
    return def;
}


var mapContex = {
    get_line_strokeColor: function (feature) { return get_attribute_value(feature.attributes, "color", "#888"); }
            	, get_fontSize: function (feature) { return 8 + road_size * (2940 / map.getScale()); }
            	, get_display: function (feature) { return get_attribute_value(feature.attributes, "mapLevel", 0) == 1 ? "display" : "none"; }
};
function getPolygonShadingStyle(key, dataTable, indicator, colorOptions) {
    var _colorOptions = { minColor: "rgb(0,200,0)", maxColor: "rgb(255,128,128)", noValColor: "rgb(200,200,200)" };
    if (!colorOptions) {
        colorOptions = {};
    }
    for (var i in _colorOptions) {
        if (typeof (colorOptions[i]) == "undefined") {
            colorOptions[i] = _colorOptions[i];
        }
    }

    normalizeIndicator(dataTable, indicator);
    return {
        "default":
        {
            "Polygon":
            {
                fillOpacity: 1
                , strokeOpacity: 1
                , label: function (feature) { return get_attribute_value(feature.attributes, "name", ""); }
                , fillColor: function (feature) {
                    var keyVal = get_attribute_value(feature.attributes, key, "");
                    var row = dataTable["row" + keyVal];
                    if (row) {
                        var att = dataTable["row" + keyVal][indicator + "normalized"];
                        return getRGBShade(colorOptions.minColor, colorOptions.maxColor, att);
                    }
                    return colorOptions.noValColor;
                }
               , strokeColor: "${get_defaultPolygonfillColor}"

            }
        }
    }
}
function createStyle(rules) {
    var context = {};
    for (var r in rules) {
        var symbolizer = rules[r];
        for (var g in symbolizer) {
            var geometry = symbolizer[g];
            for (var s in geometry) {
                var styleAtt = geometry[s];
                if (typeof (styleAtt) == "function") {

                    //console.log("createStyle", { rule: r, geometry: g, style: s, val: styleAtt });
                    var functionName = "get_" + r + g + s;
                    context[functionName] = styleAtt;
                    geometry[s] = "${" + functionName + "}";
                }
            }
        }
    }
    //console.log("createStyle-context", { context: context, rule: rules });

    var StyleMaps = {};
    for (var m in rules) {
        var symbolizer = rules[m];
        var style = new OpenLayers.Style(null, {
            context: context,
            rules: [new OpenLayers.Rule({ symbolizer: symbolizer })]
        });
        StyleMaps[m] = style;
    }
    var styles = new OpenLayers.StyleMap(StyleMaps);
    return styles;
}
function rgb(r, g, b) {
    return { r: r, g: g, b: b };
}
function middleValue(s, e, r) {
    var diff = e - s;
    return Math.round(s + diff * r);
}
function getRGBShade(colorFrom, colorTo, nv) {
    var cFrom = {};
    var cTo = {};
    eval("cFrom=" + colorFrom);
    eval("cTo=" + colorTo);
    var r = middleValue(cFrom.r, cTo.r, nv);
    var g = middleValue(cFrom.g, cTo.g, nv);
    var b = middleValue(cFrom.b, cTo.b, nv);
    var rgbstr = "rgb(" + r + "," + g + "," + b + ")";
    return rgbstr;
}
function colorValue(base, factor) {
    return Math.min(Math.round(base * factor), 255);
}
function rgbColor(base, factor) {
    var r = colorValue(base[0], factor);
    var g = colorValue(base[1], factor);
    var b = colorValue(base[2], factor);
    return "rgb(" + r + "," + g + "," + b + ")";
}

var stylePresets = {
    outline: createStyle({ "default": { "Polygon": { fill: 0, strokeWidth: 0, strokeColor: "#999" } } })
    ,solid: createStyle({ "default": { "Polygon": { fill: 0, strokeWidth: 0, strokeColor: "#999" } } })
}
function colorChanged(nam, val) {
    colorRange[nam] = val;
    drawShades();
}
function rgb(r, g, b) {
    return { r: r, g: g, b: b };
}
function middleValue(s, e, r) {
    var diff = e - s;
    return Math.round(s + diff * r);
}
function getRGBShade(colorFrom, colorTo, nv) {
    var cFrom = {};
    var cTo = {};
    eval("cFrom=" + colorFrom);
    eval("cTo=" + colorTo);
    var r = middleValue(cFrom.r, cTo.r, nv);
    var g = middleValue(cFrom.g, cTo.g, nv);
    var b = middleValue(cFrom.b, cTo.b, nv);
    var rgbstr = "rgb(" + r + "," + g + "," + b + ")";
    return rgbstr;
}
function drawShades() {
    for (var i in colorRange) {
        document.getElementById(i).style.backgroundColor = colorRange[i];
    }


    var htm = "";
    for (var i = 0; i < 10; i++) {
        var rgbstr = getRGBShade(colorRange.colorFrom, colorRange.colorTo, i / 10)
        htm += "<div class='color-pallet' style='background:" + rgbstr + ";'> " + rgbstr + "</div>";

    }
    document.getElementById("shadeDisp").innerHTML = htm;
}