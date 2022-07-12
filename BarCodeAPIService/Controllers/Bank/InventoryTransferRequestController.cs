using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class InventoryTransferRequestController : ControllerBase
    {
        private readonly IInventoryTransferRequestService inventoryTransferRequestService;

        public InventoryTransferRequestController(IInventoryTransferRequestService inventoryTransferRequestService)
        {
            this.inventoryTransferRequestService = inventoryTransferRequestService;
        }

        [HttpGet("GetIFR")]
        public async Task<IActionResult> GetInventoryTransferRequestAsync()
        {
            var a = await inventoryTransferRequestService.responseGetOWTQ();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest();
        }
    }
}