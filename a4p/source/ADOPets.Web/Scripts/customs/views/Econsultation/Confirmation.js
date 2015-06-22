 function onBegin() {
    ShowLoading();

}
function onComplete() {
    HideLoading();
    localStorage.clear();
}

jQuery(function () {
    return $("body").off("click", "#purchase-bt", function (e) {
    });
});
jQuery(function () {
    return $("body").on("click", "#purchase-bt", function (e) {
        e.preventDefault();
        $("#purchase-bt").attr('disabled', 'disabled');
        $.ajax({

            url: '/Econsultation/Confirmation',
            data: $("#EconsultationConfirmationForm").serialize(),
            type: 'POST',
            dataType: 'json',
            success: function (success) {
                var url1 = "/Econsultation/Payment";
                $.get(url1, null, function (data) {
                    $("#Confirmation").html(data);
                });
            },
            error: function (req, status, error) {
            }
        });

    });
});


$(function () {
    $('a[title]').tooltip();
});
jQuery(function () {
    return $("body").off("click", "#editSetup", function (e) {
    });
});
jQuery(function () {
    return $("body").on("click", "#editSetup", function (e) {
        e.preventDefault();
        var url1 = "/Econsultation/Setup";
        $.get(url1, function (data) {
            $('#Setup').html(data);
            Initialize();
        });

    });
});
jQuery(function () {
    return $("body").off("click", "#editBilling", function (e) {
    });
});
jQuery(function () {
    return $("body").on("click", "#editBilling", function (e) {
        e.preventDefault();
        var url1 = "/Econsultation/Billing";
        $.get(url1, function (data) {
            $('#Billing').html(data);
            Initialize();
        });
    });
});