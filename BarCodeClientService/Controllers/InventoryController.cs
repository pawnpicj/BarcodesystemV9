using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeClientService.Controllers
{
    public class InventoryController : Controller
    {
        public IActionResult CreateInventoryTransfer()
        {
            return View();
        }
    }
}
