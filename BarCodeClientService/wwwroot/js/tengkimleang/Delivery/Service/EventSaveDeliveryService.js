let EventSaveGoodReceiptPO = {
    iEventSaveGoodReceipt: new IEventSaveGoodReceipt(),
    validDateForm(array, condition) {
        var i = EventSaveGoodReceiptPO.iEventSaveGoodReceipt.validForm(array, condition);
        return i;
    },

    valiDateLine(array) {
        console.log(array);
        var i = EventSaveGoodReceiptPO.iEventSaveGoodReceipt.validLine(array);
        return i;
    },

    sendDelivery(url) {
        var validate = 1;
        validate = EventSaveGoodReceiptPO.validDateForm([
                { id: "CusID", value: "Please Enter Customer Code!" },
                { id: "SeriesID", value: "Please Select Series Code!" },
                { id: "DocDate", value: "Please Select DocDate !" },
                { id: "DocumentDate", value: "Please Select DocumentDate !" },
                { id: "BPDocCurr", value: "Please Select BP Currency!" }
            ],
            "");
        if (validate === 0) {
            console.log("Line Here");
            console.log(LinesAR);
            validate = EventSaveGoodReceiptPO.valiDateLine(LinesAR);
            if (validate === 0) {
                var sendGoodReceiptPO = {};
                sendGoodReceiptPO.CardCode = $("#CusID").val();
                sendGoodReceiptPO.Series = $("#SeriesID").val();
                sendGoodReceiptPO.DocDate = $("#DocDate").val();
                sendGoodReceiptPO.TaxDate = $("#DocumentDate").val();
                sendGoodReceiptPO.OrderNumber = $("#OrderNumberID").val();
                sendGoodReceiptPO.CurrencyCode = $("#BPDocCurr").val();
                sendGoodReceiptPO.SlpCode = $("#SaleEmp").val();
                sendGoodReceiptPO.Remark = $("#Remark").val();
                sendGoodReceiptPO.Lines = LinesAR;
                //console.log(sendGoodReceiptPO);
                $.ajax({
                    url: url,
                    type: "POST",
                    dataType: "JSON",
                    data: { sendDelivery: sendGoodReceiptPO },
                    success: function(data) {
                        console.log(data);
                        alert("Successfull");
                        document.getElementById("frmLoading").style.display = "none";
                        location.reload();
                        //$("#SerialNumber").val(data[0].serialAndBatch);
                        //$("#txtScriptID").val(data[0].script);
                    },
                    error: function (erro) {
                        console.log(erro.responseText);
                        alert("Internal Error");
                        document.getElementById("frmLoading").style.display = "none";
                    }
                });
            } else {
                document.getElementById("frmLoading").style.display = "none";
            }
        } else {
            document.getElementById("frmLoading").style.display = "none";
        }
    },
    LineDiscountPer() {
        EventSaveGoodReceiptPO.iEventSaveGoodReceipt.LineDiscountPer();
    },
    LinDiscountAMT() {
        EventSaveGoodReceiptPO.iEventSaveGoodReceipt.LinDiscountAMT();
    },
}