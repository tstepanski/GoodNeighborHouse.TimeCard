// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.GNH = (window.GNH || {});

window.GNH.increaseValue = (window.GNH.increaseValue || function () {
    var value = parseInt(document.getElementById('number').value);
    value++;
    document.getElementById('number').value = value;
});

window.GNH.decreaseValue = (window.GNH.decreaseValue || function () {
    var value = parseInt(document.getElementById('number').value);
    value < 2 ? value = 2 : '';
    value--;
    document.getElementById('number').value = value;
});

window.GNH.hideOnPunchInSelect = (window.GNH.hideOnPunchInSelect || function() {
    document.getElementById("punch-buttons").style.display = "none";
    document.getElementById("department-select").style.display = "block";
});

window.GNH.showQuantitySelect = (window.GNH.showQuantitySelect || function () {
    document.getElementById("quantity-select").style.display = "none";
    document.getElementById("department-select").style.display = "none";
    document.getElementById("quantity-select").style.display = "block";
});

window.GNH.getQuantity = (window.GNH.getQuantity || function () {
    return parseInt(document.getElementById('number').value);
});

