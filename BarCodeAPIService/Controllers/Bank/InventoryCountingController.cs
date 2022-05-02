using System.Threading.Tasks;
using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class InventoryCountingController : ControllerBase
    {
        private readonly IInventoryCountingService inventoryCounting;

        public InventoryCountingController(IInventoryCountingService inventoryCounting)
        {
            this.inventoryCounting = inventoryCounting;
        }

        [HttpPost("SendInventoryCounting")]
        public async Task<IActionResult> PostInventoryCountingAsync(SendInventoryCounting sendinventoryCounting)
        {
            var a = await inventoryCounting.responseInventoryCounting(sendinventoryCounting);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}