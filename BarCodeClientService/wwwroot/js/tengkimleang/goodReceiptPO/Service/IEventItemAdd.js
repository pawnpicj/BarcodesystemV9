class IEventItemAdd {
    Btn_ClickBatchSerail(index, table) {
        const data = table.row(index).data();
        if (data.ManageItem == "S") {
            console.log(data);
            let k = 0;
            if (data.Serial.length != 0) {
                for (let i = 0; i < data.Serial.length; i++) {
                    k = k + 1;
                }
            }
            $("#ItemCodeSerial").val(data.ItemCode);
            $("#ItemCodeSerialQty").val(data.Quantity - k);
            $("#txtRowID").val(index);
            //tbSerial1.clear();
            //tbSerial1.rows.add(data.Serial);
            //tbSerial1.search("").draw();
            $("#SerialNumber").val("");
            $("#txtManfrSerial").val("");
            $("#txtExpireDate").val("");
            $("#ModalBarCodeSerail").modal("show");
        } else if (data.ManageItem == "B") {
            alert("batch");
        }
    }

    BtnSaveSerial_ClickSave1() {
        const serial = {};
        const qty = $("#ItemCodeSerialQty").val() - 1;
        serial.SerialNumber = $("#SerialNumber").val();
        serial.MfrSerialNo = $("#txtManfrSerial").val();
        serial.ExpDate = $("#txtExpireDate").val();
        serial.Script = $("#txtScriptID").val();
        const index = $("#txtRowID").val();
        LinesAR[index].Serial.push(serial);
        lsSerial.push(serial);
        $("#SerialNumber").val("");
        $("#txtManfrSerial").val("");
        $("#txtExpireDate").val("");
        $("#ItemCodeSerialQty").val(qty);
        //tbSerial1.clear();
        //tbSerial1.rows.add(lsSerial);
        //tbSerial1.search("").draw();
    }

    GenerateSerialOrBatch(url, itemcode, qty, type) {
        const a = {};
        a.itemCode = itemcode;
        a.qty = qty;
        $.ajax({
            url: url,
            type: "POST",
            dataType: "JSON",
            data: { generateSerialBatchRequest: a },
            success: function(data) {
                $("#SerialNumber").val(data[0].serialAndBatch);
                $("#txtScriptID").val(data[0].script);
            },
            error: function(erro) {
                alert(erro.message);
            }
        });
    }

    DeleteSerial(arr, value) {
        let tmp = arr.filter(function (ele) {
            return ele.SerialNumber === value;
        });
        return tmp;
    }
    getItemCode(url) {
        $.ajax({
            url: url,
            type: "GET",
            dataType: "JSON",
            success: function (data) {
                LItm = data.data;
                console.log(LItm);
                tbItemSearch.clear();
                tbItemSearch.rows.add(LItm);
                tbItemSearch.search('').draw();
            },
            error: function (erro) {
                alert(erro.message);
            }
        });
    }
}