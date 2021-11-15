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
    public class SerialNumberController : ControllerBase
    {
        private readonly ISerialNumberService serialNumber;

        public SerialNumberController(ISerialNumberService serialNumber)
        {
            this.serialNumber = serialNumber;
        }
        [HttpGet]
        public async Task<IActionResult> GetSerialNumber()
        {
            var a = await serialNumber.ResponseOSRIGetSerial();
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
