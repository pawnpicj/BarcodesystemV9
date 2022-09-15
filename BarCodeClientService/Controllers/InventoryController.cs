using System;
using System.Diagnostics;
using BarCodeClientService.Models;
using BarCodeLibrary.APICall;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Bank;
using BarCodeLibrary.Respones.SAP.Tengkimleang;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarCodeClientService.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreateInventoryTransferBB()
        {
            return View();
        }

        public IActionResult CreateInventoryTransferRBB()
        {
            return View();
        }

        public IActionResult CreateInventoryCounting()
        {
            return View();
        }

        public IActionResult GetOWTQ()
        {
            var a = API.Read<ResponseGetOWTQ>("api/InventoryTransferRequest/GetIFR");
            return Ok(a);
        }

        public IActionResult GetOWTR()
        {
            var a = API.Read<ResponseGetOWTR>("api/InventoryTransferIM/GetIFIM");
            return Ok(a);
        }

        public IActionResult GetSaleEmployee()
        {
            var a = API.Read<ResponseOSLPGetSalesEmployee>("api/SaleEmployee");
            return Ok(a);
        }

        public IActionResult GetSeriesIM(string yymm, string typeSeries)
        {
            var yyyy = DateTime.Now.Year.ToString();
            string cmm;
            var mm = Convert.ToInt32(DateTime.Now.Month.ToString());

            if (mm < 10)
                cmm = "0" + mm;
            else
                cmm = "" + mm;

            var xyymm = yyyy + "-" + cmm;
            var xTypeSeries = typeSeries;
            //var a = API.Read<ResponseGetSeriesCode>("GetSeriesCode/"+ xYYYY + "/"+ xTypeSeries);
            //var a = API.Read<ResponseGetSeriesCode>("GetSeriesCode/2021/IC");
            var a = API.Read<ResponseGetSeriesCode>("api/SeriesCV/GetSeriesCode/" + xyymm + "/IM");

            return Ok(a);
        }

        public IActionResult GetSeriesCV(string yymm, string typeSeries)
        {
            var yyyy = DateTime.Now.Year.ToString();
            string cmm;
            var mm = Convert.ToInt32(DateTime.Now.Month.ToString());

            if (mm < 10)
                cmm = "0" + mm;
            else
                cmm = "" + mm;

            var xyymm = yyyy + "-" + cmm;
            var xTypeSeries = typeSeries;
            //var a = API.Read<ResponseGetSeriesCode>("GetSeriesCode/"+ xYYYY + "/"+ xTypeSeries);
            //var a = API.Read<ResponseGetSeriesCode>("GetSeriesCode/2021/IC");
            var a = API.Read<ResponseGetSeriesCode>("api/SeriesCV/GetSeriesCode/" + xyymm + "/CV");

            return Ok(a);
        }

        public IActionResult GetSeriesCode(string yymm, string typeSeries)
        {
            var yyyy = DateTime.Now.Year.ToString();
            string cmm;
            var mm = Convert.ToInt32(DateTime.Now.Month.ToString());

            if (mm < 10)
                cmm = "0" + mm;
            else
                cmm = "" + mm;

            var xyymm = yyyy + "-" + cmm;
            var xTypeSeries = typeSeries;
            //var a = API.Read<ResponseGetSeriesCode>("GetSeriesCode/"+ xYYYY + "/"+ xTypeSeries);
            //var a = API.Read<ResponseGetSeriesCode>("GetSeriesCode/2021/IC");
            var a = API.Read<ResponseGetSeriesCode>("api/SeriesCV/GetSeriesCode/" + xyymm + "/IC");

            return Ok(a);
        }

        public IActionResult GetWTQLine(string docentry)
        {
            var xDocEntry = docentry;
            var a = API.Read<ResponseGetWTQLine>("GetWTQLine/" + xDocEntry);
            return Ok(a);
        }

        public IActionResult GetBinLocationWhs(string whscode)
        {
            var xwhscode = whscode;
            var a = API.Read<ResponseGetBinLocation>("api/GetBinLocation/GetBinLocationWhs/" + xwhscode);
            return Ok(a);
        }

        //GetWTRLine
        public IActionResult GetWTRLine(string docentry)
        {
            var xDocEntry = docentry;
            var a = API.Read<ResponseGetWTRLine>("api/InventoryTransferIM/GetWTRLine/" + xDocEntry);
            return Ok(a);
        }

        public IActionResult GetStcok_BatchSerial(string barcode, string batchserial)
        {
            var xBatchSerial = batchserial;
            var xScanBarcode = barcode;

            var a = API.Read<ResponseGetStockBatchSerial>("GetStcokBatchSerial/" + xScanBarcode + "/" + xBatchSerial + "/" + xBatchSerial);
            return Ok(a);
        }

        public IActionResult GetStock_WhsBin(string whsCode, string binCode)
        {
            var xWhsCode = whsCode;
            var xBinCode = binCode;

            var a = API.Read<ResponseGetStockByWhsBin>("GetStockWhsBin/" + xWhsCode + "/" + xBinCode);
            return Ok(a);
        }

        public IActionResult GetStockItemBatchSerial(string barcode, string batchserial)
        {
            var xScanBarcode = barcode;
            var xBatchSerial = batchserial;
            var a = API.Read<ResponseGetStockItemBatchSerial>("GetStockItemBatchSerial/" + xScanBarcode + "/" + xBatchSerial + "/" + xBatchSerial);
            return Ok(a);
        }

        public IActionResult GetStockItemBatch(string itemcode, string batchnumber)
        {
            var xitemcode = itemcode;
            var xbatchnumber = batchnumber;
            var a = API.Read<ResponseGetStockItemBatchSerial>("GetStockItemBatch/" + xitemcode + "/" + xbatchnumber);
            return Ok(a);
        }

        public IActionResult GetStockItemBatchBin(string itemcode, string batchnumber, string binentry)
        {
            var xitemcode = itemcode;
            var xbatchnumber = batchnumber;
            var xbinentry = binentry;
            var a = API.Read<ResponseGetStockItemBatchSerial>("GetStockItemBatchBin/" + xitemcode + "/" + xbatchnumber + "/" + xbinentry);
            return Ok(a);
        }

        public IActionResult GetStockItemBatchW(string itemcode, string batchnumber, string whscode)
        {
            var a = API.Read<ResponseGetStockItemBatchSerial>("GetStockItemBatchW/" + itemcode + "/" + batchnumber + "/" + whscode);
            return Ok(a);
        }

        public IActionResult GetStockItemSerial(string itemcode, string serialnumber)
        {
            var xitemcode = itemcode;
            var xserialnumber = serialnumber;
            var a = API.Read<ResponseGetStockItemBatchSerial>("GetStockItemSerial/" + xitemcode + "/" + xserialnumber);
            return Ok(a);
        }

        public IActionResult GetStockItemSerialBin(string itemcode, string serialnumber, string binentry)
        {
            var xitemcode = itemcode;
            var xserialnumber = serialnumber;
            var xbinentry = binentry;
            var a = API.Read<ResponseGetStockItemBatchSerial>("GetStockItemSerialBin/" + xitemcode + "/" + xserialnumber + "/" + xbinentry);
            return Ok(a);
        }

        public IActionResult GetStockItemSerialW(string itemcode, string serialnumber, string whsCode)
        {
            var a = API.Read<ResponseGetStockItemBatchSerial>("GetStockItemSerialW/" + itemcode + "/" + serialnumber + "/" + whsCode);
            return Ok(a);
        }

        public IActionResult GetStockItemx(string docentry, string itemcode, string batchserialno)
        {
            var a = API.Read<ResponseScanItemsInIM>("GetStockItemx/" + docentry + "/" + itemcode + "/" + batchserialno);
            //var a = API.Read<ResponseScanItemsInIM>("GetStockItemx/19414/KS-10/Ma21071660004");
            return Ok(a);
        }

        [HttpPost]
        public IActionResult PostInventoryTransfer(SendInventoryTransfer sendInventoryTransfer)
        {
            var a = API.PostWithReturn<ResponseInventoryTransfer>("api/InventoryTransfer/SendInventoryTransfer", sendInventoryTransfer);
            return Ok(a);
        }

        [HttpPost]
        public IActionResult PostInventoryCounting(SendInventoryCounting sendInventoryCounting)
        {
            var a = API.PostWithReturn<ResponseInventoryCounting>("api/InventoryCounting/SendInventoryCounting", sendInventoryCounting);
            return Ok(a);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}