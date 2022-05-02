using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class InventoryTransferIMController : ControllerBase
    {
        private readonly IInventoryTransferIMService inventoryTransferIMService;

        public InventoryTransferIMController(IInventoryTransferIMService inventoryTransferIMService)
        {
            this.inventoryTransferIMService = inventoryTransferIMService;
        }

        [HttpGet("GetIFIM")]
        public async Task<IActionResult> GetInventoryTransferIMAsync()
        {
            var a = await inventoryTransferIMService.responseGetOWTR();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest();
        }

        [HttpGet("GetWTRLine/{DocEntry}")]
        public async Task<IActionResult> GetWTRLineAsync(int DocEntry)
        {
            var a = await inventoryTransferIMService.responseGetWTRLine(DocEntry);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}