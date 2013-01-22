$(function () {

    var addQsoEditRow = function () {
        var qsoEntryRow = $("#qsoEntryRow");
        var newRow = qsoEntryRow.clone(false);

        qsoEntryRow.attr("id", null);
        qsoEntryRow.addClass("unsavedQsoRow");
        qsoEntryRow.find(".qsocallsign").off("keypress.makenewrow");

        $("#qsotable tbody").append(newRow);
        newRow.attr("id", "qsoEntryRow");
        var newcallsignbox = newRow.find(".qsocallsign");
        newRow.find(".qsocallsign").on("keypress.makenewrow", addQsoEditRow);
        newRow.find(".qsodate").on("focus.saveoldrows", saveRows);
    };
    $(".qsocallsign").on("keypress.makenewrow", addQsoEditRow);

    var saveRows = function () {
        $("#qsotable .unsavedQsoRow").each(function () { saveRow($(this)); });
    };

    var saveRow = function (row) {
        var rowData = {
            "date": row.find(".qsodate").val(),
            "time": row.find(".qsotime").val(),
            "band": row.find(".qsoband").val(),
            "mode": row.find(".qsomode").val(),
            "callsign": row.find(".qsocallsign").val(),
            "rsttx": row.find(".qsorsttx").val(),
            "rstrx": row.find(".qsorstrx").val()
        };
        console.log(JSON.stringify(rowData));

        $.ajax({
            url: "/logentry/addqso",
            data: rowData,
            success: function (e) { staticifyRow(row, rowData, e); },
            type: "POST"
        });
        staticifyRow(row, rowData);
    };

    var staticifyRow = function (row, rowData, e) {
        console.log(e);
        row.removeClass("unsavedQsoRow");
        row.html("");
        row.append("<td>" + rowData.date + "</td>");
        row.append("<td>" + rowData.time + "</td>");
        row.append("<td>" + rowData.band + "</td>");
        row.append("<td>" + rowData.mode + "</td>");
        row.append("<td>" + rowData.callsign + "</td>");
        row.append("<td>" + rowData.rsttx + "</td>");
        row.append("<td>" + rowData.rstrx + "</td>");
    };
});