class IEventSaveGoodReceipt {
    messageBox(msg) {
        alert(msg);
    }
    validForm(array, condition) {
        for (var i = 0; i < array.length; i++) {
            if ($("#" + array[i].id).val() === condition || $("#" + array[i].id).val() === null) {
                EventSaveGoodReceiptPO.iEventSaveGoodReceipt.messageBox(array[i].value);
                return 1;
            }
        }
        return 0;
    }
    validLine(array) {
        if (array.length == 0) {
            EventSaveGoodReceiptPO.iEventSaveGoodReceipt.messageBox("PLease Input Item Before Send Data!");
            return 1;
        }
        for (var i = 0; i < array.length; i++) {
            if (array[i].ManageItem === "S") {
                var qty = 0;
                for (var arr in array[i].Serial) {
                    qty = qty + 1;
                }
                if (array[i].Quantity !== qty) {
                    EventSaveGoodReceiptPO.iEventSaveGoodReceipt.messageBox(
                        "Please Input Serial number in ItemCode line" + (i + 1));
                    return 1;
                }
            } else if(array[i].ManageItem==="B") {
                var qty = 0;
                for (var arr in array[i].Batch) {
                    qty = qty + arr.Qty;
                }
                if (array[i].Quantity !== qty) {
                    EventSaveGoodReceiptPO.iEventSaveGoodReceipt.messageBox(
                        "Please Input Batch number in ItemCode line" + (i + 1));
                    return 1;
                }
            }
        }
        return 0;
    }
}