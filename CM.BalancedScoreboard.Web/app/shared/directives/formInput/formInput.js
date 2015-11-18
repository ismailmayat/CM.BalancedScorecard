module.exports = [
    function () {
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
            restrict: "E",
            templateUrl: "/app/shared/directives/formInput/formInput.html",
            scope:{
                config: "="
            },
            require: "ngModel",
            link: function (scope, el, attrs, ngModelController) {
                var div = el.find("div");
                var input = el.find("input");
                var p = el.find("p");;

                
                ngModelController.$render = function () {
                    input.val(ngModelController.$viewValue);
                };

                input.bind("keyup", function () {
                    ngModelController.$setViewValue(input.val());
                    ngModelController.$render();
                    if (input.attr("required") && input.val().length === 0) {
                        ngModelController.$setValidity('required', false);
                    }
                    else {
                        ngModelController.$setValidity('required', true);
                    }
                });

                input.bind("blur", function () {
                    div.toggleClass("has-error", ngModelController.$invalid);
                    div.toggleClass("has-success", ngModelController.$valid && ngModelController.$dirty);
                    p.toggleClass("ng-show", ngModelController.$invalid);
                    p.toggleClass("ng-hide", ngModelController.$valid);
                    if (ngModelController.$invalid) {
                        p.text(getErrorMessage(ngModelController));
                    }
                });

                scope.$watch('config', function (config) {
                    if (config !== undefined) {
                        assignAttributes(input, config);
                    }
                });
            }
        }

        function assignAttributes(input, config) {
            if (config.Required != undefined) {
                input.attr("required", "true");
            }
            if (config.MaxLength) {
                input.attr("maxlength", config.MaxLength);
            }
            if (config.Range) {
                input.attr("min", config.Range.MinValue);
                input.attr("max", config.Range.MaxValue);
            }
        }
    }
];