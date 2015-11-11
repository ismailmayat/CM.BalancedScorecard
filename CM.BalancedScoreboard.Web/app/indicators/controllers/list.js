require("../services/api");
require("../services/graph");

angular.module("indicators").controller('indicatorsListCtrl', function ($scope, $animate, $location, indicatorsApi, graphFactory) {

    function loadMeasures(indicatorId, callback) {
        indicatorsApi.indicatorMeasures.query({ id: indicatorId }).$promise
           .then(function (response) {
               callback(response);
           })
           .catch(function () {
               toaster.error({ body: "An error ocurred while trying to load the measures of the selected indicator" });
           });
    }

    function initGraph() {
        $scope.labels = [];
        $scope.series = [];
        $scope.data = [[], []];
        $scope.colours = [];
    }

    function bindGraph(response) {
        if (response.Data.length > 0) {
            var firstYear = _.first(response.Data);
            var graphData = graphFactory.getGraphData(firstYear.Measures);
            $scope.colours = graphData.colours;
            $scope.series = graphData.series;
            $scope.labels = graphData.labels;
            $scope.data = graphData.data;
        }
    }

    $scope.onEnter = function (event) {
        if (event.charCode === 13) {
            $scope.indicators = indicatorsApi.indicators.query({ filter: $scope.filter });
        }
    };

    $scope.navigateToDetails = function (indicatorId) {
        $location.path('/Indicators/Details/' + indicatorId);
    }

    $scope.getIndicatorStateClass = function (indicator) {
        switch (indicator.State) {
            case 0:
                return 'panel-default';
            case 1:
                return 'panel-success';
            case 2:
                return 'panel-warning';
            case 3:
                return 'panel-danger';
            default:
                return 'panel-default';
        }
    }

    $scope.showGraph = function (indicatorId) {
        if ($scope.showingIndicator === undefined) {
            $scope.showingIndicator = indicatorId;
            loadMeasures(indicatorId, bindGraph);
        } else {
            if ($scope.showingIndicator === indicatorId) {
                $scope.showingIndicator = undefined;
                initGraph();
            }
        }
    }

    $scope.showingPanel = function (indicatorId) {
        return $scope.showingIndicator === indicatorId;
    }

    $scope.showLegend = function () {
        return $scope.indicators !== undefined && $scope.indicators.length > 0;
    }
}); 