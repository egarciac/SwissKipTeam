(function() {

    var init = function() {
        addUnobtrusiveValidations();
        updateBirthdayValues();
    };

    var addUnobtrusiveValidations = function() {
        $.validator.unobtrusive.adapters.add("birthday", [], function(options) {
            options.rules['splitdate'] = options.params;
            options.messages['splitdate'] = options.message;
        });

        $.validator.addMethod('splitdate', function() {
            var day = $(".Day")[0].selectedIndex;
            var month = $(".Month")[0].selectedIndex;
            var year = $(".Year")[0].selectedIndex;

            if (day === 0 && month === 0 && year === 0)
                return true;
            if (day != 0 && month != 0 && year != 0)
                return isValidDate(parseInt($(".Year")[0].value), month, day);
            return false;
        });

        function isValidDate(y, m, d) {
            var date = new Date(y, m - 1, d);
            var convertedDate = date.getFullYear() + (date.getMonth() + 1) + date.getDate();
            var givenDate = y + m + d;
            return (givenDate == convertedDate);
        }
    };

    /*
    * El template helper con la fecha dividida en 3 "Select List" (Día, Mes, Año) 
    * genera el mismo Name y Id (Birthday) para todos los "Select List",
    * pero para poder realizar el Model Binding se necesita que los IDs sean: Birthday.Day, Birthday.Month, Birthday.Year
    * 
    * Por lo tanto, se está sincronizando los valores de los 3 "Select List" con otros 3 inputs "Hidden" 
    * que tienen el atributo Name adecuado, para que estos también sean enviados al servidor y el binding funcione.
    */
    var updateBirthdayValues = function() {
        $(".Day").change(function() {
            $("#Birthday_Day").val($(this).val());
        }).change();

        $(".Month").change(function() {
            $("#Birthday_Month").val($(this).val());
        }).change();

        $(".Year").change(function() {
            $("#Birthday_Year").val($(this).val());
        }).change();
    };

    init();
})();