$(document).ready(function () {
    $("#blur").hide();
    $("#open-nav-mobile").click(function () {
        $("#blur").fadeIn();
        $("#mySidenav").css("width", "45%");
    });
    $("#close-nav-mobile, #blur").click(function () {
        $("#blur").fadeOut();
        $("#mySidenav").css("width", "0");
    });
});