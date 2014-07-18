'use strict';

/* Directives */


angular.module('resultsExplorer.directives', []).
    directive('appVersion', ['version', function (version) {
        return function (scope, elm, attrs) {
            elm.text(version);
        }
    }]).
    directive('resultFilter', function () {
        return{
            restrict: 'E',
            templateUrl: 'templates/resultFilter.html'
        };
    });