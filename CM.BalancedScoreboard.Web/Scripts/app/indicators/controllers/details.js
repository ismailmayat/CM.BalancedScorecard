indicatorsApp.controller('indicatorsDetailsCtrl', function ($scope, $routeParams, indicatorsApi) {
    $scope.indicator = null;

    $scope.submitIndicator = function () {
        indicatorsApi.update({ id: $scope.indicator.Id }, $scope.indicator);
        //indicatorsApi.save($scope.indicator);
    }

    function init() {
        indicatorsApi.get({ id: $routeParams.indicatorId }).$promise
            .then(function (data) {
                $scope.indicator = data;
            })
            .catch(function (msg) {
                console.error(msg);
            });
    };

    init();
});