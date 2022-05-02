using System;
using BarCodeLibrary.APICall;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Bank;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BarCodeClientService.Controllers
{
    public class SaleOrderController : Controller
    {
        private readonly ILogger<SaleOrderController> _logger;

        public SaleOrderController(ILogger<SaleOrderController> logger)
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

        public IActionResult CreateSaleOrder()
        {
            return View();
        }

        public IActionResult GetSO()
        {
            var a = API.Read<ResponseGetORDR>("api/Delivery/GetSO");
            return Ok(a);
        }

        public IActionResult GetSOLine(string docentry)
        {
            var xDocEntry = docentry;
            var a = API.Read<ResponseGetORDRLine>("api/Delivery/GetSOLine/" + xDocEntry);
            return Ok(a);
        }


        public IActionResult GetSeriesCode(string yyyy, string typeSeries)
        {
            var currentYear = DateTime.Now.Year.ToString();
            var xYYYY = "2021";
            var xTypeSeries = typeSeries;
            //var a = API.Read<ResponseGetSeriesCode>("GetSeriesCode/"+ xYYYY + "/"+ xTypeSeries);
            //var a = API.Read<ResponseGetSeriesCode>("GetSeriesCode/2021/IC");
            var a = API.Read<ResponseGetSeriesCode>("api/SeriesCV/GetSeriesCode/" + xYYYY + "/DE");

            return Ok(a);
        }
    }
}