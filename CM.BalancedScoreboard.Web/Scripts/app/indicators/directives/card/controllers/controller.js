var myIndicatorCardController = ['$scope', '$animate', '$location', 'indicatorsApi', 'graphFactory', 'toaster', function ($scope, $animate, $location, indicatorsApi, graphFactory, toaster) {  

    function loadIndicatorMeasures(callback) {
        indicatorsApi.indicatorMeasures.query({ id: $scope.indicator.Id }).$promise
            .then(function (data) {
                callback(data);
            })
            .catch(function () {
                toaster.error({body: "An error ocurred while trying to load the measures of the selected indicator"});
            });
    }

    function initGraph() {
        $scope.labels = [];
        $scope.series = [];
        $scope.data = [[], []];
        $scope.colours = [];
    };

    function bindGraph(data) {
        var graphData = graphFactory.getGraphData(data);
        $scope.colours = graphData.colours;
        $scope.series = graphData.series;
        $scope.labels = graphData.labels;
        $scope.data = graphData.data;
    }

    function init() {
        $scope.showingPanel = false;
        initGraph();
    };

    $scope.showGraph = function () {
        $scope.showingPanel = true;
        loadIndicatorMeasures(bindGraph);
    }

    $scope.hideGraph = function () {
        $scope.showingPanel = false;
        initGraph();
    }

    $scope.action = function() {
        if ($scope.showingPanel) {
            $scope.hideGraph();
        } else {
            $scope.showGraph();
        }
    }

    $scope.navigateToDetails = function () {
        $location.path('/Details/' + $scope.indicator.Id);
    }

    init();
}];