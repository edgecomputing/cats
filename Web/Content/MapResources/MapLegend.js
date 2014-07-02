function ShowLegend(colorOptions) {
    colorOptions = extendColorOption(colorOptions);
    var htm = "<div class='title'>Legend</div>";
    for (var i = 0; i < colorOptions.sample; i++) {
        var color = getRGBValue(colorOptions.h, colorOptions.s, colorOptions.b1, colorOptions.b2, colorOptions.sample-1, i / colorOptions.sample);
        var style = "background:"+color +";";
        htm += "<div class='item'><i class='pallet' style='" + style + "'>&nbsp;</i> From " + i + "</div>";
    }
    $("#divLegend").html(htm);
    
    
}
/*
function serialize(feature) {
    var type = document.getElementById("formatType").value;
    // second argument for pretty printing (geojson only)
    var pretty = document.getElementById("prettyPrint").checked;
    var str = formats['out'][type].write(feature, pretty);
    // not a good idea in general, just for this demo
    str = str.replace(/,/g, ', ');
    document.getElementById('output').value = str;
}

function deserialize() {
    var element = document.getElementById('text');
    var type = document.getElementById("formatType").value;
    var features = formats['in'][type].read(element.value);
    var bounds;
    if (features) {
        if (features.constructor != Array) {
            features = [features];
        }
        for (var i = 0; i < features.length; ++i) {
            if (!bounds) {
                bounds = features[i].geometry.getBounds();
            } else {
                bounds.extend(features[i].geometry.getBounds());
            }

        }
        vectors.addFeatures(features);
        map.zoomToExtent(bounds);
        var plural = (features.length > 1) ? 's' : '';
        element.value = features.length + ' feature' + plural + ' added';
    } else {
        element.value = 'Bad input ' + type;
    }
}

var in_options = {
    'internalProjection': map.baseLayer.projection,
    'externalProjection': new OpenLayers.Projection(OpenLayers.Util.getElement("inproj").value)
};
var out_options = {
    'internalProjection': map.baseLayer.projection,
    'externalProjection': new OpenLayers.Projection(OpenLayers.Util.getElement("outproj").value)
};
var gmlOptions = {
    featureType: "feature",
    featureNS: "http://example.com/feature"
};
var gmlOptionsIn = OpenLayers.Util.extend(
    OpenLayers.Util.extend({}, gmlOptions),
    in_options
);
var gmlOptionsOut = OpenLayers.Util.extend(
    OpenLayers.Util.extend({}, gmlOptions),
    out_options
);*/