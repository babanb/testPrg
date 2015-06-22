$(function () {
    $('.showBio').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#showBioModel').html(data);
            Initialize();
            $('#showBioModel').modal('show');
        });
    });

    $('#Editprofile').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#EditprofileModel').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#EditProfileForm");
            $('#EditprofileModel').modal('show');
        });
    });

    $('.expertResponseModel').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#expertResponse').html(data);
            Initialize();
            // $.validator.unobtrusive.parse("#EditProfileForm");
            $('#expertResponse').modal('show');
        });
    });

    $('.NewMsgModel').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addModalNewMessage').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#newMessageForm");
            $('#addModalNewMessage').modal('show');
        });
    });

});
$(document).ready(function () {
    $(document).off("click", ".addBio").on("click", ".addBio", function (e) {
        $.get(this.href, function (data) {
            $('#addBioModel').html(data);
            Initialize();
            // $.validator.unobtrusive.parse("#EditProfileForm");
            $('#addBioModel').modal('show');
        });
    });

    $(document).off("click", "#btnSaveBioData").on("click", "#btnSaveBioData", function (e) {

        var url = $("#UrlAddBiodataExpert").val();
        var vetExpertId = $("#vetExpertId").val();
        var message = $("#txtcomments").val();
        $.ajax({
            url: url,
            data: { 'vetExpertId': vetExpertId, 'txtcomment': message },
            type: 'POST',
            dataType: 'json',
            success: function (success) {
                var url = $('#UrlAsignedExpert').val() + "?id=" + success.smoid;
                window.location = url;
            },
            error: function (req, status, error) {

            }
        });

    });

    $(document).off("click", ".deleteBio").on("click", ".deleteBio", function (e) {
        var ID = $(this).attr('data-id');
        $.get($('#UrlConfirmDeleteAssignedExpert').val() + '/' + ID, function (data) {
            $('#deleteModalTest').html(data);
            Initialize();
            $('#deleteModalTest').modal('show');
        });


    });
});

function callDeleteExpert(ID) {
    var url = $("#UrlDeleteAssignedExpert").val();
    var smoid = $(this).attr('data-smoid');
    var vetid = $(this).attr('data-vetid');
    $.ajax({
        url: url,
        data: { 'id': ID },
        type: 'POST',
        dataType: 'json',
        success: function (success) {
            $('#expertdelclose-bt').click();
            var url = $('#UrlAsignedExpert').val() + "?id=" + success.smoid;
            window.location = url;
        },
        error: function (req, status, error) {

        }
    });
}

$('#btnSubmitResponse').click(function (e) {
    e.preventDefault();
    var responsetext = $("#a_ExpertResponse").val();
    var expertid = $("#SMOREquest_ID").val();
    if (responsetext != "") {
        $.ajax({
            url: $('#EditVDURL').val(),
            data: { 'response': responsetext, 'smoid': expertid },
            type: 'POST',
            dataType: 'json',
            success: function (success) {
                if (success) {
                    var url = $('#IndexURL').val();
                    window.location = url;
                }
            },
            error: function (req, status, error) {

            }
        });
    }
    else {
        alert("Please fill all mandatory fields");
    }
});

