$(document).ready(function () {
    
    $("#Sdate").on("change", function () {
        $("#Edate").attr("min", $(this).val());
    });
    $(".search-btn").on("click", function () {
        $(this).attr("disabled", true);
        LoadData($(this));
    });
    $("#CityId,#CustId,#Pagesize").on("change", function () {
        LoadData();
    });
    $(".clear-btn").on("click", function () {
        $("#Search").val('');
        $("#CustId").val('');
        $("#CityId").val('');
        $("#Sdate").val('');
        $("#Edate").val('');
        $("#curpageidx").val('');
        $("#lastsortby").val('odate');
        $("#lastsorto").val('ASC');
        $(this).attr("disabled", true);
        LoadData($(this));
    });
});
function LoadData(buttonid = null) {
    $.ajax({
        url: '/Order/Orderlist',
        data: {

            search: $("#Search").val(),
            custId: $("#CustId").val(),
            cityId: $("#CityId").val(),
            sdate: $("#Sdate").val(),
            edate: $("#Edate").val(),
            cPage: $("#curpageidx").val(),
            Pagesize: $("#Pagesize").val(),
            sortBy: $("#lastsortby").val(),
            sortOrder: $("#lastsorto").val()
        }
    })
        .done(function (response) {
            console.log(response);
            console.log("success");
            $(".datadiv").html(response);
            $("#"+$("#lastsortby").val()).after("<span id='arrow'> &#8597;</span>")
        })
        .fail(function (XMLHttpRequest, textStatus, errorThrown) {
            alert("FAIL");
        })
        .always(function () {
            console.log("always");
            if (buttonid!=null)
                buttonid.attr("disabled", false);
        });
}
function Sort(id) {
    document.getElementById("lastsortby").value = id;
    var las = $("#lastsorto").val();
    if (las == "ASC")
        $("#lastsorto").val("DESC");
    else
        $("#lastsorto").val("ASC");
    LoadData();
}
function Goto(index) {
    document.getElementById("curpageidx").value = index;
    LoadData();

}