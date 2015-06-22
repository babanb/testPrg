jQuery(function () {

    $(".label-primary").show(function () {

        var oItemId = $(this).attr('data-myitemid');
        if (oItemId == "Open") {
            $(this).addClass('label-primary');
        }
        else if (oItemId == "InProgress") {
            $(this).removeClass('label-primary');
            $(this).addClass('label-info');
        }
        else if (oItemId == "Scheduled") {
            $(this).removeClass('label-primary');
            $(this).addClass('label-default');
        }
        else if (oItemId == "Complete") {
            $(this).removeClass('label-primary');
            $(this).addClass('label-success');
        }
        else if (oItemId == "Withdraw") {
            $(this).removeClass('label-primary');
            $(this).addClass('label-warning');
        }
        else if (oItemId == "Closed") {
            $(this).removeClass('label-primary');
            $(this).addClass('label-custom1');
        }
        else {//if (oItemId == smoStatuslist.PaymentPending) {
            $(this).removeClass('label-primary');
            $(this).addClass('label-danger');
        }
    });

    DrawTable('#EconsultationList', [0]);
    filteronSelect('#StatusFilter', 5, '#EconsultationList');
    filteronText('#ECONIDFilter', 1, '#EconsultationList');

});