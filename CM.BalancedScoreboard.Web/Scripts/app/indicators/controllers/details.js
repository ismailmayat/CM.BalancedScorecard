﻿indicatorsApp.controller('indicatorsDetailsCtrl', function ($scope, $routeParams, indicatorsApi, $filter, utils, graphFactory, ngTableParams, toaster) {
    var originalData = [];

    function init() {
        indicatorsApi.get({ id: $routeParams.indicatorId }).$promise
            .then(function (data) {
                assignValues(data);
                bindGraph();
            })
            .catch(function (msg) {
                console.error(msg);
            });
    };

    function assignValues(data) {
        $scope.indicator = data.Indicator;
        $scope.comparisonValueTypeList = data.ComparisonValueTypeList;
        $scope.periodicityTypeList = data.PeriodicityTypeList;
        $scope.objectValueTypeList = data.ObjectValueTypeList;
        $scope.splitTypeList = data.SplitTypeList;
        $scope.startDate = new Date(data.Indicator.StartDate);
        $scope.selectedComparisonValue = $.grep($scope.comparisonValueTypeList, function (e) { return e.id === data.Indicator.ComparisonValueType; })[0];
        $scope.selectedPeriodicity = $.grep($scope.periodicityTypeList, function (e) { return e.id === data.Indicator.PeriodicityType; })[0];
        $scope.selectedObjectValue = $.grep($scope.objectValueTypeList, function (e) { return e.id === data.Indicator.ObjectValueType; })[0];
        $scope.tableParams = new ngTableParams(
        {
            page: 1,
            count: 20,
            sorting: {
                Date: 'desc'
            }
        },
        {
            total: $scope.indicator.Values.length,
            counts: [],
            getData: function ($defer, params) {
                $defer.resolve($filter('orderBy')($scope.indicator.Values, params.orderBy()));
            }
        });
        originalData = angular.copy($scope.indicator.Values);
    };

    function bindGraph() {
        var graphData = graphFactory.getGraphData($scope.indicator.Values);
        $scope.series = graphData.series;
        $scope.labels = graphData.labels;
        $scope.data = graphData.data;
        $scope.colours = graphData.colours;
    }

    function resetRow(row, rowForm) {
        row.isEditing = false;
        rowForm.$setPristine();
        return _.find(originalData, function (r) {
            return r.Date === row.Date;
        });
    }

    $scope.canEdit = function (row) {
        if (!row.isEditing) {
            row.isEditing = (row.RecordValue === '' && row.TargetValue === '');
        }
        return row.isEditing;
    }

    $scope.formatDate = function (date) {
        return utils.formatGraphDate(date);
    };

    $scope.submitIndicator = function () {
        $scope.indicator.StartDate = $scope.startDate;
        $scope.indicator.ComparisonValueType = $scope.selectedComparisonValue.id;
        $scope.indicator.PeriodicityType = $scope.selectedPeriodicity.id;
        $scope.indicator.ObjectValueType = $scope.selectedObjectValue.id;
        indicatorsApi.update({ id: $scope.indicator.Id }, $scope.indicator).$promise
            .then(function () {
                toaster.success({ body: 'Indicator successfully saved!' });
            })
            .catch(function () {
                toaster.error({ body: 'Indicator successfully saved!' });
            })
    };

    $scope.addRow = function () {
        var lastIndicatorValue = _.last($scope.indicator.Values);
        var newDate = new Date(lastIndicatorValue.Date);
        newDate.setMonth(newDate.getMonth() + 1);
        var newIndicatorValue = {
            Date : newDate.toDateString(),
            Id: '',
            IndicatorId: lastIndicatorValue.IndicatorId,
            RecordValue: '',
            TargetValue: ''
        };
        $scope.indicator.Values.push(newIndicatorValue);
        $scope.tableParams.reload();
        bindGraph();
    };

    $scope.updateRow = function (row, rowForm) {
        var originalRow = resetRow(row, rowForm);
        if (originalRow != undefined) {
            angular.extend(originalRow, row);
        }
        else {
            originalData = angular.copy($scope.indicator.Values);
        }
        
        bindGraph();
    };

    $scope.cancel = function(row, rowForm) {
        var originalRow = resetRow(row, rowForm);
        if (originalRow != undefined) {
            angular.extend(row, originalRow);
        }
        else {
            _.remove($scope.indicator.Values, function (item) {
                return row.Date === item.Date;
            });
            $scope.tableParams.reload();
            bindGraph();
        }
    }

    $scope.deleteRow = function (row) {
        _.remove($scope.indicator.Values, function (item) {
            return row.Date === item.Date;
        });
        originalData = angular.copy($scope.indicator.Values);
        $scope.tableParams.reload();
        bindGraph();
    };

    init();
});