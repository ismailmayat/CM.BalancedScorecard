indicatorsApp.directive('myIndicatorCard', function() {
    var controller = ['$scope', '$animate', '$location', 'indicatorsApi', 'graphFactory', function ($scope, $animate, $location, indicatorsApi, graphFactory) {

        function bindGraph() {
            indicatorsApi.get({ id: $scope.indicator.Id }).$promise
                .then(function (data) {
                    if (data.Indicator.Values.length > 0) {
                        $scope.showingPanel = true;
                        var graphData = graphFactory.getGraphData(data.Indicator.Values);
                        $scope.colours = graphData.colours;
                        $scope.series = graphData.series;
                        $scope.labels = graphData.labels;
                        $scope.data = graphData.data;
                    }
                })
                .catch(function (msg) {
                    console.error(msg);
                });
        };

        function init() {
            $scope.showingPanel = false;
            initGraph();
        };

        function initGraph() {
            $scope.labels = [];
            $scope.series = [];
            $scope.data = [[], []];
            $scope.colours = [];
        };

        $scope.showPanel = function () {
            if ($scope.showingPanel) {
                $scope.showingPanel = false;
                initGraph();
            } else {
                bindGraph();
            }
        };

        $scope.navigateToDetails = function () {
            $location.path('/details/' + $scope.indicator.Id);
        }

        init();
    }];

    return {
        scope:{
            indicator: '='
        },
        restrict: 'E',
        templateUrl: '/Scripts/app/indicators/views/card.html',
        controller: controller
    }
});