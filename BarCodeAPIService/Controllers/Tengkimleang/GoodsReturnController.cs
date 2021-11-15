using BarCodeAPIService.Service;
using BarCodeLibrary.Request.SAP;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarCodeAPIService.Controllers.Tengkimleang
{
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
            {
                return Ok(a);
            }
            else
            {
                return BadRequest(a);
            }
        }
        [HttpPost("SendGoodsReturn")]
        public async Task<IActionResult> SendGoodsReturnAsync(SendGoodsReturn sendGoodsReturn)
        {
            var a = await goodReturnService.sendGoodReturn(sendGoodsReturn);
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
