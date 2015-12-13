module.exports = [
    function() {
        function getErrorMessage(input) {
            if (input.$error.required) {
                return "This field is required";
            }
            return "";
        }

        return {
            restrict: "A",
            require: "^form",
            link: function (scope, el, attrs, ctrl) {
                var select = el.find("select");
                var inputName = select.attr("name");
                var help = el.find("p");

                select.bind("blur", function () {
                    el.toggleClass("has-error", ctrl[inputName].$invalid);
                    el.toggleClass("has-success", ctrl[inputName].$valid && ctrl[inputName].$dirty);
                    help.toggleClass("ng-show", ctrl[inputName].$invalid);
                    help.toggleClass("ng-hide", ctrl[inputName].$valid);
                    if (ctrl[inputName].$invalid) {
                        help[0].innerText = getErrorMessage(ctrl[inputName]);
                    }
                });

                scope.$on('show-errors-check-validity', function () {
                    el.toggleClass('has-error', ctrl[inputName].$invalid);
                    help.toggleClass("ng-show", ctrl[inputName].$invalid);
                    if (ctrl[inputName].$invalid) {
                        help[0].innerText = getErrorMessage(ctrl[inputName]);
                    }
                });
            }
        }
    }
];

