﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#insertTable').click(function () {
    var t = $("input[name='__RequestVerificationToken']").val();
    var tableLayout = $('.tableLayout').val();
    var tableName = $('.tableName').val();
    var inputArray = [];

    $("form#insertTableForm :input, form#insertTableForm select").each(function () {
        var input = $(this);

        if (input.attr('type') == "text" || input.attr('type') == "number" || input.attr('class') == "custom-select") {
            if (input.attr('type') == "text") {
                inputArray.push("'" + $(this).val() + "'");
            } else {
                inputArray.push($(this).val());
            }
        }
    });

    console.log(tableLayout);

    $.ajax({
        type: "POST",
        url: "/Database/Table?handler=FormHandler",
        data: {
            "inputs": inputArray.toString(),
            "layout": tableLayout,
            "tableName": tableName
        },
        dataType: "html",
        headers: {
            "RequestVerificationToken": t
        },
        success: function () {
            console.log("SUCCESS");
            window.location.reload();
        }
    });
});