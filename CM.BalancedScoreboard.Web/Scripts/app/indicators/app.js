var indicatorsApp = angular.module('indicatorsApp', ['ngResource', 'ngAnimate', 'ngRoute', 'chart.js', 'ngTable', 'shared', 'toaster']);

indicatorsApp.config(['$routeProvider', 'ChartJsProvider',
  function ($routeProvider, ChartJsProvider, graphFactory) {
      $routeProvider.
        when('/List', {
            templateUrl: '/Scripts/app/indicators/views/list.html',
            controller: 'indicatorsListCtrl'
        }).
        when('/Details/:indicatorId', {
            templateUrl: '/Scripts/app/indicators/views/details.html',
            controller: 'indicatorsDetailsCtrl'
        }).
        otherwise({
            redirectTo: '/List'
        });
      ChartJsProvider.setOptions({
        datasetFill: false,
        pointDotRadius: 5,
        bezierCurve: false
      });
  }]);

