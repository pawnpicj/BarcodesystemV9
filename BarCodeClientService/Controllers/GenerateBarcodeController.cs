using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.APICall;
using BarCodeLibrary.Respones.SAP;

namespace BarCodeClientService.Controllers
{
    public class GenerateBarcodeController : Controller
    {
        public IActionResult CreateBinLocation()
        {

            return View();
        }
    }
}
