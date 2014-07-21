'use strict';

/* Directives */


angular.module('resultsExplorer.directives', []).
    directive('appVersion', ['version', function (version) {
        return function (scope, elm, attrs) {
            elm.text(version);
        }
    }]).
    directive('resultFilter', function () {
        var ret = {
            restrict: 'E',
            templateUrl: 'templates/resultFilter.html'
        };
        return ret;
    });