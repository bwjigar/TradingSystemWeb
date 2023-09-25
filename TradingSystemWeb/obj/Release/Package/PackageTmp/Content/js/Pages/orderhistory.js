﻿var GridpgSize = 50;
var gridOptions = {};
var IsObj = false;
var IsObj1 = false;
var searchSummary = {};
var Scheme_Disc_Type = '';
var Scheme_Disc = "0";

if ($('#hdnisadminflg').val() == 1) {
    IsObj1 = false;
} else {
    IsObj1 = true;
}
if ($('#hdnisempflg').val() == 1 || $('#hdnisadminflg').val() == 1)
    IsObj = false;
else
    IsObj = true;

var today = new Date();
var lastWeekDate = new Date();
if (IsObj) {
    lastWeekDate = new Date(today.setDate(today.getDate() - 30));
} else {
    lastWeekDate = new Date(today.setDate(today.getDate() - 7));
}

var m_names = new Array("Jan", "Feb", "Mar",
    "Apr", "May", "Jun", "Jul", "Aug", "Sep",
    "Oct", "Nov", "Dec");
var date = new Date(lastWeekDate),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var F_date = [day, m_names[mnth - 1], date.getFullYear()].join("-");

function SetCurrentDate() {
    var m_names = new Array("Jan", "Feb", "Mar",
        "Apr", "May", "Jun", "Jul", "Aug", "Sep",
        "Oct", "Nov", "Dec");
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month]
        + "-" + curr_year);

    return FinalDate;
}
function reset() {
    $('#txtFromDate').val(F_date);
    $('#txtToDate').val(SetCurrentDate());
    $('#txtStoneId').val("");
    $('#txtCompanyName').val("");
    $("#ddlStatus").multiselect("clearSelection");

    $("#ddlStatus").multiselect('refresh');
    GetOrderFilterData();
}

function selectAllRendererDetail(params) {
    var cb = document.createElement('input');
    cb.setAttribute('type', 'checkbox');
    cb.setAttribute('id', 'checkboxAll');
    var eHeader = document.createElement('label');
    var eTitle = document.createTextNode(params.colDef.headerName);
    eHeader.appendChild(cb);
    eHeader.appendChild(eTitle);

    cb.addEventListener('change', function (e) {
        if ($(this)[0].checked) {
            if (Filtered_Data.length > 0) {
                gridOptions.api.forEachNodeAfterFilter(function (node) {
                    node.setSelected(true);
                })
            }
            else {
                gridOptions.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
        }
        else {
            params.api.deselectAll();
            onSelectionChanged();
        }

    });

    return eHeader;
}

$(document).ready(function (e) {
    on();
    GET_Scheme_Disc();
    $('#txtFromDate').val(F_date);
    $('#txtToDate').val(SetCurrentDate());
    $('#txtFromDate').daterangepicker({
        singleDatePicker: true,
        startDate: F_date,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    });
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        startDate: moment(),
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    });
    $('#ddlStatus').multiselect({
        includeSelectAllOption: true,
        numberDisplayed: 1
    });
    //GetOrderFilterData();
    GetOrderData();
    contentHeight();
    $('#txtStoneId,#txtCompanyName').on('keypress', function (e) {
        if (e.which == 13) {
            GetOrderFilterData();
        }
    });
    //$(function () {
    //    //Default Style
    //    $('h1').tooltip({
    //        speed: 'fast',
    //        animation: 'transX'
    //    });

    //    $('h2').tooltip({
    //        animation: 'transX',
    //        speed: 'slow',
    //        background: 'linear-gradient(#772300,orange)',
    //        borderBottom: '2px solid black',
    //        borderRight: '2px solid black',
    //        boxShadow: '2px 2px 7px #555'
    //    });

    //    $('h3').tooltip({
    //        backgroundColor: '#78a',
    //        padding: '15px',
    //        transition: 'all 0.1s ease',
    //    });

    //    $('h4').tooltip({
    //        backgroundColor: '#063758',
    //        padding: '15px',
    //        transition: 'all 0.1s ease',
    //    });
    //    $('.tooltip_1').tooltip({
    //        backgroundColor: 'pink'
    //    });
    //});
});
 
var header_template = '<span class="text-danger" style="height:30px;">Arrival in HK </span>';

var gridDiv = document.querySelector('#myGrid');
var columnDefs = [
    {
        headerName: "", field: "",
        headerCheckboxSelection: true,
        checkboxSelection: true, width: 28,
        suppressSorting: true,
        suppressMenu: true,
        headerCheckboxSelectionFilteredOnly: true,
        headerCellRenderer: selectAllRendererDetail,
        suppressMovable: false
    },
    { headerName: "iUserid", field: "iUserid", hide: true },
    { headerName: "FullOrderDate", field: "FullOrderDate", hide: true },
    {
        headerName: $("#hdn_Order_Date").val(), field: "OrderDate", tooltip: function (params) { return (params.value); }, width: 80, sortable: true
    },
    {
        headerName: $("#hdn_Order_No").val(), field: "iOrderid", tooltip: function (params) { return (params.value); }, width: 60, sortable: true
    },
    {
        headerName: $("#hdn_Stock_Id_DNA").val(), field: "sRefNo", width: 95, tooltip: function (params) { return (params.value); }, cellRenderer: function (params) {
            if (params.data == undefined) {
                return '';
            }
            //return '<div class="stock-font"><a target="_blank" href="http://cdn1.brainwaves.co.in/DNA/StoneDetail?StoneNo=' + params.data.sRefNo + '">' + params.data.sRefNo + '</a></div>';
            return '<div class="stock-font"><a target="_blank" href="/DNA/StoneDetail?StoneNo=' + params.data.sRefNo + '">' + params.data.sRefNo + '</a></div>';

        }
    },
    {
        headerName: $("#hdn_Status").val(), field: "sStoneStatus", tooltip: function (params) { return (params.value); }, width: 150, filter: 'agSetColumnFilter'
        , filterParams: {
            values: [],
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        },
        cellRenderer: function (params) {
            if (params.value != null && params.value != '') {
                return capitalizeTheFirstLetterOfEachWord(params.value);
            }
        },
        cellClass: function (params) {
            if (params.data != undefined) {
                if (params.data.sStoneStatus == 'AVAILABLE OFFER') {
                    return 'offercls';
                }
                if (params.data.Location == 'Upcoming') {
                    return 'upcomingcls';
                }
            }
        }, sortable: true
    },
    {
        headerName: $("#hdn_Arrival_in_HK").val(),
        field: "Exp_Del_Date",
        tooltip: function (params) { return (params.value); },
        width: 30,
        sortable: false,
        //headerTooltip: 'Estimate time of Arrival in HK',
        //tooltipComponent: 'customTooltip',
        //headerComponentParams: {
        //    template:
        //        '<div class="ag-cell-label-container" role="presentation">' +
        //        '<span ref="eMenu" class="ag-header-icon ag-header-cell-menu-button"></span>' +
        //        '<div ref="eLabel" class="aglabel_three" role="presentation">' +
        //        '<span ref="eSortOrder" class="ag-header-icon ag-sort-order" ></span>' +
        //        '<span ref="eSortAsc" class="ag-header-icon ag-sort-ascending-icon" ></span>' +
        //        '<span ref="eSortDesc" class="ag-header-icon ag-sort-descending-icon" ></span>' +
        //        '<span ref="eSortNone" class="ag-header-icon ag-sort-none-icon" ></span>' +
        //        'Arrival in HK' +
        //        '<div class="placeOrderDiv">Place Order</div>'+
        //        '<small class="aglabel_newlabel tooltip_1 effect-one" data-placement="bottom">NEW</small>' +
        //        '<span ref="eFilter" class="ag-header-icon ag-filter-icon"></span>' +
        //        '</div>' +
        //        '</div>'
        //},
    },
    {
        headerName: $("#hdn_Ready_For_Pick_up").val(),
        field: "PickUp_Status",
        tooltip: function (params) { return (params.value); },
        width: 30,
        sortable: false,
        //headerTooltip: 'Ready For Pick up',
        //tooltipComponent: 'customTooltip',
        //headerComponentParams: {
        //    template:
        //        '<div class="ag-cell-label-container" role="presentation">' +
        //        '<span ref="eMenu" class="ag-header-icon ag-header-cell-menu-button"></span>' +
        //        '<div ref="eLabel" class="aglabel_three" role="presentation">' +
        //        '<span ref="eSortOrder" class="ag-header-icon ag-sort-order" ></span>' +
        //        '<span ref="eSortAsc" class="ag-header-icon ag-sort-ascending-icon" ></span>' +
        //        '<span ref="eSortDesc" class="ag-header-icon ag-sort-descending-icon" ></span>' +
        //        '<span ref="eSortNone" class="ag-header-icon ag-sort-none-icon" ></span>' +
        //        'Ready For Pick up' +
        //        '<small class="aglabel_newlabel tooltip_1">NEW</small>' +
        //        '<span ref="eFilter" class="ag-header-icon ag-filter-icon"></span>' +
        //        '</div>' +
        //        '</div>'
        //},
    },
    {
        headerName: $("#hdn_Collected_Date").val(),
        field: "Delivery_Date",
        tooltip: function (params) { return (params.value); },
        width: 30,
        sortable: false,
        //headerTooltip: 'Stones Collected Date',
        //tooltipComponent: 'customTooltip',
        //headerComponentParams: {
        //    template:
        //        '<div class="ag-cell-label-container" role="presentation">' +
        //        '<span ref="eMenu" class="ag-header-icon ag-header-cell-menu-button"></span>' +
        //        '<div ref="eLabel" class="aglabel_three" role="presentation">' +
        //        '<span ref="eSortOrder" class="ag-header-icon ag-sort-order" ></span>' +
        //        '<span ref="eSortAsc" class="ag-header-icon ag-sort-ascending-icon" ></span>' +
        //        '<span ref="eSortDesc" class="ag-header-icon ag-sort-descending-icon" ></span>' +
        //        '<span ref="eSortNone" class="ag-header-icon ag-sort-none-icon" ></span>' +
        //        'Collected Date' +
        //        '<small class="aglabel_newlabel tooltip_1">NEW</small>' +
        //        '<span ref="eFilter" class="ag-header-icon ag-filter-icon"></span>' +
        //        '</div>' +
        //        '</div>'
        //},
    },
    {
        headerName: $("#hdn_Location").val(), field: "Location", tooltip: function (params) { return (params.value); }, width: 90, filter: 'agSetColumnFilter'
        , filterParams: {
            values: [],
            resetButton: true,
            applyButton: true,
            comparator: function (a, b) {
                return 0;
            }
        }, sortable: true
    },
    {
        headerName: $("#hdn_Username").val(), field: "UserName", hide: IsObj, tooltip: function (params) { return (params.value); }, width: 150, filter: 'agSetColumnFilter'
        , filterParams: {
            values: [],
            resetButton: true,
            applyButton: true
        }, sortable: true
    },
    {
        headerName: $("#hdn_CompanyName").val(), field: "CompanyName", hide: IsObj, tooltip: function (params) { return (params.value); }, width: 150, filter: 'agSetColumnFilter'
        , filterParams: {
            values: [],
            resetButton: true,
            applyButton: true
        }, sortable: true
    },
    {
        headerName: $("#hdn_CustomerName").val(), field: "CustomerName", hide: IsObj, tooltip: function (params) { return (params.value); }, width: 180, filter: 'agSetColumnFilter'
        , filterParams: {
            values: [],
            resetButton: true,
            applyButton: true
        }, sortable: true
    },
    {
        headerName: $("#hdn_Assist_1").val(), field: "AssistBy1", hide: IsObj1, tooltip: function (params) { return (params.value); }, width: 180, filter: 'agSetColumnFilter'
        , filterParams: {
            values: [],
            resetButton: true,
            applyButton: true
        }
    },


    { headerName: $("#hdn_Shape").val(), field: "sShape", tooltip: function (params) { return (params.value); }, width: 60, sortable: true },
    { headerName: $("#hdn_Pointer").val(), field: "sPointer", tooltip: function (params) { return (params.value); }, width: 60, sortable: true },
    { headerName: $("#hdn_Lab").val(), field: "sLab", tooltip: function (params) { return (params.value); }, width: 40, cellRenderer: LotValueGetter, sortable: false },
    { headerName: $("#hdn_Certi_No").val(), field: "sCertiNo", tooltip: function (params) { return (params.value); }, width: 80, sortable: true },
    { headerName: $("#hdn_BGM").val(), field: "BGM", tooltip: function (params) { return (params.value); }, width: 90, sortable: true },
    { headerName: $("#hdn_Color").val(), field: "sColor", tooltip: function (params) { return (params.value); }, width: 50, sortable: true },
    { headerName: $("#hdn_Clarity").val(), field: "sClarity", tooltip: function (params) { return (params.value); }, width: 50, sortable: true },
    { headerName: $("#hdn_CTS").val(), field: "dCts", tooltip: function (params) { return (params.value); }, width: 75, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },
    { headerName: $("#hdn_Rap_Price_Doller").val(), field: "dRepPrice", tooltip: function (params) { return formatNumber(params.value); }, width: 85, cellRenderer: function (params) { return formatNumber(params.value); }, sortable: true },
    { headerName: $("#hdn_Rap_Amt_Doller").val(), field: "dRapAmount", tooltip: function (params) { return formatNumber(params.value); }, width: 85, cellRenderer: function (params) { return formatNumber(params.value); }, sortable: true },
    { headerName: $("#hdn_Offer_Disc_Per").val(), field: "dDisc", tooltip: function (params) { return parseFloat(params.value).toFixed(2); }, width: 90, cellStyle: { color: 'red', 'font-weight': 'bold', 'background-color': '#4abbce73' }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },
    { headerName: $("#hdn_Offer_Value_Dollar").val(), field: "dNetPrice", tooltip: function (params) { return formatNumber(params.value); }, width: 105, cellStyle: { color: 'red', 'font-weight': 'bold', 'background-color': '#4abbce73' }, cellRenderer: function (params) { return formatNumber(params.value); }, sortable: true },
    { headerName: $("#hdn_Web_Disc_Dollar").val(), field: "Web_Benefit", tooltip: function (params) { return formatNumber(params.value); }, width: 80, cellStyle: { color: 'blue', 'font-weight': 'bold', 'background-color':'#ddeedf' },cellRenderer: function (params) { return formatNumber(params.value); }, sortable: true },
    { headerName: $("#hdn_Final_Value").val(), field: "Net_Value", tooltip: function (params) { return formatNumber(params.value); }, width: 90, cellStyle: { color: 'blue', 'font-weight': 'bold', 'background-color': '#fdfdc1'}, cellRenderer: function (params) { return formatNumber(params.value); }, sortable: true },
    { headerName: $("#hdn_Final_Disc_Per").val(), field: "Final_Disc", tooltip: function (params) { return formatNumber(params.value); }, width: 90, cellStyle: { color: 'blue', 'font-weight': 'bold', 'background-color': '#fdfdc1'}, cellRenderer: function (params) { return formatNumber(params.value); }, sortable: true },
    //{ headerName: $("#hdn_Price_Cts").val(), field: "PRICE_PER_CTS", tooltip: function (params) { return formatNumber(params.value); }, width: 80, cellRenderer: function (params) { return formatNumber(params.value); }, sortable: true },
    {
        headerName: $("#hdn_Cut").val(), field: "sCut", tooltip: function (params) { return (params.value); }, width: 50,
        cellRenderer: function (params) {
            if (params.value == undefined) {
                return '';
            }
            else {
                return (params.value == 'FR' ? 'F' : params.value);
            }
        },
        cellStyle: function (params) {
            if (params.data) {
                if (params.value == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }
    },
    {
        headerName: $("#hdn_Polish").val(), field: "sPolish", width: 50, tooltip: function (params) { return (params.value); },
        cellStyle: function (params) {
            if (params.data) {
                if (params.data.sCut == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }, sortable: true
    },
    {
        headerName: $("#hdn_Symm").val(), field: "sSymm", width: 50, tooltip: function (params) { return (params.value); },
        cellStyle: function (params) {
            if (params.data) {
                if (params.data.sCut == '3EX')
                    return { 'font-weight': 'bold' };
            }
        }, sortable: true
    },

    { headerName: $("#hdn_Fls").val(), field: "sFls", tooltip: function (params) { return (params.value); }, width: 50, sortable: true },

    { headerName: $("#hdn_Length").val(), field: "dLength", tooltip: function (params) { return (params.value); }, width: 65, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },
    { headerName: $("#hdn_Width").val(), field: "dWidth", tooltip: function (params) { return (params.value); }, width: 50, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },

    { headerName: $("#hdn_Depth").val(), field: "dDepth", tooltip: function (params) { return (params.value); }, width: 50, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },
    { headerName: $("#hdn_Depth_Per").val(), field: "dDepthPer", tooltip: function (params) { return (params.value); }, width: 70, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },
    { headerName: $("#hdn_Table_Per").val(), field: "dTablePer", tooltip: function (params) { return (params.value); }, width: 70, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2); }, sortable: true },
    { headerName: $("#hdn_Key_to_symbol").val(), field: "sSymbol", tooltip: function (params) { return (params.value); }, width: 350 },
    { headerName: $("#hdn_Culet").val(), field: "sCulet", tooltip: function (params) { return (params.value); }, width: 50 },
    //{ headerName: "Luster /Milky", field: "Luster", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: $("#hdn_Table_Black").val(), field: "sTableNatts", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: $("#hdn_Crown_Natts").val(), field: "sCrownNatts", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: $("#hdn_Table_White").val(), field: "sInclusion", tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: $("#hdn_Crown_White").val(), field: "sCrownInclusion", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: $("#hdn_Crown_Angle").val(), tooltip: function (params) { return formatNumber(params.value); }, field: "dCrAng", width: 60, cellRenderer: function (params) { return formatNumber(params.value); }, },
    { headerName: $("#hdn_CR_HT").val(), tooltip: function (params) { return formatNumber(params.value); }, field: "dCrHt", width: 50, cellRenderer: function (params) { return formatNumber(params.value); }, },
    { headerName: $("#hdn_Pav_Ang").val(), tooltip: function (params) { return formatNumber(params.value); }, field: "dPavAng", width: 60, cellRenderer: function (params) { return formatNumber(params.value); }, },
    { headerName: $("#hdn_Pav_HT").val(), tooltip: function (params) { return formatNumber(params.value); }, field: "dPavHt", width: 60, cellRenderer: function (params) { return formatNumber(params.value); }, },
    { headerName: ($("#hdn_girdle").val() + "(%)"), field: "girdle_per", tooltip: function (params) { return formatNumber(params.value); }, width: 88, cellRenderer: function (params) { return formatNumber(params.value); }, },
    { headerName: $("#hdn_Girdle_Type").val(), tooltip: function (params) { return (params.value); }, field: "sGirdleType", width: 90 },
    { headerName: $("#hdn_Laser_in_SC").val(), field: "sInscription", width: 90, tooltip: function (params) { return (params.value); }, },
    {
        headerName: $("#hdn_View_Image").val(), field: "Imag_Video", width: 80, tooltip: function (params) { return (params.value); }, cellRenderer: ImageValueGetter, suppressSorting: true,
        suppressMenu: true,
    },
];
//function CustomTooltip() { }

//CustomTooltip.prototype.init = function (params) {
//    var eGui = (this.eGui = document.createElement('div')),
//        isHeader = params.rowIndex === undefined,
//        isGroupedHeader = isHeader && !!params.colDef.children,
//        str,
//        valueToDisplay;

//    eGui.classList.add('custom-tooltip');

//    if (isHeader) {
//        str = '<p>' + params.value + '</p>';
//        if (isGroupedHeader) {
//            str += '<hr>';
//            params.colDef.children.forEach(function (header, idx) {
//                str += '<p>Child ' + (idx + 1) + ' - ' + header.headerName + '</p>';
//            });
//        }
//        eGui.innerHTML = str;
//    } else {
//        valueToDisplay = params.value.value ? params.value.value : '- Missing -';

//        eGui.innerHTML =
//            '<p><span class"name">' +
//            valueToDisplay +
//            '</span></p>';
//    }
//};

//CustomTooltip.prototype.getGui = function () {
//    return this.eGui;
//};
function capitalizeTheFirstLetterOfEachWord(words) {
    var separateWord = words.toLowerCase().split(' ');
    for (var i = 0; i < separateWord.length; i++) {
        separateWord[i] = separateWord[i].charAt(0).toUpperCase() + separateWord[i].substring(1);
    }
    return separateWord.join(' ');
}
function OrderDetailPage(params) {
    return '<a href=""style="text-decoration: underline; color: #003d66;" ng-click="GoToOrderDetail(data)">Detail</a>';
}
function ImageValueGetter(params) {
    if (params.data != undefined) {
        return params.data.ImagesLink;
    }
    else {
        return '';
    }
}
var ImagesURL = [];
function LotValueGetter(params) {
    $('.offercls').parent().addClass('offerrow');
    $('.upcomingcls').parent().addClass('upcomingrow');

    if (params.data.sCertiNo != "") {
        if (params.data != undefined) {
            if (params.value == "GIA") {
                return '<a href="http://www.gia.edu/cs/Satellite?pagename=GST%2FDispatcher&childpagename=GIA%2FPage%2FReportCheck&c=Page&cid=1355954554547&reportno=' + params.data.sCertiNo + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
            }
            else if (params.value == "HRD") {
                return '<a href="https://my.hrdantwerp.com/?id=34&record_number=' + params.data.sCertiNo + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
            }
            else if (params.value == "IGI") {
                return '<a href="https://www.igi.org/reports/verify-your-report?r=' + params.data.sCertiNo + '" target="_blank" style="text-decoration: underline; color :blue;">' + params.value + '</a>';
            }
            else {
                return '';
            }
        }
        else {
            return '';
        }
    }
    else {
        return '<span style="color :blue;">' + params.value + '</span>';
    }
}
function GoToOrderDetail(row) {
    window.open('/MyOrder/OrderDetail?OrderId=' + row.order_id);

}
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
var showEntryVar = "";

var showEntryHtml = '<div class="show_entry show_entry1"><label>'
    + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize" class="" >'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="200">200</option>'
    + '<option value="500">500</option>'
    + '</select> entries'
    + '</label>'
    + '</div>';




new WOW().init();


/*------------ order-history-dropdown-select ------------*/

function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    //gridOptions.api.paginationSetPageSize(Number(value));
    GridpgSize = Number(value);
    //GetOrderFilterData();
    GetOrderData();
}

function contentHeight() {
    var winH = $(window).height(),
        tabsmarkerHei = $(".order-title").height(),
        navbar = $(".navbar").height(),
        resultHei = $(".order-history-data").height(),
        contentHei = winH - (navbar + tabsmarkerHei + resultHei + 70);
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function () {
    //$('#ddlStatus').multiselect({
    //    includeSelectAllOption: true,
    //    numberDisplayed: 1
    //});
    ////GetOrderFilterData();
    //GetOrderData();
    //contentHeight();
    //$('#txtStoneId,#txtCompanyName').on('keypress', function (e) {
    //    if (e.which == 13) {
    //        GetOrderFilterData();
    //    }
    //});
});
$(window).resize(function () {
    contentHeight();
});
$('input[name="dates"]').daterangepicker();

/*------------ order-history-dropdown-li-select ------------*/

function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function GetOrderFilterData() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    
    $.ajax({
        url: "/Order/GetOrderHistoryFilterData",
        async: false,
        type: "POST",
        data: {
            FromDate: $('#txtFromDate').val(), ToDate: $('#txtToDate').val(), CommonName: $('#txtCompanyName').val(),
            CompanyName: CompanyName, StoneNoList: $('#txtStoneId').val(), Status: $('#ddlStatus').val().join(","),
            CustomerName: CustomerName, UserName: UserName,
            Location: Location, OrderBy: orderBy, PgSize: GridpgSize
        },
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (data.Data && data.Data.length > 0) {
                if (data.Data[0].Companies != undefined) {
                    if (_.find(columnDefs, function (num) { return num.field == 'CompanyName'; })) {
                        _.findWhere(columnDefs, { field: 'CompanyName' }).filterParams.values = data.Data[0].Companies;
                    }
                }
                if (data.Data[0].Customers != undefined) {
                    if (_.find(columnDefs, function (num) { return num.field == 'CustomerName'; })) {
                        _.findWhere(columnDefs, { field: 'CustomerName' }).filterParams.values = data.Data[0].Customers;
                    }
                }
                if (data.Data[0].Users != undefined) {
                    if (_.find(columnDefs, function (num) { return num.field == 'UserName'; })) {
                        _.findWhere(columnDefs, { field: 'UserName' }).filterParams.values = data.Data[0].Users;
                    }
                }
                if (data.Data[0].Status != undefined) {
                    if (_.find(columnDefs, function (num) { return num.field == 'sStoneStatus'; })) {
                        _.findWhere(columnDefs, { field: 'sStoneStatus' }).filterParams.values = data.Data[0].Status;
                    }
                }
                if (data.Data[0].Locations != undefined) {
                    if (_.find(columnDefs, function (num) { return num.field == 'Location'; })) {
                        _.findWhere(columnDefs, { field: 'Location' }).filterParams.values = data.Data[0].Locations;
                    }
                }

            } else {
                if (_.find(columnDefs, function (num) { return num.field == 'CompanyName'; })) {
                    _.findWhere(columnDefs, { field: 'CompanyName' }).filterParams.values = null;
                }
                if (_.find(columnDefs, function (num) { return num.field == 'UserName'; })) {
                    _.findWhere(columnDefs, { field: 'UserName' }).filterParams.values = null;
                }
                if (_.find(columnDefs, function (num) { return num.field == 'sStoneStatus'; })) {
                    _.findWhere(columnDefs, { field: 'sStoneStatus' }).filterParams.values = null;
                }
                if (_.find(columnDefs, function (num) { return num.field == 'Location'; })) {
                    _.findWhere(columnDefs, { field: 'Location' }).filterParams.values = null;
                }
            }
            GetOrderData();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function GetOrderData() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }
    gridOptions = {
        masterDetail: true,
        detailCellRenderer: 'myDetailCellRenderer',
        detailRowHeight: 70,
        groupDefaultExpanded: 1,
        components: {
            //deltaIndicator: deltaIndicator,
            //statusIndicator: statusIndicator,
            ImageValueGetter: ImageValueGetter,
            LotValueGetter: LotValueGetter
        },
        defaultColDef: {
            enableSorting: true,
            sortable: true,
            resizable: true
        },
        pagination: true,
        icons: {
            groupExpanded:
                '<i class="fa fa-minus-circle"/>',
            groupContracted:
                '<i class="fa fa-plus-circle"/>'
        },
        rowSelection: 'multiple',
        overlayLoadingTemplate: '<span class="ag-overlay-loading-center">NO DATA TO SHOW..</span>',
        suppressRowClickSelection: true,
        columnDefs: columnDefs,
        onSelectionChanged: onSelectionChanged, onBodyScroll: onBodyScroll,
        rowModelType: 'serverSide',
        cacheBlockSize: GridpgSize, // you can have your custom page size
        paginationPageSize: GridpgSize, //pagesize
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        },
        //components: {
        //    customTooltip: CustomTooltip,
        //}
    };

    new agGrid.Grid(gridDiv, gridOptions);

    $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
        if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(false);
            });
        } else {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(true);
            });
        }
        onSelectionChanged();
    });
    gridOptions.api.setServerSideDatasource(datasource1);
    setTimeout(function () {
        var allColumnIds = [];
        gridOptions.columnApi.getAllColumns().forEach(function (column) {
            allColumnIds.push(column.colId);
        });

        gridOptions.columnApi.autoSizeColumns(allColumnIds, false);
    }, 1000);

    var a = $('.ag-header-select-all')[0];
    $(a).removeClass('ag-hidden');
}
function onBodyScroll(params) {

    $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');

    $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
        if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(false);
            });
        } else {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(true);
            });
        }
        onSelectionChanged();
    });
}
function onSelectionChanged() {
    var TOT_CTS = 0;
    var AVG_SALES_DISC_PER = 0;
    var AVG_PRICE_PER_CTS = 0;
    var TOT_NET_AMOUNT = 0;
    var TOT_PCS = 0;
    var TOT_RAP_AMOUNT = 0;
    var dDisc = 0, dRepPrice = 0, DCTS = 0, dNetPrice = 0, Web_Benefit = 0, Final_Disc = 0, Net_Value = 0;
    
    if (gridOptions.api.getSelectedRows().length > 0) {
        dDisc = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'sales_disc_per'), function (memo, num) { return memo + num; }, 0);
        TOT_CTS = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'dCts'), function (memo, num) { return memo + num; }, 0);
        TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'dNetPrice'), function (memo, num) { return memo + num; }, 0);
        //TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'Net_Value'), function (memo, num) { return memo + num; }, 0);
        TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'dRapAmount'), function (memo, num) { return memo + num; }, 0);
        //AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
        AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
        AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
        TOT_PCS = gridOptions.api.getSelectedRows().length;
        //TOT_NET_AMOUNT = (TOT_NET_AMOUNT).toFixed(2);
        //TOT_RAP_AMOUNT = (TOT_RAP_AMOUNT).toFixed(2);
        
        if (Scheme_Disc_Type == "Discount") {
            Net_Value = 0;
            Final_Disc = 0;
            Web_Benefit = 0;
        }
        else if (Scheme_Disc_Type == "Value") {
            Net_Value = (parseFloat(TOT_NET_AMOUNT) + (parseFloat(TOT_NET_AMOUNT) * parseFloat(Scheme_Disc) / 100)).toFixed(2);
            Final_Disc = (((1 - parseFloat(Net_Value) / parseFloat(TOT_RAP_AMOUNT)) * 100) * -1).toFixed(2);
            Web_Benefit = (parseFloat(TOT_NET_AMOUNT) - parseFloat(Net_Value)).toFixed(2);
        }
        else {
            Net_Value = parseFloat(TOT_NET_AMOUNT);
            Final_Disc = parseFloat(AVG_SALES_DISC_PER);
            Web_Benefit = 0;
        }
        $('#tab1_WebDisc_t').show();
        $('#tab1_FinalValue_t').show();
        $('#tab1_FinalDisc_t').show();
    } else {
        TOT_CTS = searchSummary.TOT_CTS;
        AVG_SALES_DISC_PER = searchSummary.AVG_SALES_DISC_PER;
        AVG_PRICE_PER_CTS = searchSummary.AVG_PRICE_PER_CTS;
        TOT_NET_AMOUNT = searchSummary.TOT_NET_AMOUNT;
        TOT_PCS = searchSummary.TOT_PCS;
        $('#tab1_WebDisc_t').hide();
        $('#tab1_FinalValue_t').hide();
        $('#tab1_FinalDisc_t').hide();
    }

    //$('#tab1cts').html($("#hdn_Cts").val() + ' : ' + formatNumber(TOT_CTS) + '');
    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(AVG_SALES_DISC_PER) + '');
    ////$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(AVG_PRICE_PER_CTS) + '');
    //$('#tab1totAmt').html($("#hdn_Final_Value").val() + ' : ' + formatNumber(TOT_NET_AMOUNT) + '');
    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + TOT_PCS + '');
    $('#tab1TCount').show();
    $('#tab1pcs').html(TOT_PCS);
    $('#tab1cts').html(formatNumber(TOT_CTS));
    $('#tab1disc').html(formatNumber(AVG_SALES_DISC_PER));
    //$('#tab1ppcts').html(formatNumber(AVG_PRICE_PER_CTS));
    $('#tab1totAmt').html(formatNumber(TOT_NET_AMOUNT));

    $('#tab1Web_Disc').html(formatNumber(Web_Benefit));
    $('#tab1Net_Value').html(formatNumber(Net_Value));
    $('#tab1Final_Disc').html(formatNumber(Final_Disc));
}
function round(value, exp) {
    if (typeof exp === 'undefined' || +exp === 0)
        return Math.round(value);

    value = +value;
    exp = +exp;

    if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0))
        return NaN;

    // Shift
    value = value.toString().split('e');
    value = Math.round(+(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp)));

    // Shift back
    value = value.toString().split('e');
    return +(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp));
}
var UserName = "";
var CustomerName = "";
var CompanyName = "";
var Location = "";
var orderBy = "";
const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        $('.loading-overlay-image-container').show();
        $('.loading-overlay').show();

        var status = '';
        var CommonName = $('#txtCompanyName').val();

        if (params.request.filterModel.CustomerName) {
            CustomerName = params.request.filterModel.CustomerName.values.join(",");
        }
        else {
            CustomerName = '';
        }

        if (params.request.filterModel.UserName) {
            UserName = params.request.filterModel.UserName.values.join(",");
        }
        else {
            UserName = '';
        }

        if (params.request.filterModel.CompanyName) {
            CompanyName = params.request.filterModel.CompanyName.values.join(",");
        }
        else {
            CompanyName = '';
        }

        if (params.request.filterModel.Location) {
            Location = params.request.filterModel.Location.values.join(",");
        }
        else {
            Location = '';
        }

        if (params.request.filterModel.sStoneStatus) {
            status = params.request.filterModel.sStoneStatus.values.join(",");
        }
        else {
            status = $('#ddlStatus').val().join(",");
        }

        if (params.request.sortModel.length > 0) {
            orderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort
        }
        else {
            orderBy = '';
        }
        $.ajax({
            url: "/Order/GetOrderHistoryData",
            async: false,
            type: "POST",
            data: { FromDate: $('#txtFromDate').val(), ToDate: $('#txtToDate').val(), CommonName: CommonName, CompanyName: CompanyName, StoneNoList: $('#txtStoneId').val(), Status: status, PageNo: PageNo, CustomerName: CustomerName, UserName: UserName, Location: Location, OrderBy: orderBy, PgSize: GridpgSize },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data && data.Data.length > 0) {
                    searchSummary = data.Data[0].DataSummary;
                    params.successCallback(data.Data[0].DataList, searchSummary.TOT_PCS);

                    //$('#tab1cts').html($("#hdn_Cts").val() + ' : ' + formatNumber(searchSummary.TOT_CTS) + '');
                    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(searchSummary.AVG_SALES_DISC_PER) + '');
                    ////$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(searchSummary.AVG_PRICE_PER_CTS) + '');
                    //$('#tab1totAmt').html($("#hdn_Final_Value").val() + ' : ' + formatNumber(searchSummary.TOT_NET_AMOUNT) + '');
                    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + searchSummary.TOT_PCS + '');
                    $('#tab1TCount').show();
                    $('#tab1pcs').html(searchSummary.TOT_PCS);
                    $('#tab1cts').html(formatNumber(searchSummary.TOT_CTS));
                    $('#tab1disc').html(formatNumber(searchSummary.AVG_SALES_DISC_PER));
                    //$('#tab1ppcts').html(formatNumber(searchSummary.AVG_PRICE_PER_CTS));
                    $('#tab1totAmt').html(formatNumber(searchSummary.TOT_NET_AMOUNT));
                    $('#tab1_WebDisc_t').hide();
                    $('#tab1_FinalValue_t').hide();
                    $('#tab1_FinalDisc_t').hide();
                } else {
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
                    //$('#tab1cts').html($("#hdn_Cts").val() + ' : 0');
                    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : 0');
                    ////$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : 0');
                    //$('#tab1totAmt').html($("#hdn_Final_Value").val() + ' : 0');
                    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : 0');
                    $('#tab1TCount').hide();
                    $('#tab1pcs').html('0');
                    $('#tab1cts').html('0');
                    $('#tab1disc').html('0');
                    //$('#tab1ppcts').html('0');
                    $('#tab1totAmt').html('0');
                    $('#tab1_WebDisc_t').hide();
                    $('#tab1_FinalValue_t').hide();
                    $('#tab1_FinalDisc_t').hide();
                }
                if ($('#myGrid .ag-paging-panel').length > 0) {

                    $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
                    $('#ddlPagesize').val(GridpgSize);
                    clearInterval(showEntryVar);
                }
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                $('.loading-overlay-image-container').hide();
                $('.loading-overlay').hide();
            }
        });
    }
};
function DownloadExcel() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();

    var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'sRefNo').join(",");

    var iUserid = _.pluck(gridOptions.api.getSelectedRows(), 'iUserId'); //.join(",");
    var FullOrderDate = _.pluck(gridOptions.api.getSelectedRows(), 'FullOrderDate'); //.join(",")
    var iUserid_FullOrderDate = ([iUserid, FullOrderDate].reduce((a, b) => a.map((v, i) => v + b[i]))).join(",");

    if (stoneno == "") {
        stoneno = $('#txtStoneId').val();
    }

    var status = $('#ddlStatus').val().join(",");

    $.ajax({
        url: "/Order/DownloadOrderHistory",
        type: "POST",
        data: {
            iUserid_FullOrderDate: iUserid_FullOrderDate,
            FromDate: $('#txtFromDate').val(),
            ToDate: $('#txtToDate').val(),
            CommonName: $('#txtCompanyName').val(),
            CompanyName: CompanyName,
            StoneNoList: stoneno,
            Status: status,
            UserName: UserName,
            CustomerName: CustomerName,
            Location: Location,
            OrderBy: orderBy,
            PageNo: 0,
            isAdmin: !IsObj,
            isEmp: !IsObj1,
            PgSize: GridpgSize,
            FormName: "Order History",
            ActivityType: "Excel Export"
        },
        success: function (data, textStatus, jqXHR) {
            if (data.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            else if (data.indexOf('No data') > -1) {
                toastr.error(data);
            }
            else {
                location.href = data;
            }
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function GET_Scheme_Disc() {
    $.ajax({
        url: "/SearchStock/GET_Scheme_Disc",
        type: "POST",
        success: function (data, textStatus, jqXHR) {
            Scheme_Disc_Type = '';
            Scheme_Disc = "0";
            if (data.Data != null) {
                if (data.Data.length != 0) {
                    if (data.Data[0].Discount != null) {
                        Scheme_Disc_Type = 'Discount';
                        Scheme_Disc = data.Data[0].Discount;
                    }
                    if (data.Data[0].Value != null) {
                        Scheme_Disc_Type = 'Value';
                        Scheme_Disc = data.Data[0].Value;
                    }
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function on() {
    document.getElementById("overlay").style.display = "block";
}
function off() {
    document.getElementById("overlay").style.display = "none";
}