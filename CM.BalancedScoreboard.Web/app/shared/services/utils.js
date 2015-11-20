module.exports = [
    function() {
        return {
            formatDateInput: function(date) {
                var d = new Date(date),
                    month = '' + (d.getMonth() + 1),
                    day = '' + d.getDate(),
                    year = d.getFullYear();

                if (month.length < 2) month = "0" + month;
                if (day.length < 2) day = "0" + day;

                return [year, month, day].join("-");
            },
            formatMonthInput: function (date) {
                var d = new Date(date),
                    month = '' + (d.getMonth() + 1),
                    year = d.getFullYear();

                if (month.length < 2) month = "0" + month;

                return [year, month].join("-");
            },
            monthNames: function() {
                return ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
            },
            formatGraphDate: function(date) {
                var d = new Date(date);

                return this.monthNames()[d.getMonth()] + ' ' + d.getFullYear().toString().substr(2, 4);
            }
        };
    }
];