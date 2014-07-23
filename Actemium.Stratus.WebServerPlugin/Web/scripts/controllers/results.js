'use strict';

angular
    .module('resultsExplorer')
    .controller('ResultsController', ['$scope', 'StratusData', function ($scope, StratusData) {


        $scope.serverStatus = '';

        $scope.resultGridOptions = {
            dataSource: {
                transport: {
                    read: function (options) {
                        StratusData.getResults({
                            page: options.data.page,
                            pageSize: options.data.pageSize
                        }, function (response) {
                            console.log(response);
                            options.success(response);
                        }, function (response) {
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


