'use strict';


// Declare app level module which depends on filters, and services
angular
    .module('resultsExplorer', [
    'ngAnimate',
    'ngRoute',
    'kendo.directives'
    ])
    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/home', {
                templateUrl: 'views/home.html',
                controller: 'HomeController'
            });
        $routeProvider
            .when('/dashboard', {
                templateUrl: 'views/dashboard.html',
                controller: 'DashboardController'
            });
        $routeProvider
            .when('/results', {
                templateUrl: 'views/results.html',
                controller: 'ResultsController'
            });
        $routeProvider
            .when('/visits', {
                templateUrl: 'views/visits.html',
                controller: 'VisitsController'
            });
        $routeProvider
            .when('/about', {
                templateUrl: 'views/about.html',
                controller: 'AboutController'
            });
        $routeProvider
            .otherwise({ redirectTo: '/home' });
    }]);
