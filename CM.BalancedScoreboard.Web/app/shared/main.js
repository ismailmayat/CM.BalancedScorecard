angular.module("shared", []);

angular.module("shared").factory("utils", require("./services/utils"));
angular.module("shared").factory("configuration", require("./services/configuration"));
angular.module("shared").directive("showErrors", require("./directives/validation"));
angular.module("shared").directive("formInput", require("./directives/formInput/formInput"));