using Microsoft.AspNetCore.Mvc;

namespace BarCodeClientService.Controllers
{
    public class DeliveryReturnController : Controller
    {
        public IActionResult CreateDeliveryReturn()
        {
            return View();
        }
    }
}