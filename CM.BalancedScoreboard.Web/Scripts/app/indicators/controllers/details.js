indicatorsApp.controller('indicatorsDetailsCtrl', function ($scope, $routeParams, indicatorsApi, $filter, utils, graphFactory, ngTableParams, toaster) {
    //var originalData = [];

    //function getIndicator() {
    //    return indicatorsApi.get({ id: $routeParams.indicatorId });
    //}

    //function init() {
    //    getIndicator().$promise
    //        .then(function (data) {
    //            assignScope(data);
    //            $scope.tableParams = getTableParams();
    //            bindGraph();
    //        })
    //        .catch(function (msg) {
    //            console.error(msg);
    //        });
    //}

    //function update() {
    //    getIndicator().$promise
    //        .then(function (data) {
    //            assignScope(data);
    //            $scope.tableParams.reload();
    //            bindGraph();
    //        })
    //        .catch(function (msg) {
    //            console.error(msg);
    //        });
    //}

    //function assignScope(data) {
    //    $scope.indicator = data.Indicator;
    //    $scope.comparisonValueTypeList = data.ComparisonValueTypeList;
    //    $scope.periodicityTypeList = data.PeriodicityTypeList;
    //    $scope.objectValueTypeList = data.ObjectValueTypeList;
    //    $scope.splitTypeList = data.SplitTypeList;
    //    $scope.startDate = new Date(data.Indicator.StartDate);
    //    $scope.selectedComparisonValue = $.grep($scope.comparisonValueTypeList, function (e) { return e.id === data.Indicator.ComparisonValueType; })[0];
    //    $scope.selectedPeriodicity = $.grep($scope.periodicityTypeList, function (e) { return e.id === data.Indicator.PeriodicityType; })[0];
    //    $scope.selectedObjectValue = $.grep($scope.objectValueTypeList, function (e) { return e.id === data.Indicator.ObjectValueType; })[0];
    //    originalData = angular.copy($scope.indicator.Values);
    //}

    //function getTableParams() {
    //    return new ngTableParams(
    //    {
    //        page: 1,
    //        count: 20,
    //        sorting: {
    //            Date: 'desc'
    //        }
    //    },
    //    {
    //        total: $scope.indicator.Values.length,
    //        counts: [],
    //        getData: function ($defer, params) {
    //            $defer.resolve($filter('orderBy')($scope.indicator.Values, params.orderBy()));
    //        }
    //    });
    //}

    //function bindGraph() {
    //    var graphData = graphFactory.getGraphData($scope.indicator.Values);
    //    $scope.series = graphData.series;
    //    $scope.labels = graphData.labels;
    //    $scope.data = graphData.data;
    //    $scope.colours = graphData.colours;
    //}

    //function resetRow(row, rowForm) {
    //    row.isEditing = false;
    //    rowForm.$setPristine();
    //    return _.find(originalData, function (r) {
    //        return r.Date === row.Date;
    //    });
    //}

    //function assignModel() {
    //    $scope.indicator.StartDate = $scope.startDate;
    //    $scope.indicator.ComparisonValueType = $scope.selectedComparisonValue.id;
    //    $scope.indicator.PeriodicityType = $scope.selectedPeriodicity.id;
    //    $scope.indicator.ObjectValueType = $scope.selectedObjectValue.id;
    //}

    //$scope.canEdit = function (row) {
    //    if (!row.isEditing) {
    //        row.isEditing = (row.RecordValue === '' && row.TargetValue === '');
    //    }

    //    return row.isEditing;
    //}

    //$scope.formatDate = function (date) {
    //    return utils.formatGraphDate(date);
    //}

    //$scope.submitIndicator = function () {
    //    assignModel();
    //    indicatorsApi.update({ id: $scope.indicator.Id }, $scope.indicator).$promise
    //        .then(function () {
    //            update();
    //            toaster.success({ body: 'Indicator successfully saved!' });
    //        })
    //        .catch(function () {
    //            toaster.error({ body: 'Indicator successfully saved!' });
    //        })
    //}

    

    //$scope.editRow = function (row) {
    //    $scope.globalEdit = true;
    //    row.isEditing = true;
    //}

    //$scope.updateRow = function (row, rowForm) {
    //    $scope.globalEdit = false;
    //    var originalRow = resetRow(row, rowForm);
    //    if (originalRow != undefined) {
    //        angular.extend(originalRow, row);
    //    }
    //    else {
    //        originalData = angular.copy($scope.indicator.Values);
    //    }

    //    bindGraph();
    //}

    //$scope.cancelRow = function (row, rowForm) {
    //    $scope.globalEdit = false;
    //    var originalRow = resetRow(row, rowForm);
    //    if (originalRow != undefined) {
    //        angular.extend(row, originalRow);
    //    }
    //    else {
    //        _.remove($scope.indicator.Values, function (item) {
    //            return row.Date === item.Date;
    //        });
    //        $scope.tableParams.reload();
    //        bindGraph();
    //    }
    //}

    

    //init();

    var originalData = [];

    function isNewRow(item) {
        return item.Id === '';
    };

    function resetRow(row, rowForm) {
        row.isEditing = false;
        rowForm.$setPristine();
        var originalRow = _.find(originalData, function (r) {
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
            IndicatorId: $routeParams.indicatorId,
            RecordValue: '',
            TargetValue: ''
        };
    }

    function initTable() {
        $scope.tableParams = new ngTableParams(
        {
            page: 1,
            count: 20,
            sorting: {
                Date: 'desc'
            }
        },
        {
            total: $scope.indicatorMeasures.length,
            counts: [],
            getData: function ($defer, params) {
                $defer.resolve($filter('orderBy')($scope.indicatorMeasures, params.orderBy()));
            }
        });
    }

    function updateTable() {
        $scope.tableParams.reload();
    }

    function bindGraph() {
        var graphData = graphFactory.getGraphData($scope.indicatorMeasures);
        $scope.series = graphData.series;
        $scope.labels = graphData.labels;
        $scope.data = graphData.data;
        $scope.colours = graphData.colours;
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

    function bindIndicator(data) {
        $scope.indicator = data.Indicator;
        $scope.comparisonValueTypeList = data.ComparisonValueTypeList;
        $scope.periodicityTypeList = data.PeriodicityTypeList;
        $scope.objectValueTypeList = data.ObjectValueTypeList;
        $scope.splitTypeList = data.SplitTypeList;
        $scope.startDate = new Date(data.Indicator.StartDate);
        $scope.selectedComparisonValue = $.grep($scope.comparisonValueTypeList, function (e) { return e.id === data.Indicator.ComparisonValueType; })[0];
        $scope.selectedPeriodicity = $.grep($scope.periodicityTypeList, function (e) { return e.id === data.Indicator.PeriodicityType; })[0];
        $scope.selectedObjectValue = $.grep($scope.objectValueTypeList, function (e) { return e.id === data.Indicator.ObjectValueType; })[0];
    }

    function loadIndicatorMeasures(callback, tableAction) {
        indicatorsApi.indicatorMeasures.query({ id: $routeParams.indicatorId }).$promise
            .then(function(data) {
                callback(data, tableAction);
            })
            .catch(function(msg) {
                toaster.error({ body: msg });
            });
    }

    function bindIndicatorMeasures(data, tableAction) {
        originalData = angular.copy(data);
        $scope.indicatorMeasures = data;
        tableAction();
        bindGraph();
    }

    function init() {
        loadIndicator(bindIndicator);
        loadIndicatorMeasures(bindIndicatorMeasures, initTable);
    }

    $scope.formatDate = function (date) {
        return utils.formatGraphDate(date);
    }

    $scope.canEdit = function (row) {
        if (!row.isEditing) {
            row.isEditing = isNewRow(row);
        }

        return row.isEditing;
    }

    $scope.deleteRow = function (row) {
        indicatorsApi.indicatorMeasures.delete({ id: $routeParams.indicatorId, measureId: row.Id }).$promise
            .then(function() {
                loadIndicatorMeasures(bindIndicatorMeasures, updateTable);
            })
            .catch(function() {
                toaster.error({ body: 'Indicator successfully deleted!' });
            });
    }

    $scope.updateRow = function(row) {
        $scope.globalEdit = false;
        row.isEditing = false;
        var promise = null;
        if (isNewRow(row)) {
            promise = indicatorsApi.indicatorMeasures.save({ id: $routeParams.indicatorId }, row).$promise;
        } else {
            promise = indicatorsApi.indicatorMeasures.update({ id: $routeParams.indicatorId, measureId: row.Id }, row).$promise;
        }

        promise
            .then(function () {
                loadIndicatorMeasures(bindIndicatorMeasures, updateTable);
            })
            .catch(function () {
                toaster.error({ body: 'Indicator successfully deleted!' });
            });
    }

    $scope.cancelRow = function (row, rowForm) {
        $scope.globalEdit = false;
        if (!isNewRow(row)) {
            resetRow(row, rowForm);
        } else {
            _.remove($scope.indicatorMeasures, function (item) {
                return isNewRow(row);
            });
        }

        bindIndicatorMeasures($scope.indicatorMeasures, updateTable);
    }

    $scope.addRow = function () {
        $scope.globalEdit = true;
        $scope.indicatorMeasures.push(createMeasure());
        bindIndicatorMeasures($scope.indicatorMeasures, updateTable);
    }

    init();
});