/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};

/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {

/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;

/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};

/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);

/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;

/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}


/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;

/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;

/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";

/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ function(module, exports, __webpack_require__) {

	__webpack_require__(1);
	__webpack_require__(2);
	__webpack_require__(3);

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

/***/ },
/* 1 */
/***/ function(module, exports) {

	angular.module('shared', []);

/***/ },
/* 2 */
/***/ function(module, exports) {

	angular.module("indicators", ["ngResource", "ngAnimate", "chart.js", "toaster", "shared"]);


/***/ },
/* 3 */
/***/ function(module, exports, __webpack_require__) {

	__webpack_require__(4);
	__webpack_require__(5);

	angular.module("indicators").controller('indicatorsListCtrl', function ($scope, $animate, $location, $anchorScroll, indicatorsApi, graphFactory, toaster) {

	    function loadMeasures(indicatorId, callback) {
	        indicatorsApi.indicatorMeasures.query({ id: indicatorId }).$promise
	            .then(function(response) {
	                callback(indicatorId, response);
	            })
	           .catch(function () {
	               toaster.error({ body: "An error ocurred while trying to load the measures of the selected indicator" });
	           });
	    }

	    function loadIndicators() {
	        $scope.indicators = indicatorsApi.indicators.query({ filter: $scope.filter }).$promise
	            .then(function(response) {
	                $scope.indicators = response;
	            })
	            .catch(function () {
	                toaster.error({ body: "An error ocurred while trying to load the indicators" });
	            });
	    }

	    function initGraph() {
	        $scope.graphLabels = undefined;
	        $scope.graphSeries = undefined;
	        $scope.graphData = undefined;
	        $scope.graphColours = undefined;
	    }

	    function bindGraph(data) {
	        var firstYear = _.first(data);
	        var graphData = graphFactory.getGraphData(firstYear.Measures);
	        $scope.graphColours = graphData.colours;
	        $scope.graphSeries = graphData.series;
	        $scope.graphLabels = graphData.labels;
	        $scope.graphData = graphData.data;
	    }

	    function loadMeasuresCallback(indicatorId, response) {
	        if (response.Data.length > 0) {
	            bindGraph(response.Data);
	        } else {
	            initGraph();
	        }
	        $scope.showingIndicator = indicatorId;
	    }

	    $scope.onEnter = function (event) {
	        if (event.charCode === 13) {
	            loadIndicators();
	        }
	    };

	    $scope.search = function() {
	        loadIndicators();
	    }

	    $scope.navigateToDetails = function (indicatorId) {
	        $location.path('/Indicators/Details/' + indicatorId);
	    }

	    $scope.getIndicatorStateClass = function (indicator) {
	        switch (indicator.State) {
	            case 0:
	                return 'panel-default';
	            case 1:
	                return 'panel-success';
	            case 2:
	                return 'panel-warning';
	            case 3:
	                return 'panel-danger';
	            default:
	                return 'panel-default';
	        }
	    }

	    $scope.showMeasures = function (indicatorId, anchor) {
	        if ($scope.showingIndicator === undefined) {
	            loadMeasures(indicatorId, loadMeasuresCallback);
	            var hash = "indicator" + anchor;
	            if ($location.hash() !== hash) {
	                // set the $location.hash to `newHash` and
	                // $anchorScroll will automatically scroll to it
	                $location.hash(hash);
	            } else {
	                $anchorScroll();
	            }
	        } else {
	            if ($scope.showingIndicator === indicatorId) {
	                $scope.showingIndicator = undefined;
	                initGraph();
	                $anchorScroll();
	            }
	        }
	    }

	    $scope.showingMeasures = function (indicatorId) {
	        return $scope.showingIndicator === indicatorId;
	    }

	    $scope.showingGraph = function() {
	        return $scope.graphData !== undefined;
	    }

	    $scope.showingLegend = function () {
	        return $scope.indicators !== undefined && $scope.indicators.length > 0;
	    }
	}); 

/***/ },
/* 4 */
/***/ function(module, exports) {

	angular.module("indicators").factory('indicatorsApi', ['$resource', function ($resource) {
	    return {
	        indicators: $resource('/api/indicators/:id', null, {
	            'update': { method: 'PUT' }
	        }),
	        indicatorMeasures: $resource('/api/indicators/:id/measures/:measureId', null, {
	            'query': { isArray: false },
	            'update': { method: 'PUT' }
	        })
	    }
	}]);

/***/ },
/* 5 */
/***/ function(module, exports, __webpack_require__) {

	__webpack_require__(6);

	angular.module("indicators").factory("graphFactory", function (utils) {
	    return {
	        getGraphData: function (indicatorMeasures) {
	            return {
	                series: getIndicatorGraphSeriesNames(),
	                labels: getIndicatorGraphLabels(indicatorMeasures),
	                data: getIndicatorGraphValues(indicatorMeasures),
	                colours: getIndicatorGraphColours()
	            }
	        },
	        getGraphOptions: function () {
	            bezierCurve: false
	        }
	    };

	    function getIndicatorGraphColours() {
	        return ["#00868B", "#FF7216"];
	    };

	    function getIndicatorGraphSeriesNames() {
	        return ["Real Value", "Target Value"];
	    };

	    function getIndicatorGraphLabels(indicatorMeasures) {
	        var labels = [];
	        for (index = 0; index < indicatorMeasures.length; ++index) {
	            var indicatorMeasure = indicatorMeasures[index];
	            labels.push(utils.formatGraphDate(indicatorMeasure.Date));
	        }
	        return labels;
	    };

	    function getIndicatorGraphValues(indicatorMeasures) {
	        var data = [[], []];
	        for (index = 0; index < indicatorMeasures.length; ++index) {
	            var indicatorMeasure = indicatorMeasures[index];
	            data[0].push(indicatorMeasure.RealValue);
	            data[1].push(indicatorMeasure.TargetValue);
	        }
	        return data;
	    };
	});

/***/ },
/* 6 */
/***/ function(module, exports) {

	angular.module('shared').factory('utils', function () {
	    return {
	        formatFullDate: function (date) {
	            var d = new Date(date),
	                month = '' + (d.getMonth() + 1),
	                day = '' + d.getDate(),
	                year = d.getFullYear();

	            if (month.length < 2) month = '0' + month;
	            if (day.length < 2) day = '0' + day;

	            return [year, month, day].join('/');
	        },
	        monthNames: function () {
	            return ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
	        },
	        formatGraphDate: function (date) {
	            var d = new Date(date);

	            return this.monthNames()[d.getMonth()] + ' ' + d.getFullYear().toString().substr(2, 4);
	        }
	    };
	});

/***/ }
/******/ ]);