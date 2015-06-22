
    $("#editEconsultation").click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editModalEconsultation').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editEconsultationForm");

            $('#editModalEconsultation').modal('show');

        });
    });


function CloseDialogEconsultation() {
    location.reload();
}

function toggle_visibility1() {
    document.getElementById("econsultConfirm").style.display = 'block';
    document.getElementById("ExpertResponseSubmit").style.display = 'none';
    document.getElementById("ExpertResponseView").style.display = 'none';

    document.getElementById("Step1").className = 'consultSteptab radius3 active';
    document.getElementById("Step2").className = 'consultSteptab radius3';
}

function toggle_visibility2() {
    document.getElementById("econsultConfirm").style.display = 'none';
    document.getElementById("ExpertResponseSubmit").style.display = 'block';
    document.getElementById("ExpertResponseView").style.display = 'none';

    document.getElementById("Step1").className = 'consultSteptab radius3';
    document.getElementById("Step2").className = 'consultSteptab radius3 active';    
}

function toggle_visibility3() {
    document.getElementById("econsultConfirm").style.display = 'none';
    document.getElementById("ExpertResponseSubmit").style.display = 'none';
    document.getElementById("ExpertResponseView").style.display = 'block';

    document.getElementById("Step1").className = 'consultSteptab radius3';
    document.getElementById("Step2").className = 'consultSteptab radius3';    
}
