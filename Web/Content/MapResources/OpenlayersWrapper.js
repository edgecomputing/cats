function CreateMap(div, _options) {
    var options = {};
    options=$.extend(options, _options);
    var map, draw, modify, snap, point, line, poly;

    map = new OpenLayers.Map(div);
    map.addControl(new OpenLayers.Control.MousePosition());

   var base= new OpenLayers.Layer.Vector("Base", {
        strategies: [new OpenLayers.Strategy.Fixed()],
        protocol: new OpenLayers.Protocol.HTTP({ url: "/Content/MapResources/MapData/ethiopiaJson.js", format: new OpenLayers.Format.GeoJSON() }),
        isBaseLayer: true
   });
   map.addLayer(base);
   isBaseLayer = false;
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
                /*
                var layer = new OpenLayers.Layer.Vector(layerData.name, {
                    strategies: [new OpenLayers.Strategy.Fixed()],
                    //protocol: new OpenLayers.Protocol.HTTP({ url: layerData.url, format: new OpenLayers.Format.GeoJSON() }),
                    isBaseLayer: isBaseLayer,
                    styleMap: styleMap
                });
                */
                var layer = new OpenLayers.Layer.Vector(layerData.name, { styleMap: styleMap });
                map.addLayer(layer);
                if (!isBaseLayer) {
                    addSelectControl(map, layer)
                }
                isBaseLayer = false;
               /* $.get(layerData.url, function (data) {
                    console.log("Feature downloaded",data);
                    deserialize(data,map,layer);
                });
                */
                var jqxhr = $.get(layerData.url, {})
  .done(function (data) {
      console.log("second success");
      deserialize(data, map, layer);
  })
  .fail(function (data) {
     // console.log("error",data);
  })
  .always(function () {
      //alert("finished");
  });



            }
        }

    }
    // map.setCenter(new OpenLayers.LonLat(39, 9), 5);

    

    //map.zoomToMaxExtent();
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
function deserialize(text, map, layer) {
    var projection="EPSG:4326";// lon,lat in degrees.
    var projection = "EPSG:900913";
    //console.log("deserialize ", text);
    var in_options = { "internalProjection": map.baseLayer.projection, "externalProjection": new OpenLayers.Projection(projection) };
    var geojson= new OpenLayers.Format.GeoJSON(in_options)

    //var element = document.getElementById('text');
    //var type = document.getElementById("formatType").value;
// var features = formats['in'][type].read(text);
    var features = geojson.read(text);
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
        layer.addFeatures(features);
        map.zoomToExtent(bounds);
        var plural = (features.length > 1) ? 's' : '';
        //element.value = features.length + ' feature' + plural + ' added';
    } else {
       // element.value = 'Bad input ' + type;
    }
}