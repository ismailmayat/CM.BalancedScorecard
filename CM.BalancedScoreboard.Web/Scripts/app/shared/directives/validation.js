shared.directive('showErrors', function () {
    return {
        restrict: 'A',
        require: '^form',
        link: function(scope, el, attrs, ctrl) {
            var input = el.find('input');
            var inputName = input.attr('name');
            var help = el.find('p');

            input.bind('blur', function () {
                el.toggleClass('has-error', ctrl[inputName].$invalid);
                el.toggleClass('has-success', ctrl[inputName].$valid && ctrl[inputName].$dirty);
                help.toggleClass('ng-show', ctrl[inputName].$invalid);
                help.toggleClass('ng-hide', ctrl[inputName].$valid);
            });
        }
    }
});

