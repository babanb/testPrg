$(function () {

    $('#replyButton').click(function (e) {
        e.preventDefault();

        $.get(this.href, function (data) {
            $('#replyMessage').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#messageReplyForm");

        });
    });   
});

