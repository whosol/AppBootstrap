'use strict';

angular
    .module('resultsExplorer')
    .factory('FilterData', ['$rootScope', function ($rootScope) {
        var service = {}
        service.filters = {};

        service.updateFilters = function (value) {
            service.filters = value;
            $rootScope.$broadcast("filtersUpdated")
        };

        return service;
    }])