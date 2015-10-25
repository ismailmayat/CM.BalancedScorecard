indicatorsApp.factory('graphFactory', function ($filter, utils, configuration) {
    return {
        getGraphData: function (indicatorValues) {
            return {
                series: getIndicatorGraphSeriesNames(),
                labels: getIndicatorGraphLabels(indicatorValues),
                data: getIndicatorGraphValues(indicatorValues),
                colours: getIndicatorGraphColours()
            }
        },
        getGraphOptions: function () {
            bezierCurve: false
        }
    };

    function getIndicatorGraphColours() {
        return ['#00868B', '#FF7216'];
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