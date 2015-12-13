module.exports = [
    "$scope", function($scope) {
        $scope.formControlClass = function() {
            if ($scope.config !== undefined) {
                if ($scope.config.InputType === "range")
                    return "";

                if ($scope.config.InputType === "checkbox")
                    return "";

                var cssClass = "form-control";
                cssClass += $scope.config.InputType === "number" ? " text-right" : "";

                return cssClass;
            }
            return "";
        };
    }
];