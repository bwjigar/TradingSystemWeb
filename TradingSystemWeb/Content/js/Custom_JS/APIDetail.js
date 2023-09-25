function Clear() {
    window.location = '/Customer/APIDetail';
}
var API_List_View = function () {
    window.location.href = '/Customer/APIList';
}
function Get() {
    if ($("#txtAPI").val() == "") {
        toastr.warning("Please Enter API URL !", { timeOut: 2500 });
        $("#txtAPI").focus();
        return;
    }

    if ($("#txtUserName").val() == "") {
        toastr.warning("Please Enter User Name !", { timeOut: 2500 });
        $("#txtUserName").focus();
        return;
    }

    if ($("#txtPassword").val() == "") {
        toastr.warning("Please Enter Password !", { timeOut: 2500 });
        $("#txtPassword").focus();
        return;
    }

    $("#loading").css("display", "block");
    setTimeout(function () {
        var formData1 = new FormData();
        formData1.append('API', $("#txtAPI").val());
        formData1.append('UserName', $("#txtUserName").val());
        formData1.append('Password', $("#txtPassword").val());

        $.ajax({
            url: "/Customer/CallAPI",
            async: false,
            type: "POST",
            data: formData1,
            processData: false,
            contentType: false,
            success: function (data, textStatus, jqXHR) {
                if (data.search('.csv') == -1) {
                    toastr.error(data);
                } else {
                    location.href = data;
                    Clear();
                }
                $("#loading").css("display", "none");
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("#loading").css("display", "none");
            }
        });
    }, 10);
}
function Repeatevery() {
    if ($("#DdlRepeatevery").val() == "Minute") {
        $("#txtMinute").val("");
        $("#txtMinute").show();
        $("#txtHour").hide();
    }
    else if ($("#DdlRepeatevery").val() == "Hour") {
        $("#txtHour").val("");
        $("#txtMinute").hide();
        $("#txtHour").show();
    }
}
function SaveApiData() {
    if ($("#txtAPI").val() == "") {
        toastr.warning("Please Enter API URL !", { timeOut: 2500 });
        $("#txtAPI").focus();
        return;
    }

    if ($("#ddlAPIResponse").val() == "") {
        toastr.warning("Please Select API Response !", { timeOut: 2500 });
        $("#ddlAPIResponse").focus();
        return;
    }

    if ($("#txtFileLocation").val() == "") {
        toastr.warning("Please Enter File Location !", { timeOut: 2500 });
        $("#txtFileLocation").focus();
        return;
    }

    if ($("#LocationExportType").val() == "") {
        toastr.warning("Please Select Export Type !", { timeOut: 2500 });
        $("#LocationExportType").focus();
        return;
    }

    if ($("#DdlRepeatevery").val() == "Minute") {
        if ($("#txtMinute").val() == "") {
            toastr.warning("Please Enter Minute !", { timeOut: 2500 });
            $("#txtMinute").focus();
            return;
        }
    }
    else if ($("#DdlRepeatevery").val() == "Hour") {
        if ($("#txtHour").val() == "") {
            toastr.warning("Please Select Hour !", { timeOut: 2500 });
            $("#txtHour").focus();
            return;
        }
    }

    if ($("#APIName").val() == "") {
        toastr.warning("Please Enter API Name !", { timeOut: 2500 });
        $("#APIName").focus();
        return;
    }
    
    var obj = {};
    obj.Id = $("#hdnId").val();
    obj.APIURL = $("#txtAPI").val();
    obj.APIName = $("#APIName").val();
    obj.APIResponseFormat = $("#ddlAPIResponse").val();
    obj.APIMethod = $("#ddlAPIMethod").val();
    obj.FileLocation = $("#txtFileLocation").val();
    obj.LocationExportType = $("#LocationExportType").val();
    obj.RepeateveryType = $("#DdlRepeatevery").val();
    obj.Repeatevery = $('#DdlRepeatevery').val() == "Minute" ? $("#txtMinute").val() : $("#txtHour").val();
    obj.Active = document.getElementById("APIStatus").checked;
    obj.UserName = $("#txtUserName").val();
    obj.Password = $("#txtPassword").val();
    
    $.ajax({
        url: "/Customer/SaveAPIMst",
        async: false,
        type: "POST",
        dataType: "json",
        data: JSON.stringify({ saveapimst: obj }),
        contentType: "application/json; charset=utf-8",
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "0") {
                toastr.error(data.Message, { timeOut: 2500 });
            }
            else if (data.Status == "1") {
                if ($("#hdnId").val() == "0") {
                    toastr.success("API Save Successfully !!", { timeOut: 2500 });
                    setTimeout(function () {
                        location.href = window.location.href + "?Id=" + data.Message;
                    }, 1100);
                }
                else {
                    toastr.success("API Update Successfully !!", { timeOut: 2500 });
                    setTimeout(function () {
                        location.reload(true);
                    }, 1100);
                }
            }
            $("#loading").css("display", "none");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#loading").css("display", "none");
            toastr.error(textStatus);
        }
    });
}
function Get_APIMst(Id) {
    $("#loading").css("display", "block");
    $.ajax({
        url: "/Customer/Get_APIMst",
        async: false,
        type: "POST",
        data: { Id: Id },
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Message == "SUCCESS") {
                $("#txtAPI").val(data.Data[0].APIURL);
                $("#APIName").val(data.Data[0].APIName);
                $("#ddlAPIResponse").val(data.Data[0].APIResponseFormat);
                $("#txtFileLocation").val(data.Data[0].FileLocation);
                $("#LocationExportType").val(data.Data[0].LocationExportType);
                $("#DdlRepeatevery").val(data.Data[0].RepeateveryType);
                Repeatevery();
                if ($("#DdlRepeatevery").val() == "Minute") {
                    $("#txtMinute").val(data.Data[0].Repeatevery);
                }
                else if ($("#DdlRepeatevery").val() == "Hour") {
                    $("#txtHour").val(data.Data[0].Repeatevery);
                }
                $("#ddlAPIMethod").val(data.Data[0].APIMethod);
                $("#APIStatus").attr("checked", data.Data[0].Active);
                $("#txtUserName").val(data.Data[0].UserName);
                $("#txtPassword").val(data.Data[0].Password);
            }
            $("#loading").css("display", "none");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#loading").css("display", "none");
        }
    });
}
$(document).ready(function () {
    if ($("#hdnId").val() != "0") {
        Get_APIMst($("#hdnId").val());
    }
});