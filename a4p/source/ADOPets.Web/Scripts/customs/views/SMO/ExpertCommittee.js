$('#btnSendMessage').click(function (e) {
    e.preventDefault();
    var responsetext = $("#ExpertMessage").val();
    var smodid = $("#SMOId").val();
    if (responsetext != "") {
        $.ajax({
            url: $('#UrlForSendMessage').val(),
            data: { 'response': responsetext, 'smoid': smodid },
            type: 'POST',
            dataType: 'json',
            success: function (success) {
                if (success) {
                    var url = $('#ExpertCommitteeURL').val();
                    window.location = url;
                }
            },
            error: function (req, status, error) {

            }
        });
    }
    else {
        // alert("Please fill all mandatory fields");
    }
});
function deleteConfirmation() { }

$('#ExpertCommittee').change(function () {
    showAll();
    var sel = $(this).val();
    if (sel != "") {
        var filtertype = $('#ExpertCommittee :selected').text();

        if ($('div[class="listingExpert"] :visible').find("[data-item-name='first_name']")) {
            var param = $('.listingExpert :visible');
            $.each(param.find("[data-item-name='first_name']"), function (key, val) {

                if (filtertype.toLowerCase().replace(/ /g, '') == val.innerHTML.toLowerCase().replace(/ /g, '')) {
                    $(this).parent().parent().parent().parent().parent().show();
                }
                else {
                    $(this).parent().parent().parent().parent().parent().hide();
                }
            });
        }
    }
});

function deleteMessage(msgId, smoId) {
    $.ajax({
        url: "/SMO/DeleteMessage",
        data: { 'msgId': msgId, 'smoId': smoId },
        type: 'POST',
        dataType: 'json',
        success: function (response) {
            if (response.success) {
                var url = $('#ExpertCommitteeURL').val();
                ShowAjaxSuccessMessage(response.successMessage);
                window.location = url;
            }
        },
        error: function (req, status, error) {
            alert(error);
        }
    });
}