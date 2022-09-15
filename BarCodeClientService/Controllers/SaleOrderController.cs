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
using BarCodeLibrary.Respones.SAP.Vichika;

namespace BarCodeClientService.Controllers
{
    public class SaleOrderController : Controller
    {

        private readonly ILogger<SaleOrderController> _logger;

        public SaleOrderController(ILogger<SaleOrderController> logger)
        {
            _logger = logger;
        }

        public IActionResult CreateSaleOrder()
        {
            return View();
        }

        public IActionResult CreateDelivery()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetSO(string cardCode)
        {
            var a = API.Read<ResponseGetORDR>(APIRoute.Delivery.Controller + APIRoute.Delivery.GetSO + cardCode);
            if (a.ErrorCode!=0)
            {
                return BadRequest(a);
            }
            else
            {
                return Ok(a.Data);
            }
        }
        [HttpGet]
        public IActionResult GetBatchActionResult(string itemCode,string whsCode)
        {
            var a = API.Read<ResponseGetBatch>(APIRoute.Delivery.Controller + APIRoute.Delivery.GetBatch + itemCode + "/"+whsCode);
            if (a.ErrorCode != 0)
            {
                return BadRequest(a);
            }
            else
            {
                return Ok(a.Data);
            }
        }
        [HttpGet]
        public IActionResult GetSerialActionResult(string itemCode, string whsCode)
        {
            var a = API.Read<ResponseGetSerial>(APIRoute.Delivery.Controller + APIRoute.Delivery.GetSerial + itemCode + "/" + whsCode);
            if (a.ErrorCode != 0)
            {
                return BadRequest(a);
            }
            else
            {
                return Ok(a.Data);
            }
        }

        [HttpPost]
        public IActionResult PostDeliveryActionResult(SendDelivery sendDelivery)
        {
            //return Ok(sendDelivery);
            var a = API.PostWithReturn<ResponseDelivery>(APIRoute.Delivery.Controller + APIRoute.Delivery.POSTDelivery, sendDelivery);
            if (a.ErrorCode != 0)
            {
                return BadRequest(a);
            }
            else
            {
                return Ok(a);
            }
        }
        //public IActionResult GetSeriesCode(string yyyy, string typeSeries)
        //{
        //    string yy = DateTime.Now.Year.ToString();
        //    string cmm;
        //    int mm = Convert.ToInt32(DateTime.Now.Month.ToString());

        //    if (mm < 10)
        //    {
        //        cmm = "0" + mm;
        //    }
        //    else
        //    {
        //        cmm = "" + mm;
        //    }

        //    string xyymm = yy + "-" + cmm;
        //    string xTypeSeries = typeSeries;
        //    //var a = API.Read<ResponseGetSeriesCode>("GetSeriesCode/"+ xYYYY + "/"+ xTypeSeries);
        //    //var a = API.Read<ResponseGetSeriesCode>("GetSeriesCode/2021/IC");
        //    var a = API.Read<ResponseGetSeriesCode>("api/SeriesCV/GetSeriesCode/" + xyymm + "/DE");

        //    return Ok(a);
        //}

        //public IActionResult ListSO()
        //{
        //    ResponseGetDataFromSO responseGetDataFromSO = new ResponseGetDataFromSO();
        //    responseGetDataFromSO.Data = SOStatic.Data;
        //    SOStatic.Data = null;
        //    return View(responseGetDataFromSO);
        //}

        //[HttpPost]
        //public IActionResult PostDataFromSO(ResponseGetDataFromSO sdata)
        //{
        //    SOStatic.Data = sdata.Data;
        //    return Ok(1);
        //}

        //public IActionResult frmDelivery()
        //{
        //    ResponseGetSOLine responseGetSOLine = new ResponseGetSOLine();
        //    responseGetSOLine.Data = SOLStatic.Data;
        //    SOLStatic.Data = null;
        //    return View(responseGetSOLine);
        //    //return View();
        //}

        //[HttpPost]
        //public IActionResult PostDataDelivery(ResponseGetSOLine sdata)
        //{
        //    SOLStatic.Data = sdata.Data;
        //    return Ok(1);
        //}

        //[HttpPost]
        //public IActionResult PostDeliveryToSAP(SendDelivery sendDelivery)
        //{
        //    var a = API.PostWithReturn<ResponseDelivery>("api/Delivery/SendDelivery", sendDelivery);
        //    return Ok(a);
        //}

    }
}
