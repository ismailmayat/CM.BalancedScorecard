indicatorsApp.directive('myIndicatorCard', function() {
    var controller = ['$scope', '$animate', 'indicatorsApi', function($scope, $animate, indicatorsApi) {
        function bindGraph(indicator) {
            var recordValues = [];
            var targetValues = [];
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
            indicatorsApi.get({ id: indicator.Id }).$promise
                .then(function (data) {
                    if (data.Values.length > 0) {
                        $scope.showingPanel = true;
                        for (index = 0; index < data.Values.length; ++index) {
                            var indicatorValue = data.Values[index];
                            $scope.labels.push(indicatorValue.Date);
                            recordValues.push(indicatorValue.RecordValue);
                            targetValues.push(indicatorValue.TargetValue);
                        }
                        $scope.data.push(recordValues);
                        $scope.data.push(targetValues);
                    }
                })
                .catch(function(msg) {
                    console.error(msg);
                });
        };

        function initGraph() {
            $scope.labels = [];
            $scope.series = [];
            $scope.data = [];
        };

        function init() {
            $scope.showingPanel = false;
            initGraph();
        };

        $scope.bindPanel = function (indicator) {
            if ($scope.showingPanel) {
                $scope.showingPanel = false;
                initGraph();
            } else {
                bindGraph(indicator);
            }
        };

        init();
    }];

    return {
        restrict: 'E',
        templateUrl: '/Scripts/app/indicators/templates/card.html',
        controller: controller
    }
});