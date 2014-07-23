'use strict';

angular
    .module('resultsExplorer')
    .controller('ResultsController', ['$scope', 'StratusData', function ($scope, StratusData) {

        $scope.cell = StratusData.getCell(2);

        $scope.resultGridOptions = {
            dataSource: {
                transport: {
                    read: function (options) {
                        StratusData.getResults(options.data, function (response) {
                            console.log(response);
                            options.success(response);
                        });
                    }
                },
                error: function (e) {
                    if (e.xhr.status === 404) {
                        //  alert('WebApi at /api/results not found');
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
                }
            ]
        };
    }]);


