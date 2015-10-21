indicatorsApp.controller('indicatorsDetailsCtrl', function ($scope, $routeParams, indicatorsApi, $filter, ngTableParams) {
    $scope.colours = [{
        fillColor: '#0000FF',
        strokeColor: '#0000FF',
        highlightFill: '#ffd079',
        highlightStroke: '#0000FF'
    }, {
        fillColor: '#FFA500',
        strokeColor: '#FFA500',
        highlightFill: '#FFA500',
        highlightStroke: '#FFA500'
    }];

    $scope.series = ['Record Value', 'Target Value'];

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
        if (data.Indicator.Values.length > 0) {
            for (index = 0; index < data.Indicator.Values.length; ++index) {
                var indicatorValue = data.Indicator.Values[index];
                $scope.labels.push($scope.formatDate(indicatorValue.Date));
                $scope.data[0].push(indicatorValue.RecordValue);
                $scope.data[1].push(indicatorValue.TargetValue);
            }
            for (index = 0; index < data.Indicator.Values.length; ++index) {
                var indicatorValue = data.Indicator.Values[index];
                $scope.labels.push($scope.formatDate(indicatorValue.Date));
                $scope.data[0].push(indicatorValue.RecordValue);
                $scope.data[1].push(indicatorValue.TargetValue);
            }
            for (index = 0; index < data.Indicator.Values.length; ++index) {
                var indicatorValue = data.Indicator.Values[index];
                $scope.labels.push($scope.formatDate(indicatorValue.Date));
                $scope.data[0].push(indicatorValue.RecordValue);
                $scope.data[1].push(indicatorValue.TargetValue);
            }
            for (index = 0; index < data.Indicator.Values.length; ++index) {
                var indicatorValue = data.Indicator.Values[index];
                $scope.labels.push($scope.formatDate(indicatorValue.Date));
                $scope.data[0].push(indicatorValue.RecordValue);
                $scope.data[1].push(indicatorValue.TargetValue);
            }
        }
    };

    init();
});