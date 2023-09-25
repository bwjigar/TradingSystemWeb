$(document).ready(function () {
    $("#loading").css("display", "none");
    //Show Save Api Data Table
    var table = $('#SaveApiTable').DataTable({
        "bProcessing": false,
        "serverSide": true,
        "bPaginate": false,
        "bFilter": false,
        "scrollY": "500px",
        "scrollX": true,       
        "ordering": false,
        "ajax": {
            url: "/Home/GetSaveApiList", //"/Home/GetSaveApiList",
            dataType: "json",
            type: "POST",
            async: false,
            cache: false,
            success: function (data) {
                var htmldata = "";
                debugger
                var jsonDataNew = JSON.parse(data);
                j = 0;
                for (var i = 0; i < jsonDataNew.length; i++) {
                    j++;
                    htmldata += "<tr>";
                    htmldata += "<td><input type='checkbox' class='chkparty' data-ftp-path='"+jsonDataNew[i].FTP_FILE_PATH+"' data-file-name='"+jsonDataNew[i].FTP_FILENAME+"'  data-api-method='" + jsonDataNew[i].JSON_DATA_TYPE + "' data-type-attr='" + jsonDataNew[i].TYPE_OF_DATA + "' data-seq-attr='" + jsonDataNew[i].SEQ_NO + "' data-Party-attr='" + jsonDataNew[i].SUPP_SEQ + "'  data-apilink='" + jsonDataNew[i].LINK + "' name='chkselect' value='' /></td>";
                    htmldata += "<td>" + j + "</td>";
                    if (jsonDataNew[i].FTP_FILENAME == null) {
                        if (jsonDataNew[i].TYPE_OF_DATA == "JSON") {
                            htmldata += "<td class='data-fileName' data-file='JSON_" + jsonDataNew[i].FILE_NAME + "'>JSON_" + jsonDataNew[i].FILE_NAME + "</td>";
                        }
                        else if (jsonDataNew[i].TYPE_OF_DATA == "EXCEL") {
                            htmldata += "<td class='data-fileName' data-file='EXCEL_" + jsonDataNew[i].FILE_NAME + "'>EXCEL_" + jsonDataNew[i].FILE_NAME + "</td>";
                        }
                    }
                    else
                    {
                        htmldata += "<td class='data-fileName' data-file='" + jsonDataNew[i].FTP_FILENAME + "'>" + jsonDataNew[i].FTP_FILENAME + "</td>";
                    }
                    if (jsonDataNew[i].FTP_FILENAME == null) {
                        htmldata += "<td>" + jsonDataNew[i].LINK + "</td>";
                    }
                    else
                    {
                        htmldata += "<td>" + jsonDataNew[i].FTP_FILE_PATH + "</td>";
                    }
                    htmldata += "<td></td>";
                    htmldata += "<td></td>";
                    htmldata += "<td><input type='checkbox' name='Download' class='chkDownload' value='' /></td>";
                    htmldata += "<td><input type='checkbox' name='Template' class='chkTemplate' value='' /></td>";
                    htmldata += "<td><input type='checkbox' name='Verified' class='chkVerified' value='' /></td>";
                    htmldata += "<td>" + jsonDataNew[i].SUPP_NAME + "</td>";
                    if (jsonDataNew[i].FTP_FILENAME == null) {
                        if (jsonDataNew[i].TYPE_OF_DATA == "JSON") {
                            htmldata += "<td data-supp-seq='"+jsonDataNew[i].SUPP_SEQ+"' data-File-Name='JSON_" + jsonDataNew[i].FILE_NAME + "'><button type='button' class='btn btn-primary btnerror btnErrorList' style='width: 100%;'>Error <span class='errorbtn'></span></button></td>";

                        }
                        else if (jsonDataNew[i].TYPE_OF_DATA == "EXCEL") {
                            htmldata += "<td data-supp-seq='" + jsonDataNew[i].SUPP_SEQ + "' data-File-Name='EXCEL_" + jsonDataNew[i].FILE_NAME + "'><button type='button' class='btn btn-primary btnerror btnErrorList' style='width: 100%;'>Error <span class='errorbtn'></span></button></td>";

                        }
                    }
                    else {
                        htmldata += "<td data-supp-seq='" + jsonDataNew[i].SUPP_SEQ + "' data-File-Name='" + jsonDataNew[i].FTP_FILENAME + "'><button type='button' class='btn btn-primary btnerror btnErrorList' style='width: 100%;'>Error <span class='errorbtn'></span></button></td>";
                    }
                    htmldata += "<td>" + jsonDataNew[i].TYPE_OF_DATA + "</td>";
                    htmldata += "</tr>";
                }
                $("#SaveApiBody").empty();
                $("#SaveApiBody").html(htmldata);
            }
        }        
    });
    table.columns.adjust().draw();
    //End

    //Verify And Save All Data Into Table Stock And ClientStock
    $("#VerifyApi").click(function () {
        debugger
        var ModalData = [];
        $("#loading").css("display", "block");
        var ApiLink = "",PartyName="",MasSeq="",DataType="",jsonType="",FtpFilePath="",FileName="",MainFileName="";
        
        if ($(".chkparty:checked").length > 0) {
            $.each($(".chkparty:checked"), function (i) {               
                ApiLink += (ApiLink.length > 0 ? ',' : '') + $(this).attr("data-apilink");
                PartyName += (PartyName.length > 0 ? ',' : '') + $(this).attr("data-Party-attr");
                MasSeq += (MasSeq.length > 0 ? ',' : '') + $(this).attr("data-seq-attr");
                DataType += (DataType.length > 0 ? ',' : '') + $(this).attr("data-type-attr");
                jsonType +=(jsonType.length>0?',':'')+$(this).attr("data-api-method");
                FtpFilePath +=(FtpFilePath.length>0?',':'')+$(this).attr("data-ftp-path"); 
                FileName += (FileName.length > 0 ? ',' : '') + $(this).attr("data-file-name");
                MainFileName += (MainFileName.length > 0 ? ',' : '') + $(this).closest('tr').find('.data-fileName').attr("data-file");
            });
            var SaveApiModal = {
                "LINK": ApiLink, "SUPP_SEQ": PartyName, "SEQ_NO": MasSeq, "TYPE_OF_DATA": DataType, "JSON_DATA_TYPE": jsonType, "FTP_FILE_PATH": FtpFilePath, "FTP_FILENAME": FileName, "FILE_NAME": MainFileName
                }
           
        }
       
        if ($('.chkparty:checked').length > 0) {
            $.ajax({
                url: "/Home/VerifyApiData", //"/Home/VerifyApiData",
                data: { 'SaveModal': SaveApiModal, 'ApiLink': ApiLink, 'PartyName': PartyName, 'MasSeq': MasSeq, 'DataType': DataType },
                dataType: "json",
                type: "POST",
                async: true,
                cache: false,
                success: function (data) {
                    var FinalData = data.split("|");
                    debugger
                    if (FinalData[0] == "SUCCESS") {
                        $('.chkparty:checked').closest('tr').find('input:checkbox').prop('checked', true);
                        var Error = FinalData[1].split(",");                    
                       
                        $('.chkparty:checked').closest('tr').find(".errorbtn").each(function (i) {
                            var Count = Error[i].split("$");                           
                            $(this).text("T(" + Count[0] + ") F(" + Count[1] + ")");
                        });
                       
                        $("#ignorechk").removeClass('hidshow');
                       
                    }
                    $("#loading").css("display", "none");
                }
            });
        }
        else {
            alert('Select Party First');
            $("#loading").css("display", "none");
        }

    });
    //End

    //Select All CheckBox
    $('#chkAllSelect').click(function () {
        $(".chkparty").each(function () {
            if($(this).prop('checked') == false)
            {
                $(this).prop('checked', true);
            }
            else
            {
                $(this).prop('checked', false);
            }
        });
    });
    //End
    
    //Redirect To Error List Page
    $(document).on('click', '.btnErrorList', function () {
        debugger
        if ($(this).closest("tr:has(td)").find('input[type="checkbox"]').prop('checked')== true)
        {
            var FileName = $(this).closest("td").attr('data-File-Name');
            var SuppSeq = $(this).closest("td").attr('data-supp-seq');
            window.open('/Home/ErrorResolveData?FileName=' + $.trim(FileName) + '&SuppSeq=' + SuppSeq, '_blank');
        }
        else
        {
            alertify.alert("Files Verification Pending..!");
        }
        
    });
    //End

    //Reject Error Via CheckBox Selection
    $(document).on('change', '#IgnoreError', function () {
        //$("#loading").css("display", "block");      
        if ($("#IgnoreError:checked").length > 0) { 
            debugger
            var FileName = "";
            if ($(".chkparty:checked").length > 0) {

                $.each($(".chkparty:checked"), function (i) {
                    FileName += (FileName.length > 0 ? ',' : '') + $(this).closest('tr').find('.data-fileName').attr("data-file");

                });
            }
            $.ajax({
                url: "/Home/IgnoreTempStockData",
                data: { 'FileName': FileName },//"/Home/GetExcelColumnList",              
                dataType: "json",
                type: "POST",
                async: true,
                cache: false,
                success: function (data) {
                   
                }
            });
        }
        else {
           // $("#loading").css("display", "none");            
        }

    });
    //End

    //Save All Data After Resolve
    $(document).on('click', '#SaveApi', function () {
        $("#loading").css("display", "block");
        if ($("#IgnoreError").prop('checked') == true)
        {
            var FileName = "";
            var SuppSeq = "";
            if ($(".chkparty:checked").length > 0) {
                $.each($(".chkparty:checked"), function (i) {
                    FileName += (FileName.length > 0 ? ',' : '') + $(this).closest('tr').find('.data-fileName').attr("data-file");
                    SuppSeq += (SuppSeq.length > 0 ? ',' : '') + $(this).attr("data-seq-attr");
                });
            }           
            $.ajax({
                url: "/Home/SaveAllTempStockData",//"/Home/GetExcelColumnList",
                data:{'AllFile':FileName,'AllSuppSeq':SuppSeq},
                dataType: "json",
                type: "POST",
                async: true,
                cache: false,
                success: function (data) {
                    debugger
                    if (data != "FAIL") {
                        alertify.alert("Save Data SuccessFully..!", function () {
                            location.reload();
                        });
                        $("#loading").css("display", "none");
                    }
                    else {
                        alertify.alert("Reject or Resolve Error First..!");
                        $("#loading").css("display", "none");
                    }
                }
            });
        }
        else
        {
            $("#loading").css("display", "none");
            alertify.alert("Verify Or Remove Error First..!");
            
        }
        

    });
    //End

});