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
        public IActionResult GetDataX()
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

        [HttpPost]
        public IActionResult PostInventoryTransfer(SendInventoryTransfer sendInventoryTransfer)
        {
            var a = API.PostWithReturn<ResponseInventoryTransfer>("api/InventoryTransfer/SendInventoryTransfer", sendInventoryTransfer);
            return Ok(a);
        }

       [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
