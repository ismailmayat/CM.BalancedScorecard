angular.module('shared').directive('showErrors', function () {
    function getErrorMessage(input) {
        if (input.$error.required) {
            return "This field is required";
        }
        if (input.$error.pattern) {
            return "This field is incorrect";
        }
        return "";
    }

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
                if (ctrl[inputName].$invalid) {
                    help[0].innerText = getErrorMessage(ctrl[inputName]);
                }
            });
        }
    }
});

