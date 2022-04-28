using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarCodeLibrary.Request.SAP.Pannreaksmey;

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
        public async Task<IActionResult> GetUserAsync()
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
        [HttpPost("PostUser")]
        public async Task<IActionResult> PostUserAsync(SendUser send)
        {
             return Ok(await user.ResponsePostUserAsync(send));
        }
        [HttpGet("GetUserLogin")]
        public async Task<IActionResult> GetUserLoginAsync()
        {
            var a=await user.RespponseGetuser();
            return Ok(a) ;
        }
    }
}
