function initialiseclick() {
    $('.editInsurance').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#editeModalInsuranc').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#editInsuranceForm");
            $('#editeModalInsuranc').modal('show');
        });
    });

    $('.deleteInsurance').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#deleteModalInsurance').html(data);
            $('#deleteModalInsurance').modal('show');
        });
    });
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