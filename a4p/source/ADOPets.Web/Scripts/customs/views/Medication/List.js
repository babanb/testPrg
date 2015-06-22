$(function () {
    $(".showmedidetails").mouseover(function (e) {
        $(this).next(".medicationpopover").show();
    });
    $(".medicationpopoverperent").mouseleave(function (e) {
        $(".medicationpopover").hide();
    });

    $('.editMedication').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editModalMedication').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editMedicationForm");

            $('#editModalMedication').modal('show');

        });
    });


    $('.deleteMedication').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteModalMedication').html(data);

            $('#deleteModalMedication').modal('show');

        });
    });
    DrawTable('#Table_medication', [1, 2]);
});