'use strict';

angular.module('resultsExplorer').
    directive('resultFilter', function () {
        var ret = {
            restrict: 'E',
            templateUrl: 'views/resultFilter.html'
        };
        return ret;
    });