$(function () {

    $('#ownerEditProfile').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#EditprofModel').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#EditProfileForm");

            $('#EditprofModel').modal('show');

        });

    });
    
    $('#changePwd').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#ChangePwdModel').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#ChangePasswordForm");

            $('#ChangePwdModel').modal('show');

        });

    });

});

function SubmitPetImageSuccesful(data) {
    if (data.indexOf('lang="en"') > 1) {
        location.reload();
    } else {
        $(".modal-dialog").html(data);
    }
}