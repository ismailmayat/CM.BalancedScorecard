indicatorsApp.factory('graphFactory', function ($filter, utils, configuration, ngTableParams) {
    return {
        getGraphData: function (indicatorValues) {
            return {
                colours: getIndicatorGraphColours(),
                series: getIndicatorGraphSeriesNames(),
                labels: getIndicatorGraphLabels(indicatorValues),
                data: getIndicatorGraphValues(indicatorValues)
            }
        },
        getGraphConfig: function (indicatorValues) {
            return new ngTableParams(
        {
            page: 1,
            count: 12,
            sorting: {
                Date: 'desc'
            }
        },
        {
            total: indicatorValues.Count,
            counts: [],
            getData: function ($defer, params) {
                $defer.resolve($filter('orderBy')(indicatorValues, params.orderBy()));
            }
        });
        }
    };

    function getIndicatorGraphColours() {
        return [{
            fillColor: '#00868B'
        }, {
            fillColor: '#FF7216'
        }];
    };

    function getIndicatorGraphSeriesNames() {
        return ['Record Value', 'Target Value'];
    };

    function getIndicatorGraphLabels(indicatorValues) {
        var labels = [];
        for (index = 0; index < indicatorValues.length; ++index) {
            var indicatorValue = indicatorValues[index];
            labels.push(utils.formatGraphDate(indicatorValue.Date));
        }
        return labels;
    };

    function getIndicatorGraphValues(indicatorValues) {
        var data = [[], []];
        for (index = 0; index < indicatorValues.length; ++index) {
            var indicatorValue = indicatorValues[index];
            data[0].push(indicatorValue.RecordValue);
            data[1].push(indicatorValue.TargetValue);
        }
        return data;
    };
});