$(document).ready(function () {
    $("#ddlCategory").onchange(function () {
        // Home is your controller, Index is your action name
        $("#container").load("@Url.Action('Dashboard_Post','Home')",
            function (response, status, xhr) {
                if (status == "error") {
                    alert("An error occurred while loading the results.");
                }
            });
    });
});