function CreateMap(div) {
        var map

			map = new OpenLayers.Map(div);
            map.addControl(new OpenLayers.Control.MousePosition());
            
            map.setCenter(new OpenLayers.LonLat(39, 9), 6);
            
        }