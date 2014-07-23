'use strict';

angular
    .module('resultsExplorer')
    .controller('VisitsController', ['$scope', 'StratusData', function ($scope, StratusData) {

        $scope.serverStatus = '';

        $scope.visitGridOptions = {
            dataSource: {
                transport: {
                    read: function (options) {
                        StratusData.getVisits({
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
