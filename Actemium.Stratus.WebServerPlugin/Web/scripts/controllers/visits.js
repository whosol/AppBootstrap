'use strict';

angular
    .module('resultsExplorer')
    .controller('VisitsController', ['$scope', 'StratusData', function ($scope, StratusData) {

        $scope.visitGridOptions = {
            dataSource: {
                transport: {
                    read: function (options) {
                        StratusData.getVisits(options.data, function (response) {
                            console.log(response);
                            options.success(response);
                        });
                    }
                },
                error: function (e) {
                    if (e.xhr.status === 404) {
                        //  alert('WebApi at /api/visits not found');
                    }
                },
                pageSize: 20,
                serverPaging: true,
                schema: {
                    data: "Visits",
                    total: "Total"
                }
            },

            sortable: true,
            pageable: true,
            columns: [
                {
                    field: "ProductUniqueId",
                    title: "Product ID",
                    width: "120px"
                },
                {
                    field: "StartTime",
                    title: "Start Time",
                    width: "120px"
                },
                {
                    field: "EndTime",
                    title: "End Time",
                    width: "120px"
                },
                {
                    field: "Duration",
                    width: "120px"
                },
                {
                    field: "Status"
                }
            ]
        };
    }]);
