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

        [HttpGet(APIRoute.Delivery.GetSO+ "{CardCode}/{TypeShow}")]
        public async Task<IActionResult> GetDeliveryAsyc(string CardCode, string TypeShow)
        {
            var a = await Delivery.responseGetORDR(CardCode, TypeShow);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet(APIRoute.Delivery.GetBatch + "{itemCode}/{WhsCode}")]
        public async Task<IActionResult> GetBatchAsync(string itemCode,string WhsCode)
        {
            var a = await Delivery.responseGetBatch(itemCode,WhsCode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet(APIRoute.Delivery.GetSerial + "{itemCode}/{WhsCode}")]
        public async Task<IActionResult> GetSerialAsyc(string itemCode, string WhsCode)
        {
            var a = await Delivery.responseGetSerial(itemCode, WhsCode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpPost(APIRoute.Delivery.POSTDelivery)]
        public async Task<IActionResult> PostDeliveryAsync(SendDelivery sendDelivery)
        {
            var a = await Delivery.PostDelivery(sendDelivery);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet(APIRoute.Delivery.GetSaleOrderList)]
        public async Task<IActionResult> GetSaleOrderListAsync()
        {
            var a = await Delivery.responseGetSaleOrder();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet("GetSODelivery/{cardCode}")]
        public async Task<IActionResult> GetSODeliveryAsyc(string cardCode)
        {
            var a = await Delivery.responseGetSO(cardCode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

    }
}