// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var GoodNeighborHouseApi = {
    CreatePunch: function(volunteerId, isClockIn, forTimeInMilliseconds, callback) {
        $.ajax({
            type: "POST",
            url: "/api/punch/create/" + volunteerId + "/" + isClockIn + "/" + forTime,
            success: callback
        });
    };
    
    DeletePunch: function(id, callback) {
        $.ajax({
            type: "DELETE",
            url: "/api/punch/" + id,
            success: callback
        });
    };
};