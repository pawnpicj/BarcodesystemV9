﻿class GetMasterGoodReceiptPO {
    getSeries(dateOfMonth, currentDate, url, objectCode) {
        if (dateOfMonth == "") {
            dateOfMonth = formatDate(currentDate);
        }
        $.ajax({
            url: url,
            type: "GET",
            data: { objectCode: objectCode, dateOfMonth: dateOfMonth },
            dataType: "JSON",
            success: function(data) {
                $("#SeriesID").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#SeriesID").append('<option value="' + data[i].code + '">' + data[i].name + "</option>");
                }
            },
            error: function(erro) {
                console.log(erro.responseText);
            }
        });
    }

    getSaleEmployee(url) {
        $.ajax({
            url: url,
            type: "GET",
            dataType: "JSON",
            success: function(data) {
                $("#SaleEmp").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#SaleEmp").append('<option value="' + data[i].code + '">' + data[i].name + "</option>");
                }
            },
            error: function(erro) {
                console.log(erro.responseText);
            }
        });
    }

    getCurrency(url, cardCode) {
        $.ajax({
            url: url,
            type: "GET",
            dataType: "JSON",
            data: { cardCode: cardCode },
            success: function(data) {
                $("#BPDocCurr").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#BPDocCurr").append("<option value='" + data[i].code + "'>" + data[i].name + "</option>");
                }
                $("#DocCurr").val($("#BPDocCurr").val());
            }
        });
    }

    getCustomer(url) {
        $.ajax({
            url: url,
            type: "GET",
            dataType: "JSON",
            success: function(data) {
                LCus = data;
                tbCus.clear();
                tbCus.rows.add(LCus);
                tbCus.search("").columns().search("").draw();
            },
            error: function(erro) {
                console.log(erro.responseText);
            }
        });
    }

    getPurchaseOrder(url, cardCode) {
        $.ajax({
            url: url,
            type: "GET",
            data: { CardCode: cardCode },
            dataType: "JSON",
            success: function(data) {
                LCopyFromPO = data;
                TbCopyFromPO.clear();
                TbCopyFromPO.rows.add(LCopyFromPO);
                TbCopyFromPO.search("").columns().search("").draw();
            },
            error: function(erro) {
                console.log(erro.responseText);
            }
        });
    }
    getTaxCode(url) {
        $.ajax({
            url: url,
            type: "GET",
            dataType: "JSON",
            success: function (data) {
                $("#TaxCode").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#TaxCode").append("<option value='" + data[i].code + "'>" + data[i].name + "</option>");
                }
            }
        });
    }
    getWarehouse(url) {
        $.ajax({
            url: url,
            type: "GET",
            dataType: "JSON",
            success: function (data) {
                $("#whscode").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#whscode").append("<option value='" + data[i].code + "'>" + data[i].name + "</option>");
                }
            }
        });
    }
    getUomCode(url, ItemCode) {
        $.ajax({
            url: url,
            type: "GET",
            data: { ItemCode: ItemCode },
            dataType: "JSON",
            success: function (data) {
                $("#UomID").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#UomID").append("<option value='" + data[i].code + "'>" + data[i].code +"-"+ data[i].name + "</option>");
                }
            }
        });
    }
}