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
                .then(function (response) {
                    if (!$scope.isNew()) {
                        toaster.success({ body: 'Indicator successfully saved!' });
                    } else {
                        $location.path(response.headers.location);
                        toaster.success({ body: 'Indicator successfully created!' });
                    }
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

        $scope.savePeriod = function (row, rowForm) {
            $scope.$broadcast('show-errors-check-validity');
            if (rowForm.$invalid) {
                return;
            }

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