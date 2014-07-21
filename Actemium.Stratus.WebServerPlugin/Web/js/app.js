'use strict';


// Declare app level module which depends on filters, and services
angular.module('resultsExplorer', [
    'ngAnimate',
    'ngRoute',
    'resultsExplorer.filters',
    'resultsExplorer.services',
    'resultsExplorer.directives',
    'resultsExplorer.controllers',
    'kendo.directives'
]).
    config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/home', {templateUrl: 'partials/home.html', controller: 'HomeController'});
        $routeProvider.when('/dashboard', {templateUrl: 'partials/dashboard.html', controller: 'DashboardController'});
        $routeProvider.when('/results', { templateUrl: 'partials/results.html', controller: 'ResultsController' });
        $routeProvider.when('/visits', { templateUrl: 'partials/visits.html', controller: 'VisitsController' });
        $routeProvider.when('/about', { templateUrl: 'partials/about.html', controller: 'AboutController' });
        $routeProvider.otherwise({redirectTo: '/home'});
    }]);
