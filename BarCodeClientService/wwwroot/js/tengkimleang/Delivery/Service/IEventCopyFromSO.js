class IEventCopyFromPO {

    ChoosePO_Click(temporyListPO, TbCopyFromPO) {
        var remark = "";
        var doctotal = 0;
        var discountPercentage = 0;
        var discountAmount = 0;
        var discountLine = 0;
        var lineTotal = 0;
        var LinesAR = [];
        for (var i = 0; i < temporyListPO.length; i++) {
            var data = TbCopyFromPO.row(temporyListPO[i]).data();
            remark = remark + data.docNum + ". ";
            doctotal = doctotal + data.docTotal;
            discountPercentage = data.discPrcnt;
            discountAmount = data.discountAMT;
            for (var k = 0; k < data.line.length; k++) {
                var objLineItem = {};
                discountLine = discountLine + data.line[k].discountAMT;
                lineTotal = lineTotal + data.line[k].lineTotal;
                objLineItem.ItemCode = data.line[k].itemCode;
                objLineItem.UnitPrice = data.line[k].price;
                objLineItem.Discount = data.line[k].discPrcnt;
                objLineItem.DiscountAmount = data.line[k].discountAMT;
                objLineItem.Quantity = data.line[k].quatity;
                objLineItem.PriceBeforeDis = data.line[k].priceBeforeDis;
                objLineItem.LineTotal = data.line[k].lineTotal;
                objLineItem.UomName = data.line[k].uomName;
                objLineItem.TaxCode = data.line[k].taxCode;
                objLineItem.Whs = data.line[k].whsCode;
                objLineItem.DocEntry = data.docEntry;
                objLineItem.LineNum = data.line[k].lineNum;
                objLineItem.ManageItem = data.line[k].manageItem;
                objLineItem.Serial = [];
                objLineItem.Batches = [];
                LinesAR.push(objLineItem);
            }
        }
        let responseBack = {};
        responseBack.remark = remark;
        responseBack.docTotal = doctotal;
        responseBack.discountPercentage = discountPercentage;
        responseBack.discountAmount = discountAmount;
        responseBack.discountLine = discountLine;
        responseBack.lineTotal = lineTotal;
        responseBack.linesAR = LinesAR;
        return responseBack;
    }
}