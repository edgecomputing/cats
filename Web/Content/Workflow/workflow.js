function processCtrl($scope, $http) {
    $scope.saved = 1;
    $scope.processId = processId;
    $scope.serverURL = serverURL + "/";
    $scope.ProcessTemplateData = {};
    $scope.EditedState = {};
    $scope.EditedStateId = 0;
    $scope.EditedFlowIndex = 0;
    $scope.StateDictionary = {};
    $scope.newInstances = 0;

    $scope.editState = function (stateIndex) {
        $scope.closeStateEdit();
        $scope.closeFlowEdit();

        $scope.EditedStateId = stateIndex;
        var state = $scope.ProcessTemplateData.StateTemplates[stateIndex];
        var state_div = document.getElementById("states_" + state.StateTemplateID);
        var editor = document.getElementById("popover_state_editor");
        $("#popover_state_editor").show();
        editor.style.left = state_div.offsetLeft / 1 + state_div.offsetWidth / 1 + "px";
        var top = state_div.offsetTop + (state_div.offsetHeight - editor.offsetHeight) / 2;
        editor.style.top = Math.round(top)+ "px";
       
       // setTimeout(function () { $scope.EditedStateId = stateId; }, 500);
       
    };
    $scope.editflow = function (flowIndex) {
        $scope.closeStateEdit();
        $scope.closeFlowEdit();

        $scope.EditedFlowIndex = flowIndex;
        var flow = $scope.ProcessTemplateData.FlowTemplates[flowIndex];
        var flow_div = document.getElementById("flows_" + flow.FlowTemplateID);
        var editor = document.getElementById("popover_flow_editor");
        $("#popover_flow_editor").show();
        editor.style.left = flow_div.offsetLeft / 1 + flow_div.offsetWidth / 1 + "px";
        var top = flow_div.offsetTop + (flow_div.offsetHeight - editor.offsetHeight) / 2;
        editor.style.top = Math.round(top) + "px";
        setTimeout(function () {
            $("#select_InitialStateID").val(flow.InitialStateID);
            $("#select_FinalStateID").val(flow.FinalStateID);

        }, 1000);
    };
    $scope.closeStateEdit = function () {
        var popup = document.getElementById("popover_state_editor")
        if (popup.style.display == "block") {
            var state = $scope.ProcessTemplateData.StateTemplates[$scope.EditedStateId];
            $scope.SaveState(state);
            $("#popover_state_editor").hide();
        }
    };
    $scope.closeFlowEdit = function () {
        var popup = document.getElementById("popover_flow_editor")
        if (popup.style.display == "block") {
            var flow = $scope.ProcessTemplateData.FlowTemplates[$scope.EditedFlowIndex];
            $scope.SaveFlow(flow);
            $("#popover_flow_editor").hide();
        }
    };
    $scope.cancelStateEdit = function () {
        $scope.closeStateEdit();
    };
    $scope.cancelFlowEdit = function () {
        $scope.closeFlowEdit();
       
    };
    $scope.showEditorWindow = function (state_div, editor) {

    };
    $scope.SaveState = function (editedState) {
        $("#states_" + editedState.StateTemplateID).addClass("saving");

        $http.post($scope.serverURL + "EditStateJSON", { item: editedState }).success(
                    function (resp, status, headers, config) {
                        $("#states_" + editedState.StateTemplateID).removeClass("saving");
                    });
    };
    $scope.SaveFlow = function (editedFlow) {
        $("#flows_" + editedFlow.FlowTemplateID).addClass("saving");

        $http.post($scope.serverURL + "EditFlowJSON", { item: editedFlow }).success(
                    function (resp, status, headers, config) {
                        $("#flows_" + editedFlow.FlowTemplateID).removeClass("saving");
                    });
    };
    var GetProcessTemplateDataSuccessCB = function (resp, status, headers, config) {
        $$scope = $scope;
        $scope.ProcessTemplateData = resp;

        var GraphicsDataStr = $scope.ProcessTemplateData.GraphicsData;
        var GraphicsData = { states: {}, flows: {} };
        if (GraphicsDataStr) {
            eval("GraphicsData=" + GraphicsDataStr);
            $scope.ProcessTemplateData.DiagramData = GraphicsData;
        }
        $scope.initData();
    };
    $scope.toObj=function(JsonString)
    {
        var tempObj = "";
        eval("tempObj=" + JsonString);
        return tempObj;
    }
    $scope.AddState = function () {
        var newState = { "Name": "New State", "AllowedAccessLevel": 3, "StateType": 1, "StateNo": 0, "ParentProcessTemplateID": $scope.ProcessTemplateData.ProcessTemplateID};

        $http.post($scope.serverURL + "AddState", { item: newState }).success(
            function (resp, status, headers, config)
            {
               // var responseobj = $scope.toObj(resp)
                newState.StateTemplateID = resp.StateTemplateID;
                newState.pos = { left: 700, top: 100 };
                $scope.ProcessTemplateData.StateTemplates.push(newState);
                var editindex = $scope.ProcessTemplateData.StateTemplates.length - 1;
                setTimeout(function () {
                    $scope.initalizeDrag();
                   // alert(editindex);
                  //  $scope.editState(editindex);
                }, 1000);
            });

    };

    $scope.AddFlow = function () {

        $scope.newInstances++;
        var newFlow = { "Name": "Flow", "InitialStateID": 0, "FinalStateID": 0, "ParentProcessTemplateID": $scope.ProcessTemplateData.ProcessTemplateID };

        $http.post($scope.serverURL + "AddFlow", { item: newFlow }).success(
           function (resp, status, headers, config) {
               newFlow.FlowTemplateID = resp.FlowTemplateID;
               newFlow.pos = { left: 700, top: 300 };
               $scope.ProcessTemplateData.FlowTemplates.push(newFlow);
               setTimeout(function () {
                   $scope.initalizeDrag();
                  // $scope.editflow($scope.ProcessTemplateData.FlowTemplates.length - 1);
               }, 1000);
           });
    };
    $scope.onFlowChange = function () {
        var flow = $scope.ProcessTemplateData.FlowTemplates[$scope.EditedFlowIndex];
        $scope.makeFlowRelationship(flow);
        DrawAllFlows($scope.ProcessTemplateData.FlowTemplates);
    };
    $scope.makeFlowRelationship = function (flow) {
        flow.InitialState = $scope.getStateByID(flow.InitialStateID);
        flow.FinalState = $scope.getStateByID(flow.FinalStateID);
    }
    $scope.initData = function () {
        
        var GraphicsData = $scope.ProcessTemplateData.DiagramData

        for (var i in $scope.ProcessTemplateData.StateTemplates) {
            var state = $scope.ProcessTemplateData.StateTemplates[i];
            var pos = { left: 100, top: 50 * i };
            if (GraphicsData && GraphicsData.states["state_" + state.StateTemplateID]) {
                var pos = GraphicsData.states["state_" + state.StateTemplateID];
            }
            $scope.ProcessTemplateData.StateTemplates[i].pos = pos;

        }
        var lastflowId = -1;
        for (var i in $scope.ProcessTemplateData.FlowTemplates) {
            var flow = $scope.ProcessTemplateData.FlowTemplates[i];
          //  flow.InitialStateID = "" + flow.InitialStateID;
          //  flow.FinalStateID = "" + flow.FinalStateID;

            var pos = { left: 400, top: 50 * i +25 };
            if (GraphicsData && GraphicsData.flows["flow_" + flow.FlowTemplateID]) {
                var pos = GraphicsData.flows["flow_" + flow.FlowTemplateID];
            }
            $scope.ProcessTemplateData.FlowTemplates[i].pos = pos;
            $scope.makeFlowRelationship(flow);
            lastflowId = flow.FlowTemplateID;
        }
       /* setTimeout(
            function () {
                if (lastflowId == -1) { return; }
                if (document.getElementById("flows_" + lastflowId)) {
                    onRedraw();
                }
            }, 1000);*/
      //  setTimeout(onRedraw, 1000);
//        onRedraw();
        $scope.initializeUI();
    };
    $scope.getStateByID = function (id) {
        for (var i in $scope.ProcessTemplateData.StateTemplates) {
            var state = $scope.ProcessTemplateData.StateTemplates[i];
            if (state.StateTemplateID == id) {
                return state;
            }
        }
        return {};
    }
    $scope.initializeUI = function () {
        for (var i in $scope.ProcessTemplateData.StateTemplates) 
        {
            var state = $scope.ProcessTemplateData.StateTemplates[i];
            var state_div = (document.getElementById("states_" + state.StateTemplateID));
            if(!state_div)
            {
                setTimeout($scope.initializeUI, 1000);
                return;
            }
        }
        for (var i in $scope.ProcessTemplateData.FlowTemplates) 
        {
            var flow = $scope.ProcessTemplateData.FlowTemplates[i];
            var flow_div = (document.getElementById("flows_" + flow.FlowTemplateID));
            if(!flow_div)
            {
                setTimeout($scope.initializeUI, 1000);
                return;
            }
        }
        $(".state").draggable({ stop: function () { onRedraw(); } })
            //.dblclick(function () { show_state_editor(this) });
        $(".flow").draggable({ stop: function () { onRedraw(); } })
            //.dblclick(function () { show_state_editor(this) });
        onRedraw();
        /*
        $(".btn_edit_state_cancel").click(function () {
            $("#popover_state_editor").hide();
        });*/
    };
    $scope.initalizeDrag = function () {
        $(".state").draggable({ stop: function () { onRedraw(); } })
        //.dblclick(function () { show_state_editor(this) });
        $(".flow").draggable({ stop: function () { onRedraw(); } })
    }
    $scope.loadProcessTemplateData = function (showmodal) {
        var param = { id: $scope.processId};
        $http.post($scope.serverURL + "DetailJson", param).success(GetProcessTemplateDataSuccessCB);
        if (showmodal) {
           // $('#modalContent').html("...");
           // $('#myModal').modal('show');
        }
    };

    $scope.saveGraphicsData = function (showmodal) {
        var param = { processId: $scope.processId, graphicsData: $scope.GetDiagramDataJson() }

        $http.post($scope.serverURL + "saveGraphics", param).success(function () { alert("saved"); });
        if (showmodal) {
            // $('#modalContent').html("...");
            // $('#myModal').modal('show');
        }
    };
    $scope.GetDiagramDataJson=function()
    {
        var statePosJson = "";
        var comma = "";
        for (var i in $scope.ProcessTemplateData.StateTemplates) {
            var state = $scope.ProcessTemplateData.StateTemplates[i];
            var state_div = (document.getElementById("states_" + state.StateTemplateID));
            statePosJson += comma + "state_" + state.StateTemplateID + ":{left:" + state_div.offsetLeft + ",top:" + state_div.offsetTop + "}";
            comma=","
        }
        var flowPosJson = "";
        comma = "";
        for (var i in $scope.ProcessTemplateData.FlowTemplates) {
            var flow = $scope.ProcessTemplateData.FlowTemplates[i];
            var flow_div = (document.getElementById("flows_" + flow.FlowTemplateID));
            flowPosJson += comma + "flow_" + flow.FlowTemplateID + ":{left:" + flow_div.offsetLeft + ",top:" + flow_div.offsetTop + "}";
            comma = ","
        }
        var posJson = "{states:{" + statePosJson + "},flows:{" + flowPosJson + "}}";
        return posJson;
    }
    $scope.loadProcessTemplateData();
}function onSaveProcesss() {
    $$scope.saveGraphicsData();
}function onRedraw() {
    DrawAllFlows($$scope.ProcessTemplateData.FlowTemplates);}function onAddState() {
    $$scope.AddState();}function DrawAllFlows(flows) {
    var canvas = document.querySelector("#drawingCanvas");
    var ctx = canvas.getContext("2d");
    ctx.strokeStyle = "rgb(0,120,255)";
    ctx.lineWidth = 0.5;
    ctx.clearRect(0, 0, 1000, 1000);

    for (var i in flows) {
        var flow = flows[i];
        DrawFlow(ctx, flow);
    }}function DrawFlow(ctx, flow) {

    var is = flow.InitialState;
    var fs = flow.FinalState;

    var init_state_div = document.getElementById("states_" + flow.InitialState.StateTemplateID);
    var final_state_div = document.getElementById("states_" + flow.FinalState.StateTemplateID);
    var flow_div = document.getElementById("flows_" + flow.FlowTemplateID);

    var ispos = { left: init_state_div.offsetLeft, top: init_state_div.offsetTop, width: init_state_div.offsetWidth, height: init_state_div.offsetHeight };
    var flpos = { left: flow_div.offsetLeft, top: flow_div.offsetTop, width: flow_div.offsetWidth, height: flow_div.offsetHeight };
    var fspos = { left: final_state_div.offsetLeft, top: final_state_div.offsetTop, width: final_state_div.offsetWidth, height: final_state_div.offsetHeight };
   



        ctx.beginPath();
        
        ctx.moveTo(ispos.left + ispos.width / 2, ispos.top + ispos.height / 2);

        ctx.strokeStyle = "rgb(0,120,255)";
        ctx.lineTo(flpos.left + flpos.width / 2, ispos.top + ispos.height / 2);
        ctx.lineTo(flpos.left + flpos.width / 2, flpos.top + flpos.height / 2);

        ctx.lineTo(fspos.left + fspos.width / 2, flpos.top + flpos.height / 2);

        ctx.stroke();
        ctx.beginPath();
        ctx.strokeStyle = "rgb(255,0,0)";
        ctx.moveTo(fspos.left + fspos.width / 2, flpos.top + flpos.height / 2);
        ctx.lineTo(fspos.left + fspos.width / 2, fspos.top + fspos.height / 2);

        //ctx.closePath();
        ctx.stroke();
  
}var lastTarget;
function show_state_editor(elem) {
    var editor = $("#popover_state_editor");
    //$(elem).popover('show')
    $target = $(elem);
    $$scope.editState($target.data("index"));
    return;
    var htm = "<div class='small_form' id='frm_current'>" + $("#frm_state_template").clone().html() + "</div>";
    
    if (lastTarget) {
        if ($target.attr("id") == lastTarget.attr("id")) {
            return;
        }
        else {
            lastTarget.popover('destroy');
        }
    }

    $target.popover({ html: true, trigger: "manual", title: "Edit Title", content: htm,placement:'bottom' });
    $target.popover('show');
    lastTarget = $target;
    $(".btn_popup_cancel").click(function () { lastTarget.popover('destroy'); });
    return;
}

function show_popup(popup, left, top) {
    popup.css("left", left).css("top", top).show();
}