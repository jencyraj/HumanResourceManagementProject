
function validatectrl() {
    var isValid = true;
    $('.validate').each(function () {
        if ('s' + $(this).val().trim() == 's') {
            isValid = false;
            $(this).parent().parent().addClass("has-error");
        }
        else {
            $(this).parent().parent().removeClass("has-error");
        }
    });

    return isValid;
}

function validateempctrl() {
    var isValid = true;
    $('.validate').each(function () {
        if ('s' + $(this).val().trim() == 's') {
            isValid = false;
            $(this).parent().addClass("has-error");
        }
        else {
            $(this).parent().removeClass("has-error");
        }
    });

    return isValid;
}

       

