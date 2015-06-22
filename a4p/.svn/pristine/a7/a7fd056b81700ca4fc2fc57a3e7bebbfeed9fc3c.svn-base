$(document).ready(function () {
    $("#btnEditCover").show();

    $("#minimizeBuble").click(function (e) {
        $("#maximizeBuble").show();
        $("#minimizeBuble").hide();
        $("#buble").addClass('minimizeBubleStyle');
        $("#bubleContent").hide();
    });
    $("#maximizeBuble").click(function (e) {
        $("#minimizeBuble").show();
        $("#maximizeBuble").hide();
        $("#bubleContent").show();
        $("#buble").removeClass('minimizeBubleStyle');
    });
    $("#cardEdit").click(function (e) {
        e.preventDefault();
        $("#petdetails").load($(this).attr("data-attr"));

        $("#breederdetails,#insurancedetails,#addInsurance,#cardEdit").hide();
        $("#showpetdetails,#showbreederdetails,#showinsurancedetails").removeClass("btn-success").addClass("btn-default");
        $("#showpetdetails").addClass("btn-success");

        $("#profileeditbtn").show();

        return false;
    });
    $("#breederEdit").click(function () {
        $("#breederdetails").load($(this).attr("data-attr"));
        $("#breederdetails,#insurancedetails,#addInsurance,#breederEdit").hide();
        $("#showpetdetails,#showbreederdetails,#showinsurancedetails").removeClass("btn-success").addClass("btn-default");
        $("#showbreederdetails").addClass("btn-success");
        $("#breederdetails").show();
        $("#profileeditbtn").hide();
        return false;
    });

    $("#showbreederdetails").click(function () {
        $("#petdetails,#insurancedetails,#addInsurance").hide();
        $("#breederdetails").show();
        $("#profileeditbtn").hide();
        $("#breederEdit").show();
        $("#cardEdit").hide();
        $("#showpetdetails,#showbreederdetails,#showinsurancedetails").removeClass("btn-success").addClass("btn-default");
        $(this).addClass("btn-success");
    });

    $("#showpetdetails").click(function () {
        $("#breederdetails,#insurancedetails,#addInsurance").hide();
        $("#petdetails").show();
        $("#profileeditbtn").show();
        $("#breederEdit").hide();
        $("#cardEdit").show();
        $("#showpetdetails,#showbreederdetails,#showinsurancedetails").removeClass("btn-success").addClass("btn-default");
        $(this).addClass("btn-success");
    });

    $("#showinsurancedetails").click(function () {
        $("#breederdetails,#petdetails").hide();
        $("#insurancedetails,#addInsurance").show();
        $("#profileeditbtn").hide();
        $("#breederEdit").hide();
        $("#cardEdit").hide();
        $("#showpetdetails,#showbreederdetails,#addInsurance").removeClass("btn-success").addClass("btn-default");
        $(this).addClass("btn-success");
    });

    $('.NewMsgModel').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addModalNewMessage').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#newMessageForm");
            $('#addModalNewMessage').modal('show');
        });
    });
});


function setImage(input, type) {
    var result = GetTranstaltion();
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        ShowLoading();
        if (type == 'cover') {
            HideLoading();
            reader.onload = function (e) {
               
                var file = input.files[0];
                var extension = file.name.substring(file.name.lastIndexOf('.'));
                // Only process image files.
                var validFileType = ".jpeg, .jpg , .png , .bmp, .gif";
                if (validFileType.toLowerCase().indexOf(extension) < 0) {
                    var invalidFiletypeMessage = $("#hdnInvalidImagetype").val();
                    alert(invalidFiletypeMessage);
                    return false;
                } else {
                    $("#temp_image").html('<img src="' + e.target.result + '" id="tmp" />');
                    var width = $('#tmp')[0].width;
                    var height = $('#tmp')[0].height;

                    if (height < 200 || width < 900) {
                        $("#lnkSizeWarningAlert").click();
                        $("input#imageFileCover").clearInputs(true);
                        $("#imageFileCover").clearInputs(true);
                        $("#temp_image").html("");
                    }
                    else {
                        $('#imgPetCover').removeAttr("src");
                        $('#imgPetCover').attr('src', e.target.result);
                        $("#imgPetCover").addClass("headerimage ui-draggable");
                        //   $(".instructionWrap").css("visibility", "visible");
                        repositionCover();
                    }
                }
            };
        }
        else {
            HideLoading();
            reader.onload = function (e) {
                var file = input.files[0];
                var extension = file.name.substring(file.name.lastIndexOf('.'));
                // Only process image files.
                var validFileType = ".jpeg, .jpg , .png , .bmp, .gif";
                if (validFileType.toLowerCase().indexOf(extension) < 0) {
                    var invalidFiletypeMessage = $("#hdnInvalidImagetype").val();
                    alert(invalidFiletypeMessage);
                    return false;
                } else {
                    $('#imgPetProfile')
                        .attr('src', e.target.result);
                }
            };
        }
        reader.readAsDataURL(input.files[0]);
    } HideLoading();
}


function repositionCover() {
    var container = $(".picturecontainer");
    var childimage = $(".headerimage");

    childimage.draggable({
        scroll: false, axis: "y x", start: function (event, ui) {
            childimage.addClass('grabbing');
        }, stop: function (event, ui) {
            childimage.removeClass('grabbing');
        }, drag: function (event, ui) {
            //left and right threshold
            //to ease draggin images that have same width as container
            threshold = 2;//pixels
            ltop = childimage.offset().top - 1 > container.offset().top;
            lbottom = childimage.offset().top + childimage[0].offsetHeight
                      <= container.offset().top + container[0].offsetHeight;
            lleft = childimage.offset().left > container.offset().left + threshold;
            lright = childimage.offset().left + childimage[0].offsetWidth
                     < container.offset().left + container[0].offsetWidth - threshold;
            if (ltop || lbottom || lleft || lright) {
                $(this).data("uiDraggable").cancel();

                if (ltop) {
                    if (ui.position.top >= 0) {
                        $(this).offset({ top: $(this).parent().offset().top + 1 });
                    }
                }
                else if (lbottom) {
                    if (ui.position.top < 0) {
                        newtop = container.offset().top + container[0].offsetHeight
                                 - childimage[0].offsetHeight;
                    }
                }
                else if (lleft) {
                    newleft = container.offset().left;
                    $(this).offset({ left: newleft });
                }
                else if (lright) {
                    newleft = container.offset().left + container[0].offsetWidth
                              - childimage[0].offsetWidth;
                    $(this).offset({ left: newleft });
                }

            }

            $("#hdnTop").val($("#imgPetCover").css("top"));
            $("#hdnLeft").val($("#imgPetCover").css("left"));
        }
    }).parent().bind('mouseout', function () {
        childimage.trigger('mouseup');
    });
}