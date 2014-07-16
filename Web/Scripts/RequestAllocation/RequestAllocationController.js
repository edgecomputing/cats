var $$scope;
var requestMockdata = {
    request : { ReferenceNumber: "ref-00234", Region: "Afar", Program: "Relief" }
    ,allocations:[]
    ,commoditiesList:[]
};
function RequestAllocationController($scope, $http, $timeout) {
    $scope.pendingRequests = 0;
    $scope.request = {};
    $scope.allocations = [];
    $scope.commoditiesList = [];
    $scope.newAllocation = {};
    $scope.ServerUrls = ServerUrls;
    $scope.RegionalRequestID = RegionalRequestID;
    $scope.RationID = RequestRation;

    $scope.onFetchRequestSummaryDataSuccess = function (data) {
        $scope.request = data;
        $scope.fetchCommodityListData($scope.RationID);
    };

    $scope.onFetchRequestAllocationDataSuccess = function (data) {
        $scope.allocations = data;
    };

    $scope.onFetchCommodityListDataSuccess = function (data, requestedItems) {
        $scope.commoditiesList = data;
        $("#txt_requested_commodities").tokenInput(data,
                {
                    theme: "facebook", prePopulate: requestedItems, hintText: "Type in commodity", preventDuplicates: false
                    , onAdd:$scope.onCommodityAdd, onDelete: $scope.onCommodityDelete
                }
                );
    };
    $scope.onCommodityAdd = function (d) {
        $scope.onRequestStart();
        $http.get($scope.ServerUrls.Commodity_Create, { params: { commodityId: d.id, requestId: $scope.RegionalRequestID } })
                .success(function (response, status, headers, config) {
                    $scope.onRequestDone();
                })
                .error(function (data, status, headers, config) {
                    $scope.onRequestDone();
                });
    };
    $scope.onCommodityDelete = function (d) {
        $scope.onRequestStart();
        $http.get($scope.ServerUrls.Commodity_Delete, { params: { commodityId: d.id, requestId: $scope.RegionalRequestID } })
                .success(function (response, status, headers, config) {
                    $scope.onRequestDone();
                })
                .error(function (data, status, headers, config) {
                    $scope.onRequestDone();
                });

        //alert("onCommodityDelete: " + d.RequestCommodityID);
    };

    $scope.fetchRequestSummaryData = function (id) {
        $timeout(function () {
            $scope.onFetchRequestSummaryDataSuccess(requestMockdata.request);
        }, 500);
    }
    $scope.onRequestStart = function () {
        $scope.pendingRequests++;
        document.getElementById("activity_indicator").className = "pending-requests1";
    }
    $scope.onRequestDone=function()
    {
        $timeout(function () {
            $scope.pendingRequests--;
            document.getElementById("activity_indicator").className = "pending-requests" + $scope.pendingRequests;
        }, 500);
    };
    $scope.fetchRequestAllocationData = function (id) {
        $scope.onRequestStart();
        $http.get($scope.ServerUrls.Allocation_Read, { params: { id: id } })
                .success(function (response, status, headers, config)
                {
                    $scope.onRequestDone();
                    $scope.onFetchRequestAllocationDataSuccess(response.Data);
                })
                .error(function (data, status, headers, config) {
                    $scope.onRequestDone();
                });

        /*
        $timeout(function () {
            $scope.onFetchRequestAllocationDataSuccess(requestMockdata.allocations);
        }, 3000);*/
    }

    $scope.fetchCommodityListData = function (RationID) {
        //$timeout(function () {$scope.onFetchCommodityListDataSuccess(requestMockdata.commoditiesList);}, 1000);
        // return;
        console.log("fetchCommodityListData", RationID);
        $scope.onRequestStart();
        $http.get($scope.ServerUrls.getAllCommodities, { params: { RationID: RationID } })
                .success(function (response, status, headers, config)
                {
                    $scope.onRequestDone();
                    var tokenData = [];
                    var requestedItems = [];
                    for (var i in response.Data) {
                        tokenData.push({ id: response.Data[i].Value, name: response.Data[i].Text });
                    }
                    $http.get($scope.ServerUrls.getRequestedCommodities, { params: { id: $scope.RegionalRequestID } })
                    .success(function (response2, status, headers, config)
                    {
                        var msg="";
                        for (var i in response2.Data) {
                            msg += response2.Data[i].CommodityID + "\n";
                            requestedItems.push({ id: response2.Data[i].CommodityID, name: response2.Data[i].Commodity });
                        }
                        $scope.onFetchCommodityListDataSuccess(tokenData, requestedItems);
                    })
                    .error(function (data2, status, headers, config) {
                        $scope.onFetchCommodityListDataSuccess(tokenData, requestedItems);
                        alert("Error " + data);
                    });
                   
                })
                .error(function (data, status, headers, config) {
                    $scope.onRequestDone();
                });

        
    }
    $scope.onCreateNewAllocation = function () {
        $scope.newAllocation = { RegionalRequestID: $scope.RegionalRequestID, Fdpid: 0, Beneficiaries: "",FDP:"Select FPD" };
        // $('#divNewAllocation').modal("show");
        $('#newAllocationEdit').show();
        $("#cmdAddAllocation").hide();
        setTimeout(function (){$scope.onFdpListShow('new');}, 1000);
        
    }
    $scope.onCreateAllocationCancel = function () {
        hidePopover();
        $('#newAllocationEdit').hide();
        $("#cmdAddAllocation").show();
    }
    $scope.deleteRequestedFDP = function (index) {
        var reqFDP = $scope.allocations[index];
        $scope.onRequestStart();
        $http.get($scope.ServerUrls.Allocation_Delete, { params: { id: reqFDP.RegionalRequestDetailID } })
            .success(function (response, status, headers, config) {
                $scope.allocations.splice(index, 1);
                $scope.onRequestDone();
            })
            .error(function (data2, status, headers, config) {
                $scope.onRequestDone();
                alert("Error " + data2);
            });

        //alert(reqFDP.RegionalRequestDetailID);
        // Allocation_Destroy
    };
    $scope.saveAllocation = function (allocation,isNew) {
        $scope.onRequestStart();
        var url = $scope.ServerUrls.Allocation_Update;
        if (isNew) {
            url = $scope.ServerUrls.Allocation_Create;
        }
        $http.get(url, { params: allocation })
            .success(function (response, status, headers, config) {
                
                console.log("saveAllocation Success",response);
                $scope.onRequestDone();

                if (isNew) {
                    if (response.success) {

                        $('#newAllocationEdit').hide();
                        $("#cmdAddAllocation").show();
                        $scope.allocations.push(response.record);
                    }
                    //$scope.onFetchRequestAllocationDataSuccess(response.Data);
                    //console.log(" Is New", response.Data);
                    //$scope.allocations.push(allocation);
                }
                else {
                    //$scope.onFetchRequestAllocationDataSuccess(response);
                }
            })
            .error(function (data2, status, headers, config) {
                $scope.onRequestDone();
               // alert("Error " + data2);
            });

        //alert(reqFDP.RegionalRequestDetailID);
        // Allocation_Destroy
    };
    $scope.onFdpListShow=function(index )
    {
        //document.getElementById("tbl_allocation").style.marginTop=(index*-50)+"px";
        //console.log("Table Top",$("#tbl_allocation").css("top"),document.getElementById("tbl_allocation"));
        var element=document.getElementById("fdp_selector_button" + index);
        $scope.selectedAllocationIndex = index;
        var selectedAlloc = $scope.allocations[$scope.selectedAllocationIndex];


        $scope.showFDPSelector(element, selectedAlloc);
    };
    $scope.showFDPSelector = function (element, selectedAlloc) {
        selectedAlloc = selectedAlloc ? selectedAlloc : {};

        console.log("$scope.showFDPSelector",selectedAlloc);
        var selectedFDP = { RegionID: selectedAlloc.RegionId, ZoneID: selectedAlloc.ZoneId, WoredaID: selectedAlloc.WoredaId, FDPID: selectedAlloc.Fdpid };
        showFDPSelector({ target: element, callback: "onAllocationFDPChange", adminUnit: selectedFDP });
        //{RegionID:@Model.RegionID , ZoneID: @Model.ZoneID ,WoredaID: @Model.WoredaID ,FDPID: @Model.FDPID }
    };
    $scope.onAllocatoinChange=function(index)
    {
        var selectedAlloc = $scope.allocations[index];
        $scope.saveAllocation(selectedAlloc);
    }
    $scope.onFdpChange = function (fdpData) {
        var selectedAlloc = "";
        
        if ($scope.selectedAllocationIndex == "new") {
            selectedAlloc = $scope.newAllocation;
        }
        else {
            selectedAlloc = $scope.allocations[$scope.selectedAllocationIndex];
        }
      //  var selection = { FDPID: $element.data("fdpid"), fdpName: $element.data("fdpname"), WoredaID: $element.data("woredaid"), woredaName: $element.data("woredaname"), ZoneID: $element.data("zoneid"), zoneName: $element.data("zonename"), RegionID: $element.data("regionid") };

        if (selectedAlloc) {
            
            selectedAlloc.FDP = fdpData.fdpName;
            selectedAlloc.Woreda = fdpData.woredaName;
            selectedAlloc.Zone = fdpData.zoneName;
            selectedAlloc.Fdpid = fdpData.FDPID;
            selectedAlloc.WoredaId = fdpData.WoredaID;
            selectedAlloc.ZoneID = fdpData.ZoneID;
            selectedAlloc.RegionID = fdpData.RegionID;
            hidePopover();
            if ($scope.selectedAllocationIndex != "new") {
                $scope.saveAllocation(selectedAlloc);
            }
            else {
                $("#txtNewAllocationBeneficiaries").focus();
                $scope.$apply();
            }
           
        }
    };
    $scope.fetchAppData = function (id) {
        $scope.fetchRequestSummaryData(id);
        $scope.fetchRequestAllocationData(id);
        
    };
    $$scope = $scope;
    $scope.fetchAppData($scope.RegionalRequestID);
}