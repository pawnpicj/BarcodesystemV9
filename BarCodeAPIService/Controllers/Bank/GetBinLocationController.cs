using System.Threading.Tasks;
using BarCodeAPIService.Service.Bank;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class GetBinLocationController : ControllerBase
    {
        private readonly IGetBinLocationService binCode;

        public GetBinLocationController(IGetBinLocationService binCode)
        {
            this.binCode = binCode;
        }

        [HttpGet("GetBinLocationWhs/{whscode}")]
        public async Task<IActionResult> GetBinLocationAnsync(string whscode)
        {
            var a = await binCode.responseGetBinLocation(whscode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}