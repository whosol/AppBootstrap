'use strict';

angular
    .module('resultsExplorer')
    .controller('ResultFilterController', ['$scope', 'StratusData', function ($scope, StratusData) {

        $scope.visible = false;

        $scope.fromDate = new Date();

        $scope.toDate = new Date();

        $scope.foundServer;

        $scope.commonOptions = {
            change: function (ev) {
                console.log(ev);
            }
        }

        $scope.productTypes = {
            transport: {
                read: function (options) {
                    getData(StratusData.getProductTypes, options);
                }
            },
            schema: {
                data: "ProductTypes"
            },
        };

        $scope.products = {
            transport: {
                read: function (options) {
                    getData(StratusData.getProducts, options);
                }
            },
            schema: {
                data: "Products"
            }
        };

        $scope.sequences = {
            transport: {
                read: function (options) {
                    getData(StratusData.getSequences, options);
                }
            },
            schema: {
                data: "Sequences"
            }
        };

        $scope.plants = {
            transport: {
                read: function (options) {
                    getData(StratusData.getPlants, options);
                }
            },
            schema: {
                data: "Plants"
            }
        };

        $scope.processes = {
            transport: {
                read: function (options) {
                    getData(StratusData.getProcesses, options);
                }
            },
            schema: {
                data: "Processes"
            }
        };

        $scope.locations = {
            transport: {
                read: function (options) {
                    getData(StratusData.getLocations, options);
                }
            },
            schema: {
                data: "Locations"
            }
        };

        $scope.zones = {
            transport: {
                read: function (options) {
                    getData(StratusData.getZones, options);
                }
            },
            schema: {
                data: "Zones"
            }
        };

        $scope.cells = {
            transport: {
                read: function (options) {
                    getData(StratusData.getCells, options);
                }
            },
            schema: {
                data: "Cells"
            }
        };

        $scope.$on("kendoRendered", function () {
            $scope.visible = true;
        });

        var getData = function (functionCall, options) {
            functionCall(options.data,
                        function (response) {
                            $scope.foundServer = true;
                            options.success(response);
                        },
                        function (response) {
                            if (response === 404) {
                                $scope.foundServer = false;
                            }
                        })
        };
    }]);
