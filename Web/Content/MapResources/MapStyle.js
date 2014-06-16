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
function createStyle(rules) {
    var context = {};
    for (var r in rules) {
        var symbolizer = rules[r];
        for (var g in symbolizer) {
            var geometry = symbolizer[g];
            for (var s in geometry) {
                var styleAtt = geometry[s];
                if (typeof (styleAtt) == "function") {

                    console.log("createStyle", { rule: r, geometry: g, style: s, val: styleAtt });
                    var functionName = "get_" + r + g + s;
                    context[functionName] = styleAtt;
                    geometry[s] = "${" + functionName + "}";
                }
            }
        }
    }
    console.log("createStyle-context", { context: context, rule: rules });

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
