using BarCodeAPIService.Service;
using BarCodeLibrary.APICall;
using BarCodeLibrary.Request.SAP.Pannreaksmey;
using BarCodeLibrary.Respones.SAP.Pannreaksmey;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeClientService.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserSetup()
        {
            var a=API.Read<ResponseDepartment>("api/Department/GetDepartment");
            return View(a);
        }
        [HttpGet]
        public IActionResult GetDepartment()
        {
            var a=API.Read<ResponseDepartment>("api/Department/GetDepartment");
            return Ok(a);
        }
        [HttpPost]
        public IActionResult PostDepartment(DEP dep)
        {
            var a = API.PostWithReturn<ResponseDepartment>("api/Department/PostDepartment",dep);
            return Ok(a);
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
        [HttpPost]
        public IActionResult UpdateUserAsync(SendUser send)
        {
            var a = API.PostWithReturn<ResponsePostUser>("api/User/UpdateUser",send);
            return Ok(a);
        }
    }
}