using System.Threading.Tasks;
using BarCodeAPIService.Service.Bank;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers.Bank
{

    [ApiController]
    [Route(APIRoute.Root)]
    public class GetBinLocationListController : ControllerBase
    {
        private readonly IGetBinLocationListService getBinLocationList;

        public GetBinLocationListController(IGetBinLocationListService getBinLocationList)
        {
            this.getBinLocationList = getBinLocationList;
        }

        [HttpGet("GetBinLocationList/{barcode}/{itemcode}")]
        public async Task<IActionResult> GetBinLocationListAsync(string barcode, string itemcode)
        {
            var a = await getBinLocationList.responseGetBinLocationList(barcode, itemcode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

    }
}
