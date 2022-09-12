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
        console.log(array);
        if (array.length == 0) {
            EventSaveGoodReceiptPO.iEventSaveGoodReceipt.messageBox("PLease Input Item Before Send Data!");
            return 1;
        }
        for (var i = 0; i < array.length; i++) {
            console.log(array[i].ManageItem);
            if (array[i].ManageItem === "S") {
                var qty = 0;
                for (var arr in array[i].Serial) {
                    qty = qty + 1;
                }
                if (parseInt(array[i].Quantity) !== qty) {
                    EventSaveGoodReceiptPO.iEventSaveGoodReceipt.messageBox(
                        "Please Input Serial number in ItemCode line" + (i + 1));
                    return 1;
                }
            } else if (array[i].ManageItem === "B") {
                console.log(array[i].Batches);
                var qty = 0;
                for (var k = 0; k < array[i].Batches.length; k++) {
                    console.log(array[i].Batches[k].qty);
                    qty = qty + parseFloat(array[i].Batches[k].qty);
                }
                console.log(qty);
                if (parseInt(array[i].Quantity) !== qty) {
                    EventSaveGoodReceiptPO.iEventSaveGoodReceipt.messageBox(
                        "Please Input Batch number in ItemCode line" + (i + 1));
                    return 1;
                }
            }
        }
        return 0;
    }
    LineDiscountPer() {
        //var discountPercentage = $("#Discount").val();
        //var totalLine = $("#totalItem").val();
        //var totalAfterDis = totalLine * (discountPercentage / 100);
        //var totalResult = totalLine - totalAfterDis;
        //$("#totalItem").val(totalResult);
        //$("#totalItem").val(totalResult);
        var x = Number($("#Price").val()) * Number($("#Qty").val());
        if (Number($("#Discount").val()) > 100) {
            $("#Discount").val(100);
            var z = (Math.round(Math.round(x * 1000) / 10) / 100);
            $("#DiscountAmt").val(z.toFixed(2));
            $("#totalItem").val(z.toFixed(2));
        } else if (Number($("#Discount").val()) < 0) {
            $("#Discount").val(0);
            $("#DiscountAmt").val(0);
            $("#totalItem").val(0);
        } else {
            var y = x * (Number($("#Discount").val() / 100));
            var z = (Math.round(Math.round(y * 1000) / 10) / 100);
            $("#DiscountAmt").val(z.toFixed(2));
            $("#totalItem").val((x - z).toFixed(2));
        }
    }
    LinDiscountAMT() {
        var x = Number($("#Price").val()) * Number($("#Qty").val())
        if (Number($("#DiscountAmt").val()) > x) {
            $("#Discount").val(100)
            var z = Math.round(Math.round(x * 1000) / 10) / 100;
            $("#DiscountAmt").val(z.toFixed(2));
            $("#totalItem").val(z.toFixed(2));
        } else if (Number($("#DiscountAmt").val()) < 0) {
            $("#Discount").val(0);
            $("#DiscountAmt").val(0);
            $("#totalItem").val(0);
        } else {
            var y = (Number($("#DiscountAmt").val()) * 100) / x;
            $("#Discount").val(y.toFixed(2)); //Math.round(Math.round(y * 1000) / 10) / 100
            $("#totalItem").val((x - $("#DiscountAmt").val()).toFixed(2));
        }
    }
}