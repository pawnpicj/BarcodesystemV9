using BarCodeClientService.Models;
using BarCodeLibrary.APICall;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Respones.SAP;
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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
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
        public IActionResult SaleARCreation()
        {
            return View();
        }
        public IActionResult InventoryCreation()
        {
            return View();
        }

        public IActionResult GetAPITesting()
        {
            var a = API.Read<List<ResponseOPDNGetGoodReceipt>>("GetGoodReceiptPO");
            return Ok("Hello");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
