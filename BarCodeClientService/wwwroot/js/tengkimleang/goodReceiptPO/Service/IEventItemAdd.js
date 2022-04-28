class  IEventItemAdd {
    Btn_ClickBatchSerail(index,table) {
        var data = table.row(index).data();
        console.log(data);
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
    GenerateSerialOrBatch(itemCode, qty) {
        var month = new Date().getMonth();
        if (month.length == 1) {
            month = "0" + month;
        }
        return  itemCode
                + qty
                + new Date().getFullYear()
                + month
                + new Date().getDate()
                + new Date().getDay()
                + new Date().getHours()
                + new Date().getMinutes()
                + new Date().getSeconds()
                + new Date().getMilliseconds();
    }
}