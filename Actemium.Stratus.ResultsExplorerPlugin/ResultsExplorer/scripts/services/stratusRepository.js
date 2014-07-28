'use strict';

angular
    .module('stratusRepository', ['ngResource'])
    .service('StratusData', ['$resource', function ($resource) {
        
        var $scope = this;

        var cells = $resource('/api/cells/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        $scope.getCells = function (query, success, error) {
            return cells.query(query, success, error);
        };

        $scope.getCell = function (cellId, success, error) {
            return cells.get({ id: cellId }, success, error);
        };




        var companies = $resource('/api/companies/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        $scope.getCompanies = function (query, success, error) {
            return companies.query(query, success, error);
        };

        $scope.getCompany = function (companyId, success, error) {
            return companies.get({ id: companyId }, success, error);
        };




        var locations = $resource('/api/locations/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        $scope.getLocations = function (query, success, error) {
            return locations.query(query, success, error);
        };

        $scope.getLocation = function (locationId, success, error) {
            return locations.get({ id: locationId }, success, error);
        };




        var plants = $resource('/api/plants/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        $scope.getPlants = function (query, success, error) {
            return plants.query(query, success, error);
        };

        $scope.getPlant = function (plantId, success, error) {
            return plants.get({ id: plantId }, success, error);
        };




        var processes = $resource('/api/processes/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        $scope.getProcesses = function (query, success, error) {
            return processes.query(query, success, error);
        };

        $scope.getProcess = function (processId, success, error) {
            return processes.get({ id: processId }, success, error);
        };




        var products = $resource('/api/products/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });


        $scope.getProducts = function (query, success, error) {
            return products.query(query, success, error);
        };

        $scope.getProduct = function (productId, success, error) {
            return products.get({ id: productId }, success, error);
        };




        var productTypes = $resource('/api/productTypes/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        $scope.getProductTypes = function (query, success, error) {
            return productTypes.query(query, success, error);
        };

        $scope.getProductType = function (productTypeId, success, error) {
            return productTypes.get({ id: productTypeId }, success, error);
        };




        var results = $resource('/api/results/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        $scope.getResults = function (query, success, error) {
            return results.query(query, success, error);
        };

        $scope.getResult = function (resultId, success, error) {
            return results.get({ id: resultId }, success, error);
        };




        var sequences = $resource('/api/sequences/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        $scope.getSequences = function (query, success, error) {
            return sequences.query(query, success, error);
        };

        $scope.getSequence = function (sequenceId, success, error) {
            return sequences.get({ id: sequenceId }, success, error);
        };




        var testers = $resource('/api/testers/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        $scope.getTesters = function (query, success, error) {
            return testers.query(query, success, error);
        };

        $scope.getTester = function (testerId, success, error) {
            return testers.get({ id: testerId }, success, error);
        };




        var visits = $resource('/api/visits/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        $scope.getVisits = function (query, success, error) {
            return visits.query(query, success, error);
        };

        $scope.getVisit = function (visitId, success, error) {
            return visits.get({ id: visitId }, success, error);
        };




        var zones = $resource('/api/zones/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        $scope.getZones = function (query, success, error) {
            return zones.query(query, success, error);
        };

        $scope.getZone = function (zoneId, success, error) {
            return zones.get({ id: zoneId }, success, error);
        };
    }]);