$(function () {
    $('.editCondition').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#editModal').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#editConditionForm");
            $('#editModal').modal('show');
        });
    });

    $('.deleteCondition').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#deleteModal').html(data);
            $('#deleteModal').modal('show');
        });
    });

    DrawTable('#Table_condition', [1, 2]);
    HideLoading();

});