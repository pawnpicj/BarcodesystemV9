using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.APICall;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeClientService.Controllers
{
    public class BinLocationController : Controller
    {
                                                                                                                                                
        public IActionResult GBinLocation()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetWarehouseWhs()
        {
            var a = API.Read<ResponseOWHSGetWarehouse>("api/Warehouse/GetWarehouse");
            return Ok(a);
        }
    }
}
