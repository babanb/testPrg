$(function () {

    $('#imgNewUserPhoto').on('change', function () {
        readURL(this);
    });

    UpdatePhoneNumberValidation('PrimaryPhone');
    UpdatePhoneNumberValidation('SecondaryPhone');
    UpdateProfileStates();

    $("#DeleteImage").click(function () {
        $("#DeleteImg").val(true);
        $("#imgNewUserProfilePic").removeAttr("src");
        $("#imgNewUserProfilePic").attr("src", "/Content/Images/ownerProfilepic.jpg");
        $('.modal').modal('hide');
    });

    $("#Country").change(function () {
        UpdateProfileStates();
    });

    var CurrentTimezone = $("#TimeZone").val();

    $("#TimeZone").html($('#TimeZone option').sort(function (x, y) {
        return $(x).text() < $(y).text() ? -1 : 1;
    }));

    $("#TimeZone").val(CurrentTimezone);
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgNewUserProfilePic').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function UpdateProfileStates() {
    var Country = $('#Country').val();
    var State = $('#State').val();
    var subItems = "<option value>" + $("#State").children()[0].text + "</option>";
    if (Country == "France") {
        $("#State").html(subItems);
        $("#State").prop("disabled", true);
    }
    else {
        $("#State").prop("disabled", false);

        if ($("#Country option:selected").index() == 0) {
            $("#State").html(subItems);
        } else {
            $.getJSON(GetApplicationPath() + "Account/GetStates", { country: Country },
                function (data) {
                    $.each(data, function (index, item) {
                        if (item.Value == State) {
                            subItems += "<option value='" + item.Value + "' selected>" + item.Text + "</option>";
                        } else {
                            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                        }
                    });
                    $("#State").html(subItems);
                });
        }
    }
}