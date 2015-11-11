require("./shared/main");
require("./indicators/main");

angular.module("app", ["indicators"])
    .config([
        "$routeProvider",
        function($routeProvider) {
            $routeProvider.
                when("Indicators/List", {
                    templateUrl: "./indicators/views/list.html",
                    controller: "indicatorsListCtrl"
                }).
                when("Indicators/Details/:indicatorId", {
                    templateUrl: "./indicators/views/details.html",
                    controller: "indicatorsDetailsCtrl"
                }).
                otherwise({
                    redirectTo: "Indicators/List"
                });
        }
    ]);