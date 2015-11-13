angular.module("shared", []);

angular.module("shared").factory("utils", require("./services/utils"));
angular.module("shared").factory("configuration", require("./services/configuration"));
angular.module("shared").factory("directives", require("./directives/validation"));