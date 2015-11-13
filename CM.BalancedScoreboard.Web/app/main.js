require("./shared/main");
require("./indicators/main");

angular.module("app", ["ngRoute", "indicators"])
    .config([
        "$routeProvider",
        function ($routeProvider) {
            $routeProvider.
                when("/Indicators/List", {
                    templateUrl: "app/indicators/views/list.html",
                    controller: "indicatorsListCtrl"
                }).
                when("/Indicators/Details/:indicatorId", {
                    templateUrl: "app/indicators/views/details.html",
                    controller: "indicatorsDetailsCtrl"
                }).
                otherwise({
                    redirectTo: "/Indicators/List"
                });
        }
    ]);