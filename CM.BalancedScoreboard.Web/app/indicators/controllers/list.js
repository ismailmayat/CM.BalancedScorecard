module.exports = ["$scope", "$animate", "$location", "$anchorScroll", "indicatorsApi", "indicatorsGraphFactory", "toaster",

function ($scope, $animate, $location, $anchorScroll, indicatorsApi, indicatorsGraphFactory, toaster)
{
    function loadMeasures(indicatorId, callback) {
        indicatorsApi.indicatorMeasures.query({ id: indicatorId }).$promise
            .then(function(response) {
                callback(indicatorId, response);
            })
           .catch(function () {
               toaster.error({ body: "An error ocurred while trying to load the measures of the selected indicator" });
           });
    }

    function loadIndicators() {
        $scope.indicators = indicatorsApi.indicators.query({ filter: $scope.filter }).$promise
            .then(function(response) {
                $scope.indicators = response.Data;
            })
            .catch(function () {
                toaster.error({ body: "An error ocurred while trying to load the indicators" });
            });
    }

    function initGraph() {
        $scope.graphLabels = undefined;
        $scope.graphSeries = undefined;
        $scope.graphData = undefined;
        $scope.graphColours = undefined;
    }

    function bindGraph(data) {
        var firstYear = _.first(data);
        var graphData = indicatorsGraphFactory.getGraphData(firstYear.Measures);
        $scope.graphColours = graphData.colours;
        $scope.graphSeries = graphData.series;
        $scope.graphLabels = graphData.labels;
        $scope.graphData = graphData.data;
    }

    function loadMeasuresCallback(indicatorId, response) {
        if (response.Data.length > 0) {
            bindGraph(response.Data);
        } else {
            initGraph();
        }
        $scope.showingIndicator = indicatorId;
    }

    $scope.onEnter = function (event) {
        if (event.charCode === 13) {
            loadIndicators();
        }
    };

    $scope.search = function() {
        loadIndicators();
    }

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

    $scope.showMeasures = function (indicatorId, anchor) {
        if ($scope.showingIndicator === undefined) {
            loadMeasures(indicatorId, loadMeasuresCallback);
            //var hash = "indicator" + anchor;
            //if ($location.hash() !== hash) {
            //    // set the $location.hash to `newHash` and
            //    // $anchorScroll will automatically scroll to it
            //    $location.hash(hash);
            //} else {
            //    $anchorScroll();
            //}
        } else {
            if ($scope.showingIndicator === indicatorId) {
                $scope.showingIndicator = undefined;
                initGraph();
                //$anchorScroll();
            }
        }
    }

    $scope.showingMeasures = function (indicatorId) {
        return $scope.showingIndicator === indicatorId;
    }

    $scope.showingGraph = function() {
        return $scope.graphData !== undefined;
    }

    $scope.showingLegend = function () {
        return $scope.indicators !== undefined && $scope.indicators.length > 0;
    }
}];




    