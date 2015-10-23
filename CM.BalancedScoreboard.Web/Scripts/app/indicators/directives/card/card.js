var myIndicatorCard = indicatorsApp.directive('myIndicatorCard', function () {
    return {
        scope: {
            indicator: '='
        },
        restrict: 'E',
        templateUrl: '/Scripts/app/indicators/directives/card/views/view.html',
        controller: myIndicatorCardController
    }
});