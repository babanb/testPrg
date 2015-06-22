$(document).ready(function () {
    
    $("#btnAddContact").click(function (e) {
        e.preventDefault();
        $.get('/SharePetInfo/AddContact', function (data) {
            $('#addContactModel').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#addContactForm");
            $('#addContactModel').modal('show');
        });
    });


    $('.btnDeleteContact').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#deleteContactModel').html(data);
            $('#deleteContactModel').modal('show');
        });
    });
    
});



