var is_firefox = navigator.userAgent.indexOf('Firefox') > -1;

$(function () {
    $('[data-tooltip="tooltip"]').tooltip()
    deleteConfirmation();
    $("#UserType").change(function () {
        showData();
    });

    DrawTable('#TblUsers');
    filteronSelect('#UserType', 0, '#TblUsers')
    filteronSelect('#Specility', 5, '#TblUsers')
    filteronSelect('#Promocode', 1, '#TblUsers')
    filteronText('#UEmail', 6, '#TblUsers');
    filteronPlanAndPromocode('#Promocode', 1, '#TblUsers')
    filteronPlanAndPromocode('#Plan', 7, '#TblUsers')

    function filteronPlanAndPromocode(idSelect, columnFilter, IdTable) {
        $(idSelect).on('change', function () {
            if ($(this).find("option:selected").val() == 0) {
                filterColumn(IdTable, columnFilter, '');
            }
            else {
                var val = $.fn.dataTable.util.escapeRegex(
                        $(this).find("option:selected").text()
                    );
                $(IdTable).DataTable().column(columnFilter)
                    .search(val ? '^' + val + '$' : '', true, false)
                    .draw();
            }
        });
    }


    $("#Promocode").change(function () {
        ChangePlanListByPromoCode();
    });
});


function ChangePlanListByPromoCode() {
    var promocode = $("#Promocode option:selected").text();
    var promocodesVal = $('#Promocode option:selected').val();
    promocodesVal = promocodesVal || 0;
    if (promocodesVal > 0) {
        var subItems = "";
        $.getJSON(GetApplicationPath() + "Users/GetPlansByPromocode", { promocode: promocode, isGetAll: true },
                function (data) {
                    subItems += "<option value='0'>" + data.Description + "</option>";
                    $.each(data.Items, function (index, item) {
                        subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                    });

                    $("#Plan").html(subItems);
                });
    }
}



$("#UCenterID").on('keyup click', function () {
    var UCenterID = $('#UCenterID').val();
    $('#TblUsers').dataTable().fnFilter('^' + UCenterID + '.*', 8, true);
});

$("#UserID").on('keyup click', function () {
    var UserID = $('#UserID').val();
    $('#TblUsers').dataTable().fnFilter('^' + UserID + '.*', 2, true);
});

$("#UFirstName").on('keyup click', function () {
    var UFirstName = $('#UFirstName').val();
    $('#TblUsers').dataTable().fnFilter('^' + UFirstName + '.*', 3, true);
});

$("#ULastName").on('keyup click', function () {
    var ULastName = $('#ULastName').val();
    $('#TblUsers').dataTable().fnFilter('^' + ULastName + '.*', 4, true);
});

function showData() {
    if ($('#UserType').val() == "6" || $('#UserType').val() == "1") {
        $("#UCenterID").val("");
        $("#Specility option:first").attr("selected", true);
        if (is_firefox)
            $("#Specility").val("");
        $('#Specility').trigger('change');

        $("#spacialityField, #centerIDField").hide();
    } else {
        $("#spacialityField, #centerIDField").show();
    }
    if ($('#UserType').val() == "6" || $('#UserType').val() == "") {
        $("#promoCode, #planFilter, #emailIDFilter").show();
    } else {
        $("#Promocode option:first").attr("selected", true);
        $("#Plan option:first").attr("selected", true);
        if (is_firefox) {
            $("#Promocode").val("");
            $("#Plan").val("");
        }
        $('#Promocode').trigger('change');
        $('#Plan').trigger('change');

        $("#UEmail").val("");

        $("#promoCode, #planFilter, #emailIDFilter").hide();
    }
}