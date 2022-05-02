class  IEventItemAdd {
    Btn_ClickBatchSerail(index,table) {
        var data = table.row(index).data();
        if (data.ManageItem == "S") {
            $("#ItemCodeSerial").val(data.ItemCode);
            $("#ItemCodeSerialQty").val(data.Quantity);
            $("#txtRowID").val(index);
            tbSerial.clear();
            tbSerial.rows.add(data.Serial);
            tbSerial.search('').draw();
            $("#SerialNumber").val("");
            $("#txtManfrSerial").val("");
            $("#txtExpireDate").val("");
            $("#ModalBarCodeSerail").modal("show");
        } else if (data.ManageItem == "B") {
            alert("batch");
        }
    }
    BtnSaveSerial_ClickSave1() {
        var serial = {};
        serial.SerialNumber = $("#SerialNumber").val();
        serial.MfrSerialNo = $("#txtManfrSerial").val();
        serial.ExpDate = $("#txtExpireDate").val();
        var index = $("#txtRowID").val();
        LinesAR[index].Serial.push(serial);
        lsSerial.push(serial);
        tbSerial.clear();
        tbSerial.rows.add(lsSerial);
        tbSerial.search('').draw();
        $("#SerialNumber").val("");
        $("#txtManfrSerial").val("");
        $("#txtExpireDate").val("");
    }
    GenerateSerialOrBatch(url,itemCode, qty) {
        $.ajax({
            url: url,
            type: "POST",
            data: { objectCode: objectCode, dateOfMonth: dateOfMonth },
            dataType: "JSON",
            success: function (data) {
                $("#SeriesID").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#SeriesID").append('<option value="' + data[i].code + '">' + data[i].name + '</option>');
                }
            },
            error: function (erro) {
                console.log(erro.responseText);
            }
        });
        return "Hello World";
    }
}