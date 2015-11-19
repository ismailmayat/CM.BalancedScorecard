module.exports = [
    function () {
        function getErrorMessage(directive, config) {
            if (directive.$error.required) {
                return config.ErrorMessage.Required;
            }
            
            return "";
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

        function setValidation(input, controller) {
            if (input.attr("required")) {
                if (input.val().length === 0) {
                    controller.$setValidity('required', false);
                }
                else {
                    controller.$setValidity('required', true);
                }
            }
        }

        return {
            restrict: "E",
            templateUrl: "/app/shared/directives/formInput/formInput.html",
            scope:{
                config: "="
            },
            require: "ngModel",
            controller: ['$scope', function($scope) {
                $scope.formControlClass = function () {
                    if ($scope.config !== undefined) {
                        if ($scope.config.InputType === "range")
                            return "";

                        if ($scope.config.InputType === "checkbox")
                            return "";

                        return "form-control";
                    }
                };
            }],
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
                    setValidation(input, ngModelController);
                });

                input.bind("blur", function () {
                    div.toggleClass("has-error", ngModelController.$invalid);
                    div.toggleClass("has-success", ngModelController.$valid && ngModelController.$dirty);
                    p.toggleClass("ng-show", ngModelController.$invalid);
                    p.toggleClass("ng-hide", ngModelController.$valid);
                    if (ngModelController.$invalid) {
                        p.text(getErrorMessage(ngModelController, scope.config));
                    }
                });

                scope.$watch("config", function (config) {
                    if (config !== undefined) {
                        assignAttributes(input, config);
                    }
                });
            }
        }
    }
];