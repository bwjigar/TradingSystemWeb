﻿var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var today = new Date();
var lastWeekDate = new Date(today.setDate(today.getDate() - 7));
var date = new Date(lastWeekDate),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var F_date = [day, m_names[mnth - 1], date.getFullYear()].join("-");

var GridpgSize = 50;
var gridOptions = {};
var showEntryHtml = '<div class="show_entry show_entry1"><label>'
    + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize" class="" >'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="200">200</option>'
    + '</select> entries'
    + '</label>'
    + '</div>';

function SetCurrentDate() {
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}
function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    GridpgSize = Number(value);
    GetDataList();
}

function CommonNameKeypress(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 13) {
        GetDataList();
    }
}

var columnDefs = [
    { headerName: "Sr.", field: "SrNo", sortable: false, tooltip: function (params) { return (params.value); }, width: 30 },
    { headerName: "Company Name", field: "CompanyName", sortable: true, tooltip: function (params) { return (params.value); }, width: 130 },
    { headerName: "Assist By", field: "AssistBy", sortable: true, tooltip: function (params) { return (params.value); }, width: 130 },
    { headerName: "Username", field: "Username", sortable: false, tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "PwdLenth", field: "PwdLenth", sortable: false, hide: true, tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Password", field: "Password", sortable: false, tooltip: function (params) { return (params.value); }, width: 120, cellRenderer: Password, },
    { headerName: "Customer Name", field: "CustomerName", sortable: true, tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Type", field: "ApiMethod", sortable: true, tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Format", field: "Extension", sortable: true, tooltip: function (params) { return (params.value); }, width: 80 },
    { headerName: "Total Stone", field: "TotalCount", sortable: true, tooltip: function (params) { return (params.value); }, width: 85 },
    { headerName: "FTP Username", field: "FTPUser", sortable: true, tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "FTPPwdLenth", field: "FTPPwdLenth", sortable: false, hide: true, tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "FTP Password", field: "FTPPass", sortable: false, tooltip: function (params) { return (params.value); }, width: 120, cellRenderer: FTPPass, },
    { headerName: "Url", field: "APIUrl", sortable: false, tooltip: function (params) { return (params.value); }, width: 330 },
    { headerName: "Status", field: "Status", sortable: true, tooltip: function (params) { return (params.value); }, width: 70, cellRenderer: Status, },
    { headerName: "File", field: "FileName", sortable: false, tooltip: function (params) { return (params.value); }, width: 80, cellRenderer: FileName, },
    { headerName: "Date", field: "EDate", sortable: false, tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "Time", field: "ETime", sortable: false, tooltip: function (params) { return (params.value); }, width: 65 },
];
function FTPPass(params) {
    var estric = "";
    for (var i = 0; i <= parseInt(params.data.FTPPwdLenth); i++) {
        estric += "*";
    }
    var __t = "<span>" + estric + "</span>";
    return __t;
}
function Password(params) {
    var estric = "";
    for (var i = 0; i <= parseInt(params.data.PwdLenth); i++) {
        estric += "*";
    }
    var __t = "<span>" + estric + "</span>";
    return __t;
}
function Status(params) {
    if (params.data.Status == true) {
        return "<span class='Yes'>Success</span>";
    }
    else {
        return "<span class='No'>Fail</span>";
    }
}
function FileName(params) {
    if (params.data.Extension == "csv") {
        return "<div title='CSV File' onclick=\"FileOpen('" + params.data.FileName + "')\" class='Customer-action-cel'><img src='/Content/images/api/csv.png' style='cursor:pointer;'/></div>";
    }
    else if (params.data.Extension == "xml") {
        return "<div title='XML File' onclick=\"FileOpen('" + params.data.FileName + "')\" class='Customer-action-cel'><img src='/Content/images/api/xml.png' style='cursor:pointer;'/></div>";
    }
    else if (params.data.Extension == "json") {
        return "<div title='JSON File' onclick=\"FileOpen('" + params.data.FileName + "')\" class='Customer-action-cel'><img src='/Content/images/api/json.png' style='cursor:pointer;'/></div>";
    }
    else if (params.data.Extension == "xls") {
        return "<div title='XLS File' onclick=\"FileOpen('" + params.data.FileName + "')\" class='Customer-action-cel'><img src='/Content/images/api/xls.png' style='cursor:pointer;'/></div>";
    }
    else if (params.data.Extension == "xlsx") {
        return "<div title='XLSX File' onclick=\"FileOpen('" + params.data.FileName + "')\" class='Customer-action-cel'><img src='/Content/images/api/xlsx.png' style='cursor:pointer;'/></div>";
    }
    else {
        return "";
    }
}

function FileOpen(path) {
    //location.href = path;
    window.open(path)
}
function download(filename, text) {
    var element = document.createElement('a');
    element.setAttribute('href', 'data:text/plain;charset=utf-8,' + encodeURIComponent(text));
    element.setAttribute('download', filename);
    element.style.display = 'none';
    document.body.appendChild(element);
    element.click();
    document.body.removeChild(element);
}

var gridDiv = document.querySelector('#myGrid');
function GetDataList() {
    loaderShow();
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }

    gridOptions = {
        rowHeight: 38,
        wrapText: true,
        masterDetail: true,
        detailCellRenderer: 'myDetailCellRenderer',
        //detailRowHeight: 70,
        groupDefaultExpanded: 1,
        //components: {
        //    dateIndicator: dateIndicator
        //},
        defaultColDef: {
            enableSorting: true,
            sortable: false,
            width: 150,
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
        rowModelType: 'serverSide',
        cacheBlockSize: GridpgSize, // you can have your custom page size
        paginationPageSize: GridpgSize, //pagesize
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };

    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);

    showEntryVar = setInterval(function () {
        if ($('#myGrid .ag-paging-panel').length > 0) {
            $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
            $('#ddlPagesize').val(GridpgSize);
        }
        clearInterval(showEntryVar);
    }, 500);

    setTimeout(function () {
        var allColumnIds = [];
        gridOptions.columnApi.getAllColumns().forEach(function (column) {
            allColumnIds.push(column.colId);
        });

        //gridOptions.columnApi.autoSizeColumns(allColumnIds, false);
    }, 1000);
}

const datasource1 = {
    getRows(params) {

        //console.log(JSON.stringify(params.request, null, 1));
        var OrderBy = '', PageNo = gridOptions.api.paginationGetCurrentPage() + 1;

        if (params.request.sortModel.length > 0) {
            OrderBy = '' + params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort + ''
        }
        var formData = new FormData();

        if ($("#txtMSearch").val() != '' && $("#txtMSearch").val() != null) {
            formData.append('CompSearch', $("#txtMSearch").val());
        }
        formData.append('FromDate', $('#txtFromDate').val());
        formData.append('ToDate', $('#txtToDate').val());
        formData.append('PageNo', PageNo);
        formData.append('PageSize', GridpgSize);
        formData.append('OrderBy', OrderBy);
        formData.append('Distinct', 1);
        formData.append('IsFTP', 1);
        $.ajax({
            url: "/Api/FTPAPIPortalLogList",
            async: false,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: function (data, textStatus, jqXHR) {
                if (data.Status != undefined) {
                    if (data.Status == "1") {
                        if (data.Data != null && data.Data.length > 0) {
                            params.successCallback(data.Data, data.Data[0].iTotalRec);
                        }
                        else {
                            params.successCallback([], 0);
                            toastr.warning(data.Message);
                        }
                    } else {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        params.successCallback([], 0);
                        toastr.error(data.Message);
                    }
                    loaderHide();
                }

            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                loaderHide();
            }
        });

    }
};

function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".result-nav").height(),
        contentHei = winH - navbarHei - 88;
    $("#myGrid").css("height", contentHei);
}

$(document).ready(function () {
    $('#txtFromDate').val(F_date);
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

    GetDataList();
    contentHeight();
    $('#btnSearch').click(function () {
        GetDataList();
    });


    $('#btnReset').click(function () {
        $("#txtMSearch").val("");
        $('#txtFromDate').val(F_date);
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
        $('#txtToDate').val(SetCurrentDate());
        GetDataList();
    });
});

$(window).resize(function () {
    contentHeight();
});
