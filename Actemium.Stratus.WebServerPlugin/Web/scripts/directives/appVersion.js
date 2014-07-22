'use strict';

angular.module('resultsExplorer').
    directive('appVersion', ['version', function (version) {
        return function (scope, elm, attrs) {
            elm.text(version);
        }
    }]);