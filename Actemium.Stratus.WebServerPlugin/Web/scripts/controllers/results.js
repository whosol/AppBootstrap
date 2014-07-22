'use strict';

angular.module('resultsExplorer')

    .controller('ResultsController', ['$scope', function ($scope) {

        $scope.resultPanelOptions = {

        };

        $scope.fromDate = [];

        $scope.toDate = [];

        $scope.resultGridOptions = {
            dataSource: {
                transport: {
                    read: "/api/results"
                },
                error: function (e) {
                    if (e.xhr.status == 404) {
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


