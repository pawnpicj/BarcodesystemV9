using System.Threading.Tasks;
using BarCodeAPIService.Service.Bank;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers.Bank
{

    [ApiController]
    [Route(APIRoute.Root)]
    public class GetBinLocationController : ControllerBase
    {
        private readonly IGetBinLocationService getBinLocation;

        public GetBinLocationController(IGetBinLocationService getBinLocation)
        {
            this.getBinLocation = getBinLocation;
        }

        [HttpGet("GetBinLocationWhs/{whscode}")]
        public async Task<IActionResult> GetBinLocationAsync(string whscode)
        {
            var a = await getBinLocation.responseGetBinLocation(whscode);
            return Ok(a);
        }

        [HttpGet("GetBinLocationCounting/{whscode}/{iyear}")]
        public async Task<IActionResult> GetBinLocationCountingAsync(string whscode, string iyear)
        {
            var a = await getBinLocation.responseGetBinLocationCounting(whscode, iyear);
            return Ok(a);
        }

    }
}
