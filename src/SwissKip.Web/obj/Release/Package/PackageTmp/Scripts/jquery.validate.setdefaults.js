$(function() {
    $.validator.setDefaults({
        onfocusout: function (element) {
            if (!$(element).is('select[name=Birthday]')) {
                if ( !this.checkable(element) && (element.name in this.submitted || !this.optional(element)) ) {
                    this.element(element);
                }
            }
        },
        highlight: function( element, errorClass, validClass ) {
            if ($(element).is('select[name=Birthday]')) {
                $("select[name=Birthday]").addClass(errorClass).removeClass(validClass);
            } else {
                $(element).addClass(errorClass).removeClass(validClass);
            }
        },
        unhighlight: function( element, errorClass, validClass ) {
            if ($(element).is('select[name=Birthday]')) {
                $("select[name=Birthday]").removeClass(errorClass).addClass(validClass);
            } else {
                $(element).removeClass(errorClass).addClass(validClass);
            }
        }
    });
});