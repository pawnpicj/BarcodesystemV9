
let GetMasterData = {
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
    getCustomer(url, cusType) {
        GetMasterData.overallFunction.getCustomer(url,cusType);
    },
    getPurchaseOrder(url, cardCode) {
        console.log("Hello");
        GetMasterData.overallFunction.getPurchaseOrder(url, cardCode);
    },
    getGoodReceiptPO(url, cardCode) {
        console.log("Hello");
        GetMasterData.overallFunction.getGoodReceiptPO(url, cardCode);
    },
    getTaxCode(url) {
        GetMasterData.overallFunction.getTaxCode(url);
    },
    getWarehouse(url) {
        GetMasterData.overallFunction.getWarehouse(url);
    },
    getUomCode(url, ItemCode, UOMType) {
        GetMasterData.overallFunction.getUomCode(url, ItemCode, UOMType);
    }
}

let DataTableInit = {
    TableItemLine() {
        $('#TbAR').dataTable({
            bLengthChange: false,
            bFilter: false,
            bInfo: false,
            bPaginate: false,
            data: LinesAR,
            columns:
                [
                    {
                        render: function (data, type, full, meta) {
                            if (full.ManageItem === "N") {
                                return "";
                            }
                            return '<button class="btn-sm btn-info" style="margin-left: 40%;" onClick="EventItemAdd.Btn_ClickBatchSerail(\'' + meta.row + '\',tbItemLine)"><i class="fas fa-barcode"></i></button>';
                        }
                    },
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
                { data: "unitPrice", autoWidth: true },
                { data: "quantity", autoWidth: true },
                { data: "barCode", autoWidth: true }
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
    TableGoodReturn() {
        $('#TbCopyToGoodReturn').DataTable({
            responsive: true,
            bLengthChange: false,
            binfo: false,
            data: LCopyToGoodReturn,
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
    TableSerialGen() {
        $('#TbSerial').dataTable({
            bLengthChange: false,
            bFilter: false,
            bInfo: false,
            bPaginate: false,
            data: lsSerial,
            columns:
                [
                    { data: "SerialNumber", autoWidth: true },
                    {
                        data: "MfrSerialNo",
                        autoWidth: true
                        // render: function (data, type, full, meta) { return '<input type="number" class="clsinput" style="padding:0px; position:absolute;width:100px;border:none;" onchange="PriceChange(' + meta.row + ')" id="tbPrice' + meta.row + '" value="' + full.UnitPrice + '">'; }, autoWidth: true
                    },
                    {
                        data: "ExpDate",
                        autoWidth: true
                        //render: function (data, type, full, meta) { return '<input type="number" class="clsinput" style="padding:0px; position:absolute;width:100px;border:none;" onchange="DisChange('+meta.row+')" value="'+full.Discount+'" id="tbDis'+meta.row+'" >'; }, autoWidth: true
                    },
                    {
                        render: function (data, type, full, meta) {
                            return '<button class="btn-sm btn-danger" style="margin-left: 40%;" onClick="EventItemAdd.DeleteArray(lsSerial,\'' + full.SerialNumber + '\')"><i class="fas fa-trash-alt"></i></button>';
                        }
                    }
                ],
            rowCallback: function (row, data, index) {
                //$('td', row).css('background-color', '#ffffff');
                //$('td', row).css('color', '#717171');
            }
        });
    },
    TableBatch() {
        $('#TbBatch').DataTable({
            responsive: true,
            bLengthChange: false,
            binfo: false,
            data: LBatch,
            columns: [
                { data: "itemCode", autoWidth: true },
                { data: "qty", autoWidth: true },
                { data: "serialAndBatch", autoWidth: true },
                { data: "mfrDate", autoWidth: true },
                { data: "expirationDate", autoWidth: true },
                { data: "admissionDate", autoWidth: true },
                {
                    render: function (data, type, full, meta) {
                        return '<button class="btn-sm btn-danger" style="margin-left: 40%;" onClick="DeleteBatch(LBatch,\'' + full.serialAndBatch + '\')"><i class="fas fa-trash-alt"></i></button>';
                    }
                }
            ],
            rowCallback: function (row, data, index) {
                //$('td', row).css('background-color', '#ffffff');
                //$('td', row).css('color', '#717171');
            }
        });
    },
    TabletmpBatchFromTo() {
        $('#tmpBatchFromTo').DataTable({
            responsive: true,
            bLengthChange: false,
            binfo: false,
            data: LtmpBatch,
            columns: [
                { data: "BatchFrom", autoWidth: true },
                { data: "BatchTo", autoWidth: true },
                { data: "ExpirationDate", autoWidth: true },
                { data: "MfrDate", autoWidth: true },
                { data: "AdmissionDate", autoWidth: true },
                {
                    render: function (data, type, full, meta) {
                        return '<button class="btn-sm btn-danger" style="margin-left: 40%;" onClick="DeleteTmpBatch(LtmpBatch,\'' + full.BatchFrom + '\',\'' + full.BatchTo + '\')"><i class="fas fa-trash-alt"></i></button>';
                    }
                }
            ],
            rowCallback: function (row, data, index) {
                //$('td', row).css('background-color', '#ffffff');
                //$('td', row).css('color', '#717171');
            }
        });
    },
    TableSerialFromTo() {
        $('#tmpSerial').DataTable({
            responsive: true,
            bLengthChange: false,
            binfo: false,
            data: LtmpSerial,
            columns: [
                { data: "SerialFrom", autoWidth: true },
                { data: "SerialTo", autoWidth: true },
                { data: "MfrNo", autoWidth: true },
                { data: "ExpireDate", autoWidth: true },
                {
                    render: function (data, type, full, meta) {
                        return '<button class="btn-sm btn-danger" style="margin-left: 40%;" onClick="DeleteTmpSerial(LtmpSerial,\'' + full.SerialFrom + '\',\'' + full.SerialTo + '\')"><i class="fas fa-trash-alt"></i></button>';
                    }
                }
            ],
            rowCallback: function (row, data, index) {
                //$('td', row).css('background-color', '#ffffff');
                //$('td', row).css('color', '#717171');
            }
        });
    },
    TableBarCode() {
        $('#TbBarCode').DataTable({
            responsive: true,
            bLengthChange: false,
            binfo: false,
            data: LBarCode,
            columns: [
                { data: "barCode", autoWidth: true },
                { data: "barCodeName", autoWidth: true },
                { data: "itemCode", autoWidth: true },
                { data: "itemName", autoWidth: true },
                { data: "uomCode", autoWidth: true },
                { data: "price", autoWidth: true },
                { data: "uomName", autoWidth: true, visible: false },
                { data: "manageItem", autoWidth: true, visible: false }
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

