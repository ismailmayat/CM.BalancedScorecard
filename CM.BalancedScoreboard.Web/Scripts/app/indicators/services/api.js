indicatorsApp.factory('indicatorsApi', [
    '$resource', function($resource) {
        return $resource('/api/indicator/:id', {}, {
            query: { method: 'GET', isArray: true }
        });
    }
]);