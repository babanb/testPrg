$(function () {

    $('#addHospitalization').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#addModalHospitalization').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addHospitalizationForm");

            $('#addModalHospitalization').modal('show');

        });

    });

});

function CloseDialogHospitalization() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
    ShowAjaxSuccessMessage($("#hospitalizationSuccessMessage").val());
}