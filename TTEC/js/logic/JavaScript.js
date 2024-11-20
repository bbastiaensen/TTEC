$(document).ready(function () {
    $("#blur").hide();
    $("#test2").click(function () {
        $("#blur").fadeIn();
        $("#mySidenav").css("width", "45%");
    });
    $("#test, #blur").click(function () {
        $("#blur").fadeOut();
        $("#mySidenav").css("width", "0");
    });
});