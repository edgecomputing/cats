/// <reference path="jquery-1.9.1.js" />
/// <reference path="hello-angular.js" />
/// <reference path="angular.js" />

var $$scope;
var timer;
// Create app Module 
function onsaveAllocation() {
    $$scope.saveAllocation();
}
var app = angular.module("dragDrop", ['ngResource']);

// Declaring a Service
app.factory("dragDropService", function ($resource)
{

    return {
        
        getRequisitions: $resource(Url + "?regionId=" + regionId)
        
    };
    
   
    
    


});

app.factory("savefactory", function ($http) {
   
    return {
        save: function (hubAllocated) {
          
            $http.post(UrlPOST, { allocation: hubAllocated }).success(function (responseData) {
                var msg = '<div class="cats_success">Success: Hub allocation saved successfully.</div>';
                $(".message-window").html(msg).delay(800).hide().fadeIn(); // Don't change this line. 
                clearTimeout(timer); // Don't change this line.
                timer = setTimeout(function () { // Don't change this line.
                    $(".message-window").fadeOut("normal", function () { $(this).html(''); }); // Don't change this line.
                }, 5000); // Here is the millisecond duration in which the message will be displayed. Can be changed.
               
            }).error(function (responseData) {
                var msg = '<div class="cats_error">Error: Hub allocation couldn\'t be saved.</div>';
                $(".message-window").html(msg).delay(800).hide().fadeIn(); // Don't change this line. 
                clearTimeout(timer); // Don't change this line.
                timer = setTimeout(function () { // Don't change this line.
                    $(".message-window").fadeOut("normal", function () { $(this).html(''); }); // Don't change this line.
                }, 5000); // Here is the millisecond duration in which the message will be displayed. Can be changed.
               
            });
        }
    };
   
});

app.controller("DragDroController", function ($scope, dragDropService, savefactory)
{

    $scope.handleDrop = function (index) {
       
        $scope.allocated[0].HubId = index; 
    };
    
    $scope.saveAllocation = function () {

        savefactory.save($scope.allocated);
    };

    
    $scope.Requisitions = dragDropService.getRequisitions.query({}, isArray = true);
    $scope.allocated = [];

    
    $scope.newRequisitions = {
        
        0: "No requisitions in " + RegionName +" region ",
        other: "{} requisitions  in " +  RegionName
    };

    $$scope = $scope;

});


app.directive('draggable', function () {
    
    
    return function (scope, element) {
       
        // this gives us the native JS object
        var el = element[0];
        
        el.draggable = true;

        el.addEventListener(
            'dragstart',
            function (e) {
                e.dataTransfer.effectAllowed = 'move';
                e.dataTransfer.setData('Text', this.id);
                this.classList.add('drag');
                return false;
            },
            false
        );

        el.addEventListener(
            'dragend',
            function (e) {
               
                this.classList.remove('drag');
                return false;
            },
            false
        );
    }
});


app.directive('droppable', function () {
    return {
        scope: {
            drop: '&',
            allocated: "="
        },
        link: function(scope, element) {


            // again we need the native object
            var el = element[0];

            el.addEventListener(
                'dragover',
                function(e) {
                    e.dataTransfer.dropEffect = 'move';
                    // allows us to drop
                    if (e.preventDefault) e.preventDefault();
                    this.classList.add('over');
                    return false;
                },
                false
            );
            el.addEventListener(
                'dragenter',
                function(e) {
                    this.classList.add('over');
                    return false;
                },
                false
            );

            el.addEventListener(
                'dragleave',
                function(e) {
                    this.classList.remove('over');
                    return false;
                },
                false
            );


            el.addEventListener(
                'drop',
                function(e) {
                    // Stops some browsers from redirecting.
                    if (e.stopPropagation) e.stopPropagation();

                    this.classList.remove('over');

                    var item = document.getElementById(e.dataTransfer.getData('Text'));
                    this.appendChild(item);


                    for (var i = 0; i < $$scope.allocated.length; i++) {
                        if ($$scope.allocated[i].reqId == item.id) {
                            $$scope.allocated.splice(i, 1);
                        }
                    }
                    $$scope.allocated.splice(0, 0, { reqId: item.id, HubId: 'index' });

                    scope.$apply('drop()');

                    return false;
                },
                false
            );

        }
    };
});



