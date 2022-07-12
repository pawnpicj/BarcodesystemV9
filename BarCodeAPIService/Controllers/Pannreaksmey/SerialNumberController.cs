using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

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
                return Ok(a);
            return BadRequest(a);
        }
    }
}