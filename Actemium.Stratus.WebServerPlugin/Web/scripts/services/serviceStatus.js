'use strict';

angular
    .module('serviceStatus', ['ngResource'])
    .factory('ThirdParty', ['$resource', function ($resource) {
        return $resource('/api/thirdparty',{},{});
    }])
    .factory('Plugins', ['$resource', function ($resource) {
        return $resource('/api/plugins',{},{});
    }]);