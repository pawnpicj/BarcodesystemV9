using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class InventoryTransferController : ControllerBase
    {
        private readonly IInventoryTransferService  inventoryTransfer;

        public InventoryTransferController(IInventoryTransferService inventoryTransfer)
        {
            this.inventoryTransfer = inventoryTransfer;
        }

        [HttpPost("SendInventoryTransfer")]
        public async Task<IActionResult> PostInventoryTransferAsync(SendInventoryTransfer sendinventoryTransfer)
        {
            var a = await inventoryTransfer.responseInventoryTransfer(sendinventoryTransfer);
            if (a.ErrorCode == 0)
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
    }
}
