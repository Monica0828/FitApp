
$(document).ready(function () {
    $('#submitClass').on('click', SaveGymClass);
    $('#submitAppointment').on('click', saveAppointment);
    //$('#alertsBell').on('click', markNotificationsAsRead);
    pushNotifications();
    setInterval(pushNotifications, 30 * 1000);
    $('#alertsBell').on('hide.bs.dropdown', markNotificationsAsRead);
});

var markNotificationsAsRead = function () {
    $.ajax({
        type: "GET",
        url: "/Account/MarkNotificationsAsRead",
        success: function (data) {
            pushNotifications();
        },
        error: function (err) {
            alert(err);
        }
    });
}

var pushNotifications = function () {
    $.ajax({
        type: "GET",
        url: "/Account/SendNotification",
        dataType: "json",
        success: function (data) {
            setNotificationContent(data);
        },
        error: function (err) {
            alert(err);
        }
    });
}

var setNotificationContent = function (notifications) {
    var counterNotSeen = 0;

    $.each(notifications, function (index, value) {
        if (value.Seen == 0) counterNotSeen++;
    });
    $("#alerts").text(counterNotSeen);
    $("#notification-content").empty();


    $.each(notifications, function (index, value) {
        if (value.Seen == 0) {
            $("#notification-content").append("<a class='dropdown-item'><b>" + value.Message + "</b></a>");
        }
        else {
            $("#notification-content").append("<a class='dropdown-item'>" + value.Message + "</a>");

        }
        if (index < notifications.length - 1) {
            $("#notification-content").append("<div class='dropdown-divider'></div>");
        }
    });
};

var SaveGymClass = function () {
    var gymClass = {
        Name: $('#className').val(),
        MaxClients: $('#nrPersons').val()
    };

    $.ajax({
        type: "POST",
        url: "/Trainer/AddClass",
        data: gymClass,
        dataType: "html",
        success: function (data) {
            $("body").html(data);
            $("#notification-wrapper").html(notifySubmitClass(gymClass.Name));
            $("#notification-wrapper").fadeIn().delay(3000).fadeOut();
        },
        error: function (err) {
            alert(err);
        }
    });
}

var saveAppointment = function () {

    var id = $("#scheduleId").val();

    $.ajax({
        type: "POST",
        url: "/Client/AddAppointment",
        data: { scheduleId: id },
        dataType: "html",
        success: function (data) {
            $("body").html(data);
            $("#notification-wrapper").html(notifySubmitAppointment());
            $("#notification-wrapper").fadeIn().delay(3000).fadeOut();
        },
        error: function (err) {
            alert(err);
        }
    });

}

var notifySubmitClass = function (className) {
    var result = "<div class='alert alert-success' role='alert'>" +
        "Class " + className + " was added." +
        "</div>";
    return result;
}

var notifySubmitAppointment = function () {
    var result = "<div class='alert alert-success' role='alert'>" +
        "Resevation is done." +
        "</div>";
    return result;
}

