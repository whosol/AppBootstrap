'use strict';

angular
    .module('resultsExplorer')
    .controller('WeeklyFailuresController', ['$scope', function ($scope) {
        $scope.chartOptions = {
            dataSource: {},
            categoryAxis: {},
            series: [
                {
                    name: "Count"
                },
                {
                    name: '%'
                }],
            valueAxis: [{}, {}],
            legend: {},
            tooltip: {
                visible: true
            }
        };
    }]);