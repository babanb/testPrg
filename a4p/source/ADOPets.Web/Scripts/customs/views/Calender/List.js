
$(function () {
    $('.editReminder').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editModalReminder').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editReminderForm");

            $('#editModalReminder').modal('show');

        });
    });


    $('.deleteReminder').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteModalReminder').html(data);

            $('#deleteModalReminder').modal('show');

        });
    });


    DrawTable('#Table_Calender', [0]);
    filteronSelect('#show_Reminders', 6, '#Table_Calender');
    $('#Table_Calender').DataTable().column(6).search($('select#show_Reminders option:selected').val()).draw();
});
