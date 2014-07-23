'use strict';

angular
    .module('stratusRepository', ['ngResource'])
    .factory('Cells', ['$resource', function ($resource) {
        return $resource('/api/cells/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }])
    .factory('Companies', ['$resource', function ($resource) {
        return $resource('/api/companies/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }])
    .factory('Locations', ['$resource', function ($resource) {
        return $resource('/api/locations/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }])
    .factory('Plants', ['$resource', function ($resource) {
        return $resource('/api/plants/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }])
    .factory('Processes', ['$resource', function ($resource) {
        return $resource('/api/processes/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }])
    .factory('Products', ['$resource', function ($resource) {
        return $resource('/api/products/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }])
    .factory('ProductTypes', ['$resource', function ($resource) {
        return $resource('/api/producttypes/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }])
    .factory('Results', ['$resource', function ($resource) {
        return $resource('/api/results/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }])
    .factory('Sequences', ['$resource', function ($resource) {
        return $resource('/api/sequences/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }])
    .factory('Testers', ['$resource', function ($resource) {
        return $resource('/api/testers/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }])
    .factory('Visits', ['$resource', function ($resource) {
        return $resource('/api/visits/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }])
    .factory('Zones', ['$resource', function ($resource) {
        return $resource('/api/zones/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }]);