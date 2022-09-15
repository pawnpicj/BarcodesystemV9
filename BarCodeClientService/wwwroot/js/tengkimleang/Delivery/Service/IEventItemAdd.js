class IEventItemAdd {
    Btn_ClickBatchSerail(index, table) {
        const data = table.row(index).data();
        console.log(data);
        if (data.ManageItem === "S") {
            let k = 0;
            if (data.Serial.length !== 0) {
                for (let i = 0; i < data.Serial.length; i++) {
                    k = k + 1;
                }
            }
            console.log(data);
            $("#ItemCodeSerial").val(data.ItemCode);
            $("#ItemCodeSerialQty").val(data.Quantity - k);
            $("#txtRowID").val(index);
            tbSerial1.clear();
            tbSerial1.rows.add(data.Serial);
            tbSerial1.search("").draw();
            $("#SerialNumber").val("");
            $("#txtManfrSerial").val("");
            $("#txtExpireDate").val("");
            $("#ModalBarCodeSerail").modal("show");
        } else if (data.ManageItem == "B") {
            let k = 0;
            console.log(data);
            console.log(data.Batches.length);
            if (data.Batches.length !== 0) {
                for (let i = 0; i < data.Batches.length; i++) {
                    k = k + data.Batches[i].qty;
                }
            }
            console.log("HelloData");
            console.log(k);
            $("#ItemCodeBatch").val(data.ItemCode);
            $("#ItemCodeBatchQty").val(data.Quantity - k);
            $("#txtIDRowB").val(index);
            LBatch = data.Batches;
            tbBatch.clear();
            tbBatch.rows.add(LBatch);
            tbBatch.search("").draw();
            $("#ModalBarCodeBatch").modal("show");
        }
    }

    clearText(array,type) {
        if (type==="option") {
            for (var i = 0; i < array.length; i++) {
                $("#" + array[i].id+" option").remove();
            }
        } else if(type==="") {
            for (var i = 0; i < array.length; i++) {
                $("#" + array[i].id).val(array[i].value);
            }
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
        console.log(index);
        if (index !== "" && index !=="0") {
            LinesAR[index].Serial.push(serial);
            //lsSerial.push(serial);
        } else {
            objectLine.Serial.push(serial);
            //lsSerial.push(serial);
        }
        lsSerial.push(serial);
        //console.log(objectLine);
        $("#SerialNumber").val("");
        $("#txtManfrSerial").val("");
        $("#txtExpireDate").val("");
        $("#ItemCodeSerialQty").val(qty);
        tbSerial1.clear();
        tbSerial1.rows.add(lsSerial);
        tbSerial1.search("").draw();
    }

    GenerateSerialOrBatch(url, itemcode, qty, type) {
        var a = {};
        var obj = [];
        obj.push({
            "SerialFrom": 0,
            "SerialTo": 0,
            "MfrNo": "",
            "ExpireDate": "",
            "itemCode": itemcode,
            "qty": qty,
            "TypeSerialGen": "1"
        });
        a.ListSerials = obj;
        $.ajax({
            url: url,
            type: "POST",
            dataType: "JSON",
            data: { generateSerialBatchRequest: a },
            success: function (data) {
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
            return ele.SerialNumber !== value;
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