using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class BPMasterDataController : ControllerBase
    {
        private readonly IBPMasterDataService bPMasterData;

        public BPMasterDataController(IBPMasterDataService bPMasterData)
        {
            this.bPMasterData = bPMasterData;
        }

        [HttpGet("GetBP")]
        public async Task<IActionResult> GetBPMasterDataAsync()
        {
            var a = await bPMasterData.ResponseOCRDGetBP();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}