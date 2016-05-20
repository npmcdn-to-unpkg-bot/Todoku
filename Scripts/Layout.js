var app = angular.module('Todoku', []);
app.$inject = ['$scope'];

app.service('GlobalFunction', function ($http) {
    this.AjaxService = function (url, method, data, SuccessCallback, ErrorCallback) {
        $http({ method: method, url: url, params: data }).then(SuccessCallback, ErrorCallback);
    }
});

app.service('GlobalNotification', function () {
    this.ActivateNotification = function (value, SuccessCallback, ErrorCallback) {
        try {
            var result
            switch (value) {
                case "success": result = { Info: "Success", class: "alert alert-success", template: "notif-tmpl" }; break;
                case "info": result = { Info: "Info", class: "alert alert-info", template: "notif-tmpl" }; break;
                case "warning": result = { Info: "Warning", class: "alert alert-warning", template: "notif-tmpl" }; break;
                case "error": result = { Info: "Error", class: "alert alert-danger", template: "notif-tmpl" }; break;
            }
            SuccessCallback(result);
        } catch (ex) {
            ErrorCallback(ex);
        }
    }   
});

app.filter('commatodecimal', [
    function () { // should be altered to suit your needs
        return function (input) { var ret = (input) ? input.toString().trim().replace(/,/g, ".") : null; return ret; };
    }
]);

app.directive('format', ['$filter', function ($filter) {
    return {
        require: '?ngModel',
        link: function (scope, elem, attrs, ctrl) {
            if (!ctrl) return;


            ctrl.$formatters.unshift(function (a) {
                var attr = attrs.format.replace(/ /g, '').split('|');
                var value = ctrl.$modelValue;
                angular.forEach(attr, function (filter) { value = $filter(filter)(value); });
                return value;
            });


            ctrl.$parsers.unshift(function (viewValue) {
                var plainNumber = viewValue.replace(/[^\d|\-+|\,+]/g, '');
                var attr = attrs.format.replace(/ /g, '').split('|');
                var value = plainNumber;
                angular.forEach(attr, function (filter) { value = $filter(filter)(value); });
                elem.val(value);
                return plainNumber;
            });
        }
    };
} ]);