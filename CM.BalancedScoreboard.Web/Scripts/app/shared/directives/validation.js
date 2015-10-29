shared.directive('showErrors', function () {
    return {
        restrict: 'A',
        require: '^form',
        link: function(scope, el, attrs, ctrl) {
            var input = el[0].querySelector('[name]');
            var ngInput = angular.element(input);
            var inputName = ngInput.attr('name');

            ngInput.bind('blur', function() {
                el.toggleClass('has-error', ctrl[inputName].$invalid);
                el.toggleClass('has-success', ctrl[inputName].$valid && ctrl[inputName].$dirty);
            });
        }
    }
});

