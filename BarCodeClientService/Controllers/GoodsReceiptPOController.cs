using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeClientService.Controllers
{
    public class GoodsReceiptPOController : Controller
    {
        public IActionResult CreatePO()
        {
            return View();
        }
    }
}
