'use strict';

angular
    .module('resultsExplorer')
    .controller('AboutController', ['$scope', 'ThirdParty', 'Plugins', function ($scope, ThirdParty, Plugins) {
        $scope.plugins = Plugins.query();
        $scope.libraries = ThirdParty.query();
    }]);