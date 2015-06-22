$(function () {
    $("[data-toggle='tooltip']").tooltip();
    $('.deleteCenterClass').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#deleteModal').html(data);
            $('#deleteModal').modal('show');
        });
    });

    DrawTable('#tblCenter');
    filteronSelect('#Center', 1, '#tblCenter');
});


function CloseDialogCenter() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
    ShowAjaxSuccessMessage($("#CenterSuccessMessage").val());
}