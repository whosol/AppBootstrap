'use strict';

angular
    .module('resultsExplorer')
    .controller('ResultFilterController', ['$scope', 'StratusData', 'FilterData', function ($scope, StratusData, FilterData) {


        $scope.visible = false;

        $scope.fromDate = new Date();

        $scope.toDate = new Date();

        $scope.foundServer;

        $scope.options = {};




        $scope.datetimeOptions = {
            format: 'dd/MM/yyyy hh:mm',

            value: kendo.date.today(),

            change: function (e) {
                if ($scope.fromDate < $scope.toDate) {
                    $scope.options[this.element.prop('id')] = this.element.val();
                }
                else {

                }
            }
        }

        $scope.comboboxOptions = {
            open: function (e) {
                var ds = e.sender.dataSource;
                ds.read();
            },
            change: function (e) {
                var item = this.dataItem(this.select());
                if (item && item.Id > 0) {
                    $scope.options[this.element.prop('id')] = item.Id
                }
                else if (item && item.Id < 0) {
                    $scope.options = {};
                }
                else {
                    delete $scope.options[this.element.prop('id')];
                }

                FilterData.updateFilters($scope.options);

                console.log($scope.options);
            }
        }


        $scope.productTypes = {
            serverFiltering: true,
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
            serverFiltering: true,
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
            serverFiltering: true,
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
            serverFiltering: true,
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
            serverFiltering: true,
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
            serverFiltering: true,
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
            serverFiltering: true,
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
            serverFiltering: true,
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
            functionCall(FilterData.filters,
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
