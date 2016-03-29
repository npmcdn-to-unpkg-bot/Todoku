$(function () {
    //#region Datepicker
    $.datepicker.setDefaults({
        showOn: 'both',
        regional: 'id',
        changeMonth: true,
        changeYear: true,
        dateFormat: 'dd M yy'
    });
    $('.datepicker').each(function () {
        $(this).datepicker();
    });
    //#endregion

    $('.tabs').each(function () {
        $(this).tabs();
    });

    $('.txtNumber').number(true, 2, ',', '.');

    $('body').on('click', '.btnSearchZipCode', function () {
        var url = window.location.origin + "/Todoku/Ajax/GetListObject/";
        var buttons = {
            "Pilih": function () {
                var id = $('#hdnSelectedZipCode').val();
                GetListObject(url, 'GetZipCodeList', "ZipID = " + id, null, null, function (result) {
                    $('#ZipCode').val(result[0].ZipNumber);
                    $('#Province').val(result[0].Province);
                    $('#City').val(result[0].Regency);
                    var text = $('#Address').val() + " Kel. " + result[0].SubDistrict + " Kec. " + result[0].District;
                    $('#Address').val(text);
                });
                $('#hdnSelectedZipCode').val("");
                $(this).dialog("close");
            },
            "Batal": function () { $('#hdnSelectedZipCode').val(""); $(this).dialog("close"); }
        }
        var provinceVal = "SC0002.006";
        GetListObject(url, 'GetStandardCodeList', 'ParentID == "SC0002"', "", "StandardCodeName ASC", function (result) {
            var dtProvinces = result;
            $('#hdnDtProvince').val(result);
            GetListObject(url, 'GetZipCodeList', 'Province == "' + provinceVal + '"', "Regency", null, function (result) {
                var dtRegency = result;
                $('#hdnDtRegency').val(result);
                GetListObject(url, 'GetZipCodeList', 'Regency == "' + dtRegency[0].Regency + '"', "District", null, function (result) {
                    var dtDistrict = result;
                    GetListObject(url, 'GetZipCodeList', 'District == "' + dtDistrict[0].District + '"', null, null, function (result) {
                        var data = { combobox: dtProvinces, ProvinceVal: provinceVal, cboRegency: dtRegency, RegencyVal: "", cboDistrict: dtDistrict, DistrictVal: "", lstZipCode: result };
                        OpenDialog({
                            DialogTitle: "Kode Pos",
                            DialogID: "ZipCodeDialog",
                            Data: data,
                            Template: "ZipCodeTemplate",
                            buttons: buttons
                        });
                    });
                });
            })
        });
    });

    $('body').on('change', '.cboProvince', function () {
        var url = window.location.origin + "/Todoku/Ajax/GetListObject/";
        var provinceVal = $(this).val();
        GetListObject(url, 'GetStandardCodeList', 'ParentID == "SC0002"', "", "StandardCodeName ASC", function (result) {
            var dtProvinces = result;
            GetListObject(url, 'GetZipCodeList', 'Province == "' + provinceVal + '"', "Regency", null, function (result) {
                var dtRegency = result;
                GetListObject(url, 'GetZipCodeList', 'Regency == "' + dtRegency[0].Regency + '" && Province == "' + provinceVal + '"', "District", null, function (result) {
                    var dtDistrict = result;
                    GetListObject(url, 'GetZipCodeList', 'District == "' + dtDistrict[0].District + '" && Regency == "' + dtRegency[0].Regency + '" && Province == "' + provinceVal + '"', null, null, function (result) {
                        var data = { combobox: dtProvinces, ProvinceVal: provinceVal, cboRegency: dtRegency, RegencyVal: dtRegency[0].Regency, cboDistrict: dtDistrict, DistrictVal: "", lstZipCode: result };
                        $('#ZipCodeDialog').text(""); // empty dialog content
                        $('#ZipCodeTemplate').tmpl(data).appendTo('#ZipCodeDialog');
                    });
                });
            })
        });
    });

    $('body').on('change', '.cboRegency', function () {
        var url = window.location.origin + "/Todoku/Ajax/GetListObject/";
        var provinceVal = $(this).closest('table').find('.cboProvince').val();
        var regencyVal = $(this).val();
        GetListObject(url, 'GetStandardCodeList', 'ParentID == "SC0002"', "", "StandardCodeName ASC", function (result) {
            var dtProvinces = result;
            GetListObject(url, 'GetZipCodeList', 'Province == "' + provinceVal + '"', "Regency", null, function (result) {
                var dtRegency = result;
                GetListObject(url, 'GetZipCodeList', 'Regency == "' + regencyVal + '" && Province == "' + provinceVal + '"', "District", null, function (result) {
                    var dtDistrict = result;
                    GetListObject(url, 'GetZipCodeList', 'District == "' + dtDistrict[0].District + '" && Regency == "' + regencyVal + '" && Province == "' + provinceVal + '"', null, null, function (result) {
                        var data = { combobox: dtProvinces, ProvinceVal: provinceVal, cboRegency: dtRegency, RegencyVal: regencyVal, cboDistrict: dtDistrict, DistrictVal: "", lstZipCode: result };
                        $('#ZipCodeDialog').text(""); // empty dialog content
                        $('#ZipCodeTemplate').tmpl(data).appendTo('#ZipCodeDialog');
                    });
                });
            })
        });
    });

    $('body').on('change', '.cboDistrict', function () {
        var url = window.location.origin + "/Todoku/Ajax/GetListObject/";
        var provinceVal = $(this).closest('table').find('.cboProvince').val();
        var regencyVal = $(this).closest('table').find('.cboRegency').val();
        var districtVal = $(this).val();
        GetListObject(url, 'GetStandardCodeList', 'ParentID == "SC0002"', "", "StandardCodeName ASC", function (result) {
            var dtProvinces = result;
            GetListObject(url, 'GetZipCodeList', 'Province == "' + provinceVal + '"', "Regency", null, function (result) {
                var dtRegency = result;
                GetListObject(url, 'GetZipCodeList', 'Regency == "' + regencyVal + '" && Province == "' + provinceVal + '"', "District", null, function (result) {
                    var dtDistrict = result;
                    GetListObject(url, 'GetZipCodeList', 'District == "' + districtVal + '" && Regency == "' + regencyVal + '" && Province == "' + provinceVal + '"', null, null, function (result) {
                        var data = { combobox: dtProvinces, ProvinceVal: provinceVal, cboRegency: dtRegency, RegencyVal: regencyVal, cboDistrict: dtDistrict, DistrictVal: districtVal, lstZipCode: result };
                        $('#ZipCodeDialog').text(""); // empty dialog content
                        $('#ZipCodeTemplate').tmpl(data).appendTo('#ZipCodeDialog');
                    });
                });
            })
        });
    });

    $('body').on('click', 'table.SelectMode tr', function () {
        $('table.SelectMode tr').removeClass("selected");
        $(this).addClass("selected");
        var id = $(this).find('.keyField').val();
        $('#hdnSelectedZipCode').val(id);
    });
})

function GetListObject(url, method, filterExpression, groupBy, orderBy, funcHandle) {
    var data = "{'method' : '" + method + "', 'filterExpression' : '" + filterExpression + "'";
    if (groupBy != null) {
        data += ", 'GroupBy': '" + groupBy + "'";
    }
    if (orderBy != null) {
        data += ", 'OrderBy' : '" + orderBy + "'";
    }
    data += "}";
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        data: data,
        contentType: "application/json; charset=utf-8",
        success : funcHandle,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            var myWindow = window.open("", "MsgWindow", "width=200, height=100");
            myWindow.document.write("XMLHttpRequest=" + XMLHttpRequest.responseText + "\ntextStatus=" + textStatus + "\nerrorThrown=" + errorThrown);
            return "";
        }
    });
}

function OpenDialog(prop) {
    var buttons;
    var width = "";
    var height = "";
    var Template = "";
    var close;
    var dialogID = "";

    if (typeof prop.width === "undefined") {
        width = 1000;
    }
    else {
        width = prop.width;
    }

    if (typeof prop.height === "undefined") {
        height = width / 2;
    }
    else {
        height = prop.height;
    }

    if (typeof prop.buttons === "undefined") {
        buttons: [{}];
    }
    else {
        buttons = prop.buttons;
    }

    if (typeof prop.Template === "undefined") {
        Template = "dialogTemplate";
    } else {
        Template = prop.Template;
    }

    if (typeof prop.close === "undefined") {
        close = function (event, ui) { }
    } else {
        close = prop.close;
    }

    if (typeof prop.DialogID === "undefined") {
        dialogID = "dialog";
    } else {
        dialogID = prop.DialogID;
    }

    $('#' + dialogID).dialog({
        autoOpen: false,
        width: width,
        height: height,
        buttons: buttons,
        open: function (event, ui) {
            $('#' + dialogID).dialog('option', 'title', $(this).data('dialogTitle'));
            $('#' + dialogID).text(""); // empty dialog content
            $('#' + Template).tmpl($(this).data('data')).appendTo('#' + dialogID);
        },
        close: close
    });

    $('#' + dialogID).data('dialogTitle', prop.DialogTitle).data('data', prop.Data).dialog("open");
}

function OnSaveData(url, data, func) {
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: window.JSON.stringify(data),
        success: func,
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log("XMLHttpRequest=" + XMLHttpRequest.responseText + "\ntextStatus=" + textStatus + "\nerrorThrown=" + errorThrown);
            alert(XMLHttpRequest.responseText);
        }
    });
}