$('#addRow01').on('click', function () {

    var xmodule = "";
    xmodule = $('#xmodule').val();

    var showBinLocation = "";
    showBinLocation = $('#txtGetBinLocation').val();

    //var iurl;
    var idata = {};

    if ($('#txtScanBarCode01').val() != '')
    {
        var txtScanBarCode = document.getElementById("txtScanBarCode01").value;
        const strArr = txtScanBarCode.split(";");
        //document.getElementById("demo").innerHTML = StrBarCodeArr[4];
        var sItemCode = strArr[0];
        var sBatch = strArr[1];
        var sSerial = strArr[2];
        var sArr3 = strArr[3];
        var sQty;
        let lcase1 = sArr3.toLowerCase();
        let iQTY = lcase1.slice(0, 3);
        var sWarehouse = "";
        var sBatchSerial = "";
        let sBinEntry = 0;

        if (iQTY != "qty") {
            sQty = document.getElementById("txtQuantity01").value;
            sQty = parseFloat(sQty);
        } else if (iQTY == "qty") {
            sQty = sArr3.substring(3, 9);
            sQty = parseFloat(sQty);
            if (isNaN(sQty)) {
                sQty = document.getElementById("txtQuantity01").value;
            } else {
                sQty = 0;
            }
        }

        //Find Whs in Line (TbAR)
        for (let i = 0; i < tbItemLine.column(0).data().length; i++) {
            var data = tbItemLine.row(i).data();
            console.log(data);
            //console.log("itemCode = " + data.ItemCode + "==" + sItemCode);
            if (data.ItemCode == sItemCode) {
                //console.log('Whs : ' + data.Whs);
                sWarehouse = data.Whs;
            }
        }
        //=======================

        if (xmodule == "delivery") {
            console.log("Delivery Openning..");
            sBinEntry = showBinLocation;
            if (sBatch != "") {
                sBatchSerial = sBatch;
                if (sBinEntry != "") {
                    iurl = '@Url.Action("GetStockItemBatchBin", "Inventory")';
                    idata = { itemcode: sItemCode, batchnumber: sBatchSerial, binentry: sBinEntry };
                } else if (sBinEntry == "") {
                    iurl = '@Url.Action("GetStockItemBatchW", "Inventory")';
                    idata = { itemcode: sItemCode, batchnumber: sBatchSerial, whscode: sWarehouse };
                }
            }

        }

        //Case Batch Number
        if (sBatch != '') {
            sBatchSerial = sBatch;
            console.log(sItemCode + ";" + sBatchSerial + ";" + sBinEntry + ";");
            $.ajax({
                url: iurl,
                data: idata,
                type: "GET",
                dataType: "JSON",
                "dataSrc": 'data',
                success: function (data) {
                    var jqueryXml = $(data);
                    console.log(data);
                    if (jqueryXml[0].data.length != 0) {

                        var bItemCode = jqueryXml[0].data[0].itemCode;
                        var bItemName = jqueryXml[0].data[0].itemName;
                        var bQuantity = sQty;
                        var bWhsCode = jqueryXml[0].data[0].whsCode;
                        var bBinEntry = jqueryXml[0].data[0].binEntry;
                        var bBinCode = jqueryXml[0].data[0].binCode;
                        var bUOMCode = jqueryXml[0].data[0].uomCode;
                        var bBatchNumber = jqueryXml[0].data[0].batchNumber;
                        var bSerialNumber = jqueryXml[0].data[0].serialNumber;
                        var bTotalQuantity = jqueryXml[0].data[0].quantity;

                        //if (bSerialNumber != '') { bSerialNumber = bSerialNumber } else { bSerialNumber = '' }
                        var bExpDate = jqueryXml[0].data[0].expDate;
                        if (bExpDate == '1899-12-30T00:00:00') {
                            bExpDate = '';
                        } else {
                            bExpDate = bExpDate;
                        }
                        t.row.add([
                            bItemCode,
                            bItemName,
                            bQuantity,
                            bWhsCode,
                            bBinEntry,
                            bBinCode,
                            bUOMCode,
                            bExpDate,
                            sBatchSerial,
                            ''
                        ]).draw(false);

                        $('#txtScanBarCode01').val("");
                        $('#txtScanBarCode01').focus();

                    } else {
                        alert("Batch Number does not exist.\r\nไม่มีเลข Batch ใน Bin Location นี้");

                        //function add item comfirm
                        funcAddItemManual('batch', sItemCode, sBatchSerial, sWhsCode)

                        $('#txtScanBarCode01').val("");
                        $('#txtScanBarCode01').focus();
                    }

                }

            });
        }

        $('#txtScanBarCode01').val("");
        $('#txtScanBarCode01').focus();

    }
});
$('#addRow01').click();

var input01 = document.getElementById("txtScanBarCode01");
input01.addEventListener("keyup", function (event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        document.getElementById("addRow01").click();
    }
});