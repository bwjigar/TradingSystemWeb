$(document).ready(function () {
    $("#loading").css("display", "none");
    var JsonDataType = "";
    //View Table Caption and Excel Column
    var table = $('#CaptionTable').DataTable({
        "bProcessing": false,
        "serverSide": true,
        "bPaginate": false,
        "bFilter": false,        
        "scrollY": "300px",       
        "ordering": false,
        "ajax": {
            url: "/Home/GetCaptionList", //"/Home/GetCaptionList",
            dataType: "json",
            type: "POST",
            async: false,
            cache: false,
            success: function (data) {
                var htmldata = "";
                var jsonDataNew = JSON.parse(data);
                j = 0;
                for (var i = 0; i < jsonDataNew.length; i++) {
                    j++;
                    if (jsonDataNew[i].EXCEL_COLUMN == null) { jsonDataNew[i].EXCEL_COLUMN = "" }
                    if (jsonDataNew[i].COLUMN_NAME == null) { jsonDataNew[i].COLUMN_NAME = jsonDataNew[i].DB_COLUMN }
                    htmldata += "<tr><td class='cls-SeqNo' data-attr-seqno='" + jsonDataNew[i].SEQ_NO + "' data-mas_seq='" + jsonDataNew[i].MAS_SEQ + "'>" + j + "</td>";
                    htmldata += "<td class='cls-Name' _data-mas_seq='" + jsonDataNew[i].MAS_SEQ + "' data-attr-column='" + jsonDataNew[i].COLUMN_NAME + "' data-attr-seqno='" + jsonDataNew[i].SEQ_NO + "' data-attr-colseqno='" + jsonDataNew[i].COLUMN_SEQ + "'>" + jsonDataNew[i].DISPLAY_NAME + "</td>";
                    htmldata += "<td class='cls-ExcelCol' data-attr-caption='" + jsonDataNew[i].EXCEL_COLUMN + "'>" + jsonDataNew[i].EXCEL_COLUMN + "</td></tr>";
                }
                $("#CaptionBody").empty();
                $("#CaptionBody").html(htmldata);
            }
        }
    });
    //End

    //Open Modal For Insert Extra Parametars 
    $('#PostPara').click(function () {
        if ($("#PostPara").prop('checked') == true) {
            $("#exampleModal").modal('show');
        }
       
    });
    //End

    //Load Matching Excel Column In Table
    $("#LoadApi").click(function () {
        Para = {}
        ParaMetar = {}
        $("#loading").css("display", "block");
        if ($("#inlineRadio3").is(":checked") && $("#TYPE_OF_DATA").val() != "" && $("#txtftp").val()!="" && $("#FILE_NAME").val()!="")
        {           
            JsonDataType = "DIRECT FROM FTP";
            var FtpPath = $("#txtftp").val() + "\\" + $("#FILE_NAME").val();
            $.ajax({
                url: "/Home/GetDatafromFTP",//"/Home/GetDatafromFTP",
                data: { 'FtpPath': FtpPath },
                dataType: "json",
                type: "POST",
                async: true,
                cache: false,
                success: function (data) {
                    if (data == "SUCCESS") {
                        table.ajax.reload();
                    }
                    else if (data = "File Not Found")
                    {
                        alertify.alert(data);
                    }
                    $("#loading").css("display", "none");
                }
            });
        }
        else if ($("#inlineRadio1").is(":checked") || $("#inlineRadio2").is(":checked") && $("#TYPE_OF_DATA").val() != "" && $("#LINK").val() != "")
        {
            
            if ($("#PostApi").prop('checked') == true) {
                JsonDataType = "USING POST METHOD";
                
                Para[$("#Name1").val().toString().trim()] = $("#Parametars1").val().toString().trim();
                Para[$("#Name2").val().toString().trim()] = $("#Parametars2").val().toString().trim();
                Para[$("#Name3").val().toString().trim()] = $("#Parametars3").val().toString().trim();
                Para[$("#Name4").val().toString().trim()] = $("#Parametars4").val().toString().trim();
                Para[$("#Name5").val().toString().trim()] = $("#Parametars5").val().toString().trim();
                Para[$("#Name6").val().toString().trim()] = $("#Parametars6").val().toString().trim();
                Para[$("#Name7").val().toString().trim()] = $("#Parametars7").val().toString().trim();
                Para[$("#Name8").val().toString().trim()] = $("#Parametars8").val().toString().trim();
                Para[$("#Name9").val().toString().trim()] = $("#Parametars9").val().toString().trim();
            }
            if ($("#PostPara").prop('checked') == true && $("#PostApi").prop('checked') == false) {
                JsonDataType = "USING EXTRA PARAMETERS";
                
                ParaMetar = {
                    Name1: $("#Name1").val().toString().trim(),
                    Name2: $("#Name2").val().toString().trim(),
                    Name3: $("#Name3").val().toString().trim(),
                    Name4: $("#Name4").val().toString().trim(),
                    Name5: $("#Name5").val().toString().trim(),
                    Name6: $("#Name6").val().toString().trim(),
                    Name7: $("#Name7").val().toString().trim(),
                    Name8: $("#Name8").val().toString().trim(),
                    Name9: $("#Name9").val().toString().trim(),
                    Para1: $("#Parametars1").val().toString().trim(),
                    Para2: $("#Parametars2").val().toString().trim(),
                    Para3: $("#Parametars3").val().toString().trim(),
                    Para4: $("#Parametars4").val().toString().trim(),
                    Para5: $("#Parametars5").val().toString().trim(),
                    Para6: $("#Parametars6").val().toString().trim(),
                    Para7: $("#Parametars7").val().toString().trim(),
                    Para8: $("#Parametars8").val().toString().trim(),
                    Para9: $("#Parametars9").val().toString().trim()                    
                }
            }
            if ($("#PostPara").prop('checked') == false && $("#PostApi").prop('checked') == false) {
                JsonDataType = "DIRECT DATA FROM LINK";
            }

            var ColumnMapData = {
                "DATA_FROM": $("input[type=radio][name=DATA_FROM]:checked").val(),
                "TYPE_OF_DATA": $("#TYPE_OF_DATA").val(),
                "LINK": $("#LINK").val(),
                "JSON_DATA_TYPE": JsonDataType
            }

            $.ajax({
                url: "/Home/GetDataJsonApi",//"/Home/GetDataJsonApi",
                data: { 'ExcelMas': ColumnMapData, 'JsonObj': JSON.stringify(Para), 'Parametar': ParaMetar },
                dataType: "json",
                type: "POST",
                async: true,
                cache: false,
                success: function (data) {
                    if (data == "SUCCESS") {
                        table.ajax.reload();
                    }
                    $("#loading").css("display", "none");
                }
            });            
        }
        else
        {
            $("#loading").css("display", "none");
            if ($("#SUPP_SEQ").val() == "") {
                $("#SUPP_SEQ").focus();
            }
           else if ($("#TYPE_OF_DATA").val() == "") {
                $("#TYPE_OF_DATA").focus();
            }
          else if ($("#LINK").val() == "" && !$("#LINK").is(':disabled'))  {
                $("#LINK").focus();
          }
          else if ($("#FILE_NAME").val() == "") {
              $("#FILE_NAME").focus();
          }
          else if ($("#txtftp").val() == "" && !$("#txtftp").is(':disabled')) {
                $("#txtftp").focus();
          }
          
           
        }
        
    });
    //End

    //Get Client Excel Header list from Click On Td
    var columtext = "";
    $("#CaptionTable").on('click', '.cls-ExcelCol', function (e) {
        var AppendHtml = "<option value=''>NoSelect</option>";
        var DropList = $(this).find(".selectpicker");
        var SelectData = "";
        var thisText = $(this).text();
        var ExcelCol = "";
        $(".cls-ExcelCol").each(function () {

            ExcelCol += (ExcelCol.length > 0 ? "," : "") + $(this).text();
        })
        if (DropList.length == "0" && $(this).closest('input[name="drop-select"]').length == 0 && ExcelCol!="") {
            var SelectList = $("#CaptionBody").find(".selectpicker");
            if (SelectList.length > 0) {
                var dataAttr = SelectList.closest('td').attr("data-attr-caption");
                SelectList.closest('td').text(dataAttr);
            }
            $.ajax({
                url: "/Home/GetExcelColumnList", //"/Home/GetExcelColumnList",
                dataType: "json",
                type: "POST",
                async: false,
                cache: false,
                success: function (data) {
                    SelectData = data;
                    if (data != "") {
                        var jsonData = JSON.parse(data);
                        for (var i = 0; i < jsonData.length; i++) {
                            if (jsonData[i].EXCEL_COLUMN != "") {
                                if (jsonData[i] == thisText) {
                                    AppendHtml += "<option value='" + jsonData[i] + "' selected='selected'>" + jsonData[i] + "</option>";
                                }
                                else {
                                    AppendHtml += "<option value='" + jsonData[i] + "'>" + jsonData[i] + "</option>";
                                }
                            }
                        }
                    }
                }
            });
            if (SelectData != "") {
                $(this).html("");
                $(this).html("<select id='mySelect' name='drop-select' class='selectpicker form-control' style='width:100%;max-width: 100%;background: #428bca;color: white;'>" + AppendHtml + "</select>");
                $('#mySelect').selectize();
                // $("#mySelect").select2();
                // currenttd = this;
            }
            else {
                
            }
        }

    });
    //End

    //Select Client Header Caption From Drop down
    $(document).on('change', '.selectpicker', function () {

        if ($(this).find(":selected").text() == "NoSelect") {
            $(this).closest('td').text("");
        }
        else {
            $(this).closest('td').text($(this).find(":selected").text());
        }

    });
    //End

    //Disabled and Enabled Textbox On Radio Button Selection
    $(document).on('change', '#inlineRadio3', function () {
        $("#txtftp").removeAttr('disabled');
        $("#LINK").attr('disabled', true);
        $("#PostApi").attr('disabled', true);
        $("#PostPara").attr('disabled', true);
    });
    $(document).on('change', '#inlineRadio2', function () {
        $("#txtftp").attr('disabled', true);
        $("#LINK").removeAttr('disabled');
        $("#PostApi").removeAttr('disabled');
        $("#PostPara").removeAttr('disabled');
    });
    $(document).on('change', '#inlineRadio1', function () {
        $("#txtftp").attr('disabled', true);
        $("#LINK").removeAttr('disabled');
        $("#PostApi").removeAttr('disabled');
        $("#PostPara").removeAttr('disabled');
    });
    //End

    //Save Column Mapping Table
    $(document).on('click', '#SaveMapping', function () {
        var ListOfTable = []; var ListExcel = [];
        var Data = "";
        var ExcelCol = "";
        $(".cls-ExcelCol").each(function () {

            ExcelCol += (ExcelCol.length > 0 ? "," : "") + $(this).text();
        })
       
        if(ExcelCol!="")
        {
            $(".cls-Name").each(function (key, index) {

                if ($(this).closest('tr').find('.cls-ExcelCol').find("select").length > 0) {
                    $(this).closest('tr').find('.cls-ExcelCol').text("");
                }
                //Mapping Table Data
                if ($(this).attr("_data-mas_seq") > 0) {
                    ListOfTable.push({
                        SEQ_NO: $(this).attr("data-attr-seqno"), DB_COLUMN: $(this).attr("data-attr-column"), EXCEL_COLUMN: $(this).closest('tr').find('.cls-ExcelCol').text(),
                        COLUMN_SEQ: $(this).attr("data-attr-colseqno"), DISPLAY_NAME: $(this).text(), MAS_SEQ: $(this).attr("_data-mas_seq")
                    });
                }
                else {
                    ListOfTable.push({
                        SEQ_NO: $(this).attr("data-attr-seqno"), DB_COLUMN: $(this).attr("data-attr-column"), EXCEL_COLUMN: $(this).closest('tr').find('.cls-ExcelCol').text(),
                        COLUMN_SEQ: $(this).attr("data-attr-seqno"), DISPLAY_NAME: $(this).text(), MAS_SEQ: $(this).attr("_data-mas_seq")
                    });
                }
                //End
            });

            //Pipe Sign Parametar and ParametarValue For Database
            var NamePara = "";
            var ValuePara = "";
            $(".data-post-name").each(function (key, index) {
                if ($(this).val().toString().trim() != "" && $(this).val().toString().trim() != null) {
                    NamePara += (NamePara.length > 0 ? "|" : "") + $(this).val().toString().trim();
                }
            });
            $(".data-post-parametars").each(function (key, index) {
                if ($(this).val().toString().trim() != "" && $(this).val().toString().trim() != null) {
                    ValuePara += (ValuePara.length > 0 ? "|" : "") + $(this).val().toString().trim();
                }
            });
            //End

            //GetData All Textbox and Drop Down Radion Button Pass To Class
           
            var exModal = {
                "DATA_FROM": $("input[type=radio][name=DATA_FROM]:checked").val(), "TYPE_OF_DATA": $("#TYPE_OF_DATA").val(), "SUPP_SEQ": $("#SUPP_SEQ").val(),
                "NARRATION": $("#NARRATION").val(), "FILE_NAME": $("#FILE_NAME").val(), "LINK": $("#LINK").val(), "PARAMETER": NamePara,
                "PARAMETER_VALUE": ValuePara, "JSON_DATA_TYPE": JsonDataType, "FTP_FILE_PATH": $("#txtftp").val()
            }
            //End

            //Pass All Data To Home Controller Via Ajax
            $.ajax({
                url: "/Home/SaveColamnMapping",//"/Home/SaveColamnMapping",
                data: ({ 'Mappindata': ListOfTable, 'exModaldata': exModal, 'MAS_SEQ': $(".cls-SeqNo").attr("data-mas_seq") }),
                dataType: "json",
                type: "POST",
                async: false,
                cache: false,
                success: function (data) {
                    if (data != "Fail") {
                        alertify.alert('Save Data Succeffully..!', function () {
                            $(".clear").val('');
                            $("#PostApi").prop('checked', false);
                            $("#PostPara").prop('checked', false);
                            table.ajax.reload();
                        });

                    }
                    else {
                        alertify.alert('Fail to Insert..!');
                    }
                }
            });
            //End
        }
        else {
            alertify.alert("No Data Found");
        }

    });
    //End
});