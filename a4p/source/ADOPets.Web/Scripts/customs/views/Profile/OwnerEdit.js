$(function () {

    $('#imageFile').on('change', function () {
        readURL(this);
    });

    UpdatePhoneNumberValidation('PrimaryPhone');
    UpdatePhoneNumberValidation('SecondaryPhone');
    if ($("meta[name='accept-domain']").attr("content") != "French") {
        UpdateProfileStates();
    }
    $('#EditprofileModel').ajaxForm({
        success: SubmitPetImageSuccesful
    });

    $("#DeleteImage").click(function () {
        $("#DeleteImg").val(true);
        $("#imgProfilePic").removeAttr("src");
        $("#imgProfilePic").attr("src", "/Content/Images/ownerProfilepic.jpg");
        $("#lnkDeleteProfPhoto").attr("style", "display:none;");
        $('#lnkDeleteProfPhoto').click();
    });

    $("#Country").change(function () {
        if ($("meta[name='accept-domain']").attr("content") != "French") {
            UpdateProfileStates();
        }
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
            var file = input.files[0];
            var extension = file.name.substring(file.name.lastIndexOf('.'));
            // Only process image files.
            var validFileType = ".jpeg, .jpg , .png , .bmp, .gif";
            if (validFileType.toLowerCase().indexOf(extension) < 0) {
                var invalidFiletypeMessage = $("#hdnInvalidImagetype").val();
                alert(invalidFiletypeMessage);
                $("#imgProfilePic").removeAttr("src");
                $("#imgProfilePic").attr("src", "/Content/Images/ownerProfilepic.jpg");
                return false;
            } else {
                $('#imgProfilePic').attr('src', e.target.result);
                $('#ProfilePic').attr('src', e.target.result);
                $("#DeleteImg").val(false);
                $("#lnkDeleteProfPhoto").attr("style", "display:block;");
            }
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
            $.getJSON(GetApplicationPath() + "Profile/GetStates", { country: Country },
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