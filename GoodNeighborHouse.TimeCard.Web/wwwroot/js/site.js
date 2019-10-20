// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.GNH = (window.GNH || {});

window.GNH.hideOnPunchInSelect = (window.GNH.hideOnPunchInSelect || function() {
    document.getElementById("punch-buttons").style.display = "none";
    document.getElementById("department-select").style.display = "block";
});

window.GNH.showAddPunchModal = (window.GNH.showAddPunchModal || function() {
    document.getElementById("")
});

window.GNH.createPunch = (window.GNH.createPunch || function(volunteerId, isClockIn, forTimeInMilliseconds, callback) {
        $.ajax({
            type: "POST",
            url: "/api/punch/create/" + volunteerId + "/" + isClockIn + "/" + forTime,
            success: callback
        });
});

window.GNH.deletePunch = (window.GNH.createPunch || function(id, callback) {
        $.ajax({
            type: "DELETE",
            url: "/api/punch/" + id,
            success: callback
        });
});