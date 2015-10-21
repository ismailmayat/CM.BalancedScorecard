indicatorsApp.controller('indicatorsDetailsCtrl', function ($scope, $routeParams, indicatorsApi, $filter, ngTableParams) {
    $scope.submitIndicator = function () {
        $scope.indicator.StartDate = $scope.startDate;
        $scope.indicator.ComparisonValueType = $scope.selectedComparisonValue.id;
        $scope.indicator.PeriodicityType = $scope.selectedPeriodicity.id;
        $scope.indicator.ObjectValueType = $scope.selectedObjectValue.id;
        indicatorsApi.update({ id: $scope.indicator.Id }, $scope.indicator);
    }

    $scope.formatDate = function (date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('/');
    };

    function init() {
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
        $scope.tableParams = new ngTableParams(
        {
            page: 1,
            count: 12,
            sorting: {
                Date: 'desc'
            }
        },
        {
            total: data.Count,
            counts: [],
            getData: function ($defer, params) {
                $defer.resolve($filter('orderBy')(data.Indicator.Values, params.orderBy()));
            }
        });
    };

    init();
});