'use strict';

angular
    .module('stratusRepository', ['ngResource'])
    .service('StratusData', ['$resource', function ($resource) {

        this.$$cells = $resource('/api/cells/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getCells = function (query, success, error) {
            return this.$$cells.query(query, success, error);
        };

        this.getCell = function (cellId, success, error) {
            return this.$$cells.get({ id: cellId }, success, error);
        };




        this.$$companies = $resource('/api/companies/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getCompanies = function (query, success, error) {
            return this.$$companies.query(query, success, error);
        };

        this.getCompany = function (companyId, success, error) {
            return this.$$companies.get({ id: companyId }, success, error);
        };




        this.$$locations = $resource('/api/locations/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getLocations = function (query, success, error) {
            return this.$$locations.query(query, success, error);
        };

        this.getLocation = function (locationId, success, error) {
            return this.$$locations.get({ id: locationId }, success, error);
        };




        this.$$plants = $resource('/api/plants/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getPlants = function (query, success, error) {
            return this.$$plants.query(query, success, error);
        };

        this.getPlant = function (plantId, success, error) {
            return this.$$plants.get({ id: plantId }, success, error);
        };




        this.$$processes = $resource('/api/processes/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getProcesses = function (query, success, error) {
            return this.$$processes.query(query, success, error);
        };

        this.getProcess = function (processId, success, error) {
            return this.$$processes.get({ id: processId }, success, error);
        };




        this.$$products = $resource('/api/products/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });


        this.getProducts = function (query, success, error) {
            return this.$$products.query(query, success, error);
        };

        this.getProduct = function (productId, success, error) {
            return this.$$products.get({ id: productId }, success, error);
        };




        this.$$productTypes = $resource('/api/productTypes/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getProductTypes = function (query, success, error) {
            return this.$$productTypes.query(query, success, error);
        };

        this.getProductType = function (productTypeId, success, error) {
            return this.$$productTypes.get({ id: productTypeId }, success, error);
        };




        this.$$results = $resource('/api/results/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getResults = function (query, success, error) {
            return this.$$results.query(query, success, error);
        };

        this.getResult = function (resultId, success, error) {
            return this.$$results.get({ id: resultId }, success, error);
        };




        this.$$sequences = $resource('/api/sequences/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getSequences = function (query, success, error) {
            return this.$$sequences.query(query, success, error);
        };

        this.getSequence = function (sequenceId, success, error) {
            return this.$$sequences.get({ id: sequenceId }, success, error);
        };




        this.$$testers = $resource('/api/testers/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getTesters = function (query, success, error) {
            return this.$$testers.query(query, success, error);
        };

        this.getTester = function (testerId, success, error) {
            return this.$$testers.get({ id: testerId }, success, error);
        };




        this.$$visits = $resource('/api/visits/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getVisits = function (query, success, error) {
            return this.$$visits.query(query, success, error);
        };

        this.getVisit = function (visitId, success, error) {
            return this.$$visits.get({ id: visitId }, success, error);
        };




        this.$$zones = $resource('/api/zones/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getZones = function (query, success, error) {
            return this.$$zones.query(query, success, error);
        };

        this.getZone = function (zoneId, success, error) {
            return this.$$zones.get({ id: zoneId }, success, error);
        };
    }]);