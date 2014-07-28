'use strict';

angular
    .module('serviceStatus', ['ngResource'])
    .factory('ThirdParty', ['$resource', function ($resource) {
        return $resource('/api/thirdparty', {}, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }])

    .factory('Plugins', ['$resource', function ($resource) {
        return $resource('/api/plugins', {}, {
            query: {
                method: 'GET',
                isArray: false
            }
        });
    }]);

