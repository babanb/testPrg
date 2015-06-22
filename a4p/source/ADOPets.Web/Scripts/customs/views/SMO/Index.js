$(function () {
    $('[data-tooltip="tooltip"]').tooltip()
});

$(document).ready(function () {
    $(document).off("click", "#showSmoReport").on("click", "#showSmoReport", function (e) {
        e.preventDefault();
        var url = $(this).attr('data-href');
        $.get(url, function (data) {
            $('#showModalReport').html(data);
            Initialize();
            $('#showModalReport').modal('show');
        });
    });
});

$(".myreqstatusspan").show(function () {
    var smoStatuslist = GetTranstaltionSMOStatus();
    var oItemId = $(this).attr('data-myitemid');
    if (oItemId == smoStatuslist.Open) {
        $(this).addClass('label-primary');
    }
    else if (oItemId == smoStatuslist.Assigned || oItemId == smoStatuslist.Validated) {
        $(this).removeClass('label-primary');
        $(this).addClass('label-info');
    }
    else if (oItemId == smoStatuslist.Complete) {
        $(this).removeClass('label-primary');
        $(this).addClass('label-success');
    }
    else if (oItemId == smoStatuslist.PaymentPending) {
        $(this).removeClass('label-primary');
        $(this).addClass('label-danger');
    }
});

/*****************TODO :: For filter and search of SMOs ********************/
$('#StatusFilter').change(function () {
    $("#dvNoRecords").remove();
    showAll();
    var sel = $(this).val();
    if (sel != "") {
        var filtertype = $("#StatusFilter :selected").text();//$(this).find('#StatusFilter')[0][sel].label;
        if (filtertype == "InProgress") {
            filtertype = "In Progress";
        }
        displaySMORecords(filtertype, 'SMOPetNameFilter', 'listing2');
    }
    else {
        $('div[class="listing2"]').removeAttr('style');
        filtertype = $("#SMOPetNameFilter").val();
        if (filtertype == "" || filtertype == undefined) {
            showAll();
        } else {
            filtertype = filtertype.toLowerCase();
            displaySMORecordsStartsWith(filtertype, 'StatusFilter', 'listing2');
            showPageFilter();
        }
    }
});

$("#SMOPetNameFilter").on('keyup click', function () {
    filtertype = $("#SMOPetNameFilter").val();
    showAll();
    if (filtertype != "") {
        filtertype = filtertype.toLowerCase();
        displaySMORecordsStartsWith(filtertype, 'StatusFilter', 'listing2');
    }
    else {
        $("[data-sub-container='sub_container']").removeAttr('style');
        var filtertype = $('#StatusFilter').val();
        if (filtertype == "") {
            showAll();
        }
        else {
            var filterPetType = $('#StatusFilter')[0][filtertype].label;
            if (filterPetType == "InProgress") {
                filterPetType = "In Progress";
            }
            displaySMORecords(filterPetType, 'SMOPetNameFilter', 'listing2');
            showPageFilter();
        }
    }
});

function displaySMORecordsStartsWith(filtertype, petTypeFilter, subClass) {
    var petType = $('#' + petTypeFilter).val();
    var filterPetType = "";

    if (petType != undefined && petType != "") {
        filterPetType = $('#' + petTypeFilter)[0][petType].label;
        if (filterPetType == "InProgress") {
            filterPetType = "In Progress";
        }
    }

    filterPetType = filterPetType.toLowerCase();

    if (filterPetType == undefined || filterPetType == "") {
        if ($('div[class="' + subClass + '"] :visible').find("[data-item-name='pet_name']")) {
            var param = $('.' + subClass + ' :visible');
            $.each(param.find("[data-item-name='pet_name']"), function (key, val) {
                if (val.innerHTML.toLowerCase().match("^" + filtertype)) {
                    $(this).parent().parent().parent().parent().parent().show();
                }
                else {
                    $(this).parent().parent().parent().parent().parent().hide();
                }
            });
        }
    } else {

        if ($('div[class="' + subClass + '"] :visible').find("[data-item-name='pet_name']")) {
            var param = $('.' + subClass + ' :visible');
            $.each(param.find("[data-item-name='pet_name']"), function (key, val) {
                if (val.innerHTML.toLowerCase().match("^" + filtertype)) {
                    $.each($(this).parent().parent().parent().find("[data-item-type='request_status']"), function (key, val) {
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
    }
    displayResult();
}

function displaySMORecords(filtertype, petNameFilter, subClass) {
    filterText = $("#" + petNameFilter).val();
    filterText = filterText.toLowerCase();
    filtertype = filtertype.toLowerCase();

    if (filterText == undefined || filterText == "") {
        if ($('div[class="' + subClass + '"] :visible').find("[data-item-type='request_status']")) {
            var param = $('.' + subClass + ' :visible');
            $.each(param.find("[data-item-type='request_status']"), function (key, val) {
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
        if ($('div[class="' + subClass + '"] :visible').find("[data-item-type='request_status']")) {
            var param = $('.' + subClass + ' :visible');
            $.each(param.find("[data-item-type='request_status']"), function (key, val) {
                if (filtertype == val.innerHTML.toLowerCase().trim()) {
                    $.each($(this).parent().parent().parent().find("[data-item-name='pet_name']"), function (key, val) {
                        if (val.innerHTML.toLowerCase().match("^" + filterText)) {
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
    displayResult();
}

