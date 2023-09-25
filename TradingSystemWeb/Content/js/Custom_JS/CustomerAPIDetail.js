var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var SupplierList = [];
var LocationList = [];
var ShapeList = [];
var CaratList = [];
var ColorList = [];
var ClarityList = [];
var CutList = [];
var PolishList = [];
var SymList = [];
var FlsList = [];
var LabList = [];
var KTSList = [];
var BGMList = [];
var CrownBlackList = [];
var TableBlackList = [];
var CrownWhiteList = [];
var TableWhiteList = [];
var CheckedSupplierValue = "";
var CheckedLocationValue = "";
var CheckedShapeValue = "";
var CheckedColorValue = "";
var CheckedClarityValue = "";
var CheckedCutValue = "";
var CheckedPolValue = "";
var CheckedSymValue = "";
var CheckedLabValue = "";
var CheckedCaratValue = "";
var CheckedFLsValue = "";
var CheckedBgmValue = "";
var CheckedCrnBlackValue = "";
var CheckedTblBlackValue = "";
var CheckedCrnWhiteValue = "";
var CheckedTblWhiteValue = "";
var ColumnList = [];

var SSN_CARAT = [];
var CheckedCaratValue = '';
var CaratFrom = '';
var CaratTo = '';
var Color = '';
var IsCaratFT = true
var CARAT = false;
var FromSize1 = "";
var ToSize1 = "";
var FromSize2 = "";
var ToSize2 = "";
var FromSize3 = "";
var ToSize3 = "";
var FromSize4 = "";
var ToSize4 = "";
var FromSize5 = "";
var ToSize5 = "";
var FromSize6 = "";
var ToSize6 = "";
var FromSize7 = "";
var ToSize7 = "";
var FromSize8 = "";
var ToSize8 = "";
var FromSize11 = "";
var ToSize11 = "";
var FromSize10 = "";
var ToSize10 = "";
var FromSize11 = "";
var ToSize11 = "";
var FromSize12 = "";
var ToSize12 = "";
var FromSize13 = "";
var ToSize13 = "";
var FromSize14 = "";
var ToSize14 = "";
var FromSize15 = "";
var ToSize15 = "";
var FromSize16 = "";
var ToSize16 = "";
var FromSize17 = "";
var ToSize17 = "";
var FromSize18 = "";
var ToSize18 = "";

var CARAT_Size2 = false;
var CARAT_Size3 = false;
var CARAT_Size4 = false;
var CARAT_Size5 = false;
var CARAT_Size6 = false;
var CARAT_Size7 = false;
var CARAT_Size8 = false;
var CARAT_Size9 = false;
var CARAT_Size10 = false;
var CARAT_Size11 = false;
var CARAT_Size12 = false;
var CARAT_Size13 = false;
var CARAT_Size14 = false;
var CARAT_Size15 = false;
var CARAT_Size16 = false;
var CARAT_Size17 = false;
var CARAT_Size18 = false;
var Carat = "";
var KeyToSymbolList = [];
var CheckKeyToSymbolList = [];
var UnCheckKeyToSymbolList = [];
var KTS = 0;

$(document).ready(function () {
    $("input[name$='ApiMethod']").click(function () {
        var Name = $(this).val();
        $(".Div-Method").hide();
        $("#APIMethod" + Name).show();
    });
    OnetimeDate("I", "");
    Get_API_StockFilter();
    BindKeyToSymbolList();
    ColumnsettingList($("#TransId").val());

    if ($("#TransId").val() != "0") {
        Get_ApiUploadMst($("#TransId").val());
        Get_ApiFilter($("#TransId").val());
    }

    document.getElementById("txtRepeatevery").disabled = true;
    document.getElementById("DdlRepeatevery").disabled = true;
    $("input[name$='Location']").click(function () {
        var Name = $(this).val();
        if (Name == "Onetime") {
            Onetime_Click();
        }
        else if (Name == "Repeatevery") {
            Repeatevery_Click();
        }
    });
    $("#btnAddNewRow").click(function () {
        AddNewRow();
    });
    $("#mytable1").on('click', '.RemoveCriteria', function () {
        $(this).closest('tr').remove();
        if (parseInt($("#mytable1 #myTableBody1").find('tr').length) == 0) {
            $("#mytable1").hide();
        }
        var idd = 1;
        $("#mytable1 #myTableBody1 tr").each(function () {
            $(this).find("th:eq(0)").html(idd);
            idd += 1;
        });
    });
});
function Onetime_Click() {
    document.getElementById("txtOnetimeDate").disabled = false;
    document.getElementById("txtOnetime").disabled = false;
    OnetimeDate("I", "");
    $("#txtOnetime").val("");
    $("#txtRepeatevery").val("");
    $("#DdlRepeatevery").val("Minute");
    document.getElementById("txtRepeatevery").disabled = true;
    document.getElementById("DdlRepeatevery").disabled = true;
}
function Repeatevery_Click() {
    $("#txtRepeatevery").val("");
    $("#DdlRepeatevery").val("Minute");
    document.getElementById("txtRepeatevery").disabled = false;
    document.getElementById("DdlRepeatevery").disabled = false;
    OnetimeDate("I", "");
    $("#txtOnetime").val("");
    document.getElementById("txtOnetimeDate").disabled = true;
    document.getElementById("txtOnetime").disabled = true;
}
function OnetimeDate(Type, Date) {
    var _Date = "";
    if (Type == "U") {
        _Date = Date;
    }
    else {
        _Date = SetCurrentDate();
    }
    $('#txtOnetimeDate').val(_Date);
    $('#txtOnetimeDate').daterangepicker({
        singleDatePicker: true,
        startDate: _Date,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: parseInt(moment().format('YYYY'), 10),
        maxYear: parseInt(moment().format('YYYY'), 10) + 10
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    });
}
function SetCurrentDate() {
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}
function Get_API_StockFilter() {
    $("#loading").css("display", "block");
    $.ajax({
        url: "/Customer/Get_API_StockFilter",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Message == "SUCCESS") {
                $.each(data.Data, function (i, item) {
                    if (item.Type == "Location") { LocationList.push(item); }
                    if (item.Type == "Supplier") { SupplierList.push(item); }
                    if (item.Type == "Shape") { ShapeList.push(item); }
                    if (item.Type == "Color") { ColorList.push(item); }
                    if (item.Type == "Clarity") { ClarityList.push(item); }
                    if (item.Type == "Cut") { CutList.push(item); }
                    if (item.Type == "Polish") { PolishList.push(item); }
                    if (item.Type == "Symm") { SymList.push(item); }
                    if (item.Type == "Fls") { FlsList.push(item); }
                    if (item.Type == "Lab") { LabList.push(item); }
                    if (item.Type == "BGM") { BGMList.push(item); }
                    if (item.Type == "CROWN BLACK") { CrownBlackList.push(item); }
                    if (item.Type == "CROWN WHITE") { CrownWhiteList.push(item); }
                    if (item.Type == "TABLE BLACK") { TableBlackList.push(item); }
                    if (item.Type == "TABLE WHITE") { TableWhiteList.push(item); }
                });
            }
            $("#loading").css("display", "none");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#loading").css("display", "none");
        }
    });
}
var ModalShow = function (ParameterLabel, ObjLst) {
    $('#exampleModalLabel').text(ParameterLabel);
    $('#divModal').removeClass("ng-hide").addClass("ng-show");

    var content = '<ul id="popupul" class="color-whit-box">';
    var list = [];
    list = ObjLst;
    list.forEach(function (item) {
        content += '<li onclick="ItemClicked(\'' + ParameterLabel + '\',\'' + item.Value + '\', this);" class="';
        if (item.isActive) {
            content += 'active';
        }
        content += '">' + item.Value + '</li>';
    });
    content += '</ul>';
    $('#divModal').empty();
    $('#divModal').append(content);

    $("#mpdal-footer").append('<button type="button" class="btn btn-primary" ng-click="ResetSelectedAttr(' + ParameterLabel + ');">Reset</button><button type="button" class="btn btn-secondary" data-dismiss="modal">Done</button>');

    $('#myModal').modal('toggle');
}
var ResetSelectedAttr = function (attr, obj) {
    _.each(obj, function (itm) {
        itm.isActive = false;
    });
    $(attr).empty();
}
var ResetCheckCarat = function () {
    SSN_CARAT = [];
    CheckedCaratValue = '';
    $('#CaratModal input:checkbox').prop('checked', false);
    $(".divCheckedCaratValue").empty();
}
function PricingMethodDDL() {
    if ($("#PricingMethod").val() != "") {
        document.getElementById("Percentage").disabled = false;
    }
    else {
        $("#Percentage").val("0");
        document.getElementById("Percentage").disabled = true;
    }
}
var ItemClicked = function (ParameterLabel, item, curritem) {
    var list = [];
    if (ParameterLabel == 'Supplier') {
        list = SupplierList;
    }
    if (ParameterLabel == 'Location') {
        list = LocationList;
    }
    if (ParameterLabel == 'Shape') {
        list = ShapeList;
    }
    if (ParameterLabel == 'Color') {
        list = ColorList;
    }
    if (ParameterLabel == 'Clarity') {
        list = ClarityList;
    }
    if (ParameterLabel == 'Cut') {
        list = CutList;
    }
    if (ParameterLabel == 'Polish') {
        list = PolishList;
    }
    if (ParameterLabel == 'Sym') {
        list = SymList;
    }
    if (ParameterLabel == 'Lab') {
        list = LabList;
    }
    if (ParameterLabel == 'Fls') {
        list = FlsList;
    }
    if (ParameterLabel == 'BGM') {
        list = BGMList;
    }
    if (ParameterLabel == 'CrownBlack') {
        list = CrownBlackList;
    }
    if (ParameterLabel == 'TableBlack') {
        list = TableBlackList;
    }
    if (ParameterLabel == 'CrownWhite') {
        list = CrownWhiteList;
    }
    if (ParameterLabel == 'TableWhite') {
        list = TableWhiteList;
    }
    var itm = _.find(list, function (i) { return i.Value == item });
    itm.isActive = !itm.isActive;
    $(curritem).toggleClass('active');
    SetSearchParameter();
}
var SetSearchParameter = function () {
    var supplierLst = _.pluck(_.filter(SupplierList, function (e) { return e.isActive == true }), 'Value').join(",");
    var locationLst = _.pluck(_.filter(LocationList, function (e) { return e.isActive == true }), 'Value').join(",");
    var shapeLst = _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'Value').join(",");
    var colorLst = _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'Value').join(",");
    var clarityLst = _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'Value').join(",");
    var cutlst = _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Pollst = _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Symlst = _.pluck(_.filter(SymList, function (e) { return e.isActive == true }), 'Value').join(",");
    var labLst = _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'Value').join(",");
    var flslst = _.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'Value').join(",");
    var bgmlst = _.pluck(_.filter(BGMList, function (e) { return e.isActive == true }), 'Value').join(",");
    var crnblacklst = _.pluck(_.filter(CrownBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
    var tblblacklst = _.pluck(_.filter(TableBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
    var crnwhitelst = _.pluck(_.filter(CrownWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");
    var tblwhitelst = _.pluck(_.filter(TableWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");

    CheckedSupplierValue = supplierLst;
    CheckedLocationValue = locationLst;
    CheckedShapeValue = shapeLst;
    CheckedColorValue = colorLst;
    CheckedClarityValue = clarityLst;
    CheckedCutValue = cutlst;
    CheckedPolValue = Pollst;
    CheckedSymValue = Symlst;
    CheckedLabValue = labLst;
    CheckedFLsValue = flslst;
    CheckedBgmValue = bgmlst;
    CheckedCrnBlackValue = crnblacklst;
    CheckedTblBlackValue = tblblacklst;
    CheckedCrnWhiteValue = crnwhitelst;
    CheckedTblWhiteValue = tblwhitelst;

    if (CheckedSupplierValue.split(",").length >= 1) {
        $(".divCheckedSupplierValue").empty();
        $(".divCheckedSupplierValue").append(CheckedSupplierValue);
        $(".divCheckedSupplierValue").attr({
            "title": CheckedSupplierValue
        });
    }
    if (CheckedLocationValue.split(",").length >= 1) {
        $(".divCheckedLocationValue").empty();
        $(".divCheckedLocationValue").append(CheckedLocationValue);
        $(".divCheckedLocationValue").attr({
            "title": CheckedLocationValue
        });
    }
    if (CheckedShapeValue.split(",").length >= 1) {
        $(".divCheckedShapeValue").empty();
        $(".divCheckedShapeValue").append(CheckedShapeValue);
        $(".divCheckedShapeValue").attr({
            "title": CheckedShapeValue
        });
    }
    if (CheckedColorValue.split(",").length >= 1) {
        $(".divCheckedColorValue").empty();
        $(".divCheckedColorValue").append(CheckedColorValue);
        $(".divCheckedColorValue").attr({
            "title": CheckedColorValue
        });
    }
    if (CheckedClarityValue.split(",").length >= 1) {
        $(".divCheckedClarityValue").empty();
        $(".divCheckedClarityValue").append(CheckedClarityValue);
        $(".divCheckedClarityValue").attr({
            "title": CheckedClarityValue
        });
    }
    if (CheckedCutValue.split(",").length >= 1) {
        $(".divCheckedCutValue").empty();
        $(".divCheckedCutValue").append(CheckedCutValue);
        $(".divCheckedCutValue").attr({
            "title": CheckedCutValue
        });
    }
    if (CheckedPolValue.split(",").length >= 1) {
        $(".divCheckedPolValue").empty();
        $(".divCheckedPolValue").append(CheckedPolValue);
        $(".divCheckedPolValue").attr({
            "title": CheckedPolValue
        });
    }
    if (CheckedSymValue.split(",").length >= 1) {
        $(".divCheckedSymValue").empty();
        $(".divCheckedSymValue").append(CheckedSymValue);
        $(".divCheckedSymValue").attr({
            "title": CheckedSymValue
        });
    }
    if (CheckedLabValue.split(",").length >= 1) {
        $(".divCheckedLabValue").empty();
        $(".divCheckedLabValue").append(CheckedLabValue);
        $(".divCheckedLabValue").attr({
            "title": CheckedLabValue
        });
    }
    if (CheckedFLsValue.split(",").length >= 1) {
        $(".divCheckedFLsValue").empty();
        $(".divCheckedFLsValue").append(CheckedFLsValue);
        $(".divCheckedFLsValue").attr({
            "title": CheckedFLsValue
        });
    }
    if (CheckedBgmValue.split(",").length >= 1) {
        $(".divCheckedBGMValue").empty();
        $(".divCheckedBGMValue").append(CheckedBgmValue);
        $(".divCheckedBGMValue").attr({
            "title": CheckedFLsValue
        });
    }
    if (CheckedCrnBlackValue.split(",").length >= 1) {
        $(".divCheckedCrnBlackValue").empty();
        $(".divCheckedCrnBlackValue").append(CheckedCrnBlackValue);
        $(".divCheckedCrnBlackValue").attr({
            "title": CheckedCrnBlackValue
        });
    }
    if (CheckedCrnWhiteValue.split(",").length >= 1) {
        $(".divCheckedCrnWhiteValue").empty();
        $(".divCheckedCrnWhiteValue").append(CheckedCrnWhiteValue);
        $(".divCheckedCrnWhiteValue").attr({
            "title": CheckedCrnWhiteValue
        });
    }
    if (CheckedTblBlackValue.split(",").length >= 1) {
        $(".divCheckedTblBlackValue").empty();
        $(".divCheckedTblBlackValue").append(CheckedTblBlackValue);
        $(".divCheckedTblBlackValue").attr({
            "title": CheckedTblBlackValue
        });
    }
    if (CheckedTblWhiteValue.split(",").length >= 1) {
        $(".divCheckedTblWhiteValue").empty();
        $(".divCheckedTblWhiteValue").append(CheckedTblWhiteValue);
        $(".divCheckedTblWhiteValue").attr({
            "title": CheckedTblWhiteValue
        });
    }
}
var GetSelectedCarat = function () {
    CheckedCaratValue = "";
    IsCaratFT = false;
    var FromCarat = "";
    var ToCarat = "";
    var Carat = null;

    if ($("#CARAT_Size2").is(":checked") == true) {
        var FROM_VAL = $("#FromSize2").val() == undefined ? "0.18" : $("#FromSize2").val() == "" ? "0.18" : $("#FromSize2").val();
        var TO_VAL = $("#ToSize2").val() == undefined ? "0.22" : $("#ToSize2").val() == "" ? "0.22" : $("#ToSize2").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 1, Carat: str, };
        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 1) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 1) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }

    if ($("#CARAT_Size3").is(":checked") == true) {
        var FROM_VAL = $("#FromSize3").val() == undefined ? "0.23" : $("#FromSize3").val() == "" ? "0.23" : $("#FromSize3").val();
        var TO_VAL = $("#ToSize3").val() == undefined ? "0.29" : $("#ToSize3").val() == "" ? "0.29" : $("#ToSize3").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 2, Carat: str, };
        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 2) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 2) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size4").is(":checked") == true) {
        var FROM_VAL = $("#FromSize4").val() == undefined ? "0.30" : $("#FromSize4").val() == "" ? "0.30" : $("#FromSize4").val();
        var TO_VAL = $("#ToSize4").val() == undefined ? "0.39" : $("#ToSize4").val() == "" ? "0.39" : $("#ToSize4").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 3, Carat: str, };
        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 3) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 3) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size5").is(":checked") == true) {
        var FROM_VAL = $("#FromSize5").val() == undefined ? "0.40" : $("#FromSize5").val() == "" ? "0.40" : $("#FromSize5").val();
        var TO_VAL = $("#ToSize5").val() == undefined ? "0.49" : $("#ToSize5").val() == "" ? "0.49" : $("#ToSize5").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 4, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {

                if (item.id == 4) {

                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 4) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size6").is(":checked") == true) {
        var FROM_VAL = $("#FromSize6").val() == undefined ? "0.50" : $("#FromSize6").val() == "" ? "0.50" : $("#FromSize6").val();
        var TO_VAL = $("#ToSize6").val() == undefined ? "0.59" : $("#ToSize6").val() == "" ? "0.59" : $("#ToSize6").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 5, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {

                if (item.id == 5) {

                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 5) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size7").is(":checked") == true) {
        var FROM_VAL = $("#FromSize7").val() == undefined ? "0.60" : $("#FromSize7").val() == "" ? "0.60" : $("#FromSize7").val();
        var TO_VAL = $("#ToSize7").val() == undefined ? "0.69" : $("#ToSize7").val() == "" ? "0.69" : $("#ToSize7").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 6, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 6) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 6) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size8").is(":checked") == true) {
        var FROM_VAL = $("#FromSize8").val() == undefined ? "0.70" : $("#FromSize8").val() == "" ? "0.70" : $("#FromSize8").val();
        var TO_VAL = $("#ToSize8").val() == undefined ? "0.79" : $("#ToSize8").val() == "" ? "0.79" : $("#ToSize8").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 7, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {

                if (item.id == 7) {

                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 7) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size9").is(":checked") == true) {
        var FROM_VAL = $("#FromSize9").val() == undefined ? "0.80" : $("#FromSize9").val() == "" ? "0.80" : $("#FromSize9").val();
        var TO_VAL = $("#ToSize9").val() == undefined ? "0.89" : $("#ToSize9").val() == "" ? "0.89" : $("#ToSize9").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 8, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 8) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 8) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size10").is(":checked") == true) {
        var FROM_VAL = $("#FromSize10").val() == undefined ? "0.90" : $("#FromSize10").val() == "" ? "0.90" : $("#FromSize10").val();
        var TO_VAL = $("#ToSize10").val() == undefined ? "0.99" : $("#ToSize10").val() == "" ? "0.99" : $("#ToSize10").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 9, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 9) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 9) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size11").is(":checked") == true) {
        var FROM_VAL = $("#FromSize11").val() == undefined ? "1.00" : $("#FromSize11").val() == "" ? "1.00" : $("#FromSize11").val();
        var TO_VAL = $("#ToSize11").val() == undefined ? "1.19" : $("#ToSize11").val() == "" ? "1.19" : $("#ToSize11").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 10, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 10) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 10) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size12").is(":checked") == true) {
        var FROM_VAL = $("#FromSize12").val() == undefined ? "1.20" : $("#FromSize12").val() == "" ? "1.20" : $("#FromSize12").val();
        var TO_VAL = $("#ToSize12").val() == undefined ? "1.49" : $("#ToSize12").val() == "" ? "1.49" : $("#ToSize12").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 11, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {

                if (item.id == 11) {

                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 11) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }
    if ($("#CARAT_Size13").is(":checked") == true) {
        var FROM_VAL = $("#FromSize13").val() == undefined ? "1.50" : $("#FromSize13").val() == "" ? "1.50" : $("#FromSize13").val();
        var TO_VAL = $("#ToSize13").val() == undefined ? "1.99" : $("#ToSize13").val() == "" ? "1.99" : $("#ToSize13").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 12, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 12) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 12) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }

    if ($("#CARAT_Size15").is(":checked") == true) {
        var FROM_VAL = $("#FromSize15").val() == undefined ? "2.00" : $("#FromSize15").val() == "" ? "2.00" : $("#FromSize15").val();
        var TO_VAL = $("#ToSize15").val() == undefined ? "2.99" : $("#ToSize15").val() == "" ? "2.99" : $("#ToSize15").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 13, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 13) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 13) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }

    if ($("#CARAT_Size17").is(":checked") == true) {
        var FROM_VAL = $("#FromSize17").val() == undefined ? "3.00" : $("#FromSize17").val() == "" ? "3.00" : $("#FromSize17").val();
        var TO_VAL = $("#ToSize17").val() == undefined ? "99.99" : $("#ToSize17").val() == "" ? "99.99" : $("#ToSize17").val();
        var str = FROM_VAL + "-" + TO_VAL;
        var obj = { id: 14, Carat: str, };

        if (SSN_CARAT.length > 0) {
            SSN_CARAT.forEach(function (item, i) {
                if (item.id == 14) {
                    var I = (SSN_CARAT.indexOf(item));
                    SSN_CARAT.splice(I, 1);
                }
            });
        }
        SSN_CARAT.push(obj);
    }
    else {
        SSN_CARAT.forEach(function (item, i) {
            if (item.id == 14) {
                var I = (SSN_CARAT.indexOf(item));
                SSN_CARAT.splice(I, 1);
            }
        });
    }

    SSN_CARAT.forEach(function (e) {
        CheckedCaratValue = CheckedCaratValue + e.Carat + ",";
    });
    CheckedCaratValue = CheckedCaratValue.replace(/,\s*$/, "");

    //if (CheckedCaratValue != "") {
    $(".divCheckedCaratValue").empty();
    $(".divCheckedCaratValue").append(CheckedCaratValue);
    //}

    SetSearchParameter();
}
var AddNewRow = function () {
    if ($("#PricingMethod").val() == undefined || $("#PricingMethod").val() == "") {
        toastr.warning("Please Select Price Method !", { timeOut: 2500 });
        $("#PricingMethod").focus();
        return;
    }

    if ($("#PricingMethod").val() != undefined || $("#PricingMethod").val() != "") {
        if ($("#Percentage").val() == undefined || $("#Percentage").val() == "") {
            toastr.warning("Please Enter Percentage !", { timeOut: 2500 });
            $("#Percentage").focus();
            return;
        }
    }

    $("#btnAddNewRow").attr("disabled", true);
    $("#mytable1").show();

    var KeyToSymLst_Check1 = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
    var KeyToSymLst_uncheck1 = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");

    var cntRow = parseInt($("#mytable1 #myTableBody1").find('tr').length) + 1;

    var Supplier = _.pluck(_.filter(SupplierList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Location = _.pluck(_.filter(LocationList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Shape = _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Carat = CheckedCaratValue;
    var Color = _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Clarity = _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Cut = _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Polish = _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Sym = _.pluck(_.filter(SymList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Fls = _.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Lab = _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'Value').join(",");
    var FromLength = $("#FromLength").val() == "" || $("#FromLength").val() == undefined || $("#FromLength").val() == "0.00" ? "0" : $("#FromLength").val();
    var ToLength = $("#ToLength").val() == "" || $("#ToLength").val() == undefined || $("#ToLength").val() == "0.00" ? "0" : $("#ToLength").val();
    var FromWidth = $("#FromWidth").val() == "" || $("#FromWidth").val() == undefined || $("#FromWidth").val() == "0.00" ? "0" : $("#FromWidth").val();
    var ToWidth = $("#ToWidth").val() == "" || $("#ToWidth").val() == undefined || $("#ToWidth").val() == "0.00" ? "0" : $("#ToWidth").val();
    var FromDepth = $("#FromDepth").val() == "" || $("#FromDepth").val() == undefined || $("#FromDepth").val() == "0.00" ? "0" : $("#FromDepth").val();
    var ToDepth = $("#ToDepth").val() == "" || $("#ToDepth").val() == undefined || $("#ToDepth").val() == "0.00" ? "0" : $("#ToDepth").val();
    var FromDepthinPer = $("#FromDepthPer").val() == "" || $("#FromDepthPer").val() == undefined || $("#FromDepthPer").val() == "0.00" ? "0" : $("#FromDepthPer").val();
    var ToDepthinPer = $("#ToDepthPer").val() == "" || $("#ToDepthPer").val() == undefined || $("#ToDepthPer").val() == "0.00" ? "0" : $("#ToDepthPer").val();
    var FromTableinPer = $("#FromTablePer").val() == "" || $("#FromTablePer").val() == undefined || $("#FromTablePer").val() == "0.00" ? "0" : $("#FromTablePer").val();
    var ToTableinPer = $("#ToTablePer").val() == "" || $("#ToTablePer").val() == undefined || $("#ToTablePer").val() == "0.00" ? "0" : $("#ToTablePer").val();
    var FromCrAng = $("#FromCrAng").val() == "" || $("#FromCrAng").val() == undefined || $("#FromCrAng").val() == "0.00" ? "0" : $("#FromCrAng").val();
    var ToCrAng = $("#ToCrAng").val() == "" || $("#ToCrAng").val() == undefined || $("#ToCrAng").val() == "0.00" ? "0" : $("#ToCrAng").val();
    var FromCrHt = $("#FromCrHt").val() == "" || $("#FromCrHt").val() == undefined || $("#FromCrHt").val() == "0.00" ? "0" : $("#FromCrHt").val();
    var ToCrHt = $("#ToCrHt").val() == "" || $("#ToCrHt").val() == undefined || $("#ToCrHt").val() == "0.00" ? "0" : $("#ToCrHt").val();
    var FromPavAng = $("#FromPavAng").val() == "" || $("#FromPavAng").val() == undefined || $("#FromPavAng").val() == "0.00" ? "0" : $("#FromPavAng").val();
    var ToPavAng = $("#ToPavAng").val() == "" || $("#ToPavAng").val() == undefined || $("#ToPavAng").val() == "0.00" ? "0" : $("#ToPavAng").val();
    var FromPavHt = $("#FromPavHt").val() == "" || $("#FromPavHt").val() == undefined || $("#FromPavHt").val() == "0.00" ? "0" : $("#FromPavHt").val();
    var ToPavHt = $("#ToPavHt").val() == "" || $("#ToPavHt").val() == undefined || $("#ToPavHt").val() == "0.00" ? "0" : $("#ToPavHt").val();
    var Keytosymbol = KeyToSymLst_Check1 + (KeyToSymLst_Check1 == "" || KeyToSymLst_uncheck1 == "" ? "" : "-") + KeyToSymLst_uncheck1;
    var dCheckKTS = KeyToSymLst_Check1;
    var dUNCheckKTS = KeyToSymLst_uncheck1;
    var BGM = _.pluck(_.filter(BGMList, function (e) { return e.isActive == true }), 'Value').join(",");
    var CrownBlack = _.pluck(_.filter(CrownBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
    var TableBlack = _.pluck(_.filter(TableBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
    var CrownWhite = _.pluck(_.filter(CrownWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");
    var TableWhite = _.pluck(_.filter(TableWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Image = $('#Img:checked').val();
    var Video = $('#Vdo:checked').val();
    var PriceMethod = $("#PricingMethod").val() == "" && $("#PricingMethod").val() == undefined ? "" : $("#PricingMethod").val();
    var Percentage = $("#PricingMethod").val() == "" || $("#Percentage").val() == "" || $("#Percentage").val() == undefined ? "" : $("#Percentage").val();

    var html = "<tr id='tr'>";
    html += "<th class='Row Fi-Criteria' style=''>" + cntRow.toString() + "</th>";
    html += "<td style=''><span class='Fi-Criteria Supplier'>" + Supplier + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Location'>" + Location + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Shape'>" + Shape + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Carat'>" + Carat + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Color'>" + Color + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Clarity'>" + Clarity + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Cut'>" + Cut + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Polish'>" + Polish + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Sym'>" + Sym + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Fls'>" + Fls + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Lab'>" + Lab + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromLength'>" + FromLength + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToLength'>" + ToLength + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromWidth'>" + FromWidth + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToWidth'>" + ToWidth + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromDepth'>" + FromDepth + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToDepth'>" + ToDepth + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromDepthinPer'>" + FromDepthinPer + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToDepthinPer'>" + ToDepthinPer + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromTableinPer'>" + FromTableinPer + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToTableinPer'>" + ToTableinPer + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromCrAng'>" + FromCrAng + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToCrAng'>" + ToCrAng + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromCrHt'>" + FromCrHt + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToCrHt'>" + ToCrHt + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromPavAng'>" + FromPavAng + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToPavAng'>" + ToPavAng + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria FromPavHt'>" + FromPavHt + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria ToPavHt'>" + ToPavHt + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Keytosymbol'>" + Keytosymbol + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria dCheckKTS'>" + dCheckKTS + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria dUNCheckKTS'>" + dUNCheckKTS + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria BGM'>" + BGM + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria CrownBlack'>" + CrownBlack + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria TableBlack'>" + TableBlack + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria CrownWhite'>" + CrownWhite + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria TableWhite'>" + TableWhite + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Image'>" + Image + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Video'>" + Video + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria PriceMethod'>" + PriceMethod + "</span></td>";
    html += "<td style=''><span class='Fi-Criteria Percentage'>" + Percentage + "</span></td>";
    html += "<td style='width: 50px'>" + '<i style="cursor:pointer;" class="error RemoveCriteria"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;"/></i>' + "</td>";
    html += "</tr>";

    $("#mytable1 #myTableBody1").append(html);

    $("#btnAddNewRow").attr("disabled", false);
    Reset_API_Filter();
}
var LeaveTextBox = function (ele, type) {

    if (type == "CARAT") {
        $("#FromCarat").val($("#FromCarat").val() == "" ? "0.00" : $("#FromCarat").val() == undefined || $("#FromCarat").val() == "NAN" ? "0.00" : parseFloat($("#FromCarat").val()).toFixed(2));
        $("#ToCarat").val($("#ToCarat").val() == "" ? "0.00" : $("#ToCarat").val() == undefined || $("#ToCarat").val() == "NAN" ? "0.00" : parseFloat($("#ToCarat").val()).toFixed(2));

        var fromCarat = parseFloat($("#FromCarat").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromCarat").val()).toFixed(2);
        var toCarat = parseFloat($("#ToCarat").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToCarat").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromCarat).toFixed(2) > parseFloat(toCarat).toFixed(2)) {
                $("#ToCarat").val(fromCarat);
                if (fromCarat == 0) {
                    $("#FromCarat").val(0);
                    $("#ToCarat").val(0);
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toCarat).toFixed(2) < parseFloat(fromCarat).toFixed(2)) {
                $("#FromCarat").val($("#ToCarat").val());
                if (toCarat == 0) {
                    $("#FromCarat").val(0);
                    $("#ToCarat").val(0);
                }
            }
        }
    }
    if (type == "LENGTH") {
        $("#FromLength").val($("#FromLength").val() == "" ? "0.00" : $("#FromLength").val() == undefined ? "0.00" : parseFloat($("#FromLength").val()).toFixed(2));
        $("#ToLength").val($("#ToLength").val() == "" ? "0.00" : $("#ToLength").val() == undefined ? "0.00" : parseFloat($("#ToLength").val()).toFixed(2));

        var fromLength = parseFloat($("#FromLength").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromLength").val()).toFixed(2);
        var toLength = parseFloat($("#ToLength").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToLength").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromLength).toFixed(2) > parseFloat(toLength).toFixed(2)) {
                $("#ToLength").val(fromLength);
                if (fromLength == 0) {
                    $("#FromLength").val(0);
                    $("#ToLength").val(0);
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toLength).toFixed(2) < parseFloat(fromLength).toFixed(2)) {
                $("#FromLength").val($("#ToLength").val());
                if (toLength == 0) {
                    $("#FromLength").val(0);
                    $("#ToLength").val(0);
                }
            }
        }
    }
    if (type == "WIDTH") {
        $("#FromWidth").val($("#FromWidth").val() == "" ? "0.00" : $("#FromWidth").val() == undefined ? "0.00" : parseFloat($("#FromWidth").val()).toFixed(2));
        $("#ToWidth").val($("#ToWidth").val() == "" ? "0.00" : $("#ToWidth").val() == undefined ? "0.00" : parseFloat($("#ToWidth").val()).toFixed(2));

        var fromWidth = parseFloat($("#FromWidth").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromWidth").val()).toFixed(2);
        var toWidth = parseFloat($("#ToWidth").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToWidth").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromWidth).toFixed(2) > parseFloat(toWidth).toFixed(2)) {
                $("#ToWidth").val(fromWidth);
                if (fromWidth == 0) {
                    $("#FromWidth").val("0.00");
                    $("#ToWidth").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toWidth).toFixed(2) < parseFloat(fromWidth).toFixed(2)) {
                $("#FromWidth").val($("#ToWidth").val());
                if (toWidth == 0) {
                    $("#FromWidth").val("0.00");
                    $("#ToWidth").val("0.00");
                }
            }
        }
    }
    if (type == "DEPTH") {
        $("#FromDepth").val($("#FromDepth").val() == "" ? "0.00" : $("#FromDepth").val() == undefined ? "0.00" : parseFloat($("#FromDepth").val()).toFixed(2));
        $("#ToDepth").val($("#ToDepth").val() == "" ? "0.00" : $("#ToDepth").val() == undefined ? "0.00" : parseFloat($("#ToDepth").val()).toFixed(2));

        var fromDepth = parseFloat($("#FromDepth").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromDepth").val()).toFixed(2);
        var toDepth = parseFloat($("#ToDepth").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToDepth").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromDepth).toFixed(2) > parseFloat(toDepth).toFixed(2)) {
                $("#ToDepth").val(fromDepth);
                if (fromDepth == 0) {
                    $("#FromDepth").val("0.00");
                    $("#ToDepth").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toDepth).toFixed(2) < parseFloat(fromDepth).toFixed(2)) {
                $("#FromDepth").val($("#ToDepth").val());
                if (toDepth == 0) {
                    $("#FromDepth").val("0.00");
                    $("#ToDepth").val("0.00");
                }
            }
        }
    }
    if (type == "DEPTHPER") {
        $("#FromDepthPer").val($("#FromDepthPer").val() == "" ? "0.00" : $("#FromDepthPer").val() == undefined ? "0.00" : parseFloat($("#FromDepthPer").val()).toFixed(2));
        $("#ToDepthPer").val($("#ToDepthPer").val() == "" ? "0.00" : $("#ToDepthPer").val() == undefined ? "0.00" : parseFloat($("#ToDepthPer").val()).toFixed(2));

        var fromDepthPer = parseFloat($("#FromDepthPer").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromDepthPer").val()).toFixed(2);
        var toDepthPer = parseFloat($("#ToDepthPer").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToDepthPer").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromDepthPer).toFixed(2) > parseFloat(toDepthPer).toFixed(2)) {
                $("#ToDepthPer").val(fromDepthPer);
                if (fromDepthPer == 0) {
                    $("#FromDepthPer").val("0.00");
                    $("#ToDepthPer").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toDepthPer).toFixed(2) < parseFloat(fromDepthPer).toFixed(2)) {
                $("#FromDepthPer").val($("#ToDepthPer").val());
                if (toDepthPer == 0) {
                    $("#FromDepthPer").val("0.00");
                    $("#ToDepthPer").val("0.00");
                }
            }
        }

    }
    if (type == "TABLEPER") {
        $("#FromTablePer").val($("#FromTablePer").val() == "" ? "0.00" : $("#FromTablePer").val() == undefined ? "0.00" : parseFloat($("#FromTablePer").val()).toFixed(2));
        $("#ToTablePer").val($("#ToTablePer").val() == "" ? "0.00" : $("#ToTablePer").val() == undefined ? "0.00" : parseFloat($("#ToTablePer").val()).toFixed(2));

        var fromTablePer = parseFloat($("#FromTablePer").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromTablePer").val()).toFixed(2);
        var toTablePer = parseFloat($("#ToTablePer").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToTablePer").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromTablePer).toFixed(2) > parseFloat(toTablePer).toFixed(2)) {
                $("#ToTablePer").val(fromTablePer);
                if (fromTablePer == 0) {
                    $("#FromTablePer").val("0.00");
                    $("#ToTablePer").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toTablePer).toFixed(2) < parseFloat(fromTablePer).toFixed(2)) {
                $("#FromTablePer").val($("#ToTablePer").val());
                if (toTablePer == 0) {
                    $("#FromTablePer").val("0.00");
                    $("#ToTablePer").val("0.00");
                }
            }
        }
    }
    if (type == "CRANG") {
        $("#FromCrAng").val($("#FromCrAng").val() == "" ? "0.00" : $("#FromCrAng").val() == undefined ? "0.00" : parseFloat($("#FromCrAng").val()).toFixed(2));
        $("#ToCrAng").val($("#ToCrAng").val() == "" ? "0.00" : $("#ToCrAng").val() == undefined ? "0.00" : parseFloat($("#ToCrAng").val()).toFixed(2));

        var fromCrAng = parseFloat($("#FromCrAng").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromCrAng").val()).toFixed(2);
        var toCrAng = parseFloat($("#ToCrAng").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToCrAng").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromCrAng).toFixed(2) > parseFloat(toCrAng).toFixed(2)) {
                $("#ToCrAng").val(fromCrAng);
                if (fromCrAng == 0) {
                    $("#FromCrAng").val("0.00");
                    $("#ToCrAng").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toCrAng).toFixed(2) < parseFloat(fromCrAng).toFixed(2)) {
                $("#FromCrAng").val($("#ToCrAng").val());
                if (toCrAng == 0) {
                    $("#FromCrAng").val("0.00");
                    $("#ToCrAng").val("0.00");
                }
            }
        }
    }
    if (type == "CRHT") {
        $("#FromCrHt").val($("#FromCrHt").val() == "" ? "0.00" : $("#FromCrHt").val() == undefined ? "0.00" : parseFloat($("#FromCrHt").val()).toFixed(2));
        $("#ToCrHt").val($("#ToCrHt").val() == "" ? "0.00" : $("#ToCrHt").val() == undefined ? "0.00" : parseFloat($("#ToCrHt").val()).toFixed(2));

        var fromCrHt = parseFloat($("#FromCrHt").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromCrHt").val()).toFixed(2);
        var toCrHt = parseFloat($("#ToCrHt").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToCrHt").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromCrHt).toFixed(2) > parseFloat(toCrHt).toFixed(2)) {
                $("#ToCrHt").val(fromCrHt);
                if (fromCrHt == 0) {
                    $("#FromCrHt").val("0.00");
                    $("#ToCrHt").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toCrHt).toFixed(2) < parseFloat(fromCrHt).toFixed(2)) {
                $("#FromCrHt").val($("#ToCrHt").val());
                if (toCrHt == 0) {
                    $("#FromCrHt").val("0.00");
                    $("#ToCrHt").val("0.00");
                }
            }
        }
    }
    if (type == "PAVANG") {
        $("#FromPavAng").val($("#FromPavAng").val() == "" ? "0.00" : $("#FromPavAng").val() == undefined ? "0.00" : parseFloat($("#FromPavAng").val()).toFixed(2));
        $("#ToPavAng").val($("#ToPavAng").val() == "" ? "0.00" : $("#ToPavAng").val() == undefined ? "0.00" : parseFloat($("#ToPavAng").val()).toFixed(2));

        var fromPavAng = parseFloat($("#FromPavAng").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromPavAng").val()).toFixed(2);
        var toPavAng = parseFloat($("#ToPavAng").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToPavAng").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromPavAng).toFixed(2) > parseFloat(toPavAng).toFixed(2)) {
                $("#ToPavAng").val(fromPavAng);
                if (fromPavAng == 0) {
                    $("#FromPavAng").val("0.00");
                    $("#ToPavAng").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toPavAng).toFixed(2) < parseFloat(fromPavAng).toFixed(2)) {
                $("#FromPavAng").val($("#ToPavAng").val());
                if (toPavAng == 0) {
                    $("#FromPavAng").val("0.00");
                    $("#ToPavAng").val("0.00");
                }
            }
        }

    }
    if (type == "PAVHT") {
        $("#FromPavHt").val($("#FromPavHt").val() == "" ? "0.00" : $("#FromPavHt").val() == undefined ? "0.00" : parseFloat($("#FromPavHt").val()).toFixed(2));
        $("#ToPavHt").val($("#ToPavHt").val() == "" ? "0.00" : $("#ToPavHt").val() == undefined ? "0.00" : parseFloat($("#ToPavHt").val()).toFixed(2));

        var fromPavHt = parseFloat($("#FromPavHt").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromPavHt").val()).toFixed(2);
        var toPavHt = parseFloat($("#ToPavHt").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToPavHt").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(fromPavHt).toFixed(2) > parseFloat(toPavHt).toFixed(2)) {
                $("#ToPavHt").val(fromPavHt);
                if (fromPavHt == 0) {
                    $("#FromPavHt").val("0.00");
                    $("#ToPavHt").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toPavHt).toFixed(2) < parseFloat(fromPavHt).toFixed(2)) {
                $("#FromPavHt").val($("#ToPavHt").val());
                if (toPavHt == 0) {
                    $("#FromPavHt").val("0.00");
                    $("#ToPavHt").val("0.00");
                }
            }
        }
    }
    if (type == "DISC") {
        $("#FromCrAng").val($("#FromCrAng").val() == "" ? "0.00" : $("#FromCrAng").val() == undefined ? "0.00" : parseFloat($("#FromCrAng").val()).toFixed(2));
        $("#ToDisc").val($("#ToDisc").val() == "" ? "0.00" : $("#ToDisc").val() == undefined ? "0.00" : parseFloat($("#ToDisc").val()).toFixed(2));

        var FromCrAng = parseFloat($("#FromCrAng").val()).toFixed(2) == "" ? 0 : parseFloat($("#FromCrAng").val()).toFixed(2);
        var toDisc = parseFloat($("#ToDisc").val()).toFixed(2) == "" ? 0 : parseFloat($("#ToDisc").val()).toFixed(2);
        if (ele == "FROM") {
            if (parseFloat(FromCrAng).toFixed(2) > parseFloat(toDisc).toFixed(2)) {
                $("#ToDisc").val(FromCrAng);
                if (FromCrAng == 0) {
                    $("#FromCrAng").val("0.00");
                    $("#ToDisc").val("0.00");
                }
            }
        }
        else if (ele == "TO") {
            if (parseFloat(toDisc).toFixed(2) < parseFloat(FromCrAng).toFixed(2)) {
                $("#FromCrAng").val($("#ToDisc").val());
                if (toDisc == 0) {
                    $("#FromCrAng").val("0.00");
                    $("#ToDisc").val("0.00");
                }
            }
        }
    }
}
function Reset_API_Filter() {
    ResetSelectedAttr('.divCheckedSupplierValue', SupplierList);
    ResetSelectedAttr('.divCheckedLocationValue', LocationList);
    ResetSelectedAttr('.divCheckedShapeValue', ShapeList);
    ResetCheckCarat();
    ResetSelectedAttr('.divCheckedColorValue', ColorList);
    ResetSelectedAttr('.divCheckedClarityValue', ClarityList);
    ResetSelectedAttr('.divCheckedCutValue', CutList);
    ResetSelectedAttr('.divCheckedPolValue', PolishList);
    ResetSelectedAttr('.divCheckedSymValue', SymList);
    ResetSelectedAttr('.divCheckedFLsValue', FlsList);
    ResetSelectedAttr('.divCheckedLabValue', LabList);
    $("#FromLength").val("0.00");
    $("#ToLength").val("0.00");
    $("#FromWidth").val("0.00");
    $("#ToWidth").val("0.00");
    $("#FromDepth").val("0.00");
    $("#ToDepth").val("0.00");
    $("#FromDepthPer").val("0.00");
    $("#ToDepthPer").val("0.00");
    $("#FromTablePer").val("0.00");
    $("#ToTablePer").val("0.00");
    $("#FromCrAng").val("0.00");
    $("#ToCrAng").val("0.00");
    $("#FromCrHt").val("0.00");
    $("#ToCrHt").val("0.00");
    $("#FromPavAng").val("0.00");
    $("#ToPavAng").val("0.00");
    $("#FromPavHt").val("0.00");
    $("#ToPavHt").val("0.00");
    resetKeytoSymbol();
    ResetSelectedAttr('.divCheckedBGMValue', BGMList);
    ResetSelectedAttr('.divCheckedCrnBlackValue', CrownBlackList);
    ResetSelectedAttr('.divCheckedTblBlackValue', TableBlackList);
    ResetSelectedAttr('.divCheckedCrnWhiteValue', CrownWhiteList);
    ResetSelectedAttr('.divCheckedTblWhiteValue', TableWhiteList);
    $("#PricingMethod").val("");
    $("#Percentage").val("0");
    document.getElementById("Percentage").disabled = true;
    $(".IgAll").prop("checked", true);
    $(".VdAll").prop("checked", true);
}
$("#mytable tbody").sortable({
    update: function () {
        SetTableOrder();
    }
});
function SetTableOrder() {
    var OrderNo = 1;
    $("#mytable tbody tr").each(function () {
        ($(this).find(".ColumnOrder").text(OrderNo));
        OrderNo = OrderNo + 1;
    });
};
var ColumnsettingList = function (iTransId) {
    $("#loading").css("display", "block");
    $.ajax({
        url: "/Customer/Get_API_ColumnMas",
        async: false,
        type: "POST",
        data: { iTransId: iTransId },
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Message == "SUCCESS") {
                ColumnList = [];
                NewColumnList = [];
                data.Data.forEach(function (item, index) {
                    item.SEQ_NO = item.SEQ_NO,
                        item.COLUMN_NAME = item.COLUMN_NAME,
                        item.DISPLAY_NAME = item.DISPLAY_NAME,
                        item.sCustMiseCaption = item.sCustMiseCaption,
                        item.SORT_NO = item.SORT_NO,
                        item.VISIBLE = item.VISIBLE,
                        item.iPriority = index + 1,
                        item.IsActive = item.VISIBLE;
                });
                ColumnList = data.Data;
                CreateTable(ColumnList);
            }
            $("#loading").css("display", "none");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#loading").css("display", "none");
        }
    });
}
var CreateTable = function (Columns) {
    var trs = "";
    $("#myTableBody").empty();
    Columns.forEach(function (item) {
        trs += "";
        trs += '<tr>'
        trs += '<td id="lblCoolName" style="display: none;"></td>';
        trs += '<td><i style="cursor: move;" class="fa fa-bars" aria-hidden="true"></i></td>';
        trs += '<td id="lblFieldName" class="onbinding">' + item.COLUMN_NAME + '</td>';
        trs += '<td id="lblFieldName" class="onbinding">' + item.DISPLAY_NAME + '</td>';
        trs += '<td class="CustName">';
        trs += '<input onblur="" type="text" class="form-control input-sm onpristine onvalid onnot-empty onvalid-maxlength ontouched" value="' + item.sCustMiseCaption + '" maxlength="100">';
        trs += '</td>';
        trs += '<td id="lblColId" style="display: none; " class="onbinding">' + item.SEQ_NO + '</td>';
        trs += '<td id="lblOrder" class="ColumnOrder onbinding">' + item.iPriority + '</td>';
        trs += '<td><center>';
        if (item.IsActive) {
            trs += '<img src="/Content/images/chebox-fill.png" class="chebox-fill img-block" id="chebox_fillImg_' + item.SEQ_NO + '" onclick="chebox_fill(' + item.SEQ_NO + ')" style="cursor:pointer;width: 20px;" />';
            trs += '<img src="/Content/images/chebox-empty.png" class="chebox-empty img-none" id="chebox_emptyImg_' + item.SEQ_NO + '" onclick="chebox_empty(' + item.SEQ_NO + ')" style="cursor:pointer;width: 20px;margin-bottom: 7px;" />';
        }
        else {
            trs += '<img src="/Content/images/chebox-fill.png" class="chebox-fill img-none" id="chebox_fillImg_' + item.SEQ_NO + '" onclick="chebox_fill(' + item.SEQ_NO + ')" style="cursor:pointer;width: 20px" />';
            trs += '<img src="/Content/images/chebox-empty.png" class="chebox-empty img-block" id="chebox_emptyImg_' + item.SEQ_NO + '" onclick="chebox_empty(' + item.SEQ_NO + ')" style="cursor:pointer;width: 20px;margin-top: 0px;" />';
        }
        trs += '</center></td>';
        trs += '</tr>';

    });
    $("#myTableBody").html(trs);
}
function chebox_fill(icolumnId) {
    if (icolumnId == "header") {
        $("#chebox_fillImg_Header").removeClass('img-block');
        $("#chebox_fillImg_Header").addClass('img-none');

        $("#chebox_emptyImg_Header").removeClass('img-none');
        $("#chebox_emptyImg_Header").addClass('img-block');

        $(".chebox-fill").addClass('img-none');
        $(".chebox-fill").removeClass('img-block');

        $(".chebox-empty").removeClass('img-none');
        $(".chebox-empty").addClass('img-block');

    } else {
        $("#chebox_fillImg_" + icolumnId).addClass('img-none');
        $("#chebox_fillImg_" + icolumnId).removeClass('img-block');

        $("#chebox_emptyImg_" + icolumnId).removeClass('img-none');
        $("#chebox_emptyImg_" + icolumnId).addClass('img-block');
    }
}
function chebox_empty(icolumnId) {
    if (icolumnId == "header") {
        $("#chebox_emptyImg_Header").removeClass('img-block');
        $("#chebox_emptyImg_Header").addClass('img-none');

        $("#chebox_fillImg_Header").removeClass('img-none');
        $("#chebox_fillImg_Header").addClass('img-block');

        $(".chebox-fill").removeClass('img-none');
        $(".chebox-fill").addClass('img-block');

        $(".chebox-empty").addClass('img-none');
        $(".chebox-empty").removeClass('img-block');

    } else {
        $("#chebox_fillImg_" + icolumnId).removeClass('img-none');
        $("#chebox_fillImg_" + icolumnId).addClass('img-block');

        $("#chebox_emptyImg_" + icolumnId).addClass('img-none');
        $("#chebox_emptyImg_" + icolumnId).removeClass('img-block');
    }
}
var SaveApiData = function () {
    try {
        if ($('#ApiMethod:checked').val() == "WEBAPI") {
            if ($("#WebAPIUserName").val() == undefined || $("#WebAPIUserName").val() == "") {
                toastr.warning("Please Enter User Name !", { timeOut: 2500 });
                $("#WebAPIUserName").focus();
                return;
            }
            if ($("#WebAPIPassword").val() == undefined || $("#WebAPIPassword").val() == "") {
                toastr.warning("Please Enter Password !", { timeOut: 2500 });
                $("#WebAPIPassword").focus();
                return;
            }
        }
        if ($('#ApiMethod:checked').val() == "FTP") {
            if ($("#FTPType").val() == undefined || $("#FTPType").val() == "") {
                toastr.warning("Please Enter Type !", { timeOut: 2500 });
                $("#FTPType").focus();
                return;
            }
            if ($("#FTPHost").val() == undefined || $("#FTPHost").val() == "") {
                toastr.warning("Please Enter FTP Host !", { timeOut: 2500 });
                $("#FTPHost").focus();
                return;
            }
            if ($("#FTPUser").val() == undefined || $("#FTPUser").val() == "") {
                toastr.warning("Please Enter FTP Uname !", { timeOut: 2500 });
                $("#FTPUser").focus();
                return;
            }
            if ($("#FTPPass").val() == undefined || $("#FTPPass").val() == "") {
                toastr.warning("Please Enter FTP Password !", { timeOut: 2500 });
                $("#FTPPass").focus();
                return;
            }
            if ($("#FTPExportType").val() == undefined || $("#FTPExportType").val() == "") {
                toastr.warning("Please Select Export Type !", { timeOut: 2500 });
                $("#FTPExportType").focus();
                return;
            }
        }
        if ($('#ApiMethod:checked').val() == "URL") {
            if ($("#URLUserName").val() == undefined || $("#URLUserName").val() == "") {
                toastr.warning("Please Enter User Name !", { timeOut: 2500 });
                $("#URLUserName").focus();
                return;
            }
            if ($("#URLPassword").val() == undefined || $("#URLPassword").val() == "") {
                toastr.warning("Please Enter Password !", { timeOut: 2500 });
                $("#URLPassword").focus();
                return;
            }
            if ($("#URLExportType").val() == undefined || $("#URLExportType").val() == "") {
                toastr.warning("Please Select Export Type !", { timeOut: 2500 });
                $("#URLExportType").focus();
                return;
            }
        }
        if ($('#ApiMethod:checked').val() == "Location") {
            if ($("#txtFileLocation").val() == undefined || $("#txtFileLocation").val() == "") {
                toastr.warning("Please Enter File Location !", { timeOut: 2500 });
                $("#txtFileLocation").focus();
                return;
            }
            if ($("#LocationExportType").val() == undefined || $("#LocationExportType").val() == "") {
                toastr.warning("Please Select Export Type !", { timeOut: 2500 });
                $("#LocationExportType").focus();
                return;
            }
            if ($('#Location:checked').val() == "Onetime") {
                if ($("#txtOnetimeDate").val() == undefined || $("#txtOnetimeDate").val() == "") {
                    toastr.warning("Please Select File Transfer Date !", { timeOut: 2500 });
                    $("#txtOnetimeDate").focus();
                    return;
                }
                if ($("#txtOnetime").val() == undefined || $("#txtOnetime").val() == "") {
                    toastr.warning("Please Select File Transfer Time !", { timeOut: 2500 });
                    $("#txtOnetime").focus();
                    return;
                }
            }
            else if ($('#Location:checked').val() == "Repeatevery") {
                if ($("#txtRepeatevery").val() == undefined || $("#txtRepeatevery").val() == "") {
                    toastr.warning("Please Enter Repeat every " + $("#DdlRepeatevery").val() + " !", { timeOut: 2500 });
                    $("#txtRepeatevery").focus();
                    return;
                }
            }
        }
        if ($("#APIName").val() == undefined || $("#APIName").val() == "") {
            toastr.warning("Please Enter File Name !", { timeOut: 2500 });
            $("#APIName").focus();
            return;
        }
        if ($("#mytable1 #myTableBody1").find('tr').length == 0) {
            toastr.warning("Please Add Minimum 1 API Filter !", { timeOut: 2500 });
            return;
        }

        $("#loading").css("display", "block");

        var Arr1 = [];
        var Arr2 = [];
        $("#mytable tbody tr").each(function () {
            var Index = $(this).index();
            var icolumnId = $(this).find("td:eq(5)").html().trim();
            var ColumnName = $(this).find("td:eq(3)").html().trim();
            var EditColumnName = $(this).find("input").val();
            //var Visibility = $(this).find("#checkbox2").prop("checked");
            if ($('#chebox_fillImg_' + icolumnId).hasClass('img-block')) {
                var Visibility = true;
            }
            else {
                var Visibility = false;
            }
            Arr2.push({ iPriority: Index, sUser_ColumnName: ColumnName, IsActive: Visibility, EditColumnName: EditColumnName, icolumnId: icolumnId });
            Arr1 = _.filter(Arr2, function (e) { return e.IsActive == true });
        });

        var List1 = [];
        Arr1.forEach(function (e) {
            List1.push({
                "icolumnId": e.icolumnId,
                "iPriority": e.iPriority + 1,
                "sUser_ColumnName": e.sUser_ColumnName,
                "sCustMiseCaption": e.EditColumnName,
            });
        });

        var List2 = [];
        $("#mytable1 #myTableBody1 tr").each(function () {
            var Index = $(this).index();
            var Supplier = $(this).find('.Supplier').html();
            var Location = $(this).find('.Location').html();
            var Shape = $(this).find('.Shape').html();
            var Carat = $(this).find('.Carat').html();
            var Color = $(this).find('.Color').html();
            var Clarity = $(this).find('.Clarity').html();
            var Cut = $(this).find('.Cut').html();
            var Polish = $(this).find('.Polish').html();
            var Sym = $(this).find('.Sym').html();
            var Fls = $(this).find('.Fls').html();
            var Lab = $(this).find('.Lab').html();
            var FromLength = $(this).find('.FromLength').html();
            var ToLength = $(this).find('.ToLength').html();
            var FromWidth = $(this).find('.FromWidth').html();
            var ToWidth = $(this).find('.ToWidth').html();
            var FromDepth = $(this).find('.FromDepth').html();
            var ToDepth = $(this).find('.ToDepth').html();
            var FromDepthinPer = $(this).find('.FromDepthinPer').html();
            var ToDepthinPer = $(this).find('.ToDepthinPer').html();
            var FromTableinPer = $(this).find('.FromTableinPer').html();
            var ToTableinPer = $(this).find('.ToTableinPer').html();
            var FromCrAng = $(this).find('.FromCrAng').html();
            var ToCrAng = $(this).find('.ToCrAng').html();
            var FromCrHt = $(this).find('.FromCrHt').html();
            var ToCrHt = $(this).find('.ToCrHt').html();
            var FromPavAng = $(this).find('.FromPavAng').html();
            var ToPavAng = $(this).find('.ToPavAng').html();
            var FromPavHt = $(this).find('.FromPavHt').html();
            var ToPavHt = $(this).find('.ToPavHt').html();
            var Keytosymbol = $(this).find('.Keytosymbol').html();
            var dCheckKTS = $(this).find('.dCheckKTS').html();
            var dUNCheckKTS = $(this).find('.dUNCheckKTS').html();
            var BGM = $(this).find('.BGM').html();
            var CrownBlack = $(this).find('.CrownBlack').html();
            var TableBlack = $(this).find('.TableBlack').html();
            var CrownWhite = $(this).find('.CrownWhite').html();
            var TableWhite = $(this).find('.TableWhite').html();
            var Image = $(this).find('.Image').html();
            var Video = $(this).find('.Video').html();
            var PriceMethod = $(this).find('.PriceMethod').html();
            var Percentage = $(this).find('.Percentage').html();

            List2.push({
                iSupplier: Supplier,
                iLocation: Location,
                sShape: Shape,
                sPointer: Carat,
                sColor: Color,
                sClarity: Clarity,
                sCut: Cut,
                sPolish: Polish,
                sSymm: Sym,
                sFls: Fls,
                sLab: Lab,
                dFromLength: FromLength,
                dToLength: ToLength,
                dFromWidth: FromWidth,
                dToWidth: ToWidth,
                dFromDepth: FromDepth,
                dToDepth: ToDepth,
                dFromDepthPer: FromDepthinPer,
                dToDepthPer: ToDepthinPer,
                dFromTablePer: FromTableinPer,
                dToTablePer: ToTableinPer,
                dFromCrAng: FromCrAng,
                dToCrAng: ToCrAng,
                dFromCrHt: FromCrHt,
                dToCrHt: ToCrHt,
                dFromPavAng: FromPavAng,
                dToPavAng: ToPavAng,
                dFromPavHt: FromPavHt,
                dToPavHt: ToPavHt,
                dKeyToSymbol: Keytosymbol,
                dCheckKTS: dCheckKTS,
                dUNCheckKTS: dUNCheckKTS,
                sBGM: BGM,
                sCrownBlack: CrownBlack,
                sTableBlack: TableBlack,
                sCrownWhite: CrownWhite,
                sTableWhite: TableWhite,
                Img: Image,
                Vdo: Video,
                PriceMethod: PriceMethod,
                PricePer: Percentage
            });
        });
        var obj = CreateModel();
        obj.ColumnsSettings = List1;
        obj.APIFilters = List2;

        $.ajax({
            url: "/Customer/SaveUploadMethod",
            async: false,
            type: "POST",
            dataType: "json",
            data: JSON.stringify({ apiuploadmethod: obj }),
            contentType: "application/json; charset=utf-8",
            success: function (data, textStatus, jqXHR) {
                if (data.Status == "0" || data.Status == "-1") {
                    toastr.success(data.Message, { timeOut: 2500 });
                }
                else if (data.Status == "1") {
                    if ($("#TransId").val() == "0") {
                        toastr.success("Api Save Successfully !!", { timeOut: 2500 });
                        setTimeout(function () {
                            location.href = window.location.href + "?TransId=" + data.Message;
                        }, 1100);
                    }
                    else {
                        toastr.success("Api Update Successfully !!", { timeOut: 2500 });
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
    catch (err) {
        $("#loading").css("display", "none");
        toastr.error(err.message, { timeOut: 2500 });
    }
};
var CreateModel = function () {
    var KeyToSymLst_Check = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
    var KeyToSymLst_uncheck = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");
    //var Locationselected = [];
    //$.each($("#Location").select2('data'), function (key, item) {
    //    Locationselected.push(item.text);
    //});

    var obj = {
        "ApiMethod": $('#ApiMethod:checked').val(),
        "WebAPIUserName": $('#ApiMethod:checked').val() == "WEBAPI" ? $("#WebAPIUserName").val() == "" && $("#WebAPIUserName").val() == undefined ? "" : $("#WebAPIUserName").val() : "",
        "WebAPIPassword": $('#ApiMethod:checked').val() == "WEBAPI" ? $("#WebAPIPassword").val() == "" && $("#WebAPIPassword").val() == undefined ? "" : $("#WebAPIPassword").val() : "",
        "FTPHost": $('#ApiMethod:checked').val() == "FTP" ? $("#FTPHost").val() == "" && $("#FTPHost").val() == undefined ? "" : $("#FTPHost").val() : "",
        "FTPUser": $('#ApiMethod:checked').val() == "FTP" ? $("#FTPUser").val() == "" && $("#FTPUser").val() == undefined ? "" : $("#FTPUser").val() : "",
        "FTPPass": $('#ApiMethod:checked').val() == "FTP" ? $("#FTPPass").val() == "" && $("#FTPPass").val() == undefined ? "" : $("#FTPPass").val() : "",
        "FTPType": $('#ApiMethod:checked').val() == "FTP" ? $("#FTPType").val() == "" && $("#FTPType").val() == undefined ? "" : $("#FTPType").val() : "",
        "FTPExportType": $('#ApiMethod:checked').val() == "FTP" ? $("#FTPExportType").val() == "" && $("#FTPExportType").val() == undefined ? "" : $("#FTPExportType").val() : "",
        "URLUserName": $('#ApiMethod:checked').val() == "URL" ? $("#URLUserName").val() == "" && $("#URLUserName").val() == undefined ? "" : $("#URLUserName").val() : "",
        "URLPassword": $('#ApiMethod:checked').val() == "URL" ? $("#URLPassword").val() == "" && $("#URLPassword").val() == undefined ? "" : $("#URLPassword").val() : "",
        "URLExportType": $('#ApiMethod:checked').val() == "URL" ? $("#URLExportType").val() == "" && $("#URLExportType").val() == undefined ? "" : $("#URLExportType").val() : "",
        "FileLocation": $('#ApiMethod:checked').val() == "Location" ? $("#txtFileLocation").val() == "" && $("#txtFileLocation").val() == undefined ? "" : $("#txtFileLocation").val() : "",
        "LocationExportType": $('#ApiMethod:checked').val() == "Location" ? $("#LocationExportType").val() == "" && $("#LocationExportType").val() == undefined ? "" : $("#LocationExportType").val() : "",
        "LocationTransType": $('#ApiMethod:checked').val() == "Location" ? $('#Location:checked').val() == "Onetime" ? "Onetime" : "Repeatevery" : "",
        "OnetimeDate": $('#ApiMethod:checked').val() == "Location" ? ($('#Location:checked').val() == "Onetime" ? ($("#txtOnetimeDate").val() == "" && $("#txtOnetimeDate").val() == undefined ? "" : $("#txtOnetimeDate").val()) : "") : "",
        "Onetime": ($('#ApiMethod:checked').val() == "Location" ? ($('#Location:checked').val() == "Onetime" ? ($("#txtOnetime").val() == "" && $("#txtOnetime").val() == undefined ? "" : $("#txtOnetime").val()) : "") : ""),
        "RepeateveryType": ($('#ApiMethod:checked').val() == "Location" ? ($('#Location:checked').val() == "Repeatevery" ? ($("#DdlRepeatevery").val() == "" && $("#DdlRepeatevery").val() == undefined ? "" : $("#DdlRepeatevery").val()) : "") : ""),
        "Repeatevery": ($('#ApiMethod:checked').val() == "Location" ? ($('#Location:checked').val() == "Repeatevery" ? ($("#txtRepeatevery").val() == "" && $("#txtRepeatevery").val() == undefined ? "" : $("#txtRepeatevery").val()) : "") : ""),
        "APIName": $("#APIName").val() == "" && $("#APIName").val() == undefined ? "" : $("#APIName").val(),
        "APIStatus": document.getElementById("APIStatus").checked,
        "iTransId": $("#TransId").val()

        //"iSupplier": _.pluck(_.filter(SupplierList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"iLocation": _.pluck(_.filter(LocationList, function (e) { return e.isActive == true }), 'Value').join(","), //Locationselected.join(','), //$("#Location").select2('val').join(','),
        //"sShape": _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"sPointer": CheckedCaratValue,
        //"sColor": _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"sClarity": _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"sCut": _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'Value').join(","),// $scope.CutObj,              
        //"sPolish": _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"sSymm": _.pluck(_.filter(SymList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"sFls": _.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"sLab": _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"dFromLength": $("#FromLength").val() == "" || $("#FromLength").val() == undefined || ($("#FromLength").val() == "0.00" && $("#FromLength").val() == "0.00") ? "" : $("#FromLength").val(),
        //"dToLength": $("#ToLength").val() == "" || $("#ToLength").val() == undefined || $("#ToLength").val() == "0.00" ? "" : $("#ToLength").val(),
        //"dFromWidth": $("#FromWidth").val() == "" || $("#FromWidth").val() == undefined || ($("#FromWidth").val() == "0.00" && $("#FromWidth").val() == "0.00") ? "0.00" : $("#FromWidth").val(),
        //"dToWidth": $("#ToWidth").val() == "" || $("#ToWidth").val() == undefined || $("#ToWidth").val() == "0.00" ? "" : $("#ToWidth").val(),
        //"dFromDepth": $("#FromDepth").val() == "" || $("#FromDepth").val() == undefined || ($("#FromDepth").val() == "0.00" && $("#FromDepth").val() == "0.00") ? "0.00" : $("#FromDepth").val(),
        //"dToDepth": $("#ToDepth").val() == "" || $("#ToDepth").val() == undefined || $("#ToDepth").val() == "0.00" ? "" : $("#ToDepth").val(),
        //"dFromDepthPer": $("#FromDepthPer").val() == "" || $("#FromDepthPer").val() == undefined || ($("#FromDepthPer").val() == "0.00" && $("#FromDepthPer").val() == "0.00") ? "0.00" : $("#FromDepthPer").val(),
        //"dToDepthPer": $("#ToDepthPer").val() == "" || $("#ToDepthPer").val() == undefined || $("#ToDepthPer").val() == "0.00" ? "" : $("#ToDepthPer").val(),
        //"dFromTablePer": $("#FromTablePer").val() == "" || $("#FromTablePer").val() == undefined || ($("#FromTablePer").val() == "0.00" && $("#FromTablePer").val() == "0.00") ? "0.00" : $("#FromTablePer").val(),
        //"dToTablePer": $("#ToTablePer").val() == "" || $("#ToTablePer").val() == undefined || $("#ToTablePer").val() == "0.00" ? "" : $("#ToTablePer").val(),
        //"dFromCrAng": $("#FromCrAng").val() == "" || $("#FromCrAng").val() == undefined || ($("#FromCrAng").val() == "0.00" && $("#FromCrAng").val() == "0.00") ? "0.00" : $("#FromCrAng").val(),
        //"dToCrAng": $("#ToCrAng").val() == "" || $("#ToCrAng").val() == undefined || $("#ToCrAng").val() == "0.00" ? "" : $("#ToCrAng").val(),
        //"dFromCrHt": $("#FromCrHt").val() == "" || $("#FromCrHt").val() == undefined || ($("#FromCrHt").val() == "0.00" && $("#FromCrHt").val() == "0.00") ? "0.00" : $("#FromCrHt").val(),
        //"dToCrHt": $("#ToCrHt").val() == "" || $("#ToCrHt").val() == undefined || $("#ToCrHt").val() == "0.00" ? "" : $("#ToCrHt").val(),
        //"dFromPavAng": $("#FromPavAng").val() == "" || $("#FromPavAng").val() == undefined || ($("#FromPavAng").val() == "0.00" && $("#FromPavAng").val() == "0.00") ? "0.00" : $("#FromPavAng").val(),
        //"dToPavAng": $("#ToPavAng").val() == "" || $("#ToPavAng").val() == undefined || $("#ToPavAng").val() == "0.00" ? "" : $("#ToPavAng").val(),
        //"dFromPavHt": $("#FromPavHt").val() == "" || $("#FromPavHt").val() == undefined || ($("#FromPavHt").val() == "0.00" && $("#FromPavHt").val() == "0.00") ? "0.00" : $("#FromPavHt").val(),
        //"dToPavHt": $("#ToPavHt").val() == "" || $("#ToPavHt").val() == undefined || $("#ToPavHt").val() == "0.00" ? "" : $("#ToPavHt").val(),
        //"dKeyToSymbol": KeyToSymLst_Check + (KeyToSymLst_Check == "" || KeyToSymLst_uncheck == "" ? "" : "-") + KeyToSymLst_uncheck,
        //"dCheckKTS": KeyToSymLst_Check,
        //"dUNCheckKTS": KeyToSymLst_uncheck,
        //"sBGM": _.pluck(_.filter(BGMList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"sCrownBlack": _.pluck(_.filter(CrownBlackList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"sTableBlack": _.pluck(_.filter(TableBlackList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"sCrownWhite": _.pluck(_.filter(CrownWhiteList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"sTableWhite": _.pluck(_.filter(TableWhiteList, function (e) { return e.isActive == true }), 'Value').join(","),
        //"Img": $('#Img:checked').val(),
        //"Vdo": $('#Vdo:checked').val(),
        //"PriceMethod": $("#PricingMethod").val() == "" && $("#PricingMethod").val() == undefined ? "" : $("#PricingMethod").val(),
        //"PricePer": $("#PricingMethod").val() == "" || $("#Percentage").val() == "" || $("#Percentage").val() == undefined ? "" : $("#Percentage").val(),

    };
    return obj;
}
function Get_ApiUploadMst(iTransId) {
    $("#loading").css("display", "block");
    $.ajax({
        url: "/Customer/Get_ApiUploadMst",
        async: false,
        type: "POST",
        data: { TransId: iTransId },
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Message == "SUCCESS") {
                $(".Div-Method").hide();
                if (data.Data[0].ApiMethod == "WEBAPI") {
                    $(".cWEBAPI").attr("checked", true);
                    $("#APIMethod" + data.Data[0].ApiMethod).show();

                    $("#WebAPIUserName").val(data.Data[0].WebAPIUserName);
                    $("#WebAPIPassword").val(data.Data[0].WebAPIPassword);
                    $("#sWEBAPI").html("API Link : " + data.Data[0].APIUrl);
                }
                else if (data.Data[0].ApiMethod == "FTP") {
                    $(".cFTP").attr("checked", true);
                    $("#APIMethod" + data.Data[0].ApiMethod).show();

                    $("#FTPType").val(data.Data[0].FTPType);
                    $("#FTPUser").val(data.Data[0].FTPUser);
                    $("#FTPExportType").val(data.Data[0].FTPExportType);
                    $("#FTPHost").val(data.Data[0].FTPHost);
                    $("#FTPPass").val(data.Data[0].FTPPass);
                    $("#sFTP").html("API Link : " + data.Data[0].APIUrl);
                }
                else if (data.Data[0].ApiMethod == "URL") {
                    $(".cURL").attr("checked", true);
                    $("#APIMethod" + data.Data[0].ApiMethod).show();

                    $("#URLUserName").val(data.Data[0].URLUserName);
                    $("#URLPassword").val(data.Data[0].URLPassword);
                    $("#URLExportType").val(data.Data[0].URLExportType);
                    $("#sURL").html("API Link : " + data.Data[0].APIUrl);
                }
                else if (data.Data[0].ApiMethod == "Location") {
                    $(".cLocation").attr("checked", true);
                    $("#APIMethod" + data.Data[0].ApiMethod).show();

                    $("#txtFileLocation").val(data.Data[0].FileLocation);
                    $("#LocationExportType").val(data.Data[0].LocationExportType);
                    if (data.Data[0].LocationTransType == "Onetime") {
                        Onetime_Click();

                        setTimeout(function () {
                            document.getElementById("txtOnetimeDate").disabled = false;
                            document.getElementById("txtOnetime").disabled = false;
                        }, 0.5);

                        $(".LOnetime").attr("checked", true);
                        OnetimeDate("U", data.Data[0].OnetimeDate);
                        $("#txtOnetime").val(data.Data[0].Onetime);
                    }
                    else if (data.Data[0].LocationTransType == "Repeatevery") {
                        Repeatevery_Click();

                        setTimeout(function () {
                            document.getElementById("txtRepeatevery").disabled = false;
                            document.getElementById("DdlRepeatevery").disabled = false;
                        }, 0.5);

                        $(".LRepeatevery").attr("checked", true);
                        $("#txtRepeatevery").val(data.Data[0].Repeatevery);
                        $("#DdlRepeatevery").val(data.Data[0].RepeateveryType);
                    }
                }
                $("#APIName").val(data.Data[0].APIName);
                $("#APIStatus").attr("checked", data.Data[0].APIStatus);


            }
            $("#loading").css("display", "none");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#loading").css("display", "none");
        }
    });
}
function Get_ApiFilter(iTransId) {
    $("#loading").css("display", "block");
    $.ajax({
        url: "/Customer/Get_ApiFilter",
        async: false,
        type: "POST",
        data: { TransId: iTransId },
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Message == "SUCCESS") {
                if (data.Data != null) {
                    $.each(data.Data, function (i, item) {
                        var html = "<tr id='tr'>";
                        html += "<th class='Row Fi-Criteria' style=''>" + (item.Sr != null ? item.Sr : "") + "</th>";
                        html += "<td style=''><span class='Fi-Criteria Supplier'>" + (item.iSupplier != null ? item.iSupplier : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Location'>" + (item.iLocation != null ? item.iLocation : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Shape'>" + (item.sShape != null ? item.sShape : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Carat'>" + (item.sPointer != null ? item.sPointer : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Color'>" + (item.sColor != null ? item.sColor : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Clarity'>" + (item.sClarity != null ? item.sClarity : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Cut'>" + (item.sCut != null ? item.sCut : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Polish'>" + (item.sPolish != null ? item.sPolish : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Sym'>" + (item.sSymm != null ? item.sSymm : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Fls'>" + (item.sFls != null ? item.sFls : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Lab'>" + (item.sLab != null ? item.sLab : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromLength'>" + (item.dFromLength != null ? item.dFromLength : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToLength'>" + (item.dToLength != null ? item.dToLength : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromWidth'>" + (item.dFromWidth != null ? item.dFromWidth : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToWidth'>" + (item.dToWidth != null ? item.dToWidth : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromDepth'>" + (item.dFromDepth != null ? item.dFromDepth : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToDepth'>" + (item.dToDepth != null ? item.dToDepth : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromDepthinPer'>" + (item.dFromDepthPer != null ? item.dFromDepthPer : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToDepthinPer'>" + (item.dToDepthPer != null ? item.dToDepthPer : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromTableinPer'>" + (item.dFromTablePer != null ? item.dFromTablePer : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToTableinPer'>" + (item.dToTablePer != null ? item.dToTablePer : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromCrAng'>" + (item.dFromCrAng != null ? item.dFromCrAng : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToCrAng'>" + (item.dToCrAng != null ? item.dToCrAng : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromCrHt'>" + (item.dFromCrHt != null ? item.dFromCrHt : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToCrHt'>" + (item.dToCrHt != null ? item.dToCrHt : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromPavAng'>" + (item.dFromPavAng != null ? item.dFromPavAng : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToPavAng'>" + (item.dToPavAng != null ? item.dToPavAng : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria FromPavHt'>" + (item.dFromPavHt != null ? item.dFromPavHt : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria ToPavHt'>" + (item.dToPavHt != null ? item.dToPavHt : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Keytosymbol'>" + (item.dKeyToSymbol != null ? item.dKeyToSymbol : "") + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria dCheckKTS'>" + (item.dCheckKTS != null ? item.dCheckKTS : "") + "</span></td>";
                        html += "<td style='display:none;'><span class='Fi-Criteria dUNCheckKTS'>" + (item.dUNCheckKTS != null ? item.dUNCheckKTS : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria BGM'>" + (item.sBGM != null ? item.sBGM : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria CrownBlack'>" + (item.sCrownBlack != null ? item.sCrownBlack : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria TableBlack'>" + (item.sTableBlack != null ? item.sTableBlack : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria CrownWhite'>" + (item.sCrownWhite != null ? item.sCrownWhite : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria TableWhite'>" + (item.sTableWhite != null ? item.sTableWhite : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Image'>" + (item.Img != null ? item.Img : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Video'>" + (item.Vdo != null ? item.Vdo : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria PriceMethod'>" + (item.PriceMethod != null ? item.PriceMethod : "") + "</span></td>";
                        html += "<td style=''><span class='Fi-Criteria Percentage'>" + (item.PricePer != null ? item.PricePer : "") + "</span></td>";
                        html += "<td style='width: 50px'>" + '<i style="cursor:pointer;" class="error RemoveCriteria"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;"/></i>' + "</td>";
                        html += "</tr>";
                        $("#mytable1 #myTableBody1").append(html);
                    });
                    $("#mytable1").show();
                }
            }
            $("#loading").css("display", "none");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#loading").css("display", "none");
        }
    });
}
function BindKeyToSymbolList() {
    $.ajax({
        url: "/Customer/Get_KeyToSymbol",
        async: false,
        type: "POST",
        data: null,
        success: function (data, textStatus, jqXHR) {
            var KeytoSymbolList = data.Data;
            $('#searchkeytosymbol').html("");
            if (KeytoSymbolList != null) {
                if (KeytoSymbolList.length > 0) {
                    $.each(KeytoSymbolList, function (i, itm) {
                        $('#searchkeytosymbol').append('<div class="col-12 pl-0 pr-0 ng-scope">'
                            + '<ul class="row m-0">'
                            + '<li class="carat-dropdown-chkbox">'
                            + '<div class="main-cust-check">'
                            + '<label class="cust-rdi-bx mn-check">'
                            + '<input type="radio" class="checkradio" id="CHK_KTS_Radio_' + (i + 1) + '" name="radio' + (i + 1) + '" onclick="GetCheck_KTS_List(\'' + itm.sSymbol + '\');">'
                            + '<span class="cust-rdi-check">'
                            + '<i class="fa fa-check"></i>'
                            + '</span>'
                            + '</label>'
                            + '<label class="cust-rdi-bx mn-time">'
                            + '<input type="radio" id="UNCHK_KTS_Radio_' + (i + 1) + '" class="checkradio" name="radio' + (i + 1) + '" onclick="GetUnCheck_KTS_List(\'' + itm.sSymbol + '\');">'
                            + '<span class="cust-rdi-check">'
                            + '<i class="fa fa-times"></i>'
                            + '</span>'
                            + '</label>'
                            + '</div>'
                            + '</li>'
                            + '<li class="col" style="text-align: left;margin-left: -15px;">'
                            + '<span>' + itm.sSymbol + '</span>'
                            + '</li>'
                            + '</ul>'
                            + '</div>')
                        //itm.ACTIVE = false;
                        //itm.INACTIVE = false;
                    });
                    $('#searchkeytosymbol').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function Key_to_symbolShow() {
    setTimeout(function () {
        if (KTS == 0) {
            $(".carat-dropdown-main").show();
            KTS = 1;
        }
        else {
            $(".carat-dropdown-main").hide();
            KTS = 0;
        }
    }, 2);
}
function resetKeytoSymbol() {
    CheckKeyToSymbolList = [];
    UnCheckKeyToSymbolList = [];
    $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
    $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    $('#searchkeytosymbol input[type="radio"]').prop('checked', false);
    KTS = 1;
    Key_to_symbolShow();
}
function GetCheck_KTS_List(item) {
    var SList = _.reject(UnCheckKeyToSymbolList, function (e) { return e.Symbol == item });
    UnCheckKeyToSymbolList = SList;

    var res = _.filter(CheckKeyToSymbolList, function (e) { return (e.Symbol == item) });
    if (res.length == 0) {
        CheckKeyToSymbolList.push({
            "NewID": CheckKeyToSymbolList.length + 1,
            "Symbol": item,
        });
        $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
        $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    }
}
function GetUnCheck_KTS_List(item) {
    var SList = _.reject(CheckKeyToSymbolList, function (e) { return e.Symbol == item });
    CheckKeyToSymbolList = SList

    var res = _.filter(UnCheckKeyToSymbolList, function (e) { return (e.Symbol == item) });
    if (res.length == 0) {
        UnCheckKeyToSymbolList.push({
            "NewID": UnCheckKeyToSymbolList.length + 1,
            "Symbol": item,
        });
        $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
        $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    }
}
var Clear = function () {
    window.location = '/Customer/CustomerAPIDetail';
}
var API_List_View = function () {
    window.location.href = '/Customer/CustomerAPIList';
}