using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.APICall;
using BarCodeLibrary.Respones.SAP;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;

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
        [HttpGet]
        public IActionResult GetBinLocation()
        {
            var a = API.Read<ResponseOBINGetBinCode>("api/BinCode/GetBinCode");
            return Ok(a);
        }
        [HttpGet]
        public IActionResult GetGenerateBinCode()
        {
            var a = API.Read < ResponeNNG1GetGenerateBinCode > ("api/GenerateBinCode/GetGenerateBinCode");
            return Ok(a);
        }
    }
}
