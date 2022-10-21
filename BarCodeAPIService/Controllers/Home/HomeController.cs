using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeAPIService.Service.Home;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace BarCodeAPIService.Controllers.Home
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        [HttpGet("GetLogin")]
        public async Task<IActionResult> GetLoginAsync(string user,string password)
        {
            var a = await homeService.GetLoginAsync(user,password);
            if (a.ID != 0) { 
                return Ok(a); 
            } else
            {
                return Ok(a);
            }
        }
    }
}
