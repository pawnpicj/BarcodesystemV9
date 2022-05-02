using System.Threading.Tasks;
using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService Delivery;

        public DeliveryController(IDeliveryService Delivery)
        {
            this.Delivery = Delivery;
        }

        [HttpGet("GetSO")]
        public async Task<IActionResult> GetDeliveryAsyc()
        {
            var a = await Delivery.responseGetORDR();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetSOLine/{DocEntry}")]
        public async Task<IActionResult> GetSOLineAsync(int DocEntry)
        {
            var a = await Delivery.responseGetORDRLine(DocEntry);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpPost("SendDelivery")]
        public async Task<IActionResult> PostDeliveryAsync(SendDelivery sendDelivery)
        {
            var a = await Delivery.PostDelivery(sendDelivery);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}