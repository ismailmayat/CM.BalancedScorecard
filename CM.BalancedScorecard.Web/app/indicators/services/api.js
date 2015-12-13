module.exports = [
    "$resource", function ($resource) {
        return {
            indicators: $resource("/api/indicators/:id", null, {
                "query": { isArray: false },
                "save": {
                    method: "POST", transformResponse: function (data, headers) {
                        response = {}
                        response.data = data;
                        response.headers = headers();
                        return response;
                    }
                },
                "update": { method: "PUT" }
            }),
            indicatorMeasures: $resource("/api/indicators/:id/measures/:measureId", null, {
                "query": { isArray: false },
                "update": { method: "PUT" }
            })
        }
    }
];