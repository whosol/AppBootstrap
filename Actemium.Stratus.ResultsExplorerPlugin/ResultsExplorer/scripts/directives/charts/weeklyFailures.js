'use strict';

angular
    .module('resultsExplorer')
    .directive('weeklyFailures', function () {
        return {
            restrict: 'E',
            templateUrl: 'views/charts/weeklyFailures.html'
        };
    });