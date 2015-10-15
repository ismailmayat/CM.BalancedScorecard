var indicatorsApp = angular.module('indicatorsApp', ['ngResource', 'ngAnimate', 'ngRoute', 'chart.js']);

indicatorsApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/list', {
            templateUrl: '/Scripts/app/indicators/partials/list.html',
            controller: 'indicatorsListCtrl'
        }).
        when('/details/:indicatorId', {
            templateUrl: '/Scripts/app/indicators/partials/details.html',
            controller: 'indicatorsDetailsCtrl'
        }).
        otherwise({
            redirectTo: '/list'
        });
  }]);

