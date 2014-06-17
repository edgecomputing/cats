function CreateMap(div, _options) {
    var options = {};
    options=$.extend(options, _options);
    var map, draw, modify, snap, point, line, poly;

    map = new OpenLayers.Map(div);
    map.addControl(new OpenLayers.Control.MousePosition());
    if (options) {
        if (options.layers) {
            var isBaseLayer = true;
            for (var i in options.layers) {

                var layerData = options.layers[i];
                //layerData=$.extend(layerData, options.layers[i]);
               // console.log("Layer : ", layerData);
                var styleMap = layerData.styleMap;
                if (!styleMap) {
                    styleMap = createStyle(layerData.style ? layerData.style : {});
                }

                var layer = new OpenLayers.Layer.Vector(layerData.name, {
                    strategies: [new OpenLayers.Strategy.Fixed()],
                    protocol: new OpenLayers.Protocol.HTTP({ url: layerData.url, format: new OpenLayers.Format.GeoJSON() }),
                    isBaseLayer: isBaseLayer,
                    styleMap: styleMap
                });
                map.addLayer(layer);
                if (!isBaseLayer) {
                    addSelectControl(map, layer)
                }
                isBaseLayer = false;
            }
        }

    }
    // map.setCenter(new OpenLayers.LonLat(39, 9), 5);

    

    map.zoomToMaxExtent();
    return;
}
function addSelectControl(map, layer) {
    selectControl = new OpenLayers.Control.SelectFeature(layer);
    map.addControl(selectControl);
    selectControl.activate();
    layer.events.on({
        'featureselected': function () { },
        'featureunselected': function () { }
    });
}
function normalizeIndicator(data, fld) {
    var hasRows = 0;
    var maxVal = -999999;
    for (var i in data) {
        var indVal = data[i][fld];
        maxVal = Math.max(maxVal, indVal);
        hasRows = 1;
    }
    for (var i in data) {
        var indVal = data[i][fld];
        data[i][fld + "normalized"] = indVal / maxVal;
    }
    return data;
}
function addLayers(map, layers) {

}
function createShadedMap(div, _options) {
    var options = { dataTable: [], key: "", indicator: "", url: "" };
    CreateMap("map2", { layers: layers2 });
}
