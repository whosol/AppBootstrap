'use strict';

angular
    .module('resultsExplorer')
    .controller('ResultsController', ['$scope', 'StratusData', 'FilterData', function ($scope, StratusData, FilterData) {

        $scope.serverStatus = '';

        $scope.$on('filtersUpdated', function () {
            var grid = $('#result-grid').data('kendoGrid');
            grid.dataSource.read()
        });

        $scope.resultGridOptions = {
            dataSource: {
                transport: {
                    read: function (options) {
                        var data = {
                            page: options.data.page,
                            pageSize: options.data.pageSize
                        };
                        //add the filters to the data object
                        for (var attrname in FilterData.filters) {
                            data[attrname] = FilterData.filters[attrname]
                        }

                        StratusData.getResults(data,
                            function (response) {
                                console.log(response);
                                options.success(response);
                            },
                            function (response) {
                                if (response.status === 404) {
                                    $scope.serverStatus = 'Server is offline. Please check configuration.';
                                }
                            });
                    }
                },
                pageSize: 20,
                serverPaging: true,
                schema: {
                    data: "Results",
                    total: "Total"
                }
            },
            sortable: true,
            pageable: true,
            columns: [
                {
                    field: "SequenceName",
                    title: "Sequence",
                    width: "120px"
                },
                {
                    field: "ResultName",
                    title: "Result",
                    width: "120px"
                },
                {
                    field: "Value",
                    width: "120px"
                },
                {
                    field: "UpperLimit",
                    title: "Upper Limit",
                    width: "120px"
                },
                {
                    field: "LowerLimit",
                    title: "Lower Limit",
                    width: "120px"
                },
                {
                    field: "Units"
                },
                {
                    field: "Status"
                }
            ],
            enabled: $scope.enabled
        };
    }]);


