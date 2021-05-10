'use strict';
app.controller('DamageStockController', ['$scope', 'itemMasterService', 'WarehouseService', "$filter", "$http", "ngTableParams", '$modal', 'FileUploader', function ($scope, itemMasterService, WarehouseService, $filter, $http, ngTableParams, $modal, FileUploader) {
    console.log(" DamageStockController Controller reached");
    $scope.warehouse = [];
    WarehouseService.getwarehouse().then(function (results) {
        console.log(results.data);
        console.log("data");
        $scope.warehouse = results.data;
    }, function (error) {
    });
    $scope.wid = '';
    $scope.getWareitemMaster = function (data) {
        $scope.WarehouseFilterData = [];
        $scope.WarehouseFilterData = {};
        console.log(data);
        $scope.WarehouseFilter = [];
        $scope.itemMasters = [];
        $scope.dataselect = [];
        $scope.wid = data.WarehouseId;

        $scope.getdamagedata($scope.pageno);

        itemMasterService.getWarehouseItem($scope.wid).then(function (results) {
            console.log("gett");
           
            $scope.itemMasters = results.data;
            $scope.dataselect = results.data;
            $scope.WarehouseFilterData = _.filter($scope.itemMasters, function (obj) {
                return obj.WarehouseId == data.WarehouseId;
            })
            results.WarehouseFilterData;
           
        },
        function (error) {
            console.log("exel file is not uploaded...");
        });
    };
    $scope.examplemodel = [];
    $scope.exampledata = $scope.WarehouseFilterData;
    $scope.examplesettings = {
        displayProp: 'itemname', idProp: 'ItemId',
        //externalIdProp: 'Mobile',
        scrollableHeight: '300px',
        scrollableWidth: '450px',
        enableSearch: true,
        scrollable: true
    };

    //$http.get(serviceBase + 'api/itemMaster').then(function (results) {
        
    //    if (results.data != "null") {
    //        $scope.dataselect = results.data;
    //        console.log($scope.dataselect);
    //    }
    //});
    $scope.Search = function (data) {
        
        console.log($scope.wid);
        $scope.Warehouseid = $scope.wid;

        var ids = [];
        _.each($scope.examplemodel, function (o2) {
            console.log(o2);
            for (var i = 0; i < $scope.dataselect.length; i++) {
                if ($scope.dataselect[i].ItemId == o2.id) {
                    var Row =
                     {
                         "id": o2.id
                         //"id": o2.id, "Price": $scope.dataselect[i].UnitPrice
                     };
                    ids.push(Row);
                }
            }
        })
        var datatopost = {
            Warehouseid: $scope.Warehouseid,
            ids: ids
        }
        if (ids.length == 0)
        {
            alert('Please Parameter ');
            return false;
        }
        
        var url = serviceBase + "api/damagestock/filtre";
        $http.post(url, datatopost)
        .success(function (data) {
            if (data.length == 0) {
                alert("Not Found");
                console.log(data);
            }
            else {
                console.log("error");
                //Allbydate = data;
            }
            console.log("Data:", data);
            $scope.item = data;
          
        });
        
    }
    $scope.Addamagestock = function (data) {

        if (data.qty == 0 || data.qty == undefined)
        {
            alert('Please Enter Quantity');
        }
        else if (data.reasontotransfer == '' || data.reasontotransfer == undefined) {
            alert('Please Enter a reason for transfer');
        }
        else {
            var url = serviceBase + "api/damagestock/damage";
            var dataToPost = {
                CityId: data.Cityid,
                CityName: data.CityName,
                Warehouseid: data.WarehouseId,
                WarehouseName: data.WarehouseName,
                ItemId: data.ItemId,
                ItemNumber: data.Number,
                ItemName: data.itemname,
                DamageInventory: data.qty,
                UnitPrice: data.UnitPrice,
                ReasonToTransfer: data.reasontotransfer,
                ItemMultiMRPId: data.ItemMultiMRPId,
                MRP: data.price,
            };
            console.log(dataToPost);
            $http.post(url, dataToPost)
            .success(function (response) {
                $scope.items = response;
                if (response != null) {
                    alert(data.Number + " Item transfer successfully");
                    $("#st" + response.ItemId).prop("disabled", true);
                    location.reload();
                }
                else {

                    alert(data.Number+'Item not transfered');
                }
               

            });

        }

       
    }
    $scope.currentPageStores = {};
    $scope.pageno = 1; // initialize page no to 1
    $scope.total_count = 0;
    $scope.itemsPerPage = 50; //this could be a dynamic value from a drop down
    $scope.numPerPageOpt = [50, 100, 200, 300];//dropdown options for no. of Items per page
    $scope.onNumPerPageChange = function () {
        $scope.itemsPerPage = $scope.selected;
        $scope.getdamagedata($scope.pageno);
    }
    $scope.selected = $scope.numPerPageOpt[0];// for Html page dropdown

    $scope.$on('$viewContentLoaded', function () {
        $scope.getdamagedata($scope.pageno);
    });


    $scope.getdamagedata = function (pageno) {

        $scope.currentPageStores = {};
        var url = serviceBase + "api/damagestock/get" + "?list=" + $scope.itemsPerPage + "&page=" + pageno + "&WarehouseId=" + $scope.wid;

        $http.get(url)
        .success(function (results) {
            $scope.currentPageStores = results.damagest;
            $scope.total_count = results.total_count;
        })
         .error(function (data) {
             console.log(data);
         })
    };
}]);
