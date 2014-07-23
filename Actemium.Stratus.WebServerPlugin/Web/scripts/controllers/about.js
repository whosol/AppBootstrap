'use strict';

angular
    .module('resultsExplorer')
    .controller('AboutController', ['$scope', 'ThirdParty', 'Plugins', function ($scope, ThirdParty, Plugins) {
        $scope.pluginContainer = Plugins.query();
        $scope.thirdPartyContainer = ThirdParty.query();
    }]);