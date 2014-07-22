'use strict';

angular.module('resultsExplorer')
    .controller('HomeController', ['$scope', function ($scope) {
        $('.carousel').carousel({
            interval: 15000
        });
    }])