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
    public class BPAddressController : Controller
    {
        private readonly IBPAddressService bpAddress;

        public BPAddressController(IBPAddressService bpAddress) {
            this.bpAddress = bpAddress;
        }
        [HttpGet]
        public async Task<IActionResult> GetBPAddress()
        {
            var a = await bpAddress.ResponseCRD1Address();
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
