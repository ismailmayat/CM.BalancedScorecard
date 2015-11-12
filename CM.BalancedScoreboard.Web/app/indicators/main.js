angular.module("indicators", ["ngResource", "ngAnimate", "chart.js", "ngTable", "toaster", "shared"])

angular.module("indicators").run(function ($rootScope, $http) {
    $http.get("/api/indicators/resources")
        .then(function success(response) {
            $rootScope.resources = response.data;
        }, function callback(response) {

        });
});