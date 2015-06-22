//for page loader
$(window).load(function () {
    $('#loader_container').fadeOut(1000);

    //for tooltip
    $(function () { $("[data-toggle='tooltip']").tooltip(); $("[data-tooltip='tooltip']").tooltip(); });

    //for animation
    jQuery('.fadeleft1').addClass("hidden-ani").viewportChecker({
        classToAdd: 'visible-ani animated fadeInLeft', // Class to add to the elements when they are visible
        offset: 100
    });
    jQuery('.faderight').addClass("hidden-ani").viewportChecker({
        classToAdd: 'visible-ani animated fadeInRight', // Class to add to the elements when they are visible
        offset: 100
    });
    jQuery('.fadetops').addClass("hidden-ani").viewportChecker({
        classToAdd: 'visible-ani animated fadeInDown', // Class to add to the elements when they are visible
        offset: 100
    });
    jQuery('.zoomin').addClass("hidden-ani").viewportChecker({
        classToAdd: 'visible-ani animated zoomIn', // Class to add to the elements when they are visible
        offset: 100
    });

    // for sub menu	
    $(".open-submenu").click(function (e) {
        $(this).next(".submenu").slideToggle("slow");
        $(this).toggleClass("hide-submenu");
    });
    $("#linksIcon").click(function (e) {
        $("#linksSubmenu").slideToggle("slow");
        $("#linksSubmenu").toggleClass("hide-submenu");
    });

    $("#linksLogo a").click(function () {
        var weburl = $(this).attr('href');
        $("#agreeBtn").click(function (e) {
            if (weburl != "") {
                window.open(weburl);
                weburl = "";

                $("#confirmModal").find("button[data-dismiss].close").click();
            }
            return false;
        });
    });

    $("#notibell").click(function (e) {
        var msgcount = $("#hdnMessageCount").val();
        if ((parseInt(msgcount)) > 0) {
            $("#notibell i, #notibell span").removeClass('animated swing continuously bounce');
            // $("#notibell span").hide();
            $("#notification-container").show();
            $("#notification-container").addClass('fadeIn');
            $("#notification-container").removeClass('fadeOut');
            $("#notification-container .list-group").addClass('flipInX');
            $("#notification-container .list-group").removeClass('flipOutX');
        }
        else {
           // console.log(11);
            window.location = "/Message/NotificationHistoryIndex";
            return false;
        }
    });

    function aniout() {
        $("#notification-container").removeClass('fadeIn');
        $("#notification-container").addClass('fadeOut');
        $("#notification-container .list-group").removeClass('flipInX');
        $("#notification-container .list-group").addClass('flipOutX');
    };

    $("#notificntClose").click(function (e) {
        aniout();
        // $("#notibell i, #notibell span").addClass('animated swing continuously bounce');
        $("#notibell i").addClass('animated swing continuously');
        $("#notibell span").addClass('animated continuously bounce');
        $("#notification-container").fadeOut(1000);
    });

    $("#notification-container").mouseup(function (e) {
        var subject = $("#notification-container .list-group");
        if (e.target.id != subject.attr('id') && !subject.has(e.target).length) {
            aniout();
            $("#notification-container").fadeOut(1000);
        }
    });
});


$(window).on('load resize', function () {
    //for content div margin
    $("#content").css("margin-left", $("#leftSidebar").width() + 20);
    $("#content").css("margin-right", $("#rightSidebar").width() + 35);

    //for rightSidebar hide, show and animation

    if ($("body").width() < "1100") {
        $("#rightSidebar").hide();
        $("#content").css("margin-right", "20px");
        $("#rightSidebar").css("right", -$("#rightSidebar").width() - 15);
        $("#hiderightsidebar").hide();
        $("#showrightsidebar").show();

        $("#showrightsidebar").click(function (e) {
            $("#rightSidebar").css("box-shadow", "0 0 5px 0 rgba(65,65,65,1.00)");
            $("#rightSidebar").show();
            $("#hiderightsidebar4lap").show();
            $("#rightSidebar").addClass("new-style");
            $("#content").addClass("faderight2");
            $("#showrightsidebar").hide();
            $("#rightSidebar").css("right", "0");
            $("#hiderightsidebar").show();

        });
        $("#hiderightsidebar, #hiderightsidebar4lap").click(function (e) {
            $("#showrightsidebar").show();
            $("#hiderightsidebar4lap").hide();
            $("#content").css("margin-right", "20px");
            $("#content").addClass("faderight2");
            $("#rightSidebar").css("right", -$("#rightSidebar").width() - 15);
            $("#hiderightsidebar").hide();
            $("#rightSidebar").css("box-shadow", "none");
        });
    }
    else {
        $("#hiderightsidebar").click(function (e) {
            $("#showrightsidebar").show();
            $("#content").css("margin-right", "20px");
            $("#content").addClass("faderight2");
            $("#rightSidebar").css("right", -$("#rightSidebar").width() - 15);
            $("#hiderightsidebar").hide();
        });
        $("#showrightsidebar").click(function (e) {
            $("#showrightsidebar").hide();
            $("#content").css("margin-right", $("#rightSidebar").width() + 35);
            $("#rightSidebar").css("right", "0");
            $("#hiderightsidebar").show();
        });
    }
});

$(window).on('load resize', function () {
    var width = $(window).width(), height = $(window).height();
    $('#jqWidth').html(width);
    $('#jqHeight').html(height);
});

// for form div show hide	
jQuery('#bestcontactoption').on('change', function () {
    if (jQuery(this).val() == 'email') {
        jQuery('#enteremail').show();
        jQuery('#enterphone').hide();
    } else {
        jQuery('#enteremail').hide();
        jQuery('#enterphone').show();
    }
});

$("#selectCurrentvetlink").hide();
$("form").on('change click', 'input:radio', function (e) {
    if ($("#optionsRadios1").is(':checked')) {
        $('#selectCurrentvetlink').show();
        $("#optionsRadios2").prop('checked', false);
    } else {
        $("#optionsRadios1").prop('checked', false);
        $('#selectCurrentvetlink').hide();
        $("#optionsRadios2").prop('checked', true);
    }
});
$("#optionsRadios2").click(function (e) {
    $("#optionsRadios1").prop('checked', false);
    $('#selectCurrentvetlink').hide();
    $("#optionsRadios2").prop('checked', true);
});

//for hideing tour popover
$('body').on('click', function (e) {
    $('[data-role="next"]').each(function () {
        //the 'is' for buttons that trigger popups
        //the 'has' for icons within a button that triggers a popup
        if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
            $(this).popover('hide');
            $('[data-role="end"]').click();
        }
    });
});

//for countdown and date 
var result = GetTranstaltion();
tday = new Array(result.Sunday, result.Monday, result.Tuesday, result.Wednesday, result.Thursday, result.Friday, result.Saturday);
tmonth = new Array(result.January, result.February, result.March, result.April, result.May, result.June, result.July, result.August, result.September, result.October, result.November, result.December);

function GetClock(dateNew) {
    var domain = GetDomainName();
    var culture = GetCurrentCulture();

    if (domain == 'French' || domain == 'Portuguese') {
        if (culture == "fr-FR" || culture == 'pt-BR') {
            d = dateNew.substring(dateNew.lastIndexOf(' '));
            nday = dateNew.replace(dateNew.substring(dateNew.lastIndexOf(' ')).trim(), "");
            nmin = dateNew.substring(dateNew.lastIndexOf(' ')).trim();
            document.getElementById('clockbox').innerHTML = "" + nday + "<br /> <span>" + nmin + "</span>";
        }
        else {
            var d = new Date(Date.parse(dateNew));
            nday = d.getDay();
            nmonth = d.getMonth();
            ndate = d.getDate();
            nyear = d.getYear();
            nhour = d.getHours();
            nmin = d.getMinutes();
            nsec = d.getSeconds();
            var ap = "";
            if (nyear < 1000) nyear = nyear + 1900;
            var datePartSplit = dateNew.split(nyear);
            if (nhour == 0) { ap = " AM"; nhour = 12; }
            else if (nhour <= 11) { ap = " AM"; }
            else if (nhour == 12) { ap = " PM"; }
            else if (nhour >= 13) { ap = " PM"; nhour -= 12; }
            if (nmin <= 9) { nmin = "0" + nmin; }
            if (nsec <= 9) { nsec = "0" + nsec; }
            document.getElementById('clockbox').innerHTML = "" + tday[nday] + ", " + tmonth[nmonth] + " " + ndate + ", " + nyear + "<br /> <span>" + nhour + ":" + nmin + ap + "</span>";

        }
    } else {
        if (culture == "fr-FR" || culture == 'pt-BR') {
            d = dateNew.substring(dateNew.lastIndexOf(' '));
            nday = dateNew.replace(dateNew.substring(dateNew.lastIndexOf(' ')).trim(), "");
            nmin = dateNew.substring(dateNew.lastIndexOf(' ')).trim();
            document.getElementById('clockbox').innerHTML = "" + nday + "<br /> <span>" + nmin + "</span>";
        } else {
            var d = new Date(Date.parse(dateNew));
            nday = d.getDay();
            nmonth = d.getMonth();
            ndate = d.getDate();
            nyear = d.getYear();
            nhour = d.getHours();
            nmin = d.getMinutes();
            nsec = d.getSeconds();
            var ap = "";
            if (nyear < 1000) nyear = nyear + 1900;
            var datePartSplit = dateNew.split(nyear);
            if (nhour == 0) { ap = " AM"; nhour = 12; }
            else if (nhour <= 11) { ap = " AM"; }
            else if (nhour == 12) { ap = " PM"; }
            else if (nhour >= 13) { ap = " PM"; nhour -= 12; }
            if (nmin <= 9) { nmin = "0" + nmin; }
            if (nsec <= 9) { nsec = "0" + nsec; }
            document.getElementById('clockbox').innerHTML = "" + tday[nday] + ", " + tmonth[nmonth] + " " + ndate + ", " + nyear + "<br /> <span>" + nhour + ":" + nmin + ap + "</span>";
        }
    }
    setTimeout("GetClock('" + dateNew + "')", 1000);
}

$('.cardexdate').datetimepicker({
    pickTime: false,
    autoclose: true,
});

$('.timepicker').datetimepicker({
    pickDate: false,
    pick12HourFormat: true,
    autoclose: true
});

$('.timepicker')
    .on('dp.change dp.show', function (e) {
        $('#neconsultationForm').bootstrapValidator('revalidateField', 'timePicker');
    });
$('.cardexdate')
    .on('dp.change dp.show', function (e) {
        $('#neconsultationbillForm').bootstrapValidator('revalidateField', 'exdate');
    });


//for pet filter 
$("#showfilter").click(function (e) {
    $("#petfilter").slideToggle("slow");
});

//for MR main page 
$(".mrmaintab").hover(function (e) {
    $(".mrmainicons").removeClass('animated');
    $(".mrmainicons").removeClass('rotateIn');
});

$(window).on('load resize', function () {
    //$("#mrsubcatnavlevel2 .nav-tabs").tabdrop();
    //$("#table_id").DataTable();
    $(".showeditModel").click(function (e) {
        $('#editCondition').modal('toggle');
    });
    $(".opendelete").click(function (e) {
        $('#confirmbx').modal('toggle');
    });
});



//for Medication Pop over
$(".showmedidetails").mouseover(function (e) {
    $(this).next(".medicationpopover").show();
});
$(".medicationpopoverperent").mouseleave(function (e) {
    $(".medicationpopover").hide();
});

//for share button
$("#sharemedia, #sharebtbx").mouseover(function (e) {
    $("#sharebtbx").show();
});
$("#sharemediaPerent").mouseleave(function (e) {
    $("#sharebtbx").hide();
});

//for billing address

$('#checkbox1').change(function () {
    if (this.checked) {
        $('#plaintext-add').show();
        $('#billing-add').hide();
    }
    else {
        $('#plaintext-add').hide();
        $('#billing-add').show();

    }
});
//for alert hide
//var $alert = $('.alert').alert()
//setTimeout(function () {
//    $alert.alert('close')
//}, 3000)


//for custom scroller
$('.customscroll').enscroll({
    showOnHover: true,
    verticalTrackClass: 'track3',
    verticalHandleClass: 'handle3'
});
