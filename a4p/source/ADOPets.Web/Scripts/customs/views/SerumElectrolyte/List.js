$(function () {

    $('.editSerumElectrolyte').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editSerumElectrolyteModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editSerumElectrolyteForm");

            $('#editSerumElectrolyteModal').modal('show');

        });
    });


    $('.deleteSerumElectrolyte').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteSerumElectrolyteModal').html(data);

            $('#deleteSerumElectrolyteModal').modal('show');

        });
    });


    DrawTable('#Table_SerumElectrolyte', [0]);

});
