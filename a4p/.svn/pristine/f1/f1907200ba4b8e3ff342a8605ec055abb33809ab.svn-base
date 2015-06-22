$(function () {

    // DrawTable('#UserList'); TODO:: NUTAN 26 Sept *@

    $('#addUser').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#AddProfileDialog').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#AddProfileForm");

            $('#AddProfileDialog').modal('show');

        });

    });

});

function CloseDialog() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
}