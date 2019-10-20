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
    document.getElementById("punch-buttons").style.display = "none";
    document.getElementById("department-select").style.display = "none";
    document.getElementById("quantity-select").style.display = "display:flex;justify-content:center;align-items:center;";
});

window.GNH.getQuantity = (window.GNH.getQuantity || function () {
    return parseInt(document.getElementById('number').value);
});

