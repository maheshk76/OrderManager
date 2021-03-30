﻿$(document).ready(function () {
    
    $("#Sdate").on("change", function () {
        $("#Edate").attr("min", $(this).val());
    });
    $(".search-btn").on("click", function () {
        $(this).attr("disabled", true);
        LoadData($(this));
    });
    $("#Pagesize").on("change", function () {
        LoadData();
    });
    $(".clear-btn").on("click", function () {
        $("#Search").val('');
        $("#CustId").val('');
        $("#CityId").val('');
        $("#Sdate").val('');
        $("#Edate").val('');
        $("#curpageidx").val('');
        $(this).attr("disabled", true);
        LoadData($(this));
    });
});
function LoadData(buttonid=null,sortby=null,sortOrder=null) {
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
            sortBy: sortby,
            sortOrder: sortOrder
        }
    })
        .done(function (response) {
            console.log(response);
            console.log("success");
            $(".datadiv").html(response);
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
    LoadData(buttonid = null, sortby = id, sortOrder = $('#'+id).attr("val"));
}
function Goto(index) {
    document.getElementById("curpageidx").value = index;
    LoadData();

}