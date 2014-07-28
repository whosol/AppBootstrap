﻿'use strict';

angular
    .module('resultsExplorer')
    .controller('NavController', ['$scope', '$location', function ($scope, $location) {

        $scope.isCollapsed = true;

        $scope.$on('$routeChangeSuccess', function () {
            $scope.isCollapsed = true;
        });

        $scope.getClass = function (path) {
            if ($location.path().substr(0, path.length) === path) {
                return "active";
            }
            else {
                return "";
            }
        };
    }]);
