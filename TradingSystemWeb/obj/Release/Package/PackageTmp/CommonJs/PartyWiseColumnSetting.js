

$(document).ready(function () {
    $("#LoadApi").click(function () {

        $.ajax({
            
            url: "/Home/GetJsonApi",
            dataType: "json",
            type: "Get",
            async: false,
            cache: false,
            success: function (data) {
                alert(data);
            }
        });


    });
    


});