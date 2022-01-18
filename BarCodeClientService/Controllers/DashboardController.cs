using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeClientService.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult DashboardSaleAR()
        {
            return View();
        }
        public IActionResult DashboardInventory()
        {
            return View();
        }
        public IActionResult DashboardPurchasingAP()
        {
            return View();
        }
    }
}
