
$("#cancel-bt").click(function () {
    localStorage.clear();
    window.location = $('#IndexURL').val();
});
$(document).off("click", "#purchase-bt").on("click", "#purchase-bt", function (e) {
    e.preventDefault();
    $("#purchase-bt").attr('disabled', 'disabled');
    ShowLoading();
    $.ajax({
        url: $('#ConfirmationURL').val(),
        data: $("#SMOConfirmationForm").serialize(),
        type: 'POST',
        dataType: 'json',
        success: function (success) {
            var url1 = $('#PaymentURL').val();
            $.get(url1, null, function (data) {
                $("#divStep").html(data);
                HideLoading();
                localStorage.clear();
            });
        },
        error: function (req, status, error) {
        }
    });

});

$(function () {
    $('a[title]').tooltip();
});
$(document).off("click", "#editSetup").on("click", "#editSetup", function (e) {
    e.preventDefault();
    var url1 = $('#SetupURL').val();
    $.get(url1, function (data) {
        $('#divStep').html(data);
        Initialize();
        var url2 = $('#InvestigationListURL').val();
        $.get(url2, function (data) {
            $('#divInvestigation').html(data);
            Initialize();
        });
    });
    
});
$(document).off("click", "#editBilling").on("click", "#editBilling", function (e) {
    e.preventDefault();
    var url1 = $('#BillingURL').val();
    $.get(url1, function (data) {
        $('#divStep').html(data);
        Initialize();
    });
});
