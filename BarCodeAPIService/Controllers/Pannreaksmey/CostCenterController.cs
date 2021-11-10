using Barcodesystem.Contract.RouteApi;
using BarCodeAPIService.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class CostCenterController : ControllerBase
    {
        private readonly ICostCenterService costCenter;

        public CostCenterController(ICostCenterService costCenter) {
            this.costCenter = costCenter;
        }
        [HttpGet]
        public async Task<IActionResult> GetCostCenterAsync()
        {
            var a = await costCenter.ResponseOPRCGetCostCenter();
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else {
                return BadRequest(a);
            }

        }
    }
}
