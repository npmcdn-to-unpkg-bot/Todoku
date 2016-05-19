app.controller('ProcessAllController', function ($scope, $http) {
    $scope.StoreAddress = "152";
    $scope.Address = "";
    $scope.ShippingCity = "";
    $scope.SelectedCourier = "jne";
    $scope.TotalWeight = "2000";

    $scope.Init = function (value, list) {
        var arr = list.split('|');
        $scope.GetPurchaseOrderList(value, { list: arr });
        $scope.GetBankList(value);
        $scope.GetShippingAddressList(value);
    }

    $scope.GetPurchaseOrderList = function (value, data) {
        $http({
            method: 'GET',
            url: value + "Members/Order/GetPurchaseOrderHdList",
            params: data
        }).then(function successCallback(response) {
            $scope.list = response.data;
            $scope.model = response.data[0];
            $scope.GetPageCount();
        }, function errorCallback(response) {
            console.log(response);
        });
    }

    $scope.GetBankList = function (value) {
        $http({
            method: 'GET',
            url: value + "Members/Order/GetBankList"
        }).then(function successCallback(response) {
            $scope.Banks = response.data;
        }, function errorCallback(response) {
            console.log(response);
        });
    }

    $scope.GetShippingAddressList = function (value) {
        $http({
            method: 'GET',
            url: value + "Members/Order/GetShippingAddressList"
        }).then(function successCallback(response) {
            $scope.ShippingAddresses = response.data;
        }, function errorCallback(response) {
            console.log(response);
        });
    }

    $scope.GetPageCount = function () {
        $scope.PageCount = $scope.list.length;
    }

    $scope.ChangeData = function (value) {
        $scope.model = $scope.list[value];
        console.log($scope.model);
    }

    $scope.ChangeAddressData = function (value) {
        $scope.SelectedAddress = $scope.ShippingAddresses[value];
    }

    $scope.SetAddress = function () {
        var temp = $scope.SelectedAddress;
        $scope.ShippingCity = temp.RajaOngkir_City_ID;
        var txt = "Provinsi : " + temp.ScProvince.StandardCodeName + "\r\n" + "Kota : " + temp.City + "\r\n" + "Alamat : " + temp.Address + "\r\n" + "Kode Pos : " + temp.ZipCode
        $scope.Address = txt;
        $scope.GetShippingCharges();
    }

    $scope.GetShippingCharges = function () {
        var url = "http://localhost:8081/rajaongkir/cost/";
        //var data = { origin: $scope.StoreAddress, destination: $scope.ShippingCity, weight: $scope.TotalWeight, courier: $scope.SelectedCourier };
        var data = "origin=" + $scope.StoreAddress + "&destination=" + $scope.ShippingCity + "&weight=" + $scope.TotalWeight + "&courier=" + $scope.SelectedCourier;

        var config = {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            //headers: { 'Content-Type': 'text/plain' }
            //dataType: "json"
        };
        var res = $http.post(url, data, config);
        res.success(function (data, status, headers, config) {
            console.log(data)
        });
        res.error(function (data, status, headers, config) {
            alert("failure message: " + JSON.stringify({ data: data }));
        });
    }
});

app.controller('IndexController', function ($scope, $location, GlobalFunction, GlobalNotification) {
    $scope.RootUrl = "";
    $scope.IsChecked = [];

    $scope.Init = function (value) {
        $scope.RootUrl = value;
        $scope.GetPurchaseOrderList(value);
    }

    $scope.Void = function (value) {
        GlobalFunction.AjaxService($scope.RootUrl + "Members/Order/Void/", 'POST', { id: value },
        function (response) {
            if (response.data.ok) {
                $scope.GetPurchaseOrderList($scope.RootUrl);
                $scope.message = response.data.Message;
                $scope.ShowNotif('success');
            } else {
                $scope.ShowNotif('error');
                $scope.message = response.data.Message;
            }
        },
        function (response) {
            $scope.ShowNotif('error');
            $scope.message = response.data.Message;
        });
    }

    $scope.GetPurchaseOrderList = function (value) {
        GlobalFunction.AjaxService(value + "Members/Order/GetPurchaseOrderHdList", 'GET', '',
            function (response) { $scope.list = response.data; },
            function (response) {
                $scope.ShowNotif('error');
                $scope.message = response.data;
            });
    }

    $scope.ShowNotif = function (value) {
        GlobalNotification.ActivateNotification(value,
        function (result) {
            $scope.NtfStatus = result.Info;
            $scope.template = result.template;
            $scope.NtfClass = result.class;
        }, function (result) {
            $scope.ShowNotif('error');
            $scope.message = result;
        });
    }

    $scope.ClearTemplate = function () {
        $scope.template = '';
    }

    $scope.CheckAll = function () {
        var idx = 0;
        angular.forEach($scope.list, function (item) {
            $scope.IsChecked[idx++] = $scope.selectedAll;
        });
    }

    $scope.VoidAll = function () {
        var idx = 0;
        var list = [];
        angular.forEach($scope.IsChecked, function (data) {
            if (data) list.push($scope.list[idx].OrderID);
            idx++;
        });
        var url = $scope.RootUrl + "/Members/Order/VoidAll";
        GlobalFunction.AjaxService(url, 'POST', { list: list }, function (response) {
            if (response.data.ok) {
                $scope.GetPurchaseOrderList($scope.RootUrl);
                $scope.message = response.data.Message;
                $scope.ShowNotif('success');
            }
            else {
                $scope.ShowNotif('error');
                $scope.message = response.data.Message;
            }
        }, function (response) {
            $scope.ShowNotif('error');
            $scope.message = response.data.Message;
        })
    }

    $scope.ProcessAll = function () {
        var idx = 0;
        var list = [];
        angular.forEach($scope.IsChecked, function (data) {
            if (data) list.push($scope.list[idx].OrderID);
            idx++;
        });
        var url = $scope.RootUrl + "/Members/Order/ProcessAll?list="+list.join('|');
        window.location.href = url;
        //$scope.$apply(function () { $location.path('/ProcessAll'); });
    }
});