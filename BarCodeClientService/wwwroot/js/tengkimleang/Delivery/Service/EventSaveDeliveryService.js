let array = [];

let EventSaveGoodReceiptPO = {
    iEventSaveGoodReceipt: new IEventSaveGoodReceipt(),
    validDateForm(array, condition) {
        var i = EventSaveGoodReceiptPO.iEventSaveGoodReceipt.validForm(array, condition);
        return i;
    },

    valiDateLine(array) {        
        console.log("Array valiDateLine :-")
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
            //console.log("Line Here");
            //console.log(LinesAR);
            //Send to IEventSaveGoodReceipt.validLine
            validate = EventSaveGoodReceiptPO.valiDateLine(LinesAR);
            //console.log(validate);
            if (validate === 0) {

                dLine = [];
                dSubLine = {};
                //console.log(LinesAR);
                for (let x = 0; x < LinesAR.length; x++) {
                    dSubLine = {};
                    //console.log("Id :-" + x);
                    dSubLine.DocEntry = LinesAR[x].DocEntry;
                    dSubLine.LineNum = LinesAR[x].LineNum;
                    dSubLine.ItemCode = LinesAR[x].ItemCode;
                    dSubLine.Quantity = LinesAR[x].Quantity;
                    dSubLine.PriceBeforeDis = LinesAR[x].PriceBeforeDis;
                    dSubLine.Discount = LinesAR[x].Discount;
                    dSubLine.Whs = LinesAR[x].Whs;
                    dSubLine.Patient = LinesAR[x].Patient;
                    dSubLine.ManageItem = LinesAR[x].ManageItem;
                    dSubLine.YesNo = LinesAR[x].YesNo;

                    bLine = [];
                    bSubLine = {};
                    for (let b = 0; b < LinesAR[x].Batches.length; b++) {
                        bSubLine = {};
                        bSubLine.ItemCode = LinesAR[x].Batches[b].ItemCode;
                        bSubLine.qty = LinesAR[x].Batches[b].qty;
                        bSubLine.BatchNumber = LinesAR[x].Batches[b].BatchNumber;
                        bLine.push(bSubLine);
                    }

                    sLine = [];
                    sSubLine = {};
                    for (let s = 0; s < LinesAR[x].Serial.length; s++) {
                        sSubLine = {};
                        sSubLine.ItemCode = LinesAR[x].Serial[s].ItemCode;
                        sSubLine.qty = LinesAR[x].Serial[s].qty;
                        sSubLine.SerialNumber = LinesAR[x].Serial[s].SerialNumber;
                        sLine.push(sSubLine);
                    }

                    dSubLine.Batches = bLine;
                    dSubLine.Serial = sLine;
                    dLine.push(dSubLine);
                }
                console.log(dLine);

                var sendGoodReceiptPO = {};
                sendGoodReceiptPO.CardCode = $("#CusID").val();
                sendGoodReceiptPO.Series = $("#SeriesID").val();
                sendGoodReceiptPO.DocDate = $("#DocDate").val();
                sendGoodReceiptPO.TaxDate = $("#DocumentDate").val();
                sendGoodReceiptPO.OrderNumber = $("#OrderNumberID").val();
                sendGoodReceiptPO.CurrencyCode = $("#BPDocCurr").val();
                sendGoodReceiptPO.SlpCode = $("#txtSlpCode").val();
                sendGoodReceiptPO.Remark = $("#Remark").val();
                sendGoodReceiptPO.Lines = dLine;
                console.log("Data for SAP ->");
                console.log(sendGoodReceiptPO);
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
                        alert("Internal Error -> " + erro.responseText);
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