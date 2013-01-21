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
        newcallsignbox.on("keypress.makenewrow", addQsoEditRow);

    };
    $(".qsocallsign").on("keypress.makenewrow", addQsoEditRow);

});