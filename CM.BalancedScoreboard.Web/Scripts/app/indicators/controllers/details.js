indicatorsApp.controller('indicatorsDetailsCtrl', function ($scope, $routeParams, indicatorsApi, $filter, utils, graphFactory) {
    $scope.formatDate = function (date) {
        return utils.formatGraphDate(date);
    };
    $scope.submitIndicator = function () {
        $scope.indicator.StartDate = $scope.startDate;
        $scope.indicator.ComparisonValueType = $scope.selectedComparisonValue.id;
        $scope.indicator.PeriodicityType = $scope.selectedPeriodicity.id;
        $scope.indicator.ObjectValueType = $scope.selectedObjectValue.id;
        indicatorsApi.update({ id: $scope.indicator.Id }, $scope.indicator);
    }

    function init() {
        $scope.colours = [];
        $scope.series = [];
        $scope.labels = [];
        $scope.data = [[],[]];
        indicatorsApi.get({ id: $routeParams.indicatorId }).$promise
            .then(function (data) {
                assignValues(data);
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
        if (data.Indicator.Values.length > 0) {
            $scope.tableParams = graphFactory.getGraphConfig(data.Indicator.Values);
            var graphData = graphFactory.getGraphData(data.Indicator.Values);
            $scope.colours = graphData.colours;
            $scope.series = graphData.series;
            $scope.labels = graphData.labels;
            $scope.data = graphData.data;
        }
    };

    init();
});