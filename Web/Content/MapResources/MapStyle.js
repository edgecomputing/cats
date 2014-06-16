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
var styles = new OpenLayers.StyleMap({
    "default": new OpenLayers.Style(null, {
        context: mapContex,
        rules: [
            new OpenLayers.Rule({
                symbolizer: {
                    "Point": {
                        pointRadius: 5,
                        graphicName: "square",
                        fillColor: "white",
                        fillOpacity: 0.25,
                        strokeWidth: 1,
                        strokeOpacity: 1,
                        strokeColor: "#3333aa",
                        label: "${name}",
                        labelAlign: "cb",
                        labelYOffset: "7"
                    },
                    "Line": {

                        strokeWidth: 3,
                        strokeOpacity: 1,
                        strokeColor: "#6666aa"
                    },
                    "Polygon": {
                        strokeWidth: 1,
                        strokeOpacity: 1,
                        fillColor: "#9999aa",
                        strokeColor: "#6666aa"
                    }
                }
            })
        ]
    }),
    "select": new OpenLayers.Style(null, {
        rules: [
            new OpenLayers.Rule({
                symbolizer: {
                    "Point": {
                        label: "",
                        pointRadius: 5,
                        graphicName: "square",
                        fillColor: "white",
                        fillOpacity: 0.25,
                        strokeWidth: 2,
                        strokeOpacity: 1,
                        strokeColor: "#0000ff"
                    },
                    "Line": {
                        strokeWidth: 3,
                        strokeOpacity: 1,
                        strokeColor: "#0000ff"
                    },
                    "Polygon": {
                        strokeWidth: 2,
                        strokeOpacity: 1,
                        fillColor: "#0000ff",
                        strokeColor: "#0000ff"
                    }
                }
            })
        ]
    }),
    "temporary": new OpenLayers.Style(null, {
        rules: [
            new OpenLayers.Rule({
                symbolizer: {
                    "Point": {
                        graphicName: "square",
                        label: "",
                        pointRadius: 5,
                        fillColor: "red",
                        fillOpacity: 0.25,
                        strokeWidth: 2,
                        strokeColor: "#0000ff"
                    },
                    "Line": {
                        strokeWidth: 3,
                        strokeOpacity: 1,
                        strokeColor: "#0000ff"
                    },
                    "Polygon": {
                        strokeWidth: 2,
                        strokeOpacity: 1,
                        strokeColor: "#0000ff",
                        fillColor: "#0000ff"
                    }
                }
            })
        ]
    })
});
