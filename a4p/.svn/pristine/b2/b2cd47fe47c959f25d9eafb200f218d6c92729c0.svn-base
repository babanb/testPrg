$(function () {

    $('.editFoodPlan').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#editModalFoodPlan').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#editFoodPlanForm");
            $('#editModalFoodPlan').modal('show');
        });
    });


    $('.deleteFoodPlan').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#deleteModalFoodPlan').html(data);
            $('#deleteModalFoodPlan').modal('show');
        });
    });

    DrawTable('#Table_foodplan');

});