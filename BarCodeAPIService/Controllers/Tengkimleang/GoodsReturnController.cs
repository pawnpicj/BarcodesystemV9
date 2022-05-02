using System.Threading.Tasks;
using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP;
using Barcodesystem.Contract.RouteApi;
using Microsoft.AspNetCore.Mvc;

namespace BarCodeAPIService.Controllers.Tengkimleang
{
    [ApiController]
    [Route(APIRoute.Root)]
    public class GoodsReturnController : ControllerBase
    {
        private readonly IGoodReturnService goodReturnService;

        public GoodsReturnController(IGoodReturnService goodReturnService)
        {
            this.goodReturnService = goodReturnService;
        }

        [HttpGet("GetGoodsReceiptPO")]
        public async Task<IActionResult> GetGoodsReceiptPOAsync()
        {
            var a = await goodReturnService.responseOPDNGetGoodReceipt();
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpPost("SendGoodsReturn")]
        public async Task<IActionResult> SendGoodsReturnAsync(SendGoodsReturn sendGoodsReturn)
        {
            var a = await goodReturnService.sendGoodReturn(sendGoodsReturn);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}