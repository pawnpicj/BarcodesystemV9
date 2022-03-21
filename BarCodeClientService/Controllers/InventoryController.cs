using BarCodeClientService.Models;
using BarCodeLibrary.APICall;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Bank;
using BarCodeLibrary.Respones.SAP.Tengkimleang;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        public IActionResult GetSeriesIM()
        {
            var a = API.Read<ResponseNNM1_IM>("api/SeriesIM");
            return Ok(a);
        }

        public IActionResult GetSeriesCV()
        {
            var a = API.Read<ResponseNNM1_CV>("api/SeriesCV");
            return Ok(a);
        }

        public IActionResult GetSeriesCode(string yyyy, string typeSeries)
        {
            string currentYear = DateTime.Now.Year.ToString();
            string xYYYY = "2021";
            string xTypeSeries = typeSeries;
            //var a = API.Read<ResponseGetSeriesCode>("GetSeriesCode/"+ xYYYY + "/"+ xTypeSeries);
            //var a = API.Read<ResponseGetSeriesCode>("GetSeriesCode/2021/IC");
            var a = API.Read<ResponseGetSeriesCode>("api/SeriesCV/GetSeriesCode/" + xYYYY + "/IC");
            
            return Ok(a);
        }

        public IActionResult GetWTQLine(string docentry)
        {
            string xDocEntry = docentry;
            var a = API.Read<ResponseGetWTQLine>("GetWTQLine/"+ xDocEntry);
            return Ok(a);
        }

        //GetWTRLine
        public IActionResult GetWTRLine(string docentry)
        {
            string xDocEntry = docentry;
            var a = API.Read<ResponseGetWTRLine>("api/InventoryTransferIM/GetWTRLine/" + xDocEntry);
            return Ok(a);
        }

        public IActionResult GetStcok_BatchSerial(string barcode, string batchserial)
        {
            string xBatchSerial = batchserial;
            string xScanBarcode = barcode;

            var a = API.Read<ResponseGetStockBatchSerial>("GetStcokBatchSerial/" + xScanBarcode + "/" + xBatchSerial + "/" + xBatchSerial);
            return Ok(a);
        }

        public IActionResult GetStock_WhsBin(string whsCode, string binCode)
        {
            string xWhsCode = whsCode;
            string xBinCode = binCode;

            var a = API.Read<ResponseGetStockByWhsBin>("GetStockWhsBin/" + xWhsCode + "/" + xBinCode);
            return Ok(a);
        }

        public IActionResult GetStockItemBatchSerial(string barcode, string batchserial)
        {
            string xScanBarcode = barcode;
            string xBatchSerial = batchserial;
            var a = API.Read<ResponseGetStockItemBatchSerial>("GetStockItemBatchSerial/" + xScanBarcode + "/" + xBatchSerial + "/" + xBatchSerial);
            return Ok(a);
        }

        public IActionResult GetStockItemBatch(string itemcode, string batchnumber)
        {
            string xitemcode = itemcode;
            string xbatchnumber = batchnumber;
            var a = API.Read<ResponseGetStockItemBatchSerial>("GetStockItemBatch/" + xitemcode + "/" + xbatchnumber);
            return Ok(a);
        }

        public IActionResult GetStockItemBatchBin(string itemcode, string batchnumber, string binentry)
        {
            string xitemcode = itemcode;
            string xbatchnumber = batchnumber;
            string xbinentry = binentry;
            var a = API.Read<ResponseGetStockItemBatchSerial>("GetStockItemBatchBin/" + xitemcode + "/" + xbatchnumber + "/" + xbinentry);
            return Ok(a);
        }

        public IActionResult GetStockItemSerial(string itemcode, string serialnumber)
        {
            string xitemcode = itemcode;
            string xserialnumber = serialnumber;
            var a = API.Read<ResponseGetStockItemBatchSerial>("GetStockItemSerial/" + xitemcode + "/" + xserialnumber);
            return Ok(a);
        }

        public IActionResult GetStockItemSerialBin(string itemcode, string serialnumber, string binentry)
        {
            string xitemcode = itemcode;
            string xserialnumber = serialnumber;
            string xbinentry = binentry;
            var a = API.Read<ResponseGetStockItemBatchSerial>("GetStockItemSerialBin/" + xitemcode + "/" + xserialnumber + "/" + xbinentry);
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
