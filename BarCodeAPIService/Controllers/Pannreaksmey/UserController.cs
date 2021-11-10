using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class UserController : ControllerBase
    {
        private readonly IUserService user;

        public UserController(IUserService user)
        {
            this.user = user;
        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var a = await user.ResponseOUSRGetUser();
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
    }
}
