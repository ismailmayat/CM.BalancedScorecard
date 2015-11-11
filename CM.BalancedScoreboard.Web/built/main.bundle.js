/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};

/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {

/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;

/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};

/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);

/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;

/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}


/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;

/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;

/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";

/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ function(module, exports, __webpack_require__) {

	__webpack_require__(2);
	__webpack_require__(1);

	angular.module("app", ["indicators"])
	    .config([
	        "$routeProvider",
	        function($routeProvider) {
	            $routeProvider.
	                when("Indicators/List", {
	                    templateUrl: "./indicators/views/list.html",
	                    controller: "indicatorsListCtrl"
	                }).
	                when("Indicators/Details/:indicatorId", {
	                    templateUrl: "./indicators/views/details.html",
	                    controller: "indicatorsDetailsCtrl"
	                }).
	                otherwise({
	                    redirectTo: "Indicators/List"
	                });
	        }
	    ]);

/***/ },
/* 1 */
/***/ function(module, exports, __webpack_require__) {

	__webpack_require__(3);

	angular.module("indicators", []);



/***/ },
/* 2 */
/***/ function(module, exports) {

	var shared = angular.module('shared', []);

/***/ },
/* 3 */
/***/ function(module, exports, __webpack_require__) {

	__webpack_require__(4);

	angular.module("indicators")
	    .controller("indicatorsListCtrl", function() {});

/***/ },
/* 4 */
/***/ function(module, exports) {

	angular.module("indicators")
	    .factory("indicatorsApi", []);

/***/ }
/******/ ]);