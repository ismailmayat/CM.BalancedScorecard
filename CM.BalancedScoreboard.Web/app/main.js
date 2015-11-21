require("./indicators/main");
require("./projects/main");

angular.module("app", ["ngRoute", "indicators", "projects"])
    .config([
        "$routeProvider", "$locationProvider",
        function ($routeProvider, $locationProvider) {
            $routeProvider.
                when("/Indicators/List", {
                    templateUrl: "app/indicators/views/list.html",
                    controller: "indicatorsListCtrl"
                }).
                when("/Indicators/Details/", {
                    templateUrl: "app/indicators/views/details.html",
                    controller: "indicatorsDetailsCtrl"
                }).
                when("/Indicators/Details/:indicatorId", {
                    templateUrl: "app/indicators/views/details.html",
                    controller: "indicatorsDetailsCtrl"
                }).
                when("/Projects/List", {
                    templateUrl: "app/projects/views/list.html",
                    controller: "projectsListCtrl"
                }).
                otherwise({
                    redirectTo: "/Indicators/List"
                });
            //$locationProvider.html5Mode(true)
        }
    ]);