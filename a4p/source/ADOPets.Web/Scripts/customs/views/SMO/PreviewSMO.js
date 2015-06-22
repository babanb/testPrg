$(function () {
    //Show Model Popup with BioData of Expert
    $('.showBio').click(function (e) {
        e.preventDefault();

        $.get(this.href, function (data) {

            $('#showBioModel').html(data);

            Initialize();


            $('#showBioModel').modal('show');

        });

    });

    //Show Model Popup with Response of Expert
    $('.expertResponseModel').click(function (e) {
        e.preventDefault();

        $.get(this.href, function (data) {

            $('#expertResponse').html(data);

            Initialize();

            // $.validator.unobtrusive.parse("#EditProfileForm");

            $('#expertResponse').modal('show');

        });

    });
});