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
	__webpack_require__(11);

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
	            $locationProvider.html5Mode(true)
	        }
	    ]);

/***/ },
/* 1 */
/***/ function(module, exports, __webpack_require__) {

	__webpack_require__(2);

	angular.module("indicators", ["ngResource", "ngAnimate", "chart.js", "ngTable", "toaster", "shared"]);

	angular.module("indicators").config(["ChartJsProvider", function (ChartJsProvider) {

	}]);

	angular.module("indicators").run(function ($rootScope, $http) {
	    $http.get("/api/indicators/resources")
	        .then(function success(response) {
	            $rootScope.resources = response.data;
	        }, function callback(response) {
	            console.error("Error: " + response.message);
	        });
	});

	angular.module("indicators").factory("indicatorsApi", __webpack_require__(7));
	angular.module("indicators").factory("indicatorsGraphFactory", __webpack_require__(8));
	angular.module("indicators").controller("indicatorsListCtrl", __webpack_require__(9));
	angular.module("indicators").controller("indicatorsDetailsCtrl", __webpack_require__(10));

/***/ },
/* 2 */
/***/ function(module, exports, __webpack_require__) {

	angular.module("shared", []);

	angular.module("shared").factory("utils", __webpack_require__(3));
	angular.module("shared").factory("configuration", __webpack_require__(4));
	//angular.module("shared").directive("showErrors", require("./directives/validation"));
	angular.module("shared").directive("formInput", __webpack_require__(5));

/***/ },
/* 3 */
/***/ function(module, exports) {

	module.exports = [
	    function() {
	        return {
	            formatDateInput: function(date) {
	                var d = new Date(date),
	                    month = '' + (d.getMonth() + 1),
	                    day = '' + d.getDate(),
	                    year = d.getFullYear();

	                if (month.length < 2) month = "0" + month;
	                if (day.length < 2) day = "0" + day;

	                return [year, month, day].join("-");
	            },
	            formatMonthInput: function (date) {
	                var d = new Date(date),
	                    month = '' + (d.getMonth() + 1),
	                    year = d.getFullYear();

	                if (month.length < 2) month = "0" + month;

	                return [year, month].join("-");
	            },
	            monthNames: function() {
	                return ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
	            },
	            formatGraphDate: function(date) {
	                var d = new Date(date);

	                return this.monthNames()[d.getMonth()] + ' ' + d.getFullYear().toString().substr(2, 4);
	            }
	        };
	    }
	];

/***/ },
/* 4 */
/***/ function(module, exports) {

	module.exports = [
	    function() {
	        return {

	        };
	    }
	];

/***/ },
/* 5 */
/***/ function(module, exports, __webpack_require__) {

	module.exports = ["utils",
	    function (utils) {
	        function getErrorMessage(directive, config) {
	            if (directive.$error.required) {
	                return config.ErrorMessage.Required;
	            }      
	            return "";
	        }

	        function setAttributes(input, config) {
	            if (config.Required != undefined) {
	                input.attr("required", "true");
	            }
	            if (config.MaxLength) {
	                input.attr("maxlength", config.MaxLength);
	            }
	            if (config.Range) {
	                input.attr("min", config.Range.MinValue);
	                input.attr("max", config.Range.MaxValue);
	            }
	        }

	        function setValidationState(input, controller) {
	            if (input.attr("required")) {
	                if (input.val().length === 0) {
	                    controller.$setValidity("required", false);
	                }
	                else {
	                    controller.$setValidity("required", true);
	                }
	            }
	        }

	        function setValidationAttributes(div, p, controller) {
	            div.toggleClass("has-error", controller.$invalid);
	            div.toggleClass("has-success", controller.$valid && controller.$dirty);
	            p.toggleClass("ng-show", controller.$invalid);
	            p.toggleClass("ng-hide", controller.$valid);
	        }

	        function setValidationMessages(p, scope, controller) {
	            p.text(getErrorMessage(controller, scope.config));
	        }

	        function setInputValue(input, scope, controller) {
	            if (scope.config !== undefined) {
	                if (scope.config.InputType === "date") {
	                    input.val(utils.formatDateInput(new Date(controller.$viewValue)));
	                } else if (scope.config.InputType === "month") {
	                    input.val(utils.formatMonthInput(new Date(controller.$viewValue)));
	                } else {
	                    input.val(controller.$viewValue);
	                }
	            } else {
	                input.val(controller.$viewValue);
	            }
	        }

	        return {
	            restrict: "E",
	            templateUrl: "/app/shared/directives/formInput/views/view.html",
	            scope:{
	                config: "=",
	                hideLabel: "="
	            },
	            require: "ngModel",
	            controller: __webpack_require__(6),
	            link: function (scope, el, attrs, modelController) {
	                var div = el.find("div");
	                var input = el.find("input");
	                var p = el.find("p");;
	           
	                modelController.$render = function () {
	                    setInputValue(input, scope, modelController);
	                };

	                input.bind("keypress", function () {
	                    modelController.$setViewValue(input.val());
	                    modelController.$render();
	                    setValidationState(input, modelController);
	                });

	                input.bind("blur", function () {
	                    modelController.$setViewValue(input.val());
	                    modelController.$render();
	                    setValidationState(input, modelController);
	                    setValidationAttributes(div, p, modelController);
	                    if (modelController.$invalid) {
	                        setValidationMessages(p, scope, modelController);
	                    }
	                });

	                scope.$watch("config", function (config) {
	                    if (config !== undefined) {
	                        setAttributes(input, config);
	                    }
	                });

	                scope.$on('show-errors-check-validity', function () {
	                    setValidationState(input, modelController);
	                    setValidationAttributes(div, p, modelController);
	                    if (modelController.$invalid) {
	                        setValidationMessages(p, scope, modelController);
	                    }
	                });
	            }
	        }
	    }
	];

/***/ },
/* 6 */
/***/ function(module, exports) {

	module.exports = [
	    "$scope", function($scope) {
	        $scope.formControlClass = function() {
	            if ($scope.config !== undefined) {
	                if ($scope.config.InputType === "range")
	                    return "";

	                if ($scope.config.InputType === "checkbox")
	                    return "";

	                var cssClass = "form-control";
	                cssClass += $scope.config.InputType === "number" ? " text-right" : "";

	                return cssClass;
	            }
	            return "";
	        };
	    }
	];

/***/ },
/* 7 */
/***/ function(module, exports) {

	module.exports = [
	    "$resource", function ($resource) {
	        return {
	            indicators: $resource("/api/indicators/:id", null, {
	                "query": { isArray: false },
	                "save": {
	                    method: "POST", transformResponse: function (data, headers) {
	                        response = {}
	                        response.data = data;
	                        response.headers = headers();
	                        return response;
	                    }
	                },
	                "update": { method: "PUT" }
	            }),
	            indicatorMeasures: $resource("/api/indicators/:id/measures/:measureId", null, {
	                "query": { isArray: false },
	                "update": { method: "PUT" }
	            })
	        }
	    }
	];

/***/ },
/* 8 */
/***/ function(module, exports) {

	module.exports = [
	    "utils", function(utils) {
	        return {
	            getGraphData: function(indicatorMeasures) {
	                return {
	                    series: getIndicatorGraphSeriesNames(),
	                    labels: getIndicatorGraphLabels(indicatorMeasures),
	                    data: getIndicatorGraphValues(indicatorMeasures),
	                    colours: getIndicatorGraphColours(),
	                    options: getIndicatorGraphOptions()
	                }
	            },
	            getGraphOptions: function() {
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
	        }
	        function getIndicatorGraphOptions(){
	            return {
	                animation: true,
	                showTooltips: true,
	                datasetStrokeWidth: 0.5,
	                scaleBeginAtZero: true
	            };
	        }
	    }
	];

/***/ },
/* 9 */
/***/ function(module, exports) {

	module.exports = [
	    "$scope", "$animate", "$location", "$anchorScroll", "indicatorsApi", "indicatorsGraphFactory", "toaster",
	    function($scope, $animate, $location, $anchorScroll, indicatorsApi, indicatorsGraphFactory, toaster) {
	        function loadMeasures(indicatorId, callback) {
	            indicatorsApi.indicatorMeasures.query({ id: indicatorId }).$promise
	                .then(function(response) {
	                    callback(indicatorId, response);
	                })
	                .catch(function() {
	                    toaster.error({ body: "An error ocurred while trying to load the measures of the selected indicator" });
	                });
	        }

	        function loadIndicators() {
	            $scope.indicators = indicatorsApi.indicators.query({ filter: $scope.filter }).$promise
	                .then(function(response) {
	                    $scope.indicators = response.Data;
	                })
	                .catch(function() {
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
	            var graphData = indicatorsGraphFactory.getGraphData(firstYear.Measures);
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

	        $scope.onEnter = function(event) {
	            if (event.charCode === 13) {
	                loadIndicators();
	            }
	        };

	        $scope.search = function() {
	            loadIndicators();
	        }

	        $scope.navigateToDetails = function(indicatorId) {
	            $location.path('/Indicators/Details/' + indicatorId);
	        }

	        $scope.getIndicatorStateClass = function(indicator) {
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

	        $scope.showMeasures = function(indicatorId, anchor) {
	            if ($scope.showingIndicator === undefined) {
	                loadMeasures(indicatorId, loadMeasuresCallback);
	                //var hash = "indicator" + anchor;
	                //if ($location.hash() !== hash) {
	                //    // set the $location.hash to `newHash` and
	                //    // $anchorScroll will automatically scroll to it
	                //    $location.hash(hash);
	                //} else {
	                //    $anchorScroll();
	                //}
	            } else {
	                if ($scope.showingIndicator === indicatorId) {
	                    $scope.showingIndicator = undefined;
	                    initGraph();
	                    //$anchorScroll();
	                }
	            }
	        }

	        $scope.showingMeasures = function(indicatorId) {
	            return $scope.showingIndicator === indicatorId;
	        }

	        $scope.showingGraph = function() {
	            return $scope.graphData !== undefined;
	        }

	        $scope.showingLegend = function() {
	            return $scope.indicators !== undefined && $scope.indicators.length > 0;
	        }
	    }
	];




	    

/***/ },
/* 10 */
/***/ function(module, exports) {

	module.exports = [
	    "$scope", "$routeParams", "$filter", "$location", "indicatorsApi", "indicatorsGraphFactory", "utils", "ngTableParams", "toaster",
	    function($scope, $routeParams, $filter, $location, indicatorsApi, indicatorsGraphFactory, utils, ngTableParams, toaster) {
	        var originalData = [];

	        function bindModel() {
	            $scope.indicator.ComparisonValueType = $scope.selectedComparisonValue.id;
	            $scope.indicator.PeriodicityType = $scope.selectedPeriodicity.id;
	            $scope.indicator.ObjectValueType = $scope.selectedObjectValue.id;
	            $scope.indicator.IndicatorTypeId = $scope.selectedIndicatorType.Id;
	            $scope.indicator.ManagerId = $scope.selectedManager.Id;
	        }

	        function isNewPeriod(item) {
	            return item.Id === '';
	        };

	        function resetPeriod(row, rowForm) {
	            row.isEditing = false;
	            rowForm.$setPristine();

	            var originalYearData = _.find(originalData, function(r) {
	                return r.Year === $scope.selectedYear;
	            });

	            var originalRow = _.find(originalYearData.Measures, function(r) {
	                return r.Id === row.Id;
	            });

	            if (originalRow != undefined) {
	                angular.extend(row, originalRow);
	            }
	        }

	        function createMeasure() {
	            return {
	                Date: new Date().toDateString(),
	                Id: '',
	                IndicatorId: '',
	                RealValue: '',
	                TargetValue: ''
	            };
	        }

	        function initTable() {
	            $scope.tableParams = new ngTableParams(
	            {
	                page: 1,
	                sorting: {
	                    Date: 'desc'
	                },
	                count: 100
	            },
	            {
	                total: $scope.getSelectedYearData().length,
	                counts: [],
	                getData: function ($defer, params) {
	                    $defer.resolve($filter('orderBy')($scope.getSelectedYearData(), params.orderBy()));
	                }
	            });
	        }

	        function initGraph() {
	            var graphData = indicatorsGraphFactory.getGraphData($scope.getSelectedYearData());
	            $scope.series = graphData.series;
	            $scope.labels = graphData.labels;
	            $scope.data = graphData.data;
	            $scope.colours = graphData.colours;
	            $scope.options = graphData.options;
	        }

	        function updateTable() {
	            $scope.tableParams.reload();
	        }

	        function updateGraph() {
	            var graphData = indicatorsGraphFactory.getGraphData($scope.getSelectedYearData());
	            $scope.data = graphData.data;
	            $scope.labels = graphData.labels;
	        }

	        function loadIndicator(callback) {
	            var data = {};
	            if (!$scope.isNew()) {
	                data = { id: $routeParams.indicatorId };
	            }
	            indicatorsApi.indicators.get(data).$promise
	                .then(function(data) {
	                    callback(data);
	                })
	                .catch(function(msg) {
	                    toaster.error({ body: msg });
	                });
	        }

	        function bindIndicator(response) {
	            $scope.indicator = response.Data;
	            $scope.indicatorTypeList = response.IndicatorTypes;
	            $scope.userList = response.Users;
	            $scope.selectedComparisonValue = $.grep(response.Config.ComparisonValueType.Options, function (e) { return e.id === response.Data.ComparisonValueType; })[0];
	            $scope.selectedPeriodicity = $.grep(response.Config.PeriodicityType.Options, function (e) { return e.id === response.Data.PeriodicityType; })[0];
	            $scope.selectedObjectValue = $.grep(response.Config.ObjectValueType.Options, function (e) { return e.id === response.Data.ObjectValueType; })[0];
	            $scope.selectedIndicatorType = $.grep(response.IndicatorTypes, function (e) { return e.Id === response.Data.IndicatorTypeId; })[0];
	            $scope.selectedManager = $.grep(response.Users, function (e) { return e.Id === response.Data.ManagerId; })[0];
	            $scope.config = response.Config;
	        }

	        function loadIndicatorMeasures(callback, tableAction, graphAction) {
	            indicatorsApi.indicatorMeasures.query({ id: $routeParams.indicatorId }).$promise
	                .then(function (response) {
	                    callback(response.Data, tableAction, graphAction);
	                    $scope.measuresConfig = response.Config;
	                })
	                .catch(function(msg) {
	                    toaster.error({ body: msg });
	                });
	        }

	        function bindIndicatorMeasures(data, tableAction, graphAction) {
	            if (data.length > 0) {
	                $scope.measures = data;
	                originalData = angular.copy(data);
	                if ($scope.selectedYear === undefined || $scope.getSelectedYearData().length === 0) {
	                    $scope.selectedYear = _.first($scope.measures).Year;
	                }
	            } else {
	                $scope.measures = [];
	                $scope.selectedYear = undefined;
	            }
	            tableAction();
	            graphAction();
	        }

	        function init() {
	            loadIndicator(bindIndicator);
	            if (!$scope.isNew()) {
	                loadIndicatorMeasures(bindIndicatorMeasures, initTable, initGraph);
	            }
	        }

	        $scope.getSelectedYearData = function() {
	            var element = _.find($scope.measures, function(r) {
	                return r.Year === $scope.selectedYear;
	            });

	            return element !== undefined ? element.Measures : [];
	        }

	        $scope.switchYear = function(year) {
	            $scope.selectedYear = year;
	            updateTable();
	            updateGraph();
	        }

	        $scope.showYear = function(year) {
	            return $scope.selectedYear !== year;
	        }

	        $scope.saveIndicator = function () {
	            $scope.$broadcast('show-errors-check-validity');
	            if ($scope.indicatorForm.$invalid) {
	                return;
	            }

	            bindModel();

	            var promise;
	            if (!$scope.isNew()) {
	                promise = indicatorsApi.indicators.update({ id: $scope.indicator.Id }, $scope.indicator).$promise;
	            } else {
	                promise = indicatorsApi.indicators.save($scope.indicator).$promise;
	            }
	            promise
	                .then(function(response) {
	                    toaster.success({ body: 'Indicator successfully created!' });
	                    $location.path(response.headers.location);
	                })
	                .catch(function() {
	                    toaster.error({ body: 'An error ocurred while trying to save the indicator...' });
	                });
	        }

	        $scope.deleteIndicator = function() {
	            indicatorsApi.indicators.delete({ id: $scope.indicator.Id }).$promise
	                .then(function() {
	                    $location.path('/Indicators/List');
	                })
	                .catch(function() {
	                    toaster.error({ body: 'An error ocurred while trying to save the indicator...' });
	                });
	        }

	        $scope.formatGraphDate = function(date) {
	            return utils.formatGraphDate(date);
	        }

	        $scope.canEdit = function(row) {
	            if (!row.isEditing) {
	                row.isEditing = isNewPeriod(row);
	            }

	            return row.isEditing;
	        }

	        $scope.editPeriod = function(row) {
	            $scope.globalEdit = true;
	            row.isEditing = true;
	        }

	        $scope.deletePeriod = function(row) {
	            indicatorsApi.indicatorMeasures.delete({ id: $routeParams.indicatorId, measureId: row.Id }).$promise
	                .then(function() {
	                    loadIndicatorMeasures(bindIndicatorMeasures, updateTable, updateGraph);
	                })
	                .catch(function() {
	                    toaster.error({ body: 'Indicator successfully deleted!' });
	                });
	        }

	        $scope.savePeriod = function(row) {
	            $scope.selectedYear = new Date(row.Date);
	            $scope.globalEdit = false;
	            row.isEditing = false;
	            var promise = null;
	            if (isNewPeriod(row)) {
	                promise = indicatorsApi.indicatorMeasures.save({ id: $routeParams.indicatorId }, row).$promise;
	            } else {
	                promise = indicatorsApi.indicatorMeasures.update({ id: $routeParams.indicatorId, measureId: row.Id }, row).$promise;
	            }

	            promise
	                .then(function() {
	                    loadIndicatorMeasures(bindIndicatorMeasures, updateTable, updateGraph);
	                })
	                .catch(function() {
	                    toaster.error({ body: 'Indicator successfully deleted!' });
	                });
	        }

	        $scope.cancelPeriod = function(row, rowForm) {
	            $scope.globalEdit = false;
	            if (!isNewPeriod(row)) {
	                resetPeriod(row, rowForm);
	            } else {
	                _.remove($scope.measures, function(item) {
	                    return item.Year == $scope.selectedYear;
	                });
	                $scope.selectedYear = undefined;
	            }

	            bindIndicatorMeasures($scope.measures, updateTable, updateGraph);
	        }

	        $scope.addPeriod = function() {
	            $scope.globalEdit = true;
	            $scope.selectedYear = 0;
	            var measures = [];
	            measures.push(createMeasure());
	            $scope.measures.push({
	                Year: $scope.selectedYear,
	                Measures: measures
	            });

	            bindIndicatorMeasures($scope.measures, updateTable, updateGraph);
	        }

	        $scope.isNew = function () {
	            return $routeParams.indicatorId == undefined;
	        }

	        init();
	    }
	];

/***/ },
/* 11 */
/***/ function(module, exports, __webpack_require__) {

	__webpack_require__(2);

	angular.module("projects", []);

	angular.module("projects").controller("projectsListCtrl", __webpack_require__(12));

/***/ },
/* 12 */
/***/ function(module, exports) {

	module.exports = function () {

	}

/***/ }
/******/ ]);