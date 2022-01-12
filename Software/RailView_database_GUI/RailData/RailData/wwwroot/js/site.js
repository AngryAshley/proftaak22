// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#insertTable').click(function () {
    var t = $("input[name='__RequestVerificationToken']").val(); // Get the verification token required.
    var tableLayout = $('.tableLayout').val(); // Get the table layout put inside an input.
    var tableName = $('.tableName').val(); // Get the table name put inside an input.
    var inputArray = [];

    $("form#insertTableForm :input, form#insertTableForm select").each(function () {
        // Get each input type needed and put them into a variable.
        var input = $(this);

        if (input.attr('type') == "text" || input.attr('type') == "number" || input.attr('class') == "custom-select") {
            // Push every variable (except for timestamp/datetime) into the inputArray.
            if (input.attr('type') == "text") {
                inputArray.push("'" + $(this).val() + "'");
            } else {
                inputArray.push($(this).val());
            }
        }
    });

    // Send the data via Ajax to the code-behind FormHandler
    $.ajax({
        type: "POST",
        url: "/Database/Table?handler=FormHandler",
        data: { // The JSON object of data send. The names are the parameters of the method.
            "inputs": inputArray.toString(),
            "layout": tableLayout,
            "tableName": tableName
        },
        dataType: "html",
        headers: {
            "RequestVerificationToken": t // Send the verification token with the request.
        },
        success: function () {
            console.log("SUCCESS");
            window.location.reload();
        }
    });
});

$('.delete-btn').click(function () {
    var t = $("input[name='__RequestVerificationToken']").val();
    var tableName = $('.tableName').val();
    var tableLayout = $('.tableLayout').val();
    var getFirst = tableLayout.split(',')[0]; // Get the first column name of the table selected.
    var id = $(this).attr('id'); // Get the id of the selected record.

    if (confirm("Are you sure you want to delete this record?")) {
        // Send the data via Ajax to the code-behind DeleteRecord after the user confirms the delete.
        $.ajax({
            type: "POST",
            url: "/Database/Table?handler=DeleteRecord",
            data: { // The JSON object of data send. The names are the parameters of the method.
                "id": id,
                "tableName": tableName,
                "firstCol": getFirst
            },
            dataType: "html",
            headers: {
                "RequestVerificationToken": t // Send the verification token with the request.
            },
            success: function () {
                console.log("SUCCESS");
                window.location.reload();
            }
        });
    }
});