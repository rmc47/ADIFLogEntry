$(function () {

    var initRowEvents = function (row) {
        row.find(".qsocallsign").on("keypress.makenewrow", addQsoEditRow);
        row.find("input,select").on("focus.saveoldrows", saveRows);
        row.find(".qsomode").on("change.updatereport", function (evt) { updateReport(evt, row); });
        row.find(".qsocallsign").on("blur.uppercasecallsign", upperCaseCallsign);
    };

    var clearRowEvents = function(row) {
        row.find(".qsocallsign").off("keypress.makenewrow");
        row.find("input,select").off("focus.saveoldrows");
    };

    var addQsoEditRow = function () {
        var qsoEntryRow = $("#qsoEntryRow");
        var newRow = qsoEntryRow.clone(false);

        qsoEntryRow.attr("id", null);
        qsoEntryRow.addClass("unsavedQsoRow");
        clearRowEvents(qsoEntryRow);

        $("#qsotable tbody").append(newRow);
        newRow.attr("id", "qsoEntryRow");

        // Translate over the things that stay the same that don't automatically...
        newRow.find(".qsoband").val(qsoEntryRow.find(".qsoband").val());
        newRow.find(".qsomode").val(qsoEntryRow.find(".qsomode").val());

        initRowEvents(newRow);
    };

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
        //console.log(e);
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

    var updateReport = function (evt, row) {
        var newReport = reportForMode(row.find(".qsomode").val());
        row.find(".qsorsttx").val(newReport);
        row.find(".qsorstrx").val(newReport);
    };

    var reportForMode = function (mode) {
        switch (mode) {
            case "CW":
            case "PSK":
            case "RTTY":
                return "599";
            case "SSB":
            case "FM":
                return "59";
            default:
                return "59";
        };
    };

    var upperCaseCallsign = function (evt) {
        var callsignInput = $(this);
        callsignInput.val(callsignInput.val().toUpperCase());
    }

    initRowEvents($("#qsoEntryRow"));
    $(".qsodate").val(moment().format("DD/MM/YY"));
    $(".qsotime").val(moment().format("HHmm"));
    $(".qsocallsign").focus();
});