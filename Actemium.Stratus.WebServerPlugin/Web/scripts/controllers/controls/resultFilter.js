'use strict';

angular
    .module('resultsExplorer')
    .controller('ResultFilterController', ['$scope', 'StratusData', function ($scope, StratusData) {

        $scope.visible = false;

        $scope.fromDate = new Date();

        $scope.toDate = new Date();

        $scope.productTypes = {
            transport: {
                read: function (options) {
                    StratusData.getProductTypes(options.data,
                        function (response) {
                            options.success(response);
                        },
                        function (response) {

                        })
                }
            },
            schema: {
                data: "ProductTypes"
            }
        };

        $scope.products = {
            transport: {
                read: function (options) {
                    StratusData.getProducts(options.data,
                        function (response) {
                            options.success(response);
                        },
                        function (response) {
                        })
                }
            },
            schema: {
                data: "Products"
            }
        };

        $scope.sequences = {
            transport: {
                read: function (options) {
                    StratusData.getSequences(options.data,
                        function (response) {
                            options.success(response);
                        },
                        function (response) {
                        })
                }
            },
            schema: {
                data: "Sequences"
            }
        };

        $scope.plants = {
            transport: {
                read: function (options) {
                    StratusData.getPlants(options.data,
                        function (response) {
                            options.success(response);
                        },
                        function (response) {
                        })
                }
            },
            schema: {
                data: "Plants"
            }
        };

        $scope.processes = {
            transport: {
                read: function (options) {
                    StratusData.getProcesses(options.data,
                        function (response) {
                            options.success(response);
                        },
                        function (response) {
                        })
                }
            },
            schema: {
                data: "Processes"
            }
        };

        $scope.locations = {
            transport: {
                read: function (options) {
                    StratusData.getLocations(options.data,
                        function (response) {
                            options.success(response);
                        },
                        function (response) {
                        })
                }
            },
            schema: {
                data: "Locations"
            }
        };

        $scope.zones = {
            transport: {
                read: function (options) {
                    StratusData.getZones(options.data,
                        function (response) {
                            options.success(response);
                        },
                        function (response) {
                        })
                }
            },
            schema: {
                data: "Zones"
            }
        };

        $scope.cells = {
            transport: {
                read: function (options) {
                    StratusData.getCells(options.data,
                        function (response) {
                            options.success(response);
                        },
                        function (response) {
                        })
                }
            },
            schema: {
                data: "Cells"
            }
        };

        $scope.$on("kendoRendered", function () {
            $scope.visible = true;
        });
    }]);
