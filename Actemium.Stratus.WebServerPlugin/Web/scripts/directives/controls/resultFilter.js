'use strict';

angular
    .module('resultsExplorer')
    .directive('resultFilter', function () {
        return {
            restrict: 'E',
            templateUrl: 'views/controls/resultFilter.html'
        };
    });