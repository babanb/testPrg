$(function () {

    $('#addInsurance').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#addModalInsurance').html(data);

            Initialize();
            $("#addInsuranceForm").removeData("validator");
            $("#addInsuranceForm").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse($("#addInsuranceForm"));

            $('#addModalInsurance').modal('show');

            $('#addModalInsurance').on('hidden.bs.modal', function () {
                $(".modal-body").empty();
            });

        });

    });

});

function CloseDialogInsurance() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
}


function showMessage(val) {
    if (val != null && val != "") {
        $("#MessageContainer").html(
            '<div class="alert alert-success alert-dismissable">' +
                '<button type="button" class="close" ' +
                        'data-dismiss="alert" aria-hidden="true">' +
                    '&times;' +
                '</button>' +
                 val +
             '</div>');
    }
    HideSuccessMsg();
}