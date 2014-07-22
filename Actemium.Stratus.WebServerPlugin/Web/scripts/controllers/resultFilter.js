'use strict';

angular.module('resultsExplorer')
    .controller('ResultFilterController', ['$scope', function ($scope) {

        $scope.visible = false;

        $scope.fromDate = new Date();

        $scope.toDate = new Date();

        $scope.productTypes = {
            transport: {
                read: '/api/productTypes'
            },
            schema: {
                data: "ProductTypes"
            }
        };

        $scope.products = {
            transport: {
                read: '/api/products'
            },
            schema: {
                data: "Products"
            }
        };

        $scope.sequences = {
            transport: {
                read: '/api/sequences'
            },
            schema: {
                data: "Sequences"
            }
        };

        $scope.plants = {
            transport: {
                read: '/api/plants'
            },
            schema: {
                data: "Plants"
            }
        };

        $scope.processes = {
            transport: {
                read: '/api/processes'
            },
            schema: {
                data: "Processes"
            }
        };

        $scope.locations = {
            transport: {
                read: '/api/locations'
            },
            schema: {
                data: "Locations"
            }
        };

        $scope.zones = {
            transport: {
                read: '/api/zones'
            },
            schema: {
                data: "Zones"
            }
        };

        $scope.cells = {
            transport: {
                read: '/api/cells'
            },
            schema: {
                data: "Cells"
            }
        };

        $scope.$on("kendoRendered", function () {
            $scope.visible = true;
        });
    }]);
