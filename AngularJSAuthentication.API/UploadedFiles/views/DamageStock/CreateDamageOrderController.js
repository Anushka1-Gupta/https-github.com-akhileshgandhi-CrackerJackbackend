'use strict';
app.controller('CreateDamageOrderController', ['$scope', 'WarehouseService', "customerService", "$filter", "$http", "ngTableParams", '$modal', 'FileUploader', function ($scope, WarehouseService, customerService, $filter, $http, ngTableParams, $modal, FileUploader) {
    

    $scope.warehouse = [];
    $scope.warehouseId = '';
    WarehouseService.getwarehouse().then(function (results) {
        console.log(results.data);
        console.log("data");
        $scope.warehouse = results.data;
    }, function (error) {
    });
    $scope.customers = [];
    $scope.Customerid = '';

    $scope.getCustData = function (WarehouseId) {
        $scope.customers = [];
        $scope.warehouseId = WarehouseId;
        var url = serviceBase + 'api/damagestock/Custall?WarehouseId=' + $scope.warehouseId;
        $http.get(url)
        .success(function (data) {
            
            $scope.customers = data;
            console.log($scope.Customerid);
        });
    }
 

    $scope.getitemMaster = function(){
    
        $scope.getDamageItem();
      
    }

    $scope.getDamageItem = function () {
        $scope.DamageItemData = [];
        $scope.itemss = [];
        var url = serviceBase + 'api/damagestock/getall?WarehouseId=' + $scope.warehouseId;
        $http.get(url)
        .success(function (data) {
            $scope.DamageItemData = data;
            $scope.itemss = data;
            
        });
    }
    //TotalAmount = [];
    $scope.filtitemMaster = function (data) {
        
        $scope.selecteditem = JSON.parse(data.ItemId);
        $scope.AmountCalculation($scope.selecteditem);
    };

    $scope.TotalAmount = 0.0;
    $scope.AmountCalculation = function (data) {
        
        if (data.DamageInventory != 0 && data.DamageInventory != null) {
            $scope.TotalAmount = (data.DamageInventory * data.UnitPrice);
            console.log("Total amount" + $scope.TotalAmount);
        }
        else {
                $scope.TotalAmount = (data.DamageInventory * data.UnitPrice);
                console.log("Total amount" + $scope.TotalAmount);
            }

        
    }
    $scope.openmodel = function (data) {
        
        $scope.supplierData = false;
        $scope.supplierData1 = false;
        console.log("Modal opened Role");
        var modalInstance;
        modalInstance = $modal.open(
            {
                templateUrl: "myCreateModal.html",
                controller: "ModalCreateOrderController", resolve: { role: function () { return data } }
            }), modalInstance.result.then(function (selectedItem) { })
    };

    $scope.AddDOrder = function (data) {
        

        var url = serviceBase + "api/damageorder";

        var customerData = $("#site-name").val();
        if (customerData != "") {
            var customerObject = JSON.parse(customerData);
            data.Customer = customerObject;
        }
        console.log($scope.TotalAmount);
        var dataToPost = {
            "CustomerId": data.Customer,
            "Warehouseid": data.WarehouseId,
            "WarehouseName": data.WarehouseName,
            "DamageStockId": data.DamageStockId,
            "qty": data.DamageInventory,
            "ItemName": data.ItemName,
            "ItemNumber": data.ItemNumber,
            "ItemId": data.ItemId,
            "UnitPrice": data.UnitPrice,
            "TotalAmount": $scope.TotalAmount,
        };
       
        $("#stdamageorder").prop("disabled", true);
        console.log(dataToPost);
        $http.post(url, dataToPost)
            .success(function (response) {
               
                if (response != null) {
                    
                $scope.items = response;
                alert('Damage order  created Successfully');
                location.reload();
            }
            else {
                
                alert('Damage order not created');
            }
            
        });
    }



   
}]);
'use strict';
app.controller('ModalCreateOrderController', ['$scope', 'WarehouseService', "customerService", "$filter", "$http", "ngTableParams", '$modal', 'FileUploader', function ($scope, WarehouseService, customerService, $filter, $http, ngTableParams, $modal, FileUploader) {
    
    $scope.warehouse = [];
    $scope.warehouseId = '';
    WarehouseService.getwarehouse().then(function (results) {
        console.log(results.data);
        console.log("data");
        $scope.warehouse = results.data;
    }, function (error) {
    });
    $scope.customers = [];
    $scope.Customerid = '';

    $scope.getCustData = function (WarehouseId) {

        $scope.customers = [];
        $scope.warehouseId = WarehouseId;
        var url = serviceBase + 'api/damagestock/Custall?WarehouseId=' + $scope.warehouseId;
        $http.get(url)
            .success(function (data) {

                $scope.customers = data;
                console.log($scope.Customerid);
            });
    }


    $scope.getitemMaster = function () {

        $scope.getDamageItem();

    }

    $scope.getDamageItem = function () {
        
        $scope.DamageItemData = [];
        $scope.itemss = [];
        var url = serviceBase + 'api/damagestock/getall?WarehouseId=' + $scope.warehouseId;
        $http.get(url)
            .success(function (data) {
                $scope.DamageItemData = data;
                $scope.itemss = data;

            });
    }
    //TotalAmount = [];
    $scope.filtitemMaster = function (data) {
        
        $scope.selecteditem = JSON.parse(data.ItemId);
        $scope.AmountCalculation($scope.selecteditem);
    };

    $scope.TotalAmount = 0.0;
    $scope.AmountCalculation = function (data) {
        
        if (data.DamageInventory != 0 && data.DamageInventory != null) {
            $scope.TotalAmount = (data.DamageInventory * data.UnitPrice);
            console.log("Total amount" + $scope.TotalAmount);
        }
        else {
            $scope.TotalAmount = (data.DamageInventory * data.UnitPrice);
            console.log("Total amount" + $scope.TotalAmount);
        }


    }

    $scope.DOdata = [];
    $scope.itemdata = [];
    $scope.AddData = function (item,CustomerId) {
        

            var data = true;
            for (var c = 0; c < $scope.DOdata.length; c++) {
                if ($scope.DOdata[c].ItemId == item.ItemId) {
                    data = false;
                    break;
                }
            }
     
            if (data == true) {
                $scope.DOdata.push({
                    CustomerId: CustomerId,
                    WarehouseId: item.WarehouseId,
                    WarehouseName: item.WarehouseName,
                    DamageStockId: item.DamageStockId,
                    qty: item.DamageInventory,
                    ItemName: item.ItemName,
                    ItemNumber: item.ItemNumber,
                    ItemId: item.ItemId,
                    UnitPrice: item.UnitPrice,
                    TotalAmount: $scope.TotalAmount,
                });
                item.Noofset = "";
                item.PurchaseMinOrderQty = "";
                item.ItemId = "";
            }
            else {
                alert("Item is Already Added");
                item.Noofset = "";
                item.PurchaseMinOrderQty = "";
                item.ItemId = "";
            }
        }

        //$scope.POdata.push(item);
    $scope.Searchsave = function (CustomerId) {
        
        
        var data = $scope.DOdata;
        data.Customerid = CustomerId;
        var url = serviceBase + "api/damageorder/createDS";
        $http.post(url, data).success(function (result) {
            console.log("Error Got Here");
            console.log(data);
            if (data.id == 0) {
                $scope.gotErrors = true;
                if (data[0].exception == "Already") {
                    console.log("Got This User Already Exist");
                    $scope.AlreadyExist = true;
                }
            }
            else {
                //$modalInstance.close(data);
                alert('Create Damge  Done');
                window.location.reload()
            }
        })
         .error(function (data) {
             console.log("Error Got Heere is ");
             console.log(data);
             // return $scope.showInfoOnSubmit = !0, $scope.revert()
         })
    };

    //$scope.AddDOrder = function (data) {

    //    var url = serviceBase + "api/damageorder";

    //    var customerData = $("#site-name").val();
    //    if (customerData != "") {
    //        var customerObject = JSON.parse(customerData);
    //        data.Customer = customerObject;
    //    }
    //    console.log($scope.TotalAmount);
    //    var dataToPost = {
    //        "CustomerId": data.Customer,
    //        "Warehouseid": data.WarehouseId,
    //        "WarehouseName": data.WarehouseName,
    //        "DamageStockId": data.DamageStockId,
    //        "qty": data.DamageInventory,
    //        "ItemName": data.ItemName,
    //        "ItemNumber": data.ItemNumber,
    //        "ItemId": data.ItemId,
    //        "UnitPrice": data.UnitPrice,
    //        "TotalAmount": $scope.TotalAmount,
    //    };

    //    $("#stdamageorder").prop("disabled", true);
    //    console.log(dataToPost);
    //    $http.post(url, dataToPost)
    //        .success(function (response) {

    //            if (response != null) {

    //                $scope.items = response;
    //                alert('Damage order  created Successfully');
    //                location.reload();
    //            }
    //            else {

    //                alert('Damage order not created');
    //            }

    //        });
    //}


}]);
