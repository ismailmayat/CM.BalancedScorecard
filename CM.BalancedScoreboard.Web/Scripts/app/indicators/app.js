var indicatorsApp = angular.module('indicatorsApp', ['ngResource', 'ngAnimate', 'ngRoute', 'chart.js', 'ngTable']);

indicatorsApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/list', {
            templateUrl: '/Scripts/app/indicators/views/list.html',
            controller: 'indicatorsListCtrl'
        }).
        when('/details/:indicatorId', {
            templateUrl: '/Scripts/app/indicators/views/details.html',
            controller: 'indicatorsDetailsCtrl'
        }).
        otherwise({
            redirectTo: '/list'
        });
  }]);

