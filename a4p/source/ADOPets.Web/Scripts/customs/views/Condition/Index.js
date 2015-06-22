$(function () {
    $('#addCondition').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addModal').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#addConditionForm");
            $('#addModal').modal('show');
        });
    });
});

function CloseDialogCondition() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
    ShowAjaxSuccessMessage($("#ConditionSuccessMessage").val());
}