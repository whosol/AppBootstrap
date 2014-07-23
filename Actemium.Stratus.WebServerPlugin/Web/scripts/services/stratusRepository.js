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

        this.getCells = function (query, callback) {
            return this.$$cells.query(query, callback);
        };

        this.getCell = function (cellId, callback) {
            return this.$$cells.get({ id: cellId }, callback);
        };




        this.$$companies = $resource('/api/companies/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getCompanies = function (query, callback) {
            return this.$$companies.query(query, callback);
        };

        this.getCompany = function (companyId, callback) {
            return this.$$companies.get({ id: companyId }, callback);
        };




        this.$$locations = $resource('/api/locaitons/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getLocations = function (query, callback) {
            return this.$$locations.query(query, callback);
        };

        this.getLocation = function (locationId, callback) {
            return this.$$locations.get({ id: LocationId }, callback);
        };




        this.$$plants = $resource('/api/plants/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getPlants = function (query, callback) {
            return this.$$plants.query(query, callback);
        };

        this.getPlant = function (plantId, callback) {
            return this.$$plants.get({ id: plantId }, callback);
        };




        this.$$processes = $resource('/api/processes/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getProcesses = function (query, callback) {
            return this.$$processes.query(query, callback);
        };

        this.getProcess = function (processId, callback) {
            return this.$$processes.get({ id: processId }, callback);
        };




        this.$$products = $resource('/api/products/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });


        this.getProducts = function (query, callback) {
            return this.$$products.query(query, callback);
        };

        this.getProduct = function (productId, callback) {
            return this.$$products.get({ id: productId }, callback);
        };




        this.$$productTypes = $resource('/api/productTypes/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getProductTypes = function (query, callback) {
            return this.$$productTypes.query(query, callback);
        };

        this.getProductType = function (productTypeId, callback) {
            return this.$$productTypes.get({ id: productTypeId }, callback);
        };




        this.$$results = $resource('/api/results/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getResults = function (query, callback) {
            return this.$$results.query(query, callback);
        };

        this.getResult = function (resultId, callback) {
            return this.$$results.get({ id: resultId }, callback);
        };




        this.$$sequences = $resource('/api/sequences/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getSequences = function (query, callback) {
            return this.$$sequences.query(query, callback);
        };

        this.getSequence = function (sequenceId, callback) {
            return this.$$sequences.get({ id: sequenceId }, callback);
        };




        this.$$testers = $resource('/api/testers/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getTesters = function (query, callback) {
            return this.$$testers.query(query, callback);
        };

        this.getTester = function (testerId, callback) {
            return this.$$testers.get({ id: testerId }, callback);
        };




        this.$$visits = $resource('/api/visits/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getVisits = function (query, callback) {
            return this.$$visits.query(query, callback);
        };

        this.getVisit = function (visitId, callback) {
            return this.$$visits.get({ id: visitId }, callback);
        };




        this.$$zones = $resource('/api/zones/:id', { id: '@id' }, {
            query: {
                method: 'GET',
                isArray: false
            }
        });

        this.getZones = function (query, callback) {
            return this.$$zones.query(query, callback);
        };

        this.getZone = function (zoneId, callback) {
            return this.$$zones.get({ id: zoneId }, callback);
        };
    }]);