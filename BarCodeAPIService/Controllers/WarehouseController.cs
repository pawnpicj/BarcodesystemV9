using BarCodeLibrary.Respones.SAP;
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
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService warehouse;

        public WarehouseController(IWarehouseService warehouse) {
            this.warehouse = warehouse;
        }
        public async Task<IActionResult> GetWarehouseAsync() {
            var a = await warehouse.responseWHSGetWarehouse();
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
