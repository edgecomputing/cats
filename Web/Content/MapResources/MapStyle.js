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
    colorOptions = extendColorOption(colorOptions);

    
    return {
        "default":
        {
            "Polygon":
            {
                fillOpacity: 0.8
                , strokeOpacity: 1
                , label: function (feature) { return get_attribute_value(feature.attributes, "name", "") + "\n" + get_attribute_value(feature.attributes, "code", ""); }
                , label2: function (feature) {
                    return "";
                    var keyVal = get_attribute_value(feature.attributes, key, "");
                    var row = dataTable["row" + keyVal];
                    if (row) {
                        var att = dataTable["row" + keyVal][indicator];
                      //  att = Math.round(att * (colorOptions.sample-1));
                        return get_attribute_value(feature.attributes, "name", "") + " " + att;
                    }
                    return get_attribute_value(feature.attributes, "name", "") + " - ";
                }
                , fillColor: function (feature) {
                    var keyVal = get_attribute_value(feature.attributes, key, "");
                    var name=get_attribute_value(feature.attributes, "name", "");
                    var row = dataTable["row" + keyVal];
                    if (row) {
                        var att = dataTable["row" + keyVal][indicator + "normalized"];
                       // att = Math.round(att * 5) + "";
                       // att = att / 5;
                        return getRGBValue(colorOptions.h, colorOptions.s, colorOptions.b1, colorOptions.b2, colorOptions.sample-1, att, name);
                        return getRGBShade(colorOptions.minColor, colorOptions.maxColor, att);
                    }
                    return colorOptions.noValColor;
                }
               , strokeColor: "${get_defaultPolygonfillColor}"

            }
        }
    }
}
function extendColorOption(colorOptions) {
    var _colorOptions = { h: 220, s: 200, b1: 200, b2: 100, sample: 5, noValColor: "rgb(64,64,64)" };
    if (!colorOptions) {
        colorOptions = {};
    }
    for (var i in _colorOptions) {
        if (typeof (colorOptions[i]) == "undefined") {
            colorOptions[i] = _colorOptions[i];
        }
    }
    return colorOptions;
}
function getRGBValue(h, s, b1, b2, segments, v, name) {


    v = Math.round(v * (segments)) + "";
    v = v / segments;
    var bDiff = (b2 - b1);
    var b = b1 + bDiff * v;
    var color = hslToRgb(h / 240, s / 240, b / 240);
   // console.log("getRGBValue",name,v,b);
    return "rgb(" + color.r + ", " + color.g + ", " + color.b + ")";
}
function hslToRgb(h, s, l) {
    var r, g, b;

    if (s == 0) {
        r = g = b = l; // achromatic
    } else {
        function hue2rgb(p, q, t) {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (t < 1 / 6) return p + (q - p) * 6 * t;
            if (t < 1 / 2) return q;
            if (t < 2 / 3) return p + (q - p) * (2 / 3 - t) * 6;
            return p;
        }

        var q = l < 0.5 ? l * (1 + s) : l + s - l * s;
        var p = 2 * l - q;
        r = hue2rgb(p, q, h + 1 / 3);
        g = hue2rgb(p, q, h);
        b = hue2rgb(p, q, h - 1 / 3);
    }

    return { r: Math.round(r * 255), g: Math.round(g * 255), b: Math.round(b * 255) };
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