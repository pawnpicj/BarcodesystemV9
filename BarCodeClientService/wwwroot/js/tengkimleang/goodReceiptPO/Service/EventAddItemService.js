let EventItemAdd = {
    iEventItemAdd: new IEventItemAdd(),
    Btn_ClickBatchSerail(index, id) {
        EventItemAdd.iEventItemAdd.Btn_ClickBatchSerail(index, id);
    },
    BtnSaveSerial() {
        EventItemAdd.iEventItemAdd.BtnSaveSerial_ClickSave1();
    },
    GenerateSerialOrBatch(url, itemcode, qty, type) {
        EventItemAdd.iEventItemAdd.GenerateSerialOrBatch(url, itemcode, qty, type);
    },
    DeleteArray(arr, value1) {
        let tmpArr = EventItemAdd.iEventItemAdd.DeleteSerial(arr, value1);
        console.log(tmpArr);
        if ($("#txtRowID").val() === "" || $("#txtRowID").val() === "0") {
            var qty = parseInt($("#ItemCodeSerialQty").val());
            qty = qty + 1;
            $("#ItemCodeSerialQty").val(qty);
            objectLine.Serial = tmpArr;
        } else {
            const data = tbItemLine.row($("#txtRowID").val()).data();
            let k = 0;
            if (data.Serial.length !== 0) {
                for (let i = 0; i < data.Serial.length; i++) {
                    k = k + 1;
                }
            }
            var qty = parseInt(data.Quantity - k);
            $("#ItemCodeSerialQty").val(qty + 1);
            LinesAR[$("#txtRowID").val()].Serial = tmpArr;
        }
        lsSerial = tmpArr;
        tbSerial1.clear();
        tbSerial1.rows.add(lsSerial);
        tbSerial1.search("").draw();
    },
    getItemCode(url) {
        EventItemAdd.iEventItemAdd.getItemCode(url);
    },
    clearText(array, type = "") {
        EventItemAdd.iEventItemAdd.clearText(array,type);
    }
}