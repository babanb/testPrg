$(function () {
    $("input:radio[name=IsTrial]").click(function () {
        var value = $(this).val();
        if (value.toLowerCase() == "true") {
            $("#TrialDuration").removeAttr('readonly');
            $('label[for=PaymentType]').removeClass('required');
            $('label[for=TrialDuration]').addClass('required');
        }
        else {
            $("#TrialDuration").attr('readonly', 'true');
            $('label[for=PaymentType]').addClass('required');
            $('label[for=TrialDuration]').removeClass('required');
        }
    });

    $("input:radio[name=IsTrial]:checked").each(function () {
        var value = $(this).val();
        if (value.toLowerCase() == "true") {
            $("#TrialDuration").removeAttr('readonly');
            $('label[for=PaymentType]').removeClass('required');
            $('label[for=TrialDuration]').addClass('required');
        }
        else {
            $("#TrialDuration").attr('readonly', 'true');
            $('label[for=PaymentType]').addClass('required');
            $('label[for=TrialDuration]').removeClass('required');
        }
    });
});