using System;
using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Barcodesystem.Request.Token;
using Barcodesystem.Respones.Token;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService tokenService;

        public AuthController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        #region Post Method

        [HttpPost]
        [Route(APIRoute.Token.Login)]
        public async Task<ActionResult<LoginResponse>> Register(LoginRequest login)
        {
            try
            {
                var a = await tokenService.LoginAsync(login);
                if (a.ErrorCode == 0)
                    return Ok(a);
                return BadRequest(a);
            }
            catch (Exception ex)
            {
                Log.Error("Something went while generate token " + ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route(APIRoute.Token.RefreshToken)]
        public async Task<ActionResult<RefreshResponse>> Refresh(RefreshRequest refreshRequest)
        {
            try
            {
                var a = await tokenService.RefrestAsync(refreshRequest);
                if (a.ErrorCode == 0)
                    return Ok(a);
                return BadRequest(a);
            }
            catch (Exception ex)
            {
                Log.Error("Something went while generate token " + ex);
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}