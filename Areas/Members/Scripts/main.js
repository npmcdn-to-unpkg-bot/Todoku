var app = angular.module('Todoku', []);

app.controller('ProcessController', function ($scope, $http) {
    $scope.name = "John Doe";
    $scope.records = ["Semangka", "Apel", "Anggur"];
    $scope.GetShippingCost = function () {
        var data = { origin: '501', destination: '114', weight: '1700', courier: 'jne' };
        var url = "http://localhost:8081/rajaongkir/cost/";
        AjaxFunction(url, data, function (result) {
            console.log(result);
        });
    }
    $scope.GetAddress = function (value) {

    };
});