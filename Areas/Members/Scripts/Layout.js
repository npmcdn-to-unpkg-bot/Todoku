var app = angular.module('Todoku', []);

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