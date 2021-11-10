using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class BatchNumberController : ControllerBase
    {
        private readonly IBatchNumberService batchNumber;

        public BatchNumberController(IBatchNumberService batchNumber){
            this.batchNumber = batchNumber;
        }
        [HttpGet]
        public async Task<IActionResult> GetBatchNumberAsync()
        {
            var a = await batchNumber.ResponseOIBTGetBatch();
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
