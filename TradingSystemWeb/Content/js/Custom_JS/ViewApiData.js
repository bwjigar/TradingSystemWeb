$(document).ready(function () {
    $("#loading").css("display", "none");
    //View Api Data Table
    $('#ViewApiDataTable').DataTable({
        "bPaginate": false,
        "bFilter": false,
        "scrollY": "500px",
        "scrollX": true,
        "info": false
    });
    //End
   
});