angular.module("indicators").factory('indicatorsApi', ['$resource', function ($resource) {
    return {
        indicators: $resource('/api/indicators/:id', null, {
            'query': { isArray: false },
            'update': { method: 'PUT' }
        }),
        indicatorMeasures: $resource('/api/indicators/:id/measures/:measureId', null, {
            'query': { isArray: false },
            'update': { method: 'PUT' }
        })
    }
}]);