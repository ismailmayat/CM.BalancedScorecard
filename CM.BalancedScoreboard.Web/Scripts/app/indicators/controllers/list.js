indicatorsApp.controller('indicatorsListCtrl', function ($scope, indicatorsApi) {
    $scope.filter = '';

    $scope.onEnter = function(event) {
        if (event.charCode === 13) {
            $scope.indicators = indicatorsApi.query({ filter: $scope.filter });
        }
    };
});