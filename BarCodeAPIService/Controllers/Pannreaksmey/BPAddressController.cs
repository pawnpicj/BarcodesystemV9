using System.Threading.Tasks;
using BarCodeAPIService.Service;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class BPAddressController : ControllerBase
    {
        private readonly IBPAddressService bpAddress;

        public BPAddressController(IBPAddressService bpAddress)
        {
            this.bpAddress = bpAddress;
        }

        [HttpGet]
        public async Task<IActionResult> GetBPAddress()
        {
            var a = await bpAddress.responseCRD1Address();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}