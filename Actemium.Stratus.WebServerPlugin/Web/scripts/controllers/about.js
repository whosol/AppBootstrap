'use strict';

angular.module('resultsExplorer')
    .controller('AboutController', ['$scope', '$http', function ($scope, $http) {
        $http.get('/api/servicestatus').success(function (data) {
            $scope.plugins = data.Plugins;
        });
        $http.get('/api/thirdparty').success(function (data) {
            $scope.libraries = data.Libraries;
        });
    }])