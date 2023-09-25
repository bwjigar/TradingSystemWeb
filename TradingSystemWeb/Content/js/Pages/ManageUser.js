var gridOptions = {};
var iUserid = 0;

var loaderShow = function () {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
}

var loaderHide = function () {
    $('.loading-overlay-image-container').hide();
    $('.loading-overlay').hide();
}

var columnDefs = [
    // { headerName: "UserId", field: "iUserid", width: 80, },iSr
    { headerName: "Sr", field: "iSr", width: 40, tooltip: function (params) { return (params.value); }, sortable: false },
    { headerName: "Create Date", field: "sCreatedDate", tooltip: function (params) { return (params.value); }, width: 85 },//CLAR  
    { headerName: "Customer Name", field: "sFullName", tooltip: function (params) { return (params.value); }, width: 110 },
    { headerName: "User Name", field: "sUsername", width: 120, tooltip: function (params) { return (params.value); }, },
    {
        headerName: "Company Name", field: "sCompName", tooltip: function (params) { return (params.value); }, width: 200,
        cellRenderer: function (params) {
            return params.value;
        }
    },
    { headerName: "Party Code", field: "FortunePartyCode", tooltip: function (params) { return (params.value); }, width: 80, },
    { headerName: "Assist1", field: "AssistBy1", tooltip: function (params) { return (params.value); }, width: 80, },
    { headerName: "Assist2", field: "AssistBy2", tooltip: function (params) { return (params.value); }, width: 80 },// COL
    { headerName: "User Type", field: "sUserType", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "Account Suspended", field: "Suspended", tooltip: function (params) { return (params.value); }, width: 130, cellClass: ['muser-red-font'] },
    { headerName: "Active", field: "bIsActive", cellRenderer: 'faIndicator', tooltip: function (params) { return (params.value); }, width: 50, cellClass: ['muser-fa-font'] },
    { headerName: "Activation Date", field: "sModifiedDate", tooltip: function (params) { return (params.value); }, width: 90 },//CLAR
    { headerName: "Action", field: "bIsAction", tooltip: function (params) { return (params.value); }, width: 85, cellRenderer: 'deltaIndicator', sortable: false },
];
var CustomerManageActiveCol = function (params) {
    if (params.data.bIsActive == true) {
        var data = '<div class="Customer-active-cel"> <a href=""><i class="fa fa-check"></i></a></div>';
        return data;
    }
    else {
        var data = '';
        return data;
    }
}function CustomerManageAction(params) {
    var Url = ''.replace("", params.data.bIsAction);
    var data = '<div class="Customer-action-cel"> <a href=""><i class="flaticon-edit"></i></a>&nbsp;&nbsp;' +
        '<a href=""><i class="flaticon-trash-2"></i></a>' + '</div>';
    return data;
}

var UserDetailPage = function (params) {

    return '';
}

var deltaIndicator = function (params) {
    var element = "";
    if ($("#hdn_IsEmployee").val() != "1") {
        var element = '<a href="#" onclick="GoToUserDetail(\'' + params.data.sUserType + '\',' + params.data.iUserid + ',\'' + params.data.sUsername + '\')" ><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 17px;"></i></a>';
        element += '&nbsp;&nbsp;&nbsp;<a href="#" onclick="DeleteUserDetail(' + params.data.iUserid + ')" ><i class="fa fa-trash-o" aria-hidden="true"></i></a>';
    }
    //var element = document.createElement("a");
    //element.title = '';
    //element.innerHTML = '<i class="fa fa-trash-o" aria-hidden="true"></i>';
    //element.href = '#';
    //element.onclick = 'DeleteUserDetail(' + params.data + ')';

    //    element.appendChild(document.createTextNode(params.value));
    return element;
}

var faIndicator = function (params) {
    var element = document.createElement("a");
    element.title = '';
    element.innerHTML = '<i class="fa fa-check" aria-hidden="true"></i>';
    element.href = '#';
    if (params.value) {
        return element;
    }
}

var GoToUserDetail = function (sUserType, iUserid, sUsername) {
    window.open('/User/Edit?UserType=' + sUserType + '&UserID=' + iUserid + '&UserName=' + sUsername);
}

var DeleteUserDetail = function (iUserid) {
    $("#hdnDelUserId").val(iUserid);
    $("#Remove").modal("show");
}

var ClearRemoveModel = function () {
    $("#hdnDelUserId").val("0");
    $("#Remove").modal("hide");
}

var DeleteUser = function () {
    loaderShow();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: '/User/Delete',
        data: '{ "UserID": ' + $("#hdnDelUserId").val() + '}',
        success: function (data) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            loaderHide();
            ClearRemoveModel();
            toastr.success(data.Message, { timeOut: 3000 });
            GetSearch();
        }
    });
}

function GetSearch() {
    loaderShow();
    //rowData = data;
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }

    gridOptions = {
        defaultColDef: {
            enableSorting: true,
            sortable: true,
            resizable: true,
            filter: 'agTextColumnFilter',
            filterParams: {
                applyButton: true,
                resetButton: true,
            }
        },
        components: {
            deltaIndicator: deltaIndicator,
            faIndicator: faIndicator,
        },
        pagination: true,
        icons: {
            groupExpanded:
                '<i class="fa fa-minus-circle"/>',
            groupContracted:
                '<i class="fa fa-plus-circle"/>'
        },
        rowSelection: 'multiple',
        suppressRowClickSelection: true,
        columnDefs: columnDefs,
        //rowData: data,
        rowModelType: 'serverSide',
        onGridReady: onGridReady,
        cacheBlockSize: 50, // you can have your custom page size
        paginationPageSize: 50, //pagesize
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };
    var gridDiv = document.querySelector('#Cart-Gride');
    new agGrid.Grid(gridDiv, gridOptions);

    gridOptions.api.setServerSideDatasource(datasource1);
}const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var CountryName = "";
        var UserName = "";
        var UserFullName = "";
        var CompanyName = "";
        var SortColumn = "";
        var SortDirection = "";

        if ($("#ddlFilterType").val() == "CT") {
            CountryName = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "CUN") {
            UserFullName = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "UN") {
            UserName = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "CM") {
            CompanyName = $("#txtCommonName").val();
        }

        var UserType = $('#ddlUserType').val();
        var UserStatus = $('#ddlIsActive').val();
        
        if (params.request.sortModel.length > 0) {
            SortColumn = params.request.sortModel[0].colId;
            SortDirection = params.request.sortModel[0].sort;
        }

        $.ajax({
            url: "/User/GetUsers",
            async: false,
            type: "POST",
            data: {
                CompanyName: CompanyName,
                CountryName: CountryName,
                UserName: UserName,
                UserFullName: UserFullName,
                UserType: UserType,
                UserStatus: UserStatus,
                PageNo: PageNo,
                IsEmployee: $("#hdn_IsEmployee").val(),
                SortColumn: SortColumn,
                SortDirection: SortDirection
            },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    params.successCallback(data.Data, data.Data[0].iTotalRec);
                }
                else {
                    toastr.error(data.Message, { timeOut: 2500 });
                    params.successCallback([], 0);
                }

                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                loaderHide();
            }
        });
    }
};function onGridReady(params) {
    if (navigator.userAgent.indexOf('Windows') > -1) {
        this.api.sizeColumnsToFit();
    }
}var Reset = function () {
    $('#ddlFilterType').val('');
    $('#txtCommonName').val('');
    $('#ddlUserType').val('');
    $('#ddlIsActive').val('');
    GetSearch();
}var DownloadUser = function () {
    var CountryName = "";
    var UserName = "";
    var UserFullName = "";
    var CompanyName = "";
    if ($("#ddlFilterType").val() == "CT") {
        CountryName = $("#txtCommonName").val(); //$scope.Customer.CommonName
    }
    if ($("#ddlFilterType").val() == "CUN") {
        UserFullName = $("#txtCommonName").val();
    }
    if ($("#ddlFilterType").val() == "UN") {
        UserName = $("#txtCommonName").val();
    }
    if ($("#ddlFilterType").val() == "CM") {
        CompanyName = $("#txtCommonName").val();
    }

    var UserType = $('#ddlUserType').val();
    var UserStatus = $('#ddlIsActive').val();
    var FormName = 'Manage User';
    var ActivityType = 'Excel Export';
    loaderShow();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: '/User/DownloadUser',
        data: "{" +
            "'CompanyName': '" + CompanyName + "', 'CountryName': '" + CountryName + "', 'UserName': '" + UserName + "'," +
            "'UserFullName': '" + UserFullName + "', 'UserType': '" + UserType + "', 'UserStatus': '" + UserStatus + "'," +
            "'FormName': '" + FormName + "', 'ActivityType': '" + ActivityType + "', 'IsEmployee': '" + $("#hdn_IsEmployee").val() + "'" +
            "}",
        success: function (data) {
            if (data.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            else if (data.indexOf('No record found') > -1) {
                toastr.error(data);
            }
            else {
                location.href = data;
            }
            loaderHide();
        }
    });}function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".order-title").height(),
        serachHei = $(".order-history-data").height(),
        contentHei = winH - serachHei - navbarHei - 130;
    $("#Cart-Gride").css("height", contentHei);
}$(document).ready(function (e) {
    GetSearch();
    contentHeight();
});

$(window).resize(function () {
    contentHeight();
});