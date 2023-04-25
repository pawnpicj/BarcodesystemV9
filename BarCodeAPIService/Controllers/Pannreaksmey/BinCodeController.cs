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
        private readonly IBinCodeService getBinCode;

        public BinCodeController(IBinCodeService getBinCode)
        {
            this.getBinCode = getBinCode;
        }

        [HttpGet("GetBinCode")]
        public async Task<IActionResult> GetBinCodeAnsync()
        {
            var a = await getBinCode.ResponseOBINGetBinCode();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetBinCodeByCode/{binCode}")]
        public async Task<IActionResult> GetBinCodeByCodeAnsync(string binCode)
        {
            var a = await getBinCode.ResponseGetBinCodeByCode(binCode);
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