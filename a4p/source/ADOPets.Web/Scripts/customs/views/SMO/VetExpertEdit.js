/*****************TODO :: For filter and search of SMOs ********************/
$('#VetSpecialityFilter').change(function () {
    $("#dvNoRecords").remove();
    showAll();
    var sel = $(this).val();
    if (sel != "") {
        var filtertype = $('#VetSpecialityFilter')[0][sel].label;

        displaySMORecords(filtertype, 'ExpertFirstNameFilter', "ExpertLastNameFilter", 'listing2');
    }
    else {
        $('div[class="listing2"]').removeAttr('style');
        filtertype = $("#ExpertFirstNameFilter").val();
        filtertype1 = $("#ExpertLastNameFilter").val();
        if ((filtertype == "" || filtertype == undefined) && (filtertype1 == "" || filtertype1 == undefined)) {
            showAll();
        } else {
            filtertype = filtertype.toLowerCase();
            filtertype1 = filtertype1.toLowerCase();
            displaySMORecordsStartsWith(filtertype, filtertype1, 'VetSpecialityFilter', 'listing2');
            showPageFilter();
        }
    }
});

$("#ExpertFirstNameFilter").on('keyup click', function () {
    filtertype = $("#ExpertFirstNameFilter").val();
    filtertype1 = $("#ExpertLastNameFilter").val();
    showAll();
    if (filtertype != "") {
        filtertype = filtertype.toLowerCase();
        filtertype1 = filtertype1.toLowerCase();
        displaySMORecordsStartsWith(filtertype, filtertype1, 'VetSpecialityFilter', 'listing2');
    }
    else {
        $("[data-sub-container='sub_container']").removeAttr('style');
        var filtertype = $('#VetSpecialityFilter').val();
        if (filtertype == "") {
            showAll();
        }
        else {
            var filterPetType = $('#VetSpecialityFilter')[0][filtertype].label;

            displaySMORecords(filterPetType, 'ExpertFirstNameFilter', "ExpertLastNameFilter", 'listing2');
            showPageFilter();
        }
    }
});

$("#ExpertLastNameFilter").on('keyup click', function () {

    filtertype = $("#ExpertFirstNameFilter").val();
    filtertype1 = $("#ExpertLastNameFilter").val();
    showAll();
    if (filtertype1 != "") {
        filtertype = filtertype.toLowerCase();
        filtertype1 = filtertype1.toLowerCase();
        displaySMORecordsStartsWith(filtertype, filtertype1, 'VetSpecialityFilter', 'listing2');
    }
    else {
        $("[data-sub-container='sub_container']").removeAttr('style');
        var filtertype = $('#VetSpecialityFilter').val();
        if (filtertype == "") {
            showAll();
        }
        else {
            var filterPetType = $('#VetSpecialityFilter')[0][filtertype].label;

            displaySMORecords(filterPetType, 'ExpertFirstNameFilter', "ExpertLastNameFilter", 'listing2');
            showPageFilter();
        }
    }
});

function displaySMORecordsStartsWith(filtertype, filtertype1, petTypeFilter, subClass) {

    var petType = $('#' + petTypeFilter).val();
    var filterPetType = "";

    if (petType != undefined && petType != "") {
        filterPetType = $('#' + petTypeFilter)[0][petType].label;

    }

    filterPetType = filterPetType.toLowerCase();

    if (filterPetType == undefined || filterPetType == "") {
        if ((filtertype != undefined || filtertype != "") && (filtertype1 == undefined || filtertype1 == "")) {
            if ($('div[class="' + subClass + '"] :visible').find("[data-item-name='first_name']")) {
                var param = $('.' + subClass + ' :visible');

                $.each(param.find("[data-item-name='first_name']"), function (key, val) {


                    if (val.innerHTML.toLowerCase().trim().match("^" + filtertype)) {
                        $(this).parent().parent().parent().parent().parent().show();
                    }
                    else {
                        $(this).parent().parent().parent().parent().parent().hide();
                    }
                });
            }
        }
        else if ((filtertype == undefined || filtertype == "") && (filtertype1 != undefined || filtertype1 != "")) {
            if ($('div[class="' + subClass + '"] :visible').find("[data-item-name='last_name']")) {
                var param = $('.' + subClass + ' :visible');

                $.each(param.find("[data-item-name='last_name']"), function (key, val) {


                    if (val.innerHTML.toLowerCase().trim().match("^" + filtertype1)) {
                        $(this).parent().parent().parent().parent().parent().show();
                    }
                    else {
                        $(this).parent().parent().parent().parent().parent().hide();
                    }
                });
            }
        }
        else {
            if ($('div[class="' + subClass + '"] :visible').find("[data-item-name='first_name']")) {
                var param = $('.' + subClass + ' :visible');
                $.each(param.find("[data-item-name='first_name']"), function (key, val) {
                    if (val.innerHTML.toLowerCase().trim().match("^" + filtertype)) {

                        $.each($(this).parent().parent().parent().find("[data-item-name='last_name']"), function (key, val) {
                            if (val.innerHTML.toLowerCase().trim().match("^" + filtertype1)) {

                                $(this).parent().parent().parent().parent().parent().show();
                            } else {
                                $(this).parent().parent().parent().parent().parent().hide();
                            }
                        });
                    }
                    else {
                        $(this).parent().parent().parent().parent().parent().hide();
                    }
                });
            }
        }
    } else {

        if ($('div[class="' + subClass + '"] :visible').find("[data-item-name='first_name']")) {
            var param = $('.' + subClass + ' :visible');
            $.each(param.find("[data-item-name='first_name']"), function (key, val) {
                if (val.innerHTML.toLowerCase().trim().match("^" + filtertype)) {

                    $.each(param.find("[data-item-name='last_name']"), function (key, val) {

                        if (val.innerHTML.toLowerCase().trim().match("^" + filtertype1)) {
                            $.each($(this).parent().parent().parent().find("[data-item-type='speciality']"), function (key, val) {
                                if (filterPetType == val.innerHTML.toLowerCase().trim()) {
                                    $(this).parent().parent().parent().parent().parent().show();
                                } else {
                                    $(this).parent().parent().parent().parent().parent().hide();
                                }
                            });
                        }
                        else {
                            $(this).parent().parent().parent().parent().parent().hide();
                        }
                    });
                }
                else {
                    $(this).parent().parent().parent().parent().parent().hide();
                }
            });
        }
        //}
    }
    displayResult();
}

function displaySMORecords(filtertype, petNameFilter, lastNamefilter, subClass) {
    filterText = $("#" + petNameFilter).val();
    filterText = filterText.toLowerCase();
    filterText1 = $("#" + lastNamefilter).val();
    filterText1 = filterText1.toLowerCase();

    filtertype = filtertype.toLowerCase();

    if ((filterText == undefined || filterText == "") && (filterText1 == undefined || filterText1 == "")) {
        if ($('div[class="' + subClass + '"] :visible').find("[data-item-type='speciality']")) {
            var param = $('.' + subClass + ' :visible');
            $.each(param.find("[data-item-type='speciality']"), function (key, val) {

                if (filtertype == val.innerHTML.toLowerCase().trim()) {
                    $(this).parent().parent().parent().parent().parent().show();
                }
                else {
                    $(this).parent().parent().parent().parent().parent().hide();
                }
            });
        }
    }
    else {

        if ($('div[class="' + subClass + '"] :visible').find("[data-item-type='speciality']")) {
            var param = $('.' + subClass + ' :visible');

            $.each(param.find("[data-item-type='speciality']"), function (key, val) {

                //if ((filterText1 != undefined || filterText1 != "") && (filterText == undefined || filterText == "")){
                if ((filterText1 == undefined || filterText1 == "") && (filterText != undefined || filterText != "")) {
                    if (filtertype == val.innerHTML.toLowerCase().trim()) {

                        $.each($(this).parent().parent().parent().find("[data-item-name='first_name']"), function (key, val) {

                            if (val.innerHTML.toLowerCase().trim().match("^" + filterText)) {

                                $(this).parent().parent().parent().parent().parent().show();
                            } else {
                                $(this).parent().parent().parent().parent().parent().hide();
                            }
                        });
                    }
                    else {
                        $(this).parent().parent().parent().parent().parent().hide();
                    }
                }
                else if ((filterText1 != undefined || filterText1 != "") && (filterText == undefined || filterText == "")) {
                    //if ((filterText1 == undefined || filterText1 == "") && (filterText != undefined || filterText != "")) {
                    if (filtertype == val.innerHTML.toLowerCase().trim()) {

                        $.each($(this).parent().parent().parent().find("[data-item-name='last_name']"), function (key, val) {

                            if (val.innerHTML.toLowerCase().trim().match("^" + filterText1)) {

                                $(this).parent().parent().parent().parent().parent().show();
                            } else {
                                $(this).parent().parent().parent().parent().parent().hide();
                            }
                        });
                    }
                    else {
                        $(this).parent().parent().parent().parent().parent().hide();
                    }
                }

                else {
                    if ($('div[class="' + subClass + '"] :visible').find("[data-item-type='speciality']")) {
                        var param = $('.' + subClass + ' :visible');
                        $.each($(this).parent().parent().parent().find("[data-item-type='speciality']"), function (key, val) {
                            if (filtertype == val.innerHTML.toLowerCase().trim()) {

                                $.each(param.find("[data-item-name='first_name']"), function (key, val) {

                                    if (val.innerHTML.toLowerCase().trim().match("^" + filterText)) {

                                        $.each(param.find("[data-item-name='last_name']"), function (key, val) {

                                            if (val.innerHTML.toLowerCase().trim().match("^" + filterText1)) {
                                                $(this).parent().parent().parent().parent().parent().show();
                                            }
                                            else {
                                                $(this).parent().parent().parent().parent().parent().hide();
                                            }
                                        });
                                    }
                                    else {
                                        $(this).parent().parent().parent().parent().parent().hide();
                                    }
                                });
                            }
                            else {
                                $(this).parent().parent().parent().parent().parent().hide();
                            }
                        });

                    }
                }
            });
        }
    }
    displayResult();
}

//$(document).off("click", "#expertclose-bt").on("click", "#expertclose-bt", function (e) {
//    e.preventDefault();
//    var lstExpert = new Array();
//    var model = new Object();

//    for (var a = 0; a < $('.col-md-1').length; a++) {
//        if ($('.col-md-1')[a].children[2].value.toLowerCase() == 'true') {
//            var VetExpertID = $('.col-md-1')[a].children[2].className;
//            var SmoId = $('.col-md-1')[a].children[1].value;
//            var gallery = new Object();
//            gallery.VetExpertID = VetExpertID;
//            gallery.SMOId = SmoId;
//            lstExpert.push(gallery);
//        }

//    }
//    model.lstExpertRel = lstExpert;
//    var data = '{model:' + JSON.stringify(model) + '}';
//    $.ajax({
//        url: $('#UrlSelectExpert').val(),
//        data: data,
//        processData: false,
//        contentType: "application/json; charset=utf-8",
//        type: 'POST',
//        success: function (data) {
//            $("#showExpertList").html(data);
//        },
//        error: function (req, status, error) {

//        }
//    });
//});

//function callBtnClick(id) {
//    for (var a = 0; a < $('.checkExpert').parent().length; a++) {
//        if ($(".checkExpert").parent()[a].attributes[3].nodeValue.trim() == id) {
//            var doctitle = $("#selectPetAnchor_" + id);
//            var doc = $('.fa-check-square');
//            if ($('#CheckedStatus_' + id).val() == 'False') {
//                $('#CheckedStatus_' + id).attr('value', 'True');
//                $(this).attr('data-mylabel', 'True');
//                doctitle.removeClass("fa-square-o").addClass("fa-check-square");
//            }
//            else {
//                $('#CheckedStatus_' + id).attr('value', 'False');
//                $(this).attr('data-mylabel', 'False');
//                doctitle.addClass("fa-square-o").removeClass("fa-check-square");
//            }
//        }
//    }

//}



function callBtnClick(id) {
    var oItemId = id;
    var doctitle = $("#" + id).find("i[id*=selectPetAnchor]");
    $('#CheckedStatus').attr('value', 'True');
    $(this).attr('data-mylabel', 'True');
    ShowLoading();
    $.ajax({
        url: $('#UrlSelectExpert').val(),
        data: { id: oItemId },
        type: 'Get',
        success: function (data) {
            HideLoading();
            doctitle.removeClass("fa-square-o").addClass("fa-check-square");
            $("#showExpertList").html(data);
            $("#" + id).removeAttr('onclick');

            var tbllength = $("#table_id tbody tr").length;
            if (tbllength > 0) {
                $("#lnkExpertCommitteeButton").removeClass("disabled");
                var linkURL = $("#hdnExpertCommitteeURL").val();
                $("#lnkExpertCommitteeButton").removeAttr("href").attr("href", linkURL);
            }
            else {
                $("#lnkExpertCommitteeButton").addClass("disabled");
            }
        },
        error: function (req, status, error) {
            HideLoading();
            doctitle.removeClass("fa-check-square").addClass("fa-square-o");
            $("#" + id).addAttr('onclick', 'callBtnClick(' + callBtnClick + ')');
        }
    });
}

