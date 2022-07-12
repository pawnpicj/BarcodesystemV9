using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class BatchNumberController : ControllerBase
    {
        private readonly IBatchNumberService batchNumber;

        public BatchNumberController(IBatchNumberService batchNumber)
        {
            this.batchNumber = batchNumber;
        }

        [HttpGet]
        public async Task<IActionResult> GetBatchNumberAsync()
        {
            var a = await batchNumber.ResponseOIBTGetBatch();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}