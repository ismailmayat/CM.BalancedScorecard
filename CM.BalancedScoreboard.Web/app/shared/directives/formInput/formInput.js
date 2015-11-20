module.exports = ["utils",
    function (utils) {
        function getErrorMessage(directive, config) {
            if (directive.$error.required) {
                return config.ErrorMessage.Required;
            }      
            return "";
        }

        function setAttributes(input, config) {
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

        function setValidationState(input, controller) {
            if (input.attr("required")) {
                if (input.val().length === 0) {
                    controller.$setValidity("required", false);
                }
                else {
                    controller.$setValidity("required", true);
                }
            }
        }

        function setValidationAttributes(div, p, controller) {
            div.toggleClass("has-error", controller.$invalid);
            div.toggleClass("has-success", controller.$valid && controller.$dirty);
            p.toggleClass("ng-show", controller.$invalid);
            p.toggleClass("ng-hide", controller.$valid);
        }

        function setValidationMessages(p, scope, controller) {
            p.text(getErrorMessage(controller, scope.config));
        }

        function setInputValue(input, scope, controller) {
            if (scope.config !== undefined) {
                if (scope.config.InputType === "date") {
                    input.val(utils.formatDateInput(new Date(controller.$viewValue)));
                } else if (scope.config.InputType === "month") {
                    input.val(utils.formatMonthInput(new Date(controller.$viewValue)));
                } else {
                    input.val(controller.$viewValue);
                }
            } else {
                input.val(controller.$viewValue);
            }
        }

        return {
            restrict: "E",
            templateUrl: "/app/shared/directives/formInput/views/view.html",
            scope:{
                config: "=",
                hideLabel: "="
            },
            require: "ngModel",
            controller: require("./controllers/controller.js"),
            link: function (scope, el, attrs, modelController) {
                var div = el.find("div");
                var input = el.find("input");
                var p = el.find("p");;
           
                modelController.$render = function () {
                    setInputValue(input, scope, modelController);
                };

                input.bind("keypress", function () {
                    modelController.$setViewValue(input.val());
                    modelController.$render();
                    setValidationState(input, modelController);
                });

                input.bind("blur", function () {
                    modelController.$setViewValue(input.val());
                    modelController.$render();
                    setValidationState(input, modelController);

                    setValidationAttributes(div, p, modelController);
                    if (modelController.$invalid) {
                        setValidationMessages(p, scope, modelController);
                    }
                });

                scope.$watch("config", function (config) {
                    if (config !== undefined) {
                        setAttributes(input, config);
                    }
                });
            }
        }
    }
];