app.controller('IndexController', function ($scope, GlobalFunction, GlobalNotification) {
    $scope.RootUrl = "";
    $scope.IsChecked = [];

    $scope.Init = function (value) {
        $scope.RootUrl = value;
        $scope.GetAgentDiscountList(value);
    }

    $scope.GetAgentDiscountList = function (value) {
        var url = value + "/Agents/Discount/GetAgentDiscountList";
        GlobalFunction.AjaxService(url, 'GET', '', function (response) {
            $scope.list = response.data;   
        }, function (response) {
            $scope.ShowNotif('error');
            $scope.message = response.data.Message;
        });
    }

    $scope.CheckAll = function () {
        var idx = 0;
        angular.forEach($scope.list, function (item) {
            $scope.IsChecked[idx++] = $scope.selectedAll;
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
});

app.controller('CreateController', function ($scope, GlobalFunction, GlobalNotification) {
    $scope.RootUrl = "";
    $scope.DiscountAmount = 15000;
    $scope.Data = { DiscountAmount: 0, ProductName: "", DiscountPercentage:0, ProductPrice: 0, LineAmount:0, ProductID: 0};

    $scope.Init = function (RootUrl) {
        $scope.RootUrl = RootUrl;
    }

    $scope.CallculateDiscount = function (value) {
        if (value == 1)
            $scope.Data.DiscountAmount = $scope.Data.DiscountPercentage * $scope.Data.ProductPrice / 100;
        else
            $scope.Data.DiscountPercentage = $scope.Data.DiscountAmount / $scope.Data.ProductPrice * 100;

        $scope.Data.LineAmount = $scope.Data.ProductPrice * 1 - $scope.Data.DiscountAmount * 1;
    }

    $scope.SaveData = function () {
        var url = $scope.RootUrl + "/Agents/Discount/Save";
        GlobalFunction.AjaxService(url, 'POST', $scope.Data,
        function (response) {
            if (response.data.ok) {
                window.location.href = $scope.RootUrl + "/Agents/Discount/"
            } else {
                $scope.ShowNotif('error');
                $scope.message = response.data.message;
            }
        },
        function (response) {
            $scope.ShowNotif('error');
            $scope.message = response.data.Message;
        })
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
});