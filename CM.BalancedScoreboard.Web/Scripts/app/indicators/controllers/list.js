indicatorsApp.controller('indicatorsListCtrl', function ($scope, indicatorsApi) {
    $scope.filter = '';
    $scope.selectedIndicatorId = '';

    $scope.onEnter = function(event) {
        if (event.charCode == 13) {
            $scope.indicators = indicatorsApi.query({ filter: $scope.filter });
        }
    };

    $scope.fetchValues = function(indicator) {
        if ($scope.selectedIndicatorId !== indicator.Id) {
            $scope.selectedIndicator = indicatorsApi.get({ id: indicator.Id });
            $scope.selectedIndicatorId = indicator.Id;
        }
    };

    $scope.hideValuesPanel = function(indicator) {
        return !($scope.selectedIndicatorId === indicator.Id);
    };

    $scope.labels = ["January", "February", "March", "April", "May", "June", "July"];
    $scope.series = ['Series A', 'Series B'];
    $scope.data = [
      [65, 59, 80, 81, 56, 55, 40],
      [28, 48, 40, 19, 86, 27, 90]
    ];
    $scope.onClick = function (points, evt) {
        console.log(points, evt);
    };
});