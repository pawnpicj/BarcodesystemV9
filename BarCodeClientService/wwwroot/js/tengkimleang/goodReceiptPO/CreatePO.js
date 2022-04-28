﻿
let GetMasterData ={
    overallFunction: new GetMasterGoodReceiptPO(),
    getSeries(DocDate, currentDate, url, ObjectCode) {
        GetMasterData.overallFunction.getSeries(DocDate, currentDate, url, ObjectCode);
    },
    getSaleEmployee(url) {
        GetMasterData.overallFunction.getSaleEmployee(url);
    },
    getCurrency(url, cardCode) {
        GetMasterData.overallFunction.getCurrency(url, cardCode);
    },
    getCustomer(url) {
        GetMasterData.overallFunction.getCustomer(url);
    },
    getPurchaseOrder(url,cardCode) {
        GetMasterData.overallFunction.getPurchaseOrder(url,cardCode)
    }
}

let DataTableInit ={
    TableItemLine() {
        $('#TbAR').dataTable({
            bLengthChange: false,
            bFilter: false,
            bInfo: false,
            bPaginate: false,
            data: LinesAR,
            columns:
                [
                    { data: "ItemCode", autoWidth: true },
                    {
                        data: "UnitPrice", autoWidth: true
                        //render: function (data, type, full, meta) { return '<input type="number" class="clsinput" style="padding:0px; position:absolute;width:100px;border:none;" onchange="PriceChange(' + meta.row + ')" id="tbPrice' + meta.row + '" value="' + full.UnitPrice + '">'; }, autoWidth: true
                    },
                    {
                        data: "Discount", autoWidth: true
                        //render: function (data, type, full, meta) { return '<input type="number" class="clsinput" style="padding:0px; position:absolute;width:100px;border:none;" onchange="DisChange('+meta.row+')" value="'+full.Discount+'" id="tbDis'+meta.row+'" >'; }, autoWidth: true
                    },
                    {
                        data: "DiscountAmount", autoWidth: true
                        //render: function (data, type, full, meta) { return '<input type="number" class="clsinput" style="padding:0px; position:absolute;width:100px;border:none;" onchange="DisamtChange('+meta.row+')" value="'+full.DiscountAmount+'" id="tbDisamt'+meta.row+'" >'; }, autoWidth: true
                    },
                    {
                        data: "Quantity", autoWidth: true
                        //render: function (data, type, full, meta) { return '<input type="number" class="clsinput" style="padding:0px; position:absolute;width:40px;border:none;" onchange="QtyChange(' + meta.row + ')" id="tbQty' + meta.row + '" value="' + full.Quantity + '" readonly=true; >'; }, autoWidth: true
                    },
                    { data: "PriceBeforeDis", autoWidth: true },
                    { data: "LineTotal", autoWidth: true },
                    { data: "UomName", autoWidth: true },
                    { data: "TaxCode", autoWidth: true },
                    { data: "Whs", autoWidth: true },
                    {
                        render: function (data, type, full, meta) {
                            if (full.ManageItem === "N") {
                                return "";
                            }
                            return '<button class="btn-sm btn-info" style="margin-left: 40%;" onClick="EventItemAdd.Btn_ClickBatchSerail(\'' + meta.row + '\',tbItemLine)"><i class="fas fa-barcode"></i></button>';
                        }
                    },
                    {
                        render: function (data, type, full, meta) { return '<button class="btn-sm btn-danger" style="margin-left: 40%;" onClick="Remove(' + meta.row + ')"><i class="fas fa-trash-alt"></i></button>'; }
                    }
                ],
            rowCallback: function (row, data, index) {
                //$('td', row).css('background-color', '#ffffff');
                //$('td', row).css('color', '#717171');
            }
        });
    },
    TableSearchItemCode() {
        $('#TbItem').DataTable({
            responsive: true,
            bLengthChange: false,
            binfo: false,
            data: LItm,
            columns: [
                { data: "itemCode", autoWidth: true },
                { data: "itemName", autoWidth: true },
                { data: "price", autoWidth: true },
                { data: "onHand", autoWidth: true },
                { data: "bcdCode", autoWidth: true }
            ],
            rowCallback: function (row, data, index) {
                //$('td', row).css('background-color', '#ffffff');
                //$('td', row).css('color', '#717171');
            }
        });
    },
    TablePurchaseOrder() {
        $('#TbCopyFromPO').DataTable({

            responsive: true,
            bLengthChange: false,
            binfo: false,
            data: LCopyFromPO,
            columns: [
                { data: "docNum", autoWidth: true },
                { data: "cardName", autoWidth: true },
                { data: "docDate", autoWidth: true },
                { data: "docTotal", autoWidth: true },
                { data: "docStatus", autoWidth: true }
            ],
            rowCallback: function (row, data, index) {
            }
        });
    },
    TableCustomer() {
        $('#TbCusCode').DataTable({
            responsive: true,
            bLengthChange: false,
            binfo: false,
            data: LCus,
            columns: [
                { data: "cardCode", autoWidth: true },
                { data: "cardName", autoWidth: true },
                { data: "phone", autoWidth: true },
                { data: "address", autoWidth: true }
            ],
            rowCallback: function (row, data, index) {
                //$('td', row).css('background-color', '#ffffff');
                //$('td', row).css('color', '#717171');
            }
        });
    },
    TableSerial1() {
        $('#TbSerial').DataTable({
            responsive: true,
            bLengthChange: false,
            binfo: false,
            data: lsSerial,
            columns:
            [
                { data: "SerialNumber", autoWidth: true },
                {
                    data: "MfrSerialNo", autoWidth: true
                },
                {
                    data: "ExpDate", autoWidth: true
                },
                {
                    render: function (data, type, full, meta) {

                        return ''; //'<button class="btn-sm btn-info" style="margin-left: 40%;" onClick="EventItemAdd.Btn_ClickBatchSerail(\'' + meta.row + '\',tbItemLine)"><i class="fas fa-barcode"></i></button>';
                    }
                }
            ],
            rowCallback: function (row, data, index) {
                //$('td', row).css('background-color', '#ffffff');
                //$('td', row).css('color', '#717171');
            }
        });
    }
}

let DataMethod ={
    method: new DataTableMethod(), 
    AddClassSelected(id,table)
    {
        DataMethod.method.AddSelectClass(id,table);
    },
    arrayRemove(arr, value) {
        let arr1 = arr.filter(function (ele) {
            return ele != value;
        });
        return arr1;
    }
}

