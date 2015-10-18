indicatorsApp.factory('indicatorsApi', [
    '$resource', function ($resource) {
        return $resource('/api/indicator/:id', null, {
            'update': { method: 'PUT' }
        });
    }
]);