using System.Diagnostics;
using System.Threading.Tasks;
using BarCodeClientService.Models;
using BarCodeLibrary.APICall;
using BarCodeLibrary.Respones.SAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using BarCodeLibrary.Respones.SAP.Home;

namespace BarCodeClientService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginUser(string user,string password)
        {
            var a = API.Read<LoginAsync>("api/Home/GetLogin?user="+user+"&&password="+password+"");//r='" + user + "'&&password='" + password + "'");
            if (a.ID != 0)
            {
                return Ok(a);
            }
            else
            {
                return Ok(a);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
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
            var a = API.Read<ResponseOPDNGetGoodReceipt>("api/GoodsReturn/GetGoodsReceiptPO");
            return Ok(a);
        }

        public IActionResult GetContactPerson()
        {
            var a = API.Read<ResponseOCPRGetContactPerson>("api/ContactPerson");
            return Ok(a);
        }

        public IActionResult GetSaleEmployee()
        {
            var a = API.Read<ResponseOSLPGetSalesEmployee>("api/SaleEmployee");
            return Ok(a);
        }

        public IActionResult GetWarehouse()
        {
            var a = API.Read<ResponseOWHSGetWarehouse>("api/Warehouse/GetWarehouse");
            return Ok(a);
        }

        public IActionResult GetBinLocation()
        {
            var a = API.Read<ResponseOBINGetBinCode>("api/BinCode/GetBinCode");
            return Ok(a);
        }

        public IActionResult hGetOWTQ()
        {
            var a = API.Read<ResponseGetOWTQ>("api/InventoryTransferRequest/GetIFR");
            return Ok(a);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}