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

        [HttpGet(APIRoute.GoodReturn.GetGoodReceiptPO+"{cardCode}")]
        public async Task<IActionResult> GetGoodsReceiptPOAsync(string cardCode)
        {
            var a = await goodReturnService.responseOPDNGetGoodReceipt(cardCode);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpPost(APIRoute.GoodReturn.SendGoodsReturn)]
        public async Task<IActionResult> SendGoodsReturnAsync(SendGoodsReturn sendGoodsReturn)
        {
            var a = await goodReturnService.sendGoodReturn(sendGoodsReturn);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }

        [HttpGet(APIRoute.GoodReturn.GetGoodReceiptPOByDocNum + "{DocNum}")]
        public async Task<IActionResult> GetGoodsReceiptPOByDocNumAsync(string DocNum)
        {
            var a = await goodReturnService.responseOPDNGetGoodReceiptByDocNum(DocNum);
            if (a.ErrorCode == 0)
                return Ok(a);
            return BadRequest(a);
        }
    }
}