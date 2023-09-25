$(document).ready(function () {
    $("#loading").css("display", "none");
    //View Api Data Table
    var table = $('#ErrorSolveTable').DataTable({
        "bPaginate": false,
        "bFilter": false,
        "scrollY": "500px",
        "scrollX": true,
        "info": false,
        "ordering": false,
        "serverSide": false,
        "ajax": {
            url: "/Home/GetErrorList", //"/Home/GetSaveApiList",
            dataType: "json",
            type: "POST",
            async: false,
            cache: false,
            success: function (data) {
                var htmldata = "";
                debugger
                if (data != "") {
                    var jsonDataNew = JSON.parse(data);
                    for (var i = 0; i < jsonDataNew.length; i++) {

                        htmldata += "<tr class='error-fill-data'>";
                        htmldata += "<td><input type='checkbox' data-seq=no='" + jsonDataNew[i].SEQ_NO + "' class='ChkBoxError chkError" + jsonDataNew[i].REPORT_NO + "' data-ReportNo=" + jsonDataNew[i].REPORT_NO + " name='chkselect' value='' /></td>";
                        htmldata += "<td>" + jsonDataNew[i].PACKET_NO + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].REPORT_NO + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].ERROR_REMARK + "</td>";
                        htmldata += "<td class='data-Seq-Mas' data-error-remark='" + jsonDataNew[i].ERROR_REMARK + "'  data-seq='" + jsonDataNew[i].SEQ_NO + "' data-PacketNo=" + jsonDataNew[i].PACKET_NO + " data-ReportNo=" + jsonDataNew[i].REPORT_NO + " data-column='SHAPE' data-attr-caption=" + jsonDataNew[i].SHAPE + ">" + jsonDataNew[i].SHAPE + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].CTS + "</td>";
                        htmldata += "<td class='data-Seq-Mas' data-error-remark='" + jsonDataNew[i].ERROR_REMARK + "'  data-seq='" + jsonDataNew[i].SEQ_NO + "' data-PacketNo=" + jsonDataNew[i].PACKET_NO + " data-ReportNo=" + jsonDataNew[i].REPORT_NO + " data-column='COLOR' data-attr-caption=" + jsonDataNew[i].COLOR + ">" + jsonDataNew[i].COLOR + "</td>";
                        htmldata += "<td class='data-Seq-Mas' data-error-remark='" + jsonDataNew[i].ERROR_REMARK + "'  data-seq='" + jsonDataNew[i].SEQ_NO + "' data-PacketNo=" + jsonDataNew[i].PACKET_NO + " data-ReportNo=" + jsonDataNew[i].REPORT_NO + " data-column='PURITY' data-attr-caption=" + jsonDataNew[i].PURITY + ">" + jsonDataNew[i].PURITY + "</td>";
                        htmldata += "<td class='data-Seq-Mas' data-error-remark='" + jsonDataNew[i].ERROR_REMARK + "'  data-seq='" + jsonDataNew[i].SEQ_NO + "' data-PacketNo=" + jsonDataNew[i].PACKET_NO + " data-ReportNo=" + jsonDataNew[i].REPORT_NO + " data-column='CUT' data-attr-caption=" + jsonDataNew[i].CUT + ">" + jsonDataNew[i].CUT + "</td>";
                        htmldata += "<td class='data-Seq-Mas' data-error-remark='" + jsonDataNew[i].ERROR_REMARK + "'  data-seq='" + jsonDataNew[i].SEQ_NO + "' data-PacketNo=" + jsonDataNew[i].PACKET_NO + " data-ReportNo=" + jsonDataNew[i].REPORT_NO + " data-column='POLISH' data-attr-caption=" + jsonDataNew[i].POLISH + ">" + jsonDataNew[i].POLISH + "</td>";
                        htmldata += "<td class='data-Seq-Mas' data-error-remark='" + jsonDataNew[i].ERROR_REMARK + "'  data-seq='" + jsonDataNew[i].SEQ_NO + "' data-PacketNo=" + jsonDataNew[i].PACKET_NO + " data-ReportNo=" + jsonDataNew[i].REPORT_NO + " data-column='SYMM' data-attr-caption=" + jsonDataNew[i].SYMM + ">" + jsonDataNew[i].SYMM + "</td>";
                        htmldata += "<td class='data-Seq-Mas' data-error-remark='" + jsonDataNew[i].ERROR_REMARK + "'  data-seq='" + jsonDataNew[i].SEQ_NO + "' data-PacketNo=" + jsonDataNew[i].PACKET_NO + " data-ReportNo=" + jsonDataNew[i].REPORT_NO + " data-column='FLS' data-attr-caption=" + jsonDataNew[i].FLS + ">" + jsonDataNew[i].FLS + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].FLS_COLOR + "</td>";
                        htmldata += "<td class='data-Seq-Mas' id='LAB' data-error-remark='" + jsonDataNew[i].ERROR_REMARK + "'  data-seq='" + jsonDataNew[i].SEQ_NO + "' data-PacketNo=" + jsonDataNew[i].PACKET_NO + " data-ReportNo=" + jsonDataNew[i].REPORT_NO + " data-column='LAB' data-attr-caption=" + jsonDataNew[i].LAB + ">" + jsonDataNew[i].LAB + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].RATE + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].DISC_PER + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].NET_RATE + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].NET_VALUE + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].FANCY_COLOR + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].COLOR_OVERTONE + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].COLOR_INTENSITY + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].LENGTH + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].WIDTH + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].DEPTH + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].RATIO + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].DEPTH_PER + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].TABLE_PER + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].CH + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].CA + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].PH + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].PA + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].HA + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].GIRDLE_PER + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].GIRDLE_MIN + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].GIRDLE_MAX + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].GIRDLE + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].GIRDLE_COND + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].CULET + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].CULET_COND + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].SHADE + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].BROWN + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].GREEN + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].MILKY + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].LUSTER + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].EYE_CLEAN + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].CENTER_BLACK + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].SIDE_BLACK + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].CENTER_WHITE + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].SIDE_WHITE + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].IP + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].TABLE_OPEN + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].PAVILLION_OPEN + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].CROWN_OPEN + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].GIRDLE_OPEN + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].MEMBER_COMMENTS + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].CITY + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].COUNTRY + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].KEY_TO_SYMBOLS + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].REPORT_COMMENTS + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].STAR + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].LOWER_HALF + "</td>";
                        // htmldata += "<td>" + jsonDataNew[i].INSCRIPTION + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].POLISH_FEATURES + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].SYMM_FEATURES + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].GRAINING + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].COLOR_DESC + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].CLARITY_STATUS + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].PAINTING_COMMENTS + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].RESULT_DATE + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].LAB_SHAPE + "</td>";
                        //  htmldata += "<td>" + jsonDataNew[i].DNA + "</td>";
                        // htmldata += "<td>" + jsonDataNew[i].IMAGE_PATH + "</td>";
                        //  htmldata += "<td>" + jsonDataNew[i].VIDEO_PATH + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].STATUS + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].TRANS_DATE + "</td>";
                        htmldata += "<td>" + jsonDataNew[i].LWD + "</td>";
                        htmldata += "</tr>";
                    }
                    $("#ErrorSolveBody").empty();
                    $("#ErrorSolveBody").html(htmldata);
                }
                else {
                    $("#ErrorSolveBody").empty();
                    $("#ErrorSolveBody").html(htmldata);
                }
                $(".data-Seq-Mas:empty")                    
                    .css("background", "rgb(230,152,152,1)");
            }
        }
    });
    $($.fn.dataTable.tables(true)).DataTable().columns.adjust();
    //End

    //Clicking on Error Td Show Option Regarding Error Column In DropList
    $("#ErrorSolveTable").on('click', '.data-Seq-Mas', function (e) {
        debugger
        var AppendHtml = "<option value=''>NoSelect</option>";
        var DropList = $(this).find(".selectpicker");
        var SelectData = "";
        var thisText = $(this).text();
        var ColumnName = $(this).attr("data-column");
        if (DropList.length == "0" && $(this).closest('input[name="drop-select"]').length == 0) {
            if (thisText == "") {
                var SelectList = $("#ErrorSolveTable").find(".selectpicker");
                if (SelectList.length > 0) {
                    var dataAttr = SelectList.closest('td').attr("data-attr-caption");
                    SelectList.closest('td').text(dataAttr);
                }

                $.ajax({
                    url: "/Home/GetSeqList?Table=" + ColumnName, //"/Home/GetExcelColumnList",
                    dataType: "json",
                    type: "POST",
                    async: false,
                    cache: false,
                    success: function (data) {
                        SelectData = data;
                        if (data != "") {
                            var jsonData = JSON.parse(data);
                            for (var i = 0; i < jsonData.length; i++) {
                                if (jsonData[i].NAME != "") {
                                    AppendHtml += "<option value='" + jsonData[i].SHORT_NAME + "|" + jsonData[i].SEQ_NO + "'>" + jsonData[i].NAME + "</option>";
                                }
                            }
                        }
                    }
                });

                if (SelectData != "") {
                    $(this).html("");
                    $(this).html("<select id='SeqSelect' name='drop-select' class='selectpicker form-control' style='width:100%;max-width: 100%;background: #428bca;color: white;'>" + AppendHtml + "</select>");
                    $('select').selectize({
                        sortField: 'text'
                    });
                    //$("#SeqSelect").select2();
                    // currenttd = this;
                }
                else {
                }
            }
            else {
            }
        }
    });
    //End
   
    //Selection off Data For Error Resolve In Drop List
    $(document).on('change', '.selectpicker', function () {
        $("#loading").css("display", "block");
        var text = $(this).find(":selected").text();
        var value = $(this).find(":selected").val();
        var Seq = $(this).closest("td").attr('data-seq');
        var ReportNo = $(this).closest("td").attr('data-ReportNo');
        var Column = $(this).closest("td").attr('data-column');
        var tr = $(this).closest("tr");
        var ErrorRemark = $(this).closest("td").attr('data-error-remark');
        if ($(this).find(":selected").text() == "NoSelect") {
            $(this).closest('td').text("");
        }
        else {
            $(this).closest('td').text($(this).find(":selected").text());
        }

        var flag = false;
        var AllSeqCellValue = "";
        for (var i = 0; i < tr[0].cells.length; i++) {

            if (tr[0].cells[i].className == "data-Seq-Mas") {
                if (tr[0].cells[i].innerText == "") {
                    AllSeqCellValue += (AllSeqCellValue.length > 0 ? "," : "") + "null";
                }
                else {
                    AllSeqCellValue += (AllSeqCellValue.length > 0 ? "," : "") + tr[0].cells[i].innerText;
                }

            }
        }
        if (AllSeqCellValue.indexOf("null") == -1) {
            flag = true;
        }
        $.ajax({
            url: "/Home/UpdateErrorData", //"/Home/GetExcelColumnList",
            data: { 'Value': value, 'ReportNo': ReportNo, 'flag': flag, 'ColumnName': Column, 'ErrorRemark': ErrorRemark },
            dataType: "json",
            type: "POST",
            async: true,
            cache: false,
            success: function (data) {
                SelectData = data;
                debugger
                if (data != "FAIL") {
                    if (flag == true) {
                        table.ajax.reload();
                        $("#loading").css("display", "none");
                    }
                    $("#loading").css("display", "none");
                }
            }
        });
    });
    //End

    //Reject Error Via CheckBox Selection
    $(document).on('click', '#Rejectstock', function () {
        $("#loading").css("display", "block");
        var ReportNo = "";
        if ($(".ChkBoxError:checked").length > 0) {
            $.each($(".ChkBoxError:checked"), function (i) {
                ReportNo += (ReportNo.length > 0 ? ',' : '') + $(this).attr("data-ReportNo");
            });

            $.ajax({
                url: "/Home/RejectTempStockData", //"/Home/GetExcelColumnList",
                data: { 'ReportNo': ReportNo },
                dataType: "json",
                type: "POST",
                async: true,
                cache: false,
                success: function (data) {
                    if (data != "FAIL") {
                        table.ajax.reload();
                        $("#loading").css("display", "none");
                    }
                    else {
                        $("#loading").css("display", "none");
                    }
                }
            });
        }
        else {

            $("#loading").css("display", "none");
        }

    });
    //End

    //Save All Data After Resolve
    $(document).on('click', '#SaveApi', function () {
        $("#loading").css("display", "block");
        $.ajax({
            url: "/Home/SaveTempStockData",//"/Home/GetExcelColumnList",                
            dataType: "json",
            type: "POST",
            async: true,
            cache: false,
            success: function (data) {
                debugger
                if (data != "FAIL") {
                    alertify.alert("Save Data SuccessFully..!");
                    $("#loading").css("display", "none");
                }
                else {
                    alertify.alert("Reject or Resolve Error First..!");
                    $("#loading").css("display", "none");
                }
            }
        });

    });
    //End

    //Select All CheckBox
    $('#chkAllSelect').click(function () {
        $(".ChkBoxError").each(function () {
            if ($(this).prop('checked') == false) {
                $(this).prop('checked', true);
            }
            else {
                $(this).prop('checked', false);
            }
        });
    });
    //End


});