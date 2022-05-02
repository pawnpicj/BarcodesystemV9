using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class CostCenterController : ControllerBase
    {
        private readonly ICostCenterService costCenter;

        public CostCenterController(ICostCenterService costCenter)
        {
            this.costCenter = costCenter;
        }

        [HttpGet]
        public async Task<IActionResult> GetCostCenterAsync()
        {
            var a = await costCenter.ResponseOPRCGetCostCenter();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}