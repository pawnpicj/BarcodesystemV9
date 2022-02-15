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
            {
                return Ok(a);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
