indicatorsApp.controller('indicatorsDetailsCtrl', function ($scope, $routeParams, indicatorsApi) {
    $scope.indicator = null;

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