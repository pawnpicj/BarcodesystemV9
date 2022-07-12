using BarCodeAPIService.Service;
using BarCodeLibrary.APICall;
using BarCodeLibrary.Request.SAP.Pannreaksmey;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeClientService.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService user;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserSetup()
        {
            return View();
        }
        public IActionResult Authorize()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PostUserAsync(SendUser send)
        {
            var a = API.PostWithReturn<ResponsePostUser>("api/User/PostUser", send);
            return Ok(a);
        }

        [HttpGet]
        public IActionResult GetUserLogin()
        {
            var a = API.Read<ResponseGetUser>("api/User/GetUserLogin");
            return Ok(a);
        }
    }
}