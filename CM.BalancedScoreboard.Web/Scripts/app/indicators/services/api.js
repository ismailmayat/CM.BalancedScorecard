indicatorsApp.factory('indicatorsApi', ['$resource', function($resource) {
        return {
            indicators: $resource('/api/indicators/:id', null, {
                'update': { method: 'PUT' }
            }),
            indicatorMeasures: $resource('/api/indicators/:id/measures/:measureId', null, {
                'update': { method: 'PUT' }
            })
        }
    }
]);