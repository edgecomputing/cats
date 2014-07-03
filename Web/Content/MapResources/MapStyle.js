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
                fillOpacity: 1
                , strokeOpacity: 1
               // , label: function (feature) { return get_attribute_value(feature.attributes, "name", ""); }
                , label: function (feature) {
                    var keyVal = get_attribute_value(feature.attributes, key, "");
                    var row = dataTable["row" + keyVal];
                    if (row) {
                        var att = dataTable["row" + keyVal][indicator + "normalized"];
                        att = Math.round(att * (colorOptions.sample-1));
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
    var _colorOptions = { h: 70, s: 38, b1: 230, b2: 100, sample: 5, noValColor: "rgb(240,240,240)" };
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
    console.log("getRGBValue",name,v,b);
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
function HSVtoRGB(h, s, v) {
    var r, g, b, i, f, p, q, t;
    if (h && s === undefined && v === undefined) {
        s = h.s, v = h.v, h = h.h;
    }
    i = Math.floor(h * 6);
    f = h * 6 - i;
    p = v * (1 - s);
    q = v * (1 - f * s);
    t = v * (1 - (1 - f) * s);
    switch (i % 6) {
        case 0: r = v, g = t, b = p; break;
        case 1: r = q, g = v, b = p; break;
        case 2: r = p, g = v, b = t; break;
        case 3: r = p, g = q, b = v; break;
        case 4: r = t, g = p, b = v; break;
        case 5: r = v, g = p, b = q; break;
    }
    return {
        r: Math.floor(r * 255),
        g: Math.floor(g * 255),
        b: Math.floor(b * 255)
    };
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