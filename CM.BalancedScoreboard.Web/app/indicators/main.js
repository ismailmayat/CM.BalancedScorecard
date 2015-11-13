require("../shared/main");

angular.module("indicators", ["ngResource", "ngAnimate", "chart.js", "ngTable", "toaster", "shared"]);

angular.module("indicators").run(function ($rootScope, $http) {
    $http.get("/api/indicators/resources")
        .then(function success(response) {
            $rootScope.resources = response.data;
        }, function callback(response) {
            console.error("Error: " + response.message);
        });
});

angular.module("indicators").factory("indicatorsApi", require("./services/api"));
angular.module("indicators").factory("indicatorsGraphFactory", require("./services/graph"));
angular.module("indicators").controller("indicatorsListCtrl", require("./controllers/list"));
angular.module("indicators").controller("indicatorsDetailsCtrl", require("./controllers/details"));