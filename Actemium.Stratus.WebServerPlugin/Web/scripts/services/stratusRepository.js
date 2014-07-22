'use strict';

angular
    .module('stratusRepository', ['ngResource'])
    .factory('Cells', ['$resource', function ($resource) {
        return $resource('/api/cells/:id',{id:'@id'},{});
    }])
    .factory('Companies', ['$resource', function ($resource) {
        return $resource('/api/companies/:id',{id:'@id'},{});
    }])
    .factory('Locations', ['$resource', function ($resource) {
        return $resource('/api/locations/:id',{id:'@id'},{});
    }])
    .factory('Plants', ['$resource', function ($resource) {
        return $resource('/api/plants/:id',{id:'@id'},{});
    }])
    .factory('Processes', ['$resource', function ($resource) {
        return $resource('/api/processes/:id',{id:'@id'},{});
    }])
    .factory('Products', ['$resource', function ($resource) {
        return $resource('/api/products/:id',{id:'@id'},{});
    }])
    .factory('ProductTypes', ['$resource', function ($resource) {
        return $resource('/api/producttypes/:id',{id:'@id'},{});
    }])
    .factory('Results', ['$resource', function ($resource) {
        return $resource('/api/results/:id',{id:'@id'},{});
    }])
    .factory('Sequences', ['$resource', function ($resource) {
        return $resource('/api/sequences/:id',{id:'@id'},{});
    }])
    .factory('Testers', ['$resource', function ($resource) {
        return $resource('/api/testers/:id',{id:'@id'},{});
    }])
    .factory('Visits', ['$resource', function ($resource) {
        return $resource('/api/visits/:id',{id:'@id'},{});
    }])
    .factory('Zones', ['$resource', function ($resource) {
        return $resource('/api/zones/:id',{id:'@id'},{});
    }]);