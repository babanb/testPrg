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

var popupBlockerChecker = {
    check: function (popup_window) {
        var _scope = this;
        if (popup_window) {
            if (/chrome/.test(navigator.userAgent.toLowerCase())) {
                setTimeout(function () {
                    _scope._is_popup_blocked(_scope, popup_window);
                }, 200);
            } else {
                popup_window.onload = function () {
                    _scope._is_popup_blocked(_scope, popup_window);
                };
            }
        } else {
            _scope._displayError();
        }
    },
    _is_popup_blocked: function (scope, popup_window) {
        if ((popup_window.innerHeight > 0) == false) { scope._displayError(); }
    },
    _displayError: function () {
        //alert("Popup Blocker is enabled! Please add this site to your exception list.");
        $.get("/Account/Index/", function (data) {
            $('#popupblockModel').html(data);
            $('#popupblockModel').modal('show');
        });

    }
};