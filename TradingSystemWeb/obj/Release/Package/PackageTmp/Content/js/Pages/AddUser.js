﻿var AllowAlphabet = function (e) {
    isIE = document.all ? 1 : 0
    keyEntry = !isIE ? e.which : event.keyCode;
    if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || (keyEntry == '46') || (keyEntry == '32') || keyEntry == '45')
        return true;
    else {
        toastr.warning("Please Enter Only Character values.", { timeOut: 2500 });
        return false;
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57)) {
        toastr.warning("Please Enter Only Number only.");
        return false;
    }

    return true;
}
$("#FortunePartyCode").keypress(function (e) {
    //if the letter is not digit then display error and don't type anything 
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
});  
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function loaderShow() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
}

function loaderHide() {
    $('.loading-overlay-image-container').hide();
    $('.loading-overlay').hide();
}

var ChkNewUserLength = function () {
    if ($("#UNAME").val() == undefined || $("#UNAME").val() == "") {
        toastr.error("Please Enter UserName .");
        return;
    }
    else {
        var newlength = $("#UNAME").val().length;
        if (newlength < 5) {
            $("#UNAME").val("");
            toastr.error("Minmum 5 Characters Required.");
            return;
        }
    }
}

var ChkNewPassLength = function () {
    if ($("#PASS").val() == undefined || $("#PASS").val() == "") {
        toastr.error("Please Enter Password .");
        return;
    }
    else {
        var newlength = $("PASS").val().length;
        if (newlength < 6) {
            $("PASS").val("");
            toastr.error("Please Enter Minimum 6 Character PassWord.");
            return;
        }
    }
}
//================== Check Email Formate =================//
var checkemail = function (valemail) {
    var forgetfilter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (forgetfilter.test(valemail)) {
        return true;
    }
    else {
        return false;
    }
}

var validmail = function ($event) {
    var emailID = angular.element($event.target).val().trim();
    if (checkemail(emailID)) {
        $("#EMAIL").removeClass('');
        $('#EMAIL_chosen').css("border-color", "#ff0500");
    }
    else {
        toastr.error("Invalid Email Formate.");
        $("#EMAIL").addClass('');
        $('#EMAIL_chosen').css("border-color", "#ff0500");
        $("#ssEmail").val("");
        return;
    }
}

var checkemail1 = function (valemail) {
    //var forgetfilter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    var forgetfilter = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (forgetfilter.test(valemail)) {
        return true;
    }
    else {
        return false;
    }
}

var validmail1 = function ($event) {
    var emailID = angular.element($event.target).val().trim();
    if (checkemail1(emailID)) {
        $("#EMAIL1").removeClass('');
        $('#EMAIL1_chosen').css("border-color", "#ff0500");
    }
    else {
        toastr.error("Invalid Email Formate.");
        $("#EMAIL11").addClass('');
        $('#EMAIL1_chosen').css("border-color", "#ff0500");
        return;
    }
}

var ErroClearRemoveModel = function () {
    $("#ErrorModel").modal("hide");
}


var BlurClearEvent = function (Register) {
    if ($("#UNAME").val() != "") {
        $('#UNAME').removeClass("FildValid");
    }
    if ($("#PASS").val() != "") {
        $('#PASS').removeClass("FildValid");
    }
    if ($("#EmpID1").val() != "") {
        $('#EmpID1').removeClass("FildValid");
    }
    if ($("#FNAME").val() != "") {
        $('#FNAME').removeClass("FildValid");
    }
    if ($("#LNAME").val() != "") {
        $('#LNAME').removeClass("FildValid");
    }
    if ($("#COMPADD1").val() != "") {
        $('#COMPADD1').removeClass("FildValid");
    }
    if ($("#COMPCODE").val() != "") {
        $('#COMPCODE').removeClass("FildValid");
    }
    if ($("#COMPCITY").val() != "") {
        $('#COMPCITY').removeClass("FildValid");
    }
    //$scope.Customer.ssCountry 
    if ($("#COMPCOUNTRY").val() != "") {
        $('#COMPCOUNTRY_chosen').removeClass("FildValid");
    }
    if ($("#ISD_CompMobile1").val() != "") {
        $('#ISD_CompMobile1').removeClass("FildValid");
    }
    if ($("#CompMobile1").val() != "") {
        $('#CompMobile1').removeClass("FildValid");
    }
    if ($("#ISD_CompPhone1").val() != "") {
        $('#ISD_CompPhone1').removeClass("FildValid");
    }
    if ($("#CompPhone").val() != "") {
        $('#CompPhone').removeClass("FildValid");
    }
    if ($("#EMAIL").val() != "") {
        $('#EMAIL').removeClass("FildValid");
    }
    if ($("#EMAIL1").val() != "") {
        $('#EMAIL1').removeClass("FildValid");
    }
    if ($("#HKId").val() != "") {
        $('#HKId').removeClass("FildValid");
    }
    if ($("#USERTYPE").val() != "? undefined:undefined ?") {
        $('#USERTYPE_chosen').removeClass("FildValid");
    }
    if ($("#ISD_CompFax1").val() != "") {
        $('#ISD_CompFax1').removeClass("FildValid");
    }
    if ($("#CompFax").val() != "") {
        $('#CompFax').removeClass("FildValid");
    }
    
    if ($("#chkActive").is(":checked")) {
        $('.spErrorfpc').html("*");
        if ($("#FortunePartyCode").val() == "") {
            $('#FortunePartyCode').addClass("FildValid");
        }
        else {
            $('#FortunePartyCode').removeClass("FildValid");
        }
    }
    else {
        $('#FortunePartyCode').removeClass("FildValid");
        $('.spErrorfpc').html("");
    }
}

var ErrorMsg = [];
var GetError = function () {
    ErrorMsg = [];
    if ($("#UNAME").val() == "") {
        $('#UNAME').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter UserName.",
        });
    }
    else {
        var newlength = $("#UNAME").val().length;

        if (newlength < 5) {
            ErrorMsg.push({
                'Error': "Please Enter Minimum 5 Character UserName.",
            });
            $('#UNAME').addClass("FildValid");
        }
    }
    if ($("#PASS").val() == "") {
        $('#PASS').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter PassWord.",
        });
    }
    else {
        var newlength = $("#PASS").val().length;
        if (newlength < 6) {
            ErrorMsg.push({
                'Error': "Please Enter Minimum 6 Character PassWord.",
            });
            $('#PASS').addClass("FildValid");
        }
    }

    if ($("#EmpID1").val() == "") {
        $('#EmpID1').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Select Assist by 1 .",
        });
    }
    if ($("#USERTYPE").val() == "") {
        $('#USERTYPE_chosen').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Select User Type .",
        });
    }
    //if ($("#EmpID1").val() == "") {
    //    $('#EmpID1_chosen').addClass("FildValid");
    //    ErrorMsg.push({
    //        'Error': "Please Select Assist By 1 .",
    //    });
    //}
    if ($("#FNAME").val() == "") {
        $('#FNAME').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter First Name .",
        });
    }
    if ($("#LNAME").val() == "") {
        $('#LNAME').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter Last Name.",
        });
    }

    if ($("#COMPADD1").val() == "") {
        $('#COMPADD1').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter Address1 .",
        });
    }

    if ($("#COMPCITY").val() == "") {
        $('#COMPCITY').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter City.",
        });
    }
    if ($("#COMPCOUNTRY").val() == "?" || $("#COMPCOUNTRY").val() == undefined) {
        $('#COMPCOUNTRY_chosen').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter Country.",
        });
    }
    //if ($("#ISD_CompMobile1").val() == "") {
    //    $('#ISD_CompMobile1').addClass("FildValid");
    //    ErrorMsg.push({
    //        'Error': "Please Enter Mobile ISD Code .",
    //    });
    //}
    if ($("#CompMobile1").val() == "") {
        $('#CompMobile1').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter Mobile No.",
        });
    }
    //$scope.Customer.ssEmail = $("#EMAIL").val() == null ? "" : $("#EMAIL").val() == undefined ? "" : $("#EMAIL").val();
    if ($("#EMAIL").val() == "") {
        $('#EMAIL').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter  Bussiness Email Address .",
        });
    }
    else {
        if (!checkemail1($("#EMAIL").val())) {
            $('#EMAIL').addClass("FildValid");
            ErrorMsg.push({
                'Error': "Please Enter Valid  Email Address 1  Format.",
            });
        }
    }
    //$scope.Customer.ssEmailPersonal = $("#EMAIL1").val() == null ? "" : $("#EMAIL1").val() == undefined ? "" : $("#EMAIL1").val();
    if ($("#EMAIL1").val() != "") {
        if (!checkemail1($("#EMAIL1").val())) {
            ErrorMsg.push({
                'Error': "Please Enter Valid  Personal Email Format.",
            });
            $('#EMAIL1').addClass("FildValid");
        }
    }
    else {
        $('#EMAIL1').removeClass("FildValid");
    }
    if ($("#HKId").val() == "") {
        $('#HKId').addClass("FildValid");
        ErrorMsg.push({
            'Error': "Please Enter  HK Id.",
        });
    }

    if ($("#chkActive").is(":checked")) {
        if ($("#FortunePartyCode").val() == "") {
            $('#FortunePartyCode').addClass("FildValid");
            ErrorMsg.push({
                'Error': "Please Enter Fortune Party Code.",
            });
        }
        else {
            $('#FortunePartyCode').removeClass("FildValid");
        }
    }
    else {
        $('#FortunePartyCode').removeClass("FildValid");
    }
    return ErrorMsg;
}

var GetCompanyUserObject = function () {
    var CompanyUser = {};
    CompanyUser = {
        "UserName": $("#UNAME").val(),
        "Password": $("#PASS").val(),
        "FirstName": $("#FNAME").val(),
        "LastName": $("#LNAME").val(),
        "OtherName": $("#ONAME").length > 0 ? $("#ONAME").val() : '',
        "IsActive": $("#chkActive").is(":checked"),
        "Suspended": $("#hdnSuspended").val(),
        "IsCompanyUser": $("#chkCompanyUser").length > 0 ? $("#chkCompanyUser").is(":checked") : true,
        "PassportId": $("#PassPort").length > 0 ? $("#PassPort").val() : '',
        "HkId": $("#HKId").length > 0 ? $("#HKId").val() : '',
        "CompanyAddress": $("#COMPADD1").val(),
        "CompanyAddress2": $("#COMPADD2").val(),
        "CompCity": $("#COMPCITY").val(),
        "CompZipCode": $("#COMPCODE").val(),
        "CompCountry": $("#COMPCOUNTRY").val(),
        "CompEmail": $("#EMAIL").val(),
        "CompEmail2": $("#EMAIL1").val(),
        "CompRegNo": $("#RegId").val(),
        "WeChatId": $("#WeChat").val(),
        "SkypeId": $("#Skype").val(),
        "Website": $("#WebSite").val(),
        "UserType": $("#USERTYPE").length > 0 ? $("#USERTYPE").val() : 3,
        "CompCityId": parseInt($("#hdnCityCode").val()),
        "CompCountryId": parseInt($("#hdnCountryCode").val()),
        "FortunePartyCode": $("#FortunePartyCode").val(),
        "DBA": $("#DBA").val()
    };

    if ($('#CompMobile1').val() != "" && $('#CompMobile1').val() != undefined && $('#CompMobile1').val() != null)
        CompanyUser.CompMobile = $.trim($('#ISD_CompMobile1').val()) + '-' + $('#CompMobile1').val();
    else
        CompanyUser.CompMobile = '';

    if ($('#CompMobile2').val() != "" && $('#CompMobile2').val() != undefined && $('#CompMobile2').val() != null)
        CompanyUser.CompMobile2 = $.trim($('#ISD_CompMobile2').val()) + '-' + $('#CompMobile2').val();
    else
        CompanyUser.CompMobile2 = '';

    if ($('#CompPhone').val() != "" && $('#CompPhone').val() != undefined && $('#CompPhone').val() != null)
        CompanyUser.CompPhone = $.trim($('#ISD_CompPhone1').val()) + '-' + $('#CompPhone').val();
    else
        CompanyUser.CompPhone = '';

    if ($('#CompPhone1').val() != "" && $('#CompPhone1').val() != undefined && $('#CompPhone1').val() != null)
        CompanyUser.CompPhone2 = $.trim($('#ISD_CompPhone2').val()) + '-' + $('#CompPhone1').val();
    else
        CompanyUser.CompPhone2 = '';

    if ($('#CompFax').val() != "" && $('#CompFax').val() != undefined && $('#CompFax').val() != null)
        CompanyUser.CompFaxNo = $.trim($('#ISD_CompFax1').val()) + '-' + $('#CompFax').val();
    else
        CompanyUser.CompFaxNo = '';

    return CompanyUser;
}

var SaveCompanyUser = function () {
    ErrorMsg = GetError();

    if (ErrorMsg.length > 0) {
        $("#divError").empty();
        ErrorMsg.forEach(function (item) {
            $("#divError").append('<li>' + item.Error + '</li>');
        });
        $("#ErrorModel").modal("show");
    }
    else {
        var CompanyUser = GetCompanyUserObject();
        loaderShow();
        $.ajax({
            url: '/User/SaveUserData',
            type: "POST",
            data: { user: CompanyUser },
            success: function (data) {
                loaderHide();
                if (data.Status == "1") {
                    window.location = "/User/Manage";
                    toastr.success(data.Message);
                    Clear();
                }
                else {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                }
            }
        });
    }
}

var UpdateRegisteredUser = function () {
    ErrorMsg = GetError();

    if (ErrorMsg.length > 0) {
        $("#divError").empty();
        ErrorMsg.forEach(function (item) {
            $("#divError").append('<li>' + item.Error + '</li>');
        });
        $("#ErrorModel").modal("show");
    }
    else {
        var CompanyUser = GetCompanyUserObject();
        CompanyUser.UserID = $("#UserID").val();
        CompanyUser.PrevIsActive = $("#hdnPrevIsActive").val();
        if ($('#CompMobile2').val() != "" && $('#CompMobile2').val() != undefined && $('#CompMobile2').val() != null)
            CompanyUser.CompMobile2 = $.trim($('#ISD_CompMobile2').val()) + '-' + $('#CompMobile2').val();
        else
            CompanyUser.CompMobile2 = '';
            //CompanyUser.CompMobile2 = $('#ISD_CompMobile2').val() + '-';
        if ($('#CompPhone1').val() != "" && $('#CompPhone1').val() != undefined && $('#CompPhone1').val() != null)
            CompanyUser.CompPhone2 = $.trim($('#ISD_CompPhone2').val()) + '-' + $('#CompPhone1').val();
        else
            CompanyUser.CompPhone2 = '';

        CompanyUser.EmpID1 = $("#EmpID1").val();    
        CompanyUser.EmpID2 = $("#EmpID2").val();
        CompanyUser.CompanyName = $("#CompName").val();
        CompanyUser.IsCompanyUser = $("#IsCompanyUser").val();
        CompanyUser.RapnetID = $("#RapId").val();
        CompanyUser.StockType = $("#StockType").val();

        loaderShow();
        $.ajax({
            url: '/User/UpdateUserData',
            type: "POST",
            data: { user: CompanyUser },
            success: function (data) {
                loaderHide();
                if (data.Status == "1") {
                    window.location = "/User/Manage";
                    toastr.success(data.Message);
                    $("#hdnPrevIsActive").val($("#chkActive").is(":checked"));
                }
                else {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                }
            }
        });
    }
}

var UpdateCompanyUser = function () {
    ErrorMsg = GetError();
    if (ErrorMsg.length > 0) {
        $("#divError").empty();
        ErrorMsg.forEach(function (item) {
            $("#divError").append('<li>' + item.Error + '</li>');
        });
        $("#ErrorModel").modal("show");
    }
    else {
        var CompanyUser = GetCompanyUserObject();
        CompanyUser.UserID = $("#UserID").val();
        CompanyUser.PrevIsActive = $("#hdnPrevIsActive").val();

        loaderShow();
        $.ajax({
            url: '/User/UpdateUserData',
            type: "POST",
            data: { user: CompanyUser },
            success: function (data) {
                loaderHide();
                if (data.Status == "1") {
                    window.location = "/User/Manage";
                    toastr.success(data.Message);
                    $("#hdnPrevIsActive").val($("#chkActive").is(":checked"));
                }
                else {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                }
            }
        });
    }
}

var ClearObject = function () {
    $("#PASS").val('');
    $("#FNAME").val('');
    $("#LNAME").val('');
    $("#ONAME").length > 0 ? $("#ONAME").val('') : '';
    $("#chkActive").prop("checked", false);
    $("#hdnSuspended").val(),
        $("#chkCompanyUser").length > 0 ? $("#chkCompanyUser").prop("checked", false) : '';
    $("#PassPort").length > 0 ? $("#PassPort").val('') : '',
        $("#HKId").length > 0 ? $("#HKId").val('') : '',
        $("#COMPADD1").val(''),
        $("#COMPADD2").val(''),
        $("#COMPCITY").val(''),
        $("#COMPCODE").val(''),
        $("#COMPCOUNTRY").val(''),
        $('#ISD_CompMobile1').val('');
    $('#CompMobile1').val('');
    $('#ISD_CompMobile2').val('');
    $('#CompMobile2').val('');
    $('#ISD_CompPhone1').val('');
    $('#CompPhone').val('');
    $('#ISD_CompPhone2').val('');
    $('#CompPhone1').val('');
    $('#ISD_CompFax1').val('');
    $('#CompFax').val('');
    $("#EMAIL").val(''),
        $("#EMAIL1").val(''),
        $("#RegId").val(''),
        $("#WeChat").val(''),
        $("#Skype").val(''),
        $("#WebSite").val(''),
        $("#USERTYPE").length > 0 ? $("#USERTYPE").val('') : '';

    $('#UNAME').removeClass("FildValid");
    $('#PASS').removeClass("FildValid");
    $('#USERTYPE_chosen').removeClass("FildValid");
    $('#FNAME').removeClass("FildValid");
    $('#LNAME').removeClass("FildValid");
    $('#COMPADD1').removeClass("FildValid");
    $('#COMPCODE').removeClass("FildValid");
    $('#COMPCITY').removeClass("FildValid");
    $('#COMPCOUNTRY_chosen').removeClass("FildValid");
    $('#ISD_CompMobile1').removeClass("FildValid");
    $('#CompMobile1').removeClass("FildValid");
    $('#ISD_CompPhone1').removeClass("FildValid");
    $('#CompPhone').removeClass("FildValid");
    $('#EMAIL').removeClass("FildValid");
    $('#HKId').removeClass("FildValid");
}
//================== Clear Function =================//
var Clear = function () {
    $("#UNAME").val('');
    ClearObject();
}

var ClearCompanyUser = function () {
    ClearObject();
}

var ClearRegistedUser = function () {
    ClearObject();
    $('#ISD_CompMobile2').val('');
    $('#CompMobile2').val('');
    $('#ISD_CompPhone2').val('');
    $('#CompPhone2').val('');
    $("#EmpID1").val('');
    $("#EmpID2").val('');
    $("#IsCompanyUser").val('');
    $("#RapId").val('');
    $("#StockType").val('');
    $("#CompName").val('');
}

$(function () {
    $("#COMPCITY").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Common/GetCitystateCountryAutoComplete",
                data: "{ 'sSearch': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data.Data, function (item) {
                        return {
                            label: item.sCityName,
                            val: item.sCityName,
                            CityCode: item.iCityId,
                            CountryCode: item.iCountryId,
                            CountryName: item.sCountryName,
                            STDCode: item.iSTDCode,
                            ISDCode: item.sISDCode
                        }
                    }))
                },
                error: function (response) {
                    toastr.error(response.responseText);
                },
                failure: function (response) {
                    toastr.error(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#hdnCityCode").val(i.item.CityCode);
            $("#hdnCountryCode").val(i.item.CountryCode);
            $("#COMPCITY").val(i.item.val);
            $("#COMPCOUNTRY").val(i.item.CountryName);
            $('.isdcode').val('+' + i.item.ISDCode);
            
            if (i.item.STDCode == "0") {
                $('.stdisdcode').val('+' + i.item.ISDCode);
                $('.stdcode').val('');
            } else {
                $('.stdisdcode').val('+' + i.item.ISDCode + ' ' + i.item.STDCode);
                $('.stdcode').val('+' + i.item.STDCode);
            }
        },
        change: function (event, ui) {
            if (!ui.item) {
                $("#hdnCityCode").val("0");
                $("#hdnCountryCode").val("0");
                $("#COMPCITY").val("");
                $("#COMPCOUNTRY").val("");
                $('.isdcode').val("");
                $('.stdcode').val("");
                $('.stdisdcode').val("");
                toastr.warning('select city value from dropdown');
            }
        },
        minLength: 1
    });
    $('input[type="text"]').attr('autocomplete', 'nope');
});