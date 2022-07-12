using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class BinCodeController : ControllerBase
    {
        private readonly IBinCodeService binCode;

        public BinCodeController(IBinCodeService binCode)
        {
            this.binCode = binCode;
        }

        [HttpGet("GetBinCode")]
        public async Task<IActionResult> GetBinCodeAnsync()
        {
            var a = await binCode.ResponseOBINGetBinCode();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}