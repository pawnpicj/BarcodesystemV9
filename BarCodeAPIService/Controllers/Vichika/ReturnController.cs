using System.Threading.Tasks;
using BarCodeAPIService.Service;
using BarCodeAPIService.Service.Vichika;
using BarCodeLibrary.Request.SAP;
using BarCodeLibrary.Request.SAP.Vichika;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers.Vichika
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class ReturnController : ControllerBase
    {
        private readonly IReturnService returnService;

        public ReturnController(IReturnService returnService)
        {
            this.returnService = returnService;
        }

        [HttpGet(APIRoute.Return.GetDelivery + "{cardCode?}")]
        public async Task<IActionResult> GetGoodsReceiptPOAsync(string? cardCode)
        {
            var a = await returnService.responseODLNGetDelivery(cardCode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpPost(APIRoute.Return.SendReturn)]
        public async Task<IActionResult> SendGoodsReturnAsync(SendReturn sendGoodsReturn)
        {
            var a = await returnService.sendGoodReturn(sendGoodsReturn);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet(APIRoute.Return.GetDeliveryByDocNum + "{DocNum}")]
        public async Task<IActionResult> GetGoodsReceiptPOByDocNumAsync(string DocNum)
        {
            var a = await returnService.responseODLNGetDeliveryByDocNum(DocNum);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}
