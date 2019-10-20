// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.GNH = (window.GNH || {});

window.GNH.hideOnPunchInSelect = (window.GNH.hideOnPunchInSelect || function() {
    document.getElementById("punch-buttons").style.display = "none";
    document.getElementById("department-select").style.display = "block";
});