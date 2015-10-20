indicatorsApp.controller('indicatorsDetailsCtrl', function ($scope, $routeParams, indicatorsApi) {
    $scope.submitIndicator = function () {
        $scope.indicator.ComparisonValueType = $scope.selectedComparisonValue.id;
        $scope.indicator.PeriodicityType = $scope.selectedPeriodicity.id;
        $scope.indicator.ObjectValueType = $scope.selectedObjectValue.id;
        indicatorsApi.update({ id: $scope.indicator.Id }, $scope.indicator);
        //indicatorsApi.save($scope.indicator);
    }

    function init() {
        indicatorsApi.get({ id: $routeParams.indicatorId }).$promise
            .then(function (data) {
                $scope.indicator = data.Indicator;
                $scope.comparisonValueTypeList = data.ComparisonValueTypeList;
                $scope.periodicityTypeList = data.PeriodicityTypeList;
                $scope.objectValueTypeList = data.ObjectValueTypeList;
                $scope.splitTypeList = data.SplitTypeList;
                $scope.selectedComparisonValue = $.grep($scope.comparisonValueTypeList, function (e) { return e.id === data.Indicator.ComparisonValueType; })[0];
                $scope.selectedPeriodicity = $.grep($scope.periodicityTypeList, function (e) { return e.id === data.Indicator.PeriodicityType; })[0];
                $scope.selectedObjectValue = $.grep($scope.objectValueTypeList, function (e) { return e.id === data.Indicator.ObjectValueType; })[0];
            })
            .catch(function (msg) {
                console.error(msg);
            });
    };

    init();
});