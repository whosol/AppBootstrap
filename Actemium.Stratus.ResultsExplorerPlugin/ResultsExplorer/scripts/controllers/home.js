'use strict';

angular
    .module('resultsExplorer')
    .controller('HomeController', ['$scope', function ($scope) {

        $scope.interval = 5000;

        $scope.slides = [
            {
                image: 'images/products-Vi-Roam.gif',
                title: 'Vi-Roam',
                text: "An all-in-one ultra rugged hand held test solution capable of surviving the rigours of today's production environment.",
            },
            {
                image: 'images/products-Vi-Mini.gif',
                title: 'Vi-Mini',
                text: "  A compact, low cost, multi protocol/channel vehicle interface dongle designed specifically for the aftermarket. It plugs directly into a standard J1962 OBD socket communicating with a host Laptop/Netbook device via USB connection.",
            }
        ];
    }]);