function CreateMap(div, options) {
    var map, draw, modify, snap, point, line, poly;

    map = new OpenLayers.Map(div);
    map.addControl(new OpenLayers.Control.MousePosition());
    if (options) {
        if (options.layers) {
            var isBaseLayer = true;
            for (var i in options.layers) {

                var layerData = options.layers[i];
                console.log("Layer : ", layerData);
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
                isBaseLayer = false;
            }
        }

    }
    map.setCenter(new OpenLayers.LonLat(39, 9), 5);
    return;
            
}
