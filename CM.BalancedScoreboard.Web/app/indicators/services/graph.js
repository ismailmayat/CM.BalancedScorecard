module.exports = [
    "utils", function(utils) {
        return {
            getGraphData: function(indicatorMeasures) {
                return {
                    series: getIndicatorGraphSeriesNames(),
                    labels: getIndicatorGraphLabels(indicatorMeasures),
                    data: getIndicatorGraphValues(indicatorMeasures),
                    colours: getIndicatorGraphColours()
                }
            },
            getGraphOptions: function() {
                bezierCurve: false
            }
        };

        function getIndicatorGraphColours() {
            return ["#00868B", "#FF7216"];
        };

        function getIndicatorGraphSeriesNames() {
            return ["Real Value", "Target Value"];
        };

        function getIndicatorGraphLabels(indicatorMeasures) {
            var labels = [];
            for (index = 0; index < indicatorMeasures.length; ++index) {
                var indicatorMeasure = indicatorMeasures[index];
                labels.push(utils.formatGraphDate(indicatorMeasure.Date));
            }
            return labels;
        };

        function getIndicatorGraphValues(indicatorMeasures) {
            var data = [[], []];
            for (index = 0; index < indicatorMeasures.length; ++index) {
                var indicatorMeasure = indicatorMeasures[index];
                data[0].push(indicatorMeasure.RealValue);
                data[1].push(indicatorMeasure.TargetValue);
            }
            return data;
        }
    }
];