'use strict';

angular
    .module('resultsExplorer')
    .directive('failurePareto', function () {
        return {
            restrict: 'E',
            templateUrl: 'views/charts/failurePareto.html'
        };
    });