$(function () {

    $('#Editprofile').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#EditprofileModel').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#EditProfileForm");

            $('#EditprofileModel').modal('show');

        });

    });

});


$('#myAccount a[data-toggle="tab"]').click(function () {
    $('#myAccount a[data-toggle="tab"]').removeClass("active");
    $($(this).attr('href')).addClass("active");
    $(this).addClass("active");
});

function CloseDialog() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
}

function SubmitPetImageSuccesful(data) {
    if (data.indexOf('meta') > 1) {
        location.reload();
    } else {
        $(".modal-dialog").html(data);
    }
}