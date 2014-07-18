'use strict';

/* Controllers */

angular.module('resultsExplorer.controllers', [])
    .controller('HomeController', ['$scope', function ($scope) {

    }])

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
                        alert('WebApi at /api/results not found');
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
    }])

    .controller('VisitsController', ['$scope', function ($scope) {

        $scope.visitGridOptions = {
            dataSource: {
                transport: {
                    read: "/api/visits",
                },
                error: function (e) {
                    if (e.xhr.status == 404) {
                        alert('WebApi at /api/visits not found');
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
    }])

    .controller('AboutController', ['$scope', '$http', function ($scope, $http) {
        $http.get('/api/servicestatus').success(function (data) {
            $scope.plugins = data.Plugins;
        });
        $http.get('/api/thirdparty').success(function (data) {
            $scope.libraries = data.Libraries;
        });
    }])

    .controller('NavController', ['$scope', '$location', function ($scope, $location) {

        $scope.isCollapsed = true;

        $scope.$on('$routeChangeSuccess', function () {
            $scope.isCollapsed = true;
        });

        $scope.getClass = function (path) {
            if ($location.path().substr(0, path.length) === path) {
                return "active";
            }
            else {
                return "";
            }
        };
    }]).
    controller('ResultFilterController', ['$scope', function ($scope) {

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
    }]);
