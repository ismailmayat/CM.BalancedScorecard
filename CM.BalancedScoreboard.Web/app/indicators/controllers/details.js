module.exports = [
    "$scope", "$routeParams", "$filter", "$location", "indicatorsApi", "indicatorsGraphFactory", "utils", "ngTableParams", "toaster",
    function($scope, $routeParams, $filter, $location, indicatorsApi, indicatorsGraphFactory, utils, ngTableParams, toaster) {
        var originalData = [];

        function bindModel() {
            $scope.indicator.StartDate = $scope.startDate;
            $scope.indicator.ComparisonValueType = $scope.selectedComparisonValue.id;
            $scope.indicator.PeriodicityType = $scope.selectedPeriodicity.id;
            $scope.indicator.ObjectValueType = $scope.selectedObjectValue.id;
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

        function updateTable() {
            $scope.tableParams.reload();
        }

        function bindGraph() {
            var graphData = indicatorsGraphFactory.getGraphData($scope.getSelectedYearData());
            $scope.series = graphData.series;
            $scope.labels = graphData.labels;
            $scope.data = graphData.data;
            $scope.colours = graphData.colours;
        }

        function initTable() {
            $scope.tableParams = new ngTableParams(
            {
                page: 1,
                sorting: {
                    Date: 'desc'
                }
            },
            {
                total: $scope.getSelectedYearData().length,
                counts: [],
                getData: function($defer, params) {
                    $defer.resolve($filter('orderBy')($scope.getSelectedYearData(), params.orderBy()));
                }
            });
        }

        function loadIndicator(callback) {
            indicatorsApi.indicators.get({ id: $routeParams.indicatorId }).$promise
                .then(function(data) {
                    callback(data);
                })
                .catch(function(msg) {
                    toaster.error({ body: msg });
                });
        }

        function bindIndicator(response) {
            $scope.indicator = response.Data;
            $scope.comparisonValueTypeList = response.ComparisonValueTypeList;
            $scope.periodicityTypeList = response.PeriodicityTypeList;
            $scope.objectValueTypeList = response.ObjectValueTypeList;
            $scope.splitTypeList = response.SplitTypeList;
            $scope.startDate = new Date(response.Data.StartDate);
            $scope.selectedComparisonValue = $.grep($scope.comparisonValueTypeList, function(e) { return e.id === response.Data.ComparisonValueType; })[0];
            $scope.selectedPeriodicity = $.grep($scope.periodicityTypeList, function(e) { return e.id === response.Data.PeriodicityType; })[0];
            $scope.selectedObjectValue = $.grep($scope.objectValueTypeList, function(e) { return e.id === response.Data.ObjectValueType; })[0];
            $scope.config = response.Config;
        }

        function loadIndicatorMeasures(callback, tableAction) {
            indicatorsApi.indicatorMeasures.query({ id: $routeParams.indicatorId }).$promise
                .then(function (response) {
                    callback(response.Data, tableAction);
                    $scope.measuresConfig = response.Config;
                })
                .catch(function(msg) {
                    toaster.error({ body: msg });
                });
        }

        function bindIndicatorMeasures(data, tableAction) {
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
            bindGraph();
        }

        function init() {
            loadIndicator(bindIndicator);
            loadIndicatorMeasures(bindIndicatorMeasures, initTable);
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
            bindGraph();
        }

        $scope.showYear = function(year) {
            return $scope.selectedYear !== year;
        }

        $scope.saveIndicator = function() {
            if ($scope.indicatorForm.$invalid || $scope.globalEdit) {
                return;
            }

            bindModel();
            indicatorsApi.indicators.update({ id: $scope.indicator.Id }, $scope.indicator).$promise
                .then(function() {
                    toaster.success({ body: 'Indicator successfully saved!' });
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

        $scope.formatDate = function(date) {
            return new Date(date);
        }

        $scope.formatMeasureValue = function(valueType, value) {

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
                    loadIndicatorMeasures(bindIndicatorMeasures, updateTable);
                })
                .catch(function() {
                    toaster.error({ body: 'Indicator successfully deleted!' });
                });
        }

        $scope.savePeriod = function(row) {
            $scope.selectedYear = row.Date.getFullYear();
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
                    loadIndicatorMeasures(bindIndicatorMeasures, updateTable);
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

            bindIndicatorMeasures($scope.measures, updateTable);
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

            bindIndicatorMeasures($scope.measures, updateTable);
        }

        init();
    }
];