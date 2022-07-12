using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class GLAccountController : ControllerBase
    {
        private readonly IGLAccountService gLAccount;

        public GLAccountController(IGLAccountService gLAccount)
        {
            this.gLAccount = gLAccount;
        }

        [HttpGet]
        public async Task<IActionResult> GetGLAccountAsync()
        {
            var a = await gLAccount.ResponseOACTGetGLAccount();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}